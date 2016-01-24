using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Models;
using Api.Common;
using Api.DAL.DTO;
using System.Net;
using System.IO;
using System.Text;

namespace Api.BL
{
    public class BLIntegration
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public BLResponse<IntegrationModel> GetIntegrationModel(string integrationName)
        {
            var blResponse = new BLResponse<IntegrationModel>();
            blResponse.ResponseCode = ResponseCode.Success;

            try
            {
                Integration integration = db.Integrations.Single(p => p.Name == integrationName);

                WebRequest request = WebRequest.Create(integration.Url);
                var webResponse = request.GetResponse();

                string mediaType = webResponse.ContentType.Contains("json") ? "application/json" : "application/xml";

                // Get the stream associated with the response.
                Stream receiveStream = webResponse.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                IntegrationModel integrationModel = new IntegrationModel
                {
                    Content = readStream.ReadToEnd(),
                    MediaType = mediaType
                };

                blResponse.ResponseData = integrationModel;

                webResponse.Close();
                readStream.Close();
            }
            catch (Exception)
            {
                blResponse.ResponseData = null;
                blResponse.ResponseCode = ResponseCode.Fail;
                blResponse.ResponseMessage = ResponseMessage.IntegrationNotFound;
            }

            return blResponse;
        }
    }
}