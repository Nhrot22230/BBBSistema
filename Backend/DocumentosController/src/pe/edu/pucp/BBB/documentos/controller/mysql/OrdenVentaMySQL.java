/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.controller.mysql;

import java.sql.Timestamp;
import java.sql.Connection;
import java.sql.CallableStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import pe.edu.pucp.BBB.documentos.controller.dao.OrdenVentaDAO;
import pe.edu.pucp.BBB.documentos.model.EstadoOrden;
import pe.edu.pucp.BBB.documentos.model.OrdenVenta;
import pe.edu.pucp.BBB.documentos.model.TipoVenta;
import pe.edu.pucp.BBB.documentos.model.MetodoPago;
import pe.edu.pucp.BBB.documentos.model.LineaOrden;
import pe.edu.pucp.BBB.personas.model.Cliente;
import pe.edu.pucp.BBB.personas.model.Empleado;
import pe.edu.pucp.BBB.personas.model.Rol;
import pe.edu.pucp.BBB.productos.model.Producto;
import pe.edu.pucp.BBB.productos.model.TipoProducto;
import pe.edu.pucp.BBB.productos.model.UnidadMedida;
import pe.edu.pucp.BBB.utils.database.DBManager;

/**
 *
 * @author Candi
 */
public class OrdenVentaMySQL implements OrdenVentaDAO {

    private Connection con;
    private CallableStatement cs;
    private String sql;
    private ResultSet rs;

    private LineaOrdenMySQL lineaOrdenMySQL;

    public OrdenVentaMySQL() {
        lineaOrdenMySQL = new LineaOrdenMySQL();
    }

    /*
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
   */
  @Override
  public ArrayList<OrdenVenta> listar(String cadena) {
    ArrayList<OrdenVenta> ordenes = new ArrayList<>();
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL listar_ordenes_venta(?)}";
      cs = con.prepareCall(sql);
      cs.setString("p_cadena", cadena);
      rs = cs.executeQuery();

