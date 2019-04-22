using HelperTrinity;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;


namespace Kent.Boogaart.MVVM
{
	/// <summary>
	/// A base class for all view models.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class serves as a base class for view models. It implements <see cref="INotifyPropertyChanged"/> and provides access to a <see cref="Dispatcher"/>.
	/// </para>
	/// </remarks>
	public abstract class ViewModel : INotifyPropertyChanged
	{
		private readonly Dispatcher _dispatcher;

		/// <summary>
		/// Constructs an instance of <c>ViewModel</c>.
		/// </summary>
		protected ViewModel()
		{
			if (Application.Current != null)
			{
				_dispatcher = Application.Current.Dispatcher;
			}
			else
			{
				//this is useful for unit tests where there is no application running
				_dispatcher = Dispatcher.CurrentDispatcher;
			}
		}

		/// <summary>
		/// Occurs whenever a property on this object changes in value.
		/// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Gets a <see cref="Dispatcher"/> that can be used to dispatch messages to the UI thread.
		/// </summary>
		protected Dispatcher Dispatcher
		{
			get { return _dispatcher; }
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="e">
		/// The event arguments.
		/// </param>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged.Raise(this, e);
		}

		/// <summary>
		/// A convenience method that raises the <see cref="PropertyChanged"/> event for a specified property name.
		/// </summary>
		/// <param name="propertyName">
		/// The name of the property that changed.
		/// </param>
		protected void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
	}
}
