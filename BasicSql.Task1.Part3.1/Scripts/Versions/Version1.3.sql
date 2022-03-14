/*
Update script to version 1.3.
*/

IF OBJECT_ID(N'[dbo].[Regions]', N'U') IS NULL
BEGIN
	EXECUTE sp_rename N'[dbo].[Region]', N'Regions';
END

GO

IF (OBJECT_ID(N'[dbo].[Customers]') IS NOT NULL AND
    COLUMNPROPERTY( OBJECT_ID(N'[dbo].[Customers]'), 'EstablishedDate', 'ColumnId') IS NULL)
BEGIN
	ALTER TABLE [dbo].[Customers]
	ADD "EstablishedDate" "datetime" NULL
END