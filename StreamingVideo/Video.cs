namespace StreamingVideo
{
    public class Video
    {
        public int Index { get; set; }
        public int Size { get; set; }

        public Video(int index, int size)
        {
            Index = index;
            Size = size;
        }

        public override string ToString()
        {
            return $"{Index}, {Size}";
        }
    }
}