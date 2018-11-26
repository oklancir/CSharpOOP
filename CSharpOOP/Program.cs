using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            var tank = new Tank();
            vehicles.Add(tank);
            var helicopter = new Helicopter() { Id = "286", Name = "Black Hawk" };
            vehicles.Add(helicopter);
            var airplane = new Airplane() { Id = "737", Name = "Boeing" };
            vehicles.Add(airplane);
            var driverJohn = new Driver(helicopter) { Age = 18, FullName = "John McCain", Id = "John" };
            var driverSteve = new Driver(tank) { Age = 22, FullName = "Steve Buscemi", Id = "Cobra" };
            var driverRingo = new Driver(airplane) { Age = 18, FullName = "Ringo Starr", Id = "Not-A-Drummer" };

            drivers.Add(driverJohn);
            drivers.Add(driverSteve);
            drivers.Add(driverRingo);

            var allDrivers = drivers;
            var pilot = GetDrivers(typeof(IAerialVehicle), allDrivers);
            var tankDrivers = GetDrivers(typeof(IGroundVehicle), allDrivers);

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
                            Console.WriteLine("Vehicle" + vehicle.Id + " " + vehicle.Name);
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                        // TODO:
                        Console.ReadKey();
                        break;
                    case 3:
                        // TODO:
                        Console.ReadKey();
                        break;
                    case 4:
                        // TODO:
                        Console.ReadKey();
                        break;
                    case 5:
                        // TODO:
                        Console.ReadKey();
                        break;
                    case 6:
                        // TODO:
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

        static IList<Driver> GetDrivers(Type vehicleType, IList<Driver> drivers)
        {
            return drivers.Where(v => v.CanDrive(vehicleType)).ToList();
        }
    }
}
