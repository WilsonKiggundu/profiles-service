namespace ProfileService.Models.Common
{
    public abstract class Address : BaseModel
    {
        public AddressType Type { get; set; } = AddressType.Other;
        public string Country { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string Floor { get; set; }
        public string Postal { get; set; }
    }
    
    public enum AddressType
    {
        Mailing = 1,
        Physical = 2,
        Other = 3
    }
}