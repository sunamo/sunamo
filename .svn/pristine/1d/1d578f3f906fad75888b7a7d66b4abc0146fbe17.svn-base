using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public class SunamoPhotoGallery
{
    /// <summary>
    /// #photo
    /// </summary>
    public const string appendToPhotoUri = "#photo";
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
    public SunamoPhotoGallery(string a1upFolderUri, string a2mainPhotoUpFullPath, string a3upFolderName, string[] a4folders, string[] a5mainPhotos, string[] a6folderNames, string[] a7files, string[] a8idPhotos, string[] a9filesNames, bool a10SelectingPhotos, string a11insertBetweenPhotoAndID, string a12fullPathToPhotoInclude, string a13fotoGalerieTnFileClass, string[] a14PhotosWithName, object a15spOrRequest, string a16appendToPhotoUri, bool[] a17privateAlbums)
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
        string rightUpRoot = rightUpRoot = SunamoRoutePage.GetRightUpRoot(req);
        int foldersL = a4folders.Length;
        int mainPhotosL = a5mainPhotos.Length;
        int folderNamesL = a6folderNames.Length;
        int filesL = a7files.Length;
        int idPhotosL = a8idPhotos.Length;
        int filesNamesL = a9filesNames.Length;
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
            //
            hg.WriteTagWithAttrs("div", "class", "fotoGalerieTnBox fotoGalerieTnBoxCernePrekryti", "style", "background-image:url(" + a2mainPhotoUpFullPath + ");", "data-href", a2mainPhotoUpFullPath);

            hg.WriteTagWithAttr("div", "class", "fotoGalerieTnBoxBilyText");
            hg.WriteTag("div");
            hg.WriteRaw(a3upFolderName);
            hg.TerminateTag("div");
            hg.TerminateTag("div");

            if (string.IsNullOrEmpty(a13fotoGalerieTnFileClass))
            {
                a13fotoGalerieTnFileClass = "fotoGalerieTnFile";
            }

            hg.WriteTagWithAttr("div", "class", "fotoGalerieTnFolder");
            hg.WriteTagWithAttrs("img", "src", rightUpRoot + "img/tnFolderUp.png", "alt", "t");
            hg.TerminateTag("div");

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
            hg.WriteTagWithAttrs("div", "class", "fotoGalerieTnBox fotoGalerieTnBoxCernePrekryti", "style", "background-image:url('" + a5mainPhotos[i] + "');", "data-href", a5mainPhotos[i]);

            hg.WriteTagWithAttr("div", "class", "fotoGalerieTnBoxBilyText");
            hg.WriteTag("div");
            hg.WriteRaw(a6folderNames[i]);
            hg.TerminateTag("div");
            hg.TerminateTag("div");

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

            hg.TerminateTag("div");
            hg.TerminateTag("a");

        }
        #endregion

        #region MyRegion
        for (int i = 0; i < a9filesNames.Length; i++)
        {
            if (a7files[i].EndsWith("/Thumbs.db"))
            {
                continue;
            }
            hg.WriteTagWithAttr("div", "class", "fotoGalerieTnBox");
            string idPhoto = a8idPhotos[i];
            string idPhotoWithName = a14PhotosWithName[i];
            if (a11insertBetweenPhotoAndID == null)
            {
                hg.WriteTagWithAttr("a", "href", "Photo.aspx?pid=" + idPhoto + a16appendToPhotoUri);
            }
            else if (a11insertBetweenPhotoAndID == "")
            {
                hg.WriteTagWithAttr("a", "href", "../Photo/" + idPhoto + a16appendToPhotoUri);
            }
            else
            {
                //"Photo/"
                hg.WriteTagWithAttr("a", "href", a12fullPathToPhotoInclude + a11insertBetweenPhotoAndID.TrimEnd('/').TrimStart('/') + "/" + UH.UrlEncode(idPhotoWithName) + a16appendToPhotoUri);
            }
            if (a10SelectingPhotos)
            {
                hg.WriteNonPairTagWithAttrs("input", "type", "image", "onclick", "return selectImage('" + idPhoto + "');", "ondblclick", "return selectImage('" + idPhoto + "');",
                    "src", a7files[i], "alt", a9filesNames[i], "id", "img" + idPhoto, "class", a13fotoGalerieTnFileClass);
            }
            else
            {
                hg.WriteNonPairTagWithAttrs("img", "src", a7files[i], "alt", a9filesNames[i], "class", a13fotoGalerieTnFileClass);
            }
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

