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
using System.Windows.Shapes;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for FileImportErrorDialouge.xaml
    /// </summary>
    public partial class FileImportErrorDialouge : Window
    {
        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public FileImportErrorDialouge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Marks the dialogue as finished with after the ok button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
