using System;
using System.Linq;
using PAT.Utilities;

namespace PAT.Configuration
{
    public class Configuration
    {
        protected string _name;
        public string Name
        {
            get { return _name; }
        }
        
        public Configuration(string name)
        {
            _name = name;
        }
    }
    
    public class ConfigurationCheckbox : Configuration
    {
        public bool Value { get; set; }

        public ConfigurationCheckbox(string name, bool value = false) : base(name)
        {
            Value = value;
        }
    }

    public class ConfigurationDropdown : Configuration
    {
        public virtual int Value { get; set; }
        
        public ConfigurationDropdown(string name) : base(name) { }

        public virtual string[] Options()
        {
            return null;
        }

        public virtual int OptionValue(string option)
        {
            return -1;
        }
    }

    public class ConfigurationDropdown<T> : ConfigurationDropdown where T : Enum
    {
        private T _value;

        public override int Value
        {
            get { return Array.IndexOf(Values(), _value); }
            set { _value = Values()[value]; }
        }
        
        public ConfigurationDropdown(string name, T value = default) : base(name)
        {
            _value = value;
        }

        public override string[] Options()
        {
            return Values().Select(v => v.ToString()).ToArray();
        }

        private T[] Values()
        {
            return (T[]) Enum.GetValues(typeof(T));
        }

        public override int OptionValue(string option)
        {
            return Array.IndexOf(Options(), option);
        }
    }
    
    public class ConfigurationSlider<T> : Configuration where T : IComparable<T>
    {
        private T _min;
        private T _max;
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                if (value.LessThan(_min) || value.GreaterThan(_max))
                    throw new ArgumentException("Value " + value + " is outside the range [" + _min + "," + _max + "]");
                else _value = value;
            }
        }
        
        public ConfigurationSlider(string name, T min, T max, T value = default) : base(name)
        {
            _min = min;
            _max = max;
            Value = value;
        }
    }
}
