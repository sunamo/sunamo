public class TryParse
{
    public class DateTime
    {
        public System.DateTime lastDateTime = System.DateTime.Today;

        /// <summary>
        /// Vrátí True pokud se podaří vyparsovat, jinak false. 
        /// Výsledek najdeš v proměnné lastDateTime
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool TryParseDateTime(string r)
        {
            bool isValid = false;
            lastDateTime = DTHelper.IsValidDateTimeText(r);
            isValid = lastDateTime != System.DateTime.MinValue;
            //}
            if (!isValid)
            {
                lastDateTime = DTHelper.IsValidDateText(r);
                isValid = lastDateTime != System.DateTime.MinValue;
            }
            if (!isValid)
            {
                lastDateTime = DTHelper.IsValidTimeText(r);
                isValid = lastDateTime != System.DateTime.MinValue;
            }
            return lastDateTime != System.DateTime.MinValue;
        }
    }

    public class Integer
    {
        public int lastInt = -1;

        /// <summary>
        /// Vrátí True pokud se podaří vyparsovat, jinak false. 
        /// Výsledek najdeš v proměnné lastInt
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool TryParseInt(string p)
        {
            if (int.TryParse(p, out lastInt))
            {
                return true;
            }
            return false;
        }
    }
}
