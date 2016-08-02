using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CodedHomes.Data;
using CodedHomes.Models;

namespace CodedHomes.Web.Controllers
{
    [Authorize]
    public class HomesAPIController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        //api/homes/get
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Home> Get()
        {
            return this._unit.Homes.GetAll();
        }

        //api/homes/{id}
        [HttpGet]
        [Authorize(Roles = "admin, manager, user")]
        public Home Get(int id)
        {
            Home home = _unit.Homes.GetById(id);
            if (home == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return home;
        }

        [HttpPut]
        [Authorize(Roles = "admin, manager, user")]
        public HttpResponseMessage Put(int id, Home home)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != home.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Home existHome = _unit.Homes.GetById(id);
            _unit.Homes.Detach(existHome);

            //keep created on value
            home.CreatedOn = existHome.CreatedOn;

            this._unit.Homes.Update(home);

            try
            {
                _unit.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "{success:'true',verb:'PUT'}");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Authorize(Roles = "admin, manager, user")]
        public HttpResponseMessage Post(Home home)
        {
            if (ModelState.IsValid)
            {
                _unit.Homes.Add(home);
                _unit.SaveChanges();

                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, home);

                result.Headers.Location = new Uri(Url.Link("API", new { id = home.Id }));

                return result;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "admin, manager")]
        public HttpResponseMessage Delete(int id)
        {
            Home home = this._unit.Homes.GetById(id);

            if (home == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this._unit.Homes.Delete(home);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, home);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
