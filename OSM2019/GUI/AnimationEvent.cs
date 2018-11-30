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
            var base_matrix = this.GetBaseMatrix();
            e.Graphics.Transform = base_matrix;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //edge
            this.UpdateEdge(e);
            e.Graphics.Transform = base_matrix;

            //agent
            this.UpdateAgent(e, base_matrix);
            e.Graphics.Transform = base_matrix;

        }


        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            this.AgentSizeScale = this.trackBarRadius.Value;
            this.Invoke(new Action(this.UpdatePictureBox));
        }

        private void pictureBoxAnimation_MouseClick(object sender, MouseEventArgs e)
        {
            Matrix baseMatrix = this.GetBaseMatrix();
            //縮尺を変更
            Matrix agentMatrix = new Matrix();
            float modifyScale = 1 / this.ViewScale;
            agentMatrix.Scale(modifyScale, modifyScale);
            //移動の変更
            float modifyX = -1 * this.ViewX * (1 / modifyScale);
            float modifyY = -1 * this.ViewY * (1 / modifyScale);

            agentMatrix.Translate(modifyX, modifyY);

            Point p = this.pictureBoxAnimation.PointToClient(Control.MousePosition);
            Point[] pArray = { p };
            agentMatrix.TransformPoints(pArray);

            //agent select
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                foreach (var agent_view in this.AgentViews)
                {
                    if (agent_view.IsInclude(pArray[0].X, pArray[0].Y))
                    {
                        if (this.SelectedAgentViews.Contains(agent_view))
                        {
                            this.SelectedAgentViews.Remove(agent_view);
                        }
                        else
                        {
                            this.SelectedAgentViews.Add(agent_view);
                        }
                    }
                }
            }

            //clear
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                this.SelectedAgentViews.Clear();
            }

            this.SelectedAgentViews = this.SelectedAgentViews.Distinct().ToList();
            this.Invoke(new Action(this.UpdatePictureBox));
        }

    }
}
