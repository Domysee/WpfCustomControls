using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	///     "Add Reference"->"Projects"->[Browse to and select this project]
	///
	///
	/// Step 2)
	/// Go ahead and use your control in the XAML file.
	///
	///     <MyNamespace:ProgressTabView/>
	///
	/// </summary>
	public class ProcessTabItem : TabItem
	{
		private const double distance = 10;

		static ProcessTabItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ProcessTabItem), new FrameworkPropertyMetadata(typeof(ProcessTabItem)));
		}

		private Polygon polygon;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			polygon = GetTemplateChild("PART_polygon") as Polygon;

			this.AddHandler(SizeChangedEvent, new RoutedEventHandler((sender, e) =>
			{
				polygon.Points[0] = new Point(0, 0);
				polygon.Points[1] = new Point(distance / 2, this.ActualHeight / 2);
				polygon.Points[2] = new Point(0, this.ActualHeight);
				polygon.Points[3] = new Point(this.ActualWidth, this.ActualHeight);
				polygon.Points[4] = new Point(this.ActualWidth + distance / 2, this.ActualHeight / 2);
				polygon.Points[5] = new Point(this.ActualWidth, 0);
				polygon.Points[6] = new Point(0, 0);
			}));
		}
	}
}
