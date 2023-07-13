using FCI_DataAccess.Models;

namespace FCI_DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsuniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
    }
}
