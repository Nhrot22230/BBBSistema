/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.controller.mysql;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import pe.edu.pucp.BBB.documentos.controller.dao.LineaOrdenDAO;
import pe.edu.pucp.BBB.documentos.controller.dao.OrdenCompraDAO;
import pe.edu.pucp.BBB.documentos.model.EstadoOrden;
import pe.edu.pucp.BBB.utils.database.DBManager;
import pe.edu.pucp.BBB.documentos.model.LineaOrden;
import pe.edu.pucp.BBB.documentos.model.OrdenCompra;
import pe.edu.pucp.BBB.productos.model.Producto;
import pe.edu.pucp.BBB.productos.model.TipoProducto;
import pe.edu.pucp.BBB.productos.model.UnidadMedida;

public class OrdenCompraMySQL implements OrdenCompraDAO {

  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;
  private LineaOrdenDAO lineaOrdenDAO;

  public OrdenCompraMySQL() {
    lineaOrdenDAO = new LineaOrdenMySQL();
  }

  /*
   * CREATE PROCEDURE listar_ordenes_compra(
   * IN p_cadena VARCHAR(30)
   * )
   * BEGIN
   * SELECT o.id_orden, o.estado, o.fecha_creacion, o.total,
   * oc.id_orden_compra, oc.fecha_recepcion,
   * p.id_producto, p.nombre, p.precio_unitario, p.stock, p.capacidad,
   * p.unidad_medida, p.tipo, p.puntos,
   * lo.cantidad, lo.subtotal
   * FROM Orden o
   * JOIN Orden_Compra oc ON o.id_orden = oc.id_orden
   * JOIN LineaOrden lo ON o.id_orden = lo.id_orden
   * JOIN Producto p ON lo.id_producto = p.id_producto
   * WHERE (o.id_orden LIKE CONCAT('%', p_cadena, '%') OR
   * oc.id_orden_compra LIKE CONCAT('%', p_cadena, '%'));
   * END$$
   */
  @Override
  public ArrayList<OrdenCompra> listar(String cadena) {
    ArrayList<OrdenCompra> ordenCompras = new ArrayList<>();

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL listar_ordenes_compra(?)}";
      cs = con.prepareCall(sql);
      cs.setString(1, cadena);
      rs = cs.executeQuery();
      OrdenCompra prevORC = null;
      while (rs.next()) {
        // Casos a considerar:
        // 1. Si prevORC es null, entonces es la primera vez que se lee una orden de compra.
        // 2. Si prevORC no es null, entonces se está guardando una orden de compra previa.
        // 3. Si prevORC no es null y el id de la orden de compra actual es diferente al id de la orden de compra previa, entonces se está leyendo una nueva orden de compra.
        // 4. Si prevORC no es null y el id de la orden de compra actual es igual al id de la orden de compra previa, entonces se está leyendo una linea de orden de la orden de compra previa.
        // 5. Si es la última iteración...
        
        int id_orden = rs.getInt("id_orden");
        if (prevORC != null && prevORC.getIdOrden() != id_orden) {
          ordenCompras.add(prevORC);
          prevORC = null;
        }

        OrdenCompra current = new OrdenCompra();
        current.setIdOrden(id_orden);
        current.setEstado(EstadoOrden.valueOf(rs.getString("estado")));
        current.setFechaCreacion(rs.getTimestamp("fecha_creacion"));
        current.setTotal(rs.getDouble("total"));
        current.setIdOrdenCompraNumerico(rs.getInt("id_orden_compra"));
        current.setIdOrdenCompraCadena("ORC" + String.format("%05d", current.getIdOrdenCompraNumerico()));
        try {
          current.setFechaRecepcion(rs.getTimestamp("fecha_recepcion"));
        } catch (Exception ex) {
          current.setFechaRecepcion(null);
        }

        // verificar si tiene lineas de orden
        int idProducto = rs.getInt("id_producto");
        if (idProducto == 0) {
          // no tiene lineas de orden
          ordenCompras.add(current);
          prevORC = null;
          continue;
        }

        Producto producto = new Producto();
        producto.setIdProductoNumerico(idProducto);
        producto.setNombre(rs.getString("nombre"));
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

        if (prevORC == null) {
          prevORC = current;
        }
        prevORC.agregarLineaOrden(lineaOrden);

        if (rs.isLast()) {
          ordenCompras.add(prevORC);
        }
      }
    } catch (Exception ex) {
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

    return ordenCompras;
  }

  /*
   * CREATE PROCEDURE insertar_orden_compra(
   * OUT p_id_orden_compra INT,
   * OUT p_id_orden INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_creacion DATETIME,
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL insertar_orden(p_id_orden, p_estado, p_fecha_creacion, p_total);
   * INSERT INTO Orden_Compra(id_orden, fecha_recepcion)
   * VALUES(p_id_orden, NULL);
   * SET p_id_orden_compra = LAST_INSERT_ID();
   * END$$
   */
  @Override
  public int insertar(OrdenCompra ordenCompra) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL insertar_orden_compra(?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.registerOutParameter("p_id_orden_compra", java.sql.Types.INTEGER);
      cs.registerOutParameter("p_id_orden", java.sql.Types.INTEGER);
      cs.setString("p_estado", ordenCompra.getEstado().toString());
      cs.setTimestamp("p_fecha_creacion", new java.sql.Timestamp(ordenCompra.getFechaCreacion().getTime()));
      cs.setDouble("p_total", ordenCompra.getTotal());
      cs.executeUpdate();
      ordenCompra.setIdOrdenCompraNumerico(cs.getInt("p_id_orden_compra"));
      ordenCompra.setIdOrdenCompraCadena("OC" + String.format("%05d", ordenCompra.getIdOrdenCompraNumerico()));
      ordenCompra.setIdOrden(cs.getInt("p_id_orden"));
      resultado = ordenCompra.getIdOrdenCompraNumerico();
      
      for (LineaOrden lineaOrden : ordenCompra.getLineasOrden()) {
      lineaOrdenDAO.insertar(lineaOrden, ordenCompra.getIdOrden());
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

    
    return resultado;
  }

  /*
   * CREATE PROCEDURE actualizar_orden_compra(
   * IN p_id_orden_compra INT,
   * IN p_id_orden INT,
   * IN p_estado ENUM('Pendiente', 'Entregado', 'Cancelado'),
   * IN p_fecha_recepcion DATETIME,
   * IN p_total DECIMAL(10, 2)
   * )
   * BEGIN
   * CALL actualizar_orden(p_id_orden, p_estado, p_total);
   * UPDATE Orden_Compra
   * SET id_orden = p_id_orden,
   * fecha_recepcion = p_fecha_recepcion
   * WHERE id_orden_compra = p_id_orden_compra;
   * END$$
   */
  @Override
  public int modificar(OrdenCompra ordenCompra) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL actualizar_orden_compra(?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_orden_compra", ordenCompra.getIdOrdenCompraNumerico());
      cs.setInt("p_id_orden", ordenCompra.getIdOrden());
      cs.setString("p_estado", ordenCompra.getEstado().toString());
      cs.setTimestamp("p_fecha_recepcion", new java.sql.Timestamp(ordenCompra.getFechaRecepcion().getTime()));
      cs.setDouble("p_total", ordenCompra.getTotal());
      cs.executeUpdate();
      resultado = 1;
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
