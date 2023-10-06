using System;
using System.Collections.Generic;
using PAT.Automation;
using PAT.Configuration;
using PAT.Plugin;

namespace PAT.Mediation
{
    public interface IAssignable
    {
        public Type GetType();
        public void Assign(Feature playerInput);
        public void Unassign();
        public void Configure(InputConfigurator configurator);
    }
    
    public class MediationPlugin<T> : Plugin<T>, IAssignable
    {
        protected string _inputName;
        
        protected Feature<T> _aiInput;
        protected Mediator<T> _mediator;
        protected Feature<T> _playerInput;

        public Type GetType()
        {
            return typeof(T);
        }
        
        public MediationPlugin(string inputName, Mediator mediator) : base(MediationManager.GameInterfaceName, inputName)
        {
            _inputName = inputName;
            _mediator = mediator as Mediator<T>;
            Unassign();
        }

        public override void Initialize(Dictionary<string, PluginInterface> requiredInterfaces, Dictionary<string, PluginInterface> providedInterfaces)
        {
            if (!requiredInterfaces[AutomationManager.AIInputInterfaceName].ContainsKey(_inputName))
                throw new Exception("Input \"" + _inputName + "\" does not yet have an automator");
            _aiInput = (Feature<T>) requiredInterfaces[AutomationManager.AIInputInterfaceName][_inputName];
        }

        public void Assign(Feature playerInput)
        {
            _playerInput = playerInput as Feature<T>;
            _mediator.Assigned = true;
        }

        public void Unassign()
        {
            _playerInput = null;
            _mediator.Assigned = false;
        }

        public void Configure(InputConfigurator configurator)
        {
            _mediator.Configure(configurator);
        }

        public override T Compute()
        {
            if (_playerInput == null) return _aiInput.Value;
            else return _mediator.Mediate(_playerInput, _aiInput);
        }
    }
}
