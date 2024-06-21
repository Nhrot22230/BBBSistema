/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.BBB.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.BBB.personas.controller.dao.ClienteDAO;
import pe.edu.pucp.BBB.personas.controller.dao.EmpleadoDAO;
import pe.edu.pucp.BBB.personas.controller.mysql.ClienteMySQL;
import pe.edu.pucp.BBB.personas.controller.mysql.EmpleadoMySQL;
import pe.edu.pucp.BBB.personas.model.Cliente;
import pe.edu.pucp.BBB.personas.model.Empleado;

/**
 *
 * @author Candi
 */
@WebService(serviceName = "PersonasAPI", targetNamespace="http://services.webservice.BBB.pucp.edu.pe/")
public class PersonasAPI {
  ClienteDAO daoCliente;
  EmpleadoDAO daoEmpleado;
  
  public PersonasAPI(){
    daoCliente = new ClienteMySQL();
    daoEmpleado = new EmpleadoMySQL();
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
  
  @WebMethod(operationName = "listarClientes")
  public ArrayList<Cliente> listarClientes(@WebParam(name = "cadena") String txt){
    ArrayList<Cliente> lista = null;
  
    txt = (txt == null) ? "" : txt;
    try {
      lista = daoCliente.listar(txt);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    return lista;
  }
  
  @WebMethod(operationName = "insertarCliente")
  public int insertarCliente(@WebParam(name = "cliente") Cliente cli){
    int resultado = 0;
    
    try {
      resultado = daoCliente.insertar(cli);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarCliente")
  public int actualizarCliente(@WebParam(name = "cliente") Cliente cli){
    int resultado = 0;
    
    try {
      resultado = daoCliente.modificar(cli);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "eliminarCliente")
  public int eliminarCliente(@WebParam(name = "cliente") int id_cli){
    int resultado = 0;
    
    try {
      resultado = daoCliente.eliminar(id_cli);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  /* Empleados */
  
  @WebMethod(operationName = "listarEmpleados")
  public ArrayList<Empleado> listarEmleados(@WebParam(name = "cadena") String txt){
    ArrayList<Empleado> lista = null;
  
    txt = (txt == null) ? "" : txt;
    try {
      lista = daoEmpleado.listar(txt);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    return lista;
  }
  
  @WebMethod(operationName = "insertarEmpleado")
  public int insertarEmpleado(@WebParam(name = "empleado") Empleado emp){
    int resultado = 0;
    
    try {
      resultado = daoEmpleado.insertar(emp);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarEmpleado")
  public int actualizarEmpleado(@WebParam(name = "empleado") Empleado emp){
    int resultado = 0;
    
    try {
      resultado = daoEmpleado.modificar(emp);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "eliminarEmpleado")
  public int eliminarEmpleado(@WebParam(name = "empleado") int id_emp){
    int resultado = 0;
    
    try {
      resultado = daoEmpleado.eliminar(id_emp);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
}
