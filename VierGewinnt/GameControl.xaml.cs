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

namespace VierGewinnt
{
    /// <summary>
    /// Interaktionslogik für GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        public GameControl()
        {
            InitializeComponent();

            canvas.Me

            int height = (int)(canvas.ActualHeight / 6);
            int width = (int)(canvas.ActualWidth / 7);

            Console.WriteLine(canvas.ActualWidth);

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    Button btn = new Button();
                    btn.Click += btn_Click;
                    btn.Width = width;
                    btn.Height = height;

                    canvas.Children.Add(btn);
                    Canvas.SetTop(btn, y * height);
                    Canvas.SetLeft(btn, x * height);
                }
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
