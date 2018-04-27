namespace ApplicationEssentials.Tests.ArgumentsProcessing
{
    public class PositionalParameters
    {
        [PositionalParameter(0)]
        public string First { get; set; }

        [PositionalParameter(2)]
        public string Third { get; set; }

        [PositionalParameter(1)]
        public string Second { get; set; }
    }
}