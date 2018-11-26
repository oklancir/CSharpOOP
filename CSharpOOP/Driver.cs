using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpOOP
{
    public class Driver
    {
        public IList<License> Licenses { get; private set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }

        // Driver needs to know how to drive at least one vehicle.
        public Driver(params Vehicle[] vehicles)
        {
            Licenses = new List<License>();
            foreach (var vehicle in vehicles)
            {
                LearnToDrive(vehicle);
            }
        }

        public void LearnToDrive(Vehicle vehicle)
        {
            Licenses.Add(new License(this.Id, vehicle));
        }

        public bool CanDrive(Type vehicleType)
        {
            return Licenses.Any(vType => vType.GetType().IsAssignableFrom(vehicleType));
        }
    }
}
