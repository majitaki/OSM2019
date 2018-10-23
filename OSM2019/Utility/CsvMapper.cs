using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace OSM2019.Utility
{
    class LayoutCsvMapper: ClassMap<Vector2>
    {
        public LayoutCsvMapper()
        {
            Map(_ => _.X).Name("x");
            Map(_ => _.Y).Name("y");
        }
    }
}
