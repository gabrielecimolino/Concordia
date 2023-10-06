using System;
using System.Collections.Generic;
using PAT.Configuration;
using PAT.Plugin;
using UnityEngine;

namespace PAT.Mediation.LegacyUnity
{
    public class ButtonMediator : Mediator<bool>
    {
        public enum Mode
        {
            Player,
            Either,
            AI
        }

        protected ConfigurationDropdown<Mode> _mode;

        public override bool Assigned
        {
            set
            {
                _assigned = value;
                if (!value) _mode.Value = (int) Mode.AI;
                else if (_mode.Value == (int) Mode.AI) _mode.Value = (int) Mode.Player;
            }
        }

        public ButtonMediator(Mode mode = Mode.AI)
        {
            _mode = new ConfigurationDropdown<Mode>("Mode", mode);
        }

        public override bool Mediate(Feature<bool> playerInput, Feature<bool> aiInput)
        {
            if (_mode.Value == (int) Mode.Player) return playerInput.Value;
            if (_mode.Value == (int) Mode.Either) return aiInput.Value || playerInput.Value;
            if (_mode.Value == (int) Mode.AI) return aiInput.Value;
            throw new Exception("ButtonMediator has no mode");
        }

        public override void Configure(InputConfigurator configurator)
        {
            base.Configure(configurator);

            Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

            sprites[Mode.Player.ToString()] = PATManager.PlayerSprite;
            sprites[Mode.Either.ToString()] = PATManager.SharedSprite;
            sprites[Mode.AI.ToString()] = PATManager.AISprite;

            configurator.AddRadio(_mode, sprites);
        }
    }
}