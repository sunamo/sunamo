using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

public class NotifyPropertyChangedWrapper<T> : INotifyPropertyChanged where T : DependencyObject
{
	public DependencyProperty dpIsChecked;
	public T o = default(T);
	public DependencyProperty dpContent;
	public DependencyProperty dpTag;
	public DependencyProperty dpVisibility;
	public DependencyProperty dpHeight;
	public DependencyProperty dpWidth;
	DependencyProperty capturedProperty;
	public double originalHeight = double.NaN;

	public object Content
	{
		get
		{
			return Control.GetValue(dpContent);
		}
		set
		{
			Control.SetValue(dpContent, value);
		}
	}

	private bool isActive = true;

	public bool IsActive
	{
		get { return isActive; }
		set { isActive = value;
			OnPropertyChanged("IsActive");
		}
	}


	public double Height
	{
		get
		{
			return (double)Control.GetValue(dpHeight);
		}
		set
		{
			Control.SetValue(dpHeight, value);
		}
	}

	public Visibility Visibility
	{
		get
		{
			return (Visibility)Control.GetValue(dpVisibility);
		}
		set
		{
			//if (originalHeight == double.NaN)
			//{
			//	originalHeight = Height;
			//}

			if (value == Visibility.Collapsed)
			{
				//Control.SetValue(dpHeight, 0);
				isActive = false;
			}
			else 
			{
				//Control.SetValue(dpHeight, originalHeight);
				isActive = true;
			}

			Control.SetValue(dpVisibility, value);
		}
	}

	public object Tag
	{
		get
		{
			return Control.GetValue(dpTag);
		}
		set
		{
			Control.SetValue(dpTag, value);
		}
	}

	public bool? IsChecked
	{
		get
		{
			return (bool?)Control.GetValue(dpIsChecked);
		}
		set
		{
			Control.SetValue(dpIsChecked, value);
		}
	}

	public DependencyObject Control
	{
		get
		{
			DependencyObject w = (DependencyObject)o;
			return w;
		}
	}

	/// <summary>
	/// A2 can be null
	/// </summary>
	/// <param name="o"></param>
	/// <param name="d"></param>
	public NotifyPropertyChangedWrapper(T o, DependencyProperty d)
	{
		this.o = o;

		if (o.GetType() == TypesControls.tCheckBox)
		{
			NotifyPropertyHelper.CheckBox<T>(this);
		}

		if (d != null)
		{
			this.capturedProperty = d;
			//this.dpIsChecked = d;
			//this.DataContext = o;

			DependencyPropertyDescriptor
				.FromProperty(d, typeof(T))
				.AddValueChanged(o, (s, e) => { OnPropertyChanged(capturedProperty.Name); });
		}
	}

	void OnPropertyChanged(string propName)
	{
		PropertyChanged(this, new PropertyChangedEventArgs(propName));
	}

	public event PropertyChangedEventHandler PropertyChanged;
}