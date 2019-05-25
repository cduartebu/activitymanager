using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;

namespace GestorActividades.Services
{
    public interface IProjectService
    {
        ResponseDto<Project> AddProject(Project project);

        ResponseDto<Project> GetProjectById(int id);

        ResponseDto<Project> UpdateProject(Project project);

        ResponseDto<bool> DeleteProject(int id);
    }
}