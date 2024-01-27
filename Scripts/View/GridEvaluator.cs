using Godot;
using System;
using System.Collections.Generic;

public partial class GridEvaluator : Node
{
	int[] dx = { 0, 0, -1, 1 };
	int[] dy = { -1, 1, 0, 0 };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}
	
	public bool EvaluationEdgeCondition(Grid grid) //엣지체킹함수
	{
		for(int i = 0; i<grid.Tiles.Count; i++) 
		{
			for(int j = 0; j<grid.Tiles[i].Count; j++)
			{
				GD.Print("가로줄: "+grid.Tiles.Count);
				GD.Print("세로줄: "+grid.Tiles[i].Count);

				if (grid.Tiles[i][j] == null) //현재 좌표에 타일이 놓여있을 경우
				{
					continue;
				}
				//위쪽 확인
				bool isUpInvalid = !(i > 0 && grid.Tiles[i][j].North != grid.Tiles[i - 1][j].South);
				GD.Print(isUpInvalid);
				//아래쪽 확인
				bool isDownInvalid = !(i < grid.Tiles.Count && grid.Tiles[i][j].South != grid.Tiles[i + 1][j].North);
				GD.Print(isDownInvalid + " and j is " + j);
				//왼쪽 확인
				bool isLeftInvalid = !(j > 0 && grid.Tiles[i][j].West != grid.Tiles[i][j - 1].East);
				GD.Print(isLeftInvalid);
				//오른쪽 확인
				bool isRightInvalid = !(j < grid.Tiles[i].Count && grid.Tiles[i][j].East != grid.Tiles[i][j + 1].West);
				GD.Print(isRightInvalid);

				if (isUpInvalid || isDownInvalid || isLeftInvalid || isRightInvalid) 
				{
					return false;
				}
			}
		}
		return true;
	}

	public int EvaluateScore(Grid grid) //콤보체킹함수
	{
		List<List<Vector2I>> comboList = new List<List<Vector2I>>();
		List<List<bool>> isNodeChecked = new List<List<bool>>(); // 체크 여부
		
		int curComboGroup = 0; // 현재 몇 번째 콤보그룹 확인 중인가?
		comboList.Add(new List<Vector2I>());

		bool isFinishedChecking = false;

		for (int i=0; i<grid.Tiles.Count; i++)
		{
			isNodeChecked.Add(new List<bool>());
			for(int j=0; j < grid.Tiles[i].Count; j++)
			{
				isNodeChecked[i].Add(false);
			}
		}

		do
		{
			for (int i = 0; i < grid.Tiles.Count; i++)
			{
				for (int j = 0; j < grid.Tiles[i].Count; j++)
				{
					if (isNodeChecked[i][j]) continue;
					isNodeChecked[i][j] = true;

					List<Vector2I> newCombo = new List<Vector2I>();
					newCombo = EvaluateEdgeForCombo(grid, new Vector2I(i, j)); // 새로 조사해 온 배열
					for (int k = 0; k < newCombo.Count; k++)
					{
						if (comboList[curComboGroup].Contains(newCombo[k])) { newCombo.Remove(newCombo[k]); } //중복은 지워주고
						if (newCombo == null)  //더 이상 새로 콤보 추가할 후보가 없다면..
						{
							curComboGroup++; // 다음콤보 체킹으로 넘어가기
							comboList.Add(new List<Vector2I>()); // 다음콤보 리스트 미리 만들어두기
							break;
						}
						else
						{
							for (int l = 0; l < newCombo.Count; l++)
							{
								comboList[curComboGroup].Add(newCombo[l]); //추가된 콤보를 확정 배열로 집어넣기
								isNodeChecked[newCombo[l].X][newCombo[l].Y] = true; //체킹 true로 설정
							}

						}

					}
				}
			}

			for(int i = 0; i < isNodeChecked.Count; i++)
			{
				if (isNodeChecked[i].Contains(false)) break;
				isFinishedChecking = true;
			}
		} while (!isFinishedChecking);

		int FinalScore = 500;

		return FinalScore;

	}

	private List<Vector2I> EvaluateEdgeForCombo(Grid grid, Vector2I nodePos)
	{
		List<Vector2I> SurroundingComboTiles = new List<Vector2I>();
		bool[] isCombo = {false, false, false, false};
		Vector2I[] surroundPos = { };
		for(int i = 0; i < 4; i++)
		{
			int nx = nodePos.X + dx[i];
			int ny = nodePos.Y + dy[i];
			if (!(nx >= 0 && ny >= 0 && nx < grid.Tiles[0].Count && ny < grid.Tiles.Count)) continue;
			Tile curTile = grid.Tiles[ny][nx];
			switch (i)
			{
				case 0: //상
					isCombo[i] = curTile.North == grid.Tiles[ny - 1][nx].South && curTile.Node == grid.Tiles[ny - 1][nx].Node;
					break;

				case 1: //하
					isCombo[i] = curTile.South == grid.Tiles[ny + 1][nx].North && curTile.Node == grid.Tiles[ny + 1][nx].Node;
					break;

				case 2: //좌
					isCombo[i] = curTile.West == grid.Tiles[ny][nx - 1].East && curTile.Node == grid.Tiles[ny][nx - 1].Node;
					break;

				case 3: //우
					isCombo[i] = curTile.East == grid.Tiles[ny][nx + 1].West && curTile.Node == grid.Tiles[ny][nx + 1].Node;
					break;

				default:
					break;

			}
			
		}
		for(int i=0; i<4; i++)
		{
			if (isCombo[i])
			{
				SurroundingComboTiles.Add(surroundPos[i]);
			}
		}

		return SurroundingComboTiles;
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
