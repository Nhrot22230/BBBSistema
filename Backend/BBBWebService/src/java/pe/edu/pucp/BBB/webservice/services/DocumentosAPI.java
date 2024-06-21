/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package pe.edu.pucp.BBB.webservice.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.BBB.documentos.controller.dao.ComprobanteDAO;
import pe.edu.pucp.BBB.documentos.controller.dao.LineaOrdenDAO;
import pe.edu.pucp.BBB.documentos.controller.dao.OrdenCompraDAO;
import pe.edu.pucp.BBB.documentos.controller.dao.OrdenVentaDAO;
import pe.edu.pucp.BBB.documentos.controller.mysql.ComprobanteMySQL;
import pe.edu.pucp.BBB.documentos.controller.mysql.LineaOrdenMySQL;
import pe.edu.pucp.BBB.documentos.controller.mysql.OrdenCompraMySQL;
import pe.edu.pucp.BBB.documentos.controller.mysql.OrdenVentaMySQL;
import pe.edu.pucp.BBB.documentos.model.Comprobante;
import pe.edu.pucp.BBB.documentos.model.LineaOrden;
import pe.edu.pucp.BBB.documentos.model.OrdenCompra;
import pe.edu.pucp.BBB.documentos.model.OrdenVenta;
import pe.edu.pucp.BBB.documentos.model.Orden;

/**
 *
 * @author Candi
 */
@WebService(serviceName = "DocumentosAPI", targetNamespace="http://services.webservice.BBB.pucp.edu.pe/")
public class DocumentosAPI {
  OrdenCompraDAO daoOrdenCompra;
  OrdenVentaDAO daoOrdenVenta;
  ComprobanteDAO daoComprobante;
  LineaOrdenDAO daoOrdenlv;
  public DocumentosAPI(){
    daoOrdenCompra = new OrdenCompraMySQL();
    daoOrdenVenta = new OrdenVentaMySQL();
    daoComprobante = new ComprobanteMySQL();
    daoOrdenlv = new LineaOrdenMySQL();
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
  
  @WebMethod(operationName = "listarComprobante")
  public ArrayList<Comprobante> listarComprobante(@WebParam(name = "cadena") String txt){
    ArrayList<Comprobante> lista = null;
    txt = (txt == null) ? "" : txt;
    try{
      lista = daoComprobante.listar(txt);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    return lista;
  }
  
  @WebMethod(operationName = "insertarComprobante")
  public int insertarComprobante(@WebParam(name = "comprobante") Comprobante comp) {
    int resultado = 0;
    
    try {
      resultado = daoComprobante.insertar(comp);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarComprobante")
  public int actualizarComprobante(@WebParam(name = "comprobante") Comprobante comp) {
    int resultado = 0;
    
    try {
      resultado = daoComprobante.modificar(comp);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "eliminarComprobante")
  public int eliminarComprobante(@WebParam(name = "comprobante") int id_comp) {
    int resultado = 0;
    
    try {
      resultado = daoComprobante.eliminar(id_comp);
    } catch (Exception ex){
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  /* OrdenCompra */
  
  @WebMethod(operationName = "listarOrdenCompra")
  public ArrayList<OrdenCompra> listarOrdenCompra(@WebParam(name = "cadena") String txt){
    ArrayList<OrdenCompra> lista = null;
    txt = (txt == null) ? "" : txt;
    try{
      lista = daoOrdenCompra.listar(txt);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    return lista;
  }
  
  @WebMethod(operationName = "insertarOrdenCompra")
  public int insertarOrdenCompra(@WebParam(name = "ordenCompra") OrdenCompra orc){
    int resultado = 0;
    
    try{
      resultado = daoOrdenCompra.insertar(orc);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarOrdenCompra")
  public int actualizarOrdenCompra(@WebParam(name = "ordenCompra") OrdenCompra orc){
    int resultado = 0;
    
    try{
      resultado = daoOrdenCompra.modificar(orc);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  /* OrdenCompra */
  
  @WebMethod(operationName = "listarOrdenVenta")
  public ArrayList<OrdenVenta> listarOrdenVenta(@WebParam(name = "cadena") String txt){
    ArrayList<OrdenVenta> lista = null;
    txt = (txt == null) ? "" : txt;
    try{
      lista = daoOrdenVenta.listar(txt);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    return lista;
  }
  
  @WebMethod(operationName = "insertarOrdenVenta")
  public int insertarOrdenVenta(@WebParam(name = "ordenVenta") OrdenVenta orv){
    int resultado = 0;
    
    try{
      resultado = daoOrdenVenta.insertar(orv);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "actualizarOrdenVenta")
  public int actualizarOrdenVenta(@WebParam(name = "ordenVenta") OrdenVenta orv){
    int resultado = 0;
    
    try{
      resultado = daoOrdenVenta.modificar(orv);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "eliminarLOV")
  public int eliminarLOV(@WebParam(name = "id_orden") int orden, @WebParam(name = "id_producto")int producto){
    int resultado = 0;
    
    try{
      resultado = daoOrdenlv.eliminar(orden,producto );
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
  
  @WebMethod(operationName = "insertarLOV")
  public int insertarLOV(@WebParam(name = "lvOrden")  LineaOrden lvOrden, @WebParam(name = "id_orden")int id_orden){
    int resultado = 0;
    
    try{
      resultado = daoOrdenlv.insertar(lvOrden,id_orden);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    
    return resultado;
  }
    /* Ordenes */
  @WebMethod(operationName = "listarOrden")
  public ArrayList<Orden> listarOrden(@WebParam(name = "cadena") String txt){
    ArrayList<Orden> lista = null;
    ArrayList<OrdenVenta> listaVenta = null;
    ArrayList<OrdenCompra> listaCompra = null;
    txt = (txt == null) ? "" : txt;
    try{
        lista = new ArrayList<>();
        listaVenta = daoOrdenVenta.listar(txt);
        if(listaVenta!=null)
            for(OrdenVenta ordVenta : listaVenta)
                lista.add(ordVenta);
        listaCompra = daoOrdenCompra.listar(txt);
        if(listaCompra!=null)
            for(OrdenCompra ordCompra : listaCompra)
                lista.add(ordCompra);
    } catch (Exception ex) {
      System.err.println(ex.getMessage());
    }
    if(lista != null) lista = (lista.isEmpty()) ? null : lista;
    return lista;
  }
}
