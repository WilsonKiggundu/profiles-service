using System;

namespace ProfileService.Models.Common
{
    public class Device : BaseModel
    {
        public string ProfileId { get; set; }
        public string Token { get; set; }
    }

    public class VapidKeys : BaseModel
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Subject { get; set; }
    }
}