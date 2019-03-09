using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LevelEditor
{
    class GridGenerator : Window
    {

        public void GenerateGrid(int a_gridWidth, int a_gridHeight, int a_gridBoxSize, Grid a_grid)
        {

            // Create the Grid
            Grid myGrid = a_grid;
            myGrid.Width = a_gridWidth * a_gridBoxSize;
            myGrid.Height = a_gridHeight * a_gridBoxSize;
            myGrid.HorizontalAlignment = HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Center;
            myGrid.ShowGridLines = true;

            // Define the Columns
            for(var i = 0; i < a_gridWidth; i++)
            {
                GenerateColumn(myGrid);
            }

            // Define the Rows
            for (var i = 0; i < a_gridHeight; i++)
            {
                GenerateRow(myGrid);
            }

            // Add the Grid as the Content of the Parent Window Object
            //a_parentWindow.Content = myGrid;
            //a_parentWindow.Show();

        }


        private void GenerateColumn(Grid grid)
        {
            ColumnDefinition columnDef = new ColumnDefinition();
            grid.ColumnDefinitions.Add(columnDef);
        }

        private void GenerateRow(Grid grid)
        {
            RowDefinition rowDef = new RowDefinition();
            grid.RowDefinitions.Add(rowDef);
        }

    }
}
