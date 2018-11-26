using System;
using System.Collections.Generic;

namespace CSharpOOP
{
    public class License
    {
        public string Id { get; set; }
        public DateTime DateIssued { get; private set; }
        public IList<object> Vehicles { get; private set; }

        public License(string id, Vehicle vehicle)
        {
            Id = id;
            DateIssued = DateTime.Today;
            Vehicles.Add(vehicle);
        }
    }
}
