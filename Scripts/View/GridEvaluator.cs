using Godot;
using System;

public partial class GridEvaluator : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Grid sampleGrid = new Grid();
		//GD.Print("is this working?");
	}
	
	bool EvaluationEdgeCondition(Grid grid)
	{
		for(int i=0; i<grid.Count; i++) //
		{
			for(int j=0; j<grid[i].Count; j++)
			{
				if(grid[i][j]!=null) //현재 좌표에 타일이 놓여있을 경우
				{
					//왼쪽 확인
					if(i>0) //맨 왼쪽이 아니라면
					{
						if(grid[i][j].West!=grid[i-1][j].East) return false;
					}	
					
					//오른쪽 확인
					if(i<grid.Count) //맨 오른쪽이 아니라면
					{
						if(grid[i][j].East!=grid[i+1][j].West) return false;
					}
					
					//위쪽 확인
					if(j>0) //맨 위쪽이 아니라면
					{
						if(grid[i][j].North!=grid[i][j-1].South) return false;
					}
					
					//아래쪽 확인
					if(j<grid[i].Count)//맨 아래쪽이 아니라면
					{
						if(grid[i][j].South!=grid[i][j+1].North) return false;
					}
				}	
			}
		}
		
	}
	
	int EvaluateScore(Grid grid)
	{
		
	}
	
	List EvaluateComboPosition(Grid grid, Vector2I UpdatedPoint)
	{
		
	}
	
	UpdateScoreBoardView()
	{
		
	}

}
