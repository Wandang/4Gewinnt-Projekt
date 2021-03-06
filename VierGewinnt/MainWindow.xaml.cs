﻿using System;
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

using VierGewinnt.Views;

namespace VierGewinnt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Init our Utils (Logger etc)
            Utils.Utils.Init();

            //Now the GUI
            InitializeComponent();
            Navigate(new MenuView(this));
        }

        /// <summary>
        /// Navigate the MainWindow to a new View
        /// </summary>
        /// <param name="to">The View we Should navigate to</param>
        public void Navigate(IView to)
        {
            if (to.IsOverlay)
            {
                OverlayFrame.Content = to;
            }
            else
            {
                MainFrame.Content = to;
            }
        }

        /// <summary>
        /// Uses the Overlay frame to overlay the current MainView
        /// </summary>
        /// <param name="ov">View to use in Overlay</param>
        public void Overlay(IView ov)
        {
            Dispatcher.Invoke(() =>
            {
                BlendRect.Visibility = Visibility.Visible;
                OverlayFrame.Content = ov;
            });
            
        }

        /// <summary>
        /// Close a view
        /// If the last view is closed we will close this Window
        /// </summary>
        /// <param name="view">View to close</param>
        public void CloseView(IView view)
        {
            if (view.ParentView != null)
            {
                if (view.Close())
                {
                    if (view.IsOverlay)
                    {
                        OverlayFrame.Content = view.ParentView;
                    }
                    else
                    {
                        MainFrame.Content = view.ParentView;
                    }
                }
            }
            else
            {
                if (view.IsOverlay || view.Close())
                {
                    if (view.IsOverlay)
                    {
                        OverlayFrame.Content = null;
                        BlendRect.Visibility = Visibility.Hidden;
                        view.Close();
                        ((IView) MainFrame.Content).Focus();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }

    }
}
