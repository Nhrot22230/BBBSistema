using DxnSisventas.BBBWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DxnSisventas.Views
{
  public partial class Test : System.Web.UI.Page
  {
    private ReportesAPIClient reportesAPI = new ReportesAPIClient();
    
    protected void Page_Load(object sender, EventArgs e)
    {
            byte[] pdfBytes = GetPdfFromWebService();
            if (pdfBytes != null)
            {
                string base64Pdf = Convert.ToBase64String(pdfBytes);
                pdfFrame.Attributes["src"] = "data:application/pdf;base64," + base64Pdf;
                pdfFrame.Style["display"] = "block";
            }
        }

    private byte[] GetPdfFromWebService()
    {
      byte[] pdfFile = null;
      try
      {
        pdfFile = reportesAPI.generarReporteAlmacen();
      }
      catch (System.Exception ex)
      {
        lblMessage.CssClass = "error-message";
        if (ex.Message != null)
        {
          lblMessage.Text = ex.Message;
        }
        else
        {
          lblMessage.Text = "Error al generar el reporte";
        }
        return null;
      }

      return pdfFile;
    }
  }
}