namespace sunamo
{
    public class StateMessage
    {
        protected TypeOfMessage mt = TypeOfMessage.Information;
        protected string message = null;

        public StateMessage(TypeOfMessage mt, string message)
        {
            this.mt = mt;
            this.message = message;
        }

        public TypeOfMessage TypeOfMessage
        {
            get
            {
                return mt;
            }
        }

        public string TextMessage
        {
            get
            {
                return message;
            }
        }
    }
}
