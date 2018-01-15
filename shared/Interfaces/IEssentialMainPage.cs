using System.Threading.Tasks;

namespace sunamo.Interfaces
{
    // Zadává se vždy bez typového parametru, Instance musí být vždy statická, například kvůli ICommand
    public interface IEssentialMainPage<T, Z>
    {

        // TODO: LogServiceAbstract and LogMessageAbstract was missing in SE. 
        // Musí být async
        Task SetStatus(LogMessageAbstract<T, Z> lmn, bool alsoLb);
        LogServiceAbstract<T, Z> Logger { get; }

        //MainPage Instance { get; }
    }
}
