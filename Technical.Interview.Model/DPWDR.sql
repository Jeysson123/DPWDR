USE [master]
GO

/****** Object:  Database [DPWDR]    Script Date: 7/10/2024 10:28:44 AM ******/
CREATE DATABASE [DPWDR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DPWRD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DPWRD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DPWRD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DPWRD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DPWDR].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [DPWDR] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [DPWDR] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [DPWDR] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [DPWDR] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [DPWDR] SET ARITHABORT OFF 
GO

ALTER DATABASE [DPWDR] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [DPWDR] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [DPWDR] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [DPWDR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [DPWDR] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [DPWDR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [DPWDR] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [DPWDR] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [DPWDR] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [DPWDR] SET  DISABLE_BROKER 
GO

ALTER DATABASE [DPWDR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [DPWDR] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [DPWDR] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [DPWDR] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [DPWDR] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [DPWDR] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [DPWDR] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [DPWDR] SET RECOVERY FULL 
GO

ALTER DATABASE [DPWDR] SET  MULTI_USER 
GO

ALTER DATABASE [DPWDR] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [DPWDR] SET DB_CHAINING OFF 
GO

ALTER DATABASE [DPWDR] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [DPWDR] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [DPWDR] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [DPWDR] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [DPWDR] SET QUERY_STORE = OFF
GO

ALTER DATABASE [DPWDR] SET  READ_WRITE 
GO

