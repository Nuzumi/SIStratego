using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour {

    public ActualPlayer actualPlayer;

    public int? State { get; set; }
    public Vector2Int Position { get; set; }
    public GameController GameController { get; set; }

    private SpriteRenderer xSpriteRenderer;
    public bool ComputerState { get; set; }

    private void Start()
    {
        State = null;
        xSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(State == null)
        {
            MakeMove();

            if (ComputerState)
            {
                GameController.MakeMove();
            }
        }
    }

    public void MakeMove()
    {
        State = actualPlayer.Value;
        xSpriteRenderer.color = actualPlayer.ActualColor;
        SetPoints();
        actualPlayer.NextPlayer();
    }

    private void SetPoints()
    {
        int points = PointsCounter.CheckPoints(Position, (int)State, GameController.GameStateToInt());
        AssignPoints(points);
    }

    private void AssignPoints(int points)
    {
        if (State == 0)
            GameController.Player0Points += points;
        else
            GameController.Player1Points += points;
    }
}
