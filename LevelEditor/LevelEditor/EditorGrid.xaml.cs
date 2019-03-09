using LevelEditor.Classes;
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
    /// Interaction logic for EditorGrid.xaml
    /// </summary>
    public partial class EditorGrid : UserControl
    {

        #region Variable Declerations

        //Variables for grid size and grid itself
        private Grid editorGrid;
        public int GridHeight { private set; get; } = 0;
        public int GridWidth { private set; get; } = 0;
        public int GridBoxSize { private set; get; } = 0;

        //Tool Selection Bar
        EditorToolbar editorToolbar;
        SwatchPanel swatchPanel;

        //Mouse infomation
        private bool mouseIsDown = false;

        //Array of Images
        private Image[,] gridImages;

        #endregion

        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public EditorGrid()
        {
            InitializeComponent();  
        }

        /// <summary>
        /// Initalises the element by getting other elements that we need to refrence
        /// </summary>
        public void Initalise()
        {
            //Set our editor tool bar and swatch panel
            editorToolbar = LevelEditorRefrences.GetEditorToolbar(this);
            swatchPanel = LevelEditorRefrences.GetSwatchPanel(this);
        }

        /// <summary>
        /// Create a grid of a set width, height and box size 
        /// </summary>
        /// <param name="createGridWidth">Number of grid boxes to create in the y dimention</param>
        /// <param name="createGridHeight">Number of grid boxes to create in the x dimention</param>
        /// <param name="createGridBoxSize">Size of boxes to create the grid of</param>
        /// <returns>Returns the created grid</returns>
        public Grid CreateGrid(int createGridWidth, int createGridHeight, int createGridBoxSize)
        {

            //Clear the previous grid
            ClearGrid(editorGrid);

            //Create an instance of grid generator and get it to generate a grid
            GridGenerator gridGenerator = new GridGenerator();
            editorGrid = gridGenerator.GenerateGrid(createGridWidth, createGridHeight, createGridBoxSize, true, TileGrid);

            //Set Grid Size vars
            GridWidth = createGridWidth;
            GridHeight = createGridHeight;
            GridBoxSize = createGridBoxSize;

            //Initalise the grid
            IntitaliseGrid(GridWidth, GridHeight);

            //Return the grid
            return editorGrid;
        }

        /// <summary>
        /// Completly Clears the given grid
        /// </summary>
        private void ClearGrid(Grid grid)
        {
            //Null check the grid then remove all children, row and collumn definitions
            if (grid != null)
            {
                grid.Children.Clear();
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();
            }
        }

        /// <summary>
        /// Initalises Grids with empty images
        /// </summary>
        /// <param name="gridWidth">Grid Width</param>
        /// <param name="gridHeight">Grid Height</param>
        /// <param name="grid">Grid to Initalise</param>
        private void IntitaliseGrid(int gridWidth, int gridHeight)
        {
            gridImages = new Image[gridWidth, gridHeight];

            //Go through all of the grid items and fill them with empty images
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    //Sets an empty image in to a grid spot
                    Image image = new Image();
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);

                    editorGrid.Children.Add(image);

                    gridImages[x, y] = image;
                }
            }

            //Subscribe Grid Functions to our Functions 
            editorGrid.MouseDown += OnMouseDown;
            editorGrid.MouseUp += OnMouseUp;
            editorGrid.MouseMove += OnMouseMove;

        }

        /// <summary>
        /// Handles when the mouse is pressed on the grid and calls the function to use the current tool on the mouse postion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            //Get Mouse Postion in grid squares
            var (x, y) = GetGridMousePostion(e);

            UseTool(x, y);

            //Set mouse is down
            mouseIsDown = true;

        }

        /// <summary>
        /// Handles releasing the mouse button, sets that the mouse button is not held down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseIsDown = false;
        }

        /// <summary>
        /// Handles the mouse moving over the grid, updating coordinates and using the tool if the mouse is down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {

            //If the mouse is down then trigger use tool
            if (mouseIsDown)
            {
                var (x, y) = GetGridMousePostion(e);

                UseTool(x, y);
            }
        }

        /// <summary>
        /// Uses the currently selected tool at a given postion
        /// </summary>
        /// <param name="gridX">X postion to use tool (in grid squares)</param>
        /// <param name="gridY">Y postion to use tool (in grid squares)</param>
        private void UseTool(int gridX, int gridY)
        {
            //Check that we are trying to use a tool within the confines of the grid
            if (gridX < GridHeight && gridY < GridWidth)
            {
                //Use the tool that we have selected
                switch (editorToolbar.selectedTool)
                {
                    case EditorToolbar.Tool.Pen:
                        gridImages[gridX, gridY].Source = swatchPanel.SelectedTile;
                        break;
                    case EditorToolbar.Tool.Erase:
                        gridImages[gridX, gridY].Source = null;
                        break;
                }
            }
        }

        /// <summary>
        /// Splits a large image in to multiple grid cells
        /// </summary>
        /// <param name="image">Image to split in to cells</param>
        public void SplitImageInToGrid(BitmapImage image)
        {
            //Split up our loaded image and add each slice to the editor grid
            for (int x = 0; x < (GridWidth*GridBoxSize); x += GridBoxSize)
            {
                for (int y = 0; y < (GridHeight * GridBoxSize); y += GridBoxSize)
                {
                    //Split the image in to section based on grid box size and where we are in the image
                    var croppedBitmap = new CroppedBitmap(image, new Int32Rect(x, y, GridBoxSize, GridBoxSize));

                    //Word out the postion in the grid that we should instert in to
                    int gridX = x / GridBoxSize;
                    int gridY = y / GridBoxSize;

                    gridImages[gridX, gridY].Source = croppedBitmap;
                }
            }
        }

        /// <summary>
        /// Gets what grid square the mouse is currently in
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Current Grid Square the mouse is in (as a tupple)</returns>
        private (int x, int y) GetGridMousePostion(MouseEventArgs e)
        {
            int x = (int)e.GetPosition(editorGrid).X / GridBoxSize;
            int y = (int)e.GetPosition(editorGrid).Y / GridBoxSize;

            return (x, y);
        }

    }
}
