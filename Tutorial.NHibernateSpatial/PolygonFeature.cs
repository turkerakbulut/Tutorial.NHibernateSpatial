using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial.NHibernateSpatial
{
    public class PolygonFeature
    {
        public virtual long ID { get; set; }
        public virtual string Name { get; set; }
        public virtual IPolygon Geometry { get; set; }
    }
}
