using Godot;
using System;
using System.Diagnostics;

public partial class Input : Node
{
	[Signal]
	public delegate void OnGrabGridEventHandler(Vector2I tilePosition);

	[Signal]
	public delegate void OnGrabInventoryEventHandler(int index);

	[Signal]
	public delegate void OnGridTileRemoveEventHandler(Vector2I TilePosition);

/*	[Export]
	TileMap tile_map = null;*/

	[Export]
	GridViewer viewer;
	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
/*		GD.Print($"{GetViewport().GetMousePosition()}");
		
		GD.Print(viewer.GetTilePos());*/
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (!eventMouseButton.IsReleased()) return;
			if (eventMouseButton.ButtonIndex == MouseButton.Left)
			{
				EmitSignal(SignalName.OnGrabGrid,viewer.GetTilePos());
			}

			if (eventMouseButton.ButtonIndex == MouseButton.Right)
			{
				EmitSignal(SignalName.OnGridTileRemove, viewer.GetTilePos());
			}

		}
	}

	public void SelectInventoryItem(int inventoryIndex)
	{
		GD.Print($"Inventory Input! {inventoryIndex}");
		EmitSignal(SignalName.OnGrabInventory, inventoryIndex);
	}
}
