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
using Newtonsoft.Json;
using Api.Common.DataExport;

namespace Api.BL
{
    public class BLIntegration
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// returns integration content 
        /// </summary>
        /// <param name="integrationName"></param>
        ///  <param name="exportType"> category or product</param>
        /// <returns></returns>
        public BLResponse<IntegrationModel> GetIntegration(string integrationName, string exportType)
        {
            var blResponse = new BLResponse<IntegrationModel>();
            blResponse.ResponseCode = ResponseCode.Success;

            ExportType type = GetExportType(exportType);

            if (type == ExportType.Undefined)
            {
                blResponse.ResponseData = null;
                blResponse.ResponseCode = ResponseCode.Fail;
                blResponse.ResponseMessage = ResponseMessage.ExportTypeNotFound;
                return blResponse;
            }

            try
            {
                Integration integration = db.Integrations.Single(p => p.Name == integrationName);

                var integrationDetail = integration.IntegrationDetails.Single(p => p.ExportType == type);

                WebRequest request = WebRequest.Create(integrationDetail.Url);
                var webResponse = request.GetResponse();

                // Get the stream associated with the response.
                Stream receiveStream = webResponse.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                var categories = JsonConvert.DeserializeObject<IList<CategoryModel>>(readStream.ReadToEnd());

                blResponse.ResponseData = new IntegrationModel { Content = readStream.ReadToEnd() };

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

        /// <summary>
        /// returns all integration summaries
        /// </summary>
        /// <returns></returns>
        public BLResponse<List<IntegrationInfoModel>> GetAllIntegrationInfo()
        {
            var blResponse = new BLResponse<List<IntegrationInfoModel>>();
            blResponse.ResponseCode = ResponseCode.Success;

            try
            {
                var integrationsQuery = db.Integrations.Select(p => new IntegrationInfoModel
                                {
                                    Name = p.Name,
                                    Type = p.Type,
                                    Status = p.Status,
                                }
                           );

                blResponse.ResponseData = integrationsQuery.ToList();
            }
            catch (Exception)
            {
                blResponse.ResponseData = null;
                blResponse.ResponseCode = ResponseCode.Fail;
                blResponse.ResponseMessage = ResponseMessage.IntegrationNotFound;
            }

            return blResponse;
        }


        /// <summary>
        /// gets the export type from query string
        /// </summary>
        /// <param name="exportType"></param>
        /// <returns></returns>
        private ExportType GetExportType(string exportType)
        {
            switch (exportType.ToLower())
            {
                case ExportTypeText.Category:
                    return ExportType.Category;
                case ExportTypeText.Product:
                    return ExportType.Product;
                default:
                    return ExportType.Undefined;
            }
        }
    }
}