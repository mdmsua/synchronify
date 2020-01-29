using Microsoft.Azure.Cosmos.Table;

namespace Syncify.Entities
{
    public class User : TableEntity
    {
        public User()
        {
        }

        public User(string id, string type, string token)
        {
            RowKey = id;
            PartitionKey = type;
            Token = token;
        }

        public string Token { get; set; } = string.Empty;
    }
}
