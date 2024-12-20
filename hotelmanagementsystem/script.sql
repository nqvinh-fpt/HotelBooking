USE [Assignment1234]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/11/2024 11:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 7/11/2024 11:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[RoomID] [int] NULL,
	[CustomerID] [int] NULL,
	[CheckInDate] [date] NULL,
	[CheckOutDate] [date] NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 7/11/2024 11:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Address] [varchar](max) NULL,
	[Password] [varchar](max) NULL,
	[Username] [varchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 7/11/2024 11:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Position] [varchar](100) NULL,
	[Department] [varchar](100) NULL,
	[Salary] [decimal](10, 2) NULL,
	[Password] [varchar](max) NULL,
	[Role] [int] NOT NULL,
	[Username] [varchar](50) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 7/11/2024 11:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[RoomID] [int] NOT NULL,
	[CustomerID] [int] NULL,
	[FeedbackDate] [date] NULL,
	[Rating] [int] NULL,
	[Comment] [varchar](max) NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 7/11/2024 11:47:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [varchar](20) NULL,
	[RoomType] [varchar](50) NULL,
	[Status] [varchar](20) NULL,
	[Price] [decimal](10, 2) NULL,
	[Amenities] [varchar](max) NULL,
	[Floor] [int] NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240619142337_InitialCreate', N'6.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240624071107_HotelDB_ver1.0', N'6.0.31')
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([BookingID], [RoomID], [CustomerID], [CheckInDate], [CheckOutDate], [EmployeeID]) VALUES (1, 1, 2, CAST(N'2024-08-08' AS Date), CAST(N'2024-09-12' AS Date), 1)
INSERT [dbo].[Bookings] ([BookingID], [RoomID], [CustomerID], [CheckInDate], [CheckOutDate], [EmployeeID]) VALUES (2, 2, 2, CAST(N'2024-07-07' AS Date), CAST(N'2024-08-07' AS Date), 2)
INSERT [dbo].[Bookings] ([BookingID], [RoomID], [CustomerID], [CheckInDate], [CheckOutDate], [EmployeeID]) VALUES (3, 1, NULL, CAST(N'2024-07-08' AS Date), CAST(N'2024-07-17' AS Date), NULL)
INSERT [dbo].[Bookings] ([BookingID], [RoomID], [CustomerID], [CheckInDate], [CheckOutDate], [EmployeeID]) VALUES (4, 6, NULL, CAST(N'2024-07-09' AS Date), CAST(N'2024-07-31' AS Date), NULL)
INSERT [dbo].[Bookings] ([BookingID], [RoomID], [CustomerID], [CheckInDate], [CheckOutDate], [EmployeeID]) VALUES (5, 9, NULL, CAST(N'2024-08-01' AS Date), CAST(N'2024-08-10' AS Date), NULL)
INSERT [dbo].[Bookings] ([BookingID], [RoomID], [CustomerID], [CheckInDate], [CheckOutDate], [EmployeeID]) VALUES (6, 5, NULL, CAST(N'2024-07-17' AS Date), CAST(N'2024-07-27' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [Password], [Username]) VALUES (1, N'John', N'Doe', N'john.doe@example.com', N'1234567890', N'123 Main St, City, Country', N'123', N'Jone')
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [Password], [Username]) VALUES (2, N'Jane', N'Smith', N'jane.smith@example.com', N'9876543210', N'456 Elm St, City, Country', NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [Password], [Username]) VALUES (3, N'Michael', N'Johnson', N'michael.johnson@example.com', N'5555555555', N'789 Oak St, City, Country', NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [Password], [Username]) VALUES (4, N'Emily', N'Brown', N'emily.brown@example.com', N'4444444444', N'321 Maple St, City, Country', NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [Password], [Username]) VALUES (5, N'David', N'Williams', N'david.williams@example.com', N'7777777777', N'555 Pine St, City, Country', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [PhoneNumber], [Position], [Department], [Salary], [Password], [Role], [Username]) VALUES (1, N'Alice1', N'Johnson', N'alice.johnson@example.com', N'1111111111', N'Manager', N'Management', CAST(50000.00 AS Decimal(10, 2)), N'password1234', 1, N'alice.johnson')
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [PhoneNumber], [Position], [Department], [Salary], [Password], [Role], [Username]) VALUES (2, N'Bob', N'Smith', N'bob.smith@example.com', N'2222222222', N'Receptionist', N'Front Desk', CAST(30000.00 AS Decimal(10, 2)), N'password123', 0, N'bob.smith')
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [PhoneNumber], [Position], [Department], [Salary], [Password], [Role], [Username]) VALUES (3, N'Carol', N'Williams', N'carol.williams@example.com', N'3333333333', N'Housekeeper', N'Housekeeping', CAST(25000.00 AS Decimal(10, 2)), N'password123', 1, N'carol.williams')
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [PhoneNumber], [Position], [Department], [Salary], [Password], [Role], [Username]) VALUES (4, N'Daniel', N'Brown', N'daniel.brown@example.com', N'4444444444', N'Chef', N'Kitchen', CAST(45000.00 AS Decimal(10, 2)), N'password123', 1, N'daniel.brown')
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [LastName], [Email], [PhoneNumber], [Position], [Department], [Salary], [Password], [Role], [Username]) VALUES (5, N'Eva', N'Jones', N'eva.jones@example.com', N'5555555555', N'Waiter', N'Restaurant', CAST(20000.00 AS Decimal(10, 2)), N'password123', 0, N'eva.jones')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedbacks] ON 

