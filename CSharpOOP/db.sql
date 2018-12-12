DROP TABLE IF EXISTS dbo.Licenses;
DROP TABLE IF EXISTS dbo.Vehicles;
DROP TABLE IF EXISTS dbo.VehicleType;
DROP TABLE IF EXISTS dbo.Drivers;
DROP TABLE IF EXISTS dbo.Logs;

CREATE TABLE dbo.Logs 
(
	LogID INT IDENTITY PRIMARY KEY,
	DateTimeStamp DATETIME NOT NULL,
	MessageLevel VARCHAR(10) NOT NULL,
	MessageContent VARCHAR(100)
);

CREATE TABLE dbo.VehicleType(
	VehicleTypeID NVARCHAR(30) PRIMARY KEY,
	VehicleTypeName VARCHAR(50)
);

CREATE TABLE dbo.Drivers (
	DriverID INT PRIMARY KEY,
	FullName VARCHAR(50),
	Age smallint
);

CREATE TABLE dbo.Vehicles(
	VehicleID INT PRIMARY KEY,
	VehicleName VARCHAR(50),
	VehicleTypeID NVARCHAR(30),
	DriverID INT,
	Color VARCHAR(20),
	FOREIGN KEY(DriverID) REFERENCES dbo.Drivers(DriverID),
	CONSTRAINT FK_Vehicles_VehicleType FOREIGN KEY(VehicleTypeID) REFERENCES dbo.VehicleType(VehicleTypeID)
);

CREATE TABLE dbo.Licenses (
	DriverID INT NOT NULL,
	VehicleID INT NOT NULL,
	DateIssued DATETIME,
	CONSTRAINT PK_Licences_DriverID_VehicleID PRIMARY KEY (DriverID, VehicleID),
	CONSTRAINT FK_Licences_Drivers FOREIGN KEY (DriverID) REFERENCES dbo.Drivers(DriverID),
	CONSTRAINT FK_Licences_Vehicles FOREIGN KEY (VehicleID) REFERENCES dbo.Vehicles(VehicleID)
	ON DELETE CASCADE ON UPDATE CASCADE
);