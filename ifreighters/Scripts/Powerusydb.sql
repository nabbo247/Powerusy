USE [master]
GO
/****** Object:  Database [Powerusydb]    Script Date: 07/05/2022 1:41:06 pm ******/
CREATE DATABASE [Powerusydb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Powerusydb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Powerusydb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Powerusydb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Powerusydb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Powerusydb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Powerusydb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Powerusydb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Powerusydb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Powerusydb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Powerusydb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Powerusydb] SET ARITHABORT OFF 
GO
ALTER DATABASE [Powerusydb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Powerusydb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Powerusydb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Powerusydb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Powerusydb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Powerusydb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Powerusydb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Powerusydb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Powerusydb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Powerusydb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Powerusydb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Powerusydb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Powerusydb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Powerusydb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Powerusydb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Powerusydb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Powerusydb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Powerusydb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Powerusydb] SET  MULTI_USER 
GO
ALTER DATABASE [Powerusydb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Powerusydb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Powerusydb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Powerusydb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Powerusydb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Powerusydb]
GO
/****** Object:  Table [dbo].[tbl_attachment]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_attachment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [nvarchar](200) NULL,
	[path] [nvarchar](500) NULL,
	[status] [int] NULL,
	[commnet] [nvarchar](50) NULL,
	[date] [datetime] NULL,
	[userid] [int] NULL,
 CONSTRAINT [PK_tbl_attachment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_auditlog]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auditlog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[operationperformed] [nvarchar](255) NULL,
	[ipaddress] [nvarchar](255) NULL,
	[pagevisited] [nvarchar](255) NULL,
	[dateaccessed] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_bidding]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_bidding](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsType] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[category] [nvarchar](50) NULL,
	[subcategory] [nvarchar](50) NULL,
	[service] [nvarchar](50) NULL,
	[Consignee] [nvarchar](50) NULL,
	[BookingNo] [nvarchar](50) NULL,
	[BLNumber] [nvarchar](50) NULL,
	[VesselName] [nvarchar](50) NULL,
	[PortLoading] [nvarchar](50) NULL,
	[PortDischarge] [nvarchar](50) NULL,
	[ShippingLines] [nvarchar](50) NULL,
	[EstBudget] [decimal](18, 2) NULL,
	[GoodDescription] [nvarchar](max) NULL,
	[UserID] [int] NULL,
	[Date] [datetime] NULL,
	[statusid] [int] NULL,
	[IconPath] [nvarchar](100) NULL,
 CONSTRAINT [PK_tbl_bidding] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_bidding_jobs]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_bidding_jobs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BidID] [int] NULL,
	[AgentID] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Date] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[DueDate] [datetime] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_tbl_bidding_jobs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_clearing]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_clearing](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[goodsid] [int] NULL,
	[finalinvoice] [nvarchar](255) NULL,
	[billoflanding] [nvarchar](255) NULL,
	[packinglist] [nvarchar](255) NULL,
	[combinedcertificate] [nvarchar](255) NULL,
	[soncap] [nvarchar](255) NULL,
	[approvedbyid] [nvarchar](255) NULL,
	[dateapproved] [datetimeoffset](7) NULL,
	[comment] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_counntries]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_counntries](
	[Country_Code] [int] IDENTITY(1,1) NOT NULL,
	[Brief_desc] [nvarchar](50) NULL,
	[CountryName] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_goodstype]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_goodstype](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[dateadded] [datetimeoffset](7) NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_importation]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_importation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shipperid] [int] NULL,
	[goodstypeid] [int] NULL,
	[subgoodtypeid] [nvarchar](255) NULL,
	[comment] [nvarchar](255) NULL,
	[servicetypeid] [nvarchar](255) NULL,
	[status] [int] NULL,
	[dateadded] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_importation_document]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_importation_document](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[importationid] [nvarchar](255) NULL,
	[documentname] [nvarchar](255) NULL,
	[documentpath] [nvarchar](255) NULL,
	[dateadded] [datetimeoffset](7) NULL,
	[statusid] [int] NULL,
	[approvedby] [nvarchar](255) NULL,
	[dateapproved] [nvarchar](255) NULL,
	[approvalcomment] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_kyc]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_kyc](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[registeredid] [int] NULL,
	[certificateofincorporation] [nvarchar](255) NULL,
	[memorandomofassociation] [nvarchar](255) NULL,
	[articlesofassociation] [nvarchar](255) NULL,
	[powerofattorneygranted] [nvarchar](255) NULL,
	[validbusinesslicense] [nvarchar](255) NULL,
	[auditedfinancialstatement] [nvarchar](255) NULL,
	[taxclearancecertificate] [nvarchar](255) NULL,
	[dateadded] [datetimeoffset](7) NULL,
	[statusid] [int] NULL,
	[approvedby] [int] NULL,
	[approvalcomment] [nvarchar](255) NULL,
	[dateapproved] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_registered]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_registered](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[companyname] [nvarchar](255) NULL,
	[serviceid] [int] NULL,
	[description] [nvarchar](255) NULL,
	[companylocation] [nvarchar](255) NULL,
	[companyaddress] [nvarchar](255) NULL,
	[companylogo] [nvarchar](255) NULL,
	[postaddress] [nvarchar](255) NULL,
	[workingdays] [nvarchar](255) NULL,
	[workinghours] [nvarchar](255) NULL,
	[bankname] [nvarchar](255) NULL,
	[accountnumber] [nvarchar](255) NULL,
	[dateadded] [datetimeoffset](7) NULL,
	[statusid] [int] NULL,
	[approvedby] [int] NULL,
	[dateapproved] [datetimeoffset](7) NULL,
	[approvalcomment] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_role]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](255) NULL,
	[name] [nvarchar](255) NULL,
	[datecreated] [datetimeoffset](7) NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_services]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_services](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_services] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_servicetype]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_servicetype](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[service] [nvarchar](255) NULL,
	[isdeleted] [bit] NULL,
	[dateadded] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_shipper]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_shipper](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[companyname] [nvarchar](255) NULL,
	[tin] [nvarchar](255) NULL,
	[tinpassword] [nvarchar](255) NULL,
	[location] [nvarchar](255) NULL,
	[phonenumber] [nvarchar](255) NULL,
	[statusid] [int] NULL,
	[approvedby] [int] NULL,
	[comment] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[workingdays] [nvarchar](255) NULL,
	[workinghours] [nvarchar](255) NULL,
	[bankname] [nvarchar](255) NULL,
	[accountnumber] [nvarchar](255) NULL,
	[dateadded] [datetime] NULL,
	[address] [nvarchar](500) NULL,
	[state] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
 CONSTRAINT [PK__tbl_ship__3213E83FEC4D1FDB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_status]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_status](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[is_deleted] [bit] NULL,
	[dateadded] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_subgoodstype]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_subgoodstype](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[goodstypeid] [int] NULL,
	[name] [nvarchar](255) NULL,
	[dateadded] [datetimeoffset](7) NULL,
	[statusid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_uploadDocType]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_uploadDocType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Docname] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_uploadDocType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_users]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](255) NULL,
	[username] [nvarchar](255) NULL,
	[lastname] [nvarchar](255) NULL,
	[middlename] [nvarchar](255) NULL,
	[phonenumber] [nvarchar](255) NULL,
	[dateadded] [datetimeoffset](7) NULL,
	[roleid] [int] NULL,
	[status] [bit] NULL,
	[email] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
	[authcode] [nvarchar](255) NULL,
	[Logo] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Shipper]    Script Date: 07/05/2022 1:41:07 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Shipper]
AS
SELECT u.id, u.firstname, u.username, u.lastname, u.middlename, u.phonenumber, u.dateadded, u.roleid, u.status, u.email, u.password, u.authcode, s.id AS ShipID, s.userid, s.companyname, s.tin, s.tinpassword, s.location, s.phonenumber AS ShipPhon, s.statusid, s.approvedby, s.comment, s.description, s.workingdays, s.workinghours, 
           s.bankname, s.accountnumber, s.dateadded AS ShipDate, s.address, s.state, s.city
FROM   dbo.tbl_users AS u LEFT OUTER JOIN
           dbo.tbl_shipper AS s ON s.userid = u.id
GO
SET IDENTITY_INSERT [dbo].[tbl_attachment] ON 

INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (0, N'Tax clearance', N'~//upload//System.Web.HttpPostedFileWrapper', 1, NULL, CAST(N'2022-04-09T00:46:18.277' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (1, N'Certificate Of Incorporation', N'~//upload//nabbo247_15064Certificate of Incorporation.PNG', 1, NULL, CAST(N'2022-04-09T01:06:30.187' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (2, N'MemorandumofAssociation', N'~//upload//nabbo247_90330Memorandum of Association.PNG', 1, NULL, CAST(N'2022-04-09T01:06:39.817' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (3, N'ArticlesofAssociation', N'~//upload//nabbo247_90330Articles of Association.PNG', 1, NULL, CAST(N'2022-04-09T01:06:41.040' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (4, N'PowerofAttorney', N'~//upload//nabbo247_90330Power of Attorney.PNG', 1, NULL, CAST(N'2022-04-09T01:06:42.220' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (5, N'Validbusinesslicense', N'~//upload//nabbo247_32629Valid business license.PNG', 1, NULL, CAST(N'2022-04-09T01:06:43.427' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (6, N'Auditedfinancial', N'~//upload//nabbo247_32629Audited financial statements.PNG', 1, NULL, CAST(N'2022-04-09T01:06:45.090' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (7, N'Taxclearance', N'~//upload//nabbo247_32629Tax clearance certificate.PNG', 1, NULL, CAST(N'2022-04-09T01:06:47.943' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (8, N'Certificate Of Incorporation', N'~//upload//nabbo247_11068Certificate of Incorporation.PNG', 1, NULL, CAST(N'2022-04-09T18:07:04.797' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (9, N'Memorandum of Association', N'~//upload//nabbo247_11068Memorandum of Association.PNG', 1, NULL, CAST(N'2022-04-09T18:07:04.797' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (10, N'Articles of Association', N'~//upload//nabbo247_86334Articles of Association.PNG', 1, NULL, CAST(N'2022-04-09T18:07:04.800' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (11, N'Power of Attorney', N'~//upload//nabbo247_86334Power of Attorney.PNG', 1, NULL, CAST(N'2022-04-09T18:07:04.800' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (12, N'Valid business license', N'~//upload//nabbo247_28633Valid business license.PNG', 1, NULL, CAST(N'2022-04-09T18:07:04.800' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (13, N'Audited financial', N'~//upload//nabbo247_28633Audited financial statements.PNG', 1, NULL, CAST(N'2022-04-09T18:07:04.800' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (14, N'Tax clearance', N'~//upload//nabbo247_28633Tax clearance certificate.PNG', 1, NULL, CAST(N'2022-04-09T18:07:04.800' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (15, N'Certificate Of Incorporation', N'nabbo247_84406Certificate of Incorporation.PNG', 1, NULL, CAST(N'2022-04-09T21:32:06.873' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (16, N'Memorandum of Association', N'nabbo247_69682Memorandum of Association.PNG', 1, NULL, CAST(N'2022-04-09T21:32:06.873' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (17, N'Articles of Association', N'nabbo247_69682Articles of Association.PNG', 1, NULL, CAST(N'2022-04-09T21:32:06.877' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (18, N'Power of Attorney', N'nabbo247_69682Power of Attorney.PNG', 1, NULL, CAST(N'2022-04-09T21:32:06.877' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (19, N'Valid business license', N'nabbo247_11981Valid business license.PNG', 1, NULL, CAST(N'2022-04-09T21:32:06.877' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (20, N'Audited financial', N'nabbo247_11981Audited financial statements.PNG', 1, NULL, CAST(N'2022-04-09T21:32:06.877' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (21, N'Tax clearance', N'nabbo247_44269Tax clearance certificate.PNG', 1, NULL, CAST(N'2022-04-09T21:32:06.877' AS DateTime), 1)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (22, N'Certificate Of Incorporation', N'nabbo247@gmail.com_62156Certificate of Incorporation.PNG', 1, NULL, CAST(N'2022-04-09T21:42:43.460' AS DateTime), 10)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (23, N'Memorandum of Association', N'nabbo247@gmail.com_47432Memorandum of Association.PNG', 1, NULL, CAST(N'2022-04-09T21:42:44.680' AS DateTime), 10)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (24, N'Articles of Association', N'nabbo247@gmail.com_79721Articles of Association.PNG', 1, NULL, CAST(N'2022-04-09T21:42:47.657' AS DateTime), 10)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (25, N'Power of Attorney', N'nabbo247@gmail.com_79721Power of Attorney.PNG', 1, NULL, CAST(N'2022-04-09T21:42:47.660' AS DateTime), 10)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (26, N'Valid business license', N'nabbo247@gmail.com_79721Valid business license.PNG', 1, NULL, CAST(N'2022-04-09T21:42:47.660' AS DateTime), 10)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (27, N'Audited financial', N'nabbo247@gmail.com_64997Audited financial statements.PNG', 1, NULL, CAST(N'2022-04-09T21:42:47.660' AS DateTime), 10)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (28, N'Tax clearance', N'nabbo247@gmail.com_64997Tax clearance certificate.PNG', 1, NULL, CAST(N'2022-04-09T21:42:47.660' AS DateTime), 10)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (29, N'Certificate Of Incorporation', N'nabbo247gmail.com_36856Certificate of Incorporation.PNG', 1, NULL, CAST(N'2022-04-09T22:13:22.620' AS DateTime), 11)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (30, N'Memorandum of Association', N'nabbo247gmail.com_22131Memorandum of Association.PNG', 1, NULL, CAST(N'2022-04-09T22:13:22.623' AS DateTime), 11)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (31, N'Articles of Association', N'nabbo247gmail.com_54420Articles of Association.PNG', 1, NULL, CAST(N'2022-04-09T22:13:22.623' AS DateTime), 11)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (32, N'Power of Attorney', N'nabbo247gmail.com_54420Power of Attorney.PNG', 1, NULL, CAST(N'2022-04-09T22:13:22.623' AS DateTime), 11)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (33, N'Valid business license', N'nabbo247gmail.com_54420Valid business license.PNG', 1, NULL, CAST(N'2022-04-09T22:13:22.623' AS DateTime), 11)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (34, N'Audited financial', N'nabbo247gmail.com_86709Audited financial statements.PNG', 1, NULL, CAST(N'2022-04-09T22:13:22.627' AS DateTime), 11)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (35, N'Tax clearance', N'nabbo247gmail.com_86709Tax clearance certificate.PNG', 1, NULL, CAST(N'2022-04-09T22:13:22.627' AS DateTime), 11)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (36, N'Certificate Of Incorporation', N'nabbo247gmail.com_88682ATK.jpg', 1, NULL, CAST(N'2022-04-23T23:04:03.997' AS DateTime), 1012)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (37, N'Memorandum of Association', NULL, 1, NULL, CAST(N'2022-04-23T23:04:04.000' AS DateTime), 1012)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (38, N'Articles of Association', NULL, 1, NULL, CAST(N'2022-04-23T23:04:04.000' AS DateTime), 1012)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (39, N'Power of Attorney', NULL, 1, NULL, CAST(N'2022-04-23T23:04:04.000' AS DateTime), 1012)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (40, N'Valid business license', NULL, 1, NULL, CAST(N'2022-04-23T23:04:04.000' AS DateTime), 1012)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (41, N'Audited financial', NULL, 1, NULL, CAST(N'2022-04-23T23:04:04.000' AS DateTime), 1012)
INSERT [dbo].[tbl_attachment] ([id], [fileName], [path], [status], [commnet], [date], [userid]) VALUES (42, N'Tax clearance', NULL, 1, NULL, CAST(N'2022-04-23T23:04:04.003' AS DateTime), 1012)
SET IDENTITY_INSERT [dbo].[tbl_attachment] OFF
SET IDENTITY_INSERT [dbo].[tbl_bidding] ON 

INSERT [dbo].[tbl_bidding] ([id], [GoodsType], [Name], [category], [subcategory], [service], [Consignee], [BookingNo], [BLNumber], [VesselName], [PortLoading], [PortDischarge], [ShippingLines], [EstBudget], [GoodDescription], [UserID], [Date], [statusid], [IconPath]) VALUES (1, NULL, NULL, NULL, NULL, NULL, N'test', NULL, N'1213231', N'Test Vessal', N'Malaysia', N'Lagos', NULL, CAST(0.00 AS Decimal(18, 2)), N'test description ', 11, NULL, NULL, NULL)
INSERT [dbo].[tbl_bidding] ([id], [GoodsType], [Name], [category], [subcategory], [service], [Consignee], [BookingNo], [BLNumber], [VesselName], [PortLoading], [PortDischarge], [ShippingLines], [EstBudget], [GoodDescription], [UserID], [Date], [statusid], [IconPath]) VALUES (2, NULL, NULL, N'Apapa', NULL, NULL, N'Efe Urughu Bemil Estate', N'SAL-537597', N'US00547344', N'Glovis Crystal (22CY01)', N'Baltimore', N'Lagos', NULL, CAST(1500000.00 AS Decimal(18, 2)), N'CHASSIS NUMBER(S) USED UNPACKED VEHICLE(S)
2T2HK31U48C088810 1 LEXUS RX 350 Registr. Year:2008
ITN: X20220207614435', 1012, NULL, NULL, NULL)
INSERT [dbo].[tbl_bidding] ([id], [GoodsType], [Name], [category], [subcategory], [service], [Consignee], [BookingNo], [BLNumber], [VesselName], [PortLoading], [PortDischarge], [ShippingLines], [EstBudget], [GoodDescription], [UserID], [Date], [statusid], [IconPath]) VALUES (3, N'Barge Rental', NULL, N'Apapa', NULL, NULL, N'Efe Urughu Bemil Estate', N'SAL-537597', N'US00547344', N'Glovis Crystal (22CY01)', N'Baltimore', N'Lagos', NULL, CAST(1500000.00 AS Decimal(18, 2)), N'CHASSIS NUMBER(S) USED UNPACKED VEHICLE(S)
2T2HK31U48C088810 1 LEXUS RX 350 Registr. Year:2008
ITN: X20220207614435', 1012, CAST(N'2022-04-23T22:51:45.517' AS DateTime), 1, NULL)
INSERT [dbo].[tbl_bidding] ([id], [GoodsType], [Name], [category], [subcategory], [service], [Consignee], [BookingNo], [BLNumber], [VesselName], [PortLoading], [PortDischarge], [ShippingLines], [EstBudget], [GoodDescription], [UserID], [Date], [statusid], [IconPath]) VALUES (4, N'Barge Rental', NULL, N'Apapa', NULL, NULL, N'Efe Urughu Bemil Estate', N'SAL-537597', N'US00547344', N'Glovis Crystal (22CY01)', N'Baltimore', N'Lagos', NULL, CAST(1500000.00 AS Decimal(18, 2)), N'CHASSIS NUMBER(S) USED UNPACKED VEHICLE(S)
2T2HK31U48C088810 1 LEXUS RX 350 Registr. Year:2008
ITN: X20220207614435', 1012, CAST(N'2022-04-23T22:53:36.593' AS DateTime), 1, NULL)
SET IDENTITY_INSERT [dbo].[tbl_bidding] OFF
SET IDENTITY_INSERT [dbo].[tbl_bidding_jobs] ON 

INSERT [dbo].[tbl_bidding_jobs] ([ID], [BidID], [AgentID], [Amount], [Date], [Comment], [DueDate], [status]) VALUES (1, 3, 1013, CAST(800000.00 AS Decimal(18, 2)), CAST(N'2022-05-07T00:22:17.747' AS DateTime), N'okadsfsdfs', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_bidding_jobs] OFF
SET IDENTITY_INSERT [dbo].[tbl_counntries] ON 

INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (1, N'NGN', N'Nigeria')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (2, N'AFG', N'Afghanistan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (3, N'ALB', N'Albania')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (4, N'DZA', N'Algeria')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (5, N'AND', N'Andorra')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (6, N'AGO', N'Angola')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (7, N'AIA', N'Anguilla')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (8, N'ATA', N'Antarctica')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (9, N'ATG', N'Antigua And Barbuda')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (10, N'ARG', N'Argentina')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (11, N'ARM', N'Armenia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (12, N'ABW', N'Aruba')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (13, N'AUS', N'Australia.')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (14, N'AUT', N'Austria')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (15, N'AZE', N'Azerbajian')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (16, N'BHS', N'Bahamas')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (17, N'BHR', N'Kingdom of Bahrain')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (18, N'BGD', N'Bangladesh')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (19, N'BRB', N'Barbados')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (20, N'BLR', N'Belarus')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (21, N'BEL', N'Belgium')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (22, N'BLZ', N'Belize')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (23, N'BEN', N'Benin')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (24, N'BMU', N'Bermuda')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (25, N'BTN', N'Bhutan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (26, N'BOL', N'Bolivia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (27, N'BIH', N'Bosnia And Herzegovina')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (28, N'BWA', N'Botswana')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (29, N'BVT', N'Bouvet Island')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (30, N'BRA', N'Brazil')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (31, N'IOT', N'British Indian Ocean Territories')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (32, N'BRN', N'Brunei Darussalam')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (33, N'BGR', N'Bulgaria')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (34, N'BFA', N'Burkina Faso')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (35, N'BDI', N'Burundi')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (36, N'KHM', N'Cambodia (Kampuchea)')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (37, N'CMR', N'Cameroon')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (38, N'CAN', N'Canada')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (39, N'CPV', N'Cape Verde')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (40, N'CYM', N'Cayman Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (41, N'CAF', N'Central African Republic')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (42, N'CHL', N'Chile')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (43, N'CHN', N'China')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (44, N'CXR', N'Christmas Island')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (45, N'CCK', N'Cocos (Keeling) Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (46, N'COL', N'Colombia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (47, N'COM', N'Comoros')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (48, N'COG', N'Congo')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (49, N'COD', N'Congo, Democratic Rep of')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (50, N'COK', N'Cook Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (51, N'CRI', N'Costa Rica')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (52, N'HRV', N'Croatia (Hrvatska)')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (53, N'CUB', N'Cuba')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (54, N'CYP', N'Cyprus')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (55, N'CZE', N'Czech Republic')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (56, N'DNK', N'Denmark')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (57, N'DJI', N'Djibouti')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (58, N'DMA', N'Dominica')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (59, N'DOM', N'Dominican Republic')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (60, N'ECU', N'Ecuador')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (61, N'EGY', N'Egypt')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (62, N'SLV', N'El Salvador')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (63, N'GNQ', N'Equatorial Guinea')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (64, N'ERI', N'Eriteria')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (65, N'EST', N'Estonia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (66, N'ETH', N'Ethiopia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (67, N'FLK', N'Falkland Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (68, N'FRO', N'Faroe Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (69, N'FJI', N'Fiji')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (70, N'FIN', N'Finland')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (71, N'FRA', N'France')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (72, N'GUF', N'French Guiana')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (73, N'GAB', N'Gabon')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (74, N'GMB', N'Gambia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (75, N'GEO', N'Georgia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (76, N'DEU', N'Germany')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (77, N'GHA', N'Ghana')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (78, N'GIB', N'Gibraltar')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (79, N'GRC', N'Greece')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (80, N'GRL', N'Greenland')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (81, N'GRD', N'Grenada')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (82, N'GLP', N'Guadeloupe')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (83, N'GUM', N'Guam')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (84, N'GTM', N'Guatemala')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (85, N'GIN', N'Guinea')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (86, N'GNB', N'Guinea - Bisseau')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (87, N'GUY', N'Guyana')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (88, N'HTI', N'Haiti')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (89, N'HMD', N'Heard & Mc Donald Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (90, N'HND', N'Honduras')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (91, N'HKG', N'Hong Kong')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (92, N'HUN', N'Hungary')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (93, N'ISL', N'Iceland')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (94, N'IND', N'India')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (95, N'IDN', N'Indonisia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (96, N'IRN', N'Islamic Republic Of Iran')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (97, N'IRQ', N'Iraq')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (98, N'IRL', N'Ireland')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (99, N'IMN', N'Isle of Man')
GO
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (100, N'ITA', N'Italy')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (101, N'JAM', N'Jamaica')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (102, N'JPN', N'Japan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (103, N'JEY', N'Jersey')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (104, N'JOR', N'Jordan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (105, N'KAZ', N'Kazakhstan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (106, N'SAR', N'KINGDOM OF SAUDI ARABIA')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (107, N'KIR', N'Kiribati')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (108, N'PRK', N'Korea,Democratic People''s')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (109, N'KOR', N'Korea,Republic Of')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (110, N'KWT', N'Kuwait')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (111, N'KGZ', N'Kyrgyzstan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (112, N'LAO', N'Laos People''s Democratic')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (113, N'LVA', N'Latvia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (114, N'LBN', N'Lebanon')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (115, N'LSO', N'Lesotho')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (116, N'LBR', N'Liberia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (117, N'LBY', N'Libyan Arab Jamahiria')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (118, N'LIE', N'Liechtenstein')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (119, N'LTU', N'Lithuania')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (120, N'LUX', N'Luxembourg')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (121, N'MAC', N'Macau')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (122, N'MKD', N'Macedonia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (123, N'MDG', N'Madagascar')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (124, N'MWI', N'Malawi')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (125, N'MYS', N'Malaysia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (126, N'MDV', N'Maldives')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (127, N'MLI', N'Mali')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (128, N'MLT', N'Malta')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (129, N'MHL', N'Marshall Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (130, N'MTQ', N'Martinique')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (131, N'MRT', N'Mauritania')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (132, N'MUS', N'Mauritus')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (133, N'MYT', N'Mayotte')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (134, N'MEX', N'Mexico')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (135, N'FSM', N'Micronesia,Federated State')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (136, N'MDA', N'Moldova')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (137, N'MCO', N'Monaco')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (138, N'MNG', N'Mongoloia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (139, N'MSR', N'Montserrat')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (140, N'MAR', N'Kingdom of Morocco')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (141, N'MOZ', N'Mozambique')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (142, N'MMR', N'Myanmar')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (143, N'NAM', N'Nambia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (144, N'NRU', N'Nauru')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (145, N'NPL', N'Nepal')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (146, N'NLD', N'Netherlands (Holland)')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (147, N'ANT', N'Netherlands Antille Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (148, N'NCL', N'New Caledonia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (149, N'NZL', N'New Zealand')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (150, N'NIC', N'Nicaragua')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (151, N'NER', N'Niger')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (152, N'KEN', N'Kenya')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (153, N'NIU', N'Niue')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (154, N'NFK', N'Norfolk Island')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (155, N'MNP', N'Northern Mariana Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (156, N'NOR', N'Norway')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (157, N'OMN', N'Sultanate of Oman')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (158, N'PAK', N'Pakistan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (159, N'PLW', N'Palau')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (160, N'PSE', N'Palestine')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (161, N'PAN', N'Panama')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (162, N'PNG', N'Papua New Guinea')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (163, N'PRY', N'Paraguway')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (164, N'PER', N'Peru')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (165, N'PHL', N'Philippine')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (166, N'PCN', N'Pitcairn')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (167, N'POL', N'Poland')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (168, N'PRT', N'Portugal')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (169, N'PRI', N'Puerto Rico')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (170, N'QAT', N'Qatar')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (171, N'REU', N'Reunion')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (172, N'ROU', N'Romania')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (173, N'RUS', N'Russian Federation')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (174, N'RWA', N'Rwanda')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (175, N'LCA', N'Saint Lucia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (176, N'WSM', N'Samoa')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (177, N'SMR', N'San Marino')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (178, N'STP', N'Sao Tome And Principe')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (179, N'SEN', N'Senegal')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (180, N'SCG', N'Serbia and Montenegro')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (181, N'SYC', N'Seychelles')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (182, N'SLE', N'Sierra Leone')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (183, N'SGP', N'Singapore')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (184, N'SVK', N'Slovakia(Slovak Republic)')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (185, N'SVN', N'Slovenia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (186, N'SLB', N'Solomon Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (187, N'SOM', N'Somalia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (188, N'ZAF', N'South Africa')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (189, N'SGS', N'South Georgia&South Sandw')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (190, N'ESP', N'Spain')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (191, N'LKA', N'Srilanka')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (192, N'SDN', N'Sudan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (193, N'SUR', N'Suriname')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (194, N'SWZ', N'Swaziland')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (195, N'SWE', N'Sweden')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (196, N'CHE', N'Switzerland')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (197, N'SYR', N'Syrian Arab Republic')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (198, N'TWN', N'Taiwan,Province Of China')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (199, N'TJK', N'Tajikistan')
GO
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (200, N'TZA', N'Tanzania,United Republic')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (201, N'THA', N'Thailand')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (202, N'TGO', N'Togo')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (203, N'TKL', N'Tokelau')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (204, N'TON', N'Tonga')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (205, N'TTO', N'Trinidad And Tobago')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (206, N'TUN', N'Tunisia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (207, N'TUR', N'Turkey')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (208, N'TKM', N'Turkmenistan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (209, N'TCA', N'Turks And Caicos Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (210, N'TUV', N'Tuvalu')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (211, N'UGA', N'Uganda')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (212, N'UKR', N'Ukraine')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (213, N'UAE', N'United Arab Emirates')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (214, N'GBR', N'United Kingdom')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (215, N'USA', N'United States')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (216, N'URY', N'Uruguay')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (217, N'UZB', N'Uzbekistan')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (218, N'VUT', N'Vanuatu')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (219, N'VAT', N'Vatican City State')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (220, N'VEN', N'Venezuela')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (221, N'VNM', N'Vietnam')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (222, N'VIR', N'Virgin Islands (U.S.)')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (223, N'WLF', N'Wallis And Futuna Islands')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (224, N'YEM', N'Yemen')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (225, N'ZMB', N'Zambia')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (226, N'ZWE', N'Zimbabwe')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (227, N'YUG', N'YUGOSLAVIA')
INSERT [dbo].[tbl_counntries] ([Country_Code], [Brief_desc], [CountryName]) VALUES (228, N'SHS', N'Southern Sudan')
SET IDENTITY_INSERT [dbo].[tbl_counntries] OFF
SET IDENTITY_INSERT [dbo].[tbl_importation_document] ON 

INSERT [dbo].[tbl_importation_document] ([id], [importationid], [documentname], [documentpath], [dateadded], [statusid], [approvedby], [dateapproved], [approvalcomment]) VALUES (1, NULL, N'Bill of Lading', N'System.Web.HttpPostedFileWrapper', CAST(N'2022-04-23T22:51:45.5118053+01:00' AS DateTimeOffset), 1012, NULL, NULL, NULL)
INSERT [dbo].[tbl_importation_document] ([id], [importationid], [documentname], [documentpath], [dateadded], [statusid], [approvedby], [dateapproved], [approvalcomment]) VALUES (2, NULL, N'Bill of Lading', N'System.Web.HttpPostedFileWrapper', CAST(N'2022-04-23T22:53:16.7853243+01:00' AS DateTimeOffset), 1012, NULL, NULL, NULL)
INSERT [dbo].[tbl_importation_document] ([id], [importationid], [documentname], [documentpath], [dateadded], [statusid], [approvedby], [dateapproved], [approvalcomment]) VALUES (3, NULL, N'Others Name', N'System.Web.HttpPostedFileWrapper', CAST(N'2022-04-23T22:53:34.7170879+01:00' AS DateTimeOffset), 1012, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_importation_document] OFF
SET IDENTITY_INSERT [dbo].[tbl_role] ON 

INSERT [dbo].[tbl_role] ([id], [code], [name], [datecreated], [status]) VALUES (1, N'Admin', N'Admin', NULL, 1)
INSERT [dbo].[tbl_role] ([id], [code], [name], [datecreated], [status]) VALUES (2, N'Clearing Agent', N'Clearing Agent', NULL, 1)
INSERT [dbo].[tbl_role] ([id], [code], [name], [datecreated], [status]) VALUES (3, N'Importer/Exporter', N'Importer/Exporter', NULL, 1)
INSERT [dbo].[tbl_role] ([id], [code], [name], [datecreated], [status]) VALUES (5, N'Transporter', N'Transporter', NULL, 1)
SET IDENTITY_INSERT [dbo].[tbl_role] OFF
SET IDENTITY_INSERT [dbo].[tbl_services] ON 

INSERT [dbo].[tbl_services] ([id], [Name]) VALUES (1, N'Documentation')
INSERT [dbo].[tbl_services] ([id], [Name]) VALUES (2, N'Clearing & Forwarding')
INSERT [dbo].[tbl_services] ([id], [Name]) VALUES (3, N'Trucking')
INSERT [dbo].[tbl_services] ([id], [Name]) VALUES (4, N'Barge Rental')
INSERT [dbo].[tbl_services] ([id], [Name]) VALUES (5, N'Ship Chartering & Brokerage')
INSERT [dbo].[tbl_services] ([id], [Name]) VALUES (6, N'Warehousing')
SET IDENTITY_INSERT [dbo].[tbl_services] OFF
SET IDENTITY_INSERT [dbo].[tbl_servicetype] ON 

INSERT [dbo].[tbl_servicetype] ([id], [service], [isdeleted], [dateadded]) VALUES (1, N'Documentation', 0, NULL)
INSERT [dbo].[tbl_servicetype] ([id], [service], [isdeleted], [dateadded]) VALUES (2, N'Clearing & trucking', 0, NULL)
SET IDENTITY_INSERT [dbo].[tbl_servicetype] OFF
SET IDENTITY_INSERT [dbo].[tbl_shipper] ON 

INSERT [dbo].[tbl_shipper] ([id], [userid], [companyname], [tin], [tinpassword], [location], [phonenumber], [statusid], [approvedby], [comment], [description], [workingdays], [workinghours], [bankname], [accountnumber], [dateadded], [address], [state], [city]) VALUES (4, 1, N'Kryptojinas', N'2712-12112', NULL, N'Nigeria', N'+2348023232', 1, NULL, NULL, N'salling of drugs', N'monday - friday', N'7-8pm', N'GTB', N'0027687662', CAST(N'2022-04-09T18:07:04.803' AS DateTime), N'no 55 london street dusteen kura', N'Niger', N'Minna')
INSERT [dbo].[tbl_shipper] ([id], [userid], [companyname], [tin], [tinpassword], [location], [phonenumber], [statusid], [approvedby], [comment], [description], [workingdays], [workinghours], [bankname], [accountnumber], [dateadded], [address], [state], [city]) VALUES (7, 11, N'Nabbo Ent', NULL, NULL, N'Nigeria', N'08028384956', 1, NULL, NULL, N'salling of drugs', N'monday - friday', N'7am - 4pm', N'GTB', N'0034858394', CAST(N'2022-04-09T22:13:22.627' AS DateTime), N'no 55 london street dusteen kura', N'FCT', N'AMAC')
INSERT [dbo].[tbl_shipper] ([id], [userid], [companyname], [tin], [tinpassword], [location], [phonenumber], [statusid], [approvedby], [comment], [description], [workingdays], [workinghours], [bankname], [accountnumber], [dateadded], [address], [state], [city]) VALUES (8, 1012, N'Kryptojinas', N'2712-12112', NULL, N'Afghanistan', N'08036083928', 1, NULL, NULL, N'salling of drugs', N'monday - friday', N'7am - 4pm', N'GTB', N'002768766', CAST(N'2022-04-23T23:04:04.003' AS DateTime), N'no 55 london street dusteen kura', N'Niger', N'Minna')
INSERT [dbo].[tbl_shipper] ([id], [userid], [companyname], [tin], [tinpassword], [location], [phonenumber], [statusid], [approvedby], [comment], [description], [workingdays], [workinghours], [bankname], [accountnumber], [dateadded], [address], [state], [city]) VALUES (9, 1012, N'krypto', N'2712-12112', NULL, N'Nigeria', N'+2348023232', 1, NULL, NULL, N'sdfsdfsdfs', N'monday - friday', N'7-8pm', N'GTB', N'0027687662', CAST(N'2022-04-28T23:08:53.933' AS DateTime), N'no 55 london street dusteen kura', N'niger', N'minna')
SET IDENTITY_INSERT [dbo].[tbl_shipper] OFF
SET IDENTITY_INSERT [dbo].[tbl_status] ON 

INSERT [dbo].[tbl_status] ([id], [name], [is_deleted], [dateadded]) VALUES (1, N'New Record', 0, NULL)
SET IDENTITY_INSERT [dbo].[tbl_status] OFF
SET IDENTITY_INSERT [dbo].[tbl_users] ON 

INSERT [dbo].[tbl_users] ([id], [firstname], [username], [lastname], [middlename], [phonenumber], [dateadded], [roleid], [status], [email], [password], [authcode], [Logo]) VALUES (1, N'OKES', N'OKEOWOS', N'ALAOS', N'YUSSUFS', N'08027768420s', CAST(N'2022-04-02T14:38:06.3050680+00:00' AS DateTimeOffset), 0, 1, N'ALAO.ADEBUSY@GMAIL.COM', N'123456s', N'304147', NULL)
INSERT [dbo].[tbl_users] ([id], [firstname], [username], [lastname], [middlename], [phonenumber], [dateadded], [roleid], [status], [email], [password], [authcode], [Logo]) VALUES (7, N'Adminn', N'Admin', N'amdin', NULL, NULL, NULL, 1, 1, N'admin@yahoo.com', N'UVHFtvxzB086DXGDEtW+gw==', NULL, NULL)
INSERT [dbo].[tbl_users] ([id], [firstname], [username], [lastname], [middlename], [phonenumber], [dateadded], [roleid], [status], [email], [password], [authcode], [Logo]) VALUES (1012, N'nabil', N'nabbo247@gmail.com', N'abubakar', N'labbo', N'08036083928', NULL, 3, NULL, N'nabbo247@gmail.com', N'UVHFtvxzB086DXGDEtW+gw==', NULL, NULL)
INSERT [dbo].[tbl_users] ([id], [firstname], [username], [lastname], [middlename], [phonenumber], [dateadded], [roleid], [status], [email], [password], [authcode], [Logo]) VALUES (1013, N'Nabil', N'nabbo247@yahoo.com', N'Labbo', N'Abubakar', N'0803748573', NULL, 2, NULL, N'nabbo247@yahoo.com', N'UVHFtvxzB086DXGDEtW+gw==', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_users] OFF
ALTER TABLE [dbo].[tbl_shipper] ADD  CONSTRAINT [DF_tbl_shipper_dateadded]  DEFAULT (getdate()) FOR [dateadded]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "u"
            Begin Extent = 
               Top = 10
               Left = 67
               Bottom = 240
               Right = 315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 10
               Left = 382
               Bottom = 240
               Right = 638
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Shipper'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Shipper'
GO
USE [master]
GO
ALTER DATABASE [Powerusydb] SET  READ_WRITE 
GO
