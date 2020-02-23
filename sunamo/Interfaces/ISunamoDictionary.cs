using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface ISunamoDictionary<T,U>
{
    U this[T key] { get;set; }

    ICollection<T> Keys  {get;}

    ICollection<U> Values { get; }

    int Count { get; }

    bool IsReadOnly { get; }

    void Add(T key, U value);

    void Add(KeyValuePair<T, U> item);

    void Clear();

    bool Contains(KeyValuePair<T, U> item);

    bool ContainsKey(T key);

    void CopyTo(KeyValuePair<T, U>[] array, int arrayIndex);

    IEnumerator<KeyValuePair<T, U>> GetEnumerator();

    bool Remove(T key);

    bool Remove(KeyValuePair<T, U> item);

    bool TryGetValue(T key, out U value);
}