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

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for Toolbar.xaml
    /// </summary>
    public partial class EditorToolbar : UserControl
    {

        #region Variable Decleration

        //Enum for selected tool
        public enum Tool
        {
            Pen,
            Erase
        }

        public Tool selectedTool = Tool.Pen;

        #endregion

       
        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public EditorToolbar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the user pressing the Pen Selection Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlePenSelection(object sender, RoutedEventArgs e)
        {
            selectedTool = Tool.Pen;
        }

        /// <summary>
        /// Handles the user pressing the Erase Selection Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleEraseSelection(object sender, RoutedEventArgs e)
        {
            selectedTool = Tool.Erase;
        }
    }
}
