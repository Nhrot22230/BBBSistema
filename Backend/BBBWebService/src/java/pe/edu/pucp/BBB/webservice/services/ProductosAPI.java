/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.BBB.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.BBB.productos.controller.dao.ProductoDAO;
import pe.edu.pucp.BBB.productos.controller.mysql.ProductoMySQL;
import pe.edu.pucp.BBB.productos.model.Producto;

/**
 *
 * @author Candi
 */
@WebService(serviceName = "ProductosAPI", targetNamespace="http://services.webservice.BBB.pucp.edu.pe/")
public class ProductosAPI {
  ProductoDAO daoProducto;
  
  public ProductosAPI() {
    daoProducto = new ProductoMySQL();
  }
  /**
   * This is a sample web service operation
   *
   * @param txt
   * @return
   */
  @WebMethod(operationName = "hello")
  public String hello(@WebParam(name = "name") String txt) {
    return "Hello " + txt + " !";
  }
  
  @WebMethod(operationName = "listarProductos")
  public ArrayList<Producto> listarProductos(@WebParam(name = "cadena") String txt) {
    ArrayList<Producto> lista = null;
    txt = (txt == null) ? "" : txt;
    try{
      lista = daoProducto.listar(txt);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    return lista;
  }
  
  @WebMethod(operationName = "insertarProducto")
  public int insertarProducto(@WebParam(name = "producto") Producto p) {
    int resultado = 0;
    try{
      resultado = daoProducto.insertar(p);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarProducto")
  public int actualizarProducto(@WebParam(name = "producto") Producto p) {
    int resultado = 0;
    try{
      resultado = daoProducto.modificar(p);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "eliminarProducto")
  public int eliminarProducto(@WebParam(name = "id_producto") int id_producto) {
    int resultado = 0;
    try{
      resultado = daoProducto.eliminar(id_producto);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
}
