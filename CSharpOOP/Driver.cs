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
            Licenses.Add(new License(vehicle));
        }

        public bool CanDriveAerial()
        {
            return Licenses.Any(l => l.Vehicle is IAerialVehicle);
        }

        public bool CanDriveGround()
        {
            return Licenses.Any(l => l.Vehicle is IGroundVehicle);
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
