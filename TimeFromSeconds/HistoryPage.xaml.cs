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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeFromSeconds
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HistoryPage : Page
    {
        private HistoryList historyList;
        private List<string> stringList = new List<string>();
        public HistoryPage()
        {
            this.InitializeComponent();
            historyList = DBConnector.GetHistoryList();
            foreach (HistoryItem i in historyList.GetHistoryList())
            {
                stringList.Add(i.ToString());
            }//foreach
        }//HistoryPage()
    }//HistoryPage
}//namespace
