using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using System.Collections.Generic;

namespace GestorActividades.Services
{
    public interface IActivityService
    {
        ResponseDto<Activity> AddActivity(Activity Activity);

        ResponseDto<Activity> AddActivityToUser(Activity Activity,string userName);

        ResponseDto<Activity> GetActivityById(int id);

        ResponseDto<ICollection<Activity>> GetActivityByUserName(string userName);

        ResponseDto<Activity> UpdateActivity(Activity Activity);

        ResponseDto<bool> DeleteActivity(int id);
    }
}