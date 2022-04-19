CREATE TABLE [dbo].[ParrotTranslations](
	Id Bigint PRIMARY KEY IDENTITY(1,1),
	BusinessId uniqueidentifier not null default(newId()),
	[Key] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](500) NOT NULL,
	[Culture] [nvarchar](5) NULL
	)


