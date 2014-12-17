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
    /// Interaktionslogik für GameStyleView.xaml
    /// </summary>
    public partial class GameStyleView : IView
    {
        public GameStyleView(MainWindow mw, bool overlay, IView p) :
            base(mw, overlay, p)
        {
            InitializeComponent();
        }

        public override bool Close()
        {
            return true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.ViewHost.CloseView(this);
        }
    }
}
