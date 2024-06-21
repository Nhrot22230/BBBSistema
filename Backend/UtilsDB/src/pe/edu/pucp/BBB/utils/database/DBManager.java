/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package pe.edu.pucp.BBB.utils.database;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

/**
 *
 * @author Nhrot
 */
public class DBManager {

  private static DBManager instancia = null;
  private Connection con = null;
  private final String host = "lp2-20213812.cqglh3zviqfb.us-east-1.rds.amazonaws.com";
  private final String port = "3306";
  private final String db = "TA_HTML";
  private final String username = "admin";
  private final String password = "20213812";

  private DBManager() {
  }

  private void connectToDatabase() {
    try {
      Class.forName("com.mysql.cj.jdbc.Driver");
      String url = "jdbc:mysql://" + host + ":" + port + "/" + db + "?useSSL=false";
      con = DriverManager.getConnection(url, username, password);
    } catch (ClassNotFoundException | SQLException ex) {
      System.out.println(ex.getMessage());
    }
  }

  public Connection getConnection() {
    if (con == null) {
      connectToDatabase();
    } else {
      try {
        if (con.isClosed()) {
          connectToDatabase();
        }
      } catch (SQLException e) {
        System.err.println(e.getMessage());
      }
    }

    return con;
  }

  public synchronized static DBManager getInstance() {
    if (instancia == null) {
      instancia = new DBManager();
    }

    return instancia;
  }
}
