using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimeFromSeconds
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private decimal inputDec;
        public MainPage()
        {
            this.InitializeComponent();
            Seconds.IsEnabled = false;
            Minutes.IsEnabled = false;
            Hours.IsEnabled = false;
            Days.IsEnabled = false;
        }

        private void Seconds_Click(object sender, RoutedEventArgs e)
        {
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
                OutputBox.Text = "0";
            else
            {
                TimeFromSeconds aTFS = new TimeFromSeconds(TimeFromSeconds.Unit.Seconds, inputDec);
                OutputBox.Text = aTFS.ToString();
            }
        }//Seconds_Click

        private void Minutes_Click(object sender, RoutedEventArgs e)
        {
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
                OutputBox.Text = "0";
            else
            {
                TimeFromSeconds aTFS = new TimeFromSeconds(TimeFromSeconds.Unit.Minutes, inputDec);
                OutputBox.Text = aTFS.ToString();
            }
        }//Minutes_Click

        private void Hours_Click(object sender, RoutedEventArgs e)
        {
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
                OutputBox.Text = "0";
            else
            {
                TimeFromSeconds aTFS = new TimeFromSeconds(TimeFromSeconds.Unit.Hours, inputDec);
                OutputBox.Text = aTFS.ToString();
            }
        }//Hours_Click

        private void Days_Click(object sender, RoutedEventArgs e)
        {
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
                OutputBox.Text = "0";
            else
            {
                TimeFromSeconds aTFS = new TimeFromSeconds(TimeFromSeconds.Unit.Days, inputDec);
                OutputBox.Text = aTFS.ToString();
            }
        }//Days_Click

        private void InputBox_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Text = string.Empty;
        }//InputBox_click
        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Decimal.TryParse(InputBox.Text, out inputDec))
            {
                Seconds.IsEnabled = true;
                Minutes.IsEnabled = true;
                Hours.IsEnabled = true;
                Days.IsEnabled = true;
            }//fi
            else
            {
                Seconds.IsEnabled = false;
                Minutes.IsEnabled = false;
                Hours.IsEnabled = false;
                Days.IsEnabled = false;
            }//else
        }//InputBox_TextChanged()
    }//MainPage
}//namespace
