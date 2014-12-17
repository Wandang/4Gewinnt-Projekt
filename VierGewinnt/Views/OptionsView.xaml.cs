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
    /// Interaktionslogik für OptionsView.xaml
    /// </summary>
    public partial class OptionsView : IView
    {
        public OptionsView(MainWindow mw, bool overlay, IView p) : 
            base (mw, overlay, p)
        {
            InitializeComponent();
        }

        public override bool Close()
        {
            throw new NotImplementedException();
        }

    }
}
