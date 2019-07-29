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
    public partial class Asama3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ara()
        {
            string[] anahtarlist = txtanahtar.Text.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] urllist = txturl.Text.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            ArrayList suburllist = new ArrayList();
            for (int UrlSayisi = 0; UrlSayisi < urllist.Length; UrlSayisi++)
            {
                suburllist.Add(scanner.Url(urllist[UrlSayisi]));
            }

            ArrayList fullsonuclist = new ArrayList();
            for (int anaurl = 0; anaurl < urllist.Length; anaurl++)
            {
                fullsonuclist.Add(0);
            }

            for (int anaurl = 0; anaurl < urllist.Length; anaurl++)
            {
                ArrayList sonuclist = new ArrayList();
                ArrayList alturl = (ArrayList)suburllist[anaurl];
                ArrayList sublinklist = new ArrayList();
                foreach (string item in alturl)
                {
                    lblsonuc.Visible = true;
                    if (sublinklist.Contains(item)) { }
                    else
                    {
                        if (item.Contains(scanner.getDomainName(urllist[anaurl])))
                            sublinklist.Add(item);
                    }
                }

                WebClient wc = new WebClient();
                lblsonuc.Visible = true;
                ArrayList ArySonuclar = new ArrayList();
                ArrayList ArySonuclar2 = new ArrayList();
                wc.Encoding = Encoding.UTF8;
                ArrayList hatalilar = new ArrayList();
                #region Ortalama
                for (int UrlSayisi = 0; UrlSayisi < sublinklist.Count; UrlSayisi++)
                {
                    try
                    {
                        int gercerkurlsayisi = UrlSayisi - hatalilar.Count;
                        var htmltotext = NUglify.Uglify.HtmlToText(wc.DownloadString(sublinklist[UrlSayisi].ToString()));
                        int toplamsonuc = 0;
                        for (int kelime = 0; kelime < anahtarlist.Length; kelime++)
                        {
                            int sonuc = Regex.Matches(htmltotext.Code.ToLower(), anahtarlist[kelime].ToLower()).Count;
                            toplamsonuc += sonuc;
                            ArySonuclar2.Add(gercerkurlsayisi + "," + kelime + "," + sonuc);
                        }
                        ArySonuclar.Add(toplamsonuc);
                    }
                    catch
                    {
                        hatalilar.Add(sublinklist[UrlSayisi]);

                    }
                }


                foreach (var item in hatalilar)
                {
                    sublinklist.Remove(item);
                }

                ArrayList ortalamalist = new ArrayList();
                for (int UrlSayisi = 0; UrlSayisi < (sublinklist.Count); UrlSayisi++)
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
                foreach (var subin in sublinklist)
                {
                    toplamlist.Add(0);
                }

                foreach (var mutlaklistİn in mutlaklist)
                {
                    string[] ayrim = mutlaklistİn.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    toplamlist[Convert.ToInt32(ayrim[0])] = Convert.ToInt32(toplamlist[Convert.ToInt32(ayrim[0])]) + Convert.ToInt32(ayrim[2]);
                }
                #endregion

                #region Sonuç
                double[] dizi = new double[sublinklist.Count];
                for (int i = 0; i < sublinklist.Count; i++)
                {
                    double tplm = Convert.ToDouble(toplamlist[i]);
                    double ortlm = Convert.ToDouble(ortalamalist[i]);
                    //  dizi[i] = (tplm / ortlm);
                    sonuclist.Add(tplm / ortlm);

                }
                #endregion


                int sayac = 0;
                foreach (var item in sonuclist)
                {
                    if (item.ToString() == "NaN" || item.ToString() == "∞") { }
                    else
                    {
                        fullsonuclist[anaurl] = Convert.ToDouble(sonuclist[sayac]) + Convert.ToDouble(fullsonuclist[anaurl]);

                    }

                    lblsonuc.Visible = true;
                    lblsonuc.Text += sublinklist[sayac] + " | " + item.ToString() + " </br> ";
                    sayac++;
                }
            }
            double[] sıra = new double[urllist.Length];
            double[] sıra2 = new double[urllist.Length];

            for (int anaurl = 0; anaurl < urllist.Length; anaurl++)
            {

                ArrayList alturl2 = (ArrayList)suburllist[anaurl];
                ArrayList sublinklist2 = new ArrayList();
                foreach (string item in alturl2)
                {
                    lblsonuc.Visible = true;
                    if (sublinklist2.Contains(item)) { }
                    else
                    {
                        if (item.Contains(scanner.getDomainName(urllist[anaurl])))
                            sublinklist2.Add(item);
                    }
                }
                sıra[anaurl] = Convert.ToDouble(fullsonuclist[anaurl]) / Convert.ToDouble(sublinklist2.Count);
                sıra2[anaurl] = Convert.ToDouble(fullsonuclist[anaurl]) / Convert.ToDouble(sublinklist2.Count);

            }

          
            Array.Sort(sıra);
            Array.Reverse(sıra);
            int count = 1;
            for (int urls = 0; urls < urllist.Length; urls++)
            {

                for (int urls2 = 0; urls2 < urllist.Length; urls2++)
                {

                    if (sıra[urls] == sıra2[urls2])
                    {

                        lblsonuc.Text += " | " + count + "-" + urllist[urls2] + "-" + sıra2[urls2];
                        count++;


                    }
                }
            }

        }


        protected void btnbul_Click(object sender, EventArgs e)
        {
            ara();
        }
    }
}