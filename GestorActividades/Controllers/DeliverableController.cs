using GestorActividades.Infrastructure.Models;
using GestorActividades.Services;
using System.Net.Http;
using System.Web.Http;

namespace GestorActividades.Controllers
{
    public class DeliverableController : ApiController
    {
        private IDeliverableService myDeliverableService;

        public IDeliverableService DeliverableService
        {
            get { return myDeliverableService ?? (myDeliverableService = new DeliverableService()); }
            set { myDeliverableService = value; }
        }

        // GET: api/Deliverable/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var result = DeliverableService.GetDeliverableById(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // POST: api/Deliverable
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Deliverable value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            var result = DeliverableService.AddDeliverable(value);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        // PUT: api/Deliverable/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Deliverable value)
        {
            if (value == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            value.DeliverableId = id;

            var result = DeliverableService.UpdateDeliverable(value);            

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

        // DELETE: api/Deliverable/5
        public HttpResponseMessage Delete(int id)
        {
            var result = DeliverableService.DeleteDeliverable(id);

            if (result.StatusCode != Infrastructure.StatusCode.Successful)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}
