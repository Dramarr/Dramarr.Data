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
    /// Metadata repository
    /// </summary>
    public class MetadataRepository : IRepository<Metadata>
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public MetadataRepository(string connectionString)
        {
            ConnectionString = connectionString;
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Metadata entity)
        {
            using var db = new Context(ConnectionString);
            db.Metadatas.Add(entity);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Metadata entity)
        {
            using var db = new Context(ConnectionString);
            var item = db.Metadatas.SingleOrDefault(x => x.Id == entity.Id);
            db.Metadatas.Remove(item);
            db.SaveChanges();
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public List<Metadata> Select()
        {
            using var db = new Context(ConnectionString);
            return db.Metadatas.ToList();
        }

        /// <summary>
        /// Select by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Metadata SelectById(Guid id)
        {
            using var db = new Context(ConnectionString);
            return db.Metadatas.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Metadata entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create in bulk
        /// </summary>
        /// <param name="entities"></param>
        public void Create(List<Metadata> entities)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
            tbl.Columns.Add(new DataColumn("ShowId", typeof(Guid)));
            tbl.Columns.Add(new DataColumn("ImageUrl", typeof(int)));
            tbl.Columns.Add(new DataColumn("Plot", typeof(string)));
            tbl.Columns.Add(new DataColumn("Language", typeof(string)));
            tbl.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("UpdatedAt", typeof(DateTime)));

            foreach (var item in entities)
            {
                DataRow dr = tbl.NewRow();
                dr["Id"] = item.Id;
                dr["ShowId"] = item.ShowId;
                dr["ImageUrl"] = item.ImageUrl;
                dr["Plot"] = item.Plot;
                dr["Language"] = item.Language;
                dr["CreatedAt"] = item.CreatedAt;
                dr["UpdatedAt"] = item.UpdatedAt;

                tbl.Rows.Add(dr);
            }
            SqlConnection con = new SqlConnection(ConnectionString);
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //assign Destination table name  
            objbulk.DestinationTableName = "Metadatas";

            objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("ShowId", "ShowId");
            objbulk.ColumnMappings.Add("ImageUrl", "ImageUrl");
            objbulk.ColumnMappings.Add("Plot", "Plot");
            objbulk.ColumnMappings.Add("Language", "Language");
            objbulk.ColumnMappings.Add("CreatedAt", "CreatedAt");
            objbulk.ColumnMappings.Add("UpdatedAt", "UpdatedAt");

            con.Open();
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(tbl);
            con.Close();
        }
    }
}
