using Windows.UI.Xaml.Controls;

public class ItemsControlHelper
{
    public static bool HasIndexWithoutException(int p, ItemCollection nahledy)
    {
        if (p < 0)
        {
            return false;
        }
        if (nahledy.Count > p)
        {
            return true;
        }
        return false;
    }
}