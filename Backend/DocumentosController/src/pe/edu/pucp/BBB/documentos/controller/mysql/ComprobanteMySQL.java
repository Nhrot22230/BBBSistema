/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.controller.mysql;

import java.sql.Timestamp;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;

import pe.edu.pucp.BBB.documentos.controller.dao.ComprobanteDAO;
import java.util.ArrayList;
import java.util.Date;
import pe.edu.pucp.BBB.documentos.model.Comprobante;
import pe.edu.pucp.BBB.documentos.model.EstadoOrden;
import pe.edu.pucp.BBB.documentos.model.LineaOrden;
import pe.edu.pucp.BBB.documentos.model.MetodoPago;
import pe.edu.pucp.BBB.documentos.model.Orden;
import pe.edu.pucp.BBB.documentos.model.OrdenCompra;
import pe.edu.pucp.BBB.documentos.model.OrdenVenta;
import pe.edu.pucp.BBB.documentos.model.TipoComprobante;
import pe.edu.pucp.BBB.documentos.model.TipoVenta;
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
public class ComprobanteMySQL implements ComprobanteDAO {

  private Connection con;
  private CallableStatement cs;
  private String sqlcmd;
  private ResultSet rs;

  /*
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
   */
  @Override
  public ArrayList<Comprobante> listar(String cadena) {
    ArrayList<Comprobante> comprobantes = new ArrayList<>();
    try {
      con = DBManager.getInstance().getConnection();
      sqlcmd = "{CALL listar_comprobantes(?) }";
      cs = con.prepareCall(sqlcmd);
      cs.setString("p_cadena", cadena);
      rs = cs.executeQuery();

      Comprobante prevComprobante = null;
      while (rs.next()) {
        Comprobante comprobante = new Comprobante();
        comprobante.setIdComprobanteNumerico(rs.getInt("id_comprobante"));
        comprobante.setIdComprobanteCadena("COM" + String.format("%05d", comprobante.getIdComprobanteNumerico()));
        comprobante.setTipoComprobante(TipoComprobante.valueOf(rs.getString("tipo_comprobante")));
        comprobante.setFechaEmision(rs.getDate("fecha_emision"));

        Orden orden;
        int id_orden = rs.getInt("id_orden");
        EstadoOrden estado = EstadoOrden.valueOf(rs.getString("estado"));
        Date fecha_creacion = rs.getDate("fecha_creacion");
        double total = rs.getDouble("total");

        int id_orv = rs.getInt("id_orden_venta");
        if (id_orv != 0) {
          Cliente cliente = new Cliente();
          cliente.setIdNumerico(rs.getInt("id_cliente"));
          cliente.setIdCadena("CLI" + String.format("%05d", cliente.getIdNumerico()));
          cliente.setDNI(rs.getString("dni_cliente"));
          cliente.setNombre(rs.getString("nombre_cliente"));
          cliente.setApellidoPaterno(rs.getString("apellido_paterno_cliente"));
          cliente.setApellidoMaterno(rs.getString("apellido_materno_cliente"));
          cliente.setRUC(rs.getString("ruc"));
          cliente.setRazonSocial(rs.getString("razon_social"));
          cliente.setDireccion(rs.getString("direccion"));

          Empleado empleado = new Empleado();
          empleado.setIdEmpleadoNumerico(rs.getInt("id_empleado"));
          empleado.setIdEmpleadoCadena("EMP" + String.format("%05d", empleado.getIdEmpleadoNumerico()));
          empleado.setDNI(rs.getString("dni_empleado"));
          empleado.setNombre(rs.getString("nombre_empleado"));
          empleado.setApellidoPaterno(rs.getString("apellido_paterno_empleado"));
          empleado.setApellidoMaterno(rs.getString("apellido_materno_empleado"));
          empleado.setSueldo(rs.getDouble("sueldo"));
          empleado.setRol(Rol.valueOf(rs.getString("rol")));

          Empleado repartidor = null;
          int id_repartidor = rs.getInt("id_repartidor");
          if (id_repartidor != 0) {
            repartidor = new Empleado();
            repartidor.setIdEmpleadoNumerico(rs.getInt("id_repartidor"));
            repartidor.setIdEmpleadoCadena("EMP" + String.format("%05d", repartidor.getIdEmpleadoNumerico()));
            repartidor.setDNI(rs.getString("dni_repartidor"));
            repartidor.setNombre(rs.getString("nombre_repartidor"));
            repartidor.setApellidoPaterno(rs.getString("apellido_paterno_repartidor"));
            repartidor.setApellidoMaterno(rs.getString("apellido_materno_repartidor"));
            repartidor.setSueldo(rs.getDouble("sueldo_repartidor"));
            repartidor.setRol(Rol.valueOf(rs.getString("rol_repartidor")));
          }

          OrdenVenta ordenVenta = new OrdenVenta();
          try {
            Date fecha_entrega = rs.getDate("fecha_entrega");
            ordenVenta.setFechaEntrega(fecha_entrega);
          } catch (Exception ex) {
            ordenVenta.setFechaEntrega(new Date());
          }

          TipoVenta tipo_venta = TipoVenta.valueOf(rs.getString("tipo_venta"));
          MetodoPago metodo_pago = MetodoPago.valueOf(rs.getString("metodo_pago"));
          double porcentaje_descuento = rs.getDouble("porcentaje_descuento");

          ordenVenta.setIdOrdenVentaNumerico(id_orv);
          ordenVenta.setIdOrdenVentaCadena("ORV" + String.format("%05d", ordenVenta.getIdOrdenVentaNumerico()));
          ordenVenta.setIdOrden(id_orden);
          ordenVenta.setCliente(cliente);
          ordenVenta.setEncargadoVenta(empleado);
          ordenVenta.setRepartidor(repartidor);
          ordenVenta.setTipoVenta(tipo_venta);
          ordenVenta.setMetodoPago(metodo_pago);
          ordenVenta.setPorcentajeDescuento(porcentaje_descuento);
          orden = ordenVenta;
        } else {
          int id_orc = rs.getInt("id_orden_compra");
          OrdenCompra ordenCompra = new OrdenCompra();
          try {
            Date fecha_recepcion = rs.getDate("fecha_recepcion");
            ordenCompra.setFechaRecepcion(fecha_recepcion);
          } catch (Exception ex) {
            ordenCompra.setFechaRecepcion(new Date());
            ordenCompra.getFechaRecepcion().toString();
          }
          ordenCompra.setIdOrden(id_orden);
          ordenCompra.setIdOrdenCompraNumerico(id_orc);
          ordenCompra.setIdOrdenCompraCadena("ORC" + String.format("%05d", ordenCompra.getIdOrdenCompraNumerico()));
          orden = ordenCompra;
        }
        orden.setIdOrden(id_orden);
        orden.setEstado(estado);
        orden.setFechaCreacion(fecha_creacion);
        orden.setTotal(total);

        LineaOrden lineaOrden = new LineaOrden();
        Producto producto = new Producto();
        producto.setIdProductoNumerico(rs.getInt("id_producto"));
        producto.setIdProductoCadena("PRO" + String.format("%05d", producto.getIdProductoNumerico()));
        producto.setNombre(rs.getString("nombre_producto"));
        producto.setPrecioUnitario(rs.getDouble("precio_unitario"));
        producto.setStock(rs.getInt("stock"));
        producto.setCapacidad(rs.getDouble("capacidad"));
        producto.setUnidadDeMedida(UnidadMedida.valueOf(rs.getString("unidad_medida")));
        producto.setTipo(TipoProducto.valueOf(rs.getString("tipo")));
        producto.setPuntos(rs.getInt("puntos"));
        lineaOrden.setProducto(producto);
        lineaOrden.setCantidad(rs.getInt("cantidad"));
        lineaOrden.setSubtotal(rs.getDouble("subtotal"));

        if (prevComprobante != null
          && prevComprobante.getIdComprobanteNumerico() == comprobante.getIdComprobanteNumerico()) {
          prevComprobante.getOrdenAsociada().agregarLineaOrden(lineaOrden);
        } else {
          if (prevComprobante != null) {
            comprobantes.add(prevComprobante);
          }
          prevComprobante = comprobante;
          if (prevComprobante.getOrdenAsociada() == null) {
            prevComprobante.setOrdenAsociada(orden);
          }
          prevComprobante.getOrdenAsociada().agregarLineaOrden(lineaOrden);
        }
      }
      if (prevComprobante != null) {
        comprobantes.add(prevComprobante);
      }
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (con != null) {
          con.close();
        }
        if (cs != null) {
          cs.close();
        }
        if (rs != null) {
          rs.close();
        }
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return comprobantes;
  }

  /*
   * CREATE PROCEDURE insertar_comprobante(
   * OUT p_id_comprobante INT,
   * IN p_id_orden INT,
   * IN p_tipo_comprobante ENUM('Boleta', 'Factura'),
   * IN p_fecha_emision DATETIME
   * )
   * BEGIN
   * INSERT INTO Comprobante(id_orden, tipo_comprobante, fecha_emision)
   * VALUES(p_id_orden, p_tipo_comprobante, p_fecha_emision);
   * SET p_id_comprobante = LAST_INSERT_ID();
   * END$$
   */
  @Override
  public int insertar(Comprobante comprobante) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sqlcmd = "{CALL insertar_comprobante(?,?,?,?) }";
      cs = con.prepareCall(sqlcmd);
      cs.registerOutParameter("p_id_comprobante", java.sql.Types.INTEGER);
      cs.setInt("p_id_orden", comprobante.getOrdenAsociada().getIdOrden());
      cs.setString("p_tipo_comprobante", comprobante.getTipoComprobante().toString());
      cs.setTimestamp("p_fecha_emision", new Timestamp(comprobante.getFechaEmision().getTime()));
      cs.executeUpdate();
      comprobante.setIdComprobanteNumerico(cs.getInt("p_id_comprobante"));
      comprobante.setIdComprobanteCadena("COM" + String.format("%05d", comprobante.getIdComprobanteNumerico()));
      resultado = 1;
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (con != null) {
          con.close();
        }
        if (cs != null) {
          cs.close();
        }
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return resultado;
  }

