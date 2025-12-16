using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : GamePiece
{
    void Start()
    {
        this.setFENCode("b");

        relativeMoves.Add(new int[]{-7,-7});
        relativeMoves.Add(new int[]{-6,-6});
        relativeMoves.Add(new int[]{-5,-5});
        relativeMoves.Add(new int[]{-4,-4});
        relativeMoves.Add(new int[]{-3,-3});
        relativeMoves.Add(new int[]{-2,-2});
        relativeMoves.Add(new int[]{-1,-1});

        relativeMoves.Add(new int[]{-7,7});
        relativeMoves.Add(new int[]{-6,6});
        relativeMoves.Add(new int[]{-5,5});
        relativeMoves.Add(new int[]{-4,4});
        relativeMoves.Add(new int[]{-3,3});
        relativeMoves.Add(new int[]{-2,2});
        relativeMoves.Add(new int[]{-1,1});

        relativeMoves.Add(new int[]{7,-7});
        relativeMoves.Add(new int[]{6,-6});
        relativeMoves.Add(new int[]{5,-5});
        relativeMoves.Add(new int[]{4,-4});
        relativeMoves.Add(new int[]{3,-3});
        relativeMoves.Add(new int[]{2,-2});
        relativeMoves.Add(new int[]{1,-1});

        relativeMoves.Add(new int[]{7,7});
        relativeMoves.Add(new int[]{6,6});
        relativeMoves.Add(new int[]{5,5});
        relativeMoves.Add(new int[]{4,4});
        relativeMoves.Add(new int[]{3,3});
        relativeMoves.Add(new int[]{2,2});
        relativeMoves.Add(new int[]{1,1});
    }
}