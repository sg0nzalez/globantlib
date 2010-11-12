using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace globantlib.Rest.administrator
{
    public partial class uploadFrame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void click_image_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputFile file = (System.Web.UI.HtmlControls.HtmlInputFile)device_image;
            if ((file.PostedFile != null) && file.PostedFile.ContentLength > 0)
            {
               
                
                string format = file.PostedFile.FileName.Substring(file.PostedFile.FileName.LastIndexOf(".") + 1);
                string file_name = System.IO.Path.GetFileName(file.PostedFile.FileName);
                string save_in = Server.MapPath(@"/img/devices") + "\\" + file_name;
                
                try
                {
                    
                    file.PostedFile.SaveAs(save_in);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                }
            }
        }

        
    }
}