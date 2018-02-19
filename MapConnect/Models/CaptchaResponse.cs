using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjetPersoTest.Controllers
{
    internal class CaptchaResponse
    {
        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

    }
}