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
