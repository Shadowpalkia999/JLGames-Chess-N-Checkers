using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public static string COLOR_WHITE = "w";
    public static string COLOR_BLACK = "b";
    private string color;
    private string FENCode = "";

    private List<GameObject> highlights = new List<GameObject>();

    public GameObject highlight;

    public virtual string getFENCode()
    {
        if (color.Equals(COLOR_BLACK))
        {
            return FENCode.ToLower();
        }

        return FENCode.ToUpper();
    }
    public void setFENCode(string code)
    {
        FENCode = code;
    }

    public string getColor()
    {
        return color;
    }

    public void setColor(string color)
    {
        this.color = color;
        if (color.Equals(COLOR_BLACK))
        {
            setFENCode(getFENCode().ToLower());
        } else
        {
            setFENCode(getFENCode().ToUpper());
        }
    }

    public void setPosition(string position)
    {
        Debug.Log("My position is: " + position);
    }
    public void highlightMoveTargets(string[] targetSquares)
    {
        foreach (string targetSquare in targetSquares)
        {
            GameObject highlightedSquare = Instantiate(highlight, GameState.coordinates(targetSquare), Quaternion.identity);
            highlights.Add(highlightedSquare);
            HighlightBehavior sqBehavior = highlightedSquare.GetComponent<HighlightBehavior>();
            sqBehavior.setSquare(targetSquare);
            sqBehavior.setMoveCallBack(this);
            /*
             * Call a method on the script called setMoveCallback and pass in this.move.
            */
        }
    }
    public void move(string targetSquare)
    {
        Debug.Log("Piece moved to " + targetSquare);
        transform.position = GameState.coordinates(targetSquare);
        foreach (GameObject highlight in highlights)
        {
            Destroy(highlight);
        }
        /*
         * Move the piece.
         * Destroy the highlighted squares.
         * 
        */
    }
}