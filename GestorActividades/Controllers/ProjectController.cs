using GestorActividades.Infrastructure.Models;
using GestorActividades.Services;
using System.Net.Http;
using System.Web.Http;

namespace GestorActividades.Controllers
{
    public class ProjectController : ApiController
    {
        private IProjectService myProjectService;

        public IProjectService ProjectService
        {
            get { return myProjectService ?? (myProjectService = new ProjectService()); }
            set { myProjectService = value; }
        }

        // GET: api/Project/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var result = ProjectService.GetProjectById(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // POST: api/Project
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Project value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            var result = ProjectService.AddProject(value);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // PUT: api/Project/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Project value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            value.ProjectId = id;

            var result = ProjectService.UpdateProject(value);            

            if (result.StatusCode == Infrastructure.StatusCode.Warning)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }
            else if(result.StatusCode == Infrastructure.StatusCode.Error)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // DELETE: api/Project/5
        public HttpResponseMessage Delete(int id)
        {
            var result = ProjectService.DeleteProject(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}
