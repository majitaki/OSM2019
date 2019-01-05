using Konsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    class ExtendProgressBar : IProgressBar
    {
        public ProgressBar MyProgressBar;
        public int CurrentCount;
        public string Tag;
        public int CountMax;

        public ExtendProgressBar(int max)
        {
            this.MyProgressBar = new ProgressBar(PbStyle.DoubleLine, max);
            this.CurrentCount = 0;
            this.CountMax = max;
        }

        public string Line1 => throw new NotImplementedException();

        public string Line2 => throw new NotImplementedException();

        public int Y => throw new NotImplementedException();

        public int Max { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Next()
        {
            this.CurrentCount++;
            this.Refresh(this.CurrentCount);
        }

        public void Next(string item)
        {
            this.MyProgressBar.Next(item);
            this.CurrentCount++;
        }

        public void Refresh(string tag)
        {
            this.Tag = tag;
            this.MyProgressBar.Refresh(this.CurrentCount, tag);
        }

        public void RefreshWithoutChange(string tag)
        {
            this.MyProgressBar.Refresh(this.CurrentCount, tag);
        }

        public void Refresh(int current)
        {
            this.Refresh(current, this.Tag);
        }

        public void Refresh(int current, string item)
        {
            this.Tag = item;
            this.CurrentCount = current;
            this.MyProgressBar.Refresh(current, this.Tag);
        }

        public void Refresh(int current, string format, params object[] args)
        {
            this.CurrentCount = current;
            this.Tag = format;
            this.MyProgressBar.Refresh(current, format, args);
        }
    }
}
