using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    class DrawSetting
    {
        //色
        public float Hue;
        public float Voffset;

        public Color OpCorrectColor;
        public Color OpIncorrectColor;
        public Color OpUndeterColor;
        public Color OrangeColor;
        public Color GreenColor;
        public Color PriorColor;

        //ペン
        public Pen BlackPen;
        public Pen BlackThinPen;
        public Pen GrayPen;
        public Pen PriorBeliefPen;
        public Pen LinkPen;
        public Pen SelectedLinkPen;
        public Pen SelectedPen;
        public Pen SensorPen;
        public Pen MaliciousSensorPen;
        public Pen SensorNetworkPen;
        public Pen RedPen;
        public Pen MeterPen;
        public Pen InitMeterPen;

        //ブラシ
        public SolidBrush RedBrush;
        public SolidBrush GrayBrush;
        public SolidBrush LightGrayBrush;
        public SolidBrush BeliefBrush;
        public SolidBrush OpinionBrush;
        public SolidBrush GreenSigmaBlush;
        public SolidBrush RedSigmaBlush;

        public DrawSetting()
        {
            this.Hue = 200;
            this.Voffset = 0.3f;
            //this.OpCorrectColor = StaticColor.ConvertHSBtoARGB(this.Hue, 1f - 1f, 1f);
            //this.OpIncorrectColor = StaticColor.ConvertHSBtoARGB(this.Hue, 1f - 0f, this.Voffset);
            //this.OpUndeterColor = StaticColor.ConvertHSBtoARGB(this.Hue, 1f - 0.5f, (1f - this.Voffset) * 0.5f + this.Voffset);
            this.OpCorrectColor = Color.ForestGreen;
            this.OpIncorrectColor = Color.Firebrick;
            this.OpUndeterColor = Color.DarkGray;
            this.OrangeColor = StaticColor.ConvertHSBtoARGB(40F, 0.9F, 0.9F);
            //this.GreenColor = StaticColor.ConvertHSBtoARGB(91F, 0.62F, 0.80F);
            this.GreenColor = Color.GreenYellow;
            this.PriorColor = StaticColor.ConvertHSBtoARGB(this.Hue, 0.5f, 0f);
            this.BlackPen = new Pen(Color.Black);
            this.BlackThinPen = new Pen(Color.Black);
            this.BlackThinPen.Width = (float)0.5;
            this.GrayPen = new Pen(Color.Gray);
            this.GrayPen.Width = 2;
            this.GrayPen.StartCap = this.GrayPen.EndCap = LineCap.Round;
            this.PriorBeliefPen = new Pen(PriorColor);
            this.PriorBeliefPen.StartCap = this.PriorBeliefPen.EndCap = LineCap.Round;
            this.PriorBeliefPen.Width = 0.5f;
            this.LinkPen = new Pen(GreenColor);
            this.LinkPen.Width = 1.5f;
            this.SelectedLinkPen = new Pen(new SolidBrush(Color.Red));
            this.SelectedLinkPen.Width = 3f;
            this.SelectedPen = new Pen(Color.DarkRed);
            this.SelectedPen.Width = 3f;
            this.SensorPen = new Pen(Color.Orange);
            //this.SensorPen = new Pen(Color.MediumBlue);
            this.SensorPen.Width = 3f;
            this.MaliciousSensorPen = new Pen(Color.Magenta);
            this.MaliciousSensorPen.Width = 3f;
            this.SensorNetworkPen = new Pen(OrangeColor);
            this.SensorNetworkPen.Width = 10;
            this.RedPen = new Pen(Color.Red);
            this.RedPen.Width = 3f;
            this.MeterPen = new Pen(StaticColor.ConvertHSBtoARGB(this.Hue, 1f - 0f, this.Voffset));
            this.MeterPen.Width = 10; //2
            this.MeterPen.StartCap = this.MeterPen.EndCap = LineCap.Round;
            this.InitMeterPen = new Pen(Color.Indigo);
            this.InitMeterPen.Width = 6;
            this.InitMeterPen.StartCap = this.MeterPen.EndCap = LineCap.Round;

            this.RedBrush = new SolidBrush(Color.Red);
            this.GrayBrush = new SolidBrush(Color.Gray);
            this.LightGrayBrush = new SolidBrush(Color.LightGray);
            this.BeliefBrush = new SolidBrush(StaticColor.ConvertHSBtoARGB(100F, 0.9F, 0.8F));
            this.OpinionBrush = new SolidBrush(StaticColor.ConvertHSBtoARGB(0F, 0F, 1F));
            this.GreenSigmaBlush = new SolidBrush(Color.DarkGreen);
            this.RedSigmaBlush = new SolidBrush(Color.DarkRed);

        }
    }

    class StaticColor
    {
        static public Color ConvertHSBtoARGB(float H, float S, float V, byte alpha = 0xff)
        {

            float v = V;
            float s = S;

            float r, g, b;
            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                float h = H / 60f;
                int i = (int)Math.Floor(h);
                float f = h - i;
                float p = v * (1f - s);
                float q;
                if (i % 2 == 0)
                {
                    //t
                    q = v * (1f - (1f - f) * s);
                }
                else
                {
                    q = v * (1f - f * s);
                }

                switch (i)
                {
                    case 0:
                        r = v;
                        g = q;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = v;
                        b = q;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;
                    case 4:
                        r = q;
                        g = p;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = p;
                        b = q;
                        break;
                    default:
                        throw new ArgumentException(
                            "色相の値が不正です。", "hsv");
                }
            }

            return Color.FromArgb(
                alpha,
                (int)Math.Round(r * 255f),
                (int)Math.Round(g * 255f),
                (int)Math.Round(b * 255f));
        }
    }
}
