using System.Web;
using System.Web.SessionState;

public interface IHttpHandlerSession : IHttpHandler, IReadOnlySessionState//, IRequiresSessionState
{
}
