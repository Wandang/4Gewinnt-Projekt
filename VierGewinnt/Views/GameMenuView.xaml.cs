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
    public partial class GameMenuView : IView
    {
        /// <summary>
        /// Create new Menu View
        /// </summary>
        /// <param name="mw">Window to attach to</param>
        /// <param name="overlay">Is this a Overlay or not</param>
        /// <param name="p">Parent View of this View</param>
        public GameMenuView(MainWindow mw, bool overlay, IView p) :
            base(mw, overlay, p)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create new Menu View
        /// </summary>
        /// <param name="mw">Window to attach to</param>
        public GameMenuView(MainWindow mw) :
            this(mw, false, null) { }

        /// <summary>
        /// Close this View properly
        /// </summary>
        public override bool Close()
        {
            return (ParentView == null || IsOverlay);
        }

        /// <summary>
        /// Handler for every button that whants to close this Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Handler for Buttons that open the Play Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            ViewHost.CloseView(this);
        }

        /// <summary>
        /// Handler for Buttons that open the Options Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOptions_Click(object sender, RoutedEventArgs e)
        {
            ViewHost.Navigate(new OptionsView(ViewHost, true, this));
        }
    }
}
