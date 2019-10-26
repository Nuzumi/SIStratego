using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinMaxNode {

    public int player0Points;
    public int player1Points;
    public int value;
    
    private int treeDeep;
    private int[,] state;
    private Vector2Int newPositions;
    private int newPositionsState;
    private bool min;
    private List<int> nextLevelValues;
    private List<Vector2Int> moves;

    public MinMaxNode(int[,] stateBefore,int newPositionsState,int players0Points,int player1Points,int treeDeep)
    {
        nextLevelValues = new List<int>();
        moves = new List<Vector2Int>();
        state = stateBefore;
        this.newPositionsState = newPositionsState;
        this.player0Points = players0Points;
        this.player1Points = player1Points;
        this.min = false;
        this.treeDeep = treeDeep;

        CreateChildren(true);
    }

    public MinMaxNode(int[,] stateBefore,Vector2Int newPositions, int newPositionsState,int player0Points,
        int player1Points, bool min, int treeDeep)
    {
        nextLevelValues = new List<int>();
        state = stateBefore;
        this.newPositions = newPositions;
        this.newPositionsState = newPositionsState;
        this.player0Points = player0Points;
        this.player1Points = player1Points;
        this.min = min;
        this.treeDeep = treeDeep;

        GetValueOfCurrentState();
        if(treeDeep == 0 || !IsPossibleMove())
        {
            value = GetValue();
        }
        else
        {
            CreateChildren(false);
            value = GetValueFromChildren();
        }
    }

    private void GetValueOfCurrentState()
    {
        state[newPositions.x, newPositions.y] = newPositionsState;
        if(newPositionsState == 0)
        {
            player0Points += PointsCounter.CheckPoints(newPositions, newPositionsState, state);
        }
        else
        {
            player1Points += PointsCounter.CheckPoints(newPositions, newPositionsState, state);
        }
    }

    private void CreateChildren(bool withPositionsSaved)
    {
        for(int i =0;i<state.GetLength(0);i++)
        {
            for(int j = 0; j < state.GetLength(0); j++)
            {
                if (state[i,j] == -1)
                {
                    if (withPositionsSaved)
                    {
                        moves.Add(new Vector2Int(i, j));
                    }
                    CreateChild(new Vector2Int(i, j));
                }
            }
        }
    }

    private void CreateChild(Vector2Int position)
    {
        int childPositionState;
        if(newPositionsState == 0)
        {
            childPositionState = 1;
        }
        else
        {
            childPositionState = 0;
        }

        int[,] stateToPass = new int[state.GetLength(0), state.GetLength(0)];
        Array.Copy(state, stateToPass, state.Length);
        MinMaxNode child = new MinMaxNode(stateToPass, position, childPositionState, player0Points, player1Points,
            !min, treeDeep - 1);

        nextLevelValues.Add(child.value);
    }

    private int GetValue()
    {
        return player1Points - player0Points;
    }

    private int GetValueFromChildren()
    {
        if (min)
        {
            float minVal = Mathf.Infinity;
            foreach(var v in nextLevelValues)
            {
                if(v < minVal)
                {
                    minVal = v;
                }
            }
            return (int)minVal;
        }
        else
        {
            float maxVal = Mathf.NegativeInfinity;
            foreach (var v in nextLevelValues)
            {
                if (v > maxVal)
                {
                    maxVal = v;
                }
            }
            return (int)maxVal;
        }
    }

    private int GetValueFromChildren(bool min)
    {
        if (min)
        {
            float minVal = Mathf.Infinity;
            foreach (var v in nextLevelValues)
            {
                if (v < minVal)
                {
                    minVal = v;
                }
            }
            return (int)minVal;
        }
        else
        {
            float maxVal = Mathf.NegativeInfinity;
            foreach (var v in nextLevelValues)
            {
                if (v > maxVal)
                {
                    maxVal = v;
                }
            }
            return (int)maxVal;
        }
    }

    public Vector2Int GetBestMove()
    {
        float maxVal = Mathf.NegativeInfinity;
        foreach (var v in nextLevelValues)
        {
            if (v > maxVal)
            {
                maxVal = v;
            }
        }
        List<int> indexList = new List<int>();
        for(int i = 0; i < nextLevelValues.Count; i++)
        {
            if(nextLevelValues[i] == maxVal)
            {
                indexList.Add(i);
            }
        }


        System.Random random = new System.Random();
        int index = indexList[random.Next(indexList.Count)];
        return moves[index];
    }

    private bool IsPossibleMove()
    {
        foreach(int i in state)
        {
            if(i == -1)
            {
                return true;
            }
        }

        return false;
    }

}
