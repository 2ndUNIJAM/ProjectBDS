using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
				grid.Tiles[y][x] = null;
				grab.GridGrab.RemoveAt(0);
			}
			grab.GridGrab.Clear();
		}
	}

	static public void SwapTiles(Grid grid, Grab grab, Inventory inventory, Vector2I target)
	{
		// Swap tile one by one
		int swappedX = target.X;
		int swappedY = target.Y;

		if (swappedY < 0 || swappedY >= grid.Tiles.Count ||
			swappedX < 0 || swappedX >= grid.Tiles[0].Count)
		{
			GD.Print("Swap Pos is out of Range");
			if (grab.GridGrab.Count > 0)
				grab.GridGrab.Clear();
			grab.InventoryGrab = -1;    
			return;
		}
		// if tiles are from the inventory
		if (grab.InventoryGrab>=0)
		{
			GD.Print("Swap from Inventory");
			int inventoryIdx = grab.InventoryGrab;
			Tile tempTile = grid.Tiles[swappedY][swappedX];
			Tile inventoryTile = inventory.Tiles.ElementAt<Tile>(inventoryIdx);
			GD.Print("Swapped Tile: " + inventoryTile.Node);
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

			(grid.Tiles[grabbedTileY][grabbedTileX], grid.Tiles[swappedY][swappedX])
				= (grid.Tiles[swappedY][swappedX], grid.Tiles[grabbedTileY][grabbedTileX]);

			grab.GridGrab.RemoveAt(0);
		}
	}

	static public void PushGridGrab(State state, Vector2I target)
	{
		Grid grid = state.Grid;
		int x = target.X;
		int y = target.Y;
		if (x < 0 || x >= grid.Tiles[0].Count || y < 0 || y >= grid.Tiles.Count) return;
		InternalGridGrab(state.Grab, target);
	}

	static private void InternalGridGrab(Grab grab, Vector2I target)
	{
		grab.GridGrab.Add(target);
	}

	static public void PlaceTile(Grid grid, Grab grab, Inventory inventory, Vector2I target)
	{
		int placedX = target.X;
		int placedY = target.Y;

		// Check is out of range
		if (placedY < 0 || placedY >= grid.Tiles.Count ||
			placedX < 0 || placedX >= grid.Tiles[0].Count)  
		{
			GD.Print("Target Pos is out of Range");
			if(grab.GridGrab.Count>0)
				grab.GridGrab.Clear();
			grab.InventoryGrab = -1;
			return;
		}

		// if tiles are from the inventory
		if (grab.InventoryGrab >= 0)
		{
			GD.Print("Place from Inventory");
			SwapTiles(grid, grab, inventory, target);
		}
		else if (grab.GridGrab.Count > 0)
		{
			GD.Print("Place from GridGrab");
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

	static public int PopulateMapWithJson(Grid grid, Inventory inventory, in string filePath)
	{
		Godot.Collections.Dictionary jsonMap = LoadJsonMapFiles(filePath);
		int gridWidth = (int)jsonMap["width"];
		int gridHeight = (int)jsonMap["height"];
		int maxScore = (int)jsonMap["max"];
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
			return maxScore;
		}

		foreach(Variant tileVar  in jsonTiles)
		{
			Godot.Collections.Dictionary jsonTile = (Godot.Collections.Dictionary)tileVar;
			if (jsonTile == null)
			{
				GD.PrintErr("\"tiles\" is not properly set");
				return maxScore;
			}

			Tile newTile = new Tile();
			float nodeValue = (float)jsonTile["node"];
			newTile.Node = (NodeType)((int)nodeValue - 1);

			switch (nodeValue)
			{
				case 1:
					newTile.atlas_coord = new Vector2I(0, 0);
					break;
				case 2:
					newTile.atlas_coord = new Vector2I(0, 1);
					break;
				case 3:
					newTile.atlas_coord = new Vector2I(0, 2);
					break;
				default:
					break;
			}

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
		return maxScore;
	}

	static public void AddInventoryGrab(Grab grab, int index)
	{
		grab.InventoryGrab = index;
	}
}
