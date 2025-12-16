using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : GamePiece
{
    void Start()
    {
        this.setFENCode("q");

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

        relativeMoves.Add(new int[]{0,-7});
        relativeMoves.Add(new int[]{0,-6});
        relativeMoves.Add(new int[]{0,-5});
        relativeMoves.Add(new int[]{0,-4});
        relativeMoves.Add(new int[]{0,-3});
        relativeMoves.Add(new int[]{0,-2});
        relativeMoves.Add(new int[]{0,-1});

        relativeMoves.Add(new int[]{0,7});
        relativeMoves.Add(new int[]{0,6});
        relativeMoves.Add(new int[]{0,5});
        relativeMoves.Add(new int[]{0,4});
        relativeMoves.Add(new int[]{0,3});
        relativeMoves.Add(new int[]{0,2});
        relativeMoves.Add(new int[]{0,1});

        relativeMoves.Add(new int[]{7,0});
        relativeMoves.Add(new int[]{6,0});
        relativeMoves.Add(new int[]{5,0});
        relativeMoves.Add(new int[]{4,0});
        relativeMoves.Add(new int[]{3,0});
        relativeMoves.Add(new int[]{2,0});
        relativeMoves.Add(new int[]{1,0});

        relativeMoves.Add(new int[]{-7,0});
        relativeMoves.Add(new int[]{-6,0});
        relativeMoves.Add(new int[]{-5,0});
        relativeMoves.Add(new int[]{-4,0});
        relativeMoves.Add(new int[]{-3,0});
        relativeMoves.Add(new int[]{-2,0});
        relativeMoves.Add(new int[]{-1,0});
    }
}