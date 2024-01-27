using Godot;
using System;

public partial class Input : Node
{
	[Signal]
	public delegate void OnGrabGridEventHandler();

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
			if (eventMouseButton.ButtonIndex == MouseButton.Left)
			{
			}

			if (eventMouseButton.ButtonIndex == MouseButton.Right)
			{
				EmitSignal(SignalName.OnGridTileRemove, GetTilePos());
			}

		}
	}

	public Vector2I GetTilePos()
	{
		if (tile_map == null) return Vector2I.Zero;
		Vector2 mouse_pos = GetViewport().GetMousePosition();
		return tile_map.LocalToMap(mouse_pos);
	}
}
