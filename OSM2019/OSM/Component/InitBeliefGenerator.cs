using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class InitBeliefGenerator
    {
        InitBeliefMode Mode;

        public InitBeliefGenerator SetInitBeliefMode(InitBeliefMode mode)
        {
            this.Mode = mode;
            return this;
        }

        public void Generate()
        {

        }
    }
}
