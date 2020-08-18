using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Spatial.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial.NHibernateSpatial
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration config = new Configuration().Configure("database.cfg.xml");
            ISessionFactory sessionFactory = config.BuildSessionFactory();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(CreatePolygonFeatureSample());
                    session.Save(CreatePointFeatureSample());
                    transaction.Commit();
                }
            }
        }

        static IList FilterPolygonFeature(ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return session.CreateCriteria(typeof(PolygonFeature))
                     .Add(SpatialExpression.Filter("Geometry", new Envelope(0, 0, 10, 10), -1))
                    .List();
            }
        }

        static PointFeature CreatePointFeatureSample()
        {
            PointFeature pointFeature = new PointFeature
            {
                Name = "Point Sample",
                Geometry = CreatePointSample()
            };
            return pointFeature;
        }

        static PolygonFeature CreatePolygonFeatureSample()
        {
            PolygonFeature polygonFeature = new PolygonFeature
            {
                Name = "Sample Polygon",
                Geometry = CreatePolygonSample()
            };
            return polygonFeature;
        }

        static IPolygon CreatePolygonSample()
        {
            Coordinate[] coordinates = new Coordinate[5];
            coordinates[0] = new Coordinate(0, 0);
            coordinates[1] = new Coordinate(0, 10);
            coordinates[2] = new Coordinate(10, 10);
            coordinates[3] = new Coordinate(10, 0);
            coordinates[4] = coordinates[0];
            return Geometry.DefaultFactory.CreatePolygon(coordinates);
        }

        static IPoint CreatePointSample()
        {
            return Geometry.DefaultFactory.CreatePoint(new Coordinate(5, 5));
        }
    }
}
