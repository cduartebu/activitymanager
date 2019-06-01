using GestorActividades.Infrastructure.Models;
using GestorActividades.Services;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GestorActividades.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private IUserService myUserService;

        public IUserService UserService
        {
            get { return myUserService ?? (myUserService = new UserService()); }
            set { myUserService = value; }
        }

        // GET: api/User/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var result = UserService.GetUserById(id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // GET: api/User
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var result = UserService.GetUsers(HttpContext.Current.User.Identity.Name);            
            

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // POST: api/User
        [HttpPost]
        public HttpResponseMessage Post([FromBody]User value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            var result = UserService.AddUser(value);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // PUT: api/User/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]User value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            value.UserId = id;

            var result = UserService.UpdateUser(value);            

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

        // DELETE: api/User/5
        public HttpResponseMessage Delete(int id)
        {
            var result = UserService.DeleteUser(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}
