USE [master]
GO
/****** Object:  Database [detalDb]    Script Date: 02.04.2019 9:54:35 ******/
CREATE DATABASE [detalDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'detalDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\detalDb.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'detalDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\detalDb_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [detalDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [detalDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [detalDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [detalDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [detalDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [detalDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [detalDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [detalDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [detalDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [detalDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [detalDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [detalDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [detalDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [detalDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [detalDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [detalDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [detalDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [detalDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [detalDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [detalDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [detalDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [detalDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [detalDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [detalDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [detalDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [detalDb] SET  MULTI_USER 
GO
ALTER DATABASE [detalDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [detalDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [detalDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [detalDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [detalDb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [detalDb]
GO
/****** Object:  Table [dbo].[Brak]    Script Date: 02.04.2019 9:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Brak](
	[ID_Brak] [int] IDENTITY(1,1) NOT NULL,
	[ID_rabota] [int] NULL,
	[Prichina] [varchar](1000) NULL,
 CONSTRAINT [PK_Brak] PRIMARY KEY CLUSTERED 
(
	[ID_Brak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Detal]    Script Date: 02.04.2019 9:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Detal](
	[ID_Detal] [int] IDENTITY(1,1) NOT NULL,
	[NameDetal] [varchar](300) NOT NULL,
	[HarakteristiciDetali] [varchar](2000) NOT NULL,
	[VremyaNaIzgotovlenie] [time](7) NOT NULL,
	[ID_TD] [int] NULL,
 CONSTRAINT [PK_Detal] PRIMARY KEY CLUSTERED 
(
	[ID_Detal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Master]    Script Date: 02.04.2019 9:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Master](
	[ID_Master] [int] IDENTITY(1,1) NOT NULL,
	[F_Master] [varchar](200) NULL,
	[I_Master] [varchar](200) NULL,
	[O_Master] [varchar](200) NULL,
	[DataPriemaNaRabotu] [date] NULL,
	[BDate] [date] NULL,
 CONSTRAINT [PK_Master] PRIMARY KEY CLUSTERED 
(
	[ID_Master] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rabota]    Script Date: 02.04.2019 9:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rabota](
	[ID_rabota] [int] IDENTITY(1,1) NOT NULL,
	[DataNachalo] [date] NULL,
	[DataKonca] [date] NULL,
	[VremyaNachalo] [time](7) NULL,
	[VremyaKonca] [time](7) NULL,
	[ID_Detal] [int] NULL,
	[ID_Master] [int] NULL,
 CONSTRAINT [PK_Rabota] PRIMARY KEY CLUSTERED 
(
	[ID_rabota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipDetali]    Script Date: 02.04.2019 9:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipDetali](
	[ID_TD] [int] IDENTITY(1,1) NOT NULL,
	[NameTD] [varchar](200) NULL,
 CONSTRAINT [PK_TipDetali] PRIMARY KEY CLUSTERED 
(
	[ID_TD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user]    Script Date: 02.04.2019 9:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](200) NULL,
	[password] [varchar](200) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Brak] ON 

INSERT [dbo].[Brak] ([ID_Brak], [ID_rabota], [Prichina]) VALUES (1, 1, N'Simple')
INSERT [dbo].[Brak] ([ID_Brak], [ID_rabota], [Prichina]) VALUES (2, 3, N'Плохое качество детали')
INSERT [dbo].[Brak] ([ID_Brak], [ID_rabota], [Prichina]) VALUES (3, 2, N'fdbfdnb')
SET IDENTITY_INSERT [dbo].[Brak] OFF
SET IDENTITY_INSERT [dbo].[Detal] ON 

INSERT [dbo].[Detal] ([ID_Detal], [NameDetal], [HarakteristiciDetali], [VremyaNaIzgotovlenie], [ID_TD]) VALUES (1, N'Первая деталь', N'Хорошая', CAST(N'12:22:00' AS Time), 1)
INSERT [dbo].[Detal] ([ID_Detal], [NameDetal], [HarakteristiciDetali], [VremyaNaIzgotovlenie], [ID_TD]) VALUES (2, N'Detal #2', N'Ширана - 2000', CAST(N'00:20:00' AS Time), 2)
SET IDENTITY_INSERT [dbo].[Detal] OFF
SET IDENTITY_INSERT [dbo].[Master] ON 

INSERT [dbo].[Master] ([ID_Master], [F_Master], [I_Master], [O_Master], [DataPriemaNaRabotu], [BDate]) VALUES (1, N'Иванов', N'Иван', N'ИТванович', CAST(N'2019-01-23' AS Date), CAST(N'1989-02-24' AS Date))
SET IDENTITY_INSERT [dbo].[Master] OFF
SET IDENTITY_INSERT [dbo].[Rabota] ON 

INSERT [dbo].[Rabota] ([ID_rabota], [DataNachalo], [DataKonca], [VremyaNachalo], [VremyaKonca], [ID_Detal], [ID_Master]) VALUES (1, CAST(N'2019-01-04' AS Date), CAST(N'2019-01-21' AS Date), CAST(N'02:40:00' AS Time), CAST(N'05:04:00' AS Time), 2, 1)
INSERT [dbo].[Rabota] ([ID_rabota], [DataNachalo], [DataKonca], [VremyaNachalo], [VremyaKonca], [ID_Detal], [ID_Master]) VALUES (2, CAST(N'2019-01-03' AS Date), CAST(N'2019-01-19' AS Date), CAST(N'06:20:00' AS Time), CAST(N'00:20:00' AS Time), 1, 1)
INSERT [dbo].[Rabota] ([ID_rabota], [DataNachalo], [DataKonca], [VremyaNachalo], [VremyaKonca], [ID_Detal], [ID_Master]) VALUES (3, CAST(N'2019-01-09' AS Date), CAST(N'2019-01-20' AS Date), CAST(N'00:20:00' AS Time), CAST(N'00:30:00' AS Time), 2, 1)
INSERT [dbo].[Rabota] ([ID_rabota], [DataNachalo], [DataKonca], [VremyaNachalo], [VremyaKonca], [ID_Detal], [ID_Master]) VALUES (4, CAST(N'2019-01-04' AS Date), CAST(N'2019-01-24' AS Date), CAST(N'02:20:00' AS Time), CAST(N'03:20:00' AS Time), 1, 1)
INSERT [dbo].[Rabota] ([ID_rabota], [DataNachalo], [DataKonca], [VremyaNachalo], [VremyaKonca], [ID_Detal], [ID_Master]) VALUES (5, NULL, NULL, NULL, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[Rabota] OFF
SET IDENTITY_INSERT [dbo].[TipDetali] ON 

INSERT [dbo].[TipDetali] ([ID_TD], [NameTD]) VALUES (1, N'Шестеренка')
INSERT [dbo].[TipDetali] ([ID_TD], [NameTD]) VALUES (2, N'Ротор')
INSERT [dbo].[TipDetali] ([ID_TD], [NameTD]) VALUES (3, N'Винтовая')
INSERT [dbo].[TipDetali] ([ID_TD], [NameTD]) VALUES (6, N'Облегченная')
INSERT [dbo].[TipDetali] ([ID_TD], [NameTD]) VALUES (7, N'Стальная')
INSERT [dbo].[TipDetali] ([ID_TD], [NameTD]) VALUES (10, N'ccc')
INSERT [dbo].[TipDetali] ([ID_TD], [NameTD]) VALUES (12, N'кууцк')
SET IDENTITY_INSERT [dbo].[TipDetali] OFF
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([id], [login], [password]) VALUES (1, N'admin', N'admin')
SET IDENTITY_INSERT [dbo].[user] OFF
ALTER TABLE [dbo].[Brak]  WITH NOCHECK ADD  CONSTRAINT [FK_Brak_Rabota] FOREIGN KEY([ID_rabota])
REFERENCES [dbo].[Rabota] ([ID_rabota])
GO
ALTER TABLE [dbo].[Brak] NOCHECK CONSTRAINT [FK_Brak_Rabota]
GO
ALTER TABLE [dbo].[Detal]  WITH NOCHECK ADD  CONSTRAINT [FK_Detal_TipDetali] FOREIGN KEY([ID_TD])
REFERENCES [dbo].[TipDetali] ([ID_TD])
GO
ALTER TABLE [dbo].[Detal] NOCHECK CONSTRAINT [FK_Detal_TipDetali]
GO
ALTER TABLE [dbo].[Rabota]  WITH NOCHECK ADD  CONSTRAINT [FK_Rabota_Detal] FOREIGN KEY([ID_Detal])
REFERENCES [dbo].[Detal] ([ID_Detal])
GO
ALTER TABLE [dbo].[Rabota] NOCHECK CONSTRAINT [FK_Rabota_Detal]
GO
ALTER TABLE [dbo].[Rabota]  WITH NOCHECK ADD  CONSTRAINT [FK_Rabota_Master] FOREIGN KEY([ID_Master])
REFERENCES [dbo].[Master] ([ID_Master])
GO
ALTER TABLE [dbo].[Rabota] NOCHECK CONSTRAINT [FK_Rabota_Master]
GO
USE [master]
GO
ALTER DATABASE [detalDb] SET  READ_WRITE 
GO
