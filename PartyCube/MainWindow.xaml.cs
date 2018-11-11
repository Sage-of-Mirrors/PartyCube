using OpenTK;
using OpenTK.Graphics;
using OpenTKFramework.ViewModel;
using System;
using System.Windows;

namespace OpenTKFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseMainWindowViewModel m_viewModel;

        public MainWindow()
        {
            m_viewModel = new BaseMainWindowViewModel();
            DataContext = m_viewModel;
            InitializeComponent();
        }

        private void GLHost_Initialized(object sender, EventArgs e)
        {
            GLControl m_glControl;

            m_glControl = new GLControl(new GraphicsMode(32,24), 3, 0, GraphicsContextFlags.Default);
            m_glControl.MakeCurrent();
            m_glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            m_glControl.AllowDrop = true;
            m_glControl.BackColor = System.Drawing.Color.Fuchsia;
            m_viewModel.CreateGraphicsContext(m_glControl, GLHost);

            GLHost.Child = m_glControl;
            GLHost.AllowDrop = true;
        }
    }
}
