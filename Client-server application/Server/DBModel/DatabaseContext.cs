using Server.DBModel;
using System.Data.Entity;

namespace Server
{
    public class DatabaseContext : DbContext
    {
        #region Properties

        public DbSet<UserList> UserLists { get; set; }

        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<MessageList> MessageLists { get; set; }

        public DbSet<GroupList> GroupLists { get; set; }

        #endregion //Properties

        #region Constructors

        public DatabaseContext(string connectionString) : base("DBConnection")
        {
            Database.Connection.ConnectionString = connectionString;
        }

        #endregion //Constructors
    }
}
