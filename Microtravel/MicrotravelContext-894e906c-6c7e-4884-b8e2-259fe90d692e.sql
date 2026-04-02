USE [MicrotravelContext-894e906c-6c7e-4884-b8e2-259fe90d692e]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2026. 04. 02. 7:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Apiuser]    Script Date: 2026. 04. 02. 7:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apiuser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ApiToken] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Apiuser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Travel]    Script Date: 2026. 04. 02. 7:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Travel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[TravelTypeId] [int] NOT NULL,
	[TravelDealTypeId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [int] NOT NULL,
	[travelPictureUrl] [nvarchar](max) NULL,
	[TravelDate] [datetime2](7) NOT NULL,
	[TravelRegDate] [datetime2](7) NULL,
	[Enabled] [int] NULL,
	[TravelIdentifier] [nvarchar](max) NULL,
 CONSTRAINT [PK_Travel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TravelDealType]    Script Date: 2026. 04. 02. 7:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TravelDealType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TravelDealType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TravelType]    Script Date: 2026. 04. 02. 7:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TravelType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TravelType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Travel]  WITH CHECK ADD  CONSTRAINT [FK_Travel_TravelDealType_TravelDealTypeId] FOREIGN KEY([TravelDealTypeId])
REFERENCES [dbo].[TravelDealType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Travel] CHECK CONSTRAINT [FK_Travel_TravelDealType_TravelDealTypeId]
GO
ALTER TABLE [dbo].[Travel]  WITH CHECK ADD  CONSTRAINT [FK_Travel_TravelType_TravelTypeId] FOREIGN KEY([TravelTypeId])
REFERENCES [dbo].[TravelType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Travel] CHECK CONSTRAINT [FK_Travel_TravelType_TravelTypeId]
GO
/****** Object:  StoredProcedure [dbo].[StpResetDatabase]    Script Date: 2026. 04. 02. 7:38:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JI>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[StpResetDatabase]



AS
BEGIN
	TRUNCATE TABLE Travel;

	--SELECT * FROM dbo.Travel



INSERT INTO dbo.Travel 
VALUES
('Utazás Rómába', 6, 2, 'Nullam ultrices laoreet purus, non tempor ante egestas eu. Etiam mollis pretium consectetur. Aliquam erat volutpat. Etiam quis ipsum et turpis maximus viverra.', 101, '/images/roma-az-orok-varos-2484291856.jpg', '2026-02-27 14:10:00', '2026-02-12 10:11:16', 1, NULL),
('Utazás Grácba', 4, 3, 'Nullam ultrices laoreet purus, non tempor ante egestas eu. Etiam mollis pretium consectetur. Aliquam erat volutpat. Etiam quis ipsum et turpis maximus viverra.', 80, '/images/utazas_grac.jpg', '2026-02-21 20:49:00', '2026-02-16 12:11:16', 1, NULL),
('Utazás Párizsba', 5, 2, 'Nullam ultrices laoreet purus, non tempor ante egestas eu. Etiam mollis pretium consectetur. Aliquam erat volutpat. Etiam quis ipsum et turpis maximus viverra.', 120, '/images/parizs-emily-in-paris-3735084617.jpg', '2026-02-08 21:47:00', '2026-02-17 14:11:16', 1, NULL),
('Utazás Debrecenbe', 4, 1, 'Nullam ultrices laoreet purus, non tempor ante egestas eu. Etiam mollis pretium consectetur. Aliquam erat volutpat. Etiam quis ipsum et turpis maximus viverra.', 10, '/images/debrecen-ungarn.jpg', '2026-02-27 20:15:00', '2026-02-20 15:11:16', 1, NULL),
('Utazás Budapestre', 6, 2, 'Nullam ultrices laoreet purus, non tempor ante egestas eu. Etiam mollis pretium consectetur. Aliquam erat volutpat. Etiam quis ipsum et turpis maximus viverra.', 81, '/images/utazas_budapestre.jpg', '2026-03-05 14:11:00', '2026-03-01 15:03:09', 0, NULL);




END
GO
