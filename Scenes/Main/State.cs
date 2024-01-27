using Godot;
using System;

public partial class State : Node
{
	public Inventory Inventory { get; set; }
	public Grid Grid { get; set; }
	public Grab Grab { get; set; }
	public override void _Ready()
	{
		Inventory = new Inventory();

		Godot.Collections.Dictionary jsonMap =  TileHandler.LoadJsonMapFiles("res://Map.json");
		int gridWidth = (int)jsonMap["width"];
		int gridHeight = (int)jsonMap["height"];
		Grid = new Grid(gridWidth,gridHeight); // 1x1
		Grab = new Grab();
	}
}
