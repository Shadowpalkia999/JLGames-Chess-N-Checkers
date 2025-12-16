using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : GamePiece
{ 
    void Start()
    {
        this.setFENCode("n");

        List<int[]> movePath = null;
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{-2,1});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{-2,-1});
        relativeMoves.Add(movePath);
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,2});
        relativeMoves.Add(movePath);
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,-2});
        relativeMoves.Add(movePath);
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{1,-2});
        relativeMoves.Add(movePath);
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{1,2});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{2,-1});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{2,1});
        relativeMoves.Add(movePath);        
    }
}