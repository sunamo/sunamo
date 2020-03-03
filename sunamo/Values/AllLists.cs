﻿using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Values
{
    public class AllLists
    {
        public static List<string> leftBrackets = CA.ToListString(AllStrings.lb, AllStrings.cbl, AllStrings.lsf);
        public static List<string> rightBrackets = CA.ToListString(AllStrings.rb, AllStrings.cbr, AllStrings.rsf);
        public static List<string> featUpper = CA.ToListString("Feat.", "Featuring", "Ft.");
        public static List<string> featLower = CA.ToListString("feat.", "featuring", "ft.");
        public static List<string> OstravaCityParts = null;
        public static List<string> HtmlNonPairTags = CA.ToListString("area", "base", "br", "col", "embed", "hr", "img", "input", "link", "meta", "param", "source", "track", "wbr");
        public static List<string> PairingTagsDontWrapToParagraph = CA.ToListString("h1", "h2", "h3", "h4", "h5", "h6", "ul", "ol", "li");
        /// <summary>
        /// generate from https://www.w3.org/Style/CSS/all-properties.en.html at 7-6-2018
        /// </summary>
        public static List<string> allCssKeys = CA.ToListString("property", "align-content", "align-items", "align-self", "alignment-adjust", "alignment-baseline", "all", "animation", "animation-delay", "animation-direction", "animation-duration", "animation-fill-mode", "animation-iteration-count", "animation-name", "animation-play-state", "animation-timing-function", "appearance", "azimuth", "backface-visibility", "background", "background-attachment", "background-blend-mode", "background-clip", "background-color", "background-image", "background-origin", "background-position", "background-repeat", "background-size", "baseline-shift", "baseline-source", "block-ellipsis", "block-overflow", "block-size", "block-step", "block-step-align", "block-step-insert", "block-step-round", "block-step-size", "bookmark-label", "bookmark-level", "bookmark-state", "border", "border-block", "border-block-color", "border-block-end", "border-block-end-color", "border-block-end-style", "border-block-end-width", "border-block-start", "border-block-start-color", "border-block-start-style", "border-block-start-width", "border-block-style", "border-block-width", "border-bottom", "border-bottom-color", "border-bottom-fit-length", "border-bottom-fit-width", "border-bottom-image", "border-bottom-left-fit-width", "border-bottom-left-image", "border-bottom-left-radius", "border-bottom-right-fit-length", "border-bottom-right-fit-width", "border-bottom-right-image", "border-bottom-right-radius", "border-bottom-style", "border-bottom-width", "border-bottoml-eft-fit-length", "border-boundary", "border-break", "border-collapse", "border-color", "border-corner-fit", "border-corner-image", "border-corner-image-transform", "border-end-end-radius", "border-end-start-radius", "border-fit", "border-fit-length", "border-fit-width", "border-image", "border-image-outset", "border-image-repeat", "border-image-slice", "border-image-source", "border-image-transform", "border-image-width", "border-inline", "border-inline-color", "border-inline-end", "border-inline-end-color", "border-inline-end-style", "border-inline-end-width", "border-inline-start", "border-inline-start-color", "border-inline-start-style", "border-inline-start-width", "border-inline-style", "border-inline-width", "border-left", "border-left-color", "border-left-fit-length", "border-left-fit-width", "border-left-image", "border-left-style", "border-left-width", "border-radius", "border-right", "border-right-color", "border-right-fit-length", "border-right-fit-width", "border-right-image", "border-right-style", "border-right-width", "border-spacing", "border-start-end-radius", "border-start-start-radius", "border-style", "border-top", "border-top-color", "border-top-fit-length", "border-top-fit-width", "border-top-image", "border-top-left-fit-length", "border-top-left-fit-width", "border-top-left-image", "border-top-left-radius", "border-top-right-fit-length", "border-top-right-fit-width", "border-top-right-image", "border-top-right-radius", "border-top-style", "border-top-width", "border-width", "bottom", "box-decoration-break", "box-shadow", "box-sizing", "box-snap", "break-after", "break-before", "break-inside", "caption-side", "caret", "caret-color", "caret-shape", "chains", "clear", "clip", "clip-path", "clip-rule", "color", "color-adjust", "color-interpolation-filters", "color-scheme", "column-count", "column-fill", "column-gap", "column-rule", "column-rule-color", "column-rule-style", "column-rule-width", "column-span", "column-width", "columns", "contain", "content", "continue", "counter-increment", "counter-reset", "counter-set", "cue", "cue-after", "cue-before", "cursor", "direction", "display", "dominant-baseline", "drop-initial-after-adjust", "drop-initial-after-align", "drop-initial-before-adjust", "drop-initial-before-align", "drop-initial-size", "drop-initial-value", "elevation", "empty-cells", "fill", "fill-break", "fill-color", "fill-image", "fill-opacity", "fill-origin", "fill-position", "fill-repeat", "fill-rule", "fill-size", "filter", "flex", "flex-basis", "flex-direction", "flex-flow", "flex-grow", "flex-shrink", "flex-wrap", "float", "float-defer", "float-offset", "float-reference", "flood-color", "flood-opacity", "flow", "flow-from", "flow-into", "font", "font-family", "font-feature-settings", "font-kerning", "font-language-override", "font-max-size", "font-min-size", "font-optical-sizing", "font-palette", "font-size", "font-size-adjust", "font-stretch", "font-style", "font-synthesis", "font-synthesis-small-caps", "font-synthesis-style", "font-synthesis-weight", "font-variant", "font-variant-alternates", "font-variant-caps", "font-variant-east-asian", "font-variant-emoji", "font-variant-ligatures", "font-variant-numeric", "font-variant-position", "font-variation-settings", "font-weight", "footnote-display", "footnote-policy", "forced-color-adjust", "gap", "glyph-orientation-vertical", "grid", "grid-area", "grid-auto-columns", "grid-auto-flow", "grid-auto-rows", "grid-column", "grid-column-end", "grid-column-start", "grid-row", "grid-row-end", "grid-row-start", "grid-template", "grid-template-areas", "grid-template-columns", "grid-template-rows", "hanging-punctuation", "height", "hyphenate-character", "hyphenate-limit-chars", "hyphenate-limit-last", "hyphenate-limit-lines", "hyphenate-limit-zone", "hyphens", "image-orientation", "image-rendering", "image-resolution", "initial-letters", "initial-letters-align", "initial-letters-wrap", "inline-box-align", "inline-size", "inline-sizing", "inset", "inset-after", "inset-before", "inset-block", "inset-block-end", "inset-block-start", "inset-end", "inset-inline", "inset-inline-end", "inset-inline-start", "inset-start", "isolation", "justify-content", "justify-items", "justify-self", "leading-trim", "leading-trim-over", "leading-trim-under", "left", "letter-spacing", "lighting-color", "line-break", "line-clamp", "line-grid", "line-height", "line-height-step", "line-padding", "line-sizing", "line-snap", "line-stacking", "line-stacking-ruby", "line-stacking-shift", "line-stacking-strategy", "list-style", "list-style-image", "list-style-position", "list-style-type", "margin", "margin-block", "margin-block-end", "margin-block-start", "margin-bottom", "margin-break", "margin-inline", "margin-inline-end", "margin-inline-start", "margin-left", "margin-right", "margin-top", "margin-trim", "marker", "marker-end", "marker-knockout-left", "marker-knockout-right", "marker-mid", "marker-pattern", "marker-segment", "marker-side", "marker-start", "mask", "mask-border", "mask-border-mode", "mask-border-outset", "mask-border-repeat", "mask-border-slice", "mask-border-source", "mask-border-width", "mask-clip", "mask-composite", "mask-image", "mask-mode", "mask-origin", "mask-position", "mask-repeat", "mask-size", "mask-type", "max-block-size", "max-height", "max-inline-size", "max-lines", "max-width", "min-block-size", "min-height", "min-inline-size", "min-width", "mix-blend-mode", "nav-down", "nav-left", "nav-right", "nav-up", "object-fit", "object-position", "offset", "offset-after", "offset-anchor", "offset-before", "offset-distance", "offset-end", "offset-path", "offset-position", "offset-rotate", "offset-start", "opacity", "order", "orphans", "outline", "outline-color", "outline-offset", "outline-style", "outline-width", "overflow", "overflow-block", "overflow-inline", "overflow-wrap", "overflow" + "-" + "x", "overflow" + "-" + "y", "overscroll-behavior", "overscroll-behavior-block", "overscroll-behavior-inline", "overscroll-behavior" + "-" + "", "overscroll-behavior" + "-" + "", "padding", "padding-block", "padding-block-end", "padding-block-start", "padding-bottom", "padding-inline", "padding-inline-end", "padding-inline-start", "padding-left", "padding-right", "padding-top", "page", "page-break-after", "page-break-before", "page-break-inside", "pause", "pause-after", "pause-before", "perspective", "perspective-origin", "pitch", "pitch-range", "place-content", "place-items", "place-self", "play-during", "position", "quotes", "region-fragment", "resize", "richness", "right", "row-gap", "ruby-align", "ruby-merge", "ruby-position", "running", "scroll-behavior", "scroll-margin", "scroll-margin-block", "scroll-margin-block-end", "scroll-margin-block-start", "scroll-margin-bottom", "scroll-margin-inline", "scroll-margin-inline-end", "scroll-margin-inline-start", "scroll-margin-left", "scroll-margin-right", "scroll-margin-top", "scroll-padding", "scroll-padding-block", "scroll-padding-block-end", "scroll-padding-block-start", "scroll-padding-bottom", "scroll-padding-inline", "scroll-padding-inline-end", "scroll-padding-inline-start", "scroll-padding-left", "scroll-padding-right", "scroll-padding-top", "scroll-snap-align", "scroll-snap-stop", "scroll-snap-type", "scrollbar-color", "scrollbar-gutter", "scrollbar-width", "shape-image-threshold", "shape-inside", "shape-margin", "shape-outside", "spatial-navigation-action", "spatial-navigation-contain", "speak", "speak-header", "speak-numeral", "speak-punctuation", "speech-rate", "stress", "string-set", "stroke", "stroke-align", "stroke-alignment", "stroke-break", "stroke-color", "stroke-dash-corner", "stroke-dash-justify", "stroke-dashadjust", "stroke-dasharray", "stroke-dashcorner", "stroke-dashoffset", "stroke-image", "stroke-linecap", "stroke-linejoin", "stroke-miterlimit", "stroke-opacity", "stroke-origin", "stroke-position", "stroke-repeat", "stroke-size", "stroke-width", "tab-size", "table-layout", "text-align", "text-align-all", "text-align-last", "text-combine-upright", "text-decoration", "text-decoration-color", "text-decoration-line", "text-decoration-skip", "text-decoration-skip-ink", "text-decoration-style", "text-decoration-width", "text-emphasis", "text-emphasis-color", "text-emphasis-position", "text-emphasis-skip", "text-emphasis-style", "text-group-align", "text-height", "text-indent", "text-justify", "text-orientation", "text-overflow", "text-shadow", "text-space-collapse", "text-space-trim", "text-spacing", "text-transform", "text-underline-offset", "text-underline-position", "text-wrap", "top", "transform", "transform-box", "transform-origin", "transform-style", "transition", "transition-delay", "transition-duration", "transition-property", "transition-timing-function", "unicode-bidi", "user-select", "vertical-align", "visibility", "voice-family", "volume", "white-space", "widows", "width", "will-change", "word-break", "word-spacing", "word-wrap", "wrap-after", "wrap-before", "wrap-flow", "wrap-inside", "wrap-through", "writing-mode", "-" + "-" + "index");
        /// <summary>
        /// https://en.wikipedia.org/wiki/Generic_top-level_domain
        /// </summary>
        public static List<string> genericDomains = CA.ToListString("." + "com", "." + "org", ".net");

        public static List<string> htmlEntities = null;
        public static readonly List<string> BasicImageExtensions = CA.ToListString(AllExtensions.png,
           AllExtensions.bmp,
           AllExtensions.jpg,
           AllExtensions.jpeg);
        public static readonly List<string> cssTemplatesSites = new List<string>(CA.ToListString("justfreetemplates.com", "templatemo.com", "free-css.com", "templated.co", "w3layouts.com"));
        public static readonly List<string> numberPoints = new List<string> { AllStrings.comma, AllStrings.dot };

        static AllLists()
        {
            htmlEntities = CA.ToListString("excl", "quot", "QUOT", "num", "dollar", "percnt", "amp", "AMP", "apos", "lpar", "rpar", "ast", "midast", "add", "comma", "period", "sol", "colon", "semi", "lt", "LT", "equals", "gt", "GT", "quest", "commat", "lsqb", "lbrack", "bsol", "rsqb", "rbrack", "Hat", "lowbar", "grave", "DiacriticalGrave", "lcub", "lbrace", "verbar", "vert", "VerticalLine", "rcub", "rbrace", "nbsp", "NonBreakingSpace", "iexcl", "cent", "pound", "curren", "yen", "brvbar", "sect", "Dot", "die", "DoubleDot", "uml", "copy", "COPY", "ordf", "laquo", "not", "shy", "reg", "circledR", "REG", "macr", "OverBar", "strns", "deg", "plusmn", "pm", "PlusMinus", "sup2", "sup3", "acute", "DiacriticalAcute", "micro", "para", "middot", "centerdot", "CenterDot", "cedil", "Cedilla", "sup1", "ordm", "raquo", "frac14", "frac12", "half", "frac34", "iquest", "Agrave", "Aacute", "Acirc", "Atilde", "Auml", "Aring", "AElig", "Ccedil", "Egrave", "Eacute", "Ecirc", "Euml", "Igrave", "Iacute", "Icirc", "Iuml", "ETH", "Ntilde", "Ograve", "Oacute", "Ocirc", "Otilde", "Ouml", "times", "Oslash", "Ugrave", "Uacute", "Ucirc", "Uuml", "Yacute", "THORN", "szlig", "agrave", "aacute", "acirc", "atilde", "auml", "aring", "aelig", "ccedil", "egrave", "eacute", "ecirc", "euml", "igrave", "iacute", "icirc", "iuml", "eth", "ntilde", "ograve", "oacute", "ocirc", "otilde", "ouml", "divide", "oslash", "ugrave", "uacute", "ucirc", "uuml", "yacute", "thorn", "yuml", "Amacr", "amacr", "Abreve", "abreve", "Aogon", "aogon", "Cacute", "cacute", "Ccirc", "ccirc", "Cdot", "cdot", "Ccaron", "ccaron", "Dcaron", "dcaron", "Dstrok", "dstrok", "Emacr", "emacr", "Edot", "edot", "Eogon", "eogon", "Ecaron", "ecaron", "Gcirc", "gcirc", "Gbreve", "gbreve", "Gdot", "gdot", "Gcedil", "Hcirc", "hcirc", "Hstrok", "hstrok", "Itilde", "itilde", "Imacr", "imacr", "Iogon", "iogon", "Idot", "imath", "inodot", "IJlig", "ijlig", "Jcirc", "jcirc", "Kcedil", "kcedil", "kgreen", "Lacute", "lacute", "Lcedil", "lcedil", "Lcaron", "lcaron", "Lmidot", "lmidot", "Lstrok", "lstrok", "Nacute", "nacute", "Ncedil", "ncedil", "Ncaron", "ncaron", "napos", "ENG", "eng", "Omacr", "omacr", "Odblac", "odblac", "OElig", "oelig", "Racute", "racute", "Rcedil", "rcedil", "Rcaron", "rcaron", "Sacute", "sacute", "Scirc", "scirc", "Scedil", "scedil", "Scaron", "scaron", "Tcedil", "tcedil", "Tcaron", "tcaron", "Tstrok", "tstrok", "Utilde", "utilde", "Umacr", "umacr", "Ubreve", "ubreve", "Uring", "uring", "Udblac", "udblac", "Uogon", "uogon", "Wcirc", "wcirc", "Ycirc", "ycirc", "Yuml", "Zacute", "zacute", "Zdot", "zdot", "Zcaron", "zcaron", "fnof", "imped", "gacute", "jmath", "circ", "caron", "Hacek", "breve", "Breve", "dot", "DiacriticalDot", "ring", "ogon", "tilde", "DiacriticalTilde", "dblac", "DiacriticalDoubleAcute", "DownBreve", "UnderBar", "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega", "alpha", "beta", "gamma", "delta", "epsilon", "epsiv", "varepsilon", "zeta", "eta", "theta", "iota", "kappa", "lambda", "mu", "nu", "xi", "omicron", "pi", "rho", "sigmaf", "sigmav", "varsigma", "sigma", "tau", "upsilon", "upsi", "phi", "chi", "psi", "omega", "thetasym", "thetav", "vartheta", "upsih", "Upsi", "straightphi", "piv", "varpi", "Gammad", "gammad", "digamma", "kappav", "varkappa", "rhov", "varrho", "epsi", "straightepsilon", "bepsi", "backepsilon", "IOcy", "DJcy", "GJcy", "Jukcy", "DScy", "Iukcy", "YIcy", "Jsercy", "LJcy", "NJcy", "TSHcy", "KJcy", "Ubrcy", "DZcy", "Acy", "Bcy", "Vcy", "Gcy", "Dcy", "IEcy", "ZHcy", "Zcy", "Icy", "Jcy", "Kcy", "Lcy", "Mcy", "Ncy", "Ocy", "Pcy", "Rcy", "Scy", "Tcy", "Ucy", "Fcy", "KHcy", "TScy", "CHcy", "SHcy", "SHCHcy", "HARDcy", "Ycy", "SOFTcy", "Ecy", "YUcy", "YAcy", "acy", "bcy", "vcy", "gcy", "dcy", "iecy", "zhcy", "zcy", "icy", "jcy", "kcy", "lcy", "mcy", "ncy", "ocy", "pcy", "rcy", "scy", "tcy", "ucy", "fcy", "khcy", "tscy", "chcy", "shcy", "shchcy", "hardcy", "ycy", "softcy", "ecy", "yucy", "yacy", "iocy", "djcy", "gjcy", "jukcy", "dscy", "iukcy", "yicy", "jsercy", "ljcy", "njcy", "tshcy", "kjcy", "ubrcy", "dzcy", "ensp", "emsp", "emsp13", "emsp14", "numsp", "puncsp", "thinsp", "ThinSpace", "hairsp", "VeryThinSpace", "ZeroWidthSpace", "NegativeVeryThinSpace", "NegativeThinSpace", "NegativeMediumSpace", "NegativeThickSpace", "zwnj", "zwj", "lrm", "rlm", "hyphen", "dash", "ndash", "mdash", "horbar", "Verbar", "Vert", "lsquo", "OpenCurlyQuote", "rsquo", "rsquor", "CloseCurlyQuote", "sbquo", "lsquor", "ldquo", "OpenCurlyDoubleQuote", "rdquo", "rdquor", "CloseCurlyDoubleQuote", "bdquo", "ldquor", "dagger", "Dagger", "ddagger", "bull", "bullet", "nldr", "hellip", "mldr", "permil", "pertenk", "prime", "Prime", "tprime", "bprime", "backprime", "lsaquo", "rsaquo", "oline", "caret", "hybull", "frasl", "bsemi", "qprime", "MediumSpace", "NoBreak", "ApplyFunction", "af", "InvisibleTimes", "it", "InvisibleComma", "ic", "euro", "tdot", "TripleDot", "DotDot", "Copf", "complexes", "incare", "gscr", "hamilt", "HilbertSpace", "Hscr", "Hfr", "PoincarePlane", "quaternions", "Hopf", "planckh", "planck", "hbar", "plankv", "hslash", "Iscr", "imagline", "image", "Im", "imagpart", "Ifr", "Lscr", "lagram", "Laplacetrf", "ell", "Nopf", "naturals", "numero", "copysr", "weierp", "wp", "Popf", "primes", "rationals", "Qopf", "Rscr", "realine", "real", "Re", "realpart", "Rfr", "reals", "Ropf", "rx", "trade", "integers", "Zopf", "ohm", "mho", "Zfr", "zeetrf", "iiota", "angst", "bernou", "Bernoullis", "Bscr", "Cfr", "Cayleys", "escr", "Escr", "expectation", "Fscr", "Fouriertrf", "phmmat", "Mscr", "Mellintrf", "order", "orderof", "oscr", "alefsym", "aleph", "beth", "gimel", "daleth", "CapitalDifferentialD", "DD", "DifferentialD", "dd", "ExponentialE", "exponentiale", "ee", "ImaginaryI", "ii", "frac13", "frac23", "frac15", "frac25", "frac35", "frac45", "frac16", "frac56", "frac18", "frac38", "frac58", "frac78", "larr", "leftarrow", "LeftArrow", "slarr", "ShortLeftArrow", "uarr", "uparrow", "UpArrow", "ShortUpArrow", "rarr", "rightarrow", "RightArrow", "srarr", "ShortRightArrow", "darr", "downarrow", "DownArrow", "ShortDownArrow", "harr", "leftrightarrow", "LeftRightArrow", "varr", "updownarrow", "UpDownArrow", "nwarr", "UpperLeftArrow", "nwarrow", "nearr", "UpperRightArrow", "nearrow", "searr", "searrow", "LowerRightArrow", "swarr", "swarrow", "LowerLeftArrow", "nlarr", "nleftarrow", "nrarr", "nrightarrow", "rarrw", "rightsquigarrow", "Larr", "twoheadleftarrow", "Uarr", "Rarr", "twoheadrightarrow", "Darr", "larrtl", "leftarrowtail", "rarrtl", "rightarrowtail", "LeftTeeArrow", "mapstoleft", "UpTeeArrow", "mapstoup", "map", "RightTeeArrow", "mapsto", "DownTeeArrow", "mapstodown", "larrhk", "hookleftarrow", "rarrhk", "hookrightarrow", "larrlp", "looparrowleft", "rarrlp", "looparrowright", "harrw", "leftrightsquigarrow", "nharrow", "nleftrightarrow", "lsh", "Lsh", "rsh", "Rsh", "ldsh", "rdsh", "crarr", "cularr", "curvearrowleft", "curarr", "curvearrowright", "olarr", "circlearrowleft", "orarr", "circlearrowright", "lharu", "LeftVector", "leftharpoonup", "lhard", "leftharpoondown", "DownLeftVector", "uharr", "upharpoonright", "RightUpVector", "uharl", "upharpoonleft", "LeftUpVector", "rharu", "RightVector", "rightharpoonup", "rhard", "rightharpoondown", "DownRightVector", "dharr", "RightDownVector", "downharpoonright", "dharl", "LeftDownVector", "downharpoonleft", "rlarr", "rightleftarrows", "RightArrowLeftArrow", "udarr", "UpArrowDownArrow", "lrarr", "leftrightarrows", "LeftArrowRightArrow", "llarr", "leftleftarrows", "uuarr", "upuparrows", "rrarr", "rightrightarrows", "ddarr", "downdownarrows", "lrhar", "ReverseEquilibrium", "leftrightharpoons", "rlhar", "Equilibrium", "rightleftharpoons", "nlArr", "nLeftArrow", "nhArr", "nLeftrightarrow", "nrArr", "nRightArrow", "lArr", "Leftarrow", "DoubleLeftArrow", "uArr", "Uparrow", "DoubleUpArrow", "rArr", "Rightarrow", "Implies", "DoubleRightArrow", "dArr", "Downarrow", "DoubleDownArrow", "hArr", "Leftrightarrow", "DoubleLeftRightArrow", "iff", "vArr", "Updownarrow", "DoubleUpDownArrow", "nwArr", "neArr", "seArr", "swArr", "lAarr", "Lleftarrow", "rAarr", "Rrightarrow", "zigrarr", "larrb", "LeftArrowBar", "rarrb", "RightArrowBar", "duarr", "DownArrowUpArrow", "loarr", "roarr", "hoarr", "forall", "ForAll", "comp", "complement", "part", "PartialD", "exist", "Exists", "nexist", "NotExists", "nexists", "empty", "emptyset", "emptyv", "varnothing", "nabla", "Del", "isin", "isinv", "Element", "in", "notin", "NotElement", "notinva", "ni", "niv", "ReverseElement", "SuchThat", "notni", "notniva", "NotReverseElement", "prod", "Product", "coprod", "Coproduct", "sum", "Sum", "minus", "mnplus", "mp", "MinusPlus", "plusdo", "dotplus", "setmn", "setminus", "Backslash", "ssetmn", "smallsetminus", "lowast", "compfn", "SmallCircle", "radic", "Sqrt", "prop", "propto", "Proportional", "vprop", "varpropto", "infin", "angrt", "ang", "angle", "angmsd", "measuredangle", "angsph", "mid", "VerticalBar", "smid", "shortmid", "nmid", "NotVerticalBar", "nsmid", "nshortmid", "par", "parallel", "DoubleVerticalBar", "spar", "shortparallel", "npar", "nparallel", "NotDoubleVerticalBar", "nspar", "nshortparallel", "and", "wedge", "or", "vee", "cap", "cup", "int", "Integral", "int", "Integral", "tint", "iiint", "conint", "oint", "ContourIntegral", "Conint", "DoubleContourIntegral", "Cconint", "cwint", "cwconint", "ClockwiseContourIntegral", "awconint", "CounterClockwiseContourIntegral", "there4", "therefore", "Therefore", "becaus", "because", "Because", "ratio", "Colon", "Proportion", "minusd", "dotminus", "mDDot", "homtht", "sim", "Tilde", "thksim", "thicksim", "bsim", "backsim", "ac", "mstpos", "acd", "wreath", "VerticalTilde", "wr", "nsim", "NotTilde", "esim", "EqualTilde", "eqsim", "sime", "TildeEqual", "simeq", "nsime", "nsimeq", "NotTildeEqual", "cong", "TildeFullEqual", "simne", "ncong", "NotTildeFullEqual", "asymp", "ap", "TildeTilde", "approx", "thkap", "thickapprox", "nap", "NotTildeTilde", "napprox", "ape", "approxeq", "apid", "bcong", "backcong", "asympeq", "CupCap", "bump", "HumpDownHump", "Bumpeq", "bumpe", "HumpEqual", "Humpeq", "esdot", "DotEqual", "doteq", "edot", "doteqdot", "efdot", "fallingdotseq", "erdot", "risingdotseq", "colone", "coloneq", "Assign", "ecolon", "eqcolon", "ecir", "eqcirc", "cire", "circeq", "wedgeq", "veeeq", "trie", "triangleq", "equest", "questeq", "ne", "NotEqual", "equiv", "Congruent", "nequiv", "NotCongruent", "le", "LessEqual", "leq", "ge", "GreaterEqual", "geq", "lE", "LessFullEqual", "leqq", "gE", "GreaterFullEqual", "geqq", "lnE", "lneqq", "gnE", "gneqq", "Lt", "NestedLessLess", "ll", "Gt", "NestedGreaterGreater", "gg", "twixt", "between", "NotCupCap", "nlt", "NotLess", "nless", "ngt", "NotGreater", "ngtr", "nle", "NotLessEqual", "nleq", "nge", "NotGreaterEqual", "ngeq", "lsim", "LessTilde", "lesssim", "gsim", "GreaterTilde", "gtrsim", "nlsim", "NotLessTilde", "ngsim", "NotGreaterTilde", "lg", "lessgtr", "LessGreater", "gl", "gtrless", "GreaterLess", "ntlg", "NotLessGreater", "ntgl", "GreaterLess", "pr", "Precedes", "prec", "sc", "Succeeds", "succ", "prcue", "PrecedesSlantEqual", "preccurlyeq", "sccue", "SucceedsSlantEqual", "succcurlyeq", "prsim", "precsim", "PrecedesTilde", "sccue", "SucceedsSlantEqual", "succcurlyeq", "npr", "nprec", "NotPrecedes", "nsc", "nsucc", "NotSucceeds", "sub", "subset", "sup", "supset", "Superset", "nsub", "nsup", "sube", "SubsetEqual", "subseteq", "supe", "supseteq", "SupersetEqual", "nsube", "nsubseteq", "NotSubsetEqual", "nsupe", "nsupseteq", "NotSupersetEqual", "subne", "subsetneq", "supne", "supsetneq", "cupdot", "uplus", "UnionPlus", "sqsub", "SquareSubset", "sqsubset", "sqsup", "SquareSuperset", "sqsupset", "sqsube", "SquareSubsetEqual", "sqsubseteq", "sqsupe", "SquareSupersetEqual", "sqsupseteq", "sqcap", "SquareIntersection", "sqcup", "SquareUnion", "oplus", "CirclePlus", "ominus", "CircleMinus", "otimes", "CircleTimes", "osol", "odot", "CircleDot", "ocr", "circledcirc", "oast", "circledast", "odash", "circleddash", "plusb", "boxplus", "minusb", "boxminus", "timesb", "boxtimes", "sdotb", "dotsquare", "vdash", "RightTee", "dashv", "LeftTee", "top", "DownTee", "perp", "bottom", "bot", "UpTee", "models", "vDash", "DoubleRightTee", "Vdash", "Vvdash", "VDash", "nvdash", "nvDash", "nVdash", "nVDash", "prurel", "vltri", "vartriangleleft", "LeftTriangle", "vrtri", "vartriangleright", "RightTriangle;", "ltrie", "trianglelefteq", "LeftTriangleEqual", "rtrie", "trianglerighteq", "RightTriangleEqual", "origof", "imof", "mmap", "multimap", "hercon", "intcal", "intercal", "veebar", "barvee", "angrtvb", "lrtri", "xwedge", "Wedge", "bigwedge", "xvee", "Vee", "bigvee", "xcap", "Intersection", "bigcap", "xcup", "Union", "bigcup", "diam", "diamond", "Diamond", "sdot", "sstarf", "Star", "divonx", "divideontimes", "bowtie", "ltimes", "rtimes", "lthree", "leftthreetimes", "rthree", "rightthreetimes", "bsime", "backsimeq", "cuvee", "curlyvee", "cuwed", "curlywedge", "Sub", "Subset", "Sup", "Supset", "Cap", "Cup", "fork", "pitchfork", "epar", "ltdot", "lessdot", "gtdot", "gtrdot", "Ll", "lll", "Gg", "ggg", "leg", "LessEqualGreater", "lesseqgtr", "gel", "gtreqless", "GreaterEqualLess", "cuepr", "curlyeqprec", "cuesc", "curlyeqsucc", "nprcue", "NotPrecedesSlantEqual", "nsccue", "NotSucceedsSlantEqual", "nsqsube", "NotSquareSubsetEqual", "nsqsupe", "NotSquareSupersetEqual", "lnsim", "gnsim", "prnsim", "precnsim", "scnsim", "succnsim", "nltri", "ntriangleleft", "NotLeftTriangle", "nrtri", "ntriangleright", "NotRightTriangle", "nltrie", "ntrianglelefteq", "NotLeftTriangleEqual", "nrtrie", "ntrianglerighteq", "NotRightTriangleEqual", "vellip", "ctdot", "utdot", "dtdot", "disin", "isinsv", "isins", "isindot", "notinvc", "notinvb", "isinE", "nisd", "xnis", "nis", "notnivc", "notnivb", "barwed", "barwedge", "Barwed", "doublebarwedge", "lceil", "LeftCeiling", "rceil", "RightCeiling", "lfloor", "LeftFloor", "rfloor", "RightFloor", "drcrop", "dlcrop", "urcrop", "ulcrop", "bnot", "profline", "profsurf", "telrec", "target", "ulcorn", "ulcorner", "urcorn", "urcorner", "dlcorn", "dlcorner", "drcorn", "drcorner", "frown", "sfrown", "smile", "ssmile", "cylcty", "profalar", "topbot", "ovbar", "solbar", "angzarr", "lmoust", "lmoustache", "rmoust", "rmoustache", "tbrk", "OverBracket", "bbrk", "UnderBracket", "bbrktbrk", "OverParenthesis", "UnderParenthesis", "OverBrace", "UnderBrace", "trpezium", "elinters", "blank", "oS", "circledS", "boxh", "HorizontalLine", "boxv", "boxdr", "boxdl", "boxur", "boxul", "boxvr", "boxvl", "boxhd", "boxhu", "boxvh", "boxH", "boxV", "boxdR", "boxDr", "boxDR", "boxdL", "boxDl", "boxDL", "boxuR", "boxUr", "boxUR", "boxuL", "boxUl", "boxUL", "boxvR", "boxVr", "boxVR", "boxvL", "boxVl", "boxVL", "boxHd", "boxhD", "boxHD", "boxHu", "boxhU", "boxHU", "boxvH", "boxVh", "boxVH", "uhblk", "lhblk", "block", "blk14", "blk12", "blk34", "squ", "square", "Square", "squf", "squaref", "blacksquare", "FilledVerySmallSquare", "EmptyVerySmallSquare", "rect", "marker", "fltns", "xutri", "bigtriangleup", "utrif", "blacktriangle", "utri", "triangle", "rtrif", "blacktriangleright", "rtri", "triangleright", "xdtri", "bigtriangledown", "dtrif", "blacktriangledown", "dtri", "triangledown", "ltrif", "blacktriangleleft", "ltri", "triangleleft", "loz", "lozenge", "cir", "tridot", "xcirc", "bigcirc", "ultri", "urtri", "lltri", "EmptySmallSquare", "FilledSmallSquare", "starf", "bigstar", "star", "phone", "female", "male", "spades", "spadesuit", "clubs", "clubsuit", "hearts", "heartsuit", "diams", "diamondsuit", "sung", "flat", "natur", "natural", "sharp", "check", "checkmark", "cross", "malt", "Maltese", "sext", "VerticalSeparator", "lbbrk", "rbbrk", "lobrk", "LeftDoubleBracket", "robrk", "RightDoubleBracket", "lang", "LeftAngleBracket", "langle", "rang", "RightAngleBracket", "rangle", "Lang", "Rang", "loang", "roang", "xlarr", "longleftarrow", "LongLeftArrow", "xrarr", "longrightarrow", "LongRightArrow", "xharr", "longleftrightarrow", "LongLeftRightArrow", "xlArr", "Longleftarrow", "DoubleLongLeftArrow", "xrArr", "Longrightarrow", "DoubleLongRightArrow", "xhArr", "Longleftrightarrow", "DoubleLongLeftRightArrow", "xmap", "longmapsto", "dzigrarr", "nvlArr", "nvrArr", "nvHArr", "Map", "lbarr", "rbarr", "bkarow", "lBarr", "rBarr", "dbkarow", "RBarr", "drbkarow", "DDotrahd", "UpArrowBar", "DownArrowBar", "Rarrtl", "latail", "ratail", "lAtail", "rAtail", "larrfs", "rarrfs", "larrbfs", "rarrbfs", "nwarhk", "nearhk", "searhk", "hksearow", "swarhk", "hkswarow", "nwnear", "nesear", "toea", "seswar", "tosa", "swnwar", "rarrc", "cudarrr", "ldca", "rdca", "cudarrl", "larrpl", "curarrm", "cularrp", "rarrpl", "harrcir", "Uarrocir", "lurdshar", "ldrushar", "LeftRightVector", "RightUpDownVector", "DownLeftRightVector", "LeftUpDownVector", "LeftVectorBar", "RightVectorBar", "RightUpVectorBar", "RightDownVectorBar", "DownLeftVectorBar", "DownRightVectorBar", "LeftUpVectorBar", "LeftDownVectorBar", "LeftTeeVector", "RightTeeVector", "RightUpTeeVector", "RightDownTeeVector", "DownLeftTeeVector", "DownRightTeeVector", "LeftUpTeeVector", "LeftDownTeeVector", "lHar", "uHar", "rHar", "dHar", "luruhar", "ldrdhar", "ruluhar", "rdldhar", "lharul", "llhard", "rharul", "lrhard", "udhar", "UpEquilibrium", "duhar", "ReverseUpEquilibrium", "RoundImplies", "erarr", "simrarr", "larrsim", "rarrsim", "rarrap", "ltlarr", "gtrarr", "subrarr", "suprarr", "lfisht", "rfisht", "ufisht", "dfisht", "lopar", "ropar", "lbrke", "rbrke", "lbrkslu", "rbrksld", "lbrksld", "rbrkslu", "langd", "rangd", "lparlt", "rpargt", "gtlPar", "ltrPar", "vzigzag", "vangrt", "angrtvbd", "ange", "range", "dwangle", "uwangle", "angmsdaa", "angmsdab", "angmsdac", "angmsdad", "angmsdae", "angmsdaf", "angmsdag", "angmsdah", "bemptyv", "demptyv", "cemptyv", "raemptyv", "laemptyv", "ohbar", "omid", "opar", "operp", "olcross", "odsold", "olcir", "ofcir", "olt", "ogt", "cirscir", "cirE", "solb", "bsolb", "boxbox", "trisb", "rtriltri", "LeftTriangleBar", "RightTriangleBar", "race", "iinfin", "infintie", "nvinfin", "eparsl", "smeparsl", "eqvparsl", "lozf", "blacklozenge", "RuleDelayed", "dsol", "xodot", "bigodot", "xoplus", "bigoplus", "xotime", "bigotimes", "xuplus", "biguplus", "xsqcup", "bigsqcup", "qint", "iiiint", "fpartint", "cirfnint", "awint", "rppolint", "scpolint", "npolint", "pointint", "quatint", "intlarhk", "pluscir", "plusacir", "simplus", "plusdu", "plussim", "plustwo", "mcomma", "minusdu", "loplus", "roplus", "Cross", "timesd", "timesbar", "smashp", "lotimes", "rotimes", "otimesas", "Otimes", "odiv", "triplus", "triminus", "tritime", "iprod", "intprod", "amalg", "capdot", "ncup", "ncap", "capand", "cupor", "cupcap", "capcup", "cupbrcap", "capbrcup", "cupcup", "capcap", "ccups", "ccaps", "ccups", "And", "Or", "andand", "oror", "orslope", "andslope", "andv", "orv", "andd", "ord", "wedbar", "sdote", "simdot", "congdot", "easter", "apacir", "apE", "eplus", "pluse", "Esim", "Colone", "Equal", "eDDot", "ddotseq", "equivDD", "ltcir", "gtcir", "fflig", "filig", "fllig", "ffilig", "ffllig", "Ascr", "Cscr", "Dscr", "Gscr", "Jscr", "Kscr", "Nscr", "Oscr", "Pscr", "Qscr", "Sscr", "Tscr", "Uscr", "Vscr", "Wscr", "Xscr", "Yscr", "Zscr", "ascr", "bscr", "cscr", "dscr", "fscr", "hscr", "iscr", "jscr", "kscr", "lscr", "mscr", "nscr", "pscr", "qscr", "rscr", "sscr", "tscr", "uscr", "vscr", "wscr", "xscr", "yscr", "zscr", "Afr", "Bfr", "Dfr", "Efr", "Ffr", "Gfr", "Jfr", "Kfr", "Lfr", "Mfr", "Nfr", "Ofr", "Pfr", "Qfr", "Sfr", "Tfr", "Ufr", "Vfr", "Wfr", "Xfr", "Yfr", "afr", "bfr", "cfr", "dfr", "efr", "ffr", "gfr", "hfr", "ifr", "jfr", "kfr", "lfr", "mfr", "nfr", "ofr", "pfr", "qfr", "rfr", "sfr", "tfr", "ufr", "vfr", "wfr", "xfr", "yfr", "zfr", "Aopf", "Bopf", "Dopf", "Eopf", "Fopf", "Gopf", "Iopf", "Jopf", "Kopf", "Lopf", "Mopf", "Oopf", "Sopf", "Topf", "Uopf", "Vopf", "Wopf", "Xopf", "Yopf", "aopf", "bopf", "copf", "dopf", "eopf", "fopf", "gopf", "hopf", "iopf", "jopf", "kopf", "lopf", "mopf", "nopf", "oopf", "popf", "qopf", "ropf", "sopf", "topf", "uopf", "vopf", "wopf", "xopf", "yopf", "zopf");
        }
    }
}