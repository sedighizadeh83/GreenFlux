namespace GreenFlux.GlobalErrorHandling
{
    public class ValidationErrorResponse
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public IDictionary<string, string[]> errors { get; set; }
    }
}