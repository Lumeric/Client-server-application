using Server.DBModel;
using System.Data.Entity;

namespace Server
{
    public class DatabaseContext : DbContext
    {
        #region Properties

        public DbSet<Users> Users { get; set; }

        public DbSet<EventLog> EventLog { get; set; }

        public DbSet<MessageList> Messages { get; set; }

        public DbSet<GroupList> GroupList { get; set; }

        #endregion //Properties

        #region Constructors

        public DatabaseContext(string connectionString) : base("DBConnection")
        {
            Database.Connection.ConnectionString = connectionString;
        }

        #endregion //Constructors
    }
}
