namespace MinimalApi2.Aws.Options
{
    public class SqlServerOptions
    {
        public string SqlConnection { get; set; }
        public string RetryCount { get; set; }

        public string RetryDelay { get; set; }

        public string InMemory { get; set; }
    }
}
