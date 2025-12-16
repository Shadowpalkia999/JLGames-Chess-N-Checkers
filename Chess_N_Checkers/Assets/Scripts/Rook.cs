using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : GamePiece
{   // Start is called before the first frame update
    void Start()
    {
        this.setFENCode("r");

        List<int[]> movePath = null;
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{0,-1});
        movePath.Add(new int[]{0,-2});
        movePath.Add(new int[]{0,-3});
        movePath.Add(new int[]{0,-4});
        movePath.Add(new int[]{0,-5});
        movePath.Add(new int[]{0,-6});
        movePath.Add(new int[]{0,-7});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{0,1});
        movePath.Add(new int[]{0,2});
        movePath.Add(new int[]{0,3});
        movePath.Add(new int[]{0,4});
        movePath.Add(new int[]{0,5});
        movePath.Add(new int[]{0,6});
        movePath.Add(new int[]{0,7});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{1,0});
        movePath.Add(new int[]{2,0});
        movePath.Add(new int[]{3,0});
        movePath.Add(new int[]{4,0});
        movePath.Add(new int[]{5,0});
        movePath.Add(new int[]{6,0});
        movePath.Add(new int[]{7,0});
        relativeMoves.Add(movePath);

        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,0});
        movePath.Add(new int[]{-2,0});
        movePath.Add(new int[]{-3,0});
        movePath.Add(new int[]{-4,0});
        movePath.Add(new int[]{-5,0});
        movePath.Add(new int[]{-6,0});
        movePath.Add(new int[]{-7,0});
        relativeMoves.Add(movePath);
    }
}