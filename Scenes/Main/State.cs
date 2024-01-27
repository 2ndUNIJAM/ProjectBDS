using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

public partial class State : Node
{
	public Inventory Inventory { get; set; }
	public Grid Grid { get; set; }
	public Grab Grab { get; set; }
	[Export]
	string JsonMapPath;
	public override void _Ready()
	{
		Inventory = new Inventory();
		Grab = new Grab();
		Grid = new Grid();
		TileHandler.PopulateMapWithJson(Grid, Inventory, JsonMapPath);
	}

}
