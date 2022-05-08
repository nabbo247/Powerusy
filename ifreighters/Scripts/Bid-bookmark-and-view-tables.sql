USE [Powerusydb]
GO
/****** Object:  Table [dbo].[tbl_bidding_bookmark]    Script Date: 08/05/2022 18:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_bidding_bookmark](
	[bidding_id] [int] NOT NULL,
	[bookmarked_by_id] [int] NOT NULL,
	[bid_owner_id] [int] NOT NULL,
 CONSTRAINT [PK_tbl_bidding_bookmark] PRIMARY KEY CLUSTERED 
(
	[bidding_id] ASC,
	[bookmarked_by_id] ASC,
	[bid_owner_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_bidding_view]    Script Date: 08/05/2022 18:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_bidding_view](
	[bidding_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[view_count] [int] NOT NULL,
 CONSTRAINT [PK_tbl_bidding_view] PRIMARY KEY CLUSTERED 
(
	[bidding_id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_bidding_bookmark] ([bidding_id], [bookmarked_by_id], [bid_owner_id]) VALUES (2, 1012, 1012)
GO
INSERT [dbo].[tbl_bidding_view] ([bidding_id], [user_id], [view_count]) VALUES (1, 11, 5)
GO
INSERT [dbo].[tbl_bidding_view] ([bidding_id], [user_id], [view_count]) VALUES (2, 1012, 12)
GO
INSERT [dbo].[tbl_bidding_view] ([bidding_id], [user_id], [view_count]) VALUES (3, 1012, 3)
GO
INSERT [dbo].[tbl_bidding_view] ([bidding_id], [user_id], [view_count]) VALUES (4, 1012, 1)
GO
ALTER TABLE [dbo].[tbl_bidding_view] ADD  CONSTRAINT [DF_tbl_bidding_view_view_count]  DEFAULT ((0)) FOR [view_count]
GO
