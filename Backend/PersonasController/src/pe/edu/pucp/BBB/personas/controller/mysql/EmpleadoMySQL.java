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
import pe.edu.pucp.BBB.personas.controller.dao.EmpleadoDAO;
import pe.edu.pucp.BBB.personas.model.Empleado;
import pe.edu.pucp.BBB.personas.model.Rol;
import pe.edu.pucp.BBB.utils.database.DBManager;

/**
 *
 * @author Candi
 */
public class EmpleadoMySQL implements EmpleadoDAO {

  private Connection con;
  private CallableStatement cs;
  private String sql;
  private ResultSet rs;

  /*
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
   */
  @Override
  public ArrayList<Empleado> listar(String cadena) {
    ArrayList<Empleado> lista = new ArrayList<>();

    try {
      con = DBManager.getInstance().getConnection();
      cs = con.prepareCall("{CALL listar_empleados(?)}");
      cs.setString(1, cadena);
      rs = cs.executeQuery();
      while (rs.next()) {
        Empleado empleado = new Empleado();
        empleado.setIdEmpleadoNumerico(rs.getInt("id_empleado"));
        empleado.setIdEmpleadoCadena("EMP" + String.format("%05d", empleado.getIdEmpleadoNumerico()));
        empleado.setDNI(rs.getString("dni"));
        empleado.setNombre(rs.getString("nombre"));
        empleado.setApellidoPaterno(rs.getString("apellido_paterno"));
        empleado.setApellidoMaterno(rs.getString("apellido_materno"));
        empleado.setSueldo(rs.getDouble("sueldo"));
        empleado.setRol(Rol.valueOf(rs.getString("rol")));
        empleado.setEmpleadoActivo(true);
        lista.add(empleado);
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
   * CREATE PROCEDURE listar_empleados_todos()
   * BEGIN
   * SELECT id_empleado,
   * dni, nombre, apellido_paterno,
   * apellido_materno, sueldo, rol, activo
   * FROM Empleado;
   * END$$
   */
  @Override
  public ArrayList<Empleado> listar_todos() {
    ArrayList<Empleado> lista = new ArrayList<>();

    try {
      con = DBManager.getInstance().getConnection();
      cs = con.prepareCall("{CALL listar_empleados_todos()}");
      rs = cs.executeQuery();
      while (rs.next()) {
        Empleado empleado = new Empleado();
        empleado.setIdEmpleadoNumerico(rs.getInt("id_empleado"));
        empleado.setIdEmpleadoCadena("EMP" + String.format("%05d", empleado.getIdEmpleadoNumerico()));
        empleado.setDNI(rs.getString("dni"));
        empleado.setNombre(rs.getString("nombre"));
        empleado.setApellidoPaterno(rs.getString("apellido_paterno"));
        empleado.setApellidoMaterno(rs.getString("apellido_materno"));
        empleado.setSueldo(rs.getDouble("sueldo"));
        empleado.setRol(Rol.valueOf(rs.getString("rol")));
        empleado.setEmpleadoActivo(rs.getBoolean("activo"));
        lista.add(empleado);
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
   * CREATE PROCEDURE insertar_empleado(
   * OUT p_id_empleado INT,
   * IN p_dni CHAR(8),
   * IN p_nombre VARCHAR(30),
   * IN p_apellido_paterno VARCHAR(30),
   * IN p_apellido_materno VARCHAR(30),
   * IN p_sueldo DECIMAL(10, 2),
   * IN p_rol ENUM('Repartidor', 'EncargadoVentas', 'EncargadoAlmacen',
   * 'Administrador')
   * )
   * BEGIN
   * INSERT INTO Empleado(dni, nombre, apellido_paterno, apellido_materno, sueldo,
   * rol)
   * VALUES(p_dni, p_nombre, p_apellido_paterno, p_apellido_materno, p_sueldo,
   * p_rol);
   * SET p_id_empleado = LAST_INSERT_ID();
   * END$$
   */
  @Override
  public int insertar(Empleado empleado) {
    int resultado = 0;
    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL insertar_empleado(?,?,?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.registerOutParameter(1, java.sql.Types.INTEGER);
      cs.setString(2, empleado.getDNI());
      cs.setString(3, empleado.getNombre());
      cs.setString(4, empleado.getApellidoPaterno());
      cs.setString(5, empleado.getApellidoMaterno());
      cs.setDouble(6, empleado.getSueldo());
      cs.setString(7, empleado.getRol().toString());
      cs.executeUpdate();
      resultado = cs.getInt(1);
      empleado.setIdEmpleadoNumerico(resultado);
      empleado.setIdEmpleadoCadena("EMP" + String.format("%05d", resultado));
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
   * CREATE PROCEDURE eliminar_empleado(
   * IN p_id_empleado INT
   * )
   * BEGIN
   * UPDATE Empleado
   * SET activo = 0
   * WHERE id_empleado = p_id_empleado;
   * END$$
   */

  @Override
  public int eliminar(int idEmpleadoActual) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL eliminar_empleado(?)}";
      cs = con.prepareCall(sql);
      cs.setInt(1, idEmpleadoActual);
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
   * CREATE PROCEDURE actualizar_empleado(
   * IN p_id_empleado INT,
   * IN p_dni CHAR(8),
   * IN p_nombre VARCHAR(30),
   * IN p_apellido_paterno VARCHAR(30),
   * IN p_apellido_materno VARCHAR(30),
   * IN p_sueldo DECIMAL(10, 2),
   * IN p_rol ENUM('Repartidor', 'EncargadoVentas', 'EncargadoAlmacen',
   * 'Administrador')
   * )
   * BEGIN
   * UPDATE Empleado
   * SET dni = p_dni,
   * nombre = p_nombre,
   * apellido_paterno = p_apellido_paterno,
   * apellido_materno = p_apellido_materno,
   * sueldo = p_sueldo,
   * rol = p_rol
   * WHERE id_empleado = p_id_empleado;
   * END$$
   */
  @Override
  public int modificar(Empleado empleado) {
    int resultado = 0;

    try {
      con = DBManager.getInstance().getConnection();
      sql = "{CALL actualizar_empleado(?,?,?,?,?,?,?)}";
      cs = con.prepareCall(sql);
      cs.setInt(1, empleado.getIdEmpleadoNumerico());
      cs.setString(2, empleado.getDNI());
      cs.setString(3, empleado.getNombre());
      cs.setString(4, empleado.getApellidoPaterno());
      cs.setString(5, empleado.getApellidoMaterno());
      cs.setDouble(6, empleado.getSueldo());
      cs.setString(7, empleado.getRol().toString());
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
