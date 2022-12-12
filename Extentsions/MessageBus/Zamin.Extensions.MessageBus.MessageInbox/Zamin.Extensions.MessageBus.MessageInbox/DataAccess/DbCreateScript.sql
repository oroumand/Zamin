CREATE TABLE [dbo].[MessageInbox](
	[Id] [bigint] IDENTITY(1,1) Primary Key,
	[OwnerService] [nvarchar](100) NOT NULL,
	[MessageId] [nvarchar](50) NOT NULL,
	[Payload] nvarchar(max),
	[ReceivedDate] DateTime default(GetDate()),
	UNIQUE ([OwnerService],[MessageId])
)
