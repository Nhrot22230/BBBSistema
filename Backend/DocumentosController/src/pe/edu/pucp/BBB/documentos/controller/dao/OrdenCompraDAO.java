/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.documentos.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.BBB.documentos.model.OrdenCompra;

/**
 *
 * @author Candi
 */
public interface OrdenCompraDAO {
    ArrayList<OrdenCompra> listar(String cadena);
    int insertar(OrdenCompra ordenCompra);
    int modificar(OrdenCompra ordenCompra);
}
