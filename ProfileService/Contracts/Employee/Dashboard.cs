using System.Collections.Generic;

namespace ProfileService.Contracts.Employee
{
    public class Dashboard
    {
        public List<KeyValue> Wellness { get; set; } 
        public List<KeyValue> Schedule { get; set; }
        public List<KeyValue> Departments { get; set; }
    }

    public class KeyValue
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }
}