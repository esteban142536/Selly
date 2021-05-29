USE [master]
GO
/****** Object:  Database [sully]    Script Date: 29/5/2021 05:03:33 p. m. ******/
CREATE DATABASE [sully]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sully', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sully.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'sully_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sully_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [sully] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sully].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sully] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sully] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sully] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sully] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sully] SET ARITHABORT OFF 
GO
ALTER DATABASE [sully] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [sully] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sully] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sully] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sully] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sully] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sully] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sully] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sully] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sully] SET  DISABLE_BROKER 
GO
ALTER DATABASE [sully] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sully] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sully] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sully] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sully] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sully] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sully] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sully] SET RECOVERY FULL 
GO
ALTER DATABASE [sully] SET  MULTI_USER 
GO
ALTER DATABASE [sully] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sully] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sully] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sully] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [sully] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'sully', N'ON'
GO
ALTER DATABASE [sully] SET QUERY_STORE = OFF
GO
USE [sully]
GO
/****** Object:  Table [dbo].[contactos]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contactos](
	[nombre] [varchar](150) NOT NULL,
	[numero] [int] NOT NULL,
	[correo] [varchar](150) NOT NULL,
	[cedula] [varchar](50) NOT NULL,
	[proveedorAsociada] [int] NOT NULL,
 CONSTRAINT [PK_contactos] PRIMARY KEY CLUSTERED 
(
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalleFactura]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalleFactura](
	[idProducto] [int] NOT NULL,
	[idFactura] [int] NOT NULL,
	[precio] [float] NOT NULL,
	[cantidadComprada] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estante]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idTienda] [int] NOT NULL,
 CONSTRAINT [PK_bodega] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[factura]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factura](
	[idFactura] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idTienda] [int] NOT NULL,
	[fecha] [varchar](150) NOT NULL,
	[totalPagado] [float] NOT NULL,
	[idTipoSalida] [int] NOT NULL,
	[comentario] [varchar](200) NOT NULL,
 CONSTRAINT [PK_factura] PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ingreso]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingreso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idProveedor] [int] NOT NULL,
	[idEstante] [int] NOT NULL,
	[idTipoIngreso] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[cantidadIngreso] [int] NOT NULL,
	[comentario] [varchar](300) NOT NULL,
 CONSTRAINT [PK_ingreso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[nombre] [varchar](200) NOT NULL,
	[existencias] [int] NOT NULL,
	[categoria] [varchar](150) NOT NULL,
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[idEstante] [int] NOT NULL,
	[costo] [float] NOT NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productoProveedor]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productoProveedor](
	[idProducto] [int] NOT NULL,
	[idProveedor] [int] NOT NULL,
 CONSTRAINT [PK_productoProveedor] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[nombreEmpresa] [varchar](150) NOT NULL,
	[direccion] [nchar](10) NOT NULL,
	[idProveedor] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_proveedor] PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tienda]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tienda](
	[nombre] [varchar](150) NOT NULL,
	[direccion] [varchar](200) NOT NULL,
	[idTienda] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_tienda] PRIMARY KEY CLUSTERED 
(
	[idTienda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipoIngreso]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoIngreso](
	[id] [int] NOT NULL,
	[tipoIngreso] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tipoIngreso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipoSalida]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoSalida](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoSalida] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tipoSalida] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipoUsuario]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoUsuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[permisoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_tipoUsuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 29/5/2021 05:03:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[nombre] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[email] [varchar](150) NOT NULL,
	[clave] [varchar](150) NOT NULL,
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[esActivo] [bit] NOT NULL,
	[idTipoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tipoUsuario] ON 

INSERT [dbo].[tipoUsuario] ([id], [permisoUsuario]) VALUES (2, 1)
INSERT [dbo].[tipoUsuario] ([id], [permisoUsuario]) VALUES (3, 1)
INSERT [dbo].[tipoUsuario] ([id], [permisoUsuario]) VALUES (4, 1)
SET IDENTITY_INSERT [dbo].[tipoUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([nombre], [apellidos], [email], [clave], [idUsuario], [esActivo], [idTipoUsuario]) VALUES (N'admin', N'istrador', N'admin', N'123', 6, 1, 4)
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
ALTER TABLE [dbo].[contactos]  WITH CHECK ADD  CONSTRAINT [FK_contactos_proveedor] FOREIGN KEY([proveedorAsociada])
REFERENCES [dbo].[proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[contactos] CHECK CONSTRAINT [FK_contactos_proveedor]
GO
ALTER TABLE [dbo].[detalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_productoFacturados_factura] FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([idFactura])
GO
ALTER TABLE [dbo].[detalleFactura] CHECK CONSTRAINT [FK_productoFacturados_factura]
GO
ALTER TABLE [dbo].[detalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_productoFacturados_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[detalleFactura] CHECK CONSTRAINT [FK_productoFacturados_producto]
GO
ALTER TABLE [dbo].[estante]  WITH CHECK ADD  CONSTRAINT [FK_bodega_tienda] FOREIGN KEY([idTienda])
REFERENCES [dbo].[tienda] ([idTienda])
GO
ALTER TABLE [dbo].[estante] CHECK CONSTRAINT [FK_bodega_tienda]
GO
ALTER TABLE [dbo].[factura]  WITH CHECK ADD  CONSTRAINT [FK_factura_tienda] FOREIGN KEY([idTienda])
REFERENCES [dbo].[tienda] ([idTienda])
GO
ALTER TABLE [dbo].[factura] CHECK CONSTRAINT [FK_factura_tienda]
GO
ALTER TABLE [dbo].[factura]  WITH CHECK ADD  CONSTRAINT [FK_factura_tipoSalida] FOREIGN KEY([idTipoSalida])
REFERENCES [dbo].[tipoSalida] ([id])
GO
ALTER TABLE [dbo].[factura] CHECK CONSTRAINT [FK_factura_tipoSalida]
GO
ALTER TABLE [dbo].[factura]  WITH CHECK ADD  CONSTRAINT [FK_factura_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[factura] CHECK CONSTRAINT [FK_factura_usuario]
GO
ALTER TABLE [dbo].[ingreso]  WITH CHECK ADD  CONSTRAINT [FK_ingreso_estante] FOREIGN KEY([idEstante])
REFERENCES [dbo].[estante] ([id])
GO
ALTER TABLE [dbo].[ingreso] CHECK CONSTRAINT [FK_ingreso_estante]
GO
ALTER TABLE [dbo].[ingreso]  WITH CHECK ADD  CONSTRAINT [FK_ingreso_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[ingreso] CHECK CONSTRAINT [FK_ingreso_producto]
GO
ALTER TABLE [dbo].[ingreso]  WITH CHECK ADD  CONSTRAINT [FK_ingreso_proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[ingreso] CHECK CONSTRAINT [FK_ingreso_proveedor]
GO
ALTER TABLE [dbo].[ingreso]  WITH CHECK ADD  CONSTRAINT [FK_ingreso_tipoIngreso] FOREIGN KEY([idTipoIngreso])
REFERENCES [dbo].[tipoIngreso] ([id])
GO
ALTER TABLE [dbo].[ingreso] CHECK CONSTRAINT [FK_ingreso_tipoIngreso]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_bodega] FOREIGN KEY([idEstante])
REFERENCES [dbo].[estante] ([id])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_bodega]
GO
ALTER TABLE [dbo].[productoProveedor]  WITH CHECK ADD  CONSTRAINT [FK_productoProveedor_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[productoProveedor] CHECK CONSTRAINT [FK_productoProveedor_producto]
GO
ALTER TABLE [dbo].[productoProveedor]  WITH CHECK ADD  CONSTRAINT [FK_productoProveedor_proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[productoProveedor] CHECK CONSTRAINT [FK_productoProveedor_proveedor]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_tipoUsuario] FOREIGN KEY([idTipoUsuario])
REFERENCES [dbo].[tipoUsuario] ([id])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_tipoUsuario]
GO
USE [master]
GO
ALTER DATABASE [sully] SET  READ_WRITE 
GO
