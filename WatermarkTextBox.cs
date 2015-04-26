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
	///     <MyNamespace:WatermarkTextBox/>
	///
	/// </summary>
	public class WatermarkTextBox : TextBox
	{
		public string WatermarkText
		{
			get { return (string)GetValue(WatermarkTextProperty); }
			set { SetValue(WatermarkTextProperty, value); }
		}

		// Using a DependencyProperty as the backing store for WatermarkText.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WatermarkTextProperty =
			DependencyProperty.Register("WatermarkText", typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(null));

		public Brush WatermarkBrush
		{
			get { return (Brush)GetValue(WatermarkBrushProperty); }
			set { SetValue(WatermarkBrushProperty, value); }
		}

		// Using a DependencyProperty as the backing store for WatermarkBrush.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WatermarkBrushProperty =
			DependencyProperty.Register("WatermarkBrush", typeof(Brush), typeof(WatermarkTextBox), new PropertyMetadata(Brushes.LightGray));

		private Brush ForegroundBrush
		{
			get { return (Brush)GetValue(ForegroundBrushProperty); }
			set { SetValue(ForegroundBrushProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ForegroundBrush.  This enables animation, styling, binding, etc...
		private static readonly DependencyProperty ForegroundBrushProperty =
			DependencyProperty.Register("ForegroundBrush", typeof(Brush), typeof(WatermarkTextBox), new PropertyMetadata(null));

		private bool HasCustomText
		{
			get { return (bool)GetValue(HasCustomTextProperty); }
			set { SetValue(HasCustomTextProperty, value); }
		}

		// Using a DependencyProperty as the backing store for HasCustomText.  This enables animation, styling, binding, etc...
		private static readonly DependencyProperty HasCustomTextProperty =
			DependencyProperty.Register("HasCustomText", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata(false));

		public bool FocusHandlerRunning
		{
			get { return (bool)GetValue(FocusHandlerRunningProperty); }
			set { SetValue(FocusHandlerRunningProperty, value); }
		}

		// Using a DependencyProperty as the backing store for FocusHandlerRunning.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FocusHandlerRunningProperty =
			DependencyProperty.Register("FocusHandlerRunning", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata(false));

		static WatermarkTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox), new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
			EventManager.RegisterClassHandler(typeof(WatermarkTextBox), TextBox.GotFocusEvent, new RoutedEventHandler(OnGotFocusClassHandler));
			EventManager.RegisterClassHandler(typeof(WatermarkTextBox), TextBox.LostFocusEvent, new RoutedEventHandler(OnLostFocusClassHandler));
			EventManager.RegisterClassHandler(typeof(WatermarkTextBox), TextBox.TextChangedEvent, new TextChangedEventHandler(OnTextChangedClassHandler));
		}

		private static void OnGotFocusClassHandler(object sender, RoutedEventArgs e)
		{
			var textbox = sender as WatermarkTextBox;
			if (textbox != null)
			{
				textbox.FocusHandlerRunning = true;
				textbox.Foreground = textbox.ForegroundBrush;
				if (!textbox.HasCustomText)
				{
					textbox.Text = "";
				}
				textbox.FocusHandlerRunning = false;
			}
		}

		private static void OnLostFocusClassHandler(object sender, RoutedEventArgs e)
		{
			var textbox = sender as WatermarkTextBox;
			if (textbox != null && !textbox.HasCustomText)
			{
				textbox.FocusHandlerRunning = true;
				textbox.Foreground = textbox.WatermarkBrush;
				textbox.Text = textbox.WatermarkText;
				textbox.FocusHandlerRunning = false;
			}
		}

		private static void OnTextChangedClassHandler(object sender, TextChangedEventArgs e)
		{
			var textbox = sender as WatermarkTextBox;
			if (textbox != null && !textbox.FocusHandlerRunning)
			{
				textbox.HasCustomText = textbox.Text != "" && textbox.Text != null;
			}
			if (textbox.Text == null || textbox.Text.Length == 0 && !textbox.HasEffectiveKeyboardFocus)
			{
				textbox.Text = textbox.WatermarkText;
				textbox.Foreground = textbox.WatermarkBrush;
				textbox.HasCustomText = false;
			}
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			ForegroundBrush = Foreground;
			if (Text == null || Text.Length == 0)
			{
				Foreground = WatermarkBrush;
				Text = WatermarkText;
				HasCustomText = false;
			}
			else
			{
				HasCustomText = true;
			}
		}
	}
}
