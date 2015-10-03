namespace GameFifteen.Models.Contracts
{
    using System.Collections;

    public interface IGrid
    {
        int TilesCount { get; }

        bool IsSorted { get; }

        void AddTile(Tile tile);

        void Clear();

        Tile GetTileAtPosition(int position);

        Tile GetTileFromLabel(string tileLabel);

        void SwapTiles(Tile targetTile);

        bool CanSwap(Tile tile);

        IEnumerator GetEnumerator();

        Memento SaveMemento();

        void RestoreMemento(Memento memento);
    }
}
