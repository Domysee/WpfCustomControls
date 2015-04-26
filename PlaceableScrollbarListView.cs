﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControlLibrary
{
	/// <summary>
	/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
	///
	/// Step 1a) Using this custom control in a XAML file that exists in the current project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:CustomControlLibrary"
	///
	///
	/// Step 1b) Using this custom control in a XAML file that exists in a different project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:CustomControlLibrary;assembly=CustomControlLibrary"
	///
	/// You will also need to add a project reference from the project where the XAML file lives
	/// to this project and Rebuild to avoid compilation errors:
	///
	///     Right click on the target project in the Solution Explorer and
	///     "Add Reference"->"Projects"->[Select this project]
	///
	///
	/// Step 2)
	/// Go ahead and use your control in the XAML file.
	///
	///     <MyNamespace:CustomControl1/>
	///
	/// </summary>
	public class PlaceableScrollbarListView : ListView
	{
		static PlaceableScrollbarListView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceableScrollbarListView), new FrameworkPropertyMetadata(typeof(PlaceableScrollbarListView)));
		}

		public Dock VerticalScrollbarPlacement
		{
			get { return (Dock)GetValue(VerticalScrollbarPlacementProperty); }
			set { SetValue(VerticalScrollbarPlacementProperty, value); }
		}

		// Using a DependencyProperty as the backing store for VerticalScrollbarPlacement.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty VerticalScrollbarPlacementProperty =
			DependencyProperty.Register("VerticalScrollbarPlacement", typeof(Dock), typeof(PlaceableScrollbarListView), new PropertyMetadata(Dock.Right));



		public Dock HorizontalScrollbarPlacement
		{
			get { return (Dock)GetValue(HorizontalScrollbarPlacementProperty); }
			set { SetValue(HorizontalScrollbarPlacementProperty, value); }
		}

		// Using a DependencyProperty as the backing store for HorizontalScrollbarPlacement.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HorizontalScrollbarPlacementProperty =
			DependencyProperty.Register("HorizontalScrollbarPlacement", typeof(Dock), typeof(PlaceableScrollbarListView), new PropertyMetadata(Dock.Bottom));
	}
}
