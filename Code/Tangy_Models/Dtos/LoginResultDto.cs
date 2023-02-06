namespace Tangy_Models.Dtos
{
    public class LoginResultDto
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public UserInfoDto User { get; set; }

        public LoginResultDto()
        {
            User = new UserInfoDto();
        }

    }
}
