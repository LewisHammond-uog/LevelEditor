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
    /// Interaction logic for ImportTileSheetDialouge.xaml
    /// </summary>
    public partial class ImportTileSheetDialouge : Window
    {

        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public ImportTileSheetDialouge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the dimentions of a tilesheet being imported by the user
        /// </summary>
        /// <returns>Tile Width and Tile Height as tupple</returns>
        public (int tileWidth, int tileHeight) GetTileDimentions()
        {
            //Get values from user input
            int widthSelection = (int)TileWidthSelection.Value;
            int heightSelection = (int)TileHeightSelection.Value;

            return (widthSelection, heightSelection);
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
