USE [PowerPortal]
GO

/****** Object: Table [dbo].[Addresses] Script Date: 29.06.2021 13:33:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Addresses];


GO
CREATE TABLE [dbo].[Addresses] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [AddressType] NVARCHAR (MAX)   NULL,
    [StreetLine1] NVARCHAR (MAX)   NULL,
    [StreetLine2] NVARCHAR (MAX)   NULL,
    [StreetLine3] NVARCHAR (MAX)   NULL,
    [Zip]         NVARCHAR (MAX)   NULL,
    [City]        NVARCHAR (MAX)   NULL,
    [Country]     NVARCHAR (MAX)   NULL,
    [OwnerId]     UNIQUEIDENTIFIER NULL,
    [AccountId]   UNIQUEIDENTIFIER NULL
);


