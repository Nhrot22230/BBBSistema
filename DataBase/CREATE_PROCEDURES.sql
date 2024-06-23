-- ==============================================
-- PROCEDIMIENTOS ALMACENADOS
-- ==============================================

-- ----------------------------------------------
-- DROP ALL PROCEDURES
-- ----------------------------------------------
DROP PROCEDURE IF EXISTS insertar_empleado;
DROP PROCEDURE IF EXISTS actualizar_empleado;
DROP PROCEDURE IF EXISTS eliminar_empleado;
DROP PROCEDURE IF EXISTS listar_empleados;
DROP PROCEDURE IF EXISTS listar_empleados_todos;

DROP PROCEDURE IF EXISTS insertar_cliente;
DROP PROCEDURE IF EXISTS actualizar_cliente;
DROP PROCEDURE IF EXISTS eliminar_cliente;
DROP PROCEDURE IF EXISTS listar_clientes;

DROP PROCEDURE IF EXISTS validar_acceso;
DROP PROCEDURE IF EXISTS insertar_cuenta;
DROP PROCEDURE IF EXISTS actualizar_cuenta;
DROP PROCEDURE IF EXISTS eliminar_cuenta;
DROP PROCEDURE IF EXISTS listar_cuentas;

DROP PROCEDURE IF EXISTS iniciar_sesion_cliente;
DROP PROCEDURE IF EXISTS insertar_cuenta_cliente;
DROP PROCEDURE IF EXISTS actualizar_cuenta_cliente;
DROP PROCEDURE IF EXISTS eliminar_cuenta_cliente;
DROP PROCEDURE IF EXISTS listar_cuentas_cliente;

DROP PROCEDURE IF EXISTS iniciar_sesion_empleado;
DROP PROCEDURE IF EXISTS insertar_cuenta_empleado;
DROP PROCEDURE IF EXISTS actualizar_cuenta_empleado;
DROP PROCEDURE IF EXISTS eliminar_cuenta_empleado;
DROP PROCEDURE IF EXISTS listar_cuentas_empleado;

DROP PROCEDURE IF EXISTS insertar_producto;
DROP PROCEDURE IF EXISTS actualizar_producto;
DROP PROCEDURE IF EXISTS eliminar_producto;
DROP PROCEDURE IF EXISTS listar_productos;
DROP PROCEDURE IF EXISTS listar_x_id_y_nombre;

DROP PROCEDURE IF EXISTS insertar_orden;
DROP PROCEDURE IF EXISTS actualizar_orden;
DROP PROCEDURE IF EXISTS listar_ordenes;

DROP PROCEDURE IF EXISTS insertar_orden_venta;
DROP PROCEDURE IF EXISTS actualizar_orden_venta;
DROP PROCEDURE IF EXISTS listar_ordenes_venta;

DROP PROCEDURE IF EXISTS insertar_orden_compra;
DROP PROCEDURE IF EXISTS actualizar_orden_compra;
DROP PROCEDURE IF EXISTS listar_ordenes_compra;

DROP PROCEDURE IF EXISTS insertar_comprobante;
DROP PROCEDURE IF EXISTS actualizar_comprobante;
DROP PROCEDURE IF EXISTS eliminar_comprobante;
DROP PROCEDURE IF EXISTS listar_comprobantes;

DROP PROCEDURE IF EXISTS insertar_linea_orden;
DROP PROCEDURE IF EXISTS actualizar_linea_orden;
DROP PROCEDURE IF EXISTS listar_lineas_orden;
DROP PROCEDURE IF EXISTS eliminar_linea_orden;

DROP PROCEDURE IF EXISTS listar_empleado_cuenta;
DROP PROCEDURE IF EXISTS listar_cliente_cuenta;
-- ----------------------------------------------
-- CRUD para Empleado
-- ----------------------------------------------

DELIMITER $$
CREATE PROCEDURE insertar_empleado(
  OUT p_id_empleado INT,
  IN p_dni CHAR(8),
  IN p_nombre VARCHAR(30),
  IN p_apellido_paterno VARCHAR(30),
  IN p_apellido_materno VARCHAR(30),
  IN p_sueldo DECIMAL(10, 2),
  IN p_rol ENUM('Repartidor', 'EncargadoVentas', 'EncargadoAlmacen', 'Administrador')
)
BEGIN
  INSERT INTO Empleado(dni, nombre, apellido_paterno, apellido_materno, sueldo, rol)
  VALUES(p_dni, p_nombre, p_apellido_paterno, p_apellido_materno, p_sueldo, p_rol);
  SET p_id_empleado = LAST_INSERT_ID();
END$$

