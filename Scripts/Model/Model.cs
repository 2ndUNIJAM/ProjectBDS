using Godot;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public enum EdgeType
{
    DisConnected,
    Connected
}

public enum NodeType
{
    NodeType1,
    NodeType2,
    NodeType3,
}

public class Tile
{
    public EdgeType East;
    public EdgeType West;
    public EdgeType South;
    public EdgeType North;
    public NodeType Node;

    public int tile_layer;
    public int source_id;
    public Vector2I atlas_coord;

    public Tile()
    {
        East = EdgeType.DisConnected;
        West = EdgeType.DisConnected;
        South = EdgeType.DisConnected;
        North = EdgeType.DisConnected;
        Node = NodeType.NodeType1;

        tile_layer = 2;
        source_id = 1;
        atlas_coord = Vector2I.Zero;
    }
}

public class Inventory
{
    public LinkedList<Tile> Tiles;
    public Inventory()
    {
        Tiles = new LinkedList<Tile>();
    }
}

public class Grid
{
    public List<List<Tile>> Tiles;

    public Grid()
    {
        Tiles = new List<List<Tile>>(10); 
    }
    public Grid(int width, int height)
    {
        //GD.Print("Grid Constructor");
        Tiles = new List<List<Tile>>(height);
    }
}

public class Grab
{
    public int InventoryGrab;
    public List<Vector2I> GridGrab;

    public Grab()
    {
        InventoryGrab = -1;
        GridGrab = new List<Vector2I>();
    }

}
