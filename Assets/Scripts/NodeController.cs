using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{

    
    public GameObject nodePrefab;
    public GameController gameController;

    private int matrixSize;
    private Node[,] gameBoard;

    private void DisplayNodes()
    {
        Vector2 screenMin = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        Vector2 screenMax = (Vector2)Camera.main.ScreenToWorldPoint(
            new Vector3(Camera.main.scaledPixelHeight, Camera.main.scaledPixelHeight));

        float distanceX = (new Vector2(screenMin.x, 0) - new Vector2(screenMax.x, 0)).magnitude;
        float distanceY = (new Vector2(0, screenMin.y) - new Vector2(0, screenMax.y)).magnitude;
        distanceX /= (matrixSize + 1);
        distanceY /= (matrixSize + 1);
        Vector2 leftUpperCorner = new Vector2(screenMin.x, screenMax.y);

        for (int i =0;i<matrixSize; i++)
        {
            for(int j = 0; j < matrixSize; j++)
            {
                var nodeInstance = Instantiate(nodePrefab,
                    new Vector2(
                        leftUpperCorner.x + (j + 1) * distanceX,
                        leftUpperCorner.y - (i + 1) * distanceY),
                    Quaternion.identity);
                gameBoard[i, j] = nodeInstance.GetComponent<Node>();
                gameBoard[i, j].GameController = gameController;
                gameBoard[i, j].Position = new Vector2Int(i, j);
            }
        }
    }

    private void AssignNeighbour()
    {
        PointsCounter.Neighbours = new List<List<Vector2Int>>[matrixSize, matrixSize];

        for(int i = 0; i < matrixSize; i++)
        {
            for(int j = 0; j < matrixSize; j++)
            {
                PointsCounter.Neighbours[i, j] = new List<List<Vector2Int>>
                {
                    GetHorizontalNeighbours(i, j),
                    GetVerticalNeighbours(i, j),
                    GetDiagonalLeftNeighbours(i,j),
                    GetDiagonalRightNeighbours(i,j)
                };
            }
        }
    }

    private List<Vector2Int> GetVerticalNeighbours(int row, int column)
    {
        var result = new List<Vector2Int>();
        for(int i = 0; i < matrixSize; i++)
        {
            result.Add(new Vector2Int(i, column));
        }
        return result;
    }

    private List<Vector2Int> GetHorizontalNeighbours(int row,int column)
    {
        var result = new List<Vector2Int>();
        for (int i = 0; i < matrixSize; i++)
        {
            result.Add(new Vector2Int(row, i));
        }
        return result;
    }

    private List<Vector2Int> GetDiagonalLeftNeighbours(int row, int column)
    {
        var result = new List<Vector2Int>();
        if ((row == 0 && column == matrixSize - 1) || (row == matrixSize - 1 && column == 0))
            return result;
        
        int rowI = row;
        int columnI = column;
        while(rowI != 0 && columnI != 0)
        {
            rowI--;
            columnI--;
        }

        while (rowI < matrixSize && columnI < matrixSize)
        {
            result.Add(new Vector2Int(rowI, columnI));
            rowI++;
            columnI++;
        }

        return result;
    }

    private List<Vector2Int> GetDiagonalRightNeighbours(int row, int column)
    {
        var result = new List<Vector2Int>();
        if ((row == 0 && column == 0) || (row == matrixSize - 1 && column == matrixSize - 1))
            return result;

        int rowI = row;
        int columnI = column;
        while (rowI != 0 && columnI != matrixSize-1)
        {
            rowI--;
            columnI++;
        }

        while (rowI < matrixSize && columnI > -1)
        {
            result.Add(new Vector2Int(rowI, columnI));
            rowI++;
            columnI--;
        }

        return result;
    }

    public void Generate()
    {
        gameBoard = new Node[matrixSize, matrixSize];
        DisplayNodes();
        AssignNeighbour();
        gameController.gameBoard = gameBoard;
    }

    public void SetSize(string size)
    {
        matrixSize = int.Parse(size);
    }
}


