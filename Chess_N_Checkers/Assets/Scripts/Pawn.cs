using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : GamePiece
{
    void Start()
    {
        this.setFENCode("p");
    }
}
