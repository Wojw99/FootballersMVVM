using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace FootballersMVVM.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy TextBoxErrorProvider.xaml
    /// </summary>
    public partial class TextBoxErrorProvider : UserControl
    {
        #region Properties
        public string Text
        {
            get 
            {
                return (string)GetValue(TextProperty); 
            }
            set 
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxErrorProvider), new PropertyMetadata(""));
        #endregion

        public TextBoxErrorProvider()
        {
            InitializeComponent();
        }

        public void SetMessage(string text)
        {
            if (text != "")
            {
                toolTip.Visibility = Visibility.Visible;
                toolTip.Content = text;
                border.BorderThickness = new Thickness(1);
            }
            else
            {
                toolTip.Visibility = Visibility.Hidden;
                border.BorderThickness = new Thickness(0);
            }
        }

        private void Input(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(textBox.Text, @"\d"))
            {
                SetMessage("There cannot be numbers here!");
            }
            else if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                SetMessage("There cannot be null or whitespace here!");
            }
            else
            {
                SetMessage("");
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(textBox.Text, @"\d"))
            {
                SetMessage("There cannot be numbers here!");
            }
            else if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                SetMessage("Field cannot be empty!");
            }
            else
            {
                SetMessage("");
            }
        }
    }
}
