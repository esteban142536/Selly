USE [master]
GO
/****** Object:  Database [sully]    Script Date: 26/5/2021 03:01:22 p. m. ******/
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
/****** Object:  Table [dbo].[contactos]    Script Date: 26/5/2021 03:01:23 p. m. ******/
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
/****** Object:  Table [dbo].[estante]    Script Date: 26/5/2021 03:01:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estante](
	[codigoBodega] [int] IDENTITY(1,1) NOT NULL,
	[idTienda] [int] NOT NULL,
	[idProveedor] [int] NOT NULL,
 CONSTRAINT [PK_bodega] PRIMARY KEY CLUSTERED 
(
	[codigoBodega] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[factura]    Script Date: 26/5/2021 03:01:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factura](
	[codigoFactura] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[idTienda] [int] NOT NULL,
	[fecha] [varchar](150) NOT NULL,
	[totalPagado] [float] NOT NULL,
 CONSTRAINT [PK_factura] PRIMARY KEY CLUSTERED 
(
	[codigoFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 26/5/2021 03:01:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[nombre] [varchar](200) NOT NULL,
	[existencias] [int] NOT NULL,
	[categoria] [varchar](150) NOT NULL,
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[idBodega] [int] NOT NULL,
	[costo] [float] NOT NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productoFacturados]    Script Date: 26/5/2021 03:01:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productoFacturados](
	[idProducto] [int] NOT NULL,
	[codigoFactura] [int] NOT NULL,
	[precio] [float] NOT NULL,
	[existenciasCompradas] [int] NOT NULL,
	[categoria] [varchar](150) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productoProveedor]    Script Date: 26/5/2021 03:01:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productoProveedor](
	[idProducto] [int] NOT NULL,
	[codigoProveedor] [int] NOT NULL,
 CONSTRAINT [PK_productoProveedor] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[codigoProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 26/5/2021 03:01:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[nombreEmpresa] [varchar](150) NOT NULL,
	[direccion] [nchar](10) NOT NULL,
	[pais] [varchar](150) NOT NULL,
	[codigoProveedor] [int] NOT NULL,
 CONSTRAINT [PK_proveedor] PRIMARY KEY CLUSTERED 
(
	[codigoProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tienda]    Script Date: 26/5/2021 03:01:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tienda](
	[nombre] [varchar](150) NOT NULL,
	[direccion] [varchar](200) NOT NULL,
	[idPropietario] [int] NOT NULL,
	[idTienda] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_tienda] PRIMARY KEY CLUSTERED 
(
	[idTienda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 26/5/2021 03:01:23 p. m. ******/
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
	[esAdmin] [bit] NOT NULL,
	[esActivo] [bit] NOT NULL,
	[esEmpleado] [bit] NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([nombre], [apellidos], [email], [clave], [idUsuario], [esAdmin], [esActivo], [esEmpleado]) VALUES (N'admin', N'istrador', N'admin', N'123', 1, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
ALTER TABLE [dbo].[contactos]  WITH CHECK ADD  CONSTRAINT [FK_contactos_proveedor] FOREIGN KEY([proveedorAsociada])
REFERENCES [dbo].[proveedor] ([codigoProveedor])
GO
ALTER TABLE [dbo].[contactos] CHECK CONSTRAINT [FK_contactos_proveedor]
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
ALTER TABLE [dbo].[factura]  WITH CHECK ADD  CONSTRAINT [FK_factura_usuario] FOREIGN KEY([idCliente])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[factura] CHECK CONSTRAINT [FK_factura_usuario]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_bodega] FOREIGN KEY([idBodega])
REFERENCES [dbo].[estante] ([codigoBodega])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_bodega]
GO
ALTER TABLE [dbo].[productoFacturados]  WITH CHECK ADD  CONSTRAINT [FK_productoFacturados_factura] FOREIGN KEY([codigoFactura])
REFERENCES [dbo].[factura] ([codigoFactura])
GO
ALTER TABLE [dbo].[productoFacturados] CHECK CONSTRAINT [FK_productoFacturados_factura]
GO
ALTER TABLE [dbo].[productoFacturados]  WITH CHECK ADD  CONSTRAINT [FK_productoFacturados_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[productoFacturados] CHECK CONSTRAINT [FK_productoFacturados_producto]
GO
ALTER TABLE [dbo].[productoProveedor]  WITH CHECK ADD  CONSTRAINT [FK_productoProveedor_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[productoProveedor] CHECK CONSTRAINT [FK_productoProveedor_producto]
GO
ALTER TABLE [dbo].[productoProveedor]  WITH CHECK ADD  CONSTRAINT [FK_productoProveedor_proveedor] FOREIGN KEY([codigoProveedor])
REFERENCES [dbo].[proveedor] ([codigoProveedor])
GO
ALTER TABLE [dbo].[productoProveedor] CHECK CONSTRAINT [FK_productoProveedor_proveedor]
GO
ALTER TABLE [dbo].[tienda]  WITH CHECK ADD  CONSTRAINT [FK_tienda_usuario] FOREIGN KEY([idPropietario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[tienda] CHECK CONSTRAINT [FK_tienda_usuario]
GO
USE [master]
GO
ALTER DATABASE [sully] SET  READ_WRITE 
GO
