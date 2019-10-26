using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ActualPlayer",menuName ="ScriptableObjects")]
public class ActualPlayer : ScriptableObject {

    public int PlayerNumber;

    [SerializeField]
    private Color Player0Color;

    [SerializeField]
    private Color Player1Color;

    public Color ActualColor
    {
        get
        {
            if (Value == 0)
                return Player0Color;
            else
                return Player1Color;
        }
    }

    public void NextPlayer()
    {
        if (Value == 0)
            Value = 1;
        else
            Value = 0;
    }


#if UNITY_EDITOR
    public int Value
    {
        get { return keepPlaymodeChanges ? PlayerNumber : savedValue; }
        set
        {
            if (keepPlaymodeChanges)
                this.PlayerNumber = value;
            else
                savedValue = value;
        }
    }

    [SerializeField]
    private bool keepPlaymodeChanges;

    [SerializeField, HideInInspector]
    private int savedValue;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
        savedValue = PlayerNumber;
    }

#else
    public int Value {
        get { return PlayerNumber; }
        set {
            this.PlayerNumber = value;
        }
    }

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
#endif
}

