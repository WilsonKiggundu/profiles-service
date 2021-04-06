using System;

namespace ProfileService.Models.Common
{
    public class Device : BaseModel
    {
        public string Name { get; set; }
        public string PushEndpoint { get; set; }
        public string PushP256DH { get; set; }
        public string PushAuth { get; set; }
        public bool IsOnline { get; set; }
        public DateTime? LastSeen { get; set; }
    }

    public class VapidKeys : BaseModel
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Subject { get; set; }
    }
}