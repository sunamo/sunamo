
namespace web
{
    public static class ColorH
    {
        

        

        /// <summary>
        /// Vrátí hex barvu(např. #0b0) - zelenou nebo její odstín(čím větší A1, tím lehčí odstín). Když A1 je větší než 4, vrací SE(bílou).
        /// Do A1 se dává největší číslo v řadě / počet fotek(např.)
        /// Může se použít přímo 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetColorDegree(float p)
        {
            /*
             * 1/4 = velikost nejvyšší hodnoty / skutečného počtu fotek
    větší než 4 = menší než 1/4 = bílé
    4 = 1/4 = bílé
    do 3(100/33) = větší než 1/4, <= 1/3 = zelené
    do 2(100/50) = větší než 1/3, <= 1/2 = žluté
    do 1,25(100/75) = větší než 1/2, <= 1/1,3 = světle červené
    pod 1,25(100/80) = červená

    0123456789ABCDEF
       0   0   0   0
             */
            if (p < 1.25)
            {
                return "#030";
            }
            else if (p < 2)
            {
                return "#070";
            }
            else if (p < 3)
            {
                // žluté   
                return "#0b0";
            }
            else if (p < 4)
            {
                // zelené
                return "#0f0";
            }
            else
            {
                return "";
            }
        }

        
    }
}
