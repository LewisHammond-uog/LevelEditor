using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LevelEditor
{
    /// <summary>
    /// Class to handle generating grids
    /// </summary>
    class GridGenerator : Window
    {
        /// <summary>
        /// Generates a XAML Grid of squares
        /// </summary>
        /// <param name="gridWidth">Width of the grid</param>
        /// <param name="gridHeight">Height of the grid</param>
        /// <param name="gridBoxSize">Size of grid boxes</param>
        /// <param name="showGridLines">Show grid lines</param>
        /// <param name="grid">Grid to attach to</param>
        /// <returns>Returns the generated grid</returns>
        public Grid GenerateGrid(int gridWidth, int gridHeight, int gridBoxSize, bool showGridLines, Grid grid)
        {

            // Create the Grid
            Grid newGrid = grid;
            newGrid.Width = gridWidth * gridBoxSize;
            newGrid.Height = gridHeight * gridBoxSize;
            newGrid.HorizontalAlignment = HorizontalAlignment.Left;
            newGrid.VerticalAlignment = VerticalAlignment.Top;

            //Set grid line visibilty
            newGrid.ShowGridLines = showGridLines;

            // Define the Columns
            for(int i = 0; i < gridWidth; i++)
            {
                GenerateColumn(newGrid);
            }

            // Define the Rows
            for (int i = 0; i < gridHeight; i++)
            {
                GenerateRow(newGrid);
            }

            //Returns the Grid
            return newGrid;

        }

        /// <summary>
        /// Generates a grid column
        /// </summary>
        /// <param name="grid">Grid to attach to</param>
        private void GenerateColumn(Grid grid)
        {
            ColumnDefinition columnDef = new ColumnDefinition();
            grid.ColumnDefinitions.Add(columnDef);
        }

        /// <summary>
        /// Generates a grid row
        /// </summary>
        /// <param name="grid">Grid to attach to</param>
        private void GenerateRow(Grid grid)
        {
            RowDefinition rowDef = new RowDefinition();
            grid.RowDefinitions.Add(rowDef);
        }

    }
}
