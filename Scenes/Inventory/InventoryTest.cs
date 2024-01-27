using Godot;
using System;

public partial class InventoryTest : Node
{
	Inventory inventory;

	public override void _Ready()
	{
		inventory = new Inventory();

		GetNode<Timer>("Timer").Connect("timeout", new Callable(this, "AddItem"));
	}

	public void AddItem()
	{
		inventory.Tiles.AddLast(new Tile());
		InventoryView inventoryView = GetNode<InventoryView>("..");
		//inventoryView.UpdateInventoryView(inventory);
	}
}
