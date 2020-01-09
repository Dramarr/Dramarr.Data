using Dramarr.Core.Interfaces.Data;
using Dramarr.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dramarr.Data.Repository
{
    public class EpisodeRepository : IRepository<Episode>
    {
        public string ConnectionString { get; set; }

        public EpisodeRepository(string connectionString)
        {
            ConnectionString = connectionString;
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }

        public void Create(Episode entity)
        {
            using var db = new Context(ConnectionString);
            db.Episodes.Add(entity);
            db.SaveChanges();
        }

        public void Delete(Episode entity)
        {
            using var db = new Context(ConnectionString);
            db.Episodes.Remove(db.Episodes.SingleOrDefault(x => x.Id == entity.Id));
            db.SaveChanges();
        }

        public List<Episode> Select()
        {
            using var db = new Context(ConnectionString);
            return db.Episodes.AsNoTracking().ToList();
        }

        public Episode SelectById(Guid id)
        {
            using var db = new Context(ConnectionString);
            return db.Episodes.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public void Update(Episode entity)
        {
            using var db = new Context(ConnectionString);
            var ep = db.Episodes.Single(x => x.Url == entity.Url);

            ep.Filename = entity.Filename;
            ep.Url = entity.Url;
            ep.Status = entity.Status;
            ep.UpdatedAt = DateTime.UtcNow;

            db.SaveChanges();
        }
    }
}
