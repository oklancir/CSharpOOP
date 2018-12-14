using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CSharpOOP
{
    class Program
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            IList<Driver> drivers = new List<Driver>();
            IList<Vehicle> vehicles = new List<Vehicle>();

            var tank = new Tank() { Id = 34, Name = "Sherman", Color = "Blue" };
            var airplane = new Airplane() { Id = 737, Name = "Boeing", Color = "Dark Orange" };
            var helicopter = new Helicopter() { Id = 28, Name = "Black Hawk", Color = "Pink" };

            vehicles.Add(tank);
            vehicles.Add(helicopter);
            vehicles.Add(airplane);

            var driverJohn = new Driver(helicopter) { Age = 18, FullName = "John McCain", Id = 1 };
            var driverSteve = new Driver(tank) { Age = 22, FullName = "Steve Buscemi", Id = 2 };
            var driverRingo = new Driver(airplane) { Age = 18, FullName = "Ringo Starr", Id = 3 };
            var driverHal = new Driver(new Tank() { Id = 777, Name = "Wasteland tank", Color = "Blue" }) { Age = 18, FullName = "John McCain", Id = 4 };
            var driverMotorola6800 = new Driver(new Airplane() { Id = 16, Name = "Vanquish", Color = "Blue" }) { Age = 22, FullName = "Steve Buscemi", Id = 5 };
            var driverCobol = new Driver(new Helicopter() { Id = 8, Name = "Not-A-Number", Color = "Blue" }) { Age = 18, FullName = "Ringo Starr", Id = 6 };

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
                Console.WriteLine("1 - All vehicles" +
                                  "\n2 - Ground vehicles" +
                                  "\n3 - Aerial vehicles" +
                                  "\n4 - All drivers" +
                                  "\n5 - Ground drivers" +
                                  "\n6 - Pilots" +
                                  "\n7 - List Drivers from Database" +
                                  "\n8 - List Vehicles from Database" +
                                  "\n9 - Add a new Driver to Database" +
                                  "\n10 - Add a new Vehicle to Database" +
                                  "\n11 - Assign a new License to a Driver" +
                                  "\n12 - Remove a Driver from Database" +
                                  "\n13 - Remove a Vehicle from Database" +
                                  "\n0 - EXIT ");
                Console.Write("Choice: ");

                switch (choice = int.Parse(Console.ReadLine()))
                {
                    case 1:
                        foreach (var vehicle in vehicles)
                        {
                            Console.WriteLine("Vehicle: " + vehicle.ToString());
                            VehiclesDatabase.AddVehicle(vehicle);
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
                            VehiclesDatabase.AddDriver(driver);
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
                    case 9:
                        AddDriverToDb();
                        break;
                    case 10:
                        AddVehicleToDb();
                        break;
                    case 11:
                        AssignLicense();
                        break;
                    case 12:
                        RemoveDriverFromDb();
                        break;
                    case 13:
                        RemoveVehicleFromDb();
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

        private static IList<Driver> GetDrivers(IList<Driver> drivers)
        {
            return drivers.Where(d => d.CanDriveGround()).ToList();
        }

        private static IList<Driver> GetPilots(IList<Driver> drivers)
        {
            return drivers.Where(p => p.CanDriveAerial()).ToList();
        }

        private static IList<Vehicle> GetAerialVehicles(IList<Vehicle> vehicles)
        {
            return vehicles.Where(v => v is IAerialVehicle).ToList();
        }

        private static IList<Vehicle> GetGroundVehicles(IList<Vehicle> vehicles)
        {
            return vehicles.Where(v => v is IGroundVehicle).ToList();
        }

        private static void AddDriverToDb()
        {
            Console.Clear();
            Console.Write("Full Name: ");
            var fullName = Console.ReadLine();
            Console.Write("Age: ");
            var age = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("You did not enter a valid number for age"));

            VehiclesDatabase.AddDriver(new Driver() { FullName = fullName, Age = age });
        }

        private static void AddVehicleToDb()
        {
            Console.Clear();
            Console.Write("Vehicle name: ");
            var vehicleName = Console.ReadLine();
            Console.Write("Color: ");
            var color = Console.ReadLine();
            Console.Write("Vehicle type (Tank, Helicopter, Airplane): ");
            var vehicleType = Console.ReadLine();

            if (vehicleType == "Tank")
            {
                VehiclesDatabase.AddVehicle(new Tank() { Name = vehicleName, Color = color });
            }
            else if (vehicleType == "Helicopter")
            {
                VehiclesDatabase.AddVehicle(new Helicopter() { Name = vehicleName, Color = color });
            }
            else if (vehicleType == "Airplane")
            {
                VehiclesDatabase.AddVehicle(new Airplane() { Name = vehicleName, Color = color });
            }
            else
            {
                Console.WriteLine("You did not enter a proper vehicle type.");
            }
        }

        private static void AssignLicense()
        {
            Console.Clear();
            Console.Write("Driver id: ");
            var driverId = int.Parse(Console.ReadLine());

            Console.Write("Vehicle type (1: Tank, 2: Helicopter, 3: Airplane): ");
            var vehicleType = int.Parse(Console.ReadLine());

            Console.Write("Vehicle name: ");
            var vehicleName = Console.ReadLine();

            Console.Write("Color: ");
            var color = Console.ReadLine();

            var license = new License();

            switch (vehicleType)
            {
                case 1:
                    license.Vehicle = new Tank() { Name = vehicleName, Color = color };
                    break;
                case 2:
                    license.Vehicle = new Helicopter() { Name = vehicleName, Color = color };
                    break;
                default:
                    license.Vehicle = new Airplane() { Name = vehicleName, Color = color };
                    break;
            }

            VehiclesDatabase.AddLicense(license, driverId);
        }

        private static void RemoveDriverFromDb()
        {
            Console.Clear();
            var drivers = VehiclesDatabase.GetDriversFromDb();
            Console.Write("Select a driver from the list to remove.");
            foreach (var driver in drivers)
            {

                Console.WriteLine(driver.ToString());
            }

            Console.Write("Driver to remove: ");
            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Logger.Info("Wrong ID.");
                Console.Write("--> ");
            }

            if (drivers.All(d => d.Id != id))
                Logger.Info("The driver with the given ID does not exist.");
            else
                VehiclesDatabase.RemoveDriver(drivers.Find(v => v.Id == id));
        }

        private static void RemoveVehicleFromDb()
        {
            Console.Clear();
            var vehicles = VehiclesDatabase.GetVehicles();
            Console.Write("Select a vehicle from the list to remove.");

            vehicles.ForEach(v => Console.WriteLine(v.ToString()));

            Console.Write("Vehicle to remove: ");
            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Logger.Info("Wrong ID.");
                Console.Write("--> ");
            }

            if (vehicles.All(v => v.Id != id))
                Logger.Info("A vehicle with the given ID does not exist.");
            else
                VehiclesDatabase.RemoveVehicle(vehicles.Find(v => v.Id == id));
        }
    }
}
