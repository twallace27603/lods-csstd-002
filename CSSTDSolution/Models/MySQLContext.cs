using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CSSTDEvaluation;
using CSSTDModels;
using MySql.Data.Entity;

namespace CSSTDSolution.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySQLContext : DbContext, IMySQLContext
    {
        public MySQLContext(string connectionString):base(connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
        public DbSet<VendorData> Vendors { get; set; }

        public  void CreateTable()
        {
            //throw new NotImplementedException();
        }

        public  List<VendorData> GetData()
        {
            return Vendors.ToList<VendorData>();
        }

        public  void LoadData(List<VendorData> vendors)
        {
            this.Database.ExecuteSqlCommand("DELETE FROM vendordatas;");
            this.Vendors.AddRange(vendors);
            this.SaveChanges();
        }
    }
}