using System;

namespace CSharpOOP
{
    public class License
    {
        public int Id { get; set; }
        public Driver Driver { get; set; }
        public DateTime DateIssued { get; set; } 
        public Vehicle Vehicle { get; set; }

        public License()
        {
            DateIssued = DateTime.Today;
        }
        public License(Vehicle vehicle, Driver driver)
        {
            DateIssued = DateTime.Today;
            Vehicle = vehicle;
            Driver = driver;
        }

        public override string ToString()
        {
            return DateIssued + " " + Vehicle;
        }
    }
}
