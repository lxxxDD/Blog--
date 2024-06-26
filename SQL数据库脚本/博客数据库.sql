USE [master]
GO
/****** Object:  Database [DreamBlog]    Script Date: 2024/3/18 12:10:59 ******/
CREATE DATABASE [DreamBlog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'reamBlog', FILENAME = N'D:\SQL之我的兄弟叫顺溜\数据库\reamBlog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'reamBlog_log', FILENAME = N'D:\SQL之我的兄弟叫顺溜\数据库\reamBlog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DreamBlog] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DreamBlog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DreamBlog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DreamBlog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DreamBlog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DreamBlog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DreamBlog] SET ARITHABORT OFF 
GO
ALTER DATABASE [DreamBlog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DreamBlog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DreamBlog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DreamBlog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DreamBlog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DreamBlog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DreamBlog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DreamBlog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DreamBlog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DreamBlog] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DreamBlog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DreamBlog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DreamBlog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DreamBlog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DreamBlog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DreamBlog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DreamBlog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DreamBlog] SET RECOVERY FULL 
GO
ALTER DATABASE [DreamBlog] SET  MULTI_USER 
GO
ALTER DATABASE [DreamBlog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DreamBlog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DreamBlog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DreamBlog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DreamBlog] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DreamBlog] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DreamBlog', N'ON'
GO
ALTER DATABASE [DreamBlog] SET QUERY_STORE = ON
GO
ALTER DATABASE [DreamBlog] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DreamBlog]
GO
/****** Object:  Table [dbo].[BlogPost]    Script Date: 2024/3/18 12:10:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPost](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NULL,
	[Tags] [text] NULL,
	[CategoryId] [int] NULL,
	[Content] [text] NULL,
	[UserId] [int] NULL,
	[Likes] [int] NULL,
	[Views] [int] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__BlogPost__AA126018B426CBF2] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2024/3/18 12:10:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2024/3/18 12:10:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[PostId] [int] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__Comment__C3B4DFCA24242C83] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 2024/3/18 12:10:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[TagId] [int] NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2024/3/18 12:10:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1001) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ImgUrl] [text] NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogPost]  WITH CHECK ADD  CONSTRAINT [FK__BlogPost__UserId__398D8EEE] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BlogPost] CHECK CONSTRAINT [FK__BlogPost__UserId__398D8EEE]
GO
ALTER TABLE [dbo].[BlogPost]  WITH CHECK ADD  CONSTRAINT [FK_BlogPost_CID] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[BlogPost] CHECK CONSTRAINT [FK_BlogPost_CID]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK__Comment__PostId__412EB0B6] FOREIGN KEY([PostId])
REFERENCES [dbo].[BlogPost] ([PostId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK__Comment__PostId__412EB0B6]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK__Comment__UserId__403A8C7D] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK__Comment__UserId__403A8C7D]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BlogPost', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BlogPost', @level2type=N'COLUMN',@level2name=N'CategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BlogPost', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外键用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BlogPost', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点赞数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BlogPost', @level2type=N'COLUMN',@level2name=N'Likes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'浏览量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BlogPost', @level2type=N'COLUMN',@level2name=N'Views'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章发布时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BlogPost', @level2type=N'COLUMN',@level2name=N'CreatedAt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论文章ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'PostId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'CreatedAt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'CreatedAt'
GO
USE [master]
GO
ALTER DATABASE [DreamBlog] SET  READ_WRITE 
GO