CREATE PROCEDURE actualizar_empleado(
  IN p_id_empleado INT,
  IN p_dni CHAR(8),
  IN p_nombre VARCHAR(30),
  IN p_apellido_paterno VARCHAR(30),
  IN p_apellido_materno VARCHAR(30),
  IN p_sueldo DECIMAL(10, 2),
  IN p_rol ENUM('Repartidor', 'EncargadoVentas', 'EncargadoAlmacen', 'Administrador')
)
BEGIN
  UPDATE Empleado
  SET dni = p_dni,
      nombre = p_nombre,
      apellido_paterno = p_apellido_paterno,
      apellido_materno = p_apellido_materno,
      sueldo = p_sueldo,
      rol = p_rol
  WHERE id_empleado = p_id_empleado;
END$$

CREATE PROCEDURE eliminar_empleado(
  IN p_id_empleado INT
)
BEGIN
  UPDATE Empleado
  SET activo = 0
  WHERE id_empleado = p_id_empleado;
END$$

CREATE PROCEDURE listar_empleados(
  IN p_cadena VARCHAR(30)
)
BEGIN
  SELECT id_empleado, 
  dni, nombre, apellido_paterno, 
  apellido_materno, sueldo, rol
  FROM Empleado
  WHERE activo = 1 AND (nombre LIKE CONCAT('%', p_cadena, '%') OR
        apellido_paterno LIKE CONCAT('%', p_cadena, '%') OR
        apellido_materno LIKE CONCAT('%', p_cadena, '%') OR
        dni LIKE CONCAT('%', p_cadena, '%'));
END$$

CREATE PROCEDURE listar_empleados_todos()
BEGIN
  SELECT id_empleado, 
  dni, nombre, apellido_paterno, 
  apellido_materno, sueldo, rol, activo
  FROM Empleado;
END$$

-- ----------------------------------------------
-- CRUD para Cliente
-- ----------------------------------------------
DELIMITER $$
CREATE PROCEDURE insertar_cliente(
  OUT p_id_cliente INT,
  IN p_dni CHAR(8),
  IN p_nombre VARCHAR(30),
  IN p_apellido_paterno VARCHAR(30),
  IN p_apellido_materno VARCHAR(30),
  IN p_puntos INT,
  IN p_puntos_retenidos INT,
  IN p_ruc CHAR(11),
  IN p_razon_social VARCHAR(30),
  IN p_direccion VARCHAR(50),
  IN p_id_patrocinador INT
)
BEGIN
  INSERT INTO Cliente(dni, nombre, apellido_paterno, apellido_materno, puntos, puntos_retenidos, ruc, razon_social, direccion, id_patrocinador)
  VALUES(p_dni, p_nombre, p_apellido_paterno, p_apellido_materno, p_puntos, p_puntos_retenidos, p_ruc, p_razon_social, p_direccion, p_id_patrocinador);
  SET p_id_cliente = LAST_INSERT_ID();
END$$

CREATE PROCEDURE actualizar_cliente(
  IN p_id_cliente INT,
  IN p_dni CHAR(8),
  IN p_nombre VARCHAR(30),
  IN p_apellido_paterno VARCHAR(30),
  IN p_apellido_materno VARCHAR(30),
  IN p_puntos INT,
  IN p_puntos_retenidos INT,
  IN p_ruc CHAR(11),
  IN p_razon_social VARCHAR(30),
  IN p_direccion VARCHAR(50),
  IN p_id_patrocinador INT
)
BEGIN
  UPDATE Cliente
  SET dni = p_dni,
      nombre = p_nombre,
      apellido_paterno = p_apellido_paterno,
      apellido_materno = p_apellido_materno,
      puntos = p_puntos,
      puntos_retenidos = p_puntos_retenidos,
      ruc = p_ruc,
      razon_social = p_razon_social,
      direccion = p_direccion,
      id_patrocinador = p_id_patrocinador
  WHERE id_cliente = p_id_cliente;
END$$

CREATE PROCEDURE eliminar_cliente(
  IN p_id_cliente INT
)
BEGIN
  UPDATE Cliente
  SET activo = 0
  WHERE id_cliente = p_id_cliente;
END$$

