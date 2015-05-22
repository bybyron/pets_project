using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PetRescue.Controllers.Web_API
{
    public class ImageUploadController : ApiController
    {
        public async Task<HttpResponseMessage> Post()
        {           
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
            var path = HttpContext.Current.Server.MapPath("~/App_Data/Temp/");
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);
            List<string> files = new List<string>();

            try
            {
                //read form data
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                //get file name
                foreach(MultipartFileData file in streamProvider.FileData){
                    files.Add(file.LocalFileName);
                }
                //send OK response with filenames
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch(SystemException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
