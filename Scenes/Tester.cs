using Godot;
using System;
using System.Collections.Generic;

public partial class Tester : Node
{
	public override void _Ready()
	{
		Grid testGrid = new Grid();

		for (int i = 0; i < 4; i++)
		{
			testGrid.Tiles.Add(new List<Tile>());
		}

        Tile tile1 = new Tile();
        tile1.East = EdgeType.Connected;
        tile1.West = EdgeType.DisConnected;
        tile1.South = EdgeType.Connected;
        tile1.North = EdgeType.DisConnected;
        tile1.Node = NodeType.NodeType1;

        Tile tile2 = new Tile();
        tile2.East = EdgeType.Connected;
        tile2.West = EdgeType.Connected;
        tile2.South = EdgeType.Connected;
        tile2.North = EdgeType.Connected;
        tile2.Node = NodeType.NodeType1;

        Tile tile3 = new Tile();
        tile3.East = EdgeType.DisConnected;
        tile3.West = EdgeType.Connected;
        tile3.South = EdgeType.Connected;
        tile3.North = EdgeType.Connected;
        tile3.Node = NodeType.NodeType1;

        Tile tile4 = new Tile();
        tile4.East = EdgeType.Connected;
        tile4.West = EdgeType.DisConnected;
        tile4.South = EdgeType.DisConnected;
        tile4.North = EdgeType.Connected;
        tile4.Node = NodeType.NodeType1;

        Tile tile5 = new Tile();
        tile5.East = EdgeType.DisConnected;
        tile5.West = EdgeType.DisConnected;
        tile5.South = EdgeType.Connected;
        tile5.North = EdgeType.DisConnected;
        tile5.Node = NodeType.NodeType2;

        Tile tile6 = new Tile();
        tile6.East = EdgeType.Connected;
        tile6.West = EdgeType.DisConnected;
        tile6.South = EdgeType.DisConnected;
        tile6.North = EdgeType.DisConnected;
        tile6.Node = NodeType.NodeType2;

        Tile tile7 = new Tile();
        tile7.East = EdgeType.Connected;
        tile7.West = EdgeType.DisConnected;
        tile7.South = EdgeType.DisConnected;
        tile7.North = EdgeType.DisConnected;
        tile7.Node = NodeType.NodeType2;

        Tile tile8 = new Tile();
        tile8.East = EdgeType.Connected;
        tile8.West = EdgeType.DisConnected;
        tile8.South = EdgeType.DisConnected;
        tile8.North = EdgeType.DisConnected;
        tile8.Node = NodeType.NodeType2;

        Tile tile9 = new Tile();
        tile9.East = EdgeType.DisConnected;
        tile9.West = EdgeType.Connected;
        tile9.South = EdgeType.DisConnected;
        tile9.North = EdgeType.Connected;
        tile9.Node = NodeType.NodeType2;

        Tile tile10 = new Tile();
        tile10.East = EdgeType.DisConnected;
        tile10.West = EdgeType.Connected;
        tile10.South = EdgeType.DisConnected;
        tile10.North = EdgeType.Connected;
        tile10.Node = NodeType.NodeType2;

        testGrid.Tiles[0].Add(tile1);
        testGrid.Tiles[0].Add(tile6);
        testGrid.Tiles[0].Add(tile1);

        testGrid.Tiles[1].Add(tile2);
        testGrid.Tiles[1].Add(tile7);
        testGrid.Tiles[1].Add(tile2);

        testGrid.Tiles[2].Add(tile3);
        testGrid.Tiles[2].Add(tile4);
        testGrid.Tiles[2].Add(tile9);

        testGrid.Tiles[3].Add(tile5);
        testGrid.Tiles[3].Add(tile8);
        testGrid.Tiles[3].Add(tile10);

        GridEvaluator myGridEvaluator = GetNode<GridEvaluator>("../GridEvaluator");
		bool isCorrect = myGridEvaluator.EvaluationEdgeCondition(testGrid);
		GD.Print(isCorrect);

		int comboScore = myGridEvaluator.EvaluateScore(testGrid);
		GD.Print(comboScore);

		

	}

}
