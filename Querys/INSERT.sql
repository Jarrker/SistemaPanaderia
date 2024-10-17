
select * from ROL

insert into ROL (Descripcion)
values('ADMINISTRADOR')



select * from USUARIO

insert into USUARIO(Documento,NombreCompleto,Clave,IdRol,Estado)
values ('101010', 'ADMIN', '123',1,1)

insert into USUARIO(Documento,NombreCompleto,Clave,IdRol,Estado)
values ('202020', 'Gianluca Lapadula', '456',1,1)


--insert into PERMISO(IdRol, NombreMenu)
--values
--(1,'menuUsuarios'),
--(1,'menuMantenedor'),
--(1,'menuVentas'),
--(1,'menuCompras'),
--(1,'menuClientes'),
--(1,'menuProveedores'),
--(1,'menuReportes'),
--(1,'menuAcercade')

select * from PERMISO


select IdCliente,DNI,NombreCompleto,Direccion,Telefono,Estado from CLIENTE

insert into CLIENTE(DNI,NombreCompleto,Direccion,Telefono,Estado)
values ('000333222', 'Juan Cipriano', 'Av. Siempre Viva',999666333,1)



select IdProveedor,RUC,RazonSocial,Rubro,Ciudad,Estado from PROVEEDOR

insert into PROVEEDOR(RUC,RazonSocial,Rubro,Ciudad,Estado)
values ('10306699', 'HarinaPan SAC', 'Industria Alimentaria', 'Trujillo', 1)



--select p.IdRol,p.NombreMenu from PERMISO p
--inner join ROL r on r.IdRol = p.IdRol
--inner join USUARIO u on u.IdRol = r.IdRol
--where u.IdUsuario = @idusuario