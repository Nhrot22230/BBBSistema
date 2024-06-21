/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.reportes.manager;

import java.util.HashMap;
import net.sf.jasperreports.engine.JRException;
import net.sf.jasperreports.engine.JasperCompileManager;
import net.sf.jasperreports.engine.JasperExportManager;
import net.sf.jasperreports.engine.JasperFillManager;
import net.sf.jasperreports.engine.JasperPrint;
import net.sf.jasperreports.engine.JasperReport;
import pe.edu.pucp.BBB.utils.database.DBManager;

import java.sql.Connection;

/**
 *
 * @author Candi
 */
public class ReporteManager {
  public static byte[] generarReporteAlmacen(String template_path) throws JRException{
    byte[] file;
    
    // Compilación del archivo jrxml de Jasper Reports
    JasperReport report;
    report = JasperCompileManager.compileReport(template_path);
    // Obtener conexión a la base de datos
    Connection conn = DBManager.getInstance().getConnection();
    HashMap<String, Object> parameters = new HashMap<>();
    JasperPrint print = JasperFillManager.fillReport(report, parameters, conn);
    file = JasperExportManager.exportReportToPdf(print);
    
    return file;
  }
  
  public static byte[] imprimirComprobante(String template_path, int idComprobanteNumerico) throws JRException{
    byte[] file;
    
    // Compilación del archivo jrxml de Jasper Reports
    JasperReport report;
    report = JasperCompileManager.compileReport(template_path);
    // Obtener conexión a la base de datos
    Connection conn = DBManager.getInstance().getConnection();
    HashMap<String, Object> parameters = new HashMap<>();
    parameters.put("ID_comprobante", idComprobanteNumerico);
    JasperPrint print = JasperFillManager.fillReport(report, parameters, conn);
    file = JasperExportManager.exportReportToPdf(print);
    
    return file;
  }
  
  
  
}
