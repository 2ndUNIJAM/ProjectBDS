using Godot;
using System;
using System.Collections.Generic;

public partial class GridEvaluator : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Grid sampleGrid = new Grid();
		//GD.Print("is this working?");
	}
	
	bool EvaluationEdgeCondition(Grid grid) //엣지체킹함수
	{
		for(int i = 0; i<grid.Tiles.Count; i++) 
		{
			for(int j = 0; j<grid.Tiles[i].Count; j++)
			{
				if (grid.Tiles[i][j] == null) //현재 좌표에 타일이 놓여있을 경우
				{
					continue;
				}
				//위쪽 확인
				bool isUpInvalid = i > 0 && grid.Tiles[i][j].North != grid.Tiles[i - 1][j].South;
					
				//아래쪽 확인
				bool isDownInvalid = !(i < grid.Tiles.Count && grid.Tiles[i][j].South!=grid.Tiles[i+1][j].North);

				//왼쪽 확인
				bool isLeftInvalid = !(j > 0 && grid.Tiles[i][j].West != grid.Tiles[i][j - 1].East);
					
				//오른쪽 확인
				bool isRightInvalid = !(j<grid.Tiles[i].Count && grid.Tiles[i][j].East!=grid.Tiles[i][j + 1].West);

				if (isUpInvalid || isDownInvalid || isLeftInvalid || isRightInvalid) 
				{
					return false;
				}
			}
		}
		return true;
	}
	
	int EvaluateScore(Grid grid) //콤보체킹함수
	{
		return 0;
	}
	
	/*
	List EvaluateComboPosition(Grid grid, Vector2I UpdatedPoint)
	{
	}
	*/
	
	void UpdateScoreBoardView()
	{
		
	}

}
