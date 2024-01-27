using Godot;
using System;

public partial class GridViewer : TileMap
{
	[Export]

	Vector2I curTilePos;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		curTilePos = GetTilePos();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (!eventMouseButton.Pressed) return;
			// source_id <-> tile_layer are muse be mached each other.
			if (eventMouseButton.ButtonIndex == MouseButton.Left || 
				eventMouseButton.ButtonIndex == MouseButton.Right)
			{
				//UpdateGridView(testGridNode.grid, curTilePos);
			}
		}
	}

	public void UpdateGridView(in Grid grid, in Vector2I updatedPoint)
	{
		if (grid == null) return;
		GD.Print("Update Grid View");

		//update all grid cell
		for (int i = 0; i < grid.Tiles.Count; i++)
		{
			for (int j = 0; j < grid.Tiles[i].Count; j++)
			{
				Tile tile = grid.Tiles[i][j];
				if (tile == null)
				{
					for (int k = 0; k < this.GetLayersCount(); k++)
					{
						EraseTileInTileMap(k, new Vector2I(j, i));
					}
					continue;
				}
				SetTileInTileMap(tile.tile_layer, new Vector2I(j, i), tile.source_id, tile.atlas_coord);
			}
		}
	}
	public void SetTileInTileMap(int tile_layer, Vector2I cur_tile_pos, int tile_source_id, Vector2I tile_atlas_coord)
	{
		this.SetCell(tile_layer, cur_tile_pos, tile_source_id, tile_atlas_coord);
	}

	public void EraseTileInTileMap(int tile_layer, Vector2I cur_tile_pos)
	{
		this.EraseCell(tile_layer, cur_tile_pos);
	}

	public Vector2I GetTilePos()
	{
		Vector2 mouse_pos = GetViewport().GetMousePosition();
		return this.LocalToMap(mouse_pos);
	}

}
