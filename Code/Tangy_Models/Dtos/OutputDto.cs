namespace Tangy_Models.Dtos
{
    public class OutputDto
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public string Message { get; set; }
    }
}
