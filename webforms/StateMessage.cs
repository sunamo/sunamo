using System;
using System.Web.UI.HtmlControls;

namespace web
{
    public class StateMessage : sunamo.StateMessage
    {
        public StateMessage(TypeOfMessage mt, string message) : base(mt, message)
        {
        }

        public static HtmlGenericControl MessagePublic(SunamoPage sp, StateMessage message)
        {
            switch (message.TypeOfMessage)
            {
                case TypeOfMessage.Error:
                    sp.Error(message.TextMessage);
                    break;
                case TypeOfMessage.Warning:
                    sp.Warning(message.TextMessage);
                    break;
                case TypeOfMessage.Information:
                    sp.Info(message.TextMessage);
                    break;
                case TypeOfMessage.Success:
                    sp.Success(message.TextMessage);
                    break;
                case TypeOfMessage.Ordinal:
                case TypeOfMessage.Appeal:
                default:
                    throw new Exception("Neznámý nebo neimplementovaný prvek výčtu TypeOfMessage");
                    break;
            }
            return sp.errors;
        }
    }
}
