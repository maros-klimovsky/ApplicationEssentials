using System;

namespace ApplicationEssentials
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgumentAttribute : Attribute
    {
        public bool Required
        {
            get;
            set;
        } = false;


        public string HelpText
        {
            get;
            set;
        } = "No helptext provided.";
    }
}