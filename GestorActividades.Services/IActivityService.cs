using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;

namespace GestorActividades.Services
{
    public interface IActivityService
    {
        ResponseDto<Activity> AddActivity(Activity Activity);

        ResponseDto<Activity> GetActivityById(int id);

        ResponseDto<Activity> UpdateActivity(Activity Activity);

        ResponseDto<bool> DeleteActivity(int id);
    }
}