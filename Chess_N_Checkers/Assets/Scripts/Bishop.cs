using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : GamePiece
{
    void Start()
    {
        this.setFENCode("b");

        List<int[]> movePath = null;
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,-1});
        movePath.Add(new int[]{-2,-2});
        movePath.Add(new int[]{-3,-3});
        movePath.Add(new int[]{-4,-4});
        movePath.Add(new int[]{-5,-5});
        movePath.Add(new int[]{-6,-6});
        movePath.Add(new int[]{-7,-7});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,1});
        movePath.Add(new int[]{-2,2});
        movePath.Add(new int[]{-3,3});
        movePath.Add(new int[]{-4,4});
        movePath.Add(new int[]{-5,5});
        movePath.Add(new int[]{-6,6});
        movePath.Add(new int[]{-7,7});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{1,-1});
        movePath.Add(new int[]{2,-2});
        movePath.Add(new int[]{3,-3});
        movePath.Add(new int[]{4,-4});
        movePath.Add(new int[]{5,-5});
        movePath.Add(new int[]{6,-6});
        movePath.Add(new int[]{7,-7});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{1,1});
        movePath.Add(new int[]{2,2});
        movePath.Add(new int[]{3,3});
        movePath.Add(new int[]{4,4});
        movePath.Add(new int[]{5,5});
        movePath.Add(new int[]{6,6});
        movePath.Add(new int[]{7,7});
        relativeMoves.Add(movePath);
    }
}