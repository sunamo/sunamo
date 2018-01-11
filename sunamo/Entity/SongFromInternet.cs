using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
namespace sunamo
{
    public class SongFromInternet
    {
         List<string> nazev = new List<string>();
         List<string> title = new List<string>();
         List<string> remix = new List<string>();
         List<string> nazevWoDiacritic = new List<string>();
         List<string> titleWoDiacritic = new List<string>();
         List<string> remixWoDiacritic = new List<string>();

        string _artist = null;
        string _title = null;
        string _remix = null;

        public string ArtistC
        {
            get
            {
                return _artist;
            }
        }

        public string TitleC
        {
            get
            {
                return _title;
            }
        }

        public string RemixC
        {
            get
            {
                return _remix;
            }
        }

        public SongFromInternet(string song)
        {
            string n, t, r;
            ManageArtistDashTitle.GetArtistTitleRemix(song, out n, out t, out r);
            this.nazev.AddRange(SplitNazevTitle(n));
            this.title.AddRange(SplitNazevTitle(t));
            this.remix.AddRange(SplitRemix(r));

            this.nazevWoDiacritic = CA.WithoutDiacritic(this.nazev);
            this.titleWoDiacritic = CA.WithoutDiacritic(this.title);
            this.remixWoDiacritic = CA.WithoutDiacritic(this.remix);

            SetInConvention();
        }



        public SongFromInternet(SongFromInternet item2)
        {
            // TODO: Complete member initialization
            nazev = new List<string>(item2.nazev);
            title = new List<string>(item2.title);
            remix = new List<string>(item2.remix);
            SetInConvention();
        }

        private void SetInConvention()
        {
            _artist = ArtistInConvention();
            _title = TitleInConvention();
            _remix = RemixInConvention();
        }

        /// <summary>
        /// Porovnává s ohledem na diakritiku
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public float CalculateSimilarity(string p)
        {
            SongFromInternet s = new SongFromInternet(p);
            return CalculateSimilarity(s, false);
        }

        public float CalculateSimilarity(SongFromInternet s, bool woDiacritic)
        {
            float n, t, r;
            if (woDiacritic)
            {
               
                int psn, prn, pst, prt, psr, prr;
                VratPocetStejnychARozdilnych(s.nazev, this.nazevWoDiacritic, out psn, out prn);
                VratPocetStejnychARozdilnych(s.title, this.titleWoDiacritic, out pst, out prt);
                VratPocetStejnychARozdilnych(s.remix, this.remixWoDiacritic, out psr, out prr);

                n = CalculateSimilarity(psn, prn, s.nazev, this.nazevWoDiacritic);
                t = CalculateSimilarity(pst, prt, s.title, this.titleWoDiacritic);
                r = CalculateSimilarity(psr, prr, s.remix, this.remixWoDiacritic);
            }
            else
            {
                int psn, prn, pst, prt, psr, prr;
                VratPocetStejnychARozdilnych(s.nazev, this.nazev, out psn, out prn);
                VratPocetStejnychARozdilnych(s.title, this.title, out pst, out prt);
                VratPocetStejnychARozdilnych(s.remix, this.remix, out psr, out prr);

                n = CalculateSimilarity(psn, prn, s.nazev, this.nazev);
                t = CalculateSimilarity(pst, prt, s.title, this.title);
                r = CalculateSimilarity(psr, prr, s.remix, this.remix);
            }

            float vr = (n + t) / 2;
            if (vr == 1)
            {
                vr = (n + t + r) / 3;
            }
            return vr;
        }

