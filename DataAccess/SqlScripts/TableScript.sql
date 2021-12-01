USE [Voetbaltruitjes]
GO
/****** Object:  Table [dbo].[Bestelling]    Script Date: 1/12/2021 19:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bestelling](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Prijs] [decimal](19, 4) NOT NULL,
	[Betaald] [bit] NOT NULL,
	[KlantId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Club]    Script Date: 1/12/2021 19:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Club](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Competitie] [nvarchar](100) NOT NULL,
	[Ploeg] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Klant]    Script Date: 1/12/2021 19:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Klant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](255) NOT NULL,
	[Adres] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Truitje]    Script Date: 1/12/2021 19:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Truitje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kledingmaat] [nvarchar](25) NOT NULL,
	[Seizoen] [nvarchar](25) NOT NULL,
	[Prijs] [decimal](19, 4) NOT NULL,
	[Thuis] [bit] NOT NULL,
	[Versie] [int] NOT NULL,
	[ClubId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TruitjeBestelling]    Script Date: 1/12/2021 19:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TruitjeBestelling](
	[BestellingId] [int] NOT NULL,
	[TruitjeId] [int] NOT NULL,
	[Aantal] [int] NOT NULL,
 CONSTRAINT [PK_TruitjeBestelling] PRIMARY KEY CLUSTERED 
(
	[BestellingId] ASC,
	[TruitjeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bestelling]  WITH CHECK ADD  CONSTRAINT [FK_Bestelling_Klant] FOREIGN KEY([KlantId])
REFERENCES [dbo].[Klant] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bestelling] CHECK CONSTRAINT [FK_Bestelling_Klant]
GO
ALTER TABLE [dbo].[Truitje]  WITH CHECK ADD  CONSTRAINT [FK_Truitje_Club] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Club] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Truitje] CHECK CONSTRAINT [FK_Truitje_Club]
GO
ALTER TABLE [dbo].[TruitjeBestelling]  WITH CHECK ADD  CONSTRAINT [FK_TruitjeBestelling_Bestelling] FOREIGN KEY([BestellingId])
REFERENCES [dbo].[Bestelling] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TruitjeBestelling] CHECK CONSTRAINT [FK_TruitjeBestelling_Bestelling]
GO
ALTER TABLE [dbo].[TruitjeBestelling]  WITH CHECK ADD  CONSTRAINT [FK_TruitjeBestelling_Truitje] FOREIGN KEY([TruitjeId])
REFERENCES [dbo].[Truitje] ([Id])
GO
ALTER TABLE [dbo].[TruitjeBestelling] CHECK CONSTRAINT [FK_TruitjeBestelling_Truitje]
GO
