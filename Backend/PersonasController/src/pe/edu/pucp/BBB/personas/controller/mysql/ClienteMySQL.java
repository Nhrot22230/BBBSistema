/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.personas.controller.mysql;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import pe.edu.pucp.BBB.personas.controller.dao.ClienteDAO;
import pe.edu.pucp.BBB.personas.model.Cliente;
import pe.edu.pucp.BBB.utils.database.DBManager;

/**
 *
 * @author Candi
 */
public class ClienteMySQL implements ClienteDAO {

  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;

  /*
   * CREATE PROCEDURE listar_clientes(
   * IN p_cadena VARCHAR(50)
   * )
   * BEGIN
   * SELECT c1.id_cliente, c1.dni, c1.nombre,
   * c1.apellido_paterno, c1.apellido_materno, c1.puntos,
   * c1.puntos_retenidos, c1.ruc, c1.razon_social,
   * c1.direccion,
   * c1.id_patrocinador,
   * c2.dni as dni_patrocinador,
   * c2.nombre as nombre_patrocinador,
   * c2.apellido_paterno as apellido_paterno_patrocinador,
   * c2.apellido_materno as apellido_materno_patrocinador,
   * c2.puntos as puntos_patrocinador,
   * c2.puntos_retenidos as puntos_retenidos_patrocinador,
   * c2.ruc as ruc_patrocinador,
   * c2.razon_social as razon_social_patrocinador,
   * c2.direccion as direccion_patrocinador,
   * c3.id_cliente as id_patrocinado,
   * c3.dni as dni_patrocinado,
   * c3.nombre as nombre_patrocinado,
   * c3.apellido_paterno as apellido_paterno_patrocinado,
   * c3.apellido_materno as apellido_materno_patrocinado,
   * c3.puntos as puntos_patrocinado,
   * c3.puntos_retenidos as puntos_retenidos_patrocinado,
   * c3.ruc as ruc_patrocinado,
   * c3.razon_social as razon_social_patrocinado,
   * c3.direccion as direccion_patrocinado
   * FROM Cliente as c1
   * LEFT JOIN Cliente as c2 ON c1.id_patrocinador = c2.id_cliente
   * LEFT JOIN Cliente as c3 ON c1.id_cliente = c3.id_patrocinador
   * WHERE c1.activo = 1
   * AND (c1.dni LIKE CONCAT('%', p_cadena, '%') OR
   * c1.nombre LIKE CONCAT('%', p_cadena, '%') OR
   * c1.apellido_paterno LIKE CONCAT('%', p_cadena, '%') OR
   * c1.apellido_materno LIKE CONCAT('%', p_cadena, '%') OR
   * c1.ruc LIKE CONCAT('%', p_cadena, '%') OR
   * c1.razon_social LIKE CONCAT('%', p_cadena, '%') OR
   * c1.direccion LIKE CONCAT('%', p_cadena, '%'));
   * END$$
   */
  @Override
  public ArrayList<Cliente> listar(String cadena) {
    ArrayList<Cliente> lista = new ArrayList<>();

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL listar_clientes(?)}";
      cs = con.prepareCall(sql);
      cs.setString(1, cadena);
      rs = cs.executeQuery();

