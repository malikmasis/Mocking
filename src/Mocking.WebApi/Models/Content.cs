using System;

namespace Mocking.WebApi.Models
{
    [Serializable]
    public class Content
    {
        public string version { get; set; }
        public bool resultStatus { get; set; }
        public int resultCode { get; set; }
        public string resultMessage { get; set; }
        public Resultobject resultObject { get; set; }
    }
}
