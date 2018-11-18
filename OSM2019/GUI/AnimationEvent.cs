using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSM2019.GUI
{
    public partial class AnimationForm : Form
    {

        private void buttonUpdateLayout_Click(object sender, EventArgs e)
        {
            this.pictureBoxAnimation.Invalidate();
        }


        private void pictureBoxAnimation_Paint(object sender, PaintEventArgs e)
        {
            float px;
            float py;
            var layout = this.MyOSM.MyAgentNetwork.MyLayout;

            var network_size = this.MyOSM.MyAgentNetwork.Agents.Count;
            var r = (float)(this.FixClientWidth() / Math.Pow(layout.PosList.Count, 3));
            if (r == 0) r = 2;


            var CentralX = this.pictureBoxAnimation.ClientSize.Width / 2;
            var CentralY = this.pictureBoxAnimation.ClientSize.Height / 2;

            Matrix matrix = new Matrix();
            matrix.Translate(CentralX, CentralY);
            matrix.Scale(100, 100);
            matrix.Rotate(30F);

            foreach (var agent in this.MyOSM.MyAgentNetwork.Agents)
            {
                var pos_vector = layout.GetAgentPosition(agent);
                px = pos_vector.X;
                py = pos_vector.Y;

                SolidBrush pinkBrush = new SolidBrush(Color.DimGray);
                e.Graphics.Transform = matrix;
                e.Graphics.FillEllipse(pinkBrush, px, py, r, r);
            }
        }

    }
}
