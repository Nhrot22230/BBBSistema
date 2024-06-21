/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.BBB.webservice.services;

import at.favre.lib.crypto.bcrypt.BCrypt;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.BBB.cuentas.controller.dao.CuentaClienteDAO;
import pe.edu.pucp.BBB.cuentas.controller.dao.CuentaEmpleadoDAO;
import pe.edu.pucp.BBB.cuentas.controller.mysql.CuentaClienteMySQL;
import pe.edu.pucp.BBB.cuentas.controller.mysql.CuentaEmpleadoMySQL;
import pe.edu.pucp.BBB.cuentas.model.CuentaCliente;
import pe.edu.pucp.BBB.cuentas.model.CuentaEmpleado;
import pe.edu.pucp.BBB.cuentas.model.PersonaCuenta;
import pe.edu.pucp.BBB.personas.model.Cliente;
import pe.edu.pucp.BBB.personas.model.Empleado;

/**
 *
 * @author Candi
 */
@WebService(serviceName = "CuentasAPI", targetNamespace = "http://services.webservice.BBB.pucp.edu.pe/")
public class CuentasAPI {
  CuentaEmpleadoDAO daoCuentaEmp;
  CuentaClienteDAO daoCuentaCli;
  
  public CuentasAPI(){
    daoCuentaEmp = new CuentaEmpleadoMySQL();
    daoCuentaCli = new CuentaClienteMySQL();
  }
  /**
   * This is a sample web service operation
   * @param txt
   * @return 
   */
  @WebMethod(operationName = "hello")
  public String hello(@WebParam(name = "name") String txt) {
    return "Hello " + txt + " !";
  }
  
  @WebMethod(operationName = "iniciarSesionCliente")
  public Cliente iniciarSesionCliente(@WebParam(name = "user") String user, @WebParam(name = "pass") String pass) {
    Cliente cli = null;
    
    try {
      cli = daoCuentaCli.iniciar_sesion(user, pass);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return cli;
  }
  
  @WebMethod(operationName = "listarCuentaCliente")
  public ArrayList<CuentaCliente> listarCuentaCliente(){
    ArrayList<CuentaCliente> lista = null;
    
    try {
      lista = daoCuentaCli.listarCuentas_Clientes();
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    
    return lista;
  }
  
  @WebMethod(operationName = "insertarCuentaCliente")
  public int insertarCuentaCliente(@WebParam(name = "cuentaCliente") CuentaCliente cc){
    int resultado = 0;
    
    try {
      cc.setContrasena(BCrypt.withDefaults().hashToString(12, cc.getContrasena().toCharArray()));
      resultado = daoCuentaCli.insertar_Cuenta_Cliente(cc);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarCuentaCliente")
  public int actualizarCuentaCliente(@WebParam(name = "cuentaCliente") CuentaCliente cc){
    int resultado = 0;
    
    try {
      cc.setContrasena(BCrypt.withDefaults().hashToString(12, cc.getContrasena().toCharArray()));
      resultado = daoCuentaCli.actualizar_Cuenta_Cliente(cc);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "eliminarCuentaCliente")
  public int eliminarCuentaCliente(@WebParam(name = "id_cliente") int id_cliente){
    int resultado = 0;
    
    try {
      resultado = daoCuentaCli.eliminar_Cuenta_Cliente(id_cliente);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  /* Empleados */
  
  @WebMethod(operationName = "iniciarSesionEmpleado")
  public Empleado iniciarSesionEmpleado(@WebParam(name = "user") String user, @WebParam(name = "pass") String pass) {
    Empleado emp = null;
    
    try {
      emp = daoCuentaEmp.iniciar_sesion(user, pass);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return emp;
  }

  @WebMethod(operationName = "listarCuentaEmpleado")
  public ArrayList<CuentaEmpleado> listarCuentaEmpleado(){
    ArrayList<CuentaEmpleado> lista = null;
    
    try {
      lista = daoCuentaEmp.listarCuentas_Empleados();
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    
    return lista;
  }
  
  @WebMethod(operationName = "insertarCuentaEmpleado")
  public int insertarCuentaEmpleado(@WebParam(name = "cuentaEmpleado") CuentaEmpleado ce){
    int resultado = 0;
    
    try {
      ce.setContrasena(BCrypt.withDefaults().hashToString(12, ce.getContrasena().toCharArray()));
      resultado = daoCuentaEmp.insertar_Cuenta_Empleado(ce);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarCuentaEmpleado")
  public int actualizarCuentaEmpleado(@WebParam(name = "cuentaEmpleado") CuentaEmpleado ce){
    int resultado = 0;
    
    try {
      ce.setContrasena(BCrypt.withDefaults().hashToString(12, ce.getContrasena().toCharArray()));
      resultado = daoCuentaEmp.actualizar_Cuenta_Empleado(ce);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "eliminarCuentaEmpleado")
  public int eliminarCuentaEmpleado(@WebParam(name = "id_empleado") int id_empleado){
    int resultado = 0;
    
    try {
      resultado = daoCuentaEmp.eliminar_Cuenta_Empleado(id_empleado);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "listarClientesMasCuentas")
  public ArrayList<PersonaCuenta> listarClientesMasCuentas(@WebParam(name = "filtro") String txt){
    ArrayList<PersonaCuenta> lista = null;
    
    txt = txt == null ? "" : txt;
    try {
      lista = daoCuentaCli.listar_clientes_cuentas(txt);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    
    return lista;
  }

  @WebMethod(operationName = "listarEmpleadosMasCuentas")
  public ArrayList<PersonaCuenta> listarEmpleadosMasCuentas(@WebParam(name = "filtro") String txt){
    ArrayList<PersonaCuenta> lista = null;
    
    txt = txt == null ? "" : txt;
    try {
      lista = daoCuentaEmp.listar_empleados_cuentas(txt);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    
    return lista;
  }
}
