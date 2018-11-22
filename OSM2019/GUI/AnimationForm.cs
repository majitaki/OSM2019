using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSM2019.GUI
{
    public partial class AnimationForm : Form
    {
        I_OSM MyOSM;

        struct AgentView
        {
            public Agent MyAgent { get; private set; }
            Layout MyLayout;
            public float X { get; private set; }
            public float Y { get; private set; }
            public float R { get; private set; }
            public float PenWidth { get; private set; }
            public int Degree { get; private set; }
            bool _IsView;

            public AgentView(Agent agent, Layout layout, int pos_scale, float r, float width, int degree, int radius_scale = 1)
            {
                this.MyAgent = agent;
                this.MyLayout = layout;
                this.R = r;
                if (this.R == 0) this.R = 1;
                this.R *= radius_scale;

                var vector = this.MyLayout.GetAgentPosition(this.MyAgent);
                this.X = vector.X * pos_scale;
                this.Y = vector.Y * pos_scale;

                this.PenWidth = width;
                this.Degree = degree;
                this._IsView = true;
            }

            public bool IsInclude(int x, int y)
            {


                var tmp = (this.X - x) * (this.X - x) + (this.Y - y) * (this.Y - y);
                float R2 = this.R * 3 / 2;
                if (tmp <= R2 * R2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool IsView()
            {
                return this._IsView;
            }

            public bool SetView(bool is_view)
            {
                this._IsView = is_view;
                return _IsView;
            }

            public (float X, float Y) GetPosition()
            {
                return (this.X, this.Y);
            }
        }
        struct LinkView
        {
            public AgentLink MyAgentLink { get; private set; }
            public (float X, float Y) SourcePos { get; private set; }
            public (float X, float Y) TargetPos { get; private set; }

            public LinkView(AgentLink agent_link, List<AgentView> agent_views)
            {
                this.MyAgentLink = agent_link;
                var s_agent = agent_link.SourceAgent;
                var t_agent = agent_link.TargetAgent;
                this.SourcePos = agent_views[s_agent.AgentID].GetPosition();
                this.TargetPos = agent_views[t_agent.AgentID].GetPosition();
            }

            public bool IsNeighbor(Agent agent)
            {
                if (this.MyAgentLink.SourceAgent == agent || this.MyAgentLink.TargetAgent == agent)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        List<AgentView> AgentViews;
        List<LinkView> LinkViews;
        float ViewScale;
        float ViewX;
        float ViewY;
        float ViewOriginX;
        float ViewOriginY;
        int AgentSizeScale;
        DrawSetting MyDrawSetting;
        Pen MyPen;
        List<AgentView> SelectedAgentViews;

        public AnimationForm()
        {
            InitializeComponent();
            this.UserInitialize();
            this.DoubleBuffered = true;
            this.MyDrawSetting = new DrawSetting();
        }

        void UserInitialize()
        {
            foreach (var layout in Enum.GetValues(typeof(LayoutEnum)))
            {
                this.comboBoxLayout.Items.Add(layout.ToString());
            }
            this.comboBoxLayout.SelectedIndex = 0;

            this.ViewScale = 1;
            this.ViewX = 0;
            this.ViewY = 0;
            this.ViewOriginX = 0;
            this.ViewOriginY = 0;
            this.AgentSizeScale = this.trackBarRadius.Value;
            this.SelectedAgentViews = new List<AgentView>();
        }

        internal void RegistOSM(I_OSM osm)
        {
            this.MyOSM = osm;
        }

        void SetAgentViews()
        {
            this.AgentViews = new List<AgentView>();

            var layout = this.MyOSM.MyAgentNetwork.MyLayout;
            var ori_min_x = layout.PosList.Select(pos => pos.X).Min();
            var ori_max_x = layout.PosList.Select(pos => pos.X).Max();
            var ori_min_y = layout.PosList.Select(pos => pos.Y).Min();
            var ori_max_y = layout.PosList.Select(pos => pos.Y).Max();

            var ori_width = Math.Abs(ori_max_x - ori_min_x);
            var ori_height = Math.Abs(ori_max_y - ori_min_y);

            var width_div = this.FixClientWidth() / ori_width;
            var height_div = this.FixClientHeight() / ori_height;
            width_div /= (float)1.1;
            height_div /= (float)1.1;

            var pos_scale = (int)Math.Min(width_div, height_div);
            //var network_height = layout.PosList.Select(vector => vector.Y).Max() - layout.PosList.Select(vector => vector.Y).Min();
            //var r = (float)(network_height / Math.Sqrt(layout.PosList.Count));

            foreach (var agent in this.MyOSM.MyAgentNetwork.Agents)
            {

                int r = (int)(this.FixClientWidth() / Math.Pow(layout.PosList.Count, 2));
                if (r == 0) r = 1;
                int w = r / 2;
                int d = agent.GetNeighbors().Count;

                this.AgentViews.Add(new AgentView(agent, layout, pos_scale, r, w, d, this.AgentSizeScale));
            }

            var ave_central_x = layout.PosList.Select(pos => pos.X).Average();
            var ave_central_y = layout.PosList.Select(pos => pos.Y).Average();

            this.ViewX = -1 * (int)(ave_central_x * pos_scale) + this.ViewOriginX;
            this.ViewY = -1 * (int)(ave_central_y * pos_scale) + this.ViewOriginY;
        }

        void SetLinkViews()
        {
            this.LinkViews = new List<LinkView>();
            var layout = this.MyOSM.MyAgentNetwork.MyLayout;

            foreach (var link in this.MyOSM.MyAgentNetwork.AgentLinks)
            {
                this.LinkViews.Add(new LinkView(link, this.AgentViews));
            }
        }

        Matrix GetBaseMatrix()
        {
            this.SetAgentViews();
            this.SetLinkViews();

            var CentralX = this.pictureBoxAnimation.ClientSize.Width / 2;
            var CentralY = this.pictureBoxAnimation.ClientSize.Height / 2;
            this.ViewX += CentralX;
            this.ViewY += CentralY;

            var base_matrix = new Matrix();
            base_matrix.Scale(this.ViewScale, this.ViewScale);
            base_matrix.Translate(this.ViewX, this.ViewY);
            return base_matrix;
        }

        int FixClientWidth()
        {
            var fix_scale = 1.1;
            return (int)(this.pictureBoxAnimation.ClientSize.Width / fix_scale);
        }

        int FixClientHeight()
        {
            var fix_scale = 1.1;
            return (int)(this.pictureBoxAnimation.ClientSize.Height / fix_scale);
        }

        void UpdateEdge(PaintEventArgs e)
        {
            this.MyPen = (Pen)this.MyDrawSetting.LinkPen.Clone();
            var base_edge_width = this.MyPen.Width / this.ViewScale;

            this.MyPen.Width = base_edge_width;
            foreach (var link_view in this.LinkViews)
            {
                var x1 = link_view.SourcePos.X;
                var y1 = link_view.SourcePos.Y;
                var x2 = link_view.TargetPos.X;
                var y2 = link_view.TargetPos.Y;

                e.Graphics.DrawLine(this.MyPen, x1, y1, x2, y2);
            }
        }

        void UpdateAgent(PaintEventArgs e, Matrix base_matrix)
        {
            foreach (var agent_view in this.AgentViews)
            {
                int agent_id = agent_view.MyAgent.AgentID;
                float r = agent_view.R;
                float r2 = r * 2;
                float r3 = r * 3;
                float r4 = r * 4;
                float r5 = r * 5;
                float r_outer = r3;
                Matrix agentMatrix = base_matrix.Clone();

                //エージェントの位置に移動
                agentMatrix.Translate(agent_view.X, agent_view.Y);
                e.Graphics.Transform = agentMatrix;

                //this.DrawNullNode(e, r_outer);
                this.DrawSquareAgent(e, agent_view);
            }
            return;
        }


        void DrawNullNode(PaintEventArgs e, float r_outer)
        {
            e.Graphics.FillEllipse(this.MyDrawSetting.GrayBrush, -r_outer / 2, -r_outer / 2, r_outer, r_outer);
        }

        void DrawSquareAgent(PaintEventArgs e, AgentView agent_view)
        {
            var r = agent_view.R;
            var length = 2 * r;
            var height = 2 * r;
            var base_x = -r;
            var base_y = -r;
            SolidBrush tmp_brush;
            if (this.SelectedAgentViews.Contains(agent_view))
            {
                tmp_brush = this.MyDrawSetting.RedBrush;
            }
            else
            {
                tmp_brush = this.MyDrawSetting.GrayBrush;
            }

            e.Graphics.FillRectangle(tmp_brush, base_x, base_y, length, height);

            var belief_dim = agent_view.MyAgent.Belief.RowCount;
            var belief_height = height / belief_dim;
            SolidBrush dim_brush = null;
            for (int i = 0; i < belief_dim; i++)
            {
                var x = base_x;
                var y = base_y + belief_height * i;
                var belief = agent_view.MyAgent.Belief[i, 0];
                var belief_length = length * (float)belief;
                var brush = new SolidBrush(StaticColor.ConvertHSBtoARGB(360 * (i / (float)belief_dim), 0.5F, 1.0F));
                if (i == agent_view.MyAgent.OpinionDim()) dim_brush = brush;
                e.Graphics.FillRectangle(brush, x, y, belief_length, belief_height);
            }

            if (agent_view.MyAgent.OpinionDim() != -1)
            {
                var pen = new Pen(dim_brush, 3);
                e.Graphics.DrawRectangle(pen, base_x, base_y, length, height);

            }
        }

        public void UpdatePictureBox()
        {
            this.pictureBoxAnimation.Invalidate();
            this.UpdateInfo();
        }

        void UpdateInfo()
        {
            if (this.SelectedAgentViews.Count == 0) return;
            Console.WriteLine("-----");
            this.MyOSM.PrintAgentInfo(this.SelectedAgentViews.Last().MyAgent);
        }
    }

}
