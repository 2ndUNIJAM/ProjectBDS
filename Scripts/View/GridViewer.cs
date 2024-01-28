using Godot;
using System;

public partial class GridViewer : TileMap
{
    int road_source_id = 2;
    int[] road_layer = { 3, 4, 5, 6 };
    Vector2I[] road_atlas_coords = { new Vector2I(0, 0), new Vector2I(1, 0), new Vector2I(0, 1), new Vector2I(1, 1) };

	int outline_source_id = 3;
	int outline_layer = 1;
	Vector2I outline_atlas_coords = Vector2I.Zero;

    int base_grid_source_id = 4;
	int base_grid_layer = 0;
	Vector2I base_grid_atlas_coords = Vector2I.Zero;

	public void UpdateGridView(State state, Vector2I updatedPoint)
	{
		InternalUpdateGridView(state.Grid, updatedPoint);
		SetTileOutlineInTileMap(state.Grab);
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
				Vector2I newPos = new Vector2I(j, i);
				if (tile == null)
				{
					for (int k = 0; k < GetLayersCount(); k++)
					{
						EraseTileInTileMap(k, newPos);
					}
					continue;
				}
				EraseTileInTileMap(outline_layer, newPos);
                PlaceRoad(grid, new Vector2I(j, i));
                SetTileInTileMap(tile.tile_layer, newPos, tile.source_id, tile.atlas_coord);

                SetTileInTileMap(base_grid_layer, newPos, base_grid_source_id, base_grid_atlas_coords);
                //GD.Print(tile.atlas_coord);
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

	public void SetTileOutlineInTileMap(Grab grab)
	{
		if (grab.GridGrab.Count > 0)
		{
			SetTileInTileMap(outline_layer, grab.GridGrab[0], outline_source_id, outline_atlas_coords);
		}
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
		Vector2I map_pos = LocalToMap(mouse_pos);
		return map_pos;
	}

}
