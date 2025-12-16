using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : GamePiece
{
    void Start()
    {
        this.setFENCode("k");

        relativeMoves.Add(new int[]{0,1});
        relativeMoves.Add(new int[]{0,-1});
        relativeMoves.Add(new int[]{-1,0});
        relativeMoves.Add(new int[]{-1,-1});
        relativeMoves.Add(new int[]{-1,1});
        relativeMoves.Add(new int[]{1,0});
        relativeMoves.Add(new int[]{1,-1});
        relativeMoves.Add(new int[]{1,1});
    }
}