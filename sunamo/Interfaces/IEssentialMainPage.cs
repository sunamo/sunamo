using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Interfaces
{
    // Zadává se vždy bez typového parametru, Instance musí být vždy statická, například kvůli ICommand
    public interface IEssentialMainPage<T, Z>
    {
        // Musí být async
        Task SetStatus(LogMessageAbstract<T, Z> lmn, bool alsoLb);
        LogServiceAbstract<T, Z> lsg { get; }

        //MainPage Instance { get; }
    }
}
