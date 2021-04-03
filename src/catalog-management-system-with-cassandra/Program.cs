using System;

namespace catalog_management_system_with_cassandra
{
    class Program
    {
        static void Main(string[] args)
        {
            var keyspaceName = "cms";
            var columnFamilyName = "ProductListing";

            CassandraStructureManager.CreateColumnFamily(keyspaceName, columnFamilyName);

            var session = CassandraStructureManager.Session;

            var cluster = session.Cluster;

            var keyspaceMetadatas = cluster.Metadata.GetKeyspaces();

            if(keyspaceMetadatas.Contains(keyspaceName))
            {
                if (cluster.Metadata.GetKeyspace(keyspaceName).GetTableMetadata(columnFamilyName) != null)
                {
                    Console.WriteLine("Column Family (Table) : " + columnFamilyName + " exists in keyspace : " + keyspaceName);
                }
            }
            else
            {
                Console.WriteLine("The keyspace " + keyspaceName + " doesn't exist");
            }

        }
    }
}
