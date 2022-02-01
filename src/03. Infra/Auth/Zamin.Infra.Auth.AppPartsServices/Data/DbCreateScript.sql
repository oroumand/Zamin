CREATE TABLE [dbo].[ParrotTranslations](
	[Id] [int] IDENTITY(1,1) NOT NULL Primary Key,
	[Key] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](500) NOT NULL,
	[Culture] [nvarchar](5) NULL
	)

