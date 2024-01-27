using Godot;
using System;

public partial class TileHandler : GodotObject
{
    public void RemoveTile(Grid grid, Grab grab, Inventory inventory)
    {
        if (grab.GridGrab.Count > 0)
        {
            foreach (Vector2I targetTile in grab.GridGrab)
            {
                int x = targetTile.X;
                int y = targetTile.Y;
                inventory.Tiles.AddLast(grid.Tiles[y][x]);
                grid.Tiles[y][x] = null;
            }
        }
    }

    public void SwapTiles(Grid grid, Grab grab, Inventory inventory, Vector2I target)
    {
        // Swap tile one by one
        int swappedX = target.X;
        int swappedY = target.Y;
        // if tiles are from the inventory
        if (grab.InventoryGrab>=0)
        {
            //GD.Print("Swap from Inventory");
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
            inventory.Tiles.AddLast(tempTile);
        }
        else if(grab.GridGrab.Count>0)
        {
            //GD.Print("Swap from GridGrab");
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

    public void PushGridGrab(Grab grab, Vector2I target)
    {
         grab.GridGrab.Add(target);
    }

    public void PlaceTile(Grid grid, Grab grab, Inventory inventory, Vector2I target)
    {
        // Place tile one by one
        int placedX = target.X;
        int placedY = target.Y;
        // if tiles are from the inventory
        if (grab.InventoryGrab >= 0)
        {
            //GD.Print("Place from Inventory");
            int inventoryIdx = grab.InventoryGrab;
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
            grab.InventoryGrab = -1;
        }
        else if (grab.GridGrab.Count > 0)
        {
            //GD.Print("Place from GridGrab");
            SwapTiles(grid, grab, inventory, target);
        }

        // Place tiles at once
        // ?
    }

    public void PushInventoryGrab(Grab grab, int target)
    {
        grab.InventoryGrab = target;
    }

}