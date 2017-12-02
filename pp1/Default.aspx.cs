using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //string st = (FileUpload1.GetRouteUrl(FileUpload1.FileName,"").ToString());
        string bb = FileUpload1.FileName;
        //  string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
        string ss = @"~\WebApplication\content\" + bb;
        Label1.Text = ss;
        FileUpload1.SaveAs(Server.MapPath(ss));
        string[] pdfFiles = Directory.GetFiles("C:\\Documents", "*.pdf");
    }
}