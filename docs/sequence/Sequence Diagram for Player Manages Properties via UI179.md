sequenceDiagram
    participant "Human Player" as HumanPlayer
    participant "InputHandler" as InputHandler
    participant "ViewManager" as ViewManager
    participant "PropertyManagementUIController" as PropertyManagementUIController
    participant "ApplicationServices" as ApplicationServices
    participant "BusinessLogic" as BusinessLogic

    activate InputHandler
    HumanPlayer->>InputHandler: 1. Clicks 'Manage Properties' button during pre-roll phase.
    activate ViewManager
    InputHandler->>ViewManager: 2. Request to display the property management screen.
    activate PropertyManagementUIController
    ViewManager->>PropertyManagementUIController: 3. Activates UI and requests data population.
    activate ApplicationServices
    PropertyManagementUIController->>ApplicationServices: 4. Fetch current player's asset information.
    ApplicationServices-->>PropertyManagementUIController: Returns a view model for the UI.
    activate BusinessLogic
    ApplicationServices->>BusinessLogic: 4.1. Get required data from GameState object.
    BusinessLogic-->>ApplicationServices: Returns raw player and property data.
    PropertyManagementUIController->>PropertyManagementUIController: 5. Renders property list, cash, and action buttons based on view model.
    HumanPlayer->>PropertyManagementUIController: 6. Selects a property and clicks 'Build House' button.
    PropertyManagementUIController->>ApplicationServices: 7. Request to execute the 'Build House' action.
    ApplicationServices-->>PropertyManagementUIController: Returns result of the action and updated player state.
    ApplicationServices->>BusinessLogic: 8. Validate build action against game rules.
    BusinessLogic-->>ApplicationServices: Returns a validation result object.
    ApplicationServices->>BusinessLogic: 9. Apply changes to the GameState object.
    BusinessLogic-->>ApplicationServices: Returns updated PlayerState for UI refresh.
    PropertyManagementUIController->>PropertyManagementUIController: 10. Refresh UI with updated state (new house, less cash).
    HumanPlayer->>PropertyManagementUIController: 11. Attempts to mortgage a developed property.
    PropertyManagementUIController->>ApplicationServices: 12. Request to execute the 'Mortgage' action.
    ApplicationServices-->>PropertyManagementUIController: Returns failed action result.
    ApplicationServices->>BusinessLogic: 13. Validate mortgage action against game rules.
    BusinessLogic-->>ApplicationServices: Returns failed validation result.
    PropertyManagementUIController->>PropertyManagementUIController: 14. Display non-intrusive error notification.
    HumanPlayer->>ViewManager: 15. Clicks 'Close' button to exit screen.
    ViewManager->>PropertyManagementUIController: 16. Deactivates the UI component.

    note over PropertyManagementUIController: The PropertyManagementUIController serves as the Controller/Presenter in an MVC/MVP pattern. It i...
    note over BusinessLogic: All action validation logic is centralized in the RuleEngine. The Application Services layer acts...

    deactivate BusinessLogic
    deactivate ApplicationServices
    deactivate PropertyManagementUIController
    deactivate ViewManager
    deactivate InputHandler
