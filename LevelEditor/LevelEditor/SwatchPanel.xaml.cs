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
    /// Interaction logic for SwatchPanel.xaml
    /// </summary>
    public partial class SwatchPanel : UserControl
    {

        #region Variable Declerations

        //Border Components (width and colour)
        private int borderWidth = 2;
        private SolidColorBrush selectedBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush unselectedBrush = new SolidColorBrush(Colors.White);

        //Selected Swatch and selected Tile 
        private Border selectedSwatch = null;
        public ImageSource SelectedTile { private set; get; } = null;

        #endregion

        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public SwatchPanel()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Adds a sprite to the swatch panel from file path
        /// </summary>
        /// <param name="filePath">File path of image to add</param>
        public void AddSprite(string filePath)
        {
            //Convert file path to an actual bitmap image and call the add sprite function that uses an image
            var image = new Image() { Source = new BitmapImage(new Uri(filePath)) };
            image.Stretch = Stretch.None;
            AddSprite(image);
        }

        /// <summary>
        /// Adds an image to the swatch panel
        /// </summary>
        /// <param name="image">Image to add</param>
        public void AddSprite(Image image)
        {
            image.Stretch = Stretch.None;

            //Import a sprite and add a border around it then add it to the stack Panel
            Border border = new Border
            {
                BorderThickness = new Thickness(borderWidth),
                BorderBrush = unselectedBrush,
                Child = image
            };

            border.MouseDown += OnMouseDown;

            stackPanel.Children.Add(border);
        }

        /// <summary>
        /// Adds a tile sheet to the swatch panel
        /// </summary>
        /// <param name="filePath">File path of tile sheet</param>
        /// <param name="tileWidth">Width of each tile</param>
        /// <param name="tileHeight">Height of each tile</param>
        public void AddSpriteSheet(string filePath, int tileWidth, int tileHeight)
        {
            var imageSource = new BitmapImage(new Uri(filePath));

            //For the width and length of the imported image, crop the image up into different sections for each tile
            for (int y = 0; y < imageSource.PixelHeight; y += tileHeight)
            {
                for (int x = 0; x < imageSource.PixelWidth; x += tileWidth)
                {
                    var croppedBitmap = new CroppedBitmap(imageSource, new Int32Rect(x, y, tileWidth, tileHeight));
                    AddSprite(new Image() { Source = croppedBitmap });
                }
            }
        }

        /// <summary>
        /// Makes the clicked sprite the selected sprite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;

            //If another item is already selected set it to have the unselected border
            if (selectedSwatch != null)
            {
                selectedSwatch.BorderBrush = unselectedBrush;
            }

            //Give the selected swatch a border
            selectedSwatch = border;
            border.BorderBrush = selectedBrush;
            int selectedIndex = stackPanel.Children.IndexOf(border);

            //Set the selected tile, this is the raw image without the border
            SelectedTile = (border.Child as Image).Source;
        }

    }
}
