using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace apps
{
    public class GridHelper
    {

        public static void SetColumnWidthToGrid(Grid g, List<ColumnDefinition> cdn)
        {
            var cdo = g.ColumnDefinitions;
            cdo.Clear();
            foreach (var item in cdn)
            {
                cdo.Add(item);
            }
        }

        public static void SetRowHeightToGrid(Grid g, List<RowDefinition> cdn)
        {
            var cdo = g.RowDefinitions;
            cdo.Clear();
            foreach (var item in cdn)
            {
                cdo.Add(item);
            }
        }

        public static List<double> SameWidthForAllColumnsDouble(double gridWidth, params bool[] columnsShow)
        {
            int columnsLength = columnsShow.Count();
            var visibleColumns = CA.CountOfValue<bool>(true, columnsShow);
            double forEach = NH.Average(gridWidth, visibleColumns);

            List<double> vr = new List<double>(columnsLength);
            for (int i = 0; i < columnsLength; i++)
            {
                if (columnsShow[i])
                {
                    vr.Add(forEach);
                }
                else
                {
                    vr.Add(0);
                }
            }
            return vr;
        }

        public static List<double> SameWidthForAllColumnsDouble(int columnsLength, double gridWidth)
        {
            double forEach = NH.Average(gridWidth, columnsLength);

            List<double> vr = new List<double>(columnsLength);
            for (int i = 0; i < columnsLength; i++)
            {
                vr.Add(forEach);
            }
            return vr;
        }

        public static ColumnDefinition GetColumnDefinition(double oneC)
        {
            return GetColumnDefinition(GetGridLength(oneC));
        }

        public static RowDefinition GetRowDefinition(double oneC)
        {
            return GetRowDefinition(GetGridLength(oneC));
        }

        public static ColumnDefinition GetColumnDefinition(GridLength oneC)
        {
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = oneC;
            return cd;
        }

        public static GridLength GetGridLength(double oneC, GridUnitType gut)
        {
            return new GridLength(oneC, gut);
        }

        public static GridLength GetGridLength(double oneC)
        {
            return GetGridLength(oneC, GridUnitType.Pixel);
        }

        /// <summary>
        /// Získané prvky pak aplikuj metodou GridHelper.SetColumnWidthToGrid nebo SetColumnHeightToGrid
        /// 
        /// </summary>
        /// <param name="columnsLength"></param>
        /// <param name="gridWidth"></param>
        /// <returns></returns>
        public static List<GridLength> SameWidthForAllColumnsGridLength(int columnsLength, double gridWidth)
        {
            double forEach = NH.Average(gridWidth, columnsLength);
                
                List<GridLength> vr = new List<GridLength>(columnsLength);
                for (int i = 0; i < columnsLength; i++)
                {
                    vr.Add(new GridLength(forEach, GridUnitType.Pixel));
                }
                return vr;
           
        }

        public static List<ColumnDefinition> GetColumnDefinitions(List<double> d)
        {
            List<ColumnDefinition> vr = new List<ColumnDefinition>();
            foreach (var item in d)
            {
                vr.Add(GetColumnDefinition(item));
            }
            return vr;
        }

        public static List<RowDefinition> GetRowDefinitions(List<GridLength> d)
        {
            List<RowDefinition> vr = new List<RowDefinition>();
            foreach (var item in d)
            {
                vr.Add(GetRowDefinition(item));
            }
            return vr;
        }

        public static List<ColumnDefinition> GetColumnDefinitions(List<GridLength> d)
        {
            List<ColumnDefinition> vr = new List<ColumnDefinition>();
            foreach (var item in d)
            {
                vr.Add(GetColumnDefinition(item));
            }
            return vr;
        }

        public static Grid CreateGrid(int extentElementAtElement, ButtonWithAction[] bwas)
        {
            Grid vr = new Grid();
            for (int i = 0; i < bwas.Length; i++)
            {
                if (extentElementAtElement == i)
                {
                    vr.ColumnDefinitions.Add(GetColumnDefinition(new GridLength(1, GridUnitType.Star)));
                }
                else
                {
                    vr.ColumnDefinitions.Add(GetColumnDefinition(GridLength.Auto));
                }
            }
            

            for (int i = 0; i < bwas.Length; i++)
            {
                Grid.SetColumn(bwas[i], i);
                vr.Children.Add(bwas[i]);
            }
            return vr;
        }

        public static RowDefinition GetRowDefinition(GridLength auto)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = auto;
            return rd;
        }
    }
}
