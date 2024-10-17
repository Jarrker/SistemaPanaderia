USE master
go

CREATE DATABASE DB_PANADERIA
GO

ALTER AUTHORIZATION ON DATABASE::DB_PANADERIA TO SA
GO

USE DB_PANADERIA
GO

CREATE TABLE ROL(
	IdRol int primary key identity,
	Descripcion varchar(50),
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE PERMISO(
	IdPermiso int primary key identity,
	IdRol int references ROL(IdRol),
	NombreMenu varchar(100),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE USUARIO(
	IdUsuario int primary key identity,
	Documento varchar(50),
	NombreCompleto varchar(MAX),
	Clave varchar(50),
	IdRol int references ROL(IdRol),
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE CLIENTE(
	IdCliente int primary key identity,
	DNI varchar(50),
	NombreCompleto varchar(MAX),
	Direccion varchar(MAX),
	Telefono varchar(MAX),
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE CONTRATO(
	IdContrato int primary key identity,
	Cantidad INT NOT NULL,
	Descripcion VARCHAR(200),
	Estado bit NOT NULL,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE CRONOGRAMA_PAGO(
	IdCronogramaPago int primary key identity,
	IdContrato INT REFERENCES CONTRATO(IdContrato),
	MetodoPago VARCHAR(50),
	FechaRegistro datetime
)
GO

CREATE TABLE PRODUCTO(
    IdProducto int primary key identity,
    Nombre VARCHAR(MAX) NOT NULL,
    Descripcion VARCHAR(MAX) NOT NULL,
    PrecioUnidad decimal(10,2) default 0,
    Cantidad INT NOT NULL default 0,
    Vencimiento DATETIME NOT NULL,
    Estado bit NOT NULL,
)
GO

CREATE TABLE PEDIDO(
    IdPedido int primary key identity,
	Descripcion VARCHAR(MAX) NOT NULL,
    Estado bit NOT NULL,
    FechaEntrega DATETIME NOT NULL,
)
GO

CREATE TABLE PEDIDO_PRODUCTO(
    IdPedido INT REFERENCES PEDIDO(IdPedido),
    IdProducto INT REFERENCES PRODUCTO(IdProducto),
    Cantidad INT,
    PRIMARY KEY (IdPedido, IdProducto)
)
GO

CREATE TABLE VENTA(
    IdVenta int primary key identity NOT NULL,
	IdCliente INT REFERENCES CLIENTE(IdCliente) NOT NULL,
	IdUsuario int references USUARIO(IdUsuario) NOT NULL,
    IdCronogramaPago INT REFERENCES CRONOGRAMA_PAGO(IdCronogramaPago) NOT NULL,
    IdPedido INT REFERENCES PEDIDO(IdPedido) NOT NULL,
	TipoDocumento varchar(50),
	NumeroDocumento varchar(50),
	DocumentoCliente varchar(50),
	NombreCliente varchar(MAX),
    MontoPago decimal(10,2),
	MontoCambio decimal(10,2),
	MontoTotal decimal(10,2),
    FechaRegistro datetime default getdate(),
)
GO

CREATE TABLE DETALLE_VENTA(
	IdDetalleVenta int primary key identity,
	IdVenta int references VENTA(IdVenta),
	IdProducto int references PRODUCTO(IdProducto),
	PrecioVenta decimal (10,2),
	Cantidad int,
	SubTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE CONTRATO_CLIENTE(
    IdCliente INT REFERENCES CLIENTE(IdCliente) NOT NULL,
    IdContrato INT REFERENCES CONTRATO(IdContrato) NOT NULL,
    PRIMARY KEY (IdCliente, IdContrato),
)
GO

CREATE TABLE INSUMOS(
    IdInsumos int primary key identity NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Descripcion VARCHAR(200) NOT NULL,
    Cantidad INT NOT NULL,
    Precio INT NOT NULL,
    Estado INT NOT NULL,
	FechaVencimiento DATETIME NOT NULL,
)
GO

CREATE TABLE PRODUCTO_INSUMOS(
    IdInsumos INT REFERENCES INSUMOS(IdInsumos) NOT NULL,
    IdProducto INT REFERENCES PRODUCTO(IdProducto)NOT NULL,
    PRIMARY KEY (IdInsumos, IdProducto),
)
GO

CREATE TABLE PROVEEDOR(
	IdProveedor int primary key identity,
	RUC varchar(50),
	RazonSocial varchar(MAX),
	Rubro varchar(MAX),
	Ciudad varchar(MAX),
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE COMPRA(
	IdCompra int primary key identity,
	IdUsuario int references USUARIO(IdUsuario),
	IdProveedor int references PROVEEDOR(IdProveedor),
	TipoDocumento varchar(50),
	NumeroDocumento varchar(50),
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE DETALLE_COMPRA(
	IdDetalleCompra int primary key identity,
	IdCompra int references COMPRA(IdCompra),
	IdInsumos int references INSUMOS(IdInsumos),
	PrecioCompra decimal(10,2) default 0,
	Cantidad int,
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
GO

