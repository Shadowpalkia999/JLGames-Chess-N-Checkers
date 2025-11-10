using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public static string COLOR_WHITE = "w";
    public static string COLOR_BLACK = "b";
    private string color;
    private string FENCode = "";
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
}
