namespace Tangy_Models.Dtos
{
    public class UserInfoDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string  PhoneNumber { get; set; }

        public UserInfoDto()
        {

        }
        public UserInfoDto(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
