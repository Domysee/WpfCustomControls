using System;
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
using System.Windows.Media.Animation;
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
	///     "Add Reference"->"Projects"->[Browse to and select this project]
	///
	///
	/// Step 2)
	/// Go ahead and use your control in the XAML file.
	///
	///     <MyNamespace:RippleEffectDecorator/>
	///
	/// </summary>
	public class RippleEffectDecorator : ContentControl
	{
		static RippleEffectDecorator()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleEffectDecorator), new FrameworkPropertyMetadata(typeof(RippleEffectDecorator)));
		}

		public Brush HighlightBackground
		{
			get { return (Brush)GetValue(HighlightBackgroundProperty); }
			set { SetValue(HighlightBackgroundProperty, value); }
		}

		// Using a DependencyProperty as the backing store for HighlightBackground.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HighlightBackgroundProperty =
			DependencyProperty.Register("HighlightBackground", typeof(Brush), typeof(RippleEffectDecorator), new PropertyMetadata(Brushes.White));

		Ellipse ellipse;
		Grid grid;
		Storyboard animation;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			ellipse = GetTemplateChild("PART_ellipse") as Ellipse;
			grid = GetTemplateChild("PART_grid") as Grid;
			animation = grid.FindResource("PART_animation") as Storyboard;

			this.AddHandler(MouseDownEvent, new RoutedEventHandler((sender, e) =>
			{
				var mousePosition = (e as MouseButtonEventArgs).GetPosition(this);
				ellipse.Margin = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);
				(animation.Children[0] as DoubleAnimation).To = ActualWidth * 2;
				(animation.Children[1] as ThicknessAnimation).From = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);
				(animation.Children[1] as ThicknessAnimation).To = new Thickness(mousePosition.X - ActualWidth * 2 / 2, mousePosition.Y - ActualWidth * 2 / 2, 0, 0);
				ellipse.BeginStoryboard(animation);
			}), true);
		}
	}
}
