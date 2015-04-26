using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomControlLibrary
{
	public class PlaceableScrollbarScrollViewer : ScrollViewer
	{
		static PlaceableScrollbarScrollViewer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceableScrollbarScrollViewer), new FrameworkPropertyMetadata(typeof(PlaceableScrollbarScrollViewer)));
		}



		public Dock VerticalScrollbarPlacement
		{
			get { return (Dock)GetValue(VerticalScrollbarPlacementProperty); }
			set { SetValue(VerticalScrollbarPlacementProperty, value); }
		}

		// Using a DependencyProperty as the backing store for VerticalScrollbarPlacement.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty VerticalScrollbarPlacementProperty =
			DependencyProperty.Register("VerticalScrollbarPlacement", typeof(Dock), typeof(PlaceableScrollbarScrollViewer), new PropertyMetadata(Dock.Right));



		public Dock HorizontalScrollbarPlacement
		{
			get { return (Dock)GetValue(HorizontalScrollbarPlacementProperty); }
			set { SetValue(HorizontalScrollbarPlacementProperty, value); }
		}

		// Using a DependencyProperty as the backing store for HorizontalScrollbarPlacement.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HorizontalScrollbarPlacementProperty =
			DependencyProperty.Register("HorizontalScrollbarPlacement", typeof(Dock), typeof(PlaceableScrollbarScrollViewer), new PropertyMetadata(Dock.Bottom));


	}
}
