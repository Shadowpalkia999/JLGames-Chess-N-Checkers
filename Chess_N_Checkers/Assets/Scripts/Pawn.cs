using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : GamePiece
{
    void Start()
    {
        this.setFENCode("p");

        relativeMoves.Add(new int[]{-1,0});
        relativeMoves.Add(new int[]{1,0});
    }

    override public List<int[]> getRelativeMoves()
    {
        //constrain the list of moves based on piece color
        List<int[]> constrainedMoves = new List<int[]>();

        if (getColor().Equals(COLOR_BLACK))
        {
            constrainedMoves.Add(new int[]{1,0});
        } else
        {
            constrainedMoves.Add(new int[]{-1,0});
        }
        return constrainedMoves;
    }


    private void OnMouseDown()
    {
        UnityEngine.Debug.Log("Pawn Clicked");
        this.highlightMoveTargets();
    }    
}
