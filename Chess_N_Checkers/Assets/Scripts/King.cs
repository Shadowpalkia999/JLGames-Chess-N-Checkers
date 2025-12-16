using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : GamePiece
{
    void Start()
    {
        this.setFENCode("k");

        List<int[]> movePath = null;
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{0,1});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{0,-1});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,0});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,-1});
        relativeMoves.Add(movePath); 
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,1});
        relativeMoves.Add(movePath); 
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{1,0});
        relativeMoves.Add(movePath); 
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{1,-1});
        relativeMoves.Add(movePath); 
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{1,1});
        relativeMoves.Add(movePath);    
    }
}