using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Sebalance
{
    public class NHibernateSessionFactory
    {
        private ISessionFactory sessionFactory;

        private readonly string ConnectionString = "";
        private readonly List<Type> types;

        public NHibernateSessionFactory(String connectionString, List<Type> types)
        {
            this.ConnectionString = connectionString;
            this.types = types;
        }

        public ISessionFactory SessionFactory
        {
            get { return sessionFactory ?? (sessionFactory = CreateSessionFactory()); }
        }

        private ISessionFactory CreateSessionFactory()
        {

            Configuration cfg;
            cfg = new Configuration().Configure();

            foreach(var t in types) {
                cfg.AddClass(t);
            }

            cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, this.ConnectionString);

#if DEBUG
            cfg.SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, "true");
            cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
           
#endif
            new SchemaExport(cfg).Execute(true, true, false);
            return (cfg.BuildSessionFactory());
        }
    }
}
