/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.personas.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.BBB.personas.model.Cliente;

/**
 *
 * @author Candi
 */
public interface ClienteDAO {

  ArrayList<Cliente> listar(String cadena);

  int insertar(Cliente cliente);

  int eliminar(int codigoCliente);

  int modificar(Cliente cliente);
}
