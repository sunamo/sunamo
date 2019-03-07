using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace shared
{
    public class StatusHelper
    {


        public static Color GetBackgroundBrushOfTypeOfMessage(TypeOfMessage st)
        {
            switch (st)
            {
                case TypeOfMessage.Error:
                    return Colors.LightCoral;
                case TypeOfMessage.Warning:
                    return Colors.LightYellow;
                case TypeOfMessage.Information:
                    return Colors.White;
                case TypeOfMessage.Ordinal:
                    return Colors.White;
                case TypeOfMessage.Appeal:
                    return Colors.LightGray;
                case TypeOfMessage.Success:
                    return Colors.LightGreen;
                default:
                    return Colors.White;
            }
        }

        public static Color GetForegroundBrushOfTypeOfMessage(TypeOfMessage st)
        {
            switch (st)
            {
                case TypeOfMessage.Error:
                    return Colors.DarkRed;
                case TypeOfMessage.Warning:
                    return Colors.DarkOrange;
                case TypeOfMessage.Information:
                    return Colors.Black;
                case TypeOfMessage.Ordinal:
                    return Colors.Black;
                case TypeOfMessage.Appeal:
                    return Colors.Gray;
                case TypeOfMessage.Success:
                    return Colors.LightGreen;
                default:
                    return Colors.White;
            }
        }
    }
}
