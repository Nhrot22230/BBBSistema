/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.productos.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.BBB.productos.model.Producto;

/**
 *
 * @author Candi
 */
public interface ProductoDAO {
    ArrayList<Producto> listar(String filtro);   
    ArrayList<Producto> listar_todos();
    int insertar(Producto producto);
    int eliminar(int idProducto);
    int modificar(Producto producto);
}

