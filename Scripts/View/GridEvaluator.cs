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
				GD.Print("세로줄: "+grid.Tiles.Count);
				GD.Print("가로줄: "+grid.Tiles[i].Count);

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
		int width = grid.Tiles[0].Count;
		int height = grid.Tiles.Count;
		List<List<Vector2I>> comboList = new List<List<Vector2I>>();
		//List<List<bool>> isNodeChecked = new List<List<bool>>(); // 체크 여부
		bool[,] isNodeChecked = new bool[height,width];
		int curComboGroup = 0; // 현재 몇 번째 콤보그룹 확인 중인가?
		comboList.Add(new List<Vector2I>());

		bool isFinishedChecking = false;

		for (int i=0; i<grid.Tiles.Count; i++)
		{
			//isNodeChecked.Add(new List<bool>());
			for(int j=0; j < grid.Tiles[i].Count; j++)
			{
				//isNodeChecked[i].Add(false);
				isNodeChecked[i,j] = false;

			}
		}

		do
		{
			for (int i = 0; i < grid.Tiles.Count; i++)
			{
				for (int j = 0; j < grid.Tiles[i].Count; j++)
				{
					if (isNodeChecked[i,j]) continue;
					isNodeChecked[i,j] = true;

					List<Vector2I> newCombo = new List<Vector2I>();
					newCombo = BFS(j,i,grid,ref isNodeChecked); // 새로 조사해 온 배열
					//if (comboList[curComboGroup].Contains(newCombo[k])) { newCombo.Remove(newCombo[k]); } //중복은 지워주고
					for (int l = 0; l < newCombo.Count; l++)
					{
						GD.Print(newCombo[l].X + " " + newCombo[l].Y);
					}
					GD.Print("");
					
					if (newCombo.Count<=1)  //더 이상 새로 콤보 추가할 후보가 없다면..
					{
						curComboGroup++; // 다음콤보 체킹으로 넘어가기
						comboList.Add(new List<Vector2I>()); // 다음콤보 리스트 미리 만들어두기
						break;
					}
					else
					{
						for (int l = 0; l < newCombo.Count; l++)
						{
							GD.Print(newCombo[l].X + " " + newCombo[l].Y);
							comboList[curComboGroup].Add(newCombo[l]); //추가된 콤보를 확정 배열로 집어넣기
							isNodeChecked[newCombo[l].Y, newCombo[l].X] = true; //체킹 true로 설정
						}
						GD.Print("");
					}
				}
			}
			for(int i = 0; i < height; i++)
			{
				bool check = false;
				for (int j = 0; j < width; j++)
				{
					if (!isNodeChecked[i, j])
					{
						check = true;
						break;
					}
					isFinishedChecking = true;
				}
				if (check) break;
			}
		} while (!isFinishedChecking);
		GD.Print(curComboGroup);
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

	List<Vector2I> BFS(int x, int y, Grid grid, ref bool[,] isVisited)
	{
		//GD.Print("From " + x + ", " + y);
		List<Vector2I> ComboList = new List<Vector2I>();
		
		int[] dx = { 0, 0, -1, 1 };
		int[] dy = { -1, 1, 0, 0 };
		
		int width = grid.Tiles[0].Count, height = grid.Tiles.Count;

		Queue<Vector2I> q = new Queue<Vector2I>();
		q.Enqueue(new Vector2I(x, y));
        isVisited[y,x] = true;
		ComboList.Add(new Vector2I(x, y));
		while(q.Count > 0)
		{
            Vector2I tuple;
			q.TryDequeue(out tuple);
            Tile curTile = grid.Tiles[tuple.Y][tuple.X];
            for (int i=0; i<4; i++)
			{
                bool check = false;
                int nx = tuple.X + dx[i];
				int ny = tuple.Y + dy[i];
				if(nx>=0 && ny>=0 && nx<width && ny<height && !isVisited[ny,nx])
				{
                    if (i == 0) {
                        if(curTile.North == grid.Tiles[ny][nx].South && curTile.Node == grid.Tiles[ny][nx].Node)
							check = true;
                    }
					else if (i == 1)
					{
                        if (curTile.South == grid.Tiles[ny][nx].North && curTile.Node == grid.Tiles[ny][nx].Node)
                            check = true;
                    }
					else if (i == 2)
					{
						if (curTile.West == grid.Tiles[ny][nx].East && curTile.Node == grid.Tiles[ny][nx].Node)
							check = true;
                    }
					else if (i == 3)
					{
						if (curTile.East == grid.Tiles[ny][nx].West && curTile.Node == grid.Tiles[ny][nx].Node)
							check = true;
                    }

					if (check)
					{
                        //GD.PrintT(nx + " " + ny);
                        //GD.Print(curTile.East + " " + curTile.West + " " + curTile.South + " " + curTile.North);
                        isVisited[ny, nx] = true;
						ComboList.Add(new Vector2I(nx, ny));
                        q.Enqueue(new Vector2I(x, y));
						break;
					}
                }
			}
		}
        return ComboList;
    }
}

