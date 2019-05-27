using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;

namespace GestorActividades.Services
{
    public interface IUserService
    {
        ResponseDto<User> AddUser(User User);

        ResponseDto<User> GetUserById(int id);

        ResponseDto<User> UpdateUser(User User);

        ResponseDto<bool> DeleteUser(int id);
    }
}