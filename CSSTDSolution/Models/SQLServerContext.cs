using CSSTDEvaluation;
using CSSTDModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CSSTDSolution.Models
{
    public class SQLServerContext : DbContext, ISQLServerContext
    {
        public SQLServerContext(string connectionString) : base(connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public DbSet<CustomerData> Customers { get; set; }

        public void CreateTable()
        {
            //There is no need for code here because I am using Entity Framework
        }

        public List<CustomerData> GetData()
        {
            return Customers.ToListAsync().Result;
        }

        public void LoadData(List<CustomerData> customers)
        {
            this.Database.ExecuteSqlCommand("DELETE FROM dbo.CustomerDatas;");
            this.Customers.AddRange(customers);
            this.SaveChanges();
        }
    }
}