      Cliente prevCli = null;
      Cliente curCli;
      while (rs.next()) {
        curCli = new Cliente();
        curCli.setIdNumerico(rs.getInt("id_cliente"));
        curCli.setIdCadena("CLI" + String.format("%05d", curCli.getIdNumerico()));
        curCli.setDNI(rs.getString("dni"));
        curCli.setNombre(rs.getString("nombre"));
        curCli.setApellidoPaterno(rs.getString("apellido_paterno"));
        curCli.setApellidoMaterno(rs.getString("apellido_materno"));
        curCli.setDireccion(rs.getString("direccion"));
        curCli.setRUC(rs.getString("ruc"));
        curCli.setRazonSocial(rs.getString("razon_social"));
        curCli.setPuntos(rs.getInt("puntos"));
        curCli.setPuntosRetenidos(rs.getInt("puntos_retenidos"));

        int id_patrocinador = rs.getInt("id_patrocinador");
        String dni_patrocinador = rs.getString("dni_patrocinador");
        String nombre_patrocinador = rs.getString("nombre_patrocinador");
        String apellido_paterno_patrocinador = rs.getString("apellido_paterno_patrocinador");
        String apellido_materno_patrocinador = rs.getString("apellido_materno_patrocinador");
        String direccion_patrocinador = rs.getString("direccion_patrocinador");
        String ruc_patrocinador = rs.getString("ruc_patrocinador");
        String razon_social_patrocinador = rs.getString("razon_social_patrocinador");

        Cliente curPatrocinador = null;
        if (id_patrocinador != 0) {
          curPatrocinador = new Cliente();
          curPatrocinador.setIdNumerico(id_patrocinador);
          curPatrocinador.setIdCadena("CLI" + String.format("%05d", id_patrocinador));
          curPatrocinador.setDNI(dni_patrocinador);
          curPatrocinador.setNombre(nombre_patrocinador);
          curPatrocinador.setApellidoPaterno(apellido_paterno_patrocinador);
          curPatrocinador.setApellidoMaterno(apellido_materno_patrocinador);
          curPatrocinador.setDireccion(direccion_patrocinador);
          curPatrocinador.setRUC(ruc_patrocinador);
          curPatrocinador.setRazonSocial(razon_social_patrocinador);
          curPatrocinador.setPuntos(rs.getInt("puntos_patrocinador"));
          curPatrocinador.setPuntosRetenidos(rs.getInt("puntos_retenidos_patrocinador"));
        }

        int id_patrocinado = rs.getInt("id_patrocinado");
        String dni_patrocinado = rs.getString("dni_patrocinado");
        String nombre_patrocinado = rs.getString("nombre_patrocinado");
        String apellido_paterno_patrocinado = rs.getString("apellido_paterno_patrocinado");
        String apellido_materno_patrocinado = rs.getString("apellido_materno_patrocinado");
        String direccion_patrocinado = rs.getString("direccion_patrocinado");
        String ruc_patrocinado = rs.getString("ruc_patrocinado");
        String razon_social_patrocinado = rs.getString("razon_social_patrocinado");

        Cliente patrocinado = null;
        if (id_patrocinado != 0) {
          patrocinado = new Cliente();
          patrocinado.setIdNumerico(id_patrocinado);
          patrocinado.setIdCadena("CLI" + String.format("%05d", id_patrocinado));
          patrocinado.setDNI(dni_patrocinado);
          patrocinado.setNombre(nombre_patrocinado);
          patrocinado.setApellidoPaterno(apellido_paterno_patrocinado);
          patrocinado.setApellidoMaterno(apellido_materno_patrocinado);
          patrocinado.setDireccion(direccion_patrocinado);
          patrocinado.setRUC(ruc_patrocinado);
          patrocinado.setRazonSocial(razon_social_patrocinado);
          patrocinado.setPuntos(rs.getInt("puntos_patrocinado"));
          patrocinado.setPuntosRetenidos(rs.getInt("puntos_retenidos_patrocinado"));
        }

        if (prevCli != null && curCli.getIdNumerico() == prevCli.getIdNumerico()) {
          prevCli.agregarPatrocinado(patrocinado);
        } else {
          if (prevCli != null) {
            lista.add(prevCli);
          }
          prevCli = curCli;
          prevCli.setPatrocinador(curPatrocinador);
          if (patrocinado != null) {
            prevCli.agregarPatrocinado(patrocinado);
          }
        }
      }
      if (prevCli != null) {
        lista.add(prevCli);
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

  /*
   * DELIMITER $$
   * CREATE PROCEDURE insertar_cliente(
   * OUT p_id_cliente INT,
   * IN p_dni CHAR(8),
   * IN p_nombre VARCHAR(30),
   * IN p_apellido_paterno VARCHAR(30),
   * IN p_apellido_materno VARCHAR(30),
   * IN p_puntos INT,
   * IN p_puntos_retenidos INT,
   * IN p_ruc CHAR(11),
   * IN p_razon_social VARCHAR(30),
   * IN p_direccion VARCHAR(50),
   * IN p_id_patrocinador INT
   * )
   * BEGIN
   * INSERT INTO Cliente(dni, nombre, apellido_paterno, apellido_materno, puntos,
   * puntos_retenidos, ruc, razon_social, direccion, id_patrocinador)
   * VALUES(p_dni, p_nombre, p_apellido_paterno, p_apellido_materno, p_puntos,
   * p_puntos_retenidos, p_ruc, p_razon_social, p_direccion, p_id_patrocinador);
   * SET p_id_cliente = LAST_INSERT_ID();
   * END$$
   */
  @Override
  public int insertar(Cliente cliente) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      con.setAutoCommit(false);
      sql = "{CALL insertar_cliente(?,?,?,?,?,?,?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.registerOutParameter(1, java.sql.Types.INTEGER);
      cs.setString(2, cliente.getDNI());
      cs.setString(3, cliente.getNombre());
      cs.setString(4, cliente.getApellidoPaterno());
      cs.setString(5, cliente.getApellidoMaterno());
      cs.setInt(6, cliente.getPuntos());
      cs.setInt(7, cliente.getPuntosRetenidos());
      cs.setString(8, cliente.getRUC());
      cs.setString(9, cliente.getRazonSocial());
      cs.setString(10, cliente.getDireccion());
      if (cliente.getPatrocinador() != null) {
        cs.setInt(11, cliente.getPatrocinador().getIdNumerico());
      } else {
        cs.setNull(11, java.sql.Types.INTEGER);
      }
      cs.executeUpdate();
      resultado = cs.getInt(1);
      cliente.setIdNumerico(resultado);
      cliente.setIdCadena("CLI" + String.format("%05d", resultado));
      con.commit();
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
  public int eliminar(int codigoClienteActual) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL eliminar_cliente(?)}";
      cs = con.prepareCall(sql);
      cs.setInt(1, codigoClienteActual);
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

