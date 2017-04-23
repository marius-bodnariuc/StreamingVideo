using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamingVideo
{
    public class Cache
    {
        public static int CacheCapacity;

        public int Index { get; set; }
        public List<int> VideoIndexes { get; set; }

        public bool Empty { get; private set; }
        public int SpaceLeft { get; private set; }

        public Cache(int index)
        {
            Empty = true;
            Index = index;
            SpaceLeft = CacheCapacity;
            VideoIndexes = new List<int>();
        }

        public bool CanStore(Video video)
        {
            return SpaceLeft > video.Size
                && !VideoIndexes.Contains(video.Index);
        }

        public void Store(Video video)
        {
            Empty = false;
            VideoIndexes.Add(video.Index);
            SpaceLeft -= video.Size;
        }

        public string StoredVideosToString()
        {
            return string.Join(" ", VideoIndexes.Select(vi => vi.ToString()).ToList());
        }
    }
}