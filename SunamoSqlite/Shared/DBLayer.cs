using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Windows.Forms;


    /// <summary>
    /// Trida pro snadny a rychly pristup �k MSSQL DB.
    /// V K pouziji fci PridejTabulku na vsechny tabulky se kterymi budu pracovat. Nazvy sloupcu tabulek ulozi do sloupceZavorky a sloupce. U webu se toto dela v Global.asax.
    /// 2. sloupec v tabulce jsou vzdycky nazvy - at uz se jmenuji jakkoliv. Prvni je id.
    /// FOrm1 jen vola metodu NastavUC na ruzne UC.
    /// Co UMi:
    /// 
    /// </summary>
class DBLayer
{


    bool st = false;
    bool st1 = false;




    /// <summary>
    /// Vykona update dotaz nad vsemi sloupci A2 s hodnotami A3 pod ID A4 v tabulce A1
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    /// <param name="p_3"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    internal string Update(string tabulka, string sloupceBezZavorek, string hodnotyBezZavorek, int id)
    {
        StringBuilder sb = new StringBuilder();
        string[] s = Rozdel(sloupceBezZavorek, ",");
        string[] h = Rozdel(hodnotyBezZavorek, ",");

        for (int i = 0; i < s.Length; i++)
        {
            SqlCommand = "UPDATE " + tabulka + " SET " + s[i] + "=" + h[i] + " WHERE id=" + id + ";";
            string vr = null;
            bool b = false;
            while (!b)
            {
                vr = ExecuteNonQuery(out b);
            }
            sb.Append(vr + " ");
        }

        return sb.ToString();
    }



    /// <summary>
    /// GT okud je jedina polozka na A1 True
    /// </summary>
    /// <param name="obj"></param>
    internal bool IsSomethingNull(params object[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] == null)
            {
                System.Windows.Forms.MessageBox.Show("Musite vyplnit vsechny polozky");
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Vsechny metody Execute
    /// </summary>
    enum Vykonavani
    {
        NonQuery, Reader, Scalar
    }


    /// <summary>
    /// SL: Pokud je A1 -1, jedn� se o insert, jinak update.
    /// V A1 je n�zev tabulky - pak se z PP sloupceZavorky vezmou automaticky jej� sloupce
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="id"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    internal string UpdateOrInsert(string p, int id, params object[] obj)
    {
        bool d = DBLayer.ci.IsSomethingNull(obj); ;

        if (!d)
        {
            if (id == -1)
            {
                string chyba = DBLayer.ci.Insert(
                    p, //tabulka
                    DBLayer.ci.sloupceZavorky[p], //sloupceSZavorkami
                    DBLayer.ci.ArrayToString(false, true, obj) //hodnotySZavorkami
                    );
                // 
                if (!string.IsNullOrEmpty(chyba))
                {
                    return chyba;
                }
            }
            else
            {
                string chyba = DBLayer.ci.Update(
                    p,
                    DBLayer.ci.sloupce[p],
                    DBLayer.ci.PrevedNaPoleString(false, true, obj),
                    id);
                // Nemusim testovat na Trim, pokdu to je skutecna chyba, Poradi si s ni metoda ZobrazChybu
                if (!string.IsNullOrEmpty(chyba))
                {
                    return chyba;
                }
            }
        }
        else
        {
            return "Dotaz nelze zpracovat, protoze neco bylo null. Zkontrolujte zadani a akci opakujte.";
        }
        return null;
    }

    public void ErrorMbox(string chyba)
    {
        if (!string.IsNullOrEmpty(chyba.Trim()))
        {
            MessageBox.Show(chyba);
        }
    }
}
