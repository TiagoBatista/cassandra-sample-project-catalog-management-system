using Cassandra;

namespace catalog_management_system_with_cassandra.Connectivity
{
    public static class Connector
    {
        private static string Host = "localhost";
        private static int Port = 9042;
        private static string Username = "cassandra";
        private static string Password = "cassandra";
        private static int MAX_CONNECTIONS = 100;
        private static int CORE_CONNECTIONS = 25;

        public static ISession Session => Session ?? GetSession();

        private static ISession GetSession()
        {
            var poolingOptions = PoolingOptions.Create(); //Pooling options are based on the number of concurrent threads in our app that need to connect to the cluster
            poolingOptions.SetMaxConnectionsPerHost(HostDistance.Local, MAX_CONNECTIONS); //Maximum number of connections a host is allowed to make to the cluster
            poolingOptions.SetCoreConnectionsPerHost(HostDistance.Local, CORE_CONNECTIONS); //Minimum number of connections required by the app to start

            var cluster = Cluster.Builder()
                .AddContactPoint(Host) //Node of a cluster
                .WithPort(Port)
                .WithCredentials(Username, Password)
                .WithPoolingOptions(poolingOptions)
                .Build();

            var session = cluster.Connect();

            return session; //Contains connection pools and metadata about the keyspaces(databases) and column families(tables) in the cluster
        }
    }
}
