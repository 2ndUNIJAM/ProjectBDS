using Godot;
using System;

public partial class GridViewer : TileMap
{
	[Export]

	Vector2I curTilePos;

    int road_source_id = 2;
    int[] road_layer = { 2, 3, 4, 5 };
    Vector2I[] road_atlas_coords = { new Vector2I(0, 0), new Vector2I(1, 0), new Vector2I(0, 1), new Vector2I(1, 1) };

	//State state;

    public override void _Ready()
	{
		// State가 있다 가정
		// state = GetNode<State>("../State");
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

			if (eventMouseButton.ButtonIndex == MouseButton.Left || 
				eventMouseButton.ButtonIndex == MouseButton.Right)
			{
				// state가 있다 가정
				//UpdateGridView(state.grid, curTilePos);
			}
		}
	}

	public void UpdateGridView(State state, Vector2I updatedPoint)
	{
		InternalUpdateGridView(state.Grid, updatedPoint);
	}

	private void InternalUpdateGridView(in Grid grid, in Vector2I updatedPoint)
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
					for (int k = 0; k < GetLayersCount(); k++)
					{
						EraseTileInTileMap(k, new Vector2I(j, i));
					}
					continue;
				}
                PlaceRoad(grid,new Vector2I(j,i));
                SetTileInTileMap(tile.tile_layer, new Vector2I(j, i), tile.source_id, tile.atlas_coord);
				GD.Print(tile.atlas_coord);
			}
		}
	}
	public void SetTileInTileMap(int tile_layer, Vector2I cur_tile_pos, int tile_source_id, Vector2I tile_atlas_coord)
	{
		SetCell(tile_layer, cur_tile_pos, tile_source_id, tile_atlas_coord);
	}

	public void EraseTileInTileMap(int tile_layer, Vector2I cur_tile_pos)
	{
		EraseCell(tile_layer, cur_tile_pos);
	}

    public void PlaceRoad(Grid grid, Vector2I target)
    {
		if (grid == null) return;
		GD.Print("Place Road");
		int x = target.X, y = target.Y;
		if (grid.Tiles[y][x].North == EdgeType.Connected) SetTileInTileMap(road_layer[0], target, road_source_id, road_atlas_coords[0]);
		else EraseTileInTileMap(road_layer[0], target);

        if (grid.Tiles[y][x].East == EdgeType.Connected) SetTileInTileMap(road_layer[1], target, road_source_id, road_atlas_coords[1]);
        else EraseTileInTileMap(road_layer[1], target);

        if (grid.Tiles[y][x].West == EdgeType.Connected) SetTileInTileMap(road_layer[2], target, road_source_id, road_atlas_coords[2]);
        else EraseTileInTileMap(road_layer[2], target);

        if (grid.Tiles[y][x].South == EdgeType.Connected) SetTileInTileMap(road_layer[3], target, road_source_id, road_atlas_coords[3]);
        else EraseTileInTileMap(road_layer[3], target);
    }

    public Vector2I GetTilePos()
	{
		Vector2 mouse_pos = GetLocalMousePosition();
		return this.LocalToMap(mouse_pos);
	}

}
