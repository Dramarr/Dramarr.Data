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
    /// Show repository
    /// </summary>
    public class ShowRepository : IRepository<Show>
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public ShowRepository(string connectionString)
        {
            ConnectionString = connectionString;
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Show entity)
        {
            using var db = new Context(ConnectionString);
            db.Shows.Add(entity);
            db.SaveChanges();
        }

        /// <summary>
        /// Create in bulk
        /// </summary>
        /// <param name="entities"></param>
        public void Create(List<Show> entities)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
            tbl.Columns.Add(new DataColumn("Title", typeof(string)));
            tbl.Columns.Add(new DataColumn("Url", typeof(string)));
            tbl.Columns.Add(new DataColumn("Source", typeof(int)));
            tbl.Columns.Add(new DataColumn("Download", typeof(bool)));
            tbl.Columns.Add(new DataColumn("Enabled", typeof(bool)));
            tbl.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("UpdatedAt", typeof(DateTime)));

            foreach (var item in entities)
            {
                DataRow dr = tbl.NewRow();
                dr["Id"] = item.Id;
                dr["Title"] = item.Title;
                dr["Url"] = item.Url;
                dr["Source"] = item.Source;
                dr["Download"] = item.Download;
                dr["Enabled"] = item.Enabled;
                dr["CreatedAt"] = item.CreatedAt;
                dr["UpdatedAt"] = item.UpdatedAt;

                tbl.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(ConnectionString);
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //assign Destination table name  
            objbulk.DestinationTableName = "Shows";

            objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("Title", "Title");
            objbulk.ColumnMappings.Add("Url", "Url");
            objbulk.ColumnMappings.Add("Source", "Source");
            objbulk.ColumnMappings.Add("Download", "Download");
            objbulk.ColumnMappings.Add("Enabled", "Enabled");
            objbulk.ColumnMappings.Add("CreatedAt", "CreatedAt");
            objbulk.ColumnMappings.Add("UpdatedAt", "UpdatedAt");

            con.Open();
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(tbl);
            con.Close();
        }

        /// <summary>
        /// Dlete
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Show entity)
        {
            using var db = new Context(ConnectionString);
            var item = db.Shows.SingleOrDefault(x => x.Id == entity.Id);
            db.Shows.Remove(item);
            db.SaveChanges();
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public List<Show> Select()
        {
            using var db = new Context(ConnectionString);
            return db.Shows.ToList();
        }

        /// <summary>
        /// Select by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Show SelectById(Guid id)
        {
            using var db = new Context(ConnectionString);
            return db.Shows.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
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
