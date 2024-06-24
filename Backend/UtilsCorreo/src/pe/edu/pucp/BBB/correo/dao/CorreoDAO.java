/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package pe.edu.pucp.BBB.correo.dao;

/**
 *
 * @author GianLuka
 */
public interface CorreoDAO {
  int enviarCorreo(String asunto, String contenido, String correo,byte[] pdfBytes);
  int enviarCorreo(String asunto, String contenido, String correo);
}
