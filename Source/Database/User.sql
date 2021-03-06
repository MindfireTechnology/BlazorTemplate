CREATE TABLE [dbo].[User]
(
	UserId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserName NVARCHAR (250) NULL,
	Email NVARCHAR (250) NULL,
	PhoneNumber NVARCHAR (25) NULL,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Birthdate DATETIME NULL,
	Created DateTimeOffset DEFAULT sysdatetimeoffset(),
	Modified DateTimeOffset
)
