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

    private void pictureboxAnimation_MouseWheel(object sender, MouseEventArgs e)
    {
      float prev_scale = this.ViewScale;
      float allow_zoom_size = (float)0.5;

      if (this.ViewScale >= allow_zoom_size)
      {
        this.ViewScale += e.Delta * 0.001f * this.ViewScale;
        if (this.ViewScale < allow_zoom_size)
        {
          this.ViewScale = allow_zoom_size;
        }
      }

      this.ViewOriginX -= (e.X / this.ViewScale) * (this.ViewScale - prev_scale) / this.ViewScale;
      this.ViewOriginY -= (e.Y / this.ViewScale) * (this.ViewScale - prev_scale) / this.ViewScale;


      if (prev_scale != this.ViewScale)
      {
        this.Invoke(new Action(this.UpdatePictureBox));
      }
    }


    private void PictureBoxAnimation_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.NullCheck()) return;

      this.DraggingOldX = e.X;
      this.DraggingOldY = e.Y;

      switch (e.Button)
      {
        case MouseButtons.Middle:
          this.isDragging = true;
          this.Cursor = Cursors.SizeAll;
          break;
      }
    }

    private void pictureBoxAnimation_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.NullCheck()) return;

      if (this.isDragging == true)
      {
        this.ViewOriginX += (e.X - this.DraggingOldX) / this.ViewScale;
        this.ViewOriginY += (e.Y - this.DraggingOldY) / this.ViewScale;

        this.DraggingOldX = e.X;
        this.DraggingOldY = e.Y;
      }
      else
      {
        return;
      }
      this.Invoke(new Action(this.UpdatePictureBox));

    }
    private void PictureBoxAnimation_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.NullCheck()) return;

      switch (e.Button)
      {
        case MouseButtons.Middle:
          this.isDragging = false;
          this.Cursor = Cursors.Default;
          this.Invoke(new Action(this.UpdatePictureBox));
          break;
      }
    }

    private void trackBarRadius_Scroll(object sender, EventArgs e)
    {
      this.AgentSizeScale = this.trackBarRadius.Value;
      this.Invoke(new Action(this.UpdatePictureBox));
    }

    private void trackBarRotate_Scroll(object sender, EventArgs e)
    {
      this.AgentRotateScale = this.trackBarRotate.Value;
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
              this.NeighborAgentIDs.AddRange(agent_view.MyAgent.GetNeighbors().Select(agent => agent.AgentID));
            }
          }
        }
      }

      //clear
      if ((e as MouseEventArgs).Button == MouseButtons.Right)
      {
        this.SelectedAgentViews.Clear();
        this.NeighborAgentIDs.Clear();
      }

      this.SelectedAgentViews = this.SelectedAgentViews.Distinct().ToList();
      this.Invoke(new Action(this.UpdatePictureBox));
    }

  }
}
