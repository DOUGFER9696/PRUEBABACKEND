---Base de datos
create database RenoExpress;
go
use RenoExpress;
go
--Tablas de la base de datos
CREATE TABLE SUCURSAL
(id_sucursal int identity primary key not null,
 nombre varchar(200) not null,
 direccion varchar(300) null
);

CREATE TABLE PRODUCTO
(id_producto varchar(6) primary key not null,
nombre varchar(200) not null,
descripcion varchar(400) not null,
precio_venta float null,
estado char(1) not null
);

CREATE TABLE COMPRA
(id_compra int identity primary key not null,
id_sucursal int not null,
id_producto varchar(6) not null,
cantidad int not null,
precio_unitario float
);


CREATE TABLE INVENTARIO
(id_inventario int identity primary key not null,
id_producto varchar(6) not null,
id_sucursal int not null,
stoc_minimo int not null,
stock_maximo int not null,
cantidad int not null
);


create table CLIENTE
(id_cliente_nit varchar(8) primary key not null,
nombre varchar(200) not null,
apellido varchar(200) not null,
direccion varchar(300) null
);

CREATE TABLE ENCABEZADO_FACTURA
(id_encabezado_factura int primary key not null,
 fecha_venta date not null,
 id_cliente_nit varchar(8) not null,
 id_sucursal int not null,
 iva float null,
 total_venta float null
);



CREATE TABLE DETALLE_FACTURA
(id_detalle_factura int primary key not null,
id_encabezado_factura int not null,
id_producto varchar(6) not null,
cantidad int not null,
total float
);

go
alter table COMPRA add constraint fk_compra_sucursal foreign key (id_sucursal) references SUCURSAL(id_sucursal);
alter table COMPRA add constraint fk_compra_producto foreign key (id_producto) references PRODUCTO(id_producto);
alter table INVENTARIO add constraint fk_inventario_producto foreign key (id_producto) references PRODUCTO(id_producto);
alter table INVENTARIO add constraint fk_inventario_sucursal foreign key (id_sucursal) references SUCURSAL(id_sucursal);
alter table ENCABEZADO_FACTURA add constraint Fk_id_encabezado_factura_cliente foreign key (id_cliente_nit) references CLIENTE(id_cliente_nit);
alter table ENCABEZADO_FACTURA add constraint Fk_id_encabezado_factura_SUCURSAL foreign key (id_sucursal) references SUCURSAL(id_sucursal);
alter table DETALLE_FACTURA add constraint fk_detallefactura_encabezadofactura foreign key (id_encabezado_factura) references ENCABEZADO_FACTURA(id_encabezado_factura);
alter table DETALLE_FACTURA add constraint fk_detallefactura_producto foreign key (id_producto) references PRODUCTO(id_producto);

go 
--ingreso de informacion 
insert into PRODUCTO (id_producto,nombre,descripcion,precio_venta,estado)
VALUES
('JMAMSP','Max Stell Peleador','muneco de accion para hombres con accesorios de karate','150.00','A'),
('JMAMSC','Max Stell carro','carro de max stell accesorio','100.00','A'),
('JMAMSN','Max Stell Nadador','muneco de accion para hombres con accesorios de nado','250.00','A'),
('JMCPSV','Pistola de sonido verde','Pistola que lanza dardos de color verde','200.00','A'),
('JMBPAA','Pistola de agua Accesorios','Pistola de agua con accesorios para picinas','350.00','A'),
('JFABL','Barbie latina','Barbie latina con accesorios','100.00','A'),
('JFABE','Barbie Estudio','Estudio de comesticos de barbie ','550.00','A'),
('JFABD','Barbie Dentista','Barbie Dentista con accesorios','150.00','A'),
('JFBBN','Bebe nacido con accesorios','bebe con accesorios de recien nacido a baterias','350.00','A'),
('JFBMG','Muñeca grande','Muñeca grande a baterias con luces','150.00','A');

go

insert into SUCURSAL (nombre,direccion)
values
('SUCURSAL1','avenida 1 de sucural'),
('SUCURSAL2','avenida 2 de sucura2'),
('SUCURSAL3','avenida 3 de sucura3'),
('SUCURSAL4','avenida 4 de sucura4'),
('SUCURSAL5','avenida 5 de sucura5'),
('SUCURSAL6','avenida 6 de sucura6');

go

insert into CLIENTE (id_cliente_nit,nombre,apellido,direccion)
values
('98763452','NombreCliente1','Apellidocliente1','avenida cliente 1'),
('46478236','NombreCliente2','Apellidocliente2','avenida cliente 2'),
('12948562','NombreCliente3','Apellidocliente3','avenida cliente 3');


GO

CREATE TRIGGER TR_INVENTARIOCOMPRA
ON COMPRA AFTER INSERT 
as 
BEGIN
declare 
@valoractual int,
@valoringresado int,
@valornuevo int,
@id_producto varchar(6),
@id_sucursal int;

select @id_sucursal =  compra.id_sucursal from inserted compra;
select @id_producto = compra.id_producto from inserted compra;
select @valoractual = (select cantidad from INVENTARIO where id_producto=@id_producto);
select @valoractual = ISNULL(@valoractual,0);
select @valoringresado = compra.cantidad from inserted compra;
select @valornuevo = @valoractual+@valoringresado;
if @valoractual <= 0
insert into INVENTARIO(id_producto,id_sucursal,stoc_minimo,stock_maximo,cantidad)
values
(@id_producto,@id_sucursal,0,0,@valornuevo);
else
update INVENTARIO set cantidad = @valornuevo where id_producto=@id_producto;
END;

GO

CREATE TRIGGER TR_INVENTARIOVENTA
ON DETALLE_FACTURA AFTER INSERT 
AS
BEGIN
DECLARE 
@valorventa int,
@valoractual int,
@valornuevo int,
@id_producto varchar(6);

SELECT @valorventa = cantidad, @id_producto= id_producto from inserted;
SELECT @valoractual = (SELECT cantidad from INVENTARIO where id_producto=@id_producto);
SET @valornuevo = @valoractual-@valorventa;
UPDATE INVENTARIO SET cantidad= @valornuevo WHERE id_producto = @id_producto;
END;

GO
CREATE PROCEDURE SP_VENTASID
@id_encabezado_factura int
AS
BEGIN
select enca.id_encabezado_factura, enca.fecha_venta, enca.id_cliente_nit, enca.id_sucursal, enca.iva, enca.total_venta, det.id_detalle_factura, det.id_encabezado_factura, det.id_producto, det.cantidad, det.total _total from ENCABEZADO_FACTURA enca, DETALLE_FACTURA det where enca.id_encabezado_factura = det.id_encabezado_factura and det.id_encabezado_factura = @id_encabezado_factura;
END;

GO
CREATE PROCEDURE SP_CobraFactura
@id_encabezado_factura int
as
begin
DECLARE 
@valortotal float,
@Ivatotal float;

SET @valortotal = (SELECT SUM(TOTAL) from DETALLE_FACTURA where id_encabezado_factura = @id_encabezado_factura);
SET @Ivatotal = @valortotal*0.12;
set @valortotal = isnull(@valortotal,0);
UPDATE ENCABEZADO_FACTURA SET iva= @Ivatotal, total_venta=@valortotal WHERE id_encabezado_factura = @id_encabezado_factura;
end;