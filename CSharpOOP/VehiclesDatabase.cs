using NLog;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace CSharpOOP
{
    public class VehiclesDatabase
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
        private static Logger Logger = LogManager.GetLogger("dbLogger");
        private SqlConnection Connection = new SqlConnection(ConnectionString);

        public SqlConnection OpenConnection()
        {

            try
            {
                Connection.Open();
                Logger.Info("Connection open.");
            }
            catch (SqlException e)
            {
                Logger.Error(e, "Error while opening the connection.");
            }

            return Connection;
        }

        public void CloseConnection()
        {
            try
            {
                Connection.Close();
                Logger.Info("Connection closed.");
            }
            catch (SqlException e)
            {
                Logger.Error(e, "Error while closing the connection.");
            }
        }

        public List<Vehicle> GetVehicles()
        {
            Connection.Open();
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                string sqlCommandString = "SELECT VehicleID, Name, Color, VehicleTypeID FROM dbo.Vehicles";
                SqlCommand command = new SqlCommand(sqlCommandString, Connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    switch (dataReader.GetInt32(3))
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

                Logger.Info("Fetched vehicles.");
            }
            catch (SqlException e)
            {
                Logger.Error(e, $"Error while fetching vehicles.");
            }
            finally
            {
                CloseConnection();
            }

            return vehicles;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            OpenConnection();
            try
            {
                string sqlCommandString = "INSERT INTO dbo.Vehicles (Name, Color, VehicleTypeID) " +
                                          "VALUES (@Name, @Color, @VehicleTypeID";
                SqlCommand command = new SqlCommand(sqlCommandString, Connection);

                command.Parameters.AddWithValue("@Name", vehicle.Name);
                command.Parameters.AddWithValue("@Color", vehicle.Color);
                if (vehicle is Tank)
                {
                    command.Parameters.AddWithValue("@VehicleTypeId", 1);
                }
                else if (vehicle is Helicopter)
                {
                    command.Parameters.AddWithValue("@VehicleTypeId", 2);
                }
                else
                {
                    command.Parameters.AddWithValue("@VehicleTypeId", 3);
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

        public void RemoveVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                return;

            Connection = OpenConnection();

            try
            {
                string sqlCommandString = "DELETE FROM dbo.vehicles WHERE VehicleID = @Id";
                SqlCommand command = new SqlCommand(sqlCommandString, Connection);
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
    }
}