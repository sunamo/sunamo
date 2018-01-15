using sunamo;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace web.Collections
{
    /// <summary>
    /// T je klíč slovníku
    /// U je hodnota slovníku
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="X"></typeparam>
    public class SunamoDictionaryWithKeysDependencyObject<T, U> : SunamoDictionary<T, U> where T : DependencyObject
    {
        public List<U> GetValuesByValuesOfKeysProperty<X>(DependencyProperty dp, X co)
        {
            var vr = this.Where(d => EqualityComparer<X>.Default.Equals((X)d.Key.GetValue(dp), co));
            return vr.Select(d => d.Value).ToList();
            //return vr.SelectMany(d => d.Value);
        }
    }
}
