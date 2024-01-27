using Godot;
using System;
using System.Diagnostics;

public partial class Input : Node
{
	[Signal]
	public delegate void OnGrabGridEventHandler(Vector2I tilePosition);

	[Signal]
	public delegate void OnGrabInventoryEventHandler();

	[Signal]
	public delegate void OnGridTileRemoveEventHandler(Vector2I TilePosition);

	[Export]
	TileMap tile_map = null;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (!eventMouseButton.IsReleased()) return;
			if (eventMouseButton.ButtonIndex == MouseButton.Left)
			{
				EmitSignal(SignalName.OnGrabGrid,GetTilePos());
			}

			if (eventMouseButton.ButtonIndex == MouseButton.Right)
			{
				EmitSignal(SignalName.OnGridTileRemove, GetTilePos());
			}

		}
	}

	public Vector2I GetTilePos()
	{
		Debug.Assert(tile_map != null);
		Vector2 mouse_pos = GetViewport().GetMousePosition();
		return tile_map.LocalToMap(mouse_pos);
	}
}