      OrdenVenta prevORV = null;
      while (rs.next()) {
        int id_orden = rs.getInt("id_orden");
        if (prevORV != null && prevORV.getIdOrden() != id_orden) {
          ordenes.add(prevORV);
          prevORV = null;
        }

        OrdenVenta current = new OrdenVenta();
        current.setIdOrden(id_orden);
        current.setIdOrdenVentaNumerico(rs.getInt("id_orden_venta"));
        current.setIdOrdenVentaCadena("ORV" + String.format("%05d", current.getIdOrdenVentaNumerico()));
        current.setEstado(EstadoOrden.valueOf(rs.getString("estado")));
        current.setFechaCreacion(rs.getDate("fecha_creacion"));
        current.setTotal(rs.getDouble("total"));
        
        try{
          current.setFechaEntrega(rs.getDate("fecha_entrega"));
        }
        catch(Exception ex){
          current.setFechaEntrega(null);
        }
        current.setTipoVenta(TipoVenta.valueOf(rs.getString("tipo_venta")));
        current.setMetodoPago(MetodoPago.valueOf(rs.getString("metodo_pago")));
        current.setPorcentajeDescuento(rs.getDouble("porcentaje_descuento"));

        Cliente cliente = new Cliente();
        cliente.setIdNumerico(rs.getInt("id_cliente"));
        cliente.setIdCadena("CLI" + String.format("%05d", cliente.getIdNumerico()));
        cliente.setDNI(rs.getString("dni_cliente"));
        cliente.setNombre(rs.getString("nombre_cliente"));
        cliente.setApellidoPaterno(rs.getString("apellido_paterno_cliente"));
        cliente.setApellidoMaterno(rs.getString("apellido_materno_cliente"));
        cliente.setPuntos(rs.getInt("puntos"));
        cliente.setPuntosRetenidos(rs.getInt("puntos_retenidos"));
        cliente.setRUC(rs.getString("ruc"));
        cliente.setRazonSocial(rs.getString("razon_social"));
        cliente.setDireccion(rs.getString("direccion"));

        current.setCliente(cliente);

        Empleado encargadoVenta = new Empleado();
        encargadoVenta.setIdEmpleadoNumerico(rs.getInt("id_empleado"));
        encargadoVenta.setIdEmpleadoCadena("EMP" + String.format("%05d", encargadoVenta.getIdEmpleadoNumerico()));
        encargadoVenta.setDNI(rs.getString("dni_empleado"));
        encargadoVenta.setNombre(rs.getString("nombre_empleado"));
        encargadoVenta.setApellidoPaterno(rs.getString("apellido_paterno_empleado"));
        encargadoVenta.setApellidoMaterno(rs.getString("apellido_materno_empleado"));
        encargadoVenta.setSueldo(rs.getDouble("sueldo"));
        encargadoVenta.setRol(Rol.valueOf(rs.getString("rol")));

        current.setEncargadoVenta(encargadoVenta);

        int id_repartidor = rs.getInt("id_repartidor");
        Empleado repartidor = null;
        if (id_repartidor != 0) {
          repartidor = new Empleado();
          repartidor.setIdEmpleadoNumerico(id_repartidor);
          repartidor.setIdEmpleadoCadena("EMP" + String.format("%05d", repartidor.getIdEmpleadoNumerico()));
          repartidor.setDNI(rs.getString("dni_repartidor"));
          repartidor.setNombre(rs.getString("nombre_repartidor"));
          repartidor.setApellidoPaterno(rs.getString("apellido_paterno_repartidor"));
          repartidor.setApellidoMaterno(rs.getString("apellido_materno_repartidor"));
          repartidor.setSueldo(rs.getDouble("sueldo_repartidor"));
          repartidor.setRol(Rol.valueOf(rs.getString("rol_repartidor")));
        }
        current.setRepartidor(repartidor);

        int idProducto = rs.getInt("id_producto");
        if (idProducto == 0) {
          ordenes.add(current);
          prevORV = null;
          continue;
        }

        Producto producto = new Producto();
        producto.setIdProductoNumerico(idProducto);
        producto.setIdProductoCadena("PRO" + String.format("%05d", producto.getIdProductoNumerico()));
        producto.setNombre(rs.getString("nombre_producto"));
        producto.setPrecioUnitario(rs.getDouble("precio_unitario"));
        producto.setStock(rs.getInt("stock"));
        producto.setCapacidad(rs.getDouble("capacidad"));
        producto.setUnidadDeMedida(UnidadMedida.valueOf(rs.getString("unidad_medida")));
        producto.setTipo(TipoProducto.valueOf(rs.getString("tipo")));
        producto.setPuntos(rs.getInt("puntos"));

        LineaOrden lineaOrden = new LineaOrden();
        lineaOrden.setProducto(producto);
        lineaOrden.setCantidad(rs.getInt("cantidad"));
        lineaOrden.setSubtotal(rs.getDouble("subtotal"));

        if (prevORV == null) {
          prevORV = current;
        }
        prevORV.agregarLineaOrden(lineaOrden);

        if (rs.isLast()) {
          ordenes.add(prevORV);
        }
      }
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (rs != null) {
          rs.close();
        }
        if (cs != null) {
          cs.close();
        }
        if (con != null) {
          con.close();
        }
      } catch (SQLException ex) {
        System.err.println(ex.getMessage());
      }
    }
    
    return ordenes;
  }
    /*
   * CREATE PROCEDURE insertar_orden_venta(
   * OUT p_id_orden_venta INT,
   * OUT p_id_orden INT,
   * IN p_id_cliente INT,
   * IN p_id_empleado INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_creacion DATETIME,
   * IN p_tipo_venta ENUM('Presencial', 'Delivery'),
   * IN p_metodo_pago ENUM('Efectivo', 'Tarjeta'),
   * IN p_porcentaje_descuento DECIMAL(4,2),
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL insertar_orden(p_id_orden, p_estado, p_fecha_creacion, p_total);
   * INSERT INTO Orden_Venta(id_orden, id_cliente, id_empleado, tipo_venta,
   * metodo_pago, porcentaje_descuento)
   * VALUES(p_id_orden, p_id_cliente, p_id_empleado, p_tipo_venta, p_metodo_pago,
   * p_porcentaje_descuento);
   * SET p_id_orden_venta = LAST_INSERT_ID();
   * END$$
     */

    @Override
    public int insertar(OrdenVenta ordenVenta) {
        int resultado = 0;
        try {
            con = DBManager.getInstance().getConnection();
            sql = "{CALL insertar_orden_venta(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)}"; // El nuevo procedimiento unificado
            cs = con.prepareCall(sql);

            cs.registerOutParameter("p_id_orden_venta", java.sql.Types.INTEGER);
            cs.registerOutParameter("p_id_orden", java.sql.Types.INTEGER);
            cs.setInt("p_id_cliente", ordenVenta.getCliente().getIdNumerico());
            cs.setInt("p_id_empleado", ordenVenta.getEncargadoVenta().getIdEmpleadoNumerico());
            cs.setString("p_estado", ordenVenta.getEstado().toString());
            cs.setTimestamp("p_fecha_creacion", new Timestamp(ordenVenta.getFechaCreacion().getTime()));
            cs.setString("p_tipo_venta", ordenVenta.getTipoVenta().toString());
            cs.setString("p_metodo_pago", ordenVenta.getMetodoPago().toString());
            cs.setDouble("p_porcentaje_descuento", ordenVenta.getPorcentajeDescuento());
            cs.setDouble("p_total", ordenVenta.getTotal());

            if (ordenVenta.getRepartidor() != null) {
                cs.setInt("p_id_repartidor", ordenVenta.getRepartidor().getIdEmpleadoNumerico());
            } else {
                cs.setNull("p_id_repartidor", java.sql.Types.INTEGER);
            }

            cs.executeUpdate();
            ordenVenta.setIdOrdenVentaNumerico(cs.getInt("p_id_orden_venta"));
            ordenVenta.setIdOrdenVentaCadena("OV" + String.format("%05d", ordenVenta.getIdOrdenVentaNumerico()));
            ordenVenta.setIdOrden(cs.getInt("p_id_orden"));
            resultado = ordenVenta.getIdOrdenVentaNumerico();
        } catch (SQLException ex) {
            System.out.println(ex.getMessage());
        } finally {
            try {
                if (rs != null) rs.close();
                if (cs != null) cs.close();
                if (con != null) con.close();
            } catch (SQLException ex) {
                System.err.println(ex.getMessage());
            }
        }

        for (LineaOrden lineaOrden : ordenVenta.getLineasOrden()) {
            lineaOrdenMySQL.insertar(lineaOrden, ordenVenta.getIdOrden());
        }
        return resultado;
    }


    /*
   * CREATE PROCEDURE actualizar_orden_venta(
   * IN p_id_orden_venta INT,
   * IN p_id_orden INT,
   * IN p_id_cliente INT,
   * IN p_id_empleado INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_entrega DATETIME,
   * IN p_tipo_venta ENUM('Presencial', 'Delivery'),
   * IN p_metodo_pago ENUM('Efectivo', 'Tarjeta'),
   * IN p_porcentaje_descuento DECIMAL(4,2),
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL actualizar_orden(p_id_orden, p_estado);
   * UPDATE Orden_Venta
   * SET id_orden = p_id_orden,
   * id_cliente = p_id_cliente,
   * id_empleado = p_id_empleado,
   * fecha_entrega = p_fecha_entrega,
   * tipo_venta = p_tipo_venta,
   * metodo_pago = p_metodo_pago,
   * porcentaje_descuento = p_porcentaje_descuento,
   * total = p_total
   * WHERE id_orden_venta = p_id_orden_venta;
   * END$$
     */
    @Override
    public int modificar(OrdenVenta ordenVenta) {
        int resultado = 0;
        try {
            con = DBManager.getInstance().getConnection();
            sql = "{CALL actualizar_orden_venta(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)}"; // El nuevo procedimiento simplificado
            cs = con.prepareCall(sql);

            cs.setInt("p_id_orden_venta", ordenVenta.getIdOrdenVentaNumerico());
            cs.setInt("p_id_orden", ordenVenta.getIdOrden());
            cs.setInt("p_id_cliente", ordenVenta.getCliente().getIdNumerico());
            cs.setInt("p_id_empleado", ordenVenta.getEncargadoVenta().getIdEmpleadoNumerico());
            cs.setString("p_estado", ordenVenta.getEstado().toString());
            cs.setTimestamp("p_fecha_entrega", new Timestamp(ordenVenta.getFechaEntrega().getTime()));
            cs.setString("p_tipo_venta", ordenVenta.getTipoVenta().toString());
            cs.setString("p_metodo_pago", ordenVenta.getMetodoPago().toString());
            cs.setDouble("p_porcentaje_descuento", ordenVenta.getPorcentajeDescuento());
            cs.setDouble("p_total", ordenVenta.getTotal());

            // Usar 0 como valor especial para el parámetro opcional id_repartidor
            if (ordenVenta.getRepartidor() != null) {
                cs.setInt("p_id_repartidor", ordenVenta.getRepartidor().getIdEmpleadoNumerico());
            } else {
                cs.setInt("p_id_repartidor", 0);
            }

            cs.executeUpdate();
            resultado = ordenVenta.getIdOrdenVentaNumerico();
        } catch (SQLException ex) {
            System.out.println(ex.getMessage());
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (cs != null) {
                    cs.close();
                }
                if (con != null) {
                    con.close();
                }
            } catch (SQLException ex) {
                System.err.println(ex.getMessage());
            }
        }
        return resultado;
    }

}
