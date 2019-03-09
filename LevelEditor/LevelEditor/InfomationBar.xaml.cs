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
    /// Interaction logic for InfomationBar.xaml
    /// </summary>
    public partial class InfomationBar : UserControl
    {

        //Editor Grid
        EditorGrid levelGrid;

        /// <summary>
        /// Constructor, initalise the component
        /// </summary>
        public InfomationBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initalises getting the other elements of the level editor
        /// </summary>
        public void Initalise()
        {
            //Set Level Editor Grid
            levelGrid = LevelEditorRefrences.GetEditorGrid(this);

            //Subscribe Update Cordinates events to the movement of the mouse over the grid
            levelGrid.MouseMove += UpdateCoordinateUI;
        }

        /// <summary>
        /// Updates the coordinates UI with the current coordinates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateCoordinateUI(object sender, MouseEventArgs e)
        {

            //Set World Cordinates
            int worldX = (int)e.GetPosition(levelGrid).X;
            int worldY = (int)e.GetPosition(levelGrid).Y;
            WorldCoordsLabel.Content =  "World Coordinates: " + "[X] " + worldX + ", [Y] " + worldY;

            //Set Grid Cordinates if grid box size is greater than 0 (i.e a grid has been initalised)
            if (levelGrid.GridBoxSize != 0)
            {
                int gridX = worldX / levelGrid.GridBoxSize;
                int gridY = worldY / levelGrid.GridBoxSize;
                GridCoordsLabel.Content = "Grid Coordinates: " + "[X] " + gridX + ", [Y] " + gridY + " |";
            }
        }

        /// <summary>
        /// Updates the level name on the UI
        /// </summary>
        /// <param name="levelName">Name to change to level to</param>
        public void UpdateLevelNameUI(string levelName)
        {
            LevelNameLabel.Content = levelName + " |";
        }

    }
}
