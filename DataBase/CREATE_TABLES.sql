-- Drops
DROP TABLE IF EXISTS LineaOrden;
DROP TABLE IF EXISTS Comprobante;
DROP TABLE IF EXISTS Orden_Compra;
DROP TABLE IF EXISTS Orden_Venta;
DROP TABLE IF EXISTS Orden;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Cuenta_Cliente;
DROP TABLE IF EXISTS Cuenta_Empleado;
DROP TABLE IF EXISTS Cuenta;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Empleado;


-- Tabla Empleado
CREATE TABLE Empleado (
  id_empleado INT AUTO_INCREMENT PRIMARY KEY,
  dni CHAR(8) NOT NULL,
  nombre VARCHAR(30) NOT NULL,
  apellido_paterno VARCHAR(30) NOT NULL,
  apellido_materno VARCHAR(30) NOT NULL,
  sueldo DECIMAL(10, 2) NOT NULL,
  rol ENUM(
    'Repartidor', 
    'EncargadoVentas', 
    'EncargadoAlmacen', 
    'Administrador'
    ) NOT NULL,
  activo TINYINT DEFAULT 1,
  UNIQUE INDEX dni_UNIQUE (dni ASC) VISIBLE
);

-- Tabla Cliente
CREATE TABLE Cliente (
  id_cliente INT AUTO_INCREMENT PRIMARY KEY,
  dni CHAR(8) NOT NULL,
  nombre VARCHAR(30) NOT NULL,
  apellido_paterno VARCHAR(30) NOT NULL,      
  apellido_materno VARCHAR(30) NOT NULL,      
  puntos INT NOT NULL,              -- Puntos que se pueden usar
  puntos_retenidos DECIMAL(10,2) DEFAULT 0,   -- Puntos que no se pueden usar
  ruc CHAR(11) NULL DEFAULT NULL,             -- Para clientes que son empresas
  razon_social VARCHAR(30) NULL DEFAULT NULL, -- Para clientes que son empresas
  direccion VARCHAR(50) NOT NULL,
  activo TINYINT DEFAULT 1,
  id_patrocinador INT NULL DEFAULT NULL,         -- Id del cliente que lo refirio
  FOREIGN KEY (id_patrocinador) REFERENCES Cliente(id_cliente),
  UNIQUE INDEX dni_UNIQUE (dni ASC) VISIBLE
);

-- Tabla Cuenta
CREATE TABLE Cuenta (
  id_cuenta INT AUTO_INCREMENT PRIMARY KEY,
  usuario VARCHAR(30) NOT NULL,
  contrasena VARCHAR(60) NOT NULL,
  activo TINYINT DEFAULT 1,
  UNIQUE INDEX usuario_UNIQUE (usuario ASC) VISIBLE
);

-- Tabla Cuenta_Empleado
CREATE TABLE Cuenta_Empleado (
  id_cuenta INT PRIMARY KEY,
  id_empleado INT NOT NULL,
  FOREIGN KEY (id_empleado) REFERENCES Empleado(id_empleado),
  FOREIGN KEY (id_cuenta) REFERENCES Cuenta(id_cuenta),
  UNIQUE INDEX id_empleado_UNIQUE (id_empleado ASC) VISIBLE
);

-- Tabla Cuenta_Cliente
CREATE TABLE Cuenta_Cliente (
  id_cuenta INT PRIMARY KEY,
  id_cliente INT NOT NULL,
  FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
  FOREIGN KEY (id_cuenta) REFERENCES Cuenta(id_cuenta),
  UNIQUE INDEX id_cliente_UNIQUE (id_cliente ASC) VISIBLE
);

-- Tabla Producto
CREATE TABLE Producto (
  id_producto INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(30) NOT NULL,
  precio_unitario DECIMAL(10, 2) NOT NULL,
  stock INT NOT NULL,
  capacidad decimal(10,2) NOT NULL,
  unidad_medida ENUM('gramos', 'mililitros', 'unidades') NOT NULL,
  tipo ENUM('Comestible', 'CuidadoPersonal', 'Otro') NOT NULL,
  puntos INT NOT NULL,
  activo TINYINT DEFAULT 1
);

-- Tabla Orden
CREATE TABLE Orden (
  id_orden INT AUTO_INCREMENT PRIMARY KEY,
  estado ENUM('Pendiente', 'Entregado', 'Cancelado') NOT NULL,
  fecha_creacion DATETIME NOT NULL,
  total DECIMAL(10, 2) NOT NULL
);

-- Tabla Orden_Venta
CREATE TABLE Orden_Venta (
  id_orden_venta INT AUTO_INCREMENT PRIMARY KEY,
  id_orden INT NOT NULL,
  id_cliente INT NOT NULL,
  id_empleado INT NOT NULL,
  id_repartidor INT NULL DEFAULT NULL,
  fecha_entrega DATETIME NULL DEFAULT NULL,
  tipo_venta ENUM('Presencial', 'Delivery') NOT NULL,
  metodo_pago ENUM('Efectivo', 'Tarjeta') NOT NULL,
  porcentaje_descuento DECIMAL(4,2) NOT NULL,
  FOREIGN KEY (id_orden) REFERENCES Orden(id_orden),
  FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
  FOREIGN KEY (id_empleado) REFERENCES Empleado(id_empleado),
  FOREIGN KEY (id_repartidor) REFERENCES Empleado(id_empleado)
);

-- Tabla Orden_Compra
CREATE TABLE Orden_Compra (
  id_orden_compra INT AUTO_INCREMENT PRIMARY KEY,
  id_orden INT NOT NULL,
  fecha_recepcion DATETIME NULL DEFAULT NULL,
  FOREIGN KEY (id_orden) REFERENCES Orden(id_orden)
);

-- Tabla Comprobante
CREATE TABLE Comprobante (
  id_comprobante INT AUTO_INCREMENT PRIMARY KEY,
  id_orden INT NOT NULL,
--  tipo_orden INT NOT NULL,
  tipo_comprobante ENUM('BoletaSimple', 'Factura') NOT NULL,
  fecha_emision DATETIME NOT NULL,
  FOREIGN KEY (id_orden) REFERENCES Orden(id_orden)
);

-- Tabla LineaOrden
CREATE TABLE LineaOrden (
  id_orden INT NOT NULL,
  id_producto INT NOT NULL,
  cantidad INT NOT NULL,
  subtotal DECIMAL(10, 2) NOT NULL,
  PRIMARY KEY (id_orden, id_producto),
  FOREIGN KEY (id_orden) REFERENCES Orden(id_orden),
  FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);
