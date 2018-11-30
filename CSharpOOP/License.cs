using System;
using System.Collections.Generic;

namespace CSharpOOP
{
    public class License
    {
        public string Id { get; set; }
        public DateTime DateIssued { get; private set; }
        public Vehicle Vehicle { get; private set; }

        public License(Vehicle vehicle)
        {
            DateIssued = DateTime.Today;
            Vehicle = vehicle;
        }

        public override string ToString()
        {
            return DateIssued + " " + Vehicle;
        }
    }
}
