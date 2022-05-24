/****** Object:  Table [dbo].[IOHeader]    Script Date: 3/15/2022 8:18:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IOHeader](
	[TransType] [int] NULL,
	[Owner] [int] NULL,
	[Fix] [int] NULL,
	[Code2] [int] NULL,
	[Date1] [int] NULL,
	[PageNo] [varchar](50) NULL,
	[StockMan] [int] NULL,
	[Name] [int] NULL,
	[FromToType] [int] NULL,
	[From1] [int] NULL,
	[To1] [int] NULL,
	[ExitNo] [varchar](255) NULL,
	[PermisionDate] [int] NULL,
	[PermisionNo] [varchar](255) NULL,
	[BarnamehNo] [varchar](255) NULL,
	[BarnamehDate] [int] NULL,
	[MachinNo] [varchar](255) NULL,
	[DriverName] [varchar](255) NULL,
	[DriverCost] [float] NULL,
	[Note] [text] NULL,
	[id_export] [datetime] NULL,
	[code] [int] IDENTITY(50509,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IOHeader]    Script Date: 3/15/2022 8:18:07 AM ******/
CREATE NONCLUSTERED INDEX [indexTransType] ON [dbo].[IOHeader]
(
	[TransType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


