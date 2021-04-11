using catalog_management_system_with_cassandra.Connectivity;
using catalog_management_system_with_cassandra.DataPersistence.Model;
using catalog_management_system_with_cassandra.DataPersistence.Models.Enums;
using catalog_management_system_with_cassandra.DataPersistence.Queries;
using System;

namespace catalog_management_system_with_cassandra.DataPersistence
{
    public static class ListingPersistenceHandler
    {
        private static string ColumnFamily = "listings";

        public static void Put(Listing listing)
        {
            var insertPreparedStatement = QueryBuilder.InsertInto(ColumnFamily);

            var attributes = listing.Attributes;

            var insertBoundStatement = insertPreparedStatement.Bind(Enum.GetName(typeof(AttributesNames),AttributesNames.LISTINGID), listing.ListingId);

            foreach (var attributeName in attributes.Keys)
            {
                attributes.TryGetValue(attributeName, out var attributeValue);
                insertBoundStatement.PreparedStatement.Bind(attributeName, attributeValue);
            }

            var session = Connector.Session;

            session.Execute(insertBoundStatement);
        }
    }
}
