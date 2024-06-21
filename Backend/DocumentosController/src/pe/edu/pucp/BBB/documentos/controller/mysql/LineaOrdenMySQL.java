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

import pe.edu.pucp.BBB.utils.database.DBManager;
import pe.edu.pucp.BBB.documentos.controller.dao.LineaOrdenDAO;
import pe.edu.pucp.BBB.documentos.model.LineaOrden;
import pe.edu.pucp.BBB.productos.model.Producto;
import pe.edu.pucp.BBB.productos.model.TipoProducto;
import pe.edu.pucp.BBB.productos.model.UnidadMedida;

/**
 *
 * @author Candi
 */
public class LineaOrdenMySQL implements LineaOrdenDAO {

  Connection con;
  String sql;
  CallableStatement cs;
  ResultSet rs;

  /*
   * CREATE PROCEDURE insertar_linea_orden(
   * IN p_id_orden INT,
   * IN p_id_producto INT,
   * IN p_cantidad INT,
   * IN p_subtotal DECIMAL(10, 2)
   * )
   * BEGIN
   * INSERT INTO LineaOrden(id_orden, id_producto, cantidad, subtotal)
   * VALUES(p_id_orden, p_id_producto, p_cantidad, p_subtotal);
   * END$$
   */
  @Override
  public int insertar(LineaOrden lineaOrden, int id_orden) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL insertar_linea_orden(?, ?, ?, ?) }";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_orden", id_orden);
      cs.setInt("p_id_producto", lineaOrden.getProducto().getIdProductoNumerico());
      cs.setInt("p_cantidad", lineaOrden.getCantidad());
      cs.setDouble("p_subtotal", lineaOrden.getSubtotal());
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

  /*
   * CREATE PROCEDURE listar_lineas_orden(
   * IN p_id_orden INT
   * )
   * BEGIN
   * SELECT lo.id_orden, lo.id_producto,
   * p.nombre, p.precio_unitario, p.stock, p.capacidad, p.unidad_medida, p.tipo,
   * p.puntos,
   * lo.cantidad, lo.subtotal,
   * FROM LineaOrden lo
   * LEFT JOIN Orden_Venta ov ON lo.id_orden = ov.id_orden
   * LEFT JOIN Orden_Compra oc ON lo.id_orden = oc.id_orden
   * LEFT JOIN Producto p ON lo.id_producto = p.id_producto
   * WHERE lo.id_orden = p_id_orden;
   * END$$
   * 
   */
  @Override
  public ArrayList<LineaOrden> listar(int id_orden) {
    ArrayList<LineaOrden> lineasOrden = new ArrayList<>();
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL listar_lineas_orden(?) }";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_orden", id_orden);
      rs = cs.executeQuery();
      while (rs.next()) {
        LineaOrden lo = new LineaOrden();

        Producto prod = new Producto();
        prod.setIdProductoNumerico(rs.getInt("id_producto"));
        prod.setIdProductoCadena("PRO" + String.format("%05d", prod.getIdProductoNumerico()));
        prod.setNombre(rs.getString("nombre"));
        prod.setPrecioUnitario(rs.getDouble("precio_unitario"));
        prod.setStock(rs.getInt("stock"));
        prod.setCapacidad(rs.getDouble("capacidad"));
        prod.setUnidadDeMedida(UnidadMedida.valueOf(rs.getString("unidad_medida")));
        prod.setTipo(TipoProducto.valueOf(rs.getString("tipo")));
        prod.setPuntos(rs.getInt("puntos"));

        lo.setCantidad(rs.getInt("cantidad"));
        lo.setSubtotal(rs.getDouble("subtotal"));
        lo.setProducto(prod);

        lineasOrden.add(lo);
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

    return lineasOrden;
  }

    @Override
    public int eliminar(int id_orden, int id_producto) {
        
        
       int  resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL eliminar_linea_orden (?, ?) }";
      cs = con.prepareCall(sql);
      cs.setInt("p_id_orden", id_orden);
      cs.setInt("p_id_producto", id_producto);
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
