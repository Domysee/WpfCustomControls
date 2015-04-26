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
	///     <MyNamespace:ProgressBarTextBox/>
	///
	/// </summary>
	public class ProgressBarTextBox : TextBox
	{
		public int Progress
		{
			get { return (int)GetValue(ProgressProperty); }
			set { SetValue(ProgressProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Progress.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ProgressProperty =
			DependencyProperty.Register("Progress", typeof(int), typeof(ProgressBarTextBox), new PropertyMetadata(0, new PropertyChangedCallback(OnProgressChanged)));

		private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var textbox = d as ProgressBarTextBox;
			if (textbox != null)
			{
				if (textbox.progressStop != null && textbox.nonProgressStop != null)
				{
					textbox.progressStop.Offset = textbox.Progress / 100.0;
					textbox.nonProgressStop.Offset = textbox.Progress / 100.0;
				}
			}
		}

		public Color ProgressColor
		{
			get { return (Color)GetValue(ProgressColorProperty); }
			set { SetValue(ProgressColorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ProgressColor.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ProgressColorProperty =
			DependencyProperty.Register("ProgressColor", typeof(Color), typeof(ProgressBarTextBox), new PropertyMetadata(Brushes.LightGreen.Color));



		static ProgressBarTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBarTextBox), new FrameworkPropertyMetadata(typeof(ProgressBarTextBox)));
		}

		private GradientStop progressStop;
		private GradientStop nonProgressStop;
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			var border = GetTemplateChild("border") as Border;
			progressStop = (border.Background as LinearGradientBrush).GradientStops[0];
			nonProgressStop = (border.Background as LinearGradientBrush).GradientStops[1];
			progressStop.Offset = Progress / 100.0;
			nonProgressStop.Offset = Progress / 100.0;
			progressStop.Color = ProgressColor;
		}
	}
}
