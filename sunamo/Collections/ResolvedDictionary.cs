using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ResolvedDictionary<T,U>
    {
    public Dictionary<T, U> dict = new Dictionary<T, U>();

    public U Get(T idArtist, Func<T, U> nameOfArtist)
    {
        if (dict.ContainsKey(idArtist))
        {
            return dict[idArtist];
        }

        return nameOfArtist.Invoke(idArtist);
    }
}