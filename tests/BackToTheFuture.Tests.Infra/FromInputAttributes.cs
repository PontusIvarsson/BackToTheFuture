using Fixie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BackToTheFuture.Tests.Infra
{
    public class FromInputAttributes : ParameterSource
    {
        public IEnumerable<object[]> GetParameters(MethodInfo method)
        {
            var newParams = method.GetCustomAttributes<InputAttribute>(true).Select(input => input.Parameters);
            return newParams;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InputAttribute : Attribute
    {
        public InputAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }

        public object[] Parameters { get; private set; }
    }
}
