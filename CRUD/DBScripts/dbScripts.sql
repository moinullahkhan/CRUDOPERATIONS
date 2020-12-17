USE [BlogDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 17-12-2020 11:31:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [varchar](255) NULL,
	[SLUG] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 17-12-2020 11:31:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[POST_ID] [int] IDENTITY(1,1) NOT NULL,
	[TITLE] [varchar](2000) NULL,
	[DESCRIPTION] [varchar](max) NULL,
	[CATEGORY_ID] [int] NULL,
	[CREATED_DATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[POST_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (1, N'CSHARP', N'csharp')
INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (2, N'VISUAL STUDIO', N'visualstudio')
INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (3, N'ASP.NET CORE', N'aspnetcore')
INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (4, N'SQL SERVER', N'sqlserver')
INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (5, N'CSHARP', N'CSHARP')
INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (6, N'VISUAL STUDIO', N'visual studio')
INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (7, N'ASP.NET CORE', N'aspnetcore')
INSERT [dbo].[Category] ([ID], [NAME], [SLUG]) VALUES (8, N'SQL SERVER', N'sql server')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([POST_ID], [TITLE], [DESCRIPTION], [CATEGORY_ID], [CREATED_DATE]) VALUES (1, N'Test Title 1', N'Test Description 1', 2, CAST(N'2020-12-17T11:29:11.133' AS DateTime))
INSERT [dbo].[Post] ([POST_ID], [TITLE], [DESCRIPTION], [CATEGORY_ID], [CREATED_DATE]) VALUES (2, N'Test Title 2', N'Test Description 2', 3, CAST(N'2020-12-17T11:29:11.147' AS DateTime))
SET IDENTITY_INSERT [dbo].[Post] OFF
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD FOREIGN KEY([CATEGORY_ID])
REFERENCES [dbo].[Category] ([ID])
GO