INSERT [dbo].[Feedbacks] ([FeedbackID], [RoomID], [CustomerID], [FeedbackDate], [Rating], [Comment]) VALUES (1, 1, 1, CAST(N'2024-03-25' AS Date), 5, N'Great experience, highly recommended!')
INSERT [dbo].[Feedbacks] ([FeedbackID], [RoomID], [CustomerID], [FeedbackDate], [Rating], [Comment]) VALUES (2, 2, 2, CAST(N'2024-08-07' AS Date), 4, NULL)
INSERT [dbo].[Feedbacks] ([FeedbackID], [RoomID], [CustomerID], [FeedbackDate], [Rating], [Comment]) VALUES (3, 3, 3, CAST(N'2024-04-15' AS Date), 3, N'Average experience, nothing special.')
INSERT [dbo].[Feedbacks] ([FeedbackID], [RoomID], [CustomerID], [FeedbackDate], [Rating], [Comment]) VALUES (4, 4, 4, CAST(N'2024-04-25' AS Date), 5, N'Excellent service, loved the amenities.')
INSERT [dbo].[Feedbacks] ([FeedbackID], [RoomID], [CustomerID], [FeedbackDate], [Rating], [Comment]) VALUES (5, 5, 5, CAST(N'2024-05-05' AS Date), 4, N'Overall good stay, but noisy neighbors.')
SET IDENTITY_INSERT [dbo].[Feedbacks] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (1, N'101', N'Standard', N'Available', CAST(100.00 AS Decimal(10, 2)), N'WiFi, TV, Bathroom', 1)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (2, N'102', N'Suite', N'Occupied', CAST(150.00 AS Decimal(10, 2)), N'WiFi, TV', 1)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (3, N'201', N'Standard', N'Available', CAST(100.00 AS Decimal(10, 2)), N'WiFi, TV, Bathroom', 2)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (4, N'202', N'Suite', N'Available', CAST(150.00 AS Decimal(10, 2)), N'WiFi, TV', 2)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (5, N'302', N'Standard', N'Occupied', CAST(100.00 AS Decimal(10, 2)), N'WiFi, TV, Bathroom', 3)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (6, N'103', N'Standard', N'Available', CAST(100.00 AS Decimal(10, 2)), N'WiFi, TV, Bathroom', 1)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (7, N'104', N'Suite', N'Occupied', CAST(150.00 AS Decimal(10, 2)), N'WiFi, TV', 1)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (8, N'203', N'Standard', N'Available', CAST(100.00 AS Decimal(10, 2)), N'WiFi, TV, Bathroom', 2)
INSERT [dbo].[Rooms] ([RoomID], [RoomNumber], [RoomType], [Status], [Price], [Amenities], [Floor]) VALUES (9, N'204', N'Suite', N'Available', CAST(150.00 AS Decimal(10, 2)), N'WiFi, TV', 2)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [Role]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_CustomerId] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_CustomerId]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_EmployeeId] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_EmployeeId]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_RoomID] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_RoomID]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_CustomerId] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_CustomerId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_RoomId] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_RoomId]
GO
