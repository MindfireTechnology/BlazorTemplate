/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
			   SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT dbo.[User] ON
GO
MERGE dbo.[User] AS Target
USING (VALUES
	(1, 'TestUser', 'testuser@email.com', '801-123-7890', 'Test', 'User', '05/19/71')
) AS Source (UserId, UserName, Email, PhoneNumber, FirstName, LastName, Birthdate)
ON (Target.UserId = Source.UserId)
WHEN NOT MATCHED BY TARGET THEN	INSERT (UserId, UserName, Email, PhoneNumber, FirstName, LastName, Birthdate) VALUES (UserId, UserName, Email, PhoneNumber, FirstName, LastName, Birthdate);
SET IDENTITY_INSERT dbo.[User] OFF
GO

SET IDENTITY_INSERT dbo.[Address] ON
GO
MERGE dbo.[Address] AS Target
USING (VALUES
	(1, 1, 'home', '357 Magnum', 'Apt 38', 'Winchester', 'VA', '22601'),
	(2, 1, 'work', '872 W Heritage Park Blvd', 'Suite 200', 'Layton', 'UT', '84041')
) AS Source (AddressId, UserId, AddressType, Street1, Street2, City, State, Zip)
ON (Target.AddressId = Source.AddressId)
WHEN NOT MATCHED BY TARGET THEN INSERT (AddressId, UserId, AddressType, Street1, Street2, City, State, Zip) VALUES (AddressId, UserId, AddressType, Street1, Street2, City, State, Zip);
SET IDENTITY_INSERT dbo.[Address] OFF
GO
