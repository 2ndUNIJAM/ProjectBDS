using Godot;
using System;
using System.Collections.Generic;

public partial class TestNode : Node
{
    public Inventory inventory;
    public Grid grid;
    public Grab grab;
    public TileHandler tileHandler;

    Tile[] tiles;
    Tile newTile;


    public override void _Ready()
    {
        tiles = new Tile[5];
        for(int i= 0; i < 5; i++)
        {
            tiles[i] = new Tile();
        }
        inventory = new Inventory();
        grid = new Grid(3,4);
        grab = new Grab();

        newTile = new Tile();
        newTile.Node = NodeType.NodeType2;

        foreach (Tile tile in tiles)
        {
            inventory.Tiles.AddLast(tile);
        }

        tileHandler = new TileHandler();
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

}
