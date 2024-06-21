/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.productos.controller.mysql;

import pe.edu.pucp.BBB.utils.database.DBManager;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import pe.edu.pucp.BBB.productos.controller.dao.ProductoDAO;
import pe.edu.pucp.BBB.productos.model.Producto;
import pe.edu.pucp.BBB.productos.model.TipoProducto;
import pe.edu.pucp.BBB.productos.model.UnidadMedida;

/**
 *
 * @author Candi
 */
public class ProductoMySQL implements ProductoDAO {

  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;

  @Override
  public ArrayList<Producto> listar(String filtro) {
    ArrayList<Producto> lista = new ArrayList<>();
    try {

      con = DBManager.getInstance().getConnection();
      sql = "{ CALL listar_x_id_y_nombre(?) }";
      cs = con.prepareCall(sql);
      cs.setString("p_cadena", filtro);
      rs = cs.executeQuery();

      while (rs.next()) {
        Producto producto = new Producto();
        producto.setIdProductoNumerico(rs.getInt("id_producto"));
        producto.setIdProductoCadena("PRO" + String.format("%05d", rs.getInt("id_producto")));
        producto.setNombre(rs.getString("nombre"));
        producto.setStock(rs.getInt("stock"));
        producto.setCapacidad(rs.getDouble("capacidad"));
        producto.setUnidadDeMedida(UnidadMedida.valueOf(rs.getString("unidad_medida")));
        producto.setTipo(TipoProducto.valueOf(rs.getString("tipo")));
        producto.setPrecioUnitario(rs.getDouble("precio_unitario"));
        producto.setPuntos(rs.getInt("puntos"));
        lista.add(producto);
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
    return lista;
  }

  @Override
  public ArrayList<Producto> listar_todos() {
    ArrayList<Producto> lista = new ArrayList<>();
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL listar_productos() }";
      cs = con.prepareCall(sql);
      rs = cs.executeQuery();

      while (rs.next()) {
        Producto producto = new Producto();
        producto.setIdProductoNumerico(rs.getInt("id_producto"));
        producto.setIdProductoCadena("PRO" + String.format("%05d", rs.getInt("id_producto")));
        producto.setNombre(rs.getString("nombre"));
        producto.setStock(rs.getInt("stock"));
        producto.setCapacidad(rs.getDouble("capacidad"));
        producto.setUnidadDeMedida(UnidadMedida.valueOf(rs.getString("unidad_medida")));
        producto.setTipo(TipoProducto.valueOf(rs.getString("tipo")));
        producto.setPrecioUnitario(rs.getDouble("precio_unitario"));
        producto.setPuntos(rs.getInt("puntos"));
        lista.add(producto);
      }
    } catch (SQLException ex) {
      System.err.println(ex.getMessage());
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

    return lista;
  }

  @Override
  public int insertar(Producto producto) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL insertar_producto(?,?,?,?,?,?,?,?) }";
      cs = con.prepareCall(sql);
      cs.registerOutParameter("p_id_producto", java.sql.Types.INTEGER);
      cs.setString("p_nombre", producto.getNombre());
      cs.setInt("p_stock", producto.getStock());
      cs.setDouble("p_capacidad", producto.getCapacidad());
      cs.setString("p_unidad_medida", producto.getUnidadDeMedida().toString());
      cs.setString("p_tipo", producto.getTipo().toString());
      cs.setDouble("p_precio_unitario", producto.getPrecioUnitario());
      cs.setInt("p_puntos", producto.getPuntos());
      cs.executeUpdate();
      //Si se pudo insertar, se actualiza la tabla de últimos IDs
      resultado = cs.getInt("p_id_producto");
      producto.setIdProductoNumerico(resultado);
      producto.setIdProductoCadena("PRO" + String.format("%05d", resultado));
    } catch (SQLException ex) {
      System.out.print(ex.getMessage());
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

  @Override
  public int eliminar(int idProducto) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL eliminar_producto(?) }";
      cs = con.prepareCall(sql);
      cs.setInt(1, idProducto);
      cs.executeUpdate();
      resultado = 1;
    } catch (SQLException ex) {
      System.err.println(ex.getMessage());
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

  @Override
  public int modificar(Producto producto) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{ CALL actualizar_producto(?,?,?,?,?,?,?,?) }";
      cs = con.prepareCall(sql);
      cs.setString("p_nombre", producto.getNombre());
      cs.setInt("p_stock", producto.getStock());
      cs.setDouble("p_capacidad", producto.getCapacidad());
      cs.setString("p_unidad_medida", producto.getUnidadDeMedida().toString());
      cs.setString("p_tipo", producto.getTipo().toString());
      cs.setDouble("p_precio_unitario", producto.getPrecioUnitario());
      cs.setInt("p_puntos", producto.getPuntos());
      cs.setInt("p_id_producto", producto.getIdProductoNumerico());
      cs.executeUpdate();
      resultado = 1;
    } catch (SQLException ex) {
      System.err.println(ex.getMessage());
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
