using System;
using System.Collections.Generic;

namespace PAT.Input
{
    public abstract class Sensor<T>
    {
        public abstract Dictionary<string, Func<T>> Inputs();
    }
}