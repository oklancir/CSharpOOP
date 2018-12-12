using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Driver> drivers = new List<Driver>();
            IList<Vehicle> vehicles = new List<Vehicle>();

            var tank = new Tank() { Id = 34, Name = "Sherman" };
            var airplane = new Airplane() { Id = 737, Name = "Boeing" };
            var helicopter = new Helicopter() { Id = 28, Name = "Black Hawk" };

            vehicles.Add(tank);
            vehicles.Add(helicopter);
            vehicles.Add(airplane);

            var driverJohn = new Driver(helicopter) { Age = 18, FullName = "John McCain", Id = 1 };
            var driverSteve = new Driver(tank) { Age = 22, FullName = "Steve Buscemi", Id = 2 };
            var driverRingo = new Driver(airplane) { Age = 18, FullName = "Ringo Starr", Id = 3 };
            var driverHal = new Driver(new Tank() { Id = 777, Name = "Wasteland tank" }) { Age = 18, FullName = "John McCain", Id = 4 };
            var driverMotorola6800 = new Driver(new Airplane() { Id = 16, Name = "Vanquish" }) { Age = 22, FullName = "Steve Buscemi", Id = 5 };
            var driverCobol = new Driver(new Helicopter() { Id = 8, Name = "Not-A-Number" }) { Age = 18, FullName = "Ringo Starr", Id = 6 };
            Console.WriteLine("Print something: {0} {1}", "prvi string", "drugi string");
            drivers.Add(driverJohn);
            drivers.Add(driverSteve);
            drivers.Add(driverRingo);
            drivers.Add(driverHal);
            drivers.Add(driverMotorola6800);
            drivers.Add(driverCobol);

            var allDrivers = drivers;
            var pilots = GetPilots(allDrivers);
            var tankDrivers = GetDrivers(allDrivers);

            driverJohn.LearnToDrive(new Tank() { Id = 11, Name = "Abrams" });

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
                        var groundVehicles = GetGroundVehicles(vehicles);
                        foreach (var vehicle in groundVehicles)
                        {
                            Console.WriteLine(vehicle.ToString());
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        var aerialVehicles = GetGroundVehicles(vehicles);
                        foreach (var vehicle in aerialVehicles)
                        {
                            Console.WriteLine(vehicle.ToString());
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        foreach (var driver in allDrivers)
                        {
                            Console.WriteLine(driver.ToString());
                        }
                        Console.ReadKey();
                        break;
                    case 5:
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
