using Godot;
using System;

public partial class GridViewer : TileMap
{
    [Export]
    TileMap tile_map = null;
    bool bUpdated = false;

    /* Test */
    TestNode testGridNode;
    Vector2I curTilePos;
    public override void _Ready()
    {
        //Get grid node
        testGridNode = GetNode<TestNode>("../TestGridNode");
    }

    public override void _Process(double delta)
    {
        curTilePos = GetTilePos(tile_map);
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
                UpdateGridView(testGridNode.grid, curTilePos);
            }
        }
    }

    public void UpdateGridView(in Grid grid, in Vector2I updatedPoint)
    {
        if (grid == null) return;
        GD.Print("Update Grid View");
        //GD.Print(updatedPoint);

        //update all grid cell
        for (int i = 0; i < grid.Tiles.Count; i++)
        {
            for (int j = 0; j < grid.Tiles[i].Count; j++)
            {
                Tile tile = grid.Tiles[i][j];
                if (tile == null)
                {
                    for (int k = 0; k < tile_map.GetLayersCount(); k++)
                    {
                        EraseTileInTileMap(tile_map, k, new Vector2I(j, i));
                    }
                    continue;
                }
                SetTileInTileMap(tile_map, tile.tile_layer, new Vector2I(j, i), tile.source_id, tile.atlas_coord);
            }
        }

        // update only updatedPoint
        /*        int tileX = updatedPoint.X, tileY = updatedPoint.Y;
                Tile tile = grid.Tiles[tileY][tileX];
                if (tile == null) return;
                SetTileInTileMap(tile_map, tile_layer, updatedPoint, tile.source_id, tile.atlas_coord);*/
    }
    public void SetTileInTileMap(TileMap tile_map, int tile_layer, Vector2I cur_tile_pos, int tile_source_id, Vector2I tile_atlas_coord)
    {
        if (tile_map == null) return;
        tile_map.SetCell(tile_layer, cur_tile_pos, tile_source_id, tile_atlas_coord);
    }

    public void EraseTileInTileMap(TileMap tile_map, int tile_layer, Vector2I cur_tile_pos)
    {
        if (tile_map == null) return;
        tile_map.EraseCell(tile_layer, cur_tile_pos);
        //GD.Print("Erase Tile");
    }

    public Vector2I GetTilePos(TileMap tile_map)
    {
        if (tile_map == null) return Vector2I.Zero;
        Vector2 mouse_pos = GetViewport().GetMousePosition();
        return tile_map.LocalToMap(mouse_pos);
    }

}
