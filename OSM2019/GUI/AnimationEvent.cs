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

            //float px;
            //float py;
            //var layout = this.MyOSM.MyAgentNetwork.MyLayout;

            //var network_size = this.MyOSM.MyAgentNetwork.Agents.Count;
            ////var r = (float)(this.FixClientWidth() / Math.Pow(layout.PosList.Count, 3));
            //var network_height = layout.PosList.Select(vector => vector.Y).Max() - layout.PosList.Select(vector => vector.Y).Min();
            //var r = (float)(network_height / Math.Sqrt(layout.PosList.Count));
            ////if (r == 0) r = 1;
            //var scale = this.FixClientWidth() / (layout.PosList.Select(vector => vector.X).Max() - layout.PosList.Select(vector => vector.X).Min());
            //scale /= (float)1.4;

            //var CentralX = this.pictureBoxAnimation.ClientSize.Width / 2;
            //var CentralY = this.pictureBoxAnimation.ClientSize.Height / 2;

            //Matrix matrix = new Matrix();
            //matrix.Translate(CentralX, CentralY);
            //matrix.Scale(scale, scale);
            //matrix.Rotate(30F);

            //foreach (var agent in this.MyOSM.MyAgentNetwork.Agents)
            //{
            //    var pos_vector = layout.GetAgentPosition(agent);
            //    px = pos_vector.X;
            //    py = pos_vector.Y;

            //    SolidBrush grayBrush = new SolidBrush(Color.DimGray);




            //    e.Graphics.Transform = matrix;
            //    e.Graphics.FillEllipse(grayBrush, px, py, r, r);
            //}
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
