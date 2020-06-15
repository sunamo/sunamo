using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// For debug purposes
/// RefreshingList is derived from 
/// </summary>
/// <typeparam name="T"></typeparam>
public class L2<T> : RefreshingList<T>
{
	public L2() : base(null, 0)
	{

	}

	public L2(IEnumerable<T> list) : base(null, list)
	{

	}

	public L2(int capacity) : base(null, capacity)
	{

	}
}