using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text Player0Points;
    public Text Player1Points;

    public void UpdatePlayerPoints(int player, int points)
    {
        if(player == 0)
        {
            Player0Points.text = points.ToString();
        }
        else
        {
            Player1Points.text = points.ToString();
        }
    }
}
