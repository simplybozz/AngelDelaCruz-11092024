
--Creacion de la base de datos
CREATE DATABASE EvaluacionTecnicaDB;


--Usando la BD
USE EvaluacionTecnicaDB;


--Creacion de la tabla Roles
CREATE TABLE Roles (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Nombre VARCHAR(30),
	Usuario_Transaccion VARCHAR(30) DEFAULT USER,
	Fecha_Transaccion DATETIME DEFAULT GETDATE()
);


--Creacion de la tabla Usuario
CREATE TABLE Usuario (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	RolesId INT NOT NULL,
	Nombre VARCHAR(30),
	Apellido VARCHAR(30),
	Cedula BIGINT NOT NULL,
	Usuario_Nombre VARCHAR(30),
	Contrasena VARCHAR(50),
	Fecha_Nacimiento DATETIME,
	Usuario_Transaccion VARCHAR(30) DEFAULT USER,
	Fecha_Transaccion DATETIME DEFAULT GETDATE()
);


--Creacion de la clave foranea a Roles ID
ALTER TABLE Usuario ADD CONSTRAINT FK_ROLESID FOREIGN KEY (RolesId) REFERENCES Roles(Id);


--Creacion de Indice unico para el Nombre de los Roles
CREATE UNIQUE INDEX Ix_Nombre_Roles
ON Roles(Nombre);


--Creacion de Indice unico para Cedula de Usuario
CREATE UNIQUE INDEX Ix_Cedula_Usu
ON Usuario(Cedula);


--Creacion de Indice unico para el Nombre de Usuario de los Usuarios
CREATE UNIQUE INDEX Ix_Usuario_Nombre
ON Usuario(Usuario_Nombre);


--Creacion de Indice para la Fecha de nacimiento de Usuario 
CREATE INDEX Ix_Fecha_Nacimiento
ON Usuario(Fecha_Nacimiento);


--Comprobando tablas
SELECT COLUMN_NAME AS 'Nombre_Atributo', 
DATA_TYPE AS 'Tipo_Dato', 
CHARACTER_MAXIMUM_LENGTH AS 'Longitud', 
IS_NULLABLE AS 'Es_NULL?'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Roles';

SELECT COLUMN_NAME AS 'Nombre_Atributo', 
DATA_TYPE AS 'Tipo_Dato', 
CHARACTER_MAXIMUM_LENGTH AS 'Longitud', 
IS_NULLABLE AS 'Es_NULL?'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Usuario';


--Insercion de registros a tabla Roles
INSERT INTO Roles (Nombre) VALUES('ADMIN');
INSERT INTO Roles (Nombre) VALUES('DESARROLLADOR');


--Insercion de registros a tabla Usuario
INSERT INTO Usuario (RolesId, Nombre, Apellido, Cedula, Usuario_Nombre, Contrasena, Fecha_Nacimiento) VALUES (1, 'Simetrica', 'Consulting', 25322522135, 'ADMIN', 'ADMIN', 1990-01-01);
INSERT INTO Usuario (RolesId, Nombre, Apellido, Cedula, Usuario_Nombre, Contrasena, Fecha_Nacimiento) VALUES (2, 'Angel', 'De la Cruz', 40233432893, 'DESARROLLADOR', 'APLICANTE', 2005-04-06);


--Comprobando registros
SELECT * FROM Roles;
SELECT * FROM Usuario;