CREATE PROCEDURE listar_clientes(
  IN p_cadena VARCHAR(50)
)
BEGIN
  SELECT c1.id_cliente, c1.dni, c1.nombre, 
  c1.apellido_paterno, c1.apellido_materno, c1.puntos,
  c1.puntos_retenidos, c1.ruc, c1.razon_social,
  c1.direccion, 
  c1.id_patrocinador, 
  c2.dni as dni_patrocinador,
  c2.nombre as nombre_patrocinador,
  c2.apellido_paterno as apellido_paterno_patrocinador,
  c2.apellido_materno as apellido_materno_patrocinador,
  c2.puntos as puntos_patrocinador,
  c2.puntos_retenidos as puntos_retenidos_patrocinador,
  c2.ruc as ruc_patrocinador,
  c2.razon_social as razon_social_patrocinador,
  c2.direccion as direccion_patrocinador,
  c3.id_cliente as id_patrocinado,
  c3.dni as dni_patrocinado,
  c3.nombre as nombre_patrocinado,
  c3.apellido_paterno as apellido_paterno_patrocinado,
  c3.apellido_materno as apellido_materno_patrocinado,
  c3.puntos as puntos_patrocinado,
  c3.puntos_retenidos as puntos_retenidos_patrocinado,
  c3.ruc as ruc_patrocinado,
  c3.razon_social as razon_social_patrocinado,
  c3.direccion as direccion_patrocinado
  FROM Cliente as c1
  LEFT JOIN Cliente as c2 ON c1.id_patrocinador = c2.id_cliente
  LEFT JOIN Cliente as c3 ON c1.id_cliente = c3.id_patrocinador
  WHERE c1.activo = 1
  AND (c1.dni LIKE CONCAT('%', p_cadena, '%') OR
       c1.nombre LIKE CONCAT('%', p_cadena, '%') OR
       c1.apellido_paterno LIKE CONCAT('%', p_cadena, '%') OR
       c1.apellido_materno LIKE CONCAT('%', p_cadena, '%') OR
       c1.ruc LIKE CONCAT('%', p_cadena, '%') OR
       c1.razon_social LIKE CONCAT('%', p_cadena, '%') OR
       c1.direccion LIKE CONCAT('%', p_cadena, '%'));
END$$

-- ----------------------------------------------
-- CRUD para Cuenta
-- ----------------------------------------------
CREATE PROCEDURE validar_acceso(
  OUT p_acceso INT,
  IN p_usuario VARCHAR(30),
  IN p_contrasena VARCHAR(60)
)
BEGIN
  DECLARE v_id_cuenta INT;
  SELECT id_cuenta INTO v_id_cuenta
  FROM Cuenta
  WHERE usuario = p_usuario AND contrasena = p_contrasena AND activo = 1;
  IF v_id_cuenta IS NOT NULL THEN
    SET p_acceso = 1;
  ELSE
    SET p_acceso = 0;
  END IF;
END$$

CREATE PROCEDURE insertar_cuenta(
  OUT p_id_cuenta INT,
  IN p_usuario VARCHAR(30),
  IN p_contrasena VARCHAR(60)
)
BEGIN
  INSERT INTO Cuenta(usuario, contrasena)
  VALUES(p_usuario, p_contrasena);
  SET p_id_cuenta = LAST_INSERT_ID();
END$$

CREATE PROCEDURE actualizar_cuenta(
  IN p_id_cuenta INT,
  IN p_usuario VARCHAR(30),
  IN p_contrasena VARCHAR(60)
)
BEGIN
  UPDATE Cuenta
  SET usuario = p_usuario,
      contrasena = p_contrasena
  WHERE id_cuenta = p_id_cuenta;
END$$

CREATE PROCEDURE eliminar_cuenta(
  IN p_id_cuenta INT
)
BEGIN
  UPDATE Cuenta
  SET activo = 0
  WHERE id_cuenta = p_id_cuenta;
END$$

CREATE PROCEDURE listar_cuentas()
BEGIN
  SELECT id_cuenta, usuario, contrasena
  FROM Cuenta WHERE activo = 1;
END$$

-- ----------------------------------------------
-- CRUD para Cuenta Cliente
-- ----------------------------------------------
CREATE PROCEDURE iniciar_sesion_cliente(
  OUT p_contrasena VARCHAR(60),
  IN p_usuario VARCHAR(30)
)
BEGIN
  DECLARE v_id_cuenta INT;
  DECLARE v_id_cliente INT;

  SELECT id_cuenta, contrasena INTO v_id_cuenta, p_contrasena
  FROM Cuenta
  WHERE usuario = p_usuario;

  SELECT id_cliente INTO v_id_cliente
  FROM Cuenta_Cliente
  WHERE id_cuenta = v_id_cuenta;

  IF v_id_cuenta IS NOT NULL THEN
    SELECT c.id_cliente, c.dni, c.nombre, c.apellido_paterno, c.apellido_materno, c.puntos, c.puntos_retenidos, c.ruc, c.razon_social, c.direccion,
    c.id_patrocinador, p.dni as dni_patrocinador, p.nombre as nombre_patrocinador, p.apellido_paterno as apellido_paterno_patrocinador, p.apellido_materno as apellido_materno_patrocinador, p.puntos as puntos_patrocinador, p.puntos_retenidos as puntos_retenidos_patrocinador, p.ruc as ruc_patrocinador, p.razon_social as razon_social_patrocinador, p.direccion as direccion_patrocinador
    FROM Cliente c
    LEFT JOIN Cliente p ON c.id_patrocinador = p.id_cliente
    WHERE c.id_cliente = v_id_cliente;
  END IF;
END$$

CREATE PROCEDURE insertar_cuenta_cliente(
  OUT p_id_cuenta INT,
  IN p_usuario VARCHAR(30),
  IN p_contrasena VARCHAR(60),
  IN p_id_cliente INT
)
BEGIN
  CALL insertar_cuenta(p_id_cuenta, p_usuario, p_contrasena);
  INSERT INTO Cuenta_Cliente(id_cuenta, id_cliente)
  VALUES(p_id_cuenta, p_id_cliente);
