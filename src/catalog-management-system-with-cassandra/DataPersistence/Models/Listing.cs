using System.Collections.Generic;

namespace catalog_management_system_with_cassandra.DataPersistence.Model
{
    public class Listing
    {
        public string ListingId { get; set; }

        public Dictionary<string, object> Attributes { get; set; }

        public Listing()
        {
            this.Attributes = new Dictionary<string, object>();
        }

        override
        public string ToString()
        {
            return $"Listing with id: {ListingId}, {Attributes}";
        }
    }
}
