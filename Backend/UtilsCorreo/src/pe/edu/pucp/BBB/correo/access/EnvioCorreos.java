/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.correo.access;

import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.NoSuchProviderException;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.AddressException;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeBodyPart;
import javax.mail.internet.MimeMessage;
import javax.mail.internet.MimeMultipart;
import javax.swing.JOptionPane;
import javax.activation.DataHandler;
import javax.activation.DataSource;
import javax.activation.FileDataSource;
import pe.edu.pucp.BBB.correo.dao.CorreoDAO;

/**
 *
 * @author Candi
 */
public class EnvioCorreos implements CorreoDAO{

  private static String emailFrom = "dxnstore0054lim@gmail.com";
  private static String passwordFrom = "zuxh osgd akol cgxg";
  private String emailTo;
  private String subject;
  private String content;
  private String filePath; // Ruta del archivo PDF a adjuntar

  private Properties mProperties;
  private Session mSession;
  private MimeMessage mCorreo;

  public EnvioCorreos() {
    mProperties = new Properties();
  }

  @Override
  public int enviarCorreo(String asunto, String contenido, String correo, String rutaArchivo) {
    try {
      createEmail(asunto, contenido, correo, rutaArchivo);
      sendEmail();
    } catch (Exception e) {
      System.err.println(e.getMessage());
      return -1; // Indicar que hubo un error
    }
    return 0;
  }

  private void createEmail(String asunto, String contenido, String correo, String rutaArchivo) {
    emailTo = correo;
    subject = asunto;
    content = contenido;
    filePath = rutaArchivo;

    // Configuración del servidor SMTP
    mProperties.put("mail.smtp.host", "smtp.gmail.com");
    mProperties.put("mail.smtp.ssl.trust", "smtp.gmail.com");
    mProperties.setProperty("mail.smtp.starttls.enable", "true");
    mProperties.setProperty("mail.smtp.port", "587");
    mProperties.setProperty("mail.smtp.user", emailFrom);
    mProperties.setProperty("mail.smtp.ssl.protocols", "TLSv1.2");
    mProperties.setProperty("mail.smtp.auth", "true");

    mSession = Session.getDefaultInstance(mProperties);

    try {
      mCorreo = new MimeMessage(mSession);
      mCorreo.setFrom(new InternetAddress(emailFrom));
      mCorreo.setRecipient(Message.RecipientType.TO, new InternetAddress(emailTo));
      mCorreo.setSubject(subject);

      // Cuerpo del correo
      MimeBodyPart bodyPart = new MimeBodyPart();
      bodyPart.setContent(content, "text/html; charset=utf-8");

      // Archivo adjunto
      MimeBodyPart attachmentPart = new MimeBodyPart();
      DataSource source = new FileDataSource(filePath);
      attachmentPart.setDataHandler(new DataHandler(source));
      attachmentPart.setFileName(source.getName());

      // Agregar partes al mensaje
      MimeMultipart multipart = new MimeMultipart();
      multipart.addBodyPart(bodyPart);
      multipart.addBodyPart(attachmentPart);

      mCorreo.setContent(multipart);

    } catch (AddressException ex) {
      Logger.getLogger(EnvioCorreos.class.getName()).log(Level.SEVERE, null, ex);
    } catch (MessagingException ex) {
      Logger.getLogger(EnvioCorreos.class.getName()).log(Level.SEVERE, null, ex);
    }
  }

  private void sendEmail() {
    try {
      Transport mTransport = mSession.getTransport("smtp");
      mTransport.connect(emailFrom, passwordFrom);
      mTransport.sendMessage(mCorreo, mCorreo.getRecipients(Message.RecipientType.TO));
      mTransport.close();
      JOptionPane.showMessageDialog(null, "Correo enviado");
    } catch (NoSuchProviderException ex) {
      Logger.getLogger(EnvioCorreos.class.getName()).log(Level.SEVERE, null, ex);
    } catch (MessagingException ex) {
      Logger.getLogger(EnvioCorreos.class.getName()).log(Level.SEVERE, null, ex);
    }
  }
}
