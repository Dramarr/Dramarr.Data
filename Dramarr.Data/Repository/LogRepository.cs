using Dramarr.Core.Interfaces.Data;
using Dramarr.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dramarr.Data.Repository
{
    public class LogRepository : IRepository<Log>
    {
        public string ConnectionString { get; set; }

        public LogRepository(string connectionString)
        {
            ConnectionString = connectionString;
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }

        public void Create(Log entity)
        {
            using var db = new Context(ConnectionString);
            db.Logs.Add(entity);
            db.SaveChanges();
        }

        public void Delete(Log entity)
        {
            using var db = new Context(ConnectionString);
            var item = db.Logs.SingleOrDefault(x => x.Id == entity.Id);
            db.Logs.Remove(item);
            db.SaveChanges();
        }

        public List<Log> Select()
        {
            using var db = new Context(ConnectionString);
            return db.Logs.ToList();
        }

        public Log SelectById(Guid id)
        {
            using var db = new Context(ConnectionString);
            return db.Logs.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Log entity)
        {
            throw new NotImplementedException();
        }
    }
}
