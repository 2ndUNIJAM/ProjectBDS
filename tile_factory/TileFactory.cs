using Godot;
using System;

enum EdgeType { 
	EdgeType1,
	EdgeType2
}

enum NodeType
{
	NodeType1,
	NodeType2,
	NodeType3,
}

struct Tile {
	EdgeType East;
	EdgeType Weast;
	EdgeType South;
	EdgeType North;
	NodeType Node;
}

public partial class TileFactory : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
