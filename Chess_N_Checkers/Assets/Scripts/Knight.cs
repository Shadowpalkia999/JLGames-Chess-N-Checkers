using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : GamePiece
{ 
    void Start()
    {
        this.setFENCode("n");

        relativeMoves.Add(new int[]{-2,1});
        relativeMoves.Add(new int[]{-2,-1});
        relativeMoves.Add(new int[]{-1,2});
        relativeMoves.Add(new int[]{-1,-2});
        relativeMoves.Add(new int[]{1,-2});
        relativeMoves.Add(new int[]{1,2});
        relativeMoves.Add(new int[]{2,-1});
        relativeMoves.Add(new int[]{2,1});
    }
}