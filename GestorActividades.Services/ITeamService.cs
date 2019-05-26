using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;

namespace GestorActividades.Services
{
    public interface ITeamService
    {
        ResponseDto<Team> AddTeam(Team Team);

        ResponseDto<Team> GetTeamById(int id);

        ResponseDto<Team> UpdateTeam(Team Team);

        ResponseDto<bool> DeleteTeam(int id);
    }
}