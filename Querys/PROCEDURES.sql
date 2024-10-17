
--PROCEDIMIENTOS ALMACENADOS
use DB_PANADERIA

/*----------------------REGISTRAR USUARIO----------------------*/

CREATE OR ALTER PROCEDURE SP_RegistrarUsuario(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
BEGIN
    set @IdUsuarioResultado = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento)
	begin
		insert into usuario(Documento,NombreCompleto,Clave,IdRol,Estado) values
		(@Documento,@NombreCompleto,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento DNI para más de un usuario'
END
GO


--EDITAR USUARIO

CREATE OR ALTER PROCEDURE SP_EditarUsuario(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	if not exists(select * from USUARIO where Documento = @Documento and idusuario != @IdUsuario)
	begin
		update  usuario set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario
		set @Respuesta = 1
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end
go



--INHABILITAR USUARIO

CREATE OR ALTER PROCEDURE SP_EliminarUsuario(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM COMPRA C 
	INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una COMPRA\n' 
	END

	IF EXISTS (SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una VENTA\n' 
	END

	if(@pasoreglas = 1)
	begin
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1 
	end

end

go




/*----------------------PROCEDIMIENTO ALMACENADO PARA CLIENTES----------------------*/

CREATE OR ALTER PROCEDURE SP_RegistrarCliente(
@DNI varchar(50),
@NombreCompleto varchar(50),
@Direccion varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE DNI = @DNI)
	begin
		insert into CLIENTE(DNI,NombreCompleto,Direccion,Telefono,Estado) values (
		@DNI,@NombreCompleto,@Direccion,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de DNI ya existe'
end

go

CREATE OR ALTER PROCEDURE SP_ModificarCliente(
@IdCliente int,
@DNI varchar(50),
@NombreCompleto varchar(50),
@Direccion varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE DNI = @DNI and IdCliente != @IdCliente)
	begin
		update CLIENTE set
		DNI = @DNI,
		NombreCompleto = @NombreCompleto,
		Direccion = @Direccion,
		Telefono = @Telefono,
		Estado = @Estado
		where IdCliente = @IdCliente
	end
	else
	begin
		SET @Resultado = 0
		set @Mensaje = 'El numero de DNI ya existe'
	end
end




/*----------------------PROCEDIMIENTO ALMACENADO PARA PROVEEDORES----------------------*/

CREATE PROCEDURE SP_RegistrarProveedor(
@RUC varchar(50),
@RazonSocial varchar(50),
@Rubro varchar(50),
@Ciudad varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE RUC = @RUC)
	begin
		insert into PROVEEDOR(RUC,RazonSocial,Rubro,Ciudad,Estado) values (
		@RUC,@RazonSocial,@Rubro,@Ciudad,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de RUC ya existe'
end

GO

CREATE PROCEDURE SP_ModificarProveedor(
@IdProveedor int,
@RUC varchar(50),
@RazonSocial varchar(50),
@Rubro varchar(50),
@Ciudad varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE RUC = @RUC and IdProveedor != @IdProveedor)
	begin
		update PROVEEDOR set
		RUC = @RUC,
		RazonSocial = @RazonSocial,
		Rubro = @Rubro,
		Ciudad = @Ciudad,
		Estado = @Estado
		where IdProveedor = @IdProveedor
	end
	else
	begin
		SET @Resultado = 0
		set @Mensaje = 'El numero de RUC ya existe'
	end
end


go


CREATE PROCEDURE SP_EliminarProveedor(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from PROVEEDOR p
	 inner join COMPRA c on p.IdProveedor = c.IdProveedor
	 where p.IdProveedor = @IdProveedor
	)
	begin
	 delete top(1) from PROVEEDOR where IdProveedor = @IdProveedor
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'El proveedor se encuentara relacionado a una compra'
	end

end






--CREATE OR ALTER PROCEDURE spListarCliente
--AS
--BEGIN
--    SELECT * FROM CLIENTE;
--END
--GO



--CREATE PROCEDURE spBuscarCliente
--    @IdCliente INT
--AS
--BEGIN
--    SELECT * FROM Cliente WHERE IdCliente = @IdCliente;
--END
--GO