END$$

-- p_id_cliente is UNIQUE and NOT NULL and UNIQUE INDEX
CREATE PROCEDURE actualizar_cuenta_cliente(
  IN p_id_cliente INT,
  IN p_usuario VARCHAR(30),
  IN p_contrasena VARCHAR(60)
)
BEGIN
  DECLARE v_id_cuenta INT;
  SELECT id_cuenta INTO v_id_cuenta
  FROM Cuenta_Cliente
  WHERE id_cliente = p_id_cliente;
  CALL actualizar_cuenta(v_id_cuenta, p_usuario, p_contrasena);
END$$

CREATE PROCEDURE eliminar_cuenta_cliente(
  IN p_id_cliente INT
)
BEGIN
  DECLARE v_id_cuenta INT;
  SELECT id_cuenta INTO v_id_cuenta
  FROM Cuenta_Cliente
  WHERE id_cliente = p_id_cliente;
  DELETE FROM Cuenta_Cliente cc WHERE cc.id_cliente = p_id_cliente;
  DELETE FROM Cuenta c WHERE c.id_cuenta = v_id_cuenta;
  -- CALL eliminar_cuenta(v_id_cuenta);
END$$

CREATE PROCEDURE listar_cuentas_cliente()
BEGIN
  SELECT cc.id_cuenta, cc.id_cliente, c.usuario, c.contrasena
  FROM Cuenta_Cliente cc JOIN Cuenta c ON cc.id_cuenta = c.id_cuenta;
END$$

-- ----------------------------------------------
-- CRUD para Cuenta Empleado
-- ----------------------------------------------
CREATE PROCEDURE iniciar_sesion_empleado(
  OUT p_contrasena VARCHAR(60),
  IN p_usuario VARCHAR(30)
)
BEGIN
  DECLARE v_id_cuenta INT;
  DECLARE v_id_empleado INT;
  
  SELECT id_cuenta, contrasena INTO v_id_cuenta, p_contrasena
  FROM Cuenta
  WHERE usuario = p_usuario;

  SELECT id_empleado INTO v_id_empleado
  FROM Cuenta_Empleado
  WHERE id_cuenta = v_id_cuenta;

  IF v_id_cuenta IS NOT NULL THEN
    SELECT e.id_empleado, e.dni, e.nombre, e.apellido_paterno, e.apellido_materno, e.sueldo, e.rol
    FROM Empleado e
    WHERE e.id_empleado = v_id_empleado;
  END IF;
END$$

CREATE PROCEDURE insertar_cuenta_empleado(
  OUT p_id_cuenta INT,
  IN p_usuario VARCHAR(30),
  IN p_contrasena VARCHAR(60),
  IN p_id_empleado INT
)
BEGIN
  CALL insertar_cuenta(p_id_cuenta, p_usuario, p_contrasena);
  INSERT INTO Cuenta_Empleado(id_cuenta, id_empleado)
  VALUES(p_id_cuenta, p_id_empleado);
END$$

-- p_id_empleado is UNIQUE and NOT NULL and UNIQUE INDEX
CREATE PROCEDURE actualizar_cuenta_empleado(
  IN p_id_empleado INT,
  IN p_usuario VARCHAR(30),
  IN p_contrasena VARCHAR(60)
)
BEGIN
  DECLARE v_id_cuenta INT;
  SELECT id_cuenta INTO v_id_cuenta
  FROM Cuenta_Empleado
  WHERE id_empleado = p_id_empleado;
  CALL actualizar_cuenta(v_id_cuenta, p_usuario, p_contrasena);
END$$

CREATE PROCEDURE eliminar_cuenta_empleado(
  IN p_id_empleado INT
)
BEGIN
  DECLARE v_id_cuenta INT;
  SELECT id_cuenta INTO v_id_cuenta
  FROM Cuenta_Empleado
  WHERE id_empleado = p_id_empleado;
  DELETE FROM Cuenta_Empleado ce WHERE ce.id_empleado = p_id_empleado;
  DELETE FROM Cuenta c WHERE c.id_cuenta = v_id_cuenta;
  -- CALL eliminar_cuenta(v_id_cuenta);
END$$

CREATE PROCEDURE listar_cuentas_empleado()
BEGIN
  SELECT ce.id_cuenta, ce.id_empleado, c.usuario, c.contrasena
  FROM Cuenta_Empleado ce JOIN Cuenta c ON ce.id_cuenta = c.id_cuenta;
END$$


