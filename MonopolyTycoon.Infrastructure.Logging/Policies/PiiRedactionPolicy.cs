using Serilog.Core;
using Serilog.Events;
using System.Reflection;
using Serilog.Debugging;

namespace MonopolyTycoon.Infrastructure.Logging.Policies
{
    /// <summary>
    /// Implements a Serilog destructuring policy to inspect objects being logged and
    /// redact properties that are considered Personally Identifiable Information (PII).
    /// This is a critical security component to fulfill REQ-1-022.
    /// The policy operates on property names, making it decoupled from specific domain types.
    /// It is internal as it's an implementation detail of the logging factory.
    /// </summary>
    internal sealed class PiiRedactionPolicy : IDestructuringPolicy
    {
        // A set of property names (case-insensitive) that should be redacted from logs.
        // As per SDS, this includes 'DisplayName', which overrides the permission in REQ-1-022 for a safer default.
        private static readonly HashSet<string> SensitivePropertyNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "DisplayName",
            "PlayerName",
            "ProfileName",
            "FullName",
            "Email",
            "Address",
            "Password",
            "CreditCardNumber",
            "Ssn"
        };

        private const string RedactedPlaceholder = "[REDACTED]";

        /// <summary>
        /// Attempts to convert a complex object into a LogEventPropertyValue, redacting sensitive properties.
        /// </summary>
        /// <param name="value">The object being logged.</param>
        /// <param name="propertyValueFactory">A factory to create log property values.</param>
        /// <param name="result">The resulting destructured value.</param>
        /// <returns>True if the policy was applied; false otherwise.</returns>
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value is null || value.GetType().IsPrimitive || value is string)
            {
                result = null!;
                return false;
            }

            try
            {
                var type = value.GetType();
                // We only apply this policy to non-anonymous, non-system complex types.
                if (type.Namespace != null && (type.Namespace.StartsWith("System") || type.IsAnonymous()))
                {
                    result = null!;
                    return false;
                }

                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                     .Where(p => p.CanRead);

                var redactedProperties = new List<LogEventProperty>();

                foreach (var property in properties)
                {
                    object? propertyValue;
                    try
                    {
                        propertyValue = property.GetValue(value);
                    }
                    catch (Exception ex)
                    {
                        // In case a property getter throws, log it and move on.
                        SelfLog.WriteLine("PiiRedactionPolicy failed to get value for property {0}: {1}", property.Name, ex);
                        propertyValue = "Error getting value";
                    }

                    if (SensitivePropertyNames.Contains(property.Name))
                    {
                        redactedProperties.Add(new LogEventProperty(property.Name, new ScalarValue(RedactedPlaceholder)));
                    }
                    else
                    {
                        // Let Serilog handle the destructuring of the property's value recursively.
                        var destructuredValue = propertyValueFactory.CreatePropertyValue(propertyValue, destructureObjects: true);
                        redactedProperties.Add(new LogEventProperty(property.Name, destructuredValue));
                    }
                }

                result = new StructureValue(redactedProperties, type.Name);
                return true;
            }
            catch (Exception ex)
            {
                // Defensive catch-all. If redaction fails for any reason, we must not crash the logging pipeline.
                SelfLog.WriteLine("PiiRedactionPolicy encountered an unhandled exception: {0}", ex);
                result = new ScalarValue("Failed to destructure object with PII redaction.");
                return false; // Let the default behavior take over if possible.
            }
        }
    }
    
    internal static class TypeExtensions
    {
        internal static bool IsAnonymous(this Type type)
        {
            return Attribute.IsDefined(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false)
                   && type.IsGenericType && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                   && type.Attributes.HasFlag(TypeAttributes.NotPublic);
        }
    }
}