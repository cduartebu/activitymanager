using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using System.Collections.Generic;

namespace GestorActividades.Services
{
    public interface IUserService
    {
        ResponseDto<User> AddUser(User User);

        ResponseDto<User> GetUserById(int id);

        ResponseDto<IEnumerable<User>> GetUsers(string username);

        ResponseDto<User> UpdateUser(User User);

        ResponseDto<bool> DeleteUser(int id);
    }
}