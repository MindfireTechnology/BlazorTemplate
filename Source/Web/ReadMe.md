https://mudblazor.com/

Get Config from Server side

To deploy database updates, right click the Database project and select publish.

To use Blazor Hot Reload, start the app using the command.
dotnet watch run debug


Child Item updated => Notify


Component lifecycle events (https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0):

1. If the component is rendering for the first time on a request:
- Create the component's instance.
- Perform property injection. Run SetParametersAsync.
- Call OnInitialized{Async}. If a Task is returned, the Task is awaited and the component is rendered. If a Task isn't returned, the component is rendered.

2. Call OnParametersSet{Async} and render the component. If a Task is returned from OnParametersSetAsync, the Task is awaited and then the component is rerendered.

Lifecycle methods
Before parameters are set
- SetParametersAsync sets parameters supplied by the component's parent in the render tree or from route parameters. By overriding the method, developer code can interact directly with the ParameterView's parameters.

Component initialization methods
- OnInitializedAsync and OnInitialized are invoked when the component is initialized after having received its initial parameters from its parent component in SetParametersAsync.

After parameters are set
OnParametersSetAsync or OnParametersSet are called:

- After the component is initialized in OnInitialized or OnInitializedAsync.
- When the parent component re-renders and supplies:
- Only known primitive immutable types of which at least one parameter has changed.
- Any complex-typed parameters. The framework can't know whether the values of a complex-typed parameter have mutated internally, so it treats the parameter set as changed.

After component render
OnAfterRenderAsync and OnAfterRender are called after a component has finished rendering. 
Element and component references are populated at this point. Use this stage to perform additional initialization steps using the rendered content, such as activating third-party JavaScript libraries that operate on the rendered DOM elements.

State changes
StateHasChanged notifies the component that its state has changed.
When applicable, calling StateHasChanged causes the component to be rerendered.

StateHasChanged is called automatically for EventCallback methods. For more information, see ASP.NET Core Blazor event handling.
