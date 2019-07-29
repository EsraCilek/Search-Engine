using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkScanner
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            urlLst.Items.Clear();

            string url = urlTxt.Text;

            List<string> links = Scanner.getInnerUrls(url);

            urlLst.DataSource = links;
            urlLst.DataBind();
            
        }
    }
}
