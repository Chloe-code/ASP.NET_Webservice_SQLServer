using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.ContentType = "image/JPEG"; //Application/pdf
        //Get the physical path to the file.
        string FilePath = MapPath("post.jpg");
        //Write the file directly to the HTTP content output stream.
        Response.WriteFile(FilePath);
        Response.End();
    }
}