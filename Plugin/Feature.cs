using System;

namespace PAT.Plugin
{
    public abstract class Feature
    {
        public bool Fresh { get; set; }
        public new abstract Type GetType();
    }

    public class Feature<T> : Feature
    {
        private Func<T> _compute;
        protected T _value;

        public virtual T Value
        {
            get
            {
                if (!Fresh)
                {
                    Fresh = true;
                    _value = _compute();
                }

                return _value;
            }
            set { throw new ArithmeticException("Generic features cannot have their values set"); }
        }
        
        public override Type GetType()
        {
            return typeof(T);
        }

        public Feature(Func<T> compute)
        {
            _compute = compute;
        }
    }

    public class FinalFeature<T> : Feature<T>
    {
        public override T Value
        {
            get
            { return _value; }
            set
            {
                Fresh = true;
                _value = value;
            }
        }

        public FinalFeature() : base(null)
        {
        }
    }
}
