using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Api.DAL.DTO;
using Api.Models;
using Api.BL;
using Api.Common;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace Api.Controllers
{
    public class IntegrationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Integrations
        public IQueryable<Integration> GetIntegrations()
        {
            return db.Integrations;
        }

        // GET: api/Integrations/harunk
        public HttpResponseMessage GetIntegration(string name)
        {
            BLIntegration blIntegration = new BLIntegration();

            BLResponse<IntegrationModel> integrationResponse = blIntegration.GetIntegrationModel(name);

            if (integrationResponse.ResponseCode == ResponseCode.Fail)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(integrationResponse.ResponseMessage),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            return new HttpResponseMessage
            {
                Content = new StringContent(integrationResponse.ResponseData.Content, Encoding.UTF8, integrationResponse.ResponseData.MediaType),
                StatusCode = HttpStatusCode.OK
            };
        }

        // PUT: api/Integrations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIntegration(int id, Integration integration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != integration.Id)
            {
                return BadRequest();
            }

            db.Entry(integration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntegrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Integrations
        [ResponseType(typeof(Integration))]
        public IHttpActionResult PostIntegration(Integration integration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Integrations.Add(integration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = integration.Id }, integration);
        }

        // DELETE: api/Integrations/5
        [ResponseType(typeof(Integration))]
        public IHttpActionResult DeleteIntegration(int id)
        {
            Integration integration = db.Integrations.Find(id);
            if (integration == null)
            {
                return NotFound();
            }

            db.Integrations.Remove(integration);
            db.SaveChanges();

            return Ok(integration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IntegrationExists(int id)
        {
            return db.Integrations.Count(e => e.Id == id) > 0;
        }
    }
}