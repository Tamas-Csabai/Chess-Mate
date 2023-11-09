
using System.Collections.Generic;

namespace ChessMate.Games
{
    public abstract class PieceBehaviour
    {

        public abstract List<Coord> GetAvailableFieldCoords(Piece piece);

    }
}
