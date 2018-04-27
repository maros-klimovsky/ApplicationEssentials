namespace ApplicationEssentials
{
    public class PositionalParameterAttribute : ArgumentAttribute
    {
        public new bool Required => true;


        public int Position
        {
            get;
            set;
        }


        public PositionalParameterAttribute(int position)
        {
            this.Position = position;
        }
    }
}