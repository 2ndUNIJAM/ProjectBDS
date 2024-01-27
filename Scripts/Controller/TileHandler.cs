using Godot;
using System;
using System.Collections.Generic;

public partial class TileHandler : GodotObject
{
    static public void RemoveTile(Grid grid, Grab grab, Inventory inventory)
    {
        if (grid == null) return;
        if (grab == null) return;
        if (grab.GridGrab.Count > 0)
        {
            GD.Print("Remove Tile");
            for(int i=0; i<grab.GridGrab.Count; i++)
            {
                int x = grab.GridGrab[i].X;
                int y = grab.GridGrab[i].Y;
                inventory.Tiles.AddLast(grid.Tiles[y][x]);
                grab.GridGrab.Remove(new Vector2I(x, y));
                grid.Tiles[y][x] = null;
            }
/*            foreach (Vector2I targetTile in grab.GridGrab)
            {
                int x = targetTile.X;
                int y = targetTile.Y;
                inventory.Tiles.AddLast(grid.Tiles[y][x]);
                grab.GridGrab.Remove(new Vector2I(x,y));
                grid.Tiles[y][x] = null;
            }*/
        }
    }

    static public void SwapTiles(Grid grid, Grab grab, Inventory inventory, Vector2I target)
    {
        // Swap tile one by one
        int swappedX = target.X;
        int swappedY = target.Y;
        // if tiles are from the inventory
        if (grab.InventoryGrab>=0)
        {
            GD.Print("Swap from Inventory");
            int inventoryIdx = grab.InventoryGrab;
            Tile tempTile = grid.Tiles[swappedY][swappedX];
            Tile inventoryTile = null;
            int curIdx = 0;
            foreach(Tile tile in inventory.Tiles)
            {
                if (curIdx > inventoryIdx) break;
                inventoryTile = tile;
                curIdx++;
                
            }
            grid.Tiles[swappedY][swappedX] = inventoryTile;
            inventory.Tiles.Remove(inventoryTile);
            if (tempTile == null)
            {
                grab.InventoryGrab = -1;
                return;
            }
            inventory.Tiles.AddLast(tempTile);
        }
        else if(grab.GridGrab.Count>0)
        {
            GD.Print("Swap from GridGrab");
            int grabbedTileX = grab.GridGrab[0].X;
            int grabbedTileY = grab.GridGrab[0].Y;
/*            GD.Print("x: " + grabbedTileX + ", y: " + grabbedTileY);
            GD.Print("x: " + swappedY + ", y: " + swappedX);*/
            (grid.Tiles[grabbedTileY][grabbedTileX], grid.Tiles[swappedY][swappedX])
                = (grid.Tiles[swappedY][swappedX], grid.Tiles[grabbedTileY][grabbedTileX]);
            grab.GridGrab.RemoveAt(0);
        }

        // Swap tiles at once
        // ?
    }

    static public void PushGridGrab(Grab grab, Vector2I target)
    {
         grab.GridGrab.Add(target);
    }

    static public void PlaceTile(Grid grid, Grab grab, Inventory inventory, Vector2I target)
    {
        // Place tile one by one
        int placedX = target.X;
        int placedY = target.Y;

        // Check is out of range

        if (placedY < 0 || placedY > grid.Tiles.Count ||
            placedX < 0 || placedX > grid.Tiles[0].Count) return;

        // if tiles are from the inventory
        if (grab.InventoryGrab >= 0)
        {
            GD.Print("Place from Inventory");
            /*sint inventoryIdx = grab.InventoryGrab;
            Tile inventoryTile = null;
            int curIdx = 0;
            foreach (Tile tile in inventory.Tiles)
            {
                if (curIdx > inventoryIdx) break;
                inventoryTile = tile;
                curIdx++;
            }
            grid.Tiles[placedY][placedX] = inventoryTile;
            inventory.Tiles.Remove(inventoryTile);
            grab.InventoryGrab = -1;*/
            SwapTiles(grid, grab, inventory, target);
        }
        else if (grab.GridGrab.Count > 0)
        {
            GD.Print("Place from GridGrab");
/*            int grabbedTileX = grab.GridGrab[0].X;
            int grabbedTileY = grab.GridGrab[0].Y;
            grid.Tiles[placedY][placedX] = grid.Tiles[grabbedTileY][grabbedTileX];
            grab.GridGrab.RemoveAt(0);*/
            SwapTiles(grid, grab, inventory, target);
        }

        // Place tiles at once
        // ?
    }

    static public void PushInventoryGrab(Grab grab, int target)
    {
        grab.InventoryGrab = target;
    }

    static public Godot.Collections.Dictionary LoadJsonMapFiles(in string filePath)
    {
        using var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.Print("Can't find file");
            return null;
        }
        string content = file.GetAsText();
        Godot.Collections.Dictionary jsonMap = (Godot.Collections.Dictionary)Json.ParseString(content);
        return jsonMap;
    }

	static public void PopulateMapWithJson(Grid grid, Inventory inventory, in string filePath)
	{
		Godot.Collections.Dictionary jsonMap =  TileHandler.LoadJsonMapFiles(filePath);
		int gridWidth = (int)jsonMap["width"];
		int gridHeight = (int)jsonMap["height"];
		for (int i = 0; i < gridHeight; ++i)
		{
			List<Tile> newList = new List<Tile>();
			for (int j = 0; j < gridWidth; ++j)
			{
				newList.Add(null);
			}
			grid.Tiles.Add(newList);
		}

		Godot.Collections.Array<Variant> jsonTiles = (Godot.Collections.Array<Variant>)jsonMap["tiles"];

		if (jsonTiles == null)
		{
			GD.PrintErr("\"tiles\" is not properly set");
			return;
		}

		foreach(Variant tileVar  in jsonTiles)
		{
			Godot.Collections.Dictionary jsonTile = (Godot.Collections.Dictionary)tileVar;
			if (jsonTile == null)
			{
				GD.PrintErr("\"tiles\" is not properly set");
				return;
			}

			Tile newTile = new Tile();
			float nodeValue = (float)jsonTile["node"];
			newTile.Node = (NodeType)((int)nodeValue);

			Godot.Collections.Array<Variant> jsonRoad = (Godot.Collections.Array<Variant>)jsonTile["road"];

			List<string> roadList = new List<string>();
			foreach(Variant varRoad in jsonRoad)
			{
				string road = (string)varRoad;
				roadList.Add(road);
			}

			if (roadList.Contains("e"))
			{
				newTile.East = EdgeType.Connected;
			}

			if (roadList.Contains("w"))
			{
				newTile.West = EdgeType.Connected;
			}

			if (roadList.Contains("s"))
			{
				newTile.South = EdgeType.Connected;
			}

			if (roadList.Contains("n"))
			{
				newTile.North = EdgeType.Connected;
			}

			inventory.Tiles.AddLast(newTile);
		}

		GD.Print("Read Map Done");
		GD.Print($"Width: {gridWidth} / Height: {gridHeight}");
		GD.Print($"Add new inventory ${inventory.Tiles.Count} itmes");
		return;
	}
}
