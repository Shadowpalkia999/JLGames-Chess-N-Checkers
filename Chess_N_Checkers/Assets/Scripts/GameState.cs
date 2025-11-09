using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject wKing;
    public GameObject wQueen;
    public GameObject wBishop;
    public GameObject wKnight;
    public GameObject wRook;
    public GameObject wPawn;
    public GameObject bKing;
    public GameObject bQueen;
    public GameObject bBishop;
    public GameObject bKnight;
    public GameObject bRook;
    public GameObject bPawn;

    private float squareSize = 2.35f;
    
    void Start()
    {
        GameObject myKing = Instantiate(wKing, new Vector3((-3.5f * squareSize), 0.5f, (-3.5f * squareSize)), Quaternion.identity);
        myKing.name = "White King";
        GamePiece piece = myKing.GetComponent<GamePiece>();
        piece.setPosition("a1");
        GameObject myQueen = Instantiate(wQueen, new Vector3((3.5f * squareSize), 0.5f, (-3.5f * squareSize)), Quaternion.identity);
        myQueen.name = "White Queen";
        piece = myQueen.GetComponent<GamePiece>();
        piece.setPosition("a8");
    }
    void Update()
    {
        
    }
}