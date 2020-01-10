namespace ZIPCodeResolver.Core.Models
{
    public sealed class SelectionInfo
    {
        public int Count { get; set; }
        public string Message
        {
            get
            {
                return Count == 0
                    ? "No result was found"
                    : Count == 1
                        ? "Only 1 result was found. You provide precise value of postal code"
                        : $"{Count} results were in the search selection. Provide more precise value of postal code";
            }
        }
    }
}