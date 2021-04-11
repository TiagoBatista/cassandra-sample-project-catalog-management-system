using Cassandra;
using catalog_management_system_with_cassandra.Connectivity;

namespace catalog_management_system_with_cassandra.DataPersistence.Queries
{
    public static class QueryBuilder
    {
        public static PreparedStatement InsertInto(string columnFamily)
        {
            var session = Connector.Session;

            var query = "INSERT INTO " + columnFamily + " VALUES ('?,?')";

            return session.Prepare(query);
        }
    }
}
