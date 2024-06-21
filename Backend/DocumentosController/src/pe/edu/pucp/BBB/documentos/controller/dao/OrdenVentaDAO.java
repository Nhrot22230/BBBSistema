/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.BBB.documentos.model.OrdenVenta;

/**
 *
 * @author Candi
 */
public interface OrdenVentaDAO {
    ArrayList<OrdenVenta> listar(String cadena);
    int insertar(OrdenVenta ordenVenta);
    int modificar(OrdenVenta ordenVenta);
}
