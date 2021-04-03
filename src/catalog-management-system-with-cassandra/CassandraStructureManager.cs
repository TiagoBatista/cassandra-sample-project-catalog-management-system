using Cassandra;
using catalog_management_system_with_cassandra.Connectivity;
using System;

namespace catalog_management_system_with_cassandra
{
    public static class CassandraStructureManager
    {
        public static ISession Session; //Public just to check info in Main

        private static void CreateKeyspace(string keyspace)
        {
            string query = $"CREATE KEYSPACE" + keyspace + "WITH replication = {'class':'SimpleStrategy', 'replication_factor' :3};";
            
            Session = Session ?? Connector.GetSession();

            Session.Execute(query);

            Console.WriteLine("Keyspace with name " + keyspace + "created.");
        }

        public static void CreateColumnFamily(string keyspace, string columnFamily)
        {
            CreateKeyspace(keyspace);

            Console.WriteLine("Logged keyspace: " + Session.Keyspace);

            var changeKeySpaceQuery = "USE " + keyspace;

            Session.Execute(changeKeySpaceQuery);

            Console.WriteLine("Logged keyspace: " + Session.Keyspace);

            string query = "CREATE COLUMNFAMILY " + keyspace + "." + columnFamily + "(" //In queries we do not pass the keyspace name, we check the current keyspace value in the session
                + "listingId varchar,"
                + "sellerId varchar,"
                + "skuId varchar,"
                + "productId varchar,"
                + "mrp int,"
                + "ssp int,"
                + "sla int,"
                + "stock int,"
                + "title text,"
                + "PRIMARY KEY (productId, listingId));";

            Session.Execute(query);
        }
    }
}
