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
    /*
     * King has to be on his original square.
     * Are the two squares to the king side empty.
     * Check if Rook is in the next square.
     * No attackers of the King or the empty squares.
    */
    /*
     * King has to be on his original square.
     * Are the three squares to the king side empty.
     * Check if Rook is in the next square.
     * No attackers of the King or the empty squares.
    */
}