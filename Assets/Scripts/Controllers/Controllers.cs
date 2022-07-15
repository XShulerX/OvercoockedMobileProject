using System.Collections.Generic;

internal sealed class Controllers : ICleanup, IExecute, IInitialization
{
    private readonly List<ICleanup> _cleanupControllers;
    private readonly List<IExecute> _executeControllers;
    private readonly List<IInitialization> _initializeControllers;

    internal Controllers()
    {
        _cleanupControllers = new List<ICleanup>();
        _executeControllers = new List<IExecute>();
        _initializeControllers = new List<IInitialization>();
    }

    internal Controllers Add(IController controller)
    {
        if (controller is ICleanup cleanupController)
        {
            _cleanupControllers.Add(cleanupController);
        }

        if (controller is IExecute executeController)
        {
            _executeControllers.Add(executeController);
        }

        if (controller is IInitialization initializationController)
        {
            _initializeControllers.Add(initializationController);
        }

        return this;
    }

    public void Cleanup()
    {
        for (var index = 0; index < _cleanupControllers.Count; ++index)
        {
            _cleanupControllers[index].Cleanup();
        }
    }

    public void Execute(float deltaTime)
    {
        for (var index = 0; index < _executeControllers.Count; ++index)
        {
            _executeControllers[index].Execute(deltaTime);
        }
    }

    public void Initilazation()
    {
        for (var index = 0; index < _initializeControllers.Count; ++index)
        {
            _initializeControllers[index].Initilazation();
        }
    }
}