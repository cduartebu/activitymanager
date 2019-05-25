﻿using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using GestorActividades.Services.Validation;
using System;
using System.Linq;

namespace GestorActividades.Services
{
    public class ProjectService : ServiceBase, IProjectService
    {
        public ResponseDto<Project> AddProject(Project project)
        {
            var response = new ResponseDto<Project> { StatusCode = StatusCode.Error };

            var validation = ModelHelperValidator.ValidateComponentModel(project);

            if (!validation.Data)
            {
                response.StatusCode = StatusCode.Error;
                response.StatusMessage = validation.StatusMessage;
                return response;
            }

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repository = unitOfWork.GetGenericRepository<Project>();

                project.ProjectName = project.ProjectName.Trim();
                project.Description = project.ProjectName.Trim();

                if (repository.GetAll().Any(x => x.ProjectName == project.ProjectName))
                {
                    response.StatusMessage = "The project name already exits in the system.";
                    return response;
                }

                project.CreatedDt = DateTime.Now;

                repository.InsertAndSave(project);


                response.StatusCode = StatusCode.Successful;
                response.Data = project;
            }

            return response;

        }

        public ResponseDto<Project> GetProjectById(int id)
        {
            var response = new ResponseDto<Project>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Project>();

                response.Data = repo.Where(x => x.ProjectId == id).FirstOrDefault();

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Error;
                }

                return response;
            }
        }

        public ResponseDto<Project> UpdateProject(Project project)
        {
            var response = new ResponseDto<Project>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Project>();

                response.Data = repo.Where(x => x.ProjectId == project.ProjectId).FirstOrDefault();

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Warning;
                }

                var validation = ModelHelperValidator.ValidateComponentModel(project);

                if (!validation.Data)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = validation.StatusMessage;
                    return response;
                }

                if (repo.GetAll().Any(x => x.ProjectName == project.ProjectName) && project.ProjectName != response.Data.ProjectName)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The project name already exits in the system.";
                    return response;
                }

                response.Data.ProjectName = project.ProjectName;
                response.Data.Description = project.Description;
                response.Data.StartDate = project.StartDate;
                response.Data.EndDate = project.EndDate;

                repo.UpdateAndSave(response.Data);

                return response;
            }
        }

        public ResponseDto<bool> DeleteProject(int id)
        {
            var response = new ResponseDto<bool>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Project>();

                var project = repo.Where(x => x.ProjectId == id).FirstOrDefault();

                if (project == null)
                {
                    response.StatusCode = StatusCode.Error;
                }

                repo.Delete(project);

                return response;
            }
        }
    }
}