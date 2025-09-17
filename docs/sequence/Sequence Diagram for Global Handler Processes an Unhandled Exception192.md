sequenceDiagram
    participant "GlobalExceptionHandler" as GlobalExceptionHandler
    participant "ILogger" as ILogger
    participant "LoggingService" as LoggingService
    participant "Local File System" as LocalFileSystem
    participant "ViewManager" as ViewManager

    activate GlobalExceptionHandler
    GlobalExceptionHandler->>GlobalExceptionHandler: 1. 1. Catch(UnhandledExceptionEventArgs args)
    GlobalExceptionHandler->>GlobalExceptionHandler: 2. 2. Generate unique error identifier
    GlobalExceptionHandler-->>GlobalExceptionHandler: correlationId
    activate ILogger
    GlobalExceptionHandler->>ILogger: 3. 3. Error(exception, messageTemplate, correlationId)
    activate LoggingService
    ILogger->>LoggingService: 4. 3.1. [DI Resolution] Invoke concrete implementation
    activate LocalFileSystem
    LoggingService->>LocalFileSystem: 5. 3.2. Write structured error log to file
    LocalFileSystem-->>LoggingService: Write status
    activate ViewManager
    GlobalExceptionHandler->>ViewManager: 6. 4. ShowErrorDialog(dto)
    ViewManager->>ViewManager: 7. 4.1. Instantiate and display modal dialog with error details

    note over GlobalExceptionHandler: The GlobalExceptionHandler must be registered at application startup to ensure it can catch excep...
    note over ViewManager: REQ-1-023: The correlationId generated in step 2 is passed to both the logger (step 3) and the UI...
    note over GlobalExceptionHandler: REQ-1-022: PII sanitization must occur within the GlobalExceptionHandler before calling ILogger o...

    deactivate ViewManager
    deactivate LocalFileSystem
    deactivate LoggingService
    deactivate ILogger
    deactivate GlobalExceptionHandler
