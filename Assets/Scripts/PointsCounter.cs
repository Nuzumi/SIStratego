using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointsCounter {

    public static List<List<Vector2Int>>[,] Neighbours { get; set; }


    public static int CheckPoints(Vector2Int position, int state, int[,] gameState)
    {
        int points = 0;
        if (ListAllMarked(Neighbours[position.x,position.y][0],gameState))
        {
            int tmpPoints = CountPoints(gameState, state, position, Neighbours[position.x, position.y][0]);
            if (tmpPoints > 1)
            {
                points += tmpPoints;
            }
        }

        if (ListAllMarked(Neighbours[position.x, position.y][1], gameState))
        {
            int tmpPoints = CountPoints(gameState, state, position, Neighbours[position.x, position.y][1]);
            if (tmpPoints > 1)
            {
                points += tmpPoints;
            }
        }

        if (ListAllMarked(Neighbours[position.x, position.y][2], gameState))
        {
            int tmpPoints = CountPoints(gameState, state, position, Neighbours[position.x, position.y][2]);
            if (tmpPoints > 1)
            {
                points += tmpPoints;
            }
        }

        if (ListAllMarked(Neighbours[position.x, position.y][3], gameState))
        {
            int tmpPoints = CountPoints(gameState,state,position, Neighbours[position.x, position.y][3]);
            if (tmpPoints > 1)
            {
                points += tmpPoints;
            }
        }

        return points;
    }

    public static bool ListAllMarked(List<Vector2Int> positionsList, int[,] gameState)
    {
        foreach (var n in positionsList)
        {
            if (gameState[n.x,n.y] == -1)
            {
                return false;
            }
        }
        return true;
    }

    public static int CountPoints(int[,] gameState, int State, Vector2Int position, List<Vector2Int> positionList)
    {
        int points = 0;
        bool trigerMark = false;
        for (int i = 0; i < positionList.Count; i++)
        {
            if(gameState[positionList[i].x,positionList[i].y] == State){
                points++;

                if(positionList[i].x == position.x && positionList[i].y == position.y)
                {
                    trigerMark = true;
                }
            }
            else
            {
                if (trigerMark)
                {
                    return points;
                }
                else
                {
                    points = 0;
                }
            }
        }

        return points;
    }
}
