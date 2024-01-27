using Godot;
using System;

public partial class InventoryItem : Control
{
	[Export]
	Texture2D NodeAtlasTexture;

	[Export]
	int AtlasTileSize = 533;

	public void SetItem(Tile tile)
	{
		TextureRect testRect = GetNode<TextureRect>("Left");
		GetNode<TextureRect>("Left").Visible = tile.West == EdgeType.Connected;
		GetNode<TextureRect>("Right").Visible = tile.East == EdgeType.Connected;
		GetNode<TextureRect>("Up").Visible = tile.North == EdgeType.Connected;
		GetNode<TextureRect>("Down").Visible = tile.South == EdgeType.Connected;

		AtlasTexture nodeTexture = new AtlasTexture();
		nodeTexture.Atlas = NodeAtlasTexture;
		int nodeIndex = (int)tile.Node;
		nodeTexture.Region = new Rect2(0, nodeIndex * AtlasTileSize, new Vector2(AtlasTileSize, AtlasTileSize));
		GetNode<TextureRect>("Node").Texture = nodeTexture;
	}
}
