using Godot;
using System.Collections.Generic;

public enum EdgeType { 
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
	public EdgeType Weast;
	public EdgeType South;
	public EdgeType North;
	public NodeType Node;
}

public class Inventory
{
	public LinkedList<Tile> Tiles;
}

public class Grid
{
	public List<List<Tile>> Tiles;
}

public class Grab
{
	public List<int> InventoryGrab;
	public List<Vector2I> GridGrab;

}
