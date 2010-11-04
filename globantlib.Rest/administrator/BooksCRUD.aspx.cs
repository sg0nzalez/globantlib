using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using globantlib.Domain;

namespace globantlib.Rest.administrator
{
    public partial class BooksCRUD : System.Web.UI.Page
    {

        Business.LibraryManager libMgr = new Business.LibraryManager();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void add_edit_Click(object sender, EventArgs e)
        {

            Domain.Content content = new Domain.Content();
            content.Title = title.Value;
            content.Author = author.Value;
            content.Publisher = publisher.Value;
            content.Description = description.Value;
            content.ISBN = isbn.Value;
            content.Pages = int.Parse(pages.Value);
            content.Released = release.Value;
            foreach (Control control in form1.Controls) {
                if (control.ClientID.IndexOf("file") > 0) {
                    System.Web.UI.HtmlControls.HtmlInputFile file = (System.Web.UI.HtmlControls.HtmlInputFile)control;
                    if ((file.PostedFile != null) && file.PostedFile.ContentLength > 0) {
                        string file_name = System.IO.Path.GetFileName(file.PostedFile.FileName);
                        string save_in = Server.MapPath(@"/UploadedBooks") + "\\" + file_name;
                        string upload_dir = @"/UploadedBooks/" + file_name;
                        try
                        {
                            file.PostedFile.SaveAs(save_in);
                            string format = file.PostedFile.FileName.Substring(file.PostedFile.FileName.LastIndexOf(".")+1);
                            content.Digitals.Add(new DigitalContent()
                            {
                                Format = format,
                                Link = upload_dir

                            });
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Error:" + ex.Message);
                        }
                    
                    }
                }
                
            }



            if (action.Value == "edit")
            {
                content.ID = int.Parse(book_id.Value);
                
            }
            if (action.Value == "add") {
                libMgr.Create(content);
            }
            

            

        }








    }

}