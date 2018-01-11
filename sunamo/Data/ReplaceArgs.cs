public class ReplaceArgs
{
    public string initialMessage = null;
    public string lblOldValue = null;
    public string lblNewValue = null;

    public ReplaceArgs(string initialMessage, string lblOldValue, string lblNewValue)
    {
        this.initialMessage = initialMessage;
        this.lblOldValue = lblOldValue;
        this.lblNewValue = lblNewValue;
    }
}
