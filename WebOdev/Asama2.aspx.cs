using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebOdev
{
    public partial class Asama2 : System.Web.UI.Page
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
            lblsonuc.Visible = true;
            ArrayList ArySonuclar = new ArrayList();
            ArrayList ArySonuclar2 = new ArrayList();

            #region Ortalama
            for (int UrlSayisi = 0; UrlSayisi < urllist.Length; UrlSayisi++)
            {
                int toplamsonuc = 0;
                var htmltotext = NUglify.Uglify.HtmlToText(wc.DownloadString(urllist[UrlSayisi]));
                for (int kelime = 0; kelime < anahtarlist.Length; kelime++)
                {
                    int sonuc = Regex.Matches(htmltotext.Code.ToLower(), anahtarlist[kelime].ToLower()).Count;
                    toplamsonuc += sonuc;
                    ArySonuclar2.Add(UrlSayisi + "," + kelime + "," + sonuc);
                    lblsonuc.Text += urllist[UrlSayisi] + " sayfasında " + "'" + anahtarlist[kelime] + "'" + " kelimesi " + sonuc + "|" + "</br>";

                }
                
                ArySonuclar.Add(toplamsonuc);
            }



            ArrayList ortalamalist = new ArrayList();
            for (int UrlSayisi = 0; UrlSayisi < urllist.Length; UrlSayisi++)
            {
                ortalamalist.Add(Convert.ToInt32(ArySonuclar[UrlSayisi]) / anahtarlist.Length);
            }
            #endregion

            #region Fark
            ArrayList farklist = new ArrayList();
            foreach (var aryin in ArySonuclar2)
            {
                string[] ayrim = aryin.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                farklist.Add(ayrim[0] + "," + ayrim[1] + "," + (Convert.ToInt32(ayrim[2]) - Convert.ToInt32(ortalamalist[Convert.ToInt32(ayrim[0])])));
            }
            #endregion

            #region Mutlak
            ArrayList mutlaklist = new ArrayList();
            foreach (var farkin in farklist)
            {
                string[] ayrim = farkin.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                mutlaklist.Add(ayrim[0] + "," + ayrim[1] + "," + Math.Abs(Convert.ToInt32(ayrim[2])));
            }
            #endregion

            #region Toplam
            ArrayList toplamlist = new ArrayList();
            foreach (var mutlaklistİn in mutlaklist)
            {
                foreach (var item in urllist)
                {
                    toplamlist.Add(0);
                }
                string[] ayrim = mutlaklistİn.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                toplamlist[Convert.ToInt32(ayrim[0])] = Convert.ToInt32(toplamlist[Convert.ToInt32(ayrim[0])]) + Convert.ToInt32(ayrim[2]);
            }
            #endregion

            #region Sonuç
            double[] toplamlistkarsilastirma = new double[urllist.Length];
            for (int urlsayısı = 0; urlsayısı < urllist.Length; urlsayısı++)
            {
                toplamlistkarsilastirma[urlsayısı] = (Convert.ToDouble(toplamlist[urlsayısı]) / Convert.ToDouble(ortalamalist[urlsayısı]));
            }


            double[] toplamlistkarsilastirma2 = new double[urllist.Length];
            for (int urlsayısı = 0; urlsayısı < urllist.Length; urlsayısı++)
            {
                toplamlistkarsilastirma2[urlsayısı] = (Convert.ToDouble(toplamlist[urlsayısı]) / Convert.ToDouble(ortalamalist[urlsayısı]));
            }
            lblsonuc.Text += "</br>";

            Array.Sort(toplamlistkarsilastirma);

            ArrayList sifirolanlar = new ArrayList();
            int count = 1;
            for (int urls = 0; urls < urllist.Length; urls++)
            {

                for (int urls2 = 0; urls2 < urllist.Length; urls2++)
                {

                    if (toplamlistkarsilastirma[urls] == toplamlistkarsilastirma2[urls2])
                    {
                        if (toplamlistkarsilastirma2[urls2] != 0)
                        {
                            lblsonuc.Text += " | " + count + "-" + urllist[urls2] + "-" + toplamlistkarsilastirma2[urls2];
                            count++;
                        }
                        else
                        {
                            sifirolanlar.Add(urllist[urls2] + "," + toplamlistkarsilastirma2[2]);
                        }

                    }
                }
            }
            #endregion

            foreach (var sifir in sifirolanlar)
            {
                string[] ayrim = sifir.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                lblsonuc.Text += " | " + count + " - " + ayrim[0] + " - " + ayrim[1];
                count++;
            }


        }

        protected void btnbul_Click(object sender, EventArgs e)
        {
            ara();
        }
    }
}