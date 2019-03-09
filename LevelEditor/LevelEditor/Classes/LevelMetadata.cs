using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LevelEditor.Classes
{
    /// <summary>
    /// Data class for generating metadata for a level
    /// </summary>
    struct LevelMetadata
    {

        //Variables for metadata 
        public string levelName;
        public DateTime levelSaved;
        public int levelGridWidth;
        public int levelGridHeight;
        public int levelGridBoxSize;

    }
}
