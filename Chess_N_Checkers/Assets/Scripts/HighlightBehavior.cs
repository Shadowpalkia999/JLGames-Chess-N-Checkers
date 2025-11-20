using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBehavior : MonoBehaviour
{
    private GamePiece piece;
    private string mySquare;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setMoveCallBack(GamePiece selectedPiece)
    {
        piece = selectedPiece;
    }
    public void setSquare(string square)
    {
        mySquare = square;
    }
    public void OnMouseDown()
    {
        UnityEngine.Debug.Log("Highlighted Space Clicked");
        piece.move(mySquare);
    }
}