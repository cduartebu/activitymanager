using GestorActividades.Infrastructure.Models;
using GestorActividades.Services;
using System.Net.Http;
using System.Web.Http;

namespace GestorActividades.Controllers
{
    public class TeamController : ApiController
    {
        private ITeamService myTeamService;

        public ITeamService TeamService
        {
            get { return myTeamService ?? (myTeamService = new TeamService()); }
            set { myTeamService = value; }
        }

        // GET: api/Team/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var result = TeamService.GetTeamById(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // POST: api/Team
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Team value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            var result = TeamService.AddTeam(value);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // PUT: api/Team/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Team value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            value.TeamId = id;

            var result = TeamService.UpdateTeam(value);            

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

        // DELETE: api/Team/5
        public HttpResponseMessage Delete(int id)
        {
            var result = TeamService.DeleteTeam(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}
