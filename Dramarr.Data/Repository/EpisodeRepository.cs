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

        public void Create(List<Episode> entities)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
            tbl.Columns.Add(new DataColumn("ShowId", typeof(Guid)));
            tbl.Columns.Add(new DataColumn("Url", typeof(string)));
            tbl.Columns.Add(new DataColumn("Filename", typeof(string)));
            tbl.Columns.Add(new DataColumn("Status", typeof(int)));
            tbl.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("UpdatedAt", typeof(DateTime)));


            foreach (var item in entities)
            {
                DataRow dr = tbl.NewRow();
                dr["Id"] = item.Id;
                dr["ShowId"] = item.ShowId;
                dr["Url"] = item.Url;
                dr["Filename"] = item.Filename;
                dr["Status"] = item.Status;
                dr["CreatedAt"] = item.CreatedAt;
                dr["UpdatedAt"] = item.UpdatedAt;

                tbl.Rows.Add(dr);
            }
            SqlConnection con = new SqlConnection(ConnectionString);
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //assign Destination table name  
            objbulk.DestinationTableName = "Episodes";

            objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("ShowId", "ShowId");
            objbulk.ColumnMappings.Add("Url", "Url");
            objbulk.ColumnMappings.Add("Filename", "Filename");
            objbulk.ColumnMappings.Add("Status", "Status");
            objbulk.ColumnMappings.Add("CreatedAt", "CreatedAt");
            objbulk.ColumnMappings.Add("UpdatedAt", "UpdatedAt");

            con.Open();
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(tbl);
            con.Close();
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
