using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpExample.Base
{
    public class Settings
    {
        public Uri BaseURL { get; set; }
        public RestRequest Request { get; set; }
        public RestResponse Response { get; set; }
        public RestClientOptions RestClientOptions { get; set; }

        public RestClient Client {get; set; }

    }
}
