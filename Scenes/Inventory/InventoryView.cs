using Godot;
using System;
using System.Diagnostics;

public partial class InventoryView : Control
{
	[Export]
	PackedScene ItemNode;

	Node ItemListNode;

	public override void _Ready()
	{
		ItemListNode = GetNode<Node>("ScrollContainer/ItemList");
	}

	public override void _Process(double delta)
	{
	}

	public void UpdateInventoryView(Inventory inventory)
	{
		int tileIndex = 0;
		foreach(Tile tile in inventory.Tiles) 
		{
			if(ItemListNode.GetChildCount() <= tileIndex)
			{
				++tileIndex;
				SpawnItem(tile);
				continue;
			}

			InventoryItem item = ItemListNode.GetChild<InventoryItem>(tileIndex);
			item.SetItem(tile);
			item.Visible = true;
			++tileIndex;
		}

		for (int itemIndex = inventory.Tiles.Count; itemIndex < ItemListNode.GetChildCount(); ++itemIndex)
		{
			InventoryItem item = ItemListNode.GetChild<InventoryItem>(itemIndex);
			item.Visible = false;
		}
	}

	void SpawnItem(Tile tile)
	{
		Debug.Assert(ItemListNode != null, "ItemListNode is null for inventory!");

		InventoryItem newItem = ItemNode.Instantiate<InventoryItem>();
		newItem.SetItem(tile);
		ItemListNode.AddChild(newItem);
	}
}

