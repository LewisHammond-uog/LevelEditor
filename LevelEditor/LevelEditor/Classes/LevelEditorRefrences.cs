using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LevelEditor.Classes
{

    /// <summary>
    /// Class to contain refrences of other elements of the level edtior
    /// </summary>
    static class LevelEditorRefrences
    {
        /// <summary>
        /// Get the edtior grid that a refrence object should use
        /// </summary>
        /// <param name="refrenceControl"></param>
        /// <returns></returns>
        public static EditorGrid GetEditorGrid(UserControl refrenceControl)
        {
            //Get the main window
            MainWindow parentWindow = GetParentWindow(refrenceControl);

            //Get the grid in that window
            if (parentWindow != null)
            {
                return parentWindow.LevelEditorGrid;
            }
            else
            {
                return null;
            }
        }

        public static EditorToolbar GetEditorToolbar(UserControl refrenceControl)
        {
            //Get Parent Window
            MainWindow parentWindow = GetParentWindow(refrenceControl);

            //Get the toolbar in that window
            if (parentWindow != null)
            {
                return parentWindow.ToolSelector;
            }
            else
            {
                return null;
            }

        }

        public static SwatchPanel GetSwatchPanel(UserControl refrenceControl)
        {
            //Get Parent Window
            MainWindow parentWindow = GetParentWindow(refrenceControl);

            //Get the toolbar in that window
            if (parentWindow != null)
            {
                return parentWindow.Swatch;
            }
            else
            {
                return null;
            }

        }

        public static InfomationBar GetInfomationBar(UserControl refrenceControl)
        {
            //Get Parent Window
            MainWindow parentWindow = GetParentWindow(refrenceControl);

            //Get the toolbar in that window
            if (parentWindow != null)
            {
                return parentWindow.InfomationBar;
            }
            else
            {
                return null;
            }

        }

        private static MainWindow GetParentWindow(UserControl refrenceControl)
        {
            MainWindow parentWindow = Window.GetWindow(refrenceControl) as MainWindow;
            return parentWindow;
        }

    }
}
