using System.Web.Http;

namespace WebAPI_withoutIIS_sample
{
    public class SampleController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Sample";
        }

        [HttpPost]
        public string Post1([FromBody]string value)
        {
            return value;
        }

        [HttpPost]
        public Model Post2([FromBody]Model value)
        {
            return value;
        }
    }

    public class Model
    {
        public string value1 { get; set; }
        public string value2 { get; set; }
    }
}