  /*
   * CREATE PROCEDURE actualizar_cliente(
   * IN p_id_cliente INT,
   * IN p_dni CHAR(8),
   * IN p_nombre VARCHAR(30),
   * IN p_apellido_paterno VARCHAR(30),
   * IN p_apellido_materno VARCHAR(30),
   * IN p_puntos INT,
   * IN p_puntos_retenidos INT,
   * IN p_ruc CHAR(11),
   * IN p_razon_social VARCHAR(30),
   * IN p_direccion VARCHAR(50),
   * IN p_id_patrocinador INT
   * )
   * BEGIN
   * UPDATE Cliente
   * SET dni = p_dni,
   * nombre = p_nombre,
   * apellido_paterno = p_apellido_paterno,
   * apellido_materno = p_apellido_materno,
   * puntos = p_puntos,
   * puntos_retenidos = p_puntos_retenidos,
   * ruc = p_ruc,
   * razon_social = p_razon_social,
   * direccion = p_direccion,
   * id_patrocinador = p_id_patrocinador
   * WHERE id_cliente = p_id_cliente;
   * END$$
   */

  @Override
  public int modificar(Cliente cliente) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL actualizar_cliente(?,?,?,?,?,?,?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.setInt(1, cliente.getIdNumerico());
      cs.setString(2, cliente.getDNI());
      cs.setString(3, cliente.getNombre());
      cs.setString(4, cliente.getApellidoPaterno());
      cs.setString(5, cliente.getApellidoMaterno());
      cs.setInt(6, cliente.getPuntos());
      cs.setInt(7, cliente.getPuntosRetenidos());
      cs.setString(8, cliente.getRUC());
      cs.setString(9, cliente.getRazonSocial());
      cs.setString(10, cliente.getDireccion());
      if (cliente.getPatrocinador() != null) {
        cs.setInt(11, cliente.getPatrocinador().getIdNumerico());
      } else {
        cs.setNull(11, java.sql.Types.INTEGER);
      }
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
