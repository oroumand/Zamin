Create Table dbo.Applications
(
Id INT PRIMARY KEY IDENTITY(1,1),
ServiceName NVARCHAR(100) NOT NULL Unique,
Title NVARCHAR(100) NOT NULL Unique,
Description NVARCHAR(500) NOT NULL Unique,
Logo NVARCHAR(500) NOT NULL Unique
)

Create Table dbo.Controllers
(
Id INT PRIMARY KEY IDENTITY(1,1),
ApplicationId INT Not Null References dbo.Applications(Id),
Name NVARCHAR(100) NOT NULL Unique,
Title NVARCHAR(100) NOT NULL Unique,
Description NVARCHAR(500) NOT NULL Unique
)
Create Table dbo.Actions
(
Id INT PRIMARY KEY IDENTITY(1,1),
ControllerId INT Not Null References dbo.Controllers(Id),
Name NVARCHAR(100) NOT NULL Unique,
Title NVARCHAR(100) NOT NULL Unique,
Description NVARCHAR(500) NOT NULL Unique
)

