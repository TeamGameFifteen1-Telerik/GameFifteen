namespace GameFifteen.Models.Contracts
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IGrid : IGameMember
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
