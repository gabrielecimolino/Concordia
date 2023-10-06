using PAT.Configuration;
using PAT.Plugin;

namespace PAT.Mediation.LegacyUnity
{
    public class AxisMediator : Mediator<float>
    {
        protected ConfigurationSlider<float> _blendFactor;
        
        public override bool Assigned
        {
            set
            {
                _assigned = value;
                if (!value) _blendFactor.Value = 1f;
            }
        }
        public AxisMediator(float blendFactor = 1f)
        {
            _blendFactor = new ConfigurationSlider<float>("Assistance Level" ,0f, 1f, blendFactor);
        }

        public override float Mediate(Feature<float> playerInput, Feature<float> aiInput)
        {
            if (_assigned)
            {
                if (_blendFactor.Value > 0f)
                    return ((1f - _blendFactor.Value) * playerInput.Value) + (_blendFactor.Value * aiInput.Value);
                else return playerInput.Value;
            }
            else return aiInput.Value;
        }

        public override void Configure(InputConfigurator configurator)
        {
            base.Configure(configurator);
            
            configurator.AddSlider(_blendFactor, PATManager.PlayerSprite, PATManager.AISprite);
        }
    }
}