using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            bool _IsView;

            public AgentView(Agent agent, Layout layout, float r, float width, int radius_scale = 1)
            {
                this.MyAgent = agent;
                this.MyLayout = layout;
                this.R = r;
                if (this.R == 0) this.R = 1;
                this.R *= radius_scale;

                var vector = this.MyLayout.GetAgentPosition(this.MyAgent);
                this.X = vector.X;
                this.Y = vector.Y;

                this.PenWidth = width;
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
        }
        struct LinkView
        {
            public AgentLink MyAgentLink { get; private set; }
            Layout MyLayout;
            (Vector2 source_pos, Vector2 target_pos) LinkPosition;

            public LinkView(AgentLink agent_link, Layout layout)
            {
                this.MyAgentLink = agent_link;
                this.MyLayout = layout;

                var s_pos = this.MyLayout.GetAgentPosition(this.MyAgentLink.SourceAgent);
                var t_pos = this.MyLayout.GetAgentPosition(this.MyAgentLink.TargetAgent);
                this.LinkPosition.source_pos = s_pos;
                this.LinkPosition.target_pos = t_pos;

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

        float ViewScale;
        float ViewX;
        float ViewY;
        float ViewOriginX;
        float ViewOriginY;

        public AnimationForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        void UserInitialize()
        {
            foreach (var layout in Enum.GetValues(typeof(LayoutEnum)))
            {
                this.comboBoxLayout.Items.Add(layout.ToString());
            }
            this.comboBoxLayout.SelectedIndex = 0;
            
        }

        internal void RegistOSM(I_OSM osm)
        {
            this.MyOSM = osm;
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

        private void pictureBoxAnimation_Click(object sender, EventArgs e)
        {

        }
    }



}
