using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CognitiveServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCRController : ControllerBase
    {
        const string subscriptionKey = "7be78a586e4f44889b1336c26bd629c7";
        const string uriBaseVision = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/ocr";
        private const string remoteImageUrl = "{'url':'https://conteudo.imguol.com.br/c/noticias/50/2017/05/31/carrefour-trocou-a-disposicao-de-sua-etiqueta-de-precos-apos-reclamacao-1496267716554_615x300.jpg'}";


        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return MakeOCRRequest(remoteImageUrl).Result;
        }

        static async Task<string> MakeOCRRequest(string imageFilePath)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            var uri = uriBaseVision;

            HttpResponseMessage response;

            var content = new StringContent(imageFilePath);
           content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response = await client.PostAsync(uri, content);

            var contentString = await response.Content.ReadAsStringAsync();

            return JToken.Parse(contentString).ToString();
        }
    }
}
