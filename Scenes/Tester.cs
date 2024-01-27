using Godot;
using System;
using System.Collections.Generic;

public partial class Tester : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("log here");

		Grid testGrid = new Grid();
		GD.Print("this here");

		for (int i = 0; i < 3; i++)
		{
			testGrid.Tiles.Add(new List<Tile>());
			GD.Print("for index "+i);
		}


		Tile tile1 = new Tile();
		tile1.East = EdgeType.Connected;
		tile1.West = EdgeType.DisConnected;
		tile1.South = EdgeType.DisConnected;
		tile1.North = EdgeType.DisConnected;
		tile1.Node = NodeType.NodeType1;
		testGrid.Tiles[0].Add(tile1);
		testGrid.Tiles[0].Add(null);

		Tile tile2 = new Tile();
		tile2.East = EdgeType.Connected;
		tile2.West = EdgeType.Connected;
		tile2.South = EdgeType.DisConnected;
		tile2.North = EdgeType.DisConnected;
		tile2.Node = NodeType.NodeType1;
		testGrid.Tiles[1].Add(tile2);
		Tile tile3 = new Tile();
		tile3.East = EdgeType.Connected;
		tile3.West = EdgeType.DisConnected;
		tile3.South = EdgeType.DisConnected;
		tile3.North = EdgeType.DisConnected;
		tile3.Node = NodeType.NodeType2;
		testGrid.Tiles[1].Add(tile3);

		testGrid.Tiles[2].Add(null);
		Tile tile4 = new Tile();
		tile4.East = EdgeType.Connected;
		tile4.West = EdgeType.DisConnected;
		tile4.South = EdgeType.DisConnected;
		tile4.North = EdgeType.Connected;
		tile4.Node = NodeType.NodeType2;
		testGrid.Tiles[2].Add(tile4);

		GD.Print("here");

		GridEvaluator myGridEvaluator = GetNode<GridEvaluator>("../grid_evaluator");
		bool isCorrect = myGridEvaluator.EvaluationEdgeCondition(testGrid);
		GD.Print(isCorrect);

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
