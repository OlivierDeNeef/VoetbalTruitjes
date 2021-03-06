USE [Voetbaltruitjes]
GO
/****** Object:  Table [dbo].[Bestellingen]    Script Date: 5/12/2021 0:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bestellingen](
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
/****** Object:  Table [dbo].[Clubs]    Script Date: 5/12/2021 0:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clubs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Competitie] [nvarchar](100) NOT NULL,
	[Naam] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Klanten]    Script Date: 5/12/2021 0:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Klanten](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](255) NOT NULL,
	[Adres] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TruitjeBestellingen]    Script Date: 5/12/2021 0:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TruitjeBestellingen](
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
/****** Object:  Table [dbo].[Truitjes]    Script Date: 5/12/2021 0:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Truitjes](
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
ALTER TABLE [dbo].[Bestellingen]  WITH CHECK ADD  CONSTRAINT [FK_Bestelling_Klant] FOREIGN KEY([KlantId])
REFERENCES [dbo].[Klanten] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bestellingen] CHECK CONSTRAINT [FK_Bestelling_Klant]
GO
ALTER TABLE [dbo].[TruitjeBestellingen]  WITH CHECK ADD  CONSTRAINT [FK_TruitjeBestelling_Bestelling] FOREIGN KEY([BestellingId])
REFERENCES [dbo].[Bestellingen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TruitjeBestellingen] CHECK CONSTRAINT [FK_TruitjeBestelling_Bestelling]
GO
ALTER TABLE [dbo].[TruitjeBestellingen]  WITH CHECK ADD  CONSTRAINT [FK_TruitjeBestelling_Truitje] FOREIGN KEY([TruitjeId])
REFERENCES [dbo].[Truitjes] ([Id])
GO
ALTER TABLE [dbo].[TruitjeBestellingen] CHECK CONSTRAINT [FK_TruitjeBestelling_Truitje]
GO
ALTER TABLE [dbo].[Truitjes]  WITH CHECK ADD  CONSTRAINT [FK_Truitje_Club] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Clubs] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Truitjes] CHECK CONSTRAINT [FK_Truitje_Club]
GO
