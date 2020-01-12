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
    /// <summary>
    /// Log repository
    /// </summary>
    public class LogRepository : IRepository<Log>
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public LogRepository(string connectionString)
        {
            ConnectionString = connectionString;
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Log entity)
        {
            using var db = new Context(ConnectionString);
            db.Logs.Add(entity);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Log entity)
        {
            using var db = new Context(ConnectionString);
            var item = db.Logs.SingleOrDefault(x => x.Id == entity.Id);
            db.Logs.Remove(item);
            db.SaveChanges();
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public List<Log> Select()
        {
            using var db = new Context(ConnectionString);
            return db.Logs.ToList();
        }

        /// <summary>
        /// Select by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Log SelectById(Guid id)
        {
            using var db = new Context(ConnectionString);
            return db.Logs.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Log entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create in bulk
        /// </summary>
        /// <param name="entities"></param>
        public void Create(List<Log> entities)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
            tbl.Columns.Add(new DataColumn("Type", typeof(int)));
            tbl.Columns.Add(new DataColumn("Message", typeof(string)));
            tbl.Columns.Add(new DataColumn("Properties", typeof(string)));
            tbl.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));

            foreach (var item in entities)
            {
                DataRow dr = tbl.NewRow();
                dr["Id"] = item.Id;
                dr["Type"] = item.Type;
                dr["Message"] = item.Message;
                dr["Properties"] = item.Properties;
                dr["CreatedAt"] = item.CreatedAt;

                tbl.Rows.Add(dr);
            }
            SqlConnection con = new SqlConnection(ConnectionString);
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //assign Destination table name  
            objbulk.DestinationTableName = "Logs";

            objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("Type", "Type");
            objbulk.ColumnMappings.Add("Message", "Message");
            objbulk.ColumnMappings.Add("Properties", "Properties");
            objbulk.ColumnMappings.Add("CreatedAt", "CreatedAt");

            con.Open();
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(tbl);
            con.Close();
        }
    }
}
