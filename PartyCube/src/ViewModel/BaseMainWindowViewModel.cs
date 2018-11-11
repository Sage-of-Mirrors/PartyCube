using Microsoft.Win32;
using OpenTK;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Input;

namespace OpenTKFramework.ViewModel
{
    public class BaseMainWindowViewModel : INotifyPropertyChanged
    {
        #region File Input
        #region string WindowTitle
        private string m_windowTitle;

        public string WindowTitle
        {
            get { return string.Format("{0} - OpenTK with WPF Framework", m_windowTitle); }
            set
            {
                if (m_windowTitle != value)
                {
                    m_windowTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        
        public virtual void Open()
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if ((bool)openFile.ShowDialog())
            {
                string fileName = openFile.FileName;
                WindowTitle = fileName;
            }
        }
        #endregion

        #region Rendering
        private Renderer m_renderer;

        internal void CreateGraphicsContext(GLControl ctrl, WindowsFormsHost host)
        {
            m_renderer = new Renderer(ctrl, host);
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Command Callbacks
        /// <summary> The user has requested to open a file. </summary>
        public ICommand OnRequestOpenFile
        {
            get { return new RelayCommand(x => Open()); }
        }

        /// <summary> The user has requested to save the currently open data back to the file they originally opened. </summary>
        public ICommand OnRequestSave
        {
            get { return new RelayCommand(x => Open(), x => false); }
        }

        /// <summary> The user has requested to save the currently open data to a new file. </summary>
        public ICommand OnRequestSaveAs
        {
            get { return new RelayCommand(x => Open(), x => false); }
        }

        /// <summary> The user has requested to unload the currently open data. </summary>
        public ICommand OnRequestClose
        {
            get { return new RelayCommand(x => Open(), x => false); }
        }

        /// <summary> The user has pressed Alt + F4, chosen Exit from the File menu, or clicked the close button. </summary>
        public ICommand OnRequestApplicationExit
        {
            get { return new RelayCommand(x => ExitApplication()); }
        }

        /// <summary> The user has clicked Report a Bug... from the Help menu. </summary>
        public ICommand OnRequestReportBug
        {
            get { return new RelayCommand(x => ReportBug()); }
        }

        /// <summary> The user has clicked Report a Bug... from the Help menu. </summary>
        public ICommand OnRequestOpenWiki
        {
            get { return new RelayCommand(x => OpenWiki()); }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public virtual void ExitApplication()
        {
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// Opens the user's default browser to OpenGL_in_WPF_Framework's Issues page.
        /// </summary>
        public virtual void ReportBug()
        {
            System.Diagnostics.Process.Start("https://github.com/Sage-of-Mirrors/OpenTK_with_WPF_Framework/issues");
        }

        /// <summary>
        /// Opens the user's default browser to OpenGL_in_WPF_Framework's Wiki page.
        /// </summary>
        public virtual void OpenWiki()
        {
            System.Diagnostics.Process.Start("https://github.com/Sage-of-Mirrors/OpenTK_with_WPF_Framework/wiki");
        }
        #endregion
    }
}
