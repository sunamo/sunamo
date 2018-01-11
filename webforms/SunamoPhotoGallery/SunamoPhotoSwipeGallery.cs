using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public class SunamoPhotoSwipeGallery
{
        HtmlGenerator hg = new HtmlGenerator();

        /// <summary>
        /// A4 a A5 jsou rozdělené aby se mi mezi ně jednodušeji vkládalo _tn
        /// A11 se vyplňuje na řetězec (klidně i prázdný) pouze u RoutedPages(je to signál pro web aby vygeneroval cestu ../Photo/... a ne Photo.aspx?...Pokud bude nastaven na neprázdný řetězec, odkaz se nastaví na ../Photo/A11/IdPhoto. Pokud se vyplní na null, galerie vygeneruje stránku jako pro stránky s QS(ne routed)
        /// A12 se ignoruje pokud A11 bude SE nebo null. Pokud se zadává, tak s Photo/ v případě routed pages nebo Photo.aspx?arg=. Ideální je nastavit A1 na / a A2 na cestu nekončící lomítkem(/). Je to sice diskriminační pro neroutované stránky Photo, ale co už...
        /// Pokud se jedná o routovatelnou stránku, musíš něco vložit(tedy ne null/SE) do A11. Pokud bude A11 null, vytvoří se cesta pro Photo.aspx ve stejné složce v jaké je teď, pokud "", vytvoří se cesta ve nadlosložce s Photo.aspx
        /// PRo neroutovatelné stránky doplň do A11 i A12 null.
        /// A12 musí být u routovatelných stránek na konci se lomítkem!
        /// </summary>
        /// <param name="a1upFolderUri">A1 je http odkaz na složku výše. Bude null pokud jsem v rootu</param>
        /// <param name="a2mainPhotoUpFullPath">A2 je plná(tzn. včetně domény a protokolu) URI k MainPhoto složky nahoru. Bude null pokud jsem v rootu</param>
        /// <param name="a3upFolderName">A3 je název složky nad aby se uživatel lépe zorientoval</param>
        /// <param name="a4folders">A4 jsou odkazy na další složky. </param>
        /// <param name="a5mainPhotos">A5 jsou titulní obrázkypod složek jako URI s _tn</param>
        /// <param name="a6folderNames">A6 jsou názvy pod složek, které slouží jako popisky</param>
        /// <param name="a7files">A7 jsou cesty k obrázkům formou URI s _tn.{ext}</param>
        /// <param name="a8idPhotos">A8 jsou ID obrázků, aby se mohli namapovat na Photo.aspx</param>
        /// <param name="a9filesNames">A9 jsou názvy souborů, které slouží jako popisky</param>
        /// <param name="a10SelectingPhotos">A10 zda jsou fotky vybírány</param>
        /// <param name="a11insertBetweenPhotoAndID">A11 se vyplňuje na řetězec (klidně i prázdný) pouze u RoutedPages(je to signál pro web aby vygeneroval cestu A12+A11 a ne Photo.aspx?...</param>
        /// <param name="a12fullPathToPhotoInclude">A12 se ignoruje pokud A11 bude SE nebo null. Pokud se zadává, tak s Photo/ v případě routed pages nebo Photo.aspx?arg=. Ideální je nastavit A1 na / a A2 na cestu nekončící lomítkem(/). Je to sice diskriminační pro neroutované stránky Photo, ale co už...</param>
        /// <param name="a13fotoGalerieTnFileClass">A13 je název css třídy v Shared.css, aby border obrázků nebyl například na casdmladez webu světle zelený, když pozadí komentářů jsou bílé. Pokud chceš výchozí barvu, nastav na SE nebo ""</param>
        /// <param name="a14PhotosWithName">Používá se u routed pages, kde tato hodnota bude bude za A12 + A11.TrimEnd('/').TrimStart('/') + "/". Tam kde nepotřebuješ název ale jen ID fotky použij A8.</param>
        public SunamoPhotoSwipeGallery(string a1upFolderUri, string a2mainPhotoUpFullPath, string a3upFolderName, string[] a4folders, string[] a5mainPhotos, string[] a6folderNames, List<string> a7files, List<string> a8idPhotos, List< string> a9filesNames, 
             string a13fotoGalerieTnFileClass, object a15spOrRequest, bool[] a17privateAlbums, string a18gid, string a19TitleAlbum, string a20uri)
        {
            HttpRequest req = null;
            
            if (a15spOrRequest is SunamoPage)
            {
                var sp = a15spOrRequest as SunamoPage;
                req = sp.Request;
            }
            else if (a15spOrRequest is HttpRequest)
            {
                req = a15spOrRequest as HttpRequest;
            }
            else
            {
                throw new Exception("Do konstruktoru třídy SunamoPhotoGallery, A15 nebyl vložen ani objekt HttpRequest, ani SunamoPage");
            }
            string rightUpRoot  = SunamoRoutePage.GetRightUpRoot(req);
            int foldersL = a4folders.Length;
            int mainPhotosL = a5mainPhotos.Length;
            int folderNamesL = a6folderNames.Length;
            int filesL = a7files.Count;
            int idPhotosL = a8idPhotos.Count;
            int filesNamesL = a9filesNames.Count;
            if ((filesL != idPhotosL) || (idPhotosL != filesNamesL) || (filesNamesL != filesL))
            {
                throw new Exception("Nesouhlasí počet názvů souborů a jejich přípon");
            }
            if ((foldersL != mainPhotosL) || (folderNamesL != foldersL) || (mainPhotosL != folderNamesL))
            {
                throw new Exception("Nesouhlasí počet složek a titulních obrázků k nim");
            }

            hg.WriteTagWithAttr("div", "id", "divPGInner");

            if (a1upFolderUri != null)
            {
                hg.WriteTagWithAttr("a", "href", a1upFolderUri);
                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnBox");

                if (string.IsNullOrEmpty(a13fotoGalerieTnFileClass))
                {
                    a13fotoGalerieTnFileClass = "fotoGalerieTnFile";
                }

                hg.WriteNonPairTagWithAttrs("img", "src", a2mainPhotoUpFullPath, "alt", a3upFolderName, "class", a13fotoGalerieTnFileClass);

                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnFolder");
                hg.WriteTagWithAttrs("img", "src", rightUpRoot + "img/tnFolderUp.png", "alt", "t");
                hg.TerminateTag("div");

                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnDesc");
                hg.WriteRaw(a3upFolderName);
                hg.TerminateTag("div");

                hg.TerminateTag("div");
                hg.TerminateTag("a");
            }

            if (string.IsNullOrEmpty(a13fotoGalerieTnFileClass))
            {
                a13fotoGalerieTnFileClass = "fotoGalerieTnFile";
            }

            #region MyRegion
            for (int i = 0; i < a4folders.Length; i++)
            {
                hg.WriteTagWithAttr("a", "href", a4folders[i]);
                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnBox");


                hg.WriteNonPairTagWithAttrs("img", "src", a5mainPhotos[i], "alt", a6folderNames[i], "class", a13fotoGalerieTnFileClass);


                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnFolder");
                if (a17privateAlbums[i])
                {
                    hg.WriteTagWithAttrs("img", "src", rightUpRoot + "img/tnFolderPrivate.png", "alt", "t");    
                }
                else
                {
                    hg.WriteTagWithAttrs("img", "src", rightUpRoot + "img/tnFolder.png", "alt", "t");
                }
                
                hg.TerminateTag("div");

                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnDesc");
                hg.WriteRaw(a6folderNames[i]);
                hg.TerminateTag("div");

                hg.TerminateTag("div");
                hg.TerminateTag("a");

            } 
            #endregion

            #region MyRegion
            for (int i = 0; i < a9filesNames.Count; i++)
            {
                if (a7files[i].EndsWith("/Thumbs.db"))
                {
                    continue;
                }
                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnBox");
                string append = "";
                if (i != 0)
                {
                    append = "#&gid=" + a18gid + "&pid=" + (i);
                }
                
                hg.WriteTagWith2Attrs("a", "href", web.UH.GetWebUri3(req, a20uri + append), "target", "_blank");

                hg.WriteNonPairTagWithAttrs("img", "src", a7files[i], "alt", a9filesNames[i], "class", a13fotoGalerieTnFileClass);
                hg.TerminateTag("a");

                hg.WriteTagWithAttr("div", "class", "fotoGalerieTnDesc");
                hg.WriteRaw(a9filesNames[i]);
                hg.TerminateTag("div");

                hg.TerminateTag("div");
            } 
            #endregion

            hg.TerminateTag("div");
        }

        public override string ToString()
        {
            return hg.ToString();
        }
}
