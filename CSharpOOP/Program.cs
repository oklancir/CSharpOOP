using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Driver> drivers = new List<Driver>();
            IList<Vehicle> vehicles = new List<Vehicle>();
            var tank = new Tank(){Id = "234H", Name = "Sherman"};
            vehicles.Add(tank);
            var helicopter = new Helicopter() { Id = "286", Name = "Black Hawk" };
            vehicles.Add(helicopter);
            var airplane = new Airplane() { Id = "737", Name = "Boeing" };
            vehicles.Add(airplane);
            var driverJohn = new Driver(helicopter) { Age = 18, FullName = "John McCain", Id = "John" };
            var driverSteve = new Driver(tank) { Age = 22, FullName = "Steve Buscemi", Id = "Cobra" };
            var driverRingo = new Driver(airplane) { Age = 18, FullName = "Ringo Starr", Id = "Not-A-Drummer" };
            var driverHal = new Driver(new Tank(){Id = "Tanky" ,Name = "Wasteland tank"}) { Age = 18, FullName = "John McCain", Id = "John" };
            var driverMotorola6800 = new Driver(new Airplane(){Id = "F16", Name = "Vanquish"}) { Age = 22, FullName = "Steve Buscemi", Id = "Cobra" };
            var driverCobol = new Driver(new Helicopter(){Id = "MI8", Name = "Not-A-Number"}) { Age = 18, FullName = "Ringo Starr", Id = "Not-A-Drummer" };

            drivers.Add(driverJohn);
            drivers.Add(driverSteve);
            drivers.Add(driverRingo);
            drivers.Add(driverHal);
            drivers.Add(driverMotorola6800);
            drivers.Add(driverCobol);

            var allDrivers = drivers;
            var pilots = GetPilots(allDrivers);
            var tankDrivers = GetDrivers(allDrivers);

            driverJohn.LearnToDrive(new Tank() { Id = "A1", Name = "Abrams" });
            
            int? choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Please enter a number for an option to display...");
                Console.WriteLine("1 - All vehicles \n2 - Ground vehicles \n3 - Aerial vehicles \n4 - All drivers \n5 - Ground drivers \n6 - Pilots \n0 - EXIT");
                Console.Write("Choice: ");

                switch (choice = int.Parse(Console.ReadLine()))
                {
                    case 1:
                        foreach (var vehicle in vehicles)
                        {
                            Console.WriteLine("Vehicle: " + vehicle.ToString());
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        var groundVehicles = GetGroundVehicles(vehicles);
                        foreach (var vehicle in groundVehicles)
                        {
                            Console.WriteLine(vehicle.ToString());
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        var aerialVehicles = GetGroundVehicles(vehicles);
                        foreach (var vehicle in aerialVehicles)
                        {
                            Console.WriteLine(vehicle.ToString());
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        foreach (var driver in allDrivers)
                        {
                            Console.WriteLine(driver.ToString());
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        foreach (var pilot in pilots)
                        {
                            Console.WriteLine(pilot.ToString());
                            foreach (var license in pilot.Licenses)
                            {
                                Console.Write(license.ToString() + " - ");
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        foreach (var tankDriver in tankDrivers)
                        {
                            Console.WriteLine(tankDriver.ToString());
                            foreach (var license in tankDriver.Licenses)
                            {
                                Console.Write(license.ToString() + " - ");
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("The choice you have entered is not valid.");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 0);
        }

        static IList<Driver> GetDrivers(IList<Driver> drivers)
        {
            return drivers.Where(d => d.CanDriveGround()).ToList();
        }

        static IList<Driver> GetPilots(IList<Driver> drivers)
        {
            return drivers.Where(p => p.CanDriveAerial()).ToList();
        }

        static IList<Vehicle> GetAerialVehicles(IList<Vehicle> vehicles)
        {
            return vehicles.Where(v => v is IAerialVehicle).ToList();
        }

        static IList<Vehicle> GetGroundVehicles(IList<Vehicle> vehicles)
        {
            return vehicles.Where(v => v is IGroundVehicle).ToList();
        }
    }
}
