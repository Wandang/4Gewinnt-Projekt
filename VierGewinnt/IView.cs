using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VierGewinnt
{
    /// <summary>
    /// Represents a View used in the MainWindow
    /// </summary>
    public abstract class IView : Page
    {
        #region Propertys
        /// <summary>
        /// View Host this View is attached to
        /// </summary>
        public MainWindow ViewHost { get; set; }
        
        /// <summary>
        /// Represents if this View is used as a Overlay or not
        /// </summary>
        public bool IsOverlay { get; set; }

        /// <summary>
        /// Parent View of this View
        /// </summary>
        public IView ParentView { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create new View
        /// </summary>
        /// <param name="mw">Window that this View is Attached to</param>
        public IView(MainWindow mw)
            :this(mw, false, null) { }

        /// <summary>
        /// Create new View
        /// </summary>
        /// <param name="mw">Window that this View is Attached to</param>
        /// <param name="p">Parent View that we should return to when this view is closed</param>
        public IView(MainWindow mw, IView p)
            : this(mw, false, p) { }

        /// <summary>
        /// Create new View
        /// </summary>
        /// <param name="wm">Window that this View is Attached to</param>
        /// <param name="Overlay">Is this Page a Overlay or not</param>
        public IView(MainWindow wm, bool Overlay, IView p) {
            this.ViewHost = wm;
            this.IsOverlay = Overlay;
            this.ParentView = p;

            this.Loaded += IView_Loaded;
        }

        void IView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Focusable = true;
            this.Focus();
        }
        #endregion

        /// <summary>
        /// Close this View properly
        /// </summary>
        public abstract bool Close();

    }
}