        private float CalculateSimilarity(int psn, int prn, List<string> novy, List<string> puvodni)
        {
            if (psn == prn && psn == 0)
            {
                return 1.0f;
            }
            if (psn > prn)
            {
                //prn == 1 ||
                if (prn == 0)
                {
                    int uy = 0;
                    for (int i = 0; i < novy.Count; i++)
                    {
                        if (puvodni.Contains(novy[i]))
                        {
                            uy++;
                        }
                    }
                    if (uy == novy.Count) // - 1
                    {
                        return 1.0f;
                    }
                    //return 1.0f;
                    return (1f / 3f) * 2f;
                }
                if (prn != 0)
                {
                    if (prn != 1)
                    {

                        return psn / prn;
                    }
                    if (psn > 3)
                    {
                        float vr = (psn - prn) / 2f;
                        while (vr > 1f)
                        {
                            vr /= 2f;
                        }
                        return vr;
                    }
                    else
                    {
                        float vr = (psn - ((float)psn / ((float)psn - 1f))) / 2;
                        int uy = 0;
                        for (int i = 0; i < novy.Count; i++)
                        {
                            if (puvodni.Contains(novy[i]))
                            {
                                uy++;
                            }
                        }
                        if (uy > 0)
                        {
                            vr = (float)uy / (float)prn / 2f;
                        }
                        if (vr > 0.99f)
                        {
                            vr = vr / 2f;
                        }
                        return vr;
                    }
                    //return 0.5f;
                }


                return 0f;
            }
            if (psn + 1 > prn && psn < 3)
            {
                return 0.5f;
            }

            return 0f;

        }

        public string Artist()
        {
            return SH.JoinSpace(nazev);
        }

        public string ArtistInConvention()
        {
            return ConvertEveryWordLargeCharConvention.ToConvention(Artist());
        }

        public string Title()
        {
            return SH.JoinSpace(title);
        }

        public string TitleInConvention()
        {
            return ConvertEveryWordLargeCharConvention.ToConvention(Title());
        }

        public string Remix()
        {
            return SH.JoinSpace(remix);
        }

        public string RemixInConvention()
        {
            return ConvertEveryWordLargeCharConvention.ToConvention(Remix());
        }

        public string TitleAndRemixInConvention()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(TitleInConvention());
            if (remix.Count != 0)
            {
                sb.Append("[" + RemixInConvention() + "]");
            }
            return sb.ToString();
        }

        private void VratPocetStejnychARozdilnych(List<string> list, List<string> list_2, out int psn, out int prn)
        {
            List<string> l1 = new List<string>(list.ToArray());
            List<string> l2 = new List<string>(list_2.ToArray());
            psn = 0;


            for (int i = l1.Count - 1; i >= 0; i--)
            {
                int dex = l2.IndexOf(l1[i]);
                if (dex != -1)
                {
                    l1.RemoveAt(i);
                    l2.RemoveAt(dex);
                    psn++;
                }
            }

            prn = (l1.Count + l2.Count);
        }

        public override string ToString()
        {
            StringBuilder vr = new StringBuilder();
            vr.Append(Artist() + "-" + Title());
            if (remix.Count != 0)
            {
                vr.Append(" [" + Remix() + "]");
            }
            return vr.ToString();
        }

        public  string ToConventionString()
        {
            StringBuilder vr = new StringBuilder();
            vr.Append(ArtistInConvention() + "-" + TitleInConvention());
            if (remix.Count != 0)
            {
                vr.Append(" [" + RemixInConvention() + "]");
            }
            return vr.ToString();
        }

        private IEnumerable<string> SplitRemix(string u)
        {
            List<string> gg = u.Split(new string[] { " ", ",", "-", "[", "]", "(", ")" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //gg.ForEach(g => g.ToLower());
            for (int i = 0; i < gg.Count; i++)
            {
                gg[i] = gg[i].ToLower();
            }
            return gg;
        }

        private IEnumerable<string> SplitNazevTitle(string u)
        {
            List<string> gg = u.Split(new string[] { " ", ",", "-" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //gg.ForEach(g => g.ToLower());
            for (int i = 0; i < gg.Count; i++)
            {
                gg[i] = gg[i].ToLower();
            }
            return gg;
        }
    }
}
