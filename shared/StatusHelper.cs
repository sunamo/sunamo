
using System.Windows.Media;

public partial class StatusHelper
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
}