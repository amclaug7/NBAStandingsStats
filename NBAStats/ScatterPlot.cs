using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBAStats
{
    public partial class ScatterPlot : Form
    {
        public ScatterPlot()
        {
            InitializeComponent();
        }

        private void ScatterPlot_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < 83; x++)
            {
                //chart1.Series[0].Points.AddXY([0,2], x);
            }
        }
    }
}
