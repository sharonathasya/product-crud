USE [master]
GO
/****** Object:  Database [dbProduct]    Script Date: 4/17/2025 11:28:33 AM ******/
CREATE DATABASE [dbProduct]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbProduct', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbProduct.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbProduct_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbProduct_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbProduct] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbProduct].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbProduct] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbProduct] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbProduct] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbProduct] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbProduct] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbProduct] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbProduct] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbProduct] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbProduct] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbProduct] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbProduct] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbProduct] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbProduct] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbProduct] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbProduct] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbProduct] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbProduct] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbProduct] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbProduct] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbProduct] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbProduct] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbProduct] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbProduct] SET RECOVERY FULL 
GO
ALTER DATABASE [dbProduct] SET  MULTI_USER 
GO
ALTER DATABASE [dbProduct] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbProduct] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbProduct] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbProduct] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbProduct] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbProduct] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbProduct', N'ON'
GO
ALTER DATABASE [dbProduct] SET QUERY_STORE = OFF
GO
USE [dbProduct]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 4/17/2025 11:28:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Email] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/17/2025 11:28:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[Price] [decimal](18, 0) NULL,
	[CreatedAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/17/2025 11:28:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[BirthDate] [datetime] NULL,
	[Address] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [UserId], [Username], [Password], [CreatedTime], [UpdatedTime], [DeletedTime], [IsActive], [Email]) VALUES (1, 1, N'admin', N'admin123', CAST(N'2025-04-16T17:20:45.800' AS DateTime), NULL, NULL, 1, N'admin@gmail.com')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [CreatedAt]) VALUES (5, N'Product A', N'desc x', CAST(5000 AS Decimal(18, 0)), CAST(N'2025-04-17T10:19:50.530' AS DateTime))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Phone], [Gender], [BirthDate], [Address], [CreatedTime]) VALUES (1, N'Admin', N'1', N'admin@gmail.com', N'123', 1, CAST(N'2000-04-16T10:19:50.313' AS DateTime), N'Jakarta', CAST(N'2025-04-16T17:20:45.800' AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
GO
USE [master]
GO
ALTER DATABASE [dbProduct] SET  READ_WRITE 
GO
