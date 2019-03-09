using LevelEditor.Classes;
using LevelEditor.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for EditorMenu.xaml
    /// </summary>
    public partial class EditorMenu : System.Windows.Controls.UserControl
    {

        //Max Import Size Variables
        const int MaxImportTileWidth = 64;
        const int MaxImportTileHeight = 64;

        //Vars for other editor components
        EditorGrid levelGrid;
        SwatchPanel swatchPanel;
        InfomationBar infoBar;

        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public EditorMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initalises getting the other elements of the level editor
        /// </summary>
        public void Initalise()
        {
            levelGrid = LevelEditorRefrences.GetEditorGrid(this);
            swatchPanel = LevelEditorRefrences.GetSwatchPanel(this);
            infoBar = LevelEditorRefrences.GetInfomationBar(this);
        }

        /// <summary>
        /// Saves the current level along with metadata
        /// </summary>
        /// <param name="sender">Sender of the "click" event</param>
        /// <param name="e">Mouse Args from "click" event</param>
        private void HandleSave(object sender, RoutedEventArgs e)
        {

            //Do not try and save an empty grid
            if(levelGrid.GridBoxSize == 0)
            {
                return;
            }

            //Hide the grid lines while we save the image, so that they are not saved in the image itself 
            levelGrid.TileGrid.ShowGridLines = false;

            //Create a folder browser so that the user can choose a folder to save in
            SaveFileDialog saveFolderDialogue = new SaveFileDialog()
            {
                Filter = "Image Files (*.png)|*.png"
            };

            //If we have clicked ok after selecting a folder to save our level to then save the level
            if (saveFolderDialogue.ShowDialog() == DialogResult.OK)
            {
                //Breakup File name and path
                string fileName = System.IO.Path.GetFileNameWithoutExtension(saveFolderDialogue.FileName);
                string fileDirectory = System.IO.Path.GetDirectoryName(saveFolderDialogue.FileName);

                //Save files
                SaveLoadHandler saveHandler = new SaveLoadHandler();
                saveHandler.SaveLevel(levelGrid, fileName, fileDirectory);


                //Update Level Name in Info bar
                infoBar.UpdateLevelNameUI(fileName);
            }

            //Re-show grid lines
            levelGrid.TileGrid.ShowGridLines = true;
        }

        /// <summary>
        /// Loads a level selected by the user in a dialogue
        /// </summary>
        /// <param name="sender">Sender of the "click" event</param>
        /// <param name="e">Mouse Args from "click" event</param>
        private void HandleLoad(object sender, RoutedEventArgs e)
        {

            //Select File to Load
            OpenFileDialog loadFileDialogue = new OpenFileDialog()
            {
                Filter = "Image Files (*.png)|*.png"
            };

            //If we have clicked ok after selecting a folder to load our level from, then start to attempt to load the level
            if (loadFileDialogue.ShowDialog() == DialogResult.OK)
            {
                //Breakup File name (without extentsion) and path
                string fileName = System.IO.Path.GetFileNameWithoutExtension(loadFileDialogue.FileName);
                string fileDirectory = System.IO.Path.GetDirectoryName(loadFileDialogue.FileName);

                //Load files
                SaveLoadHandler loadHandler = new SaveLoadHandler();
                loadHandler.LoadLevel(levelGrid, fileName, fileDirectory);

                //Update Level Name in Info bar
                infoBar.UpdateLevelNameUI(fileName);

            }

        }

        /// <summary>
        /// Creates a new level editor grid
        /// </summary>
        /// <param name="sender">Sender of the "click" event</param>
        /// <param name="e">Mouse Args from "click" event</param>
        private void HandleNew(object sender, RoutedEventArgs e)
        {

            //Create a dialogue to select options about
            NewGridDialouge newGridDialogue = new NewGridDialouge();

            //Once the dialouge is closed then create a new grid with the user selected sizes
            if(newGridDialogue.ShowDialog() == true)
            {
                var (width, height, boxSize) = newGridDialogue.GetGridCreationSize();
                levelGrid.CreateGrid(width, height, boxSize);
            }

            //Reset Level Name
            infoBar.UpdateLevelNameUI("Untitled Level");
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleClose(object sender, RoutedEventArgs e)
        {
            //Exit
            Environment.Exit(0);
        }

        /// <summary>
        /// Handles the importing of a single tile from a file path
        /// </summary>
        /// <param name="sender">Sender of the "click" event</param>
        /// <param name="e">Mouse Args from "click" event</param>
        private void HandleImportTile(object sender, RoutedEventArgs e)
        {

            //Select File to Load
            OpenFileDialog loadFileDialogue = new OpenFileDialog()
            {
                Filter = "Image Files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp"
            };
            
            //If we have okayed out of the load file dialouge then add the image we have selected in to the swatch
            if(loadFileDialogue.ShowDialog() == DialogResult.OK)
            {
                swatchPanel.AddSprite(loadFileDialogue.FileName);
            }

        }

        /// <summary>
        /// Handles the importing of a tilesheet, loads a sprite sheet from a file path
        /// </summary>
        /// <param name="sender">Sender of the "click" event</param>
        /// <param name="e">Mouse Args from "click" event</param>
        private void HandleImportTileSheet(object sender, RoutedEventArgs e)
        {

            //Select File to Load
            OpenFileDialog loadFileDialogue = new OpenFileDialog()
            {
                Filter = "Image Files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp"
            };

            //If we have okayed out of the load file dialouge then add the image we have selected in to the swatch
            if (loadFileDialogue.ShowDialog() == DialogResult.OK)
            {
                //Create a dialouge to get the sizes required for the tilesheet
                ImportTileSheetDialouge tileSheetDialouge = new ImportTileSheetDialouge();

                if(tileSheetDialouge.ShowDialog() == true)
                {
                    //Get values of tilesheet tile sizes
                    var (tileWidth, tileHeight) = tileSheetDialouge.GetTileDimentions();

                    //Add spitesheet to dialouge
                    swatchPanel.AddSpriteSheet(loadFileDialogue.FileName, tileWidth, tileHeight);
                }
                
            }
        }

    }
}
