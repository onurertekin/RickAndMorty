using System;
using Host.Helpers;

namespace Host.Helpers
{
    public class RequiredHeaderParametersAttribute : Attribute
    {
        private readonly string parameters;

        public RequiredHeaderParametersAttribute(string parameters)
        {
            this.parameters = parameters;
        }

        public string GetParameters()
        {
            return parameters;
        }
    }
}