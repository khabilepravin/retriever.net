
/****** Object:  Table [dbo].[Test]    Script Date: 01/27/2016 07:09:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [varchar](500) NULL,
	[Details] [text] NOT NULL,
	[Picture] [varbinary](max) NULL,
	[RecordedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_Test_Update]    Script Date: 01/27/2016 07:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Test_Update] 
    @Id uniqueidentifier,
    @Name nvarchar(200),
    @Description varchar(500) = NULL,
    @Details text,
    @Picture varbinary(MAX) = NULL,
    @RecordedDate datetime = NULL,
    @IsActive bit
AS 
	 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Test]
	SET    [Id] = @Id, [Name] = @Name, [Description] = @Description, [Details] = @Details, [Picture] = @Picture, [RecordedDate] = @RecordedDate, [IsActive] = @IsActive
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name], [Description], [Details], [Picture], [RecordedDate], [IsActive]
	FROM   [dbo].[Test]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_Test_Select]    Script Date: 01/27/2016 07:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Test_Select] 
    @Id uniqueidentifier = null
AS 
	 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [Name], [Description], [Details], [Picture], [RecordedDate], [IsActive] 
	FROM   [dbo].[Test] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_Test_Insert]    Script Date: 01/27/2016 07:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Test_Insert] 
    @Id uniqueidentifier,
    @Name nvarchar(200),
    @Description varchar(500) = NULL,
    @Details text,
    @Picture varbinary(MAX) = NULL,
    @RecordedDate datetime = NULL,
    @IsActive bit
AS 
	 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Test] ([Id], [Name], [Description], [Details], [Picture], [RecordedDate], [IsActive])
	SELECT @Id, @Name, @Description, @Details, @Picture, @RecordedDate, @IsActive
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name], [Description], [Details], [Picture], [RecordedDate], [IsActive]
	FROM   [dbo].[Test]
	WHERE  [Id] = @Id
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_Test_Delete]    Script Date: 01/27/2016 07:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Test_Delete] 
    @Id uniqueidentifier
AS 
	 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Test]
	WHERE  [Id] = @Id

	COMMIT
GO
