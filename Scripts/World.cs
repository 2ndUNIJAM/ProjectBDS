using Godot;
using System;

public partial class World : Node
{
	[Export]
	TileMap tile_map = null;
    [Export]
    int ground_layer = 0;
	[Export]
	int tile_source_id = 0;
	[Export]
	Vector2I tile_atlas_coord = new Vector2I(0, 0);

	Vector2I cur_tile_Pos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//tile_map = GetNode<TileMap>($"../TileMap");
		if (tile_map!=null)
		{
			GD.Print("Tile map found");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex == MouseButton.Left)
			{
				GD.Print("Left Clicked");
                //ClearTileMap(tile_map);
                SetTileInTileMap(tile_map);
			}
			else if (eventMouseButton.ButtonIndex == MouseButton.Right)
			{
				GD.Print("Right Clicked");
				EraseTileInTileMap(tile_map);
			}

		}
		/*		if (@event is InputEventMouseButton eventMouseButton)
				{
					GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
				}
				else if (@event is InputEventMouseMotion eventMouseMotion)
				{
					GD.Print("Mouse Motion at: ", eventMouseMotion.Position);
				}*/
	}

	public void ClearTileMap(TileMap tile_map)
	{
        if (tile_map == null) return;
        tile_map.Clear();
		GD.Print("Map Cleared");
	}

	public void SetTileInTileMap(TileMap tile_map)
	{
		if (tile_map == null) return;
		tile_map.SetCell(ground_layer, cur_tile_Pos, tile_source_id, tile_atlas_coord);
		GD.Print("Set Tile");
	}

	public void EraseTileInTileMap(TileMap tile_map)
	{
        if (tile_map == null) return;
        tile_map.EraseCell(ground_layer, cur_tile_Pos);
		GD.Print("Erase Tile");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		cur_tile_Pos = GetTilePos();
		if (@event is InputEventKey eventKey)
			if (eventKey.Pressed && eventKey.Keycode == Key.Escape)
				GetTree().Quit();
	}

	public Vector2I GetTilePos()
	{
		if (tile_map == null) return Vector2I.Zero;
		Vector2 mouse_pos = GetViewport().GetMousePosition();
		//GD.Print(mouse_pos);
		return tile_map.LocalToMap(mouse_pos);
	}
}
