﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Values
{
    public class GeoStates
    {
        public Dictionary<string, string> statesFullNames = null;

        public GeoStates()
        {
        }

        public Dictionary<string, string> Init()
        {
            #region MyRegion
            statesFullNames = new Dictionary<string, string>();

            statesFullNames.Add("AD", "Andorra");
            statesFullNames.Add("AE", "United Arab Emirates");
            statesFullNames.Add("AF", "Afghanistan");
            statesFullNames.Add("AG", "Antigua and Barbuda");
            statesFullNames.Add("AI", "Anguilla");
            statesFullNames.Add("AL", "Albania");
            statesFullNames.Add("AM", "Armenia");
            statesFullNames.Add("AO", "Angola");
            statesFullNames.Add("AQ", "Antarctica");
            statesFullNames.Add("AR", "Argentina");
            statesFullNames.Add("AS", "American Samoa");
            statesFullNames.Add("AT", "Austria");
            statesFullNames.Add("AU", "Australia");
            statesFullNames.Add("AW", "Aruba");
            statesFullNames.Add("AX", "\u00C5land Islands");
            statesFullNames.Add("AZ", "Azerbaijan");
            statesFullNames.Add("BA", "Bosnia and Herzegovina");
            statesFullNames.Add("BB", "Barbados");
            statesFullNames.Add("BD", "Bangladesh");
            statesFullNames.Add("BE", "Belgium");
            statesFullNames.Add("BF", "Burkina Faso");
            statesFullNames.Add("BG", "Bulgaria");
            statesFullNames.Add("BH", "Bahrain");
            statesFullNames.Add("BI", "Burundi");
            statesFullNames.Add("BJ", "Benin");
            statesFullNames.Add("BL", "Saint Barth\u00E9lemy");
            statesFullNames.Add("BM", "Bermuda");
            statesFullNames.Add("BN", "Brunei Darussalam");
            statesFullNames.Add("BO", "Bolivia, Plurinational State of");
            statesFullNames.Add("BQ", "Bonaire, Sint Eustatius and Saba");
            statesFullNames.Add("BR", "Brazil");
            statesFullNames.Add("BS", "Bahamas");
            statesFullNames.Add("BT", "Bhutan");
            statesFullNames.Add("BV", "Bouvet Island");
            statesFullNames.Add("BW", "Botswana");
            statesFullNames.Add("BY", "Belarus");
            statesFullNames.Add("BZ", "Belize");
            statesFullNames.Add("CA", "Canada");
            statesFullNames.Add("CC", "Cocos (Keeling) Islands");
            statesFullNames.Add("CD", "Congo, the Democratic Republic of the");
            statesFullNames.Add("CF", "Central African Republic");
            statesFullNames.Add("CG", "Congo");
            statesFullNames.Add("CH", "Switzerland");
            statesFullNames.Add("CI", "C\u00F4te d'Ivoire");
            statesFullNames.Add("CK", "Cook Islands");
            statesFullNames.Add("CL", "Chile");
            statesFullNames.Add("CM", "Cameroon");
            statesFullNames.Add("CN", "China");
            statesFullNames.Add("CO", "Colombia");
            statesFullNames.Add("CR", "Costa Rica");
            statesFullNames.Add("CU", "Cuba");
            statesFullNames.Add("CV", "Cabo Verde");
            statesFullNames.Add("CW", "Cura\u00E7ao");
            statesFullNames.Add("CX", "Christmas Island");
            statesFullNames.Add("CY", "Cyprus");
            statesFullNames.Add("CZ", "Czech Republic");
            statesFullNames.Add("DE", "Germany");
            statesFullNames.Add("DJ", "Djibouti");
            statesFullNames.Add("DK", "Denmark");
            statesFullNames.Add("DM", "Dominica");
            statesFullNames.Add("DO", "Dominican Republic");
            statesFullNames.Add("DZ", "Algeria");
            statesFullNames.Add("EC", "Ecuador");
            statesFullNames.Add("EE", "Estonia");
            statesFullNames.Add("EG", "Egypt");
            statesFullNames.Add("EH", "Western Sahara");
            statesFullNames.Add("ER", "Eritrea");
            statesFullNames.Add("ES", "Spain");
            statesFullNames.Add("ET", "Ethiopia");
            statesFullNames.Add("FI", "Finland");
            statesFullNames.Add("FJ", "Fiji");
            statesFullNames.Add("FK", "Falkland Islands (Malvinas)");
            statesFullNames.Add("FM", "Micronesia, Federated States of");
            statesFullNames.Add("FO", "Faroe Islands");
            statesFullNames.Add("FR", "France");
            statesFullNames.Add("GA", "Gabon");
            statesFullNames.Add("GB", "United Kingdom of Great Britain and Northern Ireland");
            statesFullNames.Add("GD", "Grenada");
            statesFullNames.Add("GE", "Georgia");
            statesFullNames.Add("GF", "French Guiana");
            statesFullNames.Add("GG", "Guernsey");
            statesFullNames.Add("GH", "Ghana");
            statesFullNames.Add("GI", "Gibraltar");
            statesFullNames.Add("GL", "Greenland");
            statesFullNames.Add("GM", "Gambia");
            statesFullNames.Add("GN", "Guinea");
            statesFullNames.Add("GP", "Guadeloupe");
            statesFullNames.Add("GQ", "Equatorial Guinea");
            statesFullNames.Add("GR", "Greece");
            statesFullNames.Add("GS", "South Georgia and the South Sandwich Islands");
            statesFullNames.Add("GT", "Guatemala");
            statesFullNames.Add("GU", "Guam");
            statesFullNames.Add("GW", "Guinea-Bissau");
            statesFullNames.Add("GY", "Guyana");
            statesFullNames.Add("HK", "Hong Kong");
            statesFullNames.Add("HM", "Heard Island and McDonald Islands");
            statesFullNames.Add("HN", "Honduras");
            statesFullNames.Add("HR", "Croatia");
            statesFullNames.Add("HT", "Haiti");
            statesFullNames.Add("HU", "Hungary");
            statesFullNames.Add("ID", "Indonesia");
            statesFullNames.Add("IE", "Ireland");
            statesFullNames.Add("IL", "Israel");
            statesFullNames.Add("IM", "Isle of Man");
            statesFullNames.Add("IN", "India");
            statesFullNames.Add("IO", "British Indian Ocean Territory");
            statesFullNames.Add("IQ", "Iraq");
            statesFullNames.Add("IR", "Iran, Islamic Republic of");
            statesFullNames.Add("IS", "Iceland");
            statesFullNames.Add("IT", "Italy");
            statesFullNames.Add("JE", "Jersey");
            statesFullNames.Add("JM", "Jamaica");
            statesFullNames.Add("JO", "Jordan");
            statesFullNames.Add("JP", "Japan");
            statesFullNames.Add("KE", "Kenya");
            statesFullNames.Add("KG", "Kyrgyzstan");
            statesFullNames.Add("KH", "Cambodia");
            statesFullNames.Add("KI", "Kiribati");
            statesFullNames.Add("KM", "Comoros");
            statesFullNames.Add("KN", "Saint Kitts and Nevis");
            statesFullNames.Add("KP", "Korea, Democratic People's Republic of");
            statesFullNames.Add("KR", "Korea, Republic of");
            statesFullNames.Add("KW", "Kuwait");
            statesFullNames.Add("KY", "Cayman Islands");
            statesFullNames.Add("KZ", "Kazakhstan");
            statesFullNames.Add("LA", "Lao People's Democratic Republic");
            statesFullNames.Add("LB", "Lebanon");
            statesFullNames.Add("LC", "Saint Lucia");
            statesFullNames.Add("LI", "Liechtenstein");
            statesFullNames.Add("LK", "Sri Lanka");
            statesFullNames.Add("LR", "Liberia");
            statesFullNames.Add("LS", "Lesotho");
            statesFullNames.Add("LT", "Lithuania");
            statesFullNames.Add("LU", "Luxembourg");
            statesFullNames.Add("LV", "Latvia");
            statesFullNames.Add("LY", "Libya");
            statesFullNames.Add("MA", "Morocco");
            statesFullNames.Add("MC", "Monaco");
            statesFullNames.Add("MD", "Moldova, Republic of");
            statesFullNames.Add("ME", "Montenegro");
            statesFullNames.Add("MF", "Saint Martin (French part)");
            statesFullNames.Add("MG", "Madagascar");
            statesFullNames.Add("MH", "Marshall Islands");
            statesFullNames.Add("MK", "Macedonia, the former Yugoslav Republic of");
            statesFullNames.Add("ML", "Mali");
            statesFullNames.Add("MM", "Myanmar");
            statesFullNames.Add("MN", "Mongolia");
            statesFullNames.Add("MO", "Macao");
            statesFullNames.Add("MP", "Northern Mariana Islands");
            statesFullNames.Add("MQ", "Martinique");
            statesFullNames.Add("MR", "Mauritania");
            statesFullNames.Add("MS", "Montserrat");
            statesFullNames.Add("MT", "Malta");
            statesFullNames.Add("MU", "Mauritius");
            statesFullNames.Add("MV", "Maldives");
            statesFullNames.Add("MW", "Malawi");
            statesFullNames.Add("MX", "Mexico");
            statesFullNames.Add("MY", "Malaysia");
            statesFullNames.Add("MZ", "Mozambique");
            statesFullNames.Add("NA", "Namibia");
            statesFullNames.Add("NC", "New Caledonia");
            statesFullNames.Add("NE", "Niger");
            statesFullNames.Add("NF", "Norfolk Island");
            statesFullNames.Add("NG", "Nigeria");
            statesFullNames.Add("NI", "Nicaragua");
            statesFullNames.Add("NL", "Netherlands");
            statesFullNames.Add("NO", "Norway");
            statesFullNames.Add("NP", "Nepal");
            statesFullNames.Add("NR", "Nauru");
            statesFullNames.Add("NU", "Niue");
            statesFullNames.Add("NZ", "New Zealand");
            statesFullNames.Add("OM", "Oman");
            statesFullNames.Add("PA", "Panama");
            statesFullNames.Add("PE", "Peru");
            statesFullNames.Add("PF", "French Polynesia");
            statesFullNames.Add("PG", "Papua New Guinea");
            statesFullNames.Add("PH", "Philippines");
            statesFullNames.Add("PK", "Pakistan");
            statesFullNames.Add("PL", "Poland");
            statesFullNames.Add("PM", "Saint Pierre and Miquelon");
            statesFullNames.Add("PN", "Pitcairn");
            statesFullNames.Add("PR", "Puerto Rico");
            statesFullNames.Add("PS", "Palestine, State of");
            statesFullNames.Add("PT", "Portugal");
            statesFullNames.Add("PW", "Palau");
            statesFullNames.Add("PY", "Paraguay");
            statesFullNames.Add("QA", "Qatar");
            statesFullNames.Add("RE", "R\u00E9union");
            statesFullNames.Add("RO", "Romania");
            statesFullNames.Add("RS", "Serbia");
            statesFullNames.Add("RU", "Russian Federation");
            statesFullNames.Add("RW", "Rwanda");
            statesFullNames.Add("SA", "Saudi Arabia");
            statesFullNames.Add("SB", "Solomon Islands");
            statesFullNames.Add("SC", "Seychelles");
            statesFullNames.Add("SD", "Sudan");
            statesFullNames.Add("SE", "Sweden");
            statesFullNames.Add("SG", "Singapore");
            statesFullNames.Add("SH", "Saint Helena, Ascension and Tristan da Cunha");
            statesFullNames.Add("SI", "Slovenia");
            statesFullNames.Add("SJ", "Svalbard and Jan Mayen");
            statesFullNames.Add("SK", "Slovakia");
            statesFullNames.Add("SL", "Sierra Leone");
            statesFullNames.Add("SM", "San Marino");
            statesFullNames.Add("SN", "Senegal");
            statesFullNames.Add("SO", "Somalia");
            statesFullNames.Add("SR", "Suriname");
            statesFullNames.Add("SS", "South Sudan");
            statesFullNames.Add("ST", "Sao Tome and Principe");
            statesFullNames.Add("SV", "El Salvador");
            statesFullNames.Add("SX", "Sint Maarten (Dutch part)");
            statesFullNames.Add("SY", "Syrian Arab Republic");
            statesFullNames.Add("SZ", "Swaziland");
            statesFullNames.Add("TC", "Turks and Caicos Islands");
            statesFullNames.Add("TD", "Chad");
            statesFullNames.Add("TF", "French Southern Territories");
            statesFullNames.Add("TG", "Togo");
            statesFullNames.Add("TH", "Thailand");
            statesFullNames.Add("TJ", "Tajikistan");
            statesFullNames.Add("TK", "Tokelau");
            statesFullNames.Add("TL", "Timor-Leste");
            statesFullNames.Add("TM", "Turkmenistan");
            statesFullNames.Add("TN", "Tunisia");
            statesFullNames.Add("TO", "Tonga");
            statesFullNames.Add("TR", "Turkey");
            statesFullNames.Add("TT", "Trinidad and Tobago");
            statesFullNames.Add("TV", "Tuvalu");
            statesFullNames.Add("TW", "Taiwan, Province of China");
            statesFullNames.Add("TZ", "Tanzania, United Republic of");
            statesFullNames.Add("UA", "Ukraine");
            statesFullNames.Add("UG", "Uganda");
            statesFullNames.Add("UM", "United States Minor Outlying Islands");
            statesFullNames.Add("US", "United States of America");
            statesFullNames.Add("UY", "Uruguay");
            statesFullNames.Add("UZ", "Uzbekistan");
            statesFullNames.Add("VA", "Holy See");
            statesFullNames.Add("VC", "Saint Vincent and the Grenadines");
            statesFullNames.Add("VE", "Venezuela, Bolivarian Republic of");
            statesFullNames.Add("VG", "Virgin Islands, British");
            statesFullNames.Add("VI", "Virgin Islands, U.S.");
            statesFullNames.Add("VN", "Viet Nam");
            statesFullNames.Add("VU", "Vanuatu");
            statesFullNames.Add("WF", "Wallis and Futuna");
            statesFullNames.Add("WS", "Samoa");
            statesFullNames.Add("YE", "Yemen");
            statesFullNames.Add("YT", "Mayotte");
            statesFullNames.Add("ZA", "South Africa");
            statesFullNames.Add("ZM", "Zambia");
            statesFullNames.Add("ZW", "Zimbabwe");

            #endregion


            return statesFullNames;
        }
    }
}
