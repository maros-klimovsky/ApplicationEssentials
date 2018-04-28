using System;
using System.Collections.Generic;
using System.Reflection;

namespace ApplicationEssentials
{
    public class ArgumentsProcessor
    {
        public TArguments ParseArguments<TArguments>(string[] args) where TArguments : new()
        {
            var result = new TArguments();

            var properties = result.GetType().GetProperties();

            var positionalParameters = new SortedList<int, PropertyInfo>();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(ArgumentAttribute), true);

                switch (attribute)
                {
                    case PositionalParameterAttribute a:
                        positionalParameters.Add(a.Position, property);
                        break;
                    default:
                        throw new Exception($"Attribute type '{attribute.TypeId}' isn't supported.");
                }
            }

            var enumerator = args.GetEnumerator();
            var positionalParametersEnumerator = positionalParameters.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var argument = ((string) enumerator.Current);

                if (argument.StartsWith("--"))
                {
                // search by fullname
                throw new NotImplementedException("No options yet.");
                }
                else if (argument.StartsWith("-"))
                {
                    // search by short name
                    throw new NotImplementedException("No options yet.");

                }
                else
                {
                    //PositionalArgumentAttribute argument
                    if (!positionalParametersEnumerator.MoveNext())
                    {
                        throw new Exception($"Unexpected positional parameter '{argument}'");
                    }

                    // TODO cast it to correct type
                    ((PropertyInfo) positionalParametersEnumerator.Current.Value).SetValue(result, argument);

                }
            }

            if (positionalParametersEnumerator.MoveNext())
            {
                var name = positionalParametersEnumerator.Current.Value.Name;
                throw new Exception($"Missing value for positional parameter '{name}'");
            }

            return result;
        }
    }
}