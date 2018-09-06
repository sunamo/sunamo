using sunamo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace apps
{
    public class SizeColumnsMusicListView : SizeColumnsListView
    {
        public SizeColumnsMusicListView() : base("SizeColumnsMusicListView")
        {

        }
    }

    public class SizeColumnsVideosListView : SizeColumnsListView
    {
        public SizeColumnsVideosListView() : base("SizeColumnsVideosListView")
        {

        }
    }

    public class SizeColumnsListView : BindingClassDouble
    {
        public SizeColumnsListView(string className) : base(className)
        {

        }
    }

    public class BindingClassDouble : INotifyPropertyChanged
    {
        readonly string className = "";

        public BindingClassDouble(string className)
        {
            this.className = className;
        }

        /// <summary>
        /// A2 je o kolik se změní nový počet sloupců
        /// </summary>
        /// <param name="nt"></param>
        /// <param name="add"></param>
        public void ShowHideColumn(int nt, bool show, double ActualWidth, object grid)
        {
            int add = -1;
            if (show)
            {
                add = 1;
            }
            SunamoDictionary<int, double> showColumns = GetDictionary();
            int visibleBefore = showColumns.CountOfValueNot(0);
            int visibleNow = visibleBefore + add;
            double avgWidth = ActualWidth / visibleNow;
            double onePercent = avgWidth / 100;
            Dictionary<int, double> beforeVisibleWithoutA1 = GetDictionaryWhereIsValueNot(0);
            Dictionary<int, double> nowVisibleWithoutA1 = new Dictionary<int, double>();

            double widthNewColumn = 0;

            if (add == 1)
            {
                double trimAtAllBeforeVisible = onePercent / visibleBefore;
                for (int i = 1; i < 101; i++)
                {
                    widthNewColumn = onePercent * i;
                    if (widthNewColumn >= avgWidth)
                    {
                        foreach (var item in beforeVisibleWithoutA1)
                        {
                            nowVisibleWithoutA1[item.Key] = widthNewColumn; //item.Value - (trimAtAllBeforeVisible * i); //(item.Value / 100) * (100 - i);
                        }
                        nowVisibleWithoutA1.Add(nt, widthNewColumn);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 1; i < 101; i++)
                {
                    widthNewColumn = onePercent * i;
                    if (widthNewColumn >= avgWidth)
                    {
                        foreach (var item in beforeVisibleWithoutA1)
                        {
                            if (item.Key == nt)
                            {
                                Set(nt, 0);
                                //ChangedColumnWidth(nt, 0);
                                continue;
                            }
                            nowVisibleWithoutA1[item.Key] = widthNewColumn; //item.Value + (widthNewColumn / visibleNow); //(item.Value / 100) * (100 - i);
                        }

                        break;
                    }
                }
            }

            // Všechny sloupce nastavím na novou hodnotu
            for (int i = 0; i < showColumns.Count; i++)
            {
                if (nowVisibleWithoutA1.ContainsKey(i))
                {
                    Set(i, nowVisibleWithoutA1[i]);
                }
            }

            Grid g = grid as Grid;
            if (g != null)
            {
                GridHelper.SetColumnWidthToGrid(g, GridHelper.GetColumnDefinitions(GetList()));
            }
            
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        double __0 = 0;
        double __1 = 0;
        double __2 = 0;
        double __3 = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public double _0
        {
            //get; set;
            get
            {
                return __0;
            }
            set
            {
                __0 = value;
                OnPropertyChanged("_0");
            }
        }

        public double _1
        {
            get
            {
                return __1;
            }
            set
            {
                __1 = value;
                OnPropertyChanged("_1");
            }
        }
        public double _2
        {
            get
            {
                return __2;
            }
            set
            {
                __2 = value;
                OnPropertyChanged("_2");
            }
        }

        public double _3
        {
            get
            {
                return __3;
            }
            set
            {
                __3 = value;
                OnPropertyChanged("_3");
            }
        }

        public void Set(List<double> d, object grid)
        {
            Grid g = grid as Grid;
            if (g != null)
            {
                var cdn = GridHelper.GetColumnDefinitions(d);
                GridHelper.SetColumnWidthToGrid(g, cdn);
                ////g.ColumnDefinitions = 
            }
            _0 = d[0];
            _1 = d[1];
            _2 = d[2];
            _3 = d[3];
        }

        public void Set(int nt, double v)
        {
            switch (nt)
            {
                case 0:
                    _0 = v;
                    break;
                case 1:
                    _1 = v;
                    break;
                case 2:
                    _2 = v;
                    break;
                case 3:
                    _3 = v;
                    break;
                default:
                    throw new Exception( className + ".Set() obtained value " + nt + " which is not in switch.");
            }
        }

        public SunamoDictionary<int, double> GetDictionaryWhereIsValueNot(double v)
        {
            SunamoDictionary<int, double> vr = new SunamoDictionary<int, double>();
            if (HasNotValue(_0, v))
            {
                vr.Add(0, _0);
            }
            if (HasNotValue(_1, v))
            {
                vr.Add(1, _1);
            }
            if (HasNotValue(_2, v))
            {
                vr.Add(2, _2);
            }
            if (HasNotValue(_3, v))
            {
                vr.Add(3, _3);
            }
            return vr;
        }

        private bool HasNotValue(double _0, double v)
        {
            if (EqualityComparer<double>.Default.Equals(_0, v))
            {
                return false;
            }
            return true;
        }

        public SunamoDictionary<int, double> GetDictionaryWhereIsValue(double v)
        {
            SunamoDictionary<int, double> vr = new SunamoDictionary<int, double>();
            if (HasValue(_0, v))
            {
                vr.Add(0, _0);
            }
            if (HasValue(_1, v))
            {
                vr.Add(1, _1);
            }
            if (HasValue(_2, v))
            {
                vr.Add(2, _2);
            }
            if (HasValue(_3, v))
            {
                vr.Add(3, _3);
            }
            return vr;
        }

        private bool HasValue(double _0, double v)
        {
            if (EqualityComparer<double>.Default.Equals(_0, v))
            {
                return true;
            }
            return false;
        }

        public SunamoDictionary<int, double> GetDictionary()
        {
            SunamoDictionary<int, double> vr = new SunamoDictionary<int, double>();
            vr.Add(0,_0);
            vr.Add(1,_1);
            vr.Add(2,_2);
            vr.Add(3,_3);
            return vr;
        }

        public List<double> GetList()
        {
            List<double> vr = new List<double>();
            vr.Add(_0);
            vr.Add(_1);
            vr.Add(_2);
            vr.Add(_3);
            return vr;
        }
    }
}
