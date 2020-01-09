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
    public class ShowRepository : IRepository<Show>
    {
        public string ConnectionString { get; set; }

        public ShowRepository(string connectionString)
        {
            ConnectionString = connectionString;
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }

        public void Create(Show entity)
        {
            using var db = new Context(ConnectionString);
            db.Shows.Add(entity);
            db.SaveChanges();
        }

        public void Delete(Show entity)
        {
            using var db = new Context(ConnectionString);
            var item = db.Shows.SingleOrDefault(x => x.Id == entity.Id);
            db.Shows.Remove(item);
            db.SaveChanges();
        }

        public List<Show> Select()
        {
            using var db = new Context(ConnectionString);
            return db.Shows.ToList();
        }

        public Show SelectById(Guid id)
        {
            using var db = new Context(ConnectionString);
            return db.Shows.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Show entity)
        {
            using var db = new Context(this.ConnectionString);
            var sh = db.Shows.Single(x => x.Id == entity.Id);

            sh.UpdatedAt = DateTime.UtcNow;
            sh.Title = entity.Title;
            sh.Url = entity.Url;
            sh.Source = entity.Source;
            sh.Download = entity.Download;

            db.SaveChanges();
        }
    }
}
