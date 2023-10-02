using Newtonsoft.Json;

namespace IotWebApi.Helpers
{
    //
    // Summary:
    //     Defines the members of the data manager operation result.
    //
    // Type parameters:
    //   T:
    //     Type of the data source element.
    public class CollectionResult<T>
    {
        //
        // Summary:
        //     Gets the result of the data operation.
        [JsonProperty("result")]
        public IEnumerable<T> Result
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets the total count of the records in data source.
        [JsonProperty("count")]
        public int Count
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets the aggregate result based on the aggregate query.
        [JsonProperty("aggregates")]
        public IDictionary<string, object> Aggregates
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets the filtered records.
        public IEnumerable<T> FilteredRecords
        {
            get;
            set;
        }
    }
}
