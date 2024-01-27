using Godot;
using System;
using System.Diagnostics;

public partial class InventoryView : Control
{
	[Export]
	PackedScene ItemNode;

	[Export]
	Input InputNode;

	Node ItemListNode;

	public override void _Ready()
	{
		ItemListNode = GetNode<Node>("MarginContainer/ScrollContainer/ItemList");
	}

	public override void _Process(double delta)
	{
	}

	public void UpdateInventoryView(State state, Vector2I _position)
	{
		InternalUpdateInventoryView(state.Inventory);
	}

	private void InternalUpdateInventoryView(Inventory inventory)
	{
		int tileIndex = 0;
		foreach(Tile tile in inventory.Tiles) 
		{
			if(ItemListNode.GetChildCount() <= tileIndex)
			{
				++tileIndex;
				SpawnItem(tile, tileIndex);
				continue;
			}

			InventoryItem item = ItemListNode.GetChild<InventoryItem>(tileIndex);
			item.SetItem(tile, tileIndex);
			item.Visible = true;
			++tileIndex;
		}

		for (int itemIndex = inventory.Tiles.Count; itemIndex < ItemListNode.GetChildCount(); ++itemIndex)
		{
			InventoryItem item = ItemListNode.GetChild<InventoryItem>(itemIndex);
			item.Visible = false;
		}
	}

	void SpawnItem(Tile tile, int tileIndex)
	{
		Debug.Assert(ItemListNode != null, "ItemListNode is null for inventory!");

		InventoryItem newItem = ItemNode.Instantiate<InventoryItem>();
		newItem.SetItem(tile, tileIndex);
		ItemListNode.AddChild(newItem);
		if (InputNode != null)
		{
			newItem.OnItemClick += InputNode.SelectInventoryItem;
		}
	}
}

