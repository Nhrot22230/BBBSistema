/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.cuentas.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.BBB.cuentas.model.CuentaEmpleado;
import pe.edu.pucp.BBB.cuentas.model.PersonaCuenta;
import pe.edu.pucp.BBB.personas.model.Empleado;

/**
 *
 * @author Candi
 */
public interface CuentaEmpleadoDAO {

  Empleado iniciar_sesion(String usuario, String contrasena);

  ArrayList<CuentaEmpleado> listarCuentas_Empleados();

  int insertar_Cuenta_Empleado(CuentaEmpleado cuenta);

  int actualizar_Cuenta_Empleado(CuentaEmpleado cuenta);

  int eliminar_Cuenta_Empleado(int idCuenta);
  
  ArrayList<PersonaCuenta> listar_empleados_cuentas(String cadena);
}
