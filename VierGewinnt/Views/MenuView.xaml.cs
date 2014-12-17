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

namespace VierGewinnt.Views
{
    /// <summary>
    /// Interaktionslogik für MenuView.xaml
    /// </summary>
    public partial class MenuView : IView
    {
        /// <summary>
        /// Create new Menu View
        /// </summary>
        /// <param name="mw">Window to attach to</param>
        /// <param name="overlay">Is this a Overlay or not</param>
        /// <param name="p">Parent View of this View</param>
        public MenuView(MainWindow mw, bool overlay, IView p) :
            base(mw, overlay, p)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create new Menu View
        /// </summary>
        /// <param name="mw">Window to attach to</param>
        public MenuView(MainWindow mw) :
            this(mw, false, null) { }

        /// <summary>
        /// Close this View properly
        /// </summary>
        public override bool Close()
        {
            return (this.ParentView == null);
        }

        /// <summary>
        /// Handler for every button that whants to close this Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.ViewHost.CloseView(this);
        }

        /// <summary>
        /// Handler for Buttons that open the Play Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.ViewHost.Navigate(new GameStyleView(this.ViewHost, false, this));
        }

        /// <summary>
        /// Handler for Buttons that open the Options Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOptions_Click(object sender, RoutedEventArgs e)
        {
            this.ViewHost.Navigate(new OptionsView(this.ViewHost, false, this));
        }
    }
}
