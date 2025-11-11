using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : GamePiece
{
    void Start()
    {
        this.setFENCode("k");      
    }
    private void OnMouseDown()
    {
        Debug.Log("King Clicked");
        string[] targetSquares = new string[]{"a4", "a6", "b4", "b5", "b6"};
        this.highlightMoveTargets(targetSquares);
    }
}