-- ----------------------------------------------
-- CRUD para Producto
-- ----------------------------------------------
CREATE PROCEDURE insertar_producto(
  OUT p_id_producto INT,
  IN p_nombre VARCHAR(30),
  IN p_precio_unitario DECIMAL(10, 2),
  IN p_stock INT,
  IN p_capacidad DECIMAL(10,2),
  IN p_unidad_medida ENUM('gramos', 'mililitros'),
  IN p_tipo ENUM('Comestible', 'CuidadoPersonal'),
  IN p_puntos INT
)
BEGIN
  INSERT INTO Producto(nombre, precio_unitario, stock, capacidad, unidad_medida, tipo, puntos)
  VALUES(p_nombre, p_precio_unitario, p_stock, p_capacidad, p_unidad_medida, p_tipo, p_puntos);
  SET p_id_producto = LAST_INSERT_ID();
END$$

CREATE PROCEDURE actualizar_producto(
  IN p_id_producto INT,
  IN p_nombre VARCHAR(30),
  IN p_precio_unitario DECIMAL(10, 2),
  IN p_stock INT,
  IN p_capacidad DECIMAL(10,2),
  IN p_unidad_medida ENUM('gramos', 'mililitros'),
  IN p_tipo ENUM('Comestible', 'CuidadoPersonal'),
  IN p_puntos INT
)
BEGIN
  UPDATE Producto
  SET nombre = p_nombre,
      precio_unitario = p_precio_unitario,
      stock = p_stock,
      capacidad = p_capacidad,
      unidad_medida = p_unidad_medida,
      tipo = p_tipo,
      puntos = p_puntos
  WHERE id_producto = p_id_producto;
END$$

CREATE PROCEDURE eliminar_producto(
  IN p_id_producto INT
)
BEGIN
  UPDATE Producto
  SET activo = 0
  WHERE id_producto = p_id_producto;
END$$

CREATE PROCEDURE listar_productos()
BEGIN
  SELECT id_producto, 
  nombre, precio_unitario, stock, 
  capacidad, unidad_medida, tipo, puntos
  FROM Producto WHERE activo = 1;
END$$

CREATE PROCEDURE listar_x_id_y_nombre(
  IN p_cadena VARCHAR(30)
)
BEGIN
  SELECT id_producto, 
  nombre, precio_unitario, stock, 
  capacidad, unidad_medida, tipo, puntos
  FROM Producto
  WHERE activo = 1 AND (id_producto LIKE CONCAT('%', p_cadena, '%') OR
        nombre LIKE CONCAT('%', p_cadena, '%'));
END$$
-- ----------------------------------------------
-- CRUD para Orden
-- ----------------------------------------------

CREATE PROCEDURE insertar_orden(
  OUT p_id_orden INT,
  IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
  IN p_fecha_creacion DATETIME,
  IN p_total DECIMAL(10, 2)
)
BEGIN
  INSERT INTO Orden(estado, fecha_creacion, total)
  VALUES(p_estado, p_fecha_creacion, p_total);
  SET p_id_orden = LAST_INSERT_ID();
END$$

CREATE PROCEDURE actualizar_orden(
  IN p_id_orden INT,
  IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
  IN p_total DECIMAL(10, 2)
)
BEGIN
  UPDATE Orden
  SET estado = p_estado,
      total = p_total
  WHERE id_orden = p_id_orden;
END$$

CREATE PROCEDURE listar_ordenes()
BEGIN
  SELECT id_orden, estado, fecha_creacion, total
  FROM Orden; 
END$$

-- ----------------------------------------------
-- CRUD para Orden_Venta
-- ----------------------------------------------
CREATE PROCEDURE insertar_orden_venta(
  OUT p_id_orden_venta INT,
  OUT p_id_orden INT,
  IN p_id_cliente INT,
  IN p_id_empleado INT,
  IN p_id_repartidor INT, -- Este es ahora opcional
  IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
  IN p_fecha_creacion DATETIME,
  IN p_tipo_venta ENUM('Presencial', 'Delivery'),
  IN p_metodo_pago ENUM('Efectivo', 'Tarjeta'),
  IN p_porcentaje_descuento DECIMAL(4,2),
  IN p_total DECIMAL(10, 2)
)
BEGIN
  DECLARE v_puntos_cliente INT;
  DECLARE v_id_patrocinador INT;
  DECLARE v_puntos_patrocinador INT;
  
  SELECT puntos INTO v_puntos_cliente FROM Cliente WHERE id_cliente = p_id_cliente;
  UPDATE Cliente SET puntos = v_puntos_cliente + FLOOR(p_total*0.2) WHERE id_cliente = p_id_cliente;


  SELECT id_patrocinador INTO v_id_patrocinador FROM Cliente WHERE id_cliente = p_id_cliente;
  IF v_id_patrocinador IS NOT NULL THEN
    SELECT puntos INTO v_puntos_patrocinador FROM Cliente WHERE id_cliente = v_id_patrocinador;

    UPDATE Cliente SET puntos = FLOOR(p_total*0.05) + v_puntos_patrocinador WHERE id_cliente = v_id_patrocinador;
  END IF;
  
  -- Insertar en la tabla Orden
  CALL insertar_orden(p_id_orden, p_estado, p_fecha_creacion, p_total);

  -- Insertar en la tabla Orden_Venta, manejando el id_repartidor opcional
  IF p_id_repartidor IS NOT NULL THEN
    INSERT INTO Orden_Venta(id_orden, id_cliente, id_empleado, id_repartidor, tipo_venta, metodo_pago, porcentaje_descuento)
    VALUES(p_id_orden, p_id_cliente, p_id_empleado, p_id_repartidor, p_tipo_venta, p_metodo_pago, p_porcentaje_descuento);
  ELSE
    INSERT INTO Orden_Venta(id_orden, id_cliente, id_empleado, tipo_venta, metodo_pago, porcentaje_descuento)
    VALUES(p_id_orden, p_id_cliente, p_id_empleado, p_tipo_venta, p_metodo_pago, p_porcentaje_descuento);
  END IF;
  
  SET p_id_orden_venta = LAST_INSERT_ID();
END$$



CREATE PROCEDURE actualizar_orden_venta(
  IN p_id_orden_venta INT,
  IN p_id_orden INT,
  IN p_id_cliente INT,
  IN p_id_empleado INT,
  IN p_id_repartidor INT, -- Este es ahora opcional
  IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
  IN p_fecha_entrega DATETIME,
  IN p_tipo_venta ENUM('Presencial', 'Delivery'),
  IN p_metodo_pago ENUM('Efectivo', 'Tarjeta'),
  IN p_porcentaje_descuento DECIMAL(4,2),
  IN p_total DECIMAL(10, 2)
)
BEGIN
  -- Actualizar en la tabla Orden
  CALL actualizar_orden(p_id_orden, p_estado, p_total);
  
  -- Actualizar en la tabla Orden_Venta
  UPDATE Orden_Venta
  SET id_orden = p_id_orden,
      id_cliente = p_id_cliente,
      id_empleado = p_id_empleado,
      id_repartidor = COALESCE(NULLIF(p_id_repartidor, 0), NULL),
      fecha_entrega = COALESCE(NULLIF(p_fecha_entrega, 0), NULL),
      tipo_venta = p_tipo_venta,
      metodo_pago = p_metodo_pago,
      porcentaje_descuento = p_porcentaje_descuento
  WHERE id_orden_venta = p_id_orden_venta;
END$$

CREATE PROCEDURE listar_ordenes_venta(
  IN p_cadena VARCHAR(30)
)
BEGIN
  SELECT o.id_orden, o.estado, o.fecha_creacion, o.total,
  ov.id_orden_venta, ov.fecha_entrega, ov.tipo_venta, ov.metodo_pago, ov.porcentaje_descuento,
  ov.id_cliente, c.dni AS dni_cliente, c.nombre AS nombre_cliente, c.apellido_paterno AS apellido_paterno_cliente, c.apellido_materno AS apellido_materno_cliente, c.puntos, c.puntos_retenidos, c.ruc, c.razon_social, c.direccion,
  ov.id_empleado, e.dni AS dni_empleado, e.nombre AS nombre_empleado, e.apellido_paterno AS apellido_paterno_empleado, e.apellido_materno AS apellido_materno_empleado, e.sueldo, e.rol,
  ov.id_repartidor, e2.dni AS dni_repartidor, e2.nombre AS nombre_repartidor, e2.apellido_paterno AS apellido_paterno_repartidor, e2.apellido_materno AS apellido_materno_repartidor, e2.sueldo AS sueldo_repartidor, e2.rol AS rol_repartidor,
  e.sueldo, e.rol,
  ov.fecha_entrega, ov.tipo_venta, ov.metodo_pago,
  p.id_producto, p.nombre AS nombre_producto, p.precio_unitario, p.stock, p.capacidad, p.unidad_medida, p.tipo, p.puntos,
  lo.cantidad, lo.subtotal
  FROM Orden o 
  JOIN Orden_Venta ov ON o.id_orden = ov.id_orden
  JOIN Cliente c ON ov.id_cliente = c.id_cliente
  JOIN Empleado e ON ov.id_empleado = e.id_empleado
  LEFT JOIN Empleado e2 ON ov.id_repartidor = e2.id_empleado
  LEFT JOIN LineaOrden lo ON o.id_orden = lo.id_orden
  LEFT JOIN Producto p ON lo.id_producto = p.id_producto
  WHERE (o.id_orden LIKE CONCAT('%', p_cadena, '%') OR
         ov.id_orden_venta LIKE CONCAT('%', p_cadena, '%'));
END$$

-- ----------------------------------------------
-- CRUD para Orden_Compra
-- ----------------------------------------------
CREATE PROCEDURE insertar_orden_compra(
  OUT p_id_orden_compra INT,
  OUT p_id_orden INT,
  IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
  IN p_fecha_creacion DATETIME,
  IN p_total DECIMAL(10, 2)
)
BEGIN
  CALL insertar_orden(p_id_orden, p_estado, p_fecha_creacion, p_total);
  INSERT INTO Orden_Compra(id_orden, fecha_recepcion)
  VALUES(p_id_orden, NULL);
  SET p_id_orden_compra = LAST_INSERT_ID();
