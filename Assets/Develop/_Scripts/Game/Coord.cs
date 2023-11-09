
namespace ChessMate.Games
{
    [System.Serializable]
    public struct Coord
    {

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x;

        public int y;

    }
}
