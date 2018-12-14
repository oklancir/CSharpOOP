using System;
using NLog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace CSharpOOP
{
    public class VehiclesDatabase
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
        private static readonly Logger Logger = LogManager.GetLogger("dbLogger");

        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                Logger.Info("Connection open.");
            }
            catch (SqlException e)
            {
                Logger.Error(e, "Error while opening the connection.");
            }

            return connection;
        }

        public static void CloseConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Close();
                Logger.Info("Connection closed.");
            }
            catch (SqlException e)
            {
                Logger.Error(e, "Error while closing the connection.");
            }
        }

        public static List<Vehicle> GetVehicles()
        {
            SqlConnection connection = OpenConnection();
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                string sqlCommandString = "SELECT VehicleID, VehicleName, VehicleTypeID, Color  FROM dbo.Vehicles";
                SqlCommand command = new SqlCommand(sqlCommandString, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    switch (dataReader.GetValue(3))
                    {
                        case 1:
                            Tank tank = new Tank()
                            {
                                Id = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1),
                                Color = dataReader.GetString(2)
                            };
                            break;
                        case 2:
                            Helicopter helicopter = new Helicopter
                            {
                                Id = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1),
                                Color = dataReader.GetString(2)
                            };
                            break;
                        case 3:
                            Airplane airplane = new Airplane()
                            {
                                Id = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1),
                                Color = dataReader.GetString(2)
                            };
                            break;
                    }
                }

                Logger.Info("Vehicles fetched successfully.");
            }
            catch (SqlException e)
            {
                Logger.Error(e, "Error while fetching vehicles.");
            }
            finally
            {
                CloseConnection();
            }

            return vehicles;
        }

        public static void AddVehicle(Vehicle vehicle)
        {
            SqlConnection connection = OpenConnection();

            try
            {
                string sqlCommandString = "INSERT INTO dbo.Vehicles (VehicleName, Color, VehicleTypeID) " +
                                          "VALUES (@Name, @Color, @VehicleTypeID)";
                SqlCommand command = new SqlCommand(sqlCommandString, connection);

                command.Parameters.AddWithValue("@Name", vehicle.Name);
                command.Parameters.AddWithValue("@Color", vehicle.Color);
                if (vehicle is Tank)
                {
                    command.Parameters.AddWithValue("@VehicleTypeID", 1);
                }
                else if (vehicle is Helicopter)
                {
                    command.Parameters.AddWithValue("@VehicleTypeID", 2);
                }
                else
                {
                    command.Parameters.AddWithValue("@VehicleTypeID", 3);
                }

                if (command.ExecuteNonQuery() > 0)
                {
                    Logger.Info($"You have added {vehicle.ToString()}");
                }
                else
                {
                    Logger.Error($"Error while adding {vehicle.ToString()}");
                }
            }
            catch (SqlException e)
            {
                Logger.Error(e, $"Error while adding: {vehicle.ToString()}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public static void RemoveVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                return;

            SqlConnection connection = OpenConnection();

            try
            {
                string sqlCommandString = "DELETE FROM dbo.vehicles WHERE VehicleID = @Id";
                SqlCommand command = new SqlCommand(sqlCommandString, connection);
                command.Parameters.AddWithValue("@Id", vehicle.Id);

                if (command.ExecuteNonQuery() > 0)
                    Logger.Info($"Vehicle {vehicle.ToString()} has been removed.");
                else
                    Logger.Error($"Error while trying to remove {vehicle.ToString()}");
            }
            catch (SqlException e)
            {
                Logger.Error(e, $"Error while trying to remove {vehicle.ToString()}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public static List<Driver> GetDriversFromDb()
        {
            SqlConnection connection = OpenConnection();
            List<Driver> drivers = new List<Driver>();

            try
            {
                string sqlCommandString =
                    "SELECT dbo.Drivers.DriverID, FullName, Age, VehicleTypeID" +
                    " FROM dbo.Drivers LEFT JOIN dbo.Licenses ON dbo.Licenses.DriverID = dbo.Drivers.DriverID;";
                SqlCommand command = new SqlCommand(sqlCommandString, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    if (drivers.All(v => v.Id != dataReader.GetInt32(0)))
                    {
                        var driver = new Driver()
                        {
                            Id = Convert.ToInt32(dataReader.GetValue(0)),
                            FullName = dataReader.GetString(1),
                            Age = Convert.ToInt32(dataReader.GetValue(2))

                        };

                        if (!dataReader.IsDBNull(2))
                        {
                            if (dataReader.GetInt32(2) == 1)
                                driver.LearnToDrive(new Tank());
                            else if (dataReader.GetInt32(2) == 2)
                                driver.LearnToDrive(new Helicopter());
                            else
                                driver.LearnToDrive(new Airplane());
                        }
                        drivers.Add(driver);
                    }
                    else
                    {
                        if (dataReader.GetInt32(4) == 1)
                            drivers.Last().LearnToDrive(new Tank());
                        else if (dataReader.GetInt32(4) == 2)
                            drivers.Last().LearnToDrive(new Helicopter());
                        else
                            drivers.Last().LearnToDrive(new Airplane());
                    }
                }
                Logger.Info("Drivers fetched successfully.");
            }
            catch (SqlException e)
            {
                Logger.Error(e, "Error while fetching drivers.");
            }
            finally
            {
                CloseConnection();
            }

            return drivers;
        }

        public static void AddDriver(Driver driver)
        {
            SqlConnection connection = OpenConnection();
            try
            {
                string sqlCommandString =
                    "INSERT INTO dbo.Drivers (FullName, Age) " +
                    "VALUES (@FullName, @Age)";
                SqlCommand command = new SqlCommand(sqlCommandString, connection);

                command.Parameters.AddWithValue("@FullName", driver.FullName);
                command.Parameters.AddWithValue("@Age", driver.Age);

                if (command.ExecuteNonQuery() > 0)
                {
                    Logger.Info($"Driver {driver.ToString()} has been successfully added.");
                }
                else
                {
                    Logger.Error($"Error while adding driver {driver.ToString()}");
                }
            }
            catch (SqlException e)
            {
                Logger.Error(e, $"Error while adding driver {driver.ToString()}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public static void RemoveDriver(Driver driver)
        {
            if (driver == null)
                return;

            SqlConnection connection = OpenConnection();
            try
            {
                string sqlCommandString =
                    "DELETE FROM dbo.Drivers WHERE DriverID = @Id";
                SqlCommand command = new SqlCommand(sqlCommandString, connection);
                command.Parameters.AddWithValue("@Id", driver.Id);

                if (command.ExecuteNonQuery() > 0)
                {
                    Logger.Info($"Driver {driver.ToString()} has been successfully removed.");
                }
                else
                {
                    Logger.Error($"Error while trying to remove driver {driver.ToString()}");
                }
            }
            catch (SqlException e)
            {
                Logger.Error(e, $"Error while trying to remove driver {driver.ToString()}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public static void AddLicense(License license, int driverId)
        {
            SqlConnection connection = OpenConnection();
            try
            {
                string sqlCommandString =
                    "INSERT INTO dbo.Licenses VALUES (@DriverID, @VehicleTypeID, @DateIssued)";
                SqlCommand command = new SqlCommand(sqlCommandString, connection);

                command.Parameters.AddWithValue("@DriverID", driverId);
                command.Parameters.AddWithValue("@DateIssued", license.DateIssued);
                if (license.Vehicle is Tank)
                    command.Parameters.AddWithValue("@VehicleTypeID", 1);
                else if (license.Vehicle is Helicopter)
                    command.Parameters.AddWithValue("@VehicleTypeID", 2);
                else
                    command.Parameters.AddWithValue("@VehicleTypeID", 3);


                if (command.ExecuteNonQuery() > 0)
                {
                    Logger.Info($"Added a new license for the driver: {license.ToString()} ");
                }
                else
                {
                    Logger.Error($"Error while adding a license for the driver: {license.ToString()}");
                }
            }
            catch (SqlException e)
            {
                Logger.Error(e, $"Error while adding a license for the driver: {license.ToString()}");

            }
            finally
            {
                CloseConnection();
            }
        }
    }
}