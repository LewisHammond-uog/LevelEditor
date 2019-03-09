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

namespace LevelEditor.Windows
{
    /// <summary>
    /// Interaction logic for NewGridDialouge.xaml
    /// </summary>
    public partial class NewGridDialouge : Window
    {
        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public NewGridDialouge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the grid size that the user wants the grid to be
        /// </summary>
        /// <returns>Grid Width, Height and Box size (as tuple)</returns>
        public (int width, int height, int boxSize) GetGridCreationSize()
        {
            //Get the values from the int up/downs
            int widthSelection = (int)GridWidthSelection.Value;
            int heightSelection = (int)GridHeightSelection.Value;
            int boxSizeSelection = (int)GridBoxSizeSelection.Value;

            return (widthSelection,heightSelection,boxSizeSelection);
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
