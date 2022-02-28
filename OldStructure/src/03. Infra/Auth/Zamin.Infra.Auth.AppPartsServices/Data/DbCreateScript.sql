

Create Table dbo.Applications
(
Id Bigint PRIMARY KEY IDENTITY(1,1),
BusinessId uniqueidentifier not null default(newId()),
ServiceName NVARCHAR(100) NOT NULL Unique,
Title NVARCHAR(100) NOT NULL Unique,
Description NVARCHAR(500) NOT NULL Unique,
Logo NVARCHAR(500) NOT NULL Unique
)

Create Table dbo.Controllers
(
Id Bigint PRIMARY KEY IDENTITY(1,1),
BusinessId uniqueidentifier not null default(newId()),
ApplicationId INT Not Null References dbo.Applications(Id),
Name NVARCHAR(100) NOT NULL Unique,
Title NVARCHAR(100) NOT NULL Unique,
Description NVARCHAR(500) NOT NULL Unique
)
Create Table dbo.Actions
(
Id Bigint PRIMARY KEY IDENTITY(1,1),
BusinessId uniqueidentifier not null default(newId()),
ControllerId INT Not Null References dbo.Controllers(Id),
Name NVARCHAR(100) NOT NULL Unique,
Title NVARCHAR(100) NOT NULL Unique,
Description NVARCHAR(500) NOT NULL Unique
)
