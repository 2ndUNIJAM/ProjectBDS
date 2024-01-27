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
		Grid = new Grid();
		Grab = new Grab();
	}
}
