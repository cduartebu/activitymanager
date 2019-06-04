using GestorActividades.Infrastructure.Models;
using GestorActividades.Services;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GestorActividades.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        private IActivityService myActivityService;

        public IActivityService ActivityService
        {
            get { return myActivityService ?? (myActivityService = new ActivityService()); }
            set { myActivityService = value; }
        }

        // GET: api/Activity/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var result = ActivityService.GetActivityById(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // GET: api/Activity
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var result = ActivityService.GetActivityByUserName(HttpContext.Current.User.Identity.Name);            

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // POST: api/Activity
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Activity value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            var result = value.TeamId==0?ActivityService.AddActivityToUser(value, HttpContext.Current.User.Identity.Name): ActivityService.AddActivity(value);
           
            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // PUT: api/Activity/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Activity value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            value.ActivityId = id;

            var result = ActivityService.UpdateActivity(value);            

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

        // DELETE: api/Activity/5
        public HttpResponseMessage Delete(int id)
        {
            var result = ActivityService.DeleteActivity(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}
