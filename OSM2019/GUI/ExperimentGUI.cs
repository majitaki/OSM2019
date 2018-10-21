using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSM2019.Utility;

namespace OSM2019.GUI
{
    public partial class ExperimentGUI : UserControl
    {
        internal GUIEnum MyGUIEnum;

        public ExperimentGUI()
        {
            this.MyGUIEnum = GUIEnum.ExperimentGUI;
            InitializeComponent();
        }
    }
}
