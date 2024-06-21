/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.personas.controller.dao;

import java.util.ArrayList;
import pe.edu.pucp.BBB.personas.model.Empleado;

/**
 *
 * @author Candi
 */
public interface EmpleadoDAO {

  ArrayList<Empleado> listar(String cadena);

  public ArrayList<Empleado> listar_todos();

  int insertar(Empleado empleado);

  int eliminar(int idEmpleadoActual);

  int modificar(Empleado empleado);
}
