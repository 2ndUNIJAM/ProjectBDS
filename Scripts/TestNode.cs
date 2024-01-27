using Godot;
using System;
using System.Collections.Generic;

public partial class TestNode : Node
{
    [Export]
    int gridWidth;
    [Export]
    int gridHeight;

    public Inventory inventory;
    public Grid grid;
    public Grab grab;
    public TileHandler tileHandler;

    TileMap tileMap;
    Tile[] tiles;
    Tile newTile;

    public override void _Ready()
    {
        InitialSetup();
        //TestSetupGridViewer();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == MouseButton.Left ||
                eventMouseButton.ButtonIndex == MouseButton.Right)
            {
                if (tileMap == null) return;
                
                Vector2I curTilePos = GetTilePos(tileMap);

                if (grid == null) return;
                if (curTilePos.X < 0 || curTilePos.X >= gridWidth || curTilePos.Y < 0 || curTilePos.Y >= gridHeight) return;
                
                Tile tile = grid.Tiles[curTilePos.Y][curTilePos.X];
                if (eventMouseButton.ButtonIndex == MouseButton.Left)
                {
                    if (tile == null)
                    {
                        GD.Print("Tile is null");
                        if (grab.GridGrab.Count <= 0 && grab.InventoryGrab < 0) return;
                        //GD.Print("Place Tile");
                        if (grab.GridGrab.Count > 0)
                        {
                            tileHandler.SwapTiles(grid, grab, inventory, curTilePos);
                        }
                        else
                        {
                            tileHandler.PlaceTile(grid, grab, inventory, curTilePos);
                        }
                    }
                    else
                    {
                        if (grab.GridGrab.Count <= 0 && grab.InventoryGrab < 0)
                        {
                            tileHandler.PushGridGrab(grab, curTilePos);
                        }
                        else
                        {
                            tileHandler.SwapTiles(grid, grab, inventory, curTilePos);
                        }
                    }
                }
                else if(eventMouseButton.ButtonIndex == MouseButton.Right)
                {
                    if (tile == null) return;
                    tileHandler.PushGridGrab(grab, curTilePos);
                    tileHandler.RemoveTile(grid, grab, inventory);
                }

                GD.Print(inventory.Tiles.Count);
            }
        }
        else if(@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed)
            {
                Key key = eventKey.Keycode;
                if(key == Key.A)
                {
                    //GD.Print("Key A Pressed");
                    Tile tile = new Tile();
                    tile.tile_layer = 1;
                    tile.source_id = 1;
                    inventory.Tiles.AddLast(tile);
                }
                else if(key == Key.B)
                {
                    //GD.Print("Key B Pressed");
                    Tile tile = new Tile();
                    tile.tile_layer = 1;
                    tile.source_id = 1;
                    tile.atlas_coord = new Vector2I(1, 0);
                    inventory.Tiles.AddLast(tile);
                }
                else if(key == Key.C)
                {
                    tileHandler.PushInventoryGrab(grab, 0);
                }
            }
        }
    }
    public void InitialSetup()
    {
        tileMap = GetNode<TileMap>("../TileMap");

        inventory = new Inventory();
        tileHandler = new TileHandler();
        Godot.Collections.Dictionary jsonMap = tileHandler.LoadJsonMapFiles("res://Map.json");
        if (jsonMap != null)
        {
            gridWidth = (int)jsonMap["width"];
            gridHeight = (int)jsonMap["height"];
        }
        else
        {
            gridWidth = 5;
            gridHeight = 5;
        }
        //GD.Print("Grid Width: " + gridWidth);
        //GD.Print("Grid Height: " + gridHeight);
        grid = new Grid(gridWidth, gridHeight);
        grab = new Grab();


    }


    public void TestSetupGridViewer()
    {
        for(int i=0; i<gridHeight; i++)
        {
            for(int j=0; j<gridWidth; j++)
            {
                grid.Tiles[i][j] = new Tile();
            }
        }
    }

    public void TestTileHandler()
    {
        tiles = new Tile[5];
        for (int i = 0; i < 5; i++)
        {
            tiles[i] = new Tile();
        }

        newTile = new Tile();
        newTile.Node = NodeType.NodeType2;

        foreach (Tile tile in tiles)
        {
            inventory.Tiles.AddLast(tile);
        }

        grid.Tiles[0][0] = newTile;

        PrintGrid();

        tileHandler.PushGridGrab(grab, new Vector2I(0, 0));

        tileHandler.PlaceTile(grid, grab, inventory, new Vector2I(2, 2));

        PrintGrid();

        tileHandler.PushInventoryGrab(grab, 0);

        tileHandler.PlaceTile(grid, grab, inventory, new Vector2I(1, 1));

        PrintGrid();
    }

    public void PrintGrid()
    {
        for (int i = 0; i < grid.Tiles.Count; i++)
        {
            for (int j = 0; j < grid.Tiles[i].Count; j++)
            {
                Tile tile = grid.Tiles[i][j];
                if (tile != null)
                {
                    GD.Print("x: " + j + ", y: " + i + ", " + "NodeType: " + tile.Node);
                }
                else
                {
                    GD.Print("x: " + j + ", y: " + i + ", " + "NodeType: null");
                }
            }
        }
        GD.Print();
    }

    public Vector2I GetTilePos(TileMap tile_map)
    {
        if (tile_map == null) return Vector2I.Zero;
        Vector2 mouse_pos = GetViewport().GetMousePosition();
        return tile_map.LocalToMap(mouse_pos);
    }

}