END$$

CREATE PROCEDURE actualizar_orden_compra(
  IN p_id_orden_compra INT,
  IN p_id_orden INT,
  IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
  IN p_fecha_recepcion DATETIME,
  IN p_total DECIMAL(10, 2)
)
BEGIN
  CALL actualizar_orden(p_id_orden, p_estado, p_total);
  UPDATE Orden_Compra
  SET id_orden = p_id_orden,
      fecha_recepcion = p_fecha_recepcion
  WHERE id_orden_compra = p_id_orden_compra;
END$$

CREATE PROCEDURE listar_ordenes_compra(
  IN p_cadena VARCHAR(30)
)
BEGIN
  SELECT o.id_orden, o.estado, o.fecha_creacion, o.total,
  oc.id_orden_compra, oc.fecha_recepcion,
  p.id_producto, p.nombre, p.precio_unitario, p.stock, p.capacidad, p.unidad_medida, p.tipo, p.puntos,
  lo.cantidad, lo.subtotal
  FROM Orden o 
  JOIN Orden_Compra oc ON o.id_orden = oc.id_orden
  LEFT JOIN LineaOrden lo ON o.id_orden = lo.id_orden
  LEFT JOIN Producto p ON lo.id_producto = p.id_producto
  WHERE (o.id_orden LIKE CONCAT('%', p_cadena, '%') OR
         oc.id_orden_compra LIKE CONCAT('%', p_cadena, '%'));
END$$

-- ----------------------------------------------
-- CRUD para Comprobante
-- ----------------------------------------------
CREATE PROCEDURE insertar_comprobante(
  OUT p_id_comprobante INT,
  IN p_id_orden INT,
  IN p_tipo_comprobante ENUM('BoletaSimple', 'Factura'),
  IN p_fecha_emision DATETIME
)
BEGIN
  INSERT INTO Comprobante(id_orden, tipo_comprobante, fecha_emision)
  VALUES(p_id_orden, p_tipo_comprobante, p_fecha_emision);
  SET p_id_comprobante = LAST_INSERT_ID();
END$$

CREATE PROCEDURE actualizar_comprobante(
  IN p_id_comprobante INT,
  IN p_id_orden INT,
  IN p_tipo_comprobante ENUM('BoletaSimple', 'Factura'),
  IN p_fecha_emision DATETIME
)
BEGIN
  UPDATE Comprobante
  SET id_orden = p_id_orden,
      tipo_comprobante = p_tipo_comprobante,
      fecha_emision = p_fecha_emision
  WHERE id_comprobante = p_id_comprobante;
END$$

CREATE PROCEDURE eliminar_comprobante(
  IN p_id_comprobante INT
)
BEGIN
  DELETE FROM Comprobante
  WHERE id_comprobante = p_id_comprobante;
END$$

CREATE PROCEDURE listar_comprobantes(
  IN p_cadena VARCHAR(30)
)
BEGIN
  SELECT c.id_comprobante, c.tipo_comprobante, c.fecha_emision,
  o.id_orden, o.estado, o.fecha_creacion, o.total,
  ov.id_orden_venta, 
  ov.id_cliente, cl.dni as dni_cliente, cl.nombre as nombre_cliente, cl.apellido_paterno as apellido_paterno_cliente, cl.apellido_materno as apellido_materno_cliente, cl.puntos, cl.puntos_retenidos, cl.ruc, cl.razon_social, cl.direccion,
  ov.id_empleado, e.dni as dni_empleado, e.nombre as nombre_empleado, e.apellido_paterno as apellido_paterno_empleado, e.apellido_materno as apellido_materno_empleado, e.sueldo, e.rol,
  ov.id_repartidor, e2.dni as dni_repartidor, e2.nombre as nombre_repartidor, e2.apellido_paterno as apellido_paterno_repartidor, e2.apellido_materno as apellido_materno_repartidor, e2.sueldo as sueldo_repartidor, e2.rol as rol_repartidor,
  ov.fecha_entrega, ov.tipo_venta, ov.metodo_pago, ov.porcentaje_descuento,
  oc.id_orden_compra, oc.fecha_recepcion,
  lo.id_producto, p.nombre as nombre_producto, p.precio_unitario, p.stock, p.capacidad, p.unidad_medida, p.tipo, p.puntos, 
  lo.cantidad, lo.subtotal
  FROM Comprobante c
  LEFT JOIN Orden o ON o.id_orden = c.id_orden
  LEFT JOIN Orden_Venta ov ON o.id_orden = ov.id_orden
  LEFT JOIN Orden_Compra oc ON o.id_orden = oc.id_orden
  LEFT JOIN LineaOrden lo ON o.id_orden = lo.id_orden
  LEFT JOIN Producto p ON lo.id_producto = p.id_producto
  LEFT JOIN Cliente cl ON ov.id_cliente = cl.id_cliente
  LEFT JOIN Empleado e ON ov.id_empleado = e.id_empleado
  LEFT JOIN Empleado e2 ON ov.id_repartidor = e2.id_empleado
  WHERE c.id_orden IS NOT NULL
  AND (c.id_comprobante LIKE CONCAT('%', p_cadena, '%') );
