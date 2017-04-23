using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamingVideo
{
    public class Endpoint
    {
        public int Index { get; set; }
        public int DatacenterLatency { get; set; }

        public Dictionary<int, int> CacheDict { get; set; }

        public Endpoint(int index, int datacenterLatency, Dictionary<int, int> cacheDict)
        {
            Index = index;
            DatacenterLatency = datacenterLatency;
            CacheDict = cacheDict;
        }

        public List<RequestType> VideoRequests(List<RequestType> requestTypes)
        {
            return requestTypes.Where(rt => rt.EndpointIdx == Index).ToList();
        }

        public override string ToString()
        {
            return $"{Index}, {DatacenterLatency}, {CacheDictionaryToString()}";
        }

        #region Private

        private string CacheDictionaryToString()
        {
            return string.Join(", ", CacheDict.ToList().Select(kv => $"({kv.Key}, {kv.Value})").ToList());
        }

        #endregion Private
    }
}