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

using VierGewinnt.Model;

namespace VierGewinnt.Views
{
    /// <summary>
    /// Interaktionslogik für GameView.xaml
    /// </summary>
    public partial class GameView : IView
    {
        public GameView(MainWindow mw, IView p, IPlayer p1, IPlayer p2) :
            base(mw, false, p)
        {
            InitializeComponent();
            

        }

        public override bool Close()
        {
            return true;
        }
    }
}