END$$

-- ----------------------------------------------
-- CRUD para LineaOrden
-- ----------------------------------------------
CREATE PROCEDURE insertar_linea_orden(
  IN p_id_orden INT,
  IN p_id_producto INT,
  IN p_cantidad INT,
  IN p_subtotal DECIMAL(10, 2)
)
BEGIN
  INSERT INTO LineaOrden(id_orden, id_producto, cantidad, subtotal)
  VALUES(p_id_orden, p_id_producto, p_cantidad, p_subtotal);
END$$

CREATE PROCEDURE actualizar_linea_orden(
  IN p_id_orden INT,
  IN p_id_producto INT,
  IN p_cantidad INT,
  IN p_subtotal DECIMAL(10, 2)
)
BEGIN
  UPDATE LineaOrden
  SET cantidad = p_cantidad,
      subtotal = p_subtotal
  WHERE id_orden = p_id_orden AND id_producto = p_id_producto;
END$$

CREATE PROCEDURE listar_lineas_orden(
  IN p_id_orden INT
)
BEGIN
  SELECT lo.id_orden, lo.id_producto, 
  p.nombre, p.precio_unitario, p.stock, p.capacidad, p.unidad_medida, p.tipo, p.puntos,
  lo.cantidad, lo.subtotal
  FROM LineaOrden lo
  LEFT JOIN Orden_Venta ov ON lo.id_orden = ov.id_orden
  LEFT JOIN Orden_Compra oc ON lo.id_orden = oc.id_orden
  LEFT JOIN Producto p ON lo.id_producto = p.id_producto
  WHERE lo.id_orden = p_id_orden;
END$$

CREATE PROCEDURE eliminar_linea_orden(
  IN p_id_orden INT,
  IN p_id_producto INT
)
BEGIN
  DELETE FROM LineaOrden
  WHERE id_orden = p_id_orden AND id_producto = p_id_producto;
END$$

-- ==============================================
-- CUENTAS DE EMPLEADO Y CLIENTE
-- ==============================================

CREATE PROCEDURE listar_empleado_cuenta(
  IN p_cadena VARCHAR(30)
)
BEGIN
  SELECT e.id_empleado, e.dni, e.nombre, e.apellido_paterno, e.apellido_materno, e.sueldo, e.rol,
  c.id_cuenta, c.usuario, c.contrasena
  FROM Empleado e
  LEFT JOIN Cuenta_Empleado ce ON ce.id_empleado = e.id_empleado
  LEFT JOIN Cuenta c ON c.id_cuenta = ce.id_cuenta
  WHERE e.activo = 1
  AND (e.dni LIKE CONCAT('%', p_cadena, '%') OR
       e.nombre LIKE CONCAT('%', p_cadena, '%') OR
       e.apellido_paterno LIKE CONCAT('%', p_cadena, '%') OR
       e.apellido_materno LIKE CONCAT('%', p_cadena, '%') OR
       c.usuario LIKE CONCAT('%', p_cadena, '%')
       );
END$$

CREATE PROCEDURE listar_cliente_cuenta(
  IN p_cadena VARCHAR(30)
)
BEGIN
  SELECT cli.id_cliente, cli.dni, cli.nombre, cli.apellido_paterno, cli.apellido_materno, cli.direccion, cli.ruc, cli.razon_social, cli.puntos, cli.puntos_retenidos,
  c.id_cuenta, c.usuario, c.contrasena
  FROM Cliente cli
  LEFT JOIN Cuenta_Cliente cc ON cc.id_cliente = cli.id_cliente
  LEFT JOIN Cuenta c ON c.id_cuenta = cc.id_cuenta
  WHERE cli.activo = 1
  AND (cli.dni LIKE CONCAT('%', p_cadena, '%') OR
       cli.nombre LIKE CONCAT('%', p_cadena, '%') OR
       cli.apellido_paterno LIKE CONCAT('%', p_cadena, '%') OR
       cli.apellido_materno LIKE CONCAT('%', p_cadena, '%') OR
       cli.direccion LIKE CONCAT('%', p_cadena, '%') OR
       cli.ruc LIKE CONCAT('%', p_cadena, '%') OR
       cli.razon_social LIKE CONCAT('%', p_cadena, '%') OR
       c.usuario LIKE CONCAT('%', p_cadena, '%')
       );
END$$

DELIMITER ;