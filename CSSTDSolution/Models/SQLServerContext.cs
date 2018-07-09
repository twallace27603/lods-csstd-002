using CSSTDModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CSSTDSolution.Models
{
    public class SQLServerContext :  ISQLServerContext
    {
        public SQLServerContext(string connectionString) 
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }


        public void CreateTable()
        {
            //There is no need for code here because I am using Entity Framework
        }

        public List<CustomerData> GetData()
        {
            List<CustomerData> results;
            using (var context = new SqlServerDbContext(this.ConnectionString))
            {
                results = new List<CustomerData>( context.Customers.ToListAsync().Result);
            }
            return results;
        }

        public void LoadData(List<CustomerData> customers)
        {
            using (var context = new SqlServerDbContext(this.ConnectionString))
            {
                try
                {
                    context.Database.ExecuteSqlCommand("IF EXISTS(SELECT * FROM sys.tables WHERE name = 'Customers') DELETE FROM dbo.Customers;");
                }
                catch { }
                foreach(var customer in customers)
                {
                    context.Customers.Add(new SQLCustomer(customer));
                }
                context.SaveChanges();
            }

        }
    }
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(string connectionString) : base(connectionString) { }
        public DbSet<SQLCustomer> Customers { get; set; }

    }

    [MetadataType(typeof(SQLCustomerMetadata))]
    public class SQLCustomer:CustomerData
    {
        public SQLCustomer() { }

        public SQLCustomer(CustomerData source)
        {
            this.ID = source.ID;
            this.Name = source.Name;
            this.PostalCode = source.PostalCode;
        }

    }

    [Table("Customers")]
    public class SQLCustomerMetadata
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }

    }
}