using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevelEditor.Classes
{
    /// <summary>
    /// Class to handle saving and loading level data and metadata
    /// </summary>
    class SaveLoadHandler
    {

        /// <summary>
        /// Creates a new folder for this level and saves editorGrid image and metadata inside it
        /// </summary>
        /// <param name="editorGrid">editorGrid to save</param>
        /// <param name="levelName">Level name to save as</param>
        /// <param name="filePath">File Path to create folder and save at</param>
        public void SaveLevel(EditorGrid editorGrid, string levelName = "untitled_level", string filePath = "")
        {


            //Format file path and save editorGrid image (e.g C:LevelSaves\levelName\levelName.png)
            string gridSaveFilePath = System.IO.Path.Combine(filePath, levelName + ".png");
            SaveGridImage(editorGrid, gridSaveFilePath);

            //Formate file path and save metadata (e.g C:LevelSaves\levelName\levelName.json)
            string metadataLoadFilePath = System.IO.Path.Combine(filePath, levelName + ".json");
            SaveGridMetadata(editorGrid, metadataLoadFilePath, levelName);
        }

        /// <summary>
        /// Saves a editorGrid as an image to a specficified output location
        /// </summary>
        /// <param name="editorGrid">editorGrid to save</param>
        /// <param name="filePath">File path to save image to</param>
        private void SaveGridImage(EditorGrid editorGrid, string filePath)
        {

            //Get the width of the editorGrid
            int dpi = 96; //One of the standard dpi (72/96) We're not using DPI. It would be for printing/viewing (dots per inch)
            int width = editorGrid.GridWidth * editorGrid.GridBoxSize;
            int height = editorGrid.GridHeight * editorGrid.GridBoxSize;

            //Renders to a target bitmap
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, dpi, dpi, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(editorGrid.TileGrid);

            //Encodes to a PNG
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            //Writes PNG encoded image to file

            using (System.IO.Stream fileStream = System.IO.File.Create(filePath))
            {
                pngImage.Save(fileStream);
                fileStream.Close();
            }

        }

        /// <summary>
        /// Saves metadata relating to the level saved
        /// </summary>
        /// <param name="editorGrid">editorGrid to save metadata about</param>
        /// <param name="filePath">File path to save metadata to</param>
        /// /// <param name="levelName">Level Name to save</param>
        private void SaveGridMetadata(EditorGrid editorGrid, string filePath, string levelName)
        {
            //Create a metadata object, for data storage
            LevelMetadata levelMetadata = new LevelMetadata
            {
                levelName = levelName,
                levelSaved = DateTime.Now,
                levelGridWidth = editorGrid.GridHeight,
                levelGridHeight = editorGrid.GridHeight,
                levelGridBoxSize = editorGrid.GridBoxSize
            };

            //Serialize as JSON
            string jsonMetadata = JsonConvert.SerializeObject(levelMetadata, Formatting.Indented);

            //Save File
            File.WriteAllText(filePath, jsonMetadata);

        }

        /// <summary>
        /// Load Level Image and Metadata
        /// </summary>
        /// <param name="editorGrid"></param>
        /// <param name="levelName"></param>
        /// <param name="filePath"></param>
        public void LoadLevel(EditorGrid editorGrid, string levelName, string filePath)
        {

            //File Paths for both the image and metadata
            string gridLoadFilePath = System.IO.Path.Combine(filePath, levelName + ".png");
            string metadataLoadFilePath = System.IO.Path.Combine(filePath, levelName + ".json");

            //Load image and metadata
            BitmapImage loadedImage;
            LevelMetadata loadedMetadata;

            //Try to load images and metadata
            try
            { 
                loadedImage = LoadGridImage(gridLoadFilePath);
                loadedMetadata = LoadGridMetadata(metadataLoadFilePath);
            }
            catch (FileNotFoundException)
            {
                //If the we failed to load the files, show an error window and exit of loading function
                new FileImportErrorDialouge().ShowDialog();
                return;
            }

            //Apply loaded level to the level editorGrid and set level title in the info bar
            InsertImageToGrid(editorGrid, loadedImage, loadedMetadata);
        }

        /// <summary>
        /// Loads a bitmap image given a file path
        /// </summary>
        /// <param name="filePath">File Path of the Image</param>
        /// <returns>Loaded Image</returns>
        private BitmapImage LoadGridImage(string filePath)
        {

            //Check file path, if it exists then load the image
            if (File.Exists(filePath))
            {
                BitmapImage imageSource = GetBitmapFromUri(new Uri(filePath));
                return imageSource;
                
            }
            else
            {
                throw new FileNotFoundException();
            }

        }

        /// <summary>
        /// Loads the metadata of a level
        /// </summary>
        /// <param name="filePath">File Path of the metadata</param>
        /// <returns>Loaded Metadata</returns>
        private LevelMetadata LoadGridMetadata(string filePath)
        {
            //Check that metadata file path exists
            if (File.Exists(filePath))
            {

                //Deserialise JSON file to metadata object
                using(StreamReader jsonFile = File.OpenText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    LevelMetadata loadedMetadata = (LevelMetadata)serializer.Deserialize(jsonFile, typeof(LevelMetadata));

                    jsonFile.Close();

                    //Return the got metadata object
                    return loadedMetadata;
                }

            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Inserts an image in to a specifed grid
        /// </summary>
        /// <param name="editorGrid">Grid to Insert into</param>
        /// <param name="image">Image to insert in to grid</param>
        /// <param name="metadata"> Metadata of Image to insert in to grid</param>
        private void InsertImageToGrid(EditorGrid editorGrid, BitmapImage image, LevelMetadata metadata)
        {
            //Create a new editorGrid with values from our metadata
            int newGridWidth = metadata.levelGridWidth;
            int newGridHeight = metadata.levelGridHeight;
            int newGridBoxSize = metadata.levelGridBoxSize;
            Grid newGrid = editorGrid.CreateGrid(newGridWidth,newGridHeight,newGridBoxSize);

            //Get grid to insert our image in to tiles
            editorGrid.SplitImageInToGrid(image);
        }

        // JUSTIFICATION: This method has been created over the normal BitmapImage(new Uri(filePath)) because that
        // does not release the file for overwriting and causes a System.IO.IOException that the file is in use
        /// <summary>
        /// Gets a bit map image from a filepath, releasing that image for overwrite in the proccess
        /// </summary>
        /// <param name="filePath">Path of the file to load</param>
        /// <returns></returns>
        private static BitmapImage GetBitmapFromUri(Uri filePath)
        {
            //Load an image from a file, and then close the file so that
            //it can be overwritten.
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = filePath;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            return bitmapImage;
        }

    }
}
