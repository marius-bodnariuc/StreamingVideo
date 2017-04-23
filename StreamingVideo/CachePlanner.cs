using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingVideo
{
    public class CachePlanner
    {
        public int VideosCount, EndpointsCount, RequestTypesCount, CachesCount, CacheCapacity;

        public List<Video> Videos;

        public List<Endpoint> Endpoints;
        public List<RequestType> RequestTypes;

        public List<Cache> Caches;

        private string _filename;

        public CachePlanner(string file)
        {
            _filename = file;
            ReadInput();
        }

        public void Plan()
        {
            Endpoints.ForEach(endpoint =>
            {
                var requests = endpoint.VideoRequests(RequestTypes)
                    .Where(rt => Videos[rt.VideoIdx].Size <= Cache.CacheCapacity)
                    .OrderByDescending(rt => rt.RequestCount / Videos[rt.VideoIdx].Size)
                    .ToList();

                var caches = endpoint.CacheDict.OrderBy(kv => kv.Value).ToList();

                requests.ForEach(req =>
                {
                    var bestCache = caches.FirstOrDefault(cache => Caches[cache.Key].CanStore(Videos[req.VideoIdx]));
                    if (!bestCache.Equals(new KeyValuePair<int, int>(0, 0))) // equiv. to null check
                    {
                        Caches[bestCache.Key].Store(Videos[req.VideoIdx]);
                    }
                });
            });
        }

        #region input

        private void ReadInput()
        {
            using (var reader = new StreamReader($"{_filename}.in"))
            {
                var tokens = reader.TokenizeNextLine();

                VideosCount = int.Parse(tokens[0]);
                EndpointsCount = int.Parse(tokens[1]);
                RequestTypesCount = int.Parse(tokens[2]);
                CachesCount = int.Parse(tokens[3]);
                CacheCapacity = int.Parse(tokens[4]);

                tokens = reader.TokenizeNextLine();

                // Caches
                Caches = new List<Cache>(CachesCount);
                Cache.CacheCapacity = CacheCapacity;
                CachesCount.TimesWithIndex(idx => Caches.Add(new Cache(idx)));

                // Videos
                Videos = new List<Video>();
                tokens.Length.TimesWithIndex(idx => Videos.Add(new Video(idx, int.Parse(tokens[idx]))));

                // Endpoints
                Endpoints = new List<Endpoint>();
                EndpointsCount.TimesWithIndex(i =>
                {
                    tokens = reader.TokenizeNextLine();

                    var datacenterLatency = int.Parse(tokens[0]);
                    var cacheCount = int.Parse(tokens[1]);

                    var cacheDict = new Dictionary<int, int>();
                    cacheCount.Times(() =>
                    {
                        tokens = reader.TokenizeNextLine();
                        cacheDict.Add(int.Parse(tokens[0]), int.Parse(tokens[1]));
                    });

                    Endpoints.Add(new Endpoint(i, datacenterLatency, cacheDict));
                });

                // Requests
                RequestTypes = new List<RequestType>();
                RequestTypesCount.TimesWithIndex(i =>
                {
                    tokens = reader.TokenizeNextLine();

                    var videoIdx = int.Parse(tokens[0]);
                    var endpointIdx = int.Parse(tokens[1]);
                    var reqCount = int.Parse(tokens[2]);

                    RequestTypes.Add(new RequestType(i, videoIdx, endpointIdx, reqCount));
                });
            }
        }

        #endregion input

        #region output

        public void PrintInputData()
        {
            Console.WriteLine($"{VideosCount}, {EndpointsCount}, {RequestTypesCount}, {CachesCount}, {CacheCapacity}");

            PrintCollection(Videos, "Videos:");
            PrintCollection(Endpoints, "Endpoints:");
            PrintCollection(RequestTypes, "RequestTypes:");
        }

        private void PrintCollection<T>(List<T> collection, string startLine)
        {
            Console.WriteLine(startLine);
            Videos.ForEach(v => Console.WriteLine(v));
            Console.WriteLine("------------------------");
        }

        public void PrintPlan()
        {
            var usedCaches = Caches.Where(c => !c.Empty).ToList();
            var cacheCountString = $"{usedCaches.Count}";

            using (var sw = new StreamWriter($"{_filename}.out"))
            {
                sw.WriteLine(cacheCountString);
                usedCaches.ForEach(cache =>
                {
                    var cacheVideosString = $"{cache.Index} {cache.StoredVideosToString()}";
                    sw.WriteLine(cacheVideosString);
                });
            }
        }

        #endregion output
    }
}
