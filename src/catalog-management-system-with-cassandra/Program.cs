using catalog_management_system_with_cassandra.DataPersistence;
using catalog_management_system_with_cassandra.DataPersistence.Model;
using catalog_management_system_with_cassandra.DataPersistence.Models.Enums;
using System;
using System.Collections.Generic;

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

            var listing = CreateListingData();

            ListingPersistenceHandler.Put(listing);
        }

        private static Listing CreateListingData()
        {
            var listing = new Listing();
            listing.ListingId = ("LISTINGFABSOFA5");

            var attributes = new Dictionary<string, object>();
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.SELLERID), "Fab");
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.SKUID), "SKU2");
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.MRP), 5000);
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.SSP), 4000);
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.SLA), 2);
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.STOCK), 2);
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.PRODUCTID), "SOFA5");
            attributes.Add(Enum.GetName(typeof(AttributesNames),AttributesNames.TITLE), "Urban Loving Sofa 3 Seater");

            listing.Attributes = attributes;

            return listing;
        }
    }
}
