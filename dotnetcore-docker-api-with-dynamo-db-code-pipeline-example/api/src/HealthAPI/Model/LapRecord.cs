using Amazon.DynamoDBv2.DataModel;

namespace HealthAPI.Model
{
    [DynamoDBTable("LapRecords")]
    public class LapRecord
    {
        [DynamoDBHashKey("track")]
        public string Track { get; set; }

        [DynamoDBRangeKey("driver")]
        public string Driver { get; set; }

        public string LapTime { get; set; }
    }
}
