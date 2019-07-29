using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Text;
using System.Collections;
using HtmlAgilityPack;
using System.Threading;

namespace WebOdev
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtanahtar.Text = "ulusal\nsiber\ngüvenlik";
            //txturl.Text = "https://www.usom.gov.tr/" + "\n" + "https://www.btk.gov.tr/tr-TR/" + "\n" + "http://www.udhb.gov.tr/";
        }

        private void ara()
        {

            string[] anahtarlist = txtanahtar.Text.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] urllist = txturl.Text.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var htmltotext = NUglify.Uglify.HtmlToText(wc.DownloadString(txturl.Text));
            lblsonuc.Visible = true;
            lblsonuc.Text = "Sonuç : " + Regex.Matches(htmltotext.Code.ToLower(), txtanahtar.Text.ToLower()).Count;



            ////lblsonuc.Text += htmltotext;
            //System.Net.WebRequest Site = System.Net.HttpWebRequest.Create(urllist[0]);
            //WebResponse G = Site.GetResponse();
            //StreamReader C = new StreamReader(G.GetResponseStream());
            //string kaynak = C.ReadToEnd();
            //kaynak = NUglify.Uglify.HtmlToText(wc.DownloadString(txturl.Text));
            //kaynak = kaynak.ToLower();
            //int degisken = new Regex(Regex.Escape(anahtarlist[0])).Matches(kaynak).Count;
            //lblsonuc.Visible = true;
            //lblsonuc.Text = "Sonuç : " + degisken;


        }



















        ////lblsonuc.Text += htmltotext;
        //System.Net.WebRequest Site = System.Net.HttpWebRequest.Create(urllist[0]);
        //WebResponse G = Site.GetResponse();
        //StreamReader C = new StreamReader(G.GetResponseStream());
        //string kaynak = C.ReadToEnd();
        //lblsonuc.Visible = true;
        //lblsonuc.Text = "Sonuç : " + Regex.Matches(kaynak.ToLower(), txtanahtar.Text.ToLower()).Count;




        protected void btnbul_Click(object sender, EventArgs e)
        {
            ara();
        }
    }
}