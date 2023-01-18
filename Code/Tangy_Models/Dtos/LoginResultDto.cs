namespace Tangy_Models.Dtos
{
    public class LoginResultDto
    {
        public string Name { get; set; }
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
