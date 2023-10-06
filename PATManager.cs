using System;
using System.Collections.Generic;
using PAT.Automation;
using PAT.Awareness;
using PAT.Configuration;
using PAT.Input;
using PAT.Mediation;
using PAT.Plugin;
using UnityEngine;

public class PATManager : MonoBehaviour
{
    public static string GameStateInterfaceName = "Game State";

    public static Sprite PlayerSprite;
    public static Sprite SharedSprite;
    public static Sprite AISprite;
    
    private static InputManager _inputManager;
    private static MediationManager _mediationManager;
    private static AIManager _aiManager;
    private static AutomationManager _automationManager;
    private static AwarenessManager _awarenessManager;

    private static PluginInterface _gameState;

    // Initialize components first
    public static void Initialize()
    {
        // Create the game state interface
        _gameState = new PluginInterface();
        
        // Initialize components
        _inputManager = new InputManager();
        _mediationManager = new MediationManager();
        _aiManager = new AIManager();
        _automationManager = new AutomationManager();
        _awarenessManager = new AwarenessManager();
        
        // Link the components together
        _aiManager.ConnectRequiredInterface(GameStateInterfaceName, _gameState);
        
        _automationManager.ConnectRequiredInterface(AIManager.AIStateInterfaceName, _aiManager[AIManager.AIStateInterfaceName]);
        
        _awarenessManager.ConnectRequiredInterface(AIManager.AIStateInterfaceName, _aiManager[AIManager.AIStateInterfaceName]);
        
        _mediationManager.ConnectRequiredInterface(InputManager.PlayerInterfaceName, _inputManager[InputManager.PlayerInterfaceName]);
        _mediationManager.ConnectRequiredInterface(AutomationManager.AIInputInterfaceName, _automationManager[AutomationManager.AIInputInterfaceName]);
    }
    
    // Add whatever plugins you want
    public static void RegisterSensor<T>(Sensor<T> sensor)
    {
        _inputManager.RegisterSensor(sensor);    
    }

    public static void RegisterProcessor<T>(Processor<T> processor)
    {
        _aiManager.RegisterPlugin(processor);
    }
    
    public static void RegisterAutomator<T>(Automator<T> automator)
    {
        _automationManager.RegisterPlugin(automator);
    }

    public static void RegisterCommunicator<T>(Communicator<T> communicator)
    {
        _awarenessManager.RegisterPlugin(communicator);
    }

    // Initialize the plugins next
    public static void InitializePlugins()
    {
        _inputManager.InitializePlugins();
        _mediationManager.InitializePlugins();
        _aiManager.InitializePlugins();
        _automationManager.InitializePlugins();
        _awarenessManager.InitializePlugins();
    }

    // Create inputs after the plugins are initialized
    public static void CreateInput<T>(string inputName, Mediator<T> mediator)
    {
        _mediationManager.CreateInput(inputName, mediator);
    }

    // Assign inputs after they are created
    public static void AssignInput(string inputName, string playerInputName)
    {
        _mediationManager.AssignInput(inputName, playerInputName);
    }

    public static void UnassignInput(string inputName)
    {
        _mediationManager.UnassignInput(inputName);
    }

    public static void ConfigureInput(string inputName, InputConfigurator configurator)
    {
        _mediationManager.ConfigureInput(inputName, configurator);
    }
    
    // Refresh every time state changes
    public static void Refresh()
    {
        _inputManager.Refresh();
        _mediationManager.Refresh();
        _aiManager.Refresh();
        _automationManager.Refresh();
        _awarenessManager.Refresh();
        _gameState.Refresh();
    }
    
    public static T GetState<T>(string name)
    {
        return ((FinalFeature<T>) _gameState[name]).Value;
    }
    
    // Put data in here
    public static void SetState<T>(string name, T value)
    {
        if (!_gameState.ContainsKey(name)) _gameState[name] = new FinalFeature<T>();
        ((FinalFeature<T>) _gameState[name]).Value = value;
    }

    // Get data out of here
    public static T GetInput<T>(string inputName)
    {
        return ((Feature<T>) _mediationManager[MediationManager.GameInterfaceName][inputName]).Value;
    }

    public static T GetCue<T>(string cueName)
    {
        return ((Feature<T>) _awarenessManager[AwarenessManager.CuesInterfaceName][cueName]).Value;
    }
    
    // Get input names for configuration
    public static Dictionary<string, Type> GameInputs()
    {
        return _mediationManager.GameInputs();
    }

    public static string[] PlayerInputs(Type type)
    {
        return _mediationManager.PlayerInputs(type);
    }
}
