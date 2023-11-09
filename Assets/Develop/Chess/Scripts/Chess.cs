
using UnityEngine;

namespace ChessMate.Games.ChessGame
{
    public class Chess : Game
    {

        [Header("Player Assets")]
        [SerializeField] private PlayerSO whitePlayerSO;
        [SerializeField] private PlayerSO blackPlayerSO;

        public PlayerSO WhitePlayerSO => whitePlayerSO;

        public PlayerSO BlackPlayerSO => blackPlayerSO;

        public static string GetRank(string squareID)
        {
            return squareID[1].ToString();
        }

        public static string GetFile(string squareID)
        {
            return squareID[0].ToString();
        }

        public static Coord SquareToCoord(string squareID)
        {
            int rank = int.Parse(GetRank(squareID)) - 1;

            int file = GetFile(squareID) switch
            {
                "A" => 0,
                "B" => 1,
                "C" => 2,
                "D" => 3,
                "E" => 4,
                "F" => 5,
                "G" => 6,
                "H" => 7,
                _ => -1
            };

            return new Coord(file, rank);
        }

        public static string CoordToSquare(Coord coord)
        {
            string squareID = coord.x switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                3 => "D",
                4 => "E",
                5 => "F",
                6 => "G",
                7 => "H",
                _ => "X"
            };

            squareID += (coord.y + 1).ToString();

            return squareID;
        }

    }
}
