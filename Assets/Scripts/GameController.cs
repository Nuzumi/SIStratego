using UnityEngine;
using System.Diagnostics;
using System.Text;

public class GameController : MonoBehaviour {
    
    public UIController uIController;
    public ActualPlayer actualPlayer;
    public Node[,] gameBoard;
    

    private int player0Points;
    public int Player0Points
    {
        get { return player0Points; }
        set
        {
            player0Points = value;
            uIController.UpdatePlayerPoints(0, value);
        }
    }

    private int player1Points;
    public int Player1Points
    {
        get { return player1Points; }
        set
        {
            player1Points = value;
            uIController.UpdatePlayerPoints(1, value);
        }
    }

    private bool minMax = true;
    private int treeDeep = 3;
    private StringBuilder stringBuilder;

    private void Start()
    {
        stringBuilder = new StringBuilder();
    }

    public void MakeMove()
    {
        if (IsPosibleMove())
        {
            Stopwatch sw = new Stopwatch();
            actualPlayer.NextPlayer();
            int state = actualPlayer.Value;
            Vector2Int bestMove;
            sw.Start();
            if (minMax)
            {
                MinMaxNode firstNode = new MinMaxNode(this.GameStateToInt(), state, player0Points, player1Points, treeDeep);
                bestMove = firstNode.GetBestMove();
            }
            else
            {
                AlfaBetaNode firstNode = new AlfaBetaNode(this.GameStateToInt(), state, player0Points, player1Points, treeDeep);
                bestMove = firstNode.GetBestMove();
            }
            
            sw.Stop();
            stringBuilder.Append(sw.Elapsed.TotalMilliseconds + "\n");

            UnityEngine.Debug.Log(stringBuilder.ToString());
            actualPlayer.NextPlayer();
            gameBoard[bestMove.x, bestMove.y].MakeMove();
        }
    }

    public int[,] GameStateToInt()
    {
        var result = new int[gameBoard.GetLength(0), gameBoard.GetLength(0)];

        for(int i = 0; i < gameBoard.GetLength(0); i++)
        {
            for(int j = 0; j < gameBoard.GetLength(0); j++)
            {
                if(gameBoard[i,j].State.HasValue)
                {
                    result[i, j] = gameBoard[i, j].State.Value;
                }
                else
                {
                    result[i, j] = -1;
                }
                
            }
        }

        return result;
    }

    private bool IsPosibleMove()
    {
        foreach(var n in gameBoard)
            if (!n.State.HasValue)
                return true;

        return false;
    }

    public void ChangeComputerState(bool off)
    {
        foreach(var v in gameBoard)
        {
            v.ComputerState = off;
        }
    }

    public void ChangeState(bool state)
    {
        minMax = state;
    }

    public void EndInputingTreeDeep(string treeD)
    {
        treeDeep = int.Parse(treeD);
    }

}
