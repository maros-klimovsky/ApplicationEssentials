namespace ApplicationEssentials
{
    public class Option : ArgumentAttribute
    {
        public string ShortName
        {
            get;
            set;
        }


        public string LongName
        {
            get;
            set;
        }
    }
}