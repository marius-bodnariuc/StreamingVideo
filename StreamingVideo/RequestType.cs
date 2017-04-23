namespace StreamingVideo
{
    public class RequestType
    {
        public int Index { get; set; }

        public int EndpointIdx { get; set; }
        public int VideoIdx { get; set; }
        public int RequestCount { get; set; }

        public RequestType(int i, int videoIdx, int endpointIdx, int reqCount)
        {
            Index = i;
            VideoIdx = videoIdx;
            EndpointIdx = endpointIdx;
            RequestCount = reqCount;
        }

        public override string ToString()
        {
            return $"{Index}, {EndpointIdx}, {VideoIdx}, {RequestCount}";
        }
    }
}