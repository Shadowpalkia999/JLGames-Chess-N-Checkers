using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : GamePiece
{
    private bool firstMove;
    void Start()
    {
        firstMove = true;
        this.setFENCode("p");

        List<int[]> movePath = null;
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,0});
        relativeMoves.Add(movePath);
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{1,0});
        relativeMoves.Add(movePath);
    }


    override public List<List<int[]>> getRelativeMoves()
    {
        //constrain the list of moves based on piece color
        List<int[]> constrainedMoves = new List<int[]>();

        if (getColor().Equals(COLOR_BLACK))
        {
            constrainedMoves.Add(new int[] { 1, 0 });
            if (firstMove)
            {
                constrainedMoves.Add(new int[] { 2, 0 });
            }
        }
        else
        {
            constrainedMoves.Add(new int[]{ -1, 0 });
            if (firstMove)
            {
                constrainedMoves.Add(new int[] { -2, 0 });
            }
        }
        
        List<List<int[]>> relMoves = new List<List<int[]>>();
        relMoves.Add(constrainedMoves);
        return relMoves;
    }
    override public void move(string targetSquare)
    {
        base.move(targetSquare);
        firstMove = false;
    }
}