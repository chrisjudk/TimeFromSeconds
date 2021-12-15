using System;
using System.Text;
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
using Entities;
using Controller;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimeFromSeconds
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool subSecBool = false;
        private TFS aTFS = new TFS(TFS.Unit.Seconds, 0m,false);
        private decimal inputDec;
        private TFS.Unit currentUnit = TFS.Unit.Millennia;
        public MainPage()
        {
            this.InitializeComponent();
            Seconds.IsEnabled = false;
            Minutes.IsEnabled = false;
            Hours.IsEnabled = false;
            Days.IsEnabled = false;
        }//MainPage()

        private void Seconds_Click(object sender, RoutedEventArgs e)
        {
            currentUnit = TFS.Unit.Seconds;
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
            {
                OutputBox.Text = "0";
            }//fi
            else
            {
                aTFS = aTFS.Set(currentUnit, inputDec, subSecBool);
                OutputBox.Text = aTFS.ToString();
            }//else
        }//Seconds_Click

        private void Minutes_Click(object sender, RoutedEventArgs e)
        {
            currentUnit = TFS.Unit.Minutes;
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
            {
                OutputBox.Text = "0";
            }//fi
            else
            {
                aTFS = aTFS.Set(currentUnit, inputDec, subSecBool);
                OutputBox.Text = aTFS.ToString();
            }//else
        }//Minutes_Click

        private void Hours_Click(object sender, RoutedEventArgs e)
        {
            currentUnit = TFS.Unit.Hours;
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
                OutputBox.Text = "0";
            else
            {
                aTFS = aTFS.Set(currentUnit, inputDec, subSecBool);
                OutputBox.Text = aTFS.ToString();
            }//else
        }//Hours_Click

        private void Days_Click(object sender, RoutedEventArgs e)
        {
            currentUnit = TFS.Unit.Days;
            if (inputDec.Equals(decimal.Zero) || inputDec.Equals(null))
                OutputBox.Text = "0";
            else
            {
                aTFS = aTFS.Set(currentUnit, inputDec, subSecBool);
                OutputBox.Text = aTFS.ToString();
            }//else
        }//Days_Click

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Decimal.TryParse(InputBox.Text, out inputDec))
            {
                Seconds.IsEnabled = true;
                Minutes.IsEnabled = true;
                Hours.IsEnabled = true;
                Days.IsEnabled = true;
                if(currentUnit != TFS.Unit.Millennia)
                {
                    aTFS.Set(currentUnit, inputDec, subSecBool);
                    OutputBox.Text = aTFS.ToString();
                }//fi
            }//fi
            else
            {
                Seconds.IsEnabled = false;
                Minutes.IsEnabled = false;
                Hours.IsEnabled = false;
                Days.IsEnabled = false;
            }//else
        }//InputBox_TextChanged()

        private void SubSec_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch tog)
            {
                subSecBool = tog.IsOn;//else
                switch (currentUnit)
                {
                    case TFS.Unit.Seconds:
                        {
                            Seconds_Click(sender, e);
                            break;
                        }//seconds

                    case TFS.Unit.Minutes:
                        {
                            Minutes_Click(sender, e);
                            break;
                        }//minutes

                    case TFS.Unit.Hours:
                        {
                            Hours_Click(sender, e);
                            break;
                        }//minutes

                    case TFS.Unit.Days:
                        {
                            Days_Click(sender, e);
                            break;
                        }//days

                    default:
                        break;
                }//switch
            }//fi
        }//SubSec_Toggled()
    }//MainPage
}//namespace
