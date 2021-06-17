USE [master]
GO
/****** Object:  Database [selly]    Script Date: 16/6/2021 09:22:39 p. m. ******/
CREATE DATABASE [selly]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'selly', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\selly.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'selly_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\selly_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [selly] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [selly].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [selly] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [selly] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [selly] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [selly] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [selly] SET ARITHABORT OFF 
GO
ALTER DATABASE [selly] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [selly] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [selly] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [selly] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [selly] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [selly] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [selly] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [selly] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [selly] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [selly] SET  DISABLE_BROKER 
GO
ALTER DATABASE [selly] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [selly] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [selly] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [selly] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [selly] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [selly] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [selly] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [selly] SET RECOVERY FULL 
GO
ALTER DATABASE [selly] SET  MULTI_USER 
GO
ALTER DATABASE [selly] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [selly] SET DB_CHAINING OFF 
GO
ALTER DATABASE [selly] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [selly] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [selly] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'selly', N'ON'
GO
ALTER DATABASE [selly] SET QUERY_STORE = OFF
GO
USE [selly]
GO
/****** Object:  Table [dbo].[contactos]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contactos](
	[nombre] [varchar](150) NOT NULL,
	[numero] [int] NOT NULL,
	[correo] [varchar](150) NOT NULL,
	[idProveedor] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalleFactura]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalleFactura](
	[idProducto] [int] NOT NULL,
	[idInventario] [int] NOT NULL,
	[precio] [float] NOT NULL,
	[cantidadComprada] [int] NOT NULL,
	[idEstante] [int] NULL,
	[idProveedor] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estante]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](150) NOT NULL,
 CONSTRAINT [PK_bodega] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[inventario]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inventario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idTienda] [int] NOT NULL,
	[fecha] [varchar](150) NOT NULL,
	[totalPagado] [float] NULL,
	[comentario] [varchar](200) NULL,
	[iva] [float] NULL,
	[idTipoMovimiento] [int] NOT NULL,
 CONSTRAINT [PK_factura] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pais]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pais](
	[id] [int] NOT NULL,
	[nombre] [varchar](150) NOT NULL,
 CONSTRAINT [PK_pais] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[nombre] [varchar](200) NOT NULL,
	[totalStock] [int] NOT NULL,
	[cantMaxima] [int] NOT NULL,
	[cantMinima] [int] NOT NULL,
	[idCategoria] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[costoUnitario] [float] NOT NULL,
	[descripcion] [varchar](300) NOT NULL,
	[imagen] [varbinary](max) NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productoEstante]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productoEstante](
	[idProducto] [int] NOT NULL,
	[cantidad] [int] NULL,
	[idEstante] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[nombreEmpresa] [varchar](150) NOT NULL,
	[direccion] [varchar](150) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idPais] [int] NOT NULL,
 CONSTRAINT [PK_proveedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedorProducto]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedorProducto](
	[idProveedor] [int] NOT NULL,
	[idProducto] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tienda]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tienda](
	[nombre] [varchar](150) NOT NULL,
	[direccion] [varchar](200) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_tienda] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoCategoria]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCategoria](
	[id] [int] NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TipoCategoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoMovimiento]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoMovimiento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoSalida] [varchar](50) NOT NULL,
	[tipoEntrada] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoMovimiento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipoUsuario]    Script Date: 16/6/2021 09:22:40 p. m. ******/
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
/****** Object:  Table [dbo].[usuario]    Script Date: 16/6/2021 09:22:40 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[nombre] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[email] [varchar](150) NOT NULL,
	[clave] [varchar](max) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[esActivo] [bit] NOT NULL,
	[idTipoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[contactos] ([nombre], [numero], [correo], [idProveedor]) VALUES (N'Leda Porras', 72094882, N'LPorras@demasa.com', 1005)
INSERT [dbo].[contactos] ([nombre], [numero], [correo], [idProveedor]) VALUES (N'Diego Smith', 54865214, N'dswallmark@gmail.com', 1006)
INSERT [dbo].[contactos] ([nombre], [numero], [correo], [idProveedor]) VALUES (N'Daniel Rivera', 547821450, N'Danporras@hotmail.com', 4)
INSERT [dbo].[contactos] ([nombre], [numero], [correo], [idProveedor]) VALUES (N'Alex Alvarado', 24015748, N'alexAlva@gmail.com', 6)
INSERT [dbo].[contactos] ([nombre], [numero], [correo], [idProveedor]) VALUES (N'Guiselle Rodriguez', 24018695, N'gRodriguez@demasa.com', 1005)
GO
SET IDENTITY_INSERT [dbo].[estante] ON 

INSERT [dbo].[estante] ([id], [nombre]) VALUES (1, N'ASW-01')
INSERT [dbo].[estante] ([id], [nombre]) VALUES (2, N'AAA-01')
INSERT [dbo].[estante] ([id], [nombre]) VALUES (3, N'AAB-05')
SET IDENTITY_INSERT [dbo].[estante] OFF
GO
SET IDENTITY_INSERT [dbo].[inventario] ON 

INSERT [dbo].[inventario] ([id], [idUsuario], [idTienda], [fecha], [totalPagado], [comentario], [iva], [idTipoMovimiento]) VALUES (1, 2, 1, N'10-6-2021', 32000, N'El cliente se comporto bien, a pesar que tuvo que esperar mas', 13, 1)
INSERT [dbo].[inventario] ([id], [idUsuario], [idTienda], [fecha], [totalPagado], [comentario], [iva], [idTipoMovimiento]) VALUES (2, 2, 1, N'5-1-2021', 12000, N'El cliente devolvio una liqueadora al dia siguiente de comprarla', 0, 2)
INSERT [dbo].[inventario] ([id], [idUsuario], [idTienda], [fecha], [totalPagado], [comentario], [iva], [idTipoMovimiento]) VALUES (3, 2, 2, N'4-8-2019', 3200, N'Se compro unos productos al cliente y a cambio se le dio una cafetera', 13, 3)
INSERT [dbo].[inventario] ([id], [idUsuario], [idTienda], [fecha], [totalPagado], [comentario], [iva], [idTipoMovimiento]) VALUES (5, 2, 3, N'5-4-2020', 2000, N'El cliente se comporto mal con el personal, se procedio a la compra y agregado a la lista negra de mo dejar entrar ', 13, 4)
SET IDENTITY_INSERT [dbo].[inventario] OFF
GO
INSERT [dbo].[pais] ([id], [nombre]) VALUES (1, N'Costa rica')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (2, N'India')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (7, N'Rusia')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (31, N'Países Bajos')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (33, N'Francia')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (39, N'Italia')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (44, N'Reino Unido')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (49, N'Alemania')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (81, N'Japón')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (82, N'Corea del sur')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (86, N'China')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (506, N'Costa Rica')
INSERT [dbo].[pais] ([id], [nombre]) VALUES (507, N'Panamá')
GO
SET IDENTITY_INSERT [dbo].[producto] ON 

INSERT [dbo].[producto] ([nombre], [totalStock], [cantMaxima], [cantMinima], [idCategoria], [id], [costoUnitario], [descripcion], [imagen]) VALUES (N'Silla ortopedica', 70, 85, 1, 1, 2, 32000, N'Silla ortopedica para evitar arcos de espalda', NULL)
INSERT [dbo].[producto] ([nombre], [totalStock], [cantMaxima], [cantMinima], [idCategoria], [id], [costoUnitario], [descripcion], [imagen]) VALUES (N'Pistola de agua Nerf', 21, 40, 2, 2, 3, 12000, N'Pistola que dispara agua marca nerf', NULL)
INSERT [dbo].[producto] ([nombre], [totalStock], [cantMaxima], [cantMinima], [idCategoria], [id], [costoUnitario], [descripcion], [imagen]) VALUES (N'Cartuchera', 24, 55, 0, 3, 5, 1000, N'Cartuchera de plastico para el inicio escolar', NULL)
INSERT [dbo].[producto] ([nombre], [totalStock], [cantMaxima], [cantMinima], [idCategoria], [id], [costoUnitario], [descripcion], [imagen]) VALUES (N'Kit de inicio de clases', 5, 10, 1, 3, 6, 4000, N'Kit para el inicio de clases, incluye 3 libros, cartuchera, lapizes y lapizeros', NULL)
SET IDENTITY_INSERT [dbo].[producto] OFF
GO
INSERT [dbo].[productoEstante] ([idProducto], [cantidad], [idEstante]) VALUES (2, 5, 1)
INSERT [dbo].[productoEstante] ([idProducto], [cantidad], [idEstante]) VALUES (3, 2, 2)
INSERT [dbo].[productoEstante] ([idProducto], [cantidad], [idEstante]) VALUES (5, 40, 1)
INSERT [dbo].[productoEstante] ([idProducto], [cantidad], [idEstante]) VALUES (6, 20, 3)
GO
SET IDENTITY_INSERT [dbo].[proveedor] ON 

INSERT [dbo].[proveedor] ([nombreEmpresa], [direccion], [id], [idPais]) VALUES (N'Manuels.co', N'Alajuela, el coyol', 4, 1)
INSERT [dbo].[proveedor] ([nombreEmpresa], [direccion], [id], [idPais]) VALUES (N'Indian', N'San jose', 6, 2)
INSERT [dbo].[proveedor] ([nombreEmpresa], [direccion], [id], [idPais]) VALUES (N'Demasa', N'San Jose, Pavas', 1005, 1)
INSERT [dbo].[proveedor] ([nombreEmpresa], [direccion], [id], [idPais]) VALUES (N'Walmart', N'Alajuela centro', 1006, 1)
SET IDENTITY_INSERT [dbo].[proveedor] OFF
GO
INSERT [dbo].[proveedorProducto] ([idProveedor], [idProducto]) VALUES (4, 2)
INSERT [dbo].[proveedorProducto] ([idProveedor], [idProducto]) VALUES (6, 3)
INSERT [dbo].[proveedorProducto] ([idProveedor], [idProducto]) VALUES (1005, 6)
INSERT [dbo].[proveedorProducto] ([idProveedor], [idProducto]) VALUES (4, 5)
INSERT [dbo].[proveedorProducto] ([idProveedor], [idProducto]) VALUES (1006, 2)
GO
SET IDENTITY_INSERT [dbo].[tienda] ON 

INSERT [dbo].[tienda] ([nombre], [direccion], [id]) VALUES (N'Moshu Stores', N'Heredia, San rafael', 1)
INSERT [dbo].[tienda] ([nombre], [direccion], [id]) VALUES (N'DLeditas', N'Alajuela, Zona franca', 2)
INSERT [dbo].[tienda] ([nombre], [direccion], [id]) VALUES (N'7Even', N'Alajuela, Mercado Central', 3)
SET IDENTITY_INSERT [dbo].[tienda] OFF
GO
INSERT [dbo].[TipoCategoria] ([id], [Descripcion]) VALUES (1, N'Hogar')
INSERT [dbo].[TipoCategoria] ([id], [Descripcion]) VALUES (2, N'Juguete')
INSERT [dbo].[TipoCategoria] ([id], [Descripcion]) VALUES (3, N'Escolar')
INSERT [dbo].[TipoCategoria] ([id], [Descripcion]) VALUES (4, N'Tecnologia')
GO
SET IDENTITY_INSERT [dbo].[TipoMovimiento] ON 

INSERT [dbo].[TipoMovimiento] ([id], [tipoSalida], [tipoEntrada]) VALUES (1, N'Encargo', N'No aplica')
INSERT [dbo].[TipoMovimiento] ([id], [tipoSalida], [tipoEntrada]) VALUES (2, N'No aplica', N'Devolucion')
INSERT [dbo].[TipoMovimiento] ([id], [tipoSalida], [tipoEntrada]) VALUES (3, N'Intercambio', N'Compra')
INSERT [dbo].[TipoMovimiento] ([id], [tipoSalida], [tipoEntrada]) VALUES (4, N'Compra', N'No aplica')
SET IDENTITY_INSERT [dbo].[TipoMovimiento] OFF
GO
SET IDENTITY_INSERT [dbo].[tipoUsuario] ON 

INSERT [dbo].[tipoUsuario] ([id], [permisoUsuario]) VALUES (1, 1)
INSERT [dbo].[tipoUsuario] ([id], [permisoUsuario]) VALUES (2, 2)
INSERT [dbo].[tipoUsuario] ([id], [permisoUsuario]) VALUES (3, 2)
SET IDENTITY_INSERT [dbo].[tipoUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([nombre], [apellidos], [email], [clave], [id], [esActivo], [idTipoUsuario]) VALUES (N'admin', N'istrador', N'admin', N'yQp4jaI+UcREW1GJCAfieA==', 1, 1, 1)
INSERT [dbo].[usuario] ([nombre], [apellidos], [email], [clave], [id], [esActivo], [idTipoUsuario]) VALUES (N'Esteban', N'rivera porras  ', N'luigirp64@gmail.com', N'VEexUQRHOMQOvdnJVPluTg==', 2, 1, 2)
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
ALTER TABLE [dbo].[contactos]  WITH CHECK ADD  CONSTRAINT [FK_contactos_proveedor1] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[proveedor] ([id])
GO
ALTER TABLE [dbo].[contactos] CHECK CONSTRAINT [FK_contactos_proveedor1]
GO
ALTER TABLE [dbo].[detalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_detalleFactura_estante] FOREIGN KEY([idEstante])
REFERENCES [dbo].[estante] ([id])
GO
ALTER TABLE [dbo].[detalleFactura] CHECK CONSTRAINT [FK_detalleFactura_estante]
GO
ALTER TABLE [dbo].[detalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_detalleFactura_proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[proveedor] ([id])
GO
ALTER TABLE [dbo].[detalleFactura] CHECK CONSTRAINT [FK_detalleFactura_proveedor]
GO
ALTER TABLE [dbo].[detalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_productoFacturados_factura] FOREIGN KEY([idInventario])
REFERENCES [dbo].[inventario] ([id])
GO
ALTER TABLE [dbo].[detalleFactura] CHECK CONSTRAINT [FK_productoFacturados_factura]
GO
ALTER TABLE [dbo].[detalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_productoFacturados_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[detalleFactura] CHECK CONSTRAINT [FK_productoFacturados_producto]
GO
ALTER TABLE [dbo].[inventario]  WITH CHECK ADD  CONSTRAINT [FK_factura_tienda] FOREIGN KEY([idTienda])
REFERENCES [dbo].[tienda] ([id])
GO
ALTER TABLE [dbo].[inventario] CHECK CONSTRAINT [FK_factura_tienda]
GO
ALTER TABLE [dbo].[inventario]  WITH CHECK ADD  CONSTRAINT [FK_factura_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[inventario] CHECK CONSTRAINT [FK_factura_usuario]
GO
ALTER TABLE [dbo].[inventario]  WITH CHECK ADD  CONSTRAINT [FK_inventario_TipoMovimiento] FOREIGN KEY([idTipoMovimiento])
REFERENCES [dbo].[TipoMovimiento] ([id])
GO
ALTER TABLE [dbo].[inventario] CHECK CONSTRAINT [FK_inventario_TipoMovimiento]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_TipoCategoria] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[TipoCategoria] ([id])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_TipoCategoria]
GO
ALTER TABLE [dbo].[productoEstante]  WITH CHECK ADD  CONSTRAINT [FK_productoEstante_estante] FOREIGN KEY([idEstante])
REFERENCES [dbo].[estante] ([id])
GO
ALTER TABLE [dbo].[productoEstante] CHECK CONSTRAINT [FK_productoEstante_estante]
GO
ALTER TABLE [dbo].[productoEstante]  WITH CHECK ADD  CONSTRAINT [FK_productoEstante_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[productoEstante] CHECK CONSTRAINT [FK_productoEstante_producto]
GO
ALTER TABLE [dbo].[proveedor]  WITH CHECK ADD  CONSTRAINT [FK_proveedor_pais] FOREIGN KEY([idPais])
REFERENCES [dbo].[pais] ([id])
GO
ALTER TABLE [dbo].[proveedor] CHECK CONSTRAINT [FK_proveedor_pais]
GO
ALTER TABLE [dbo].[proveedorProducto]  WITH CHECK ADD  CONSTRAINT [FK_proveedorProducto_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[proveedorProducto] CHECK CONSTRAINT [FK_proveedorProducto_producto]
GO
ALTER TABLE [dbo].[proveedorProducto]  WITH CHECK ADD  CONSTRAINT [FK_proveedorProducto_proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[proveedor] ([id])
GO
ALTER TABLE [dbo].[proveedorProducto] CHECK CONSTRAINT [FK_proveedorProducto_proveedor]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_tipoUsuario] FOREIGN KEY([idTipoUsuario])
REFERENCES [dbo].[tipoUsuario] ([id])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_tipoUsuario]
GO
USE [master]
GO
ALTER DATABASE [selly] SET  READ_WRITE 
GO
