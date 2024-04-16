-- Eliminar la base de datos si existe
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'Forrajeria')
BEGIN
    USE master;
	 --DATABASE Forrajeria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

-- Eliminar la base de datos Forrajeria
		DROP DATABASE Forrajeria;
END
 USE master;
-- Crear la base de datos Forrajeria
CREATE DATABASE Forrajeria;

-- Usar la base de datos recién creada
USE Forrajeria;

-- Crear la tabla Usuarios
CREATE TABLE Usuarios (
   UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
   NombreUsuario NVARCHAR(255) NOT NULL,
   Pass NVARCHAR(255) NOT NULL,
   NombreCompleto NVARCHAR(255),
   Rol NVARCHAR(50) NOT NULL
);

-- Crear la tabla Productos
CREATE TABLE Productos (
    ProductoID INT IDENTITY(100,1) PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    ProveedorID INT
);


-- Crear la tabla Clientes
CREATE TABLE Clientes (
    ClienteID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(15)
);

-- Crear la tabla Ventas
CREATE TABLE Ventas (
    VentaID INT IDENTITY(1,1) PRIMARY KEY,
    Fecha nvarchar(255) NOT NULL,
    ClienteID INT,
);
-- modificar tabla 
ALTER TABLE Ventas
ADD ImporteTotal DECIMAL(10, 2) NULL;

-- Crear la tabla DetallesVentas
CREATE TABLE DetallesVentas (
    DetalleVentaID INT IDENTITY(1,1) PRIMARY KEY,
    VentaID INT,
    ProductoID INT,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
);

-- Crear la tabla Proveedores
CREATE TABLE Proveedores (
    ProveedorID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(15)
);


-- Crear la tabla Compras
CREATE TABLE Compras (
   CompraID INT IDENTITY(1,1) PRIMARY KEY,
   Fecha DATE NOT NULL,
   ProveedorID INT
);

-- modficar tabla compras
ALTER TABLE Compras
ALTER COLUMN Fecha VARCHAR(50);
-- agregar columna importe
ALTER TABLE Compras
ADD ImporteTotal DECIMAL(18, 2) NULL;

-- Agregar la restricción de clave foranea para la columna ProveedorID en la tabla Productos
ALTER TABLE Productos
ADD CONSTRAINT FK_Productos_Proveedores FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID);

-- Agregar la restricción de clave foranea para la columna ProveedorID en la tabla Compras
ALTER TABLE Compras
ADD CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID);



--Ingreso usuario Adminstrador
INSERT INTO Usuarios(NombreUsuario,Pass,NombreCompleto,Rol)
VALUES('admin','admin','Administrador','Administrador');


--ingresos Proveedores
INSERT INTO Proveedores(Nombre, Direccion, Telefono)
VALUES ('ProveedorForraje', 'Avenida Verde 456', '381555556789');

-- Proveedor 3
INSERT INTO Proveedores(Nombre, Direccion, Telefono)
VALUES ('AgroSuministros', 'Calle de los Suministros 789', '381555554321');

-- Proveedor 4
INSERT INTO Proveedores(Nombre, Direccion, Telefono)
VALUES ('Forrajes y Más', 'Calle de los Agricultores 987', '381555557890');

-- Proveedor 5
INSERT INTO Proveedores(Nombre, Direccion, Telefono)
VALUES ('Alimentos para Animales S.A.', 'Avenida de los Alimentos 654', '381551112345');

--ingresos Productos
insert into Productos(Nombre,Descripcion,Precio,Stock,ProveedorID)
values ('Purina','Suplementos vitamínicos y minerales para animales.',3500,10,1);
-- Producto 2
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Alfalfa', 'Forraje de alta calidad para animales', 800, 20, 2);

-- Producto 3
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Heno de avena', 'Alimento para roedores y conejos', 500, 15, 3);

-- Producto 4
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Fertilizante orgánico', 'Mejora la calidad del suelo', 1200, 5, 1);

-- Producto 5
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Granos de maíz', 'Alimento básico para animales de granja', 600, 30, 4);

-- Producto 6
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Paja de trigo', 'Lecho para animales pequeños', 300, 25, 2);

-- Producto 7
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Semillas de girasol', 'Snack para aves', 700, 10, 3);

-- Producto 8
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Sal mineral', 'Suplemento para ganado', 900, 12, 1);

-- Producto 9
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Heno de alfalfa', 'Forraje rico en proteínas', 850, 18, 2);

-- Producto 10
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Avena', 'Grano para equinos', 750, 22, 3);

-- Producto 11
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Suplemento de calcio', 'Mejora la salud de los huesos', 1200, 8, 1);

-- Producto 12
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Piedras de sal', 'Bloques de sal para lamer', 400, 15, 4);

-- Producto 13
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Pienso de conejo', 'Nutrición equilibrada para conejos', 900, 10, 3);

-- Producto 14
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Fardos de heno', 'Gran cantidad de forraje', 1500, 5, 2);

-- Producto 15
INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES ('Semillas de maíz', 'Alimento para aves y roedores', 650, 15, 1);


SELECT * FROM Usuarios;
select * from Productos;
select * from Proveedores;

--update Productos
--set stock=10