  /*
   * CREATE PROCEDURE eliminar_comprobante(
   * IN p_id_comprobante INT
   * )
   * BEGIN
   * DELETE FROM Comprobante
   * WHERE id_comprobante = p_id_comprobante;
   * END$$
   */
  @Override
  public int eliminar(int idComprobante) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sqlcmd = "{CALL eliminar_comprobante(?) }";
      cs = con.prepareCall(sqlcmd);
      cs.setInt("p_id_comprobante", idComprobante);
      cs.executeUpdate();
      resultado = 1;
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (con != null) {
          con.close();
        }
        if (cs != null) {
          cs.close();
        }
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return resultado;
  }

  /*
   * CREATE PROCEDURE actualizar_comprobante(
   * IN p_id_comprobante INT,
   * IN p_id_orden INT,
   * IN p_tipo_comprobante ENUM('Boleta', 'Factura'),
   * IN p_fecha_emision DATETIME
   * )
   * BEGIN
   * UPDATE Comprobante
   * SET id_orden = p_id_orden,
   * tipo_comprobante = p_tipo_comprobante,
   * fecha_emision = p_fecha_emision
   * WHERE id_comprobante = p_id_comprobante;
   * END$$
   */
  @Override
  public int modificar(Comprobante comprobante) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sqlcmd = "{CALL actualizar_comprobante(?,?,?,?) }";
      cs = con.prepareCall(sqlcmd);
      cs.setInt("p_id_comprobante", comprobante.getIdComprobanteNumerico());
      cs.setInt("p_id_orden", comprobante.getOrdenAsociada().getIdOrden());
      cs.setString("p_tipo_comprobante", comprobante.getTipoComprobante().toString());
      cs.setTimestamp("p_fecha_emision", new Timestamp(comprobante.getFechaEmision().getTime()));
      cs.executeUpdate();
      resultado = 1;
    } catch (SQLException ex) {
      System.out.println(ex.getMessage());
    } finally {
      try {
        if (con != null) {
          con.close();
        }
        if (cs != null) {
          cs.close();
        }
      } catch (SQLException ex) {
        System.out.println(ex.getMessage());
      }
    }
    return resultado;
  }
}
