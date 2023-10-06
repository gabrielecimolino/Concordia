using PAT.Configuration;
using PAT.Plugin;

namespace PAT.Mediation
{
    public abstract class Mediator
    {

    }

    public abstract class Mediator<T> : Mediator
    {
        public virtual bool Assigned
        {
            get { return _assigned; }
            set { _assigned = value; }
        }
        protected bool _assigned;

        public Mediator() { }
        
        public abstract T Mediate(Feature<T> playerInput, Feature<T> aiInput);

        public virtual void Configure(InputConfigurator configurator) { }
    }
}
