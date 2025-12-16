using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public static string COLOR_WHITE = "w";
    public static string COLOR_BLACK = "b";
    private string color;
    private string FENCode = "";
    private string position = "";
    
    //It seems that a piece has chains of squares where it can move with each
    //chain representing a row, column, or diagonal.  Therefore, the possible 
    //relative moves is a list of list of coordinates where each list of coordinates
    //represents a direction of movement
    protected List<List<int[]>> relativeMoves = new List<List<int[]>>();

    private List<GameObject> highlights = new List<GameObject>();

    public GameObject highlight;
    private GameState gState;

    private bool isClicked = false;

    public GameState getGameState()
    {
        return this.gState;
    }
    public void setGameState(GameState gameState)
    {
        gState = gameState;
    }
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

    public string getPosition()
    {
        return position;
    }

    public void setPosition(string position)
    {
        this.position = position;
        transform.position = GameState.positionToVector3(this.position);
    }

    public int[] getCoords()
    {
        int[] coords = new int[]{0, 0};
 
        if (position.Equals("") || position.Length != 2)
        {
            return coords;
        }

        coords[0] = 8 - (int)(position[1] - '0');
        coords[1] = (int)(position[0] - 'a');
        return coords;
    }

    public static string coordsToPosition(int row, int col)
    {
        char[] coords = new char[2];
        
        if (row < 0 || row > 7 || col < 0 ||  col > 7)
        {
            return "";
        }

        coords[0] = (char)('a' + col);
        coords[1] = (char)('0' + (8 - row));

        string mypos = new string(coords);
        return mypos;        
    }

    public static int[] positionToCoords(string square)
    {
        char[] coords = square.ToCharArray();
        int col = 7 - (int)('h' - coords[0]);
        int row = 7 - (int)(coords[1] - '1');
        return new int[]{row, col};
    }

    public void setCoords(int row, int col)
    {
        setPosition(coordsToPosition(row, col));
    }

    virtual public List<List<int[]>> getRelativeMoves()
    {
        return relativeMoves;
    }

    public void setRelativeMoves(List<List<int[]>> moves)
    {
        relativeMoves = moves;
    }

    //returns an list of move paths containing target squares based on current list of relative moves
    public List<List<string>> getMoves()
    {
        //for return value
        List<List<string>> moves = new List<List<string>>();

        //getCoords returns current position in zero-based row,col array with origin at top left
        int[] coords = getCoords();

        //must call getRelativeMoves rather than directly accessing the collection to 
        // allow subclass (Pawn) to constrain the moves based on color
        foreach (List<int[]> relativeMovePath in getRelativeMoves())
        {
            List<string> movePath = new List<string>();

            foreach (int[] relCoords in relativeMovePath)
            {
                //relative moves are denoted as coordinates in [row, col] with + or - square count
                //now check if square is valid and then add to list
                string str = coordsToPosition(coords[0] + relCoords[0], coords[1] + relCoords[1]);
                if (! str.Equals(""))
                {
                    movePath.Add(str);                
                }
            }
            moves.Add(movePath);            
        }

        return moves;
    }

    public void highlightMoveTargets()
    {
        foreach (List<string> movePath in getMoves())
        {
            foreach (string targetSquare in movePath)
            {  
                bool endMovePathEval = false;
                bool skipThisSquare = false;

                int[] coords = GamePiece.positionToCoords(targetSquare);
                if (gState.isSpaceOccupied(coords[0], coords[1]))
                {
                    if (color.Equals(gState.getGamePieceAtCoords(coords).getColor()))
                    {
                        skipThisSquare = true;
                    }
                    endMovePathEval = true;
                }

                if (! skipThisSquare)
                {                    
                    GameObject highlightedSquare = Instantiate(highlight, GameState.positionToVector3(targetSquare), Quaternion.identity);
                    highlights.Add(highlightedSquare);
                    HighlightBehavior sqBehavior = highlightedSquare.GetComponent<HighlightBehavior>();
                    sqBehavior.setSquare(targetSquare);
                    sqBehavior.setMoveCallBack(this);
                }

                UnityEngine.Debug.Log("evaluating target square: " + targetSquare);
                if (endMovePathEval)
                {
                    UnityEngine.Debug.Log("BREAK!");
                    break;
                }
            }
        }
    }
    public void unhighlightMoveTargets()
    {
        isClicked = false;
        foreach (GameObject g in highlights)
        {            
            Destroy(g);
        }
        highlights.Clear();
    }
    virtual public void move(string targetSquare)
    {
        gState.movePiece(getCoords(), positionToCoords(targetSquare));
        setPosition(targetSquare);
        foreach (GameObject highlight in highlights)
        {
            Destroy(highlight);
        }
    }
    private void OnMouseDown()
    {
        UnityEngine.Debug.Log("Piece Clicked");
        isClicked = !isClicked;
        if (isClicked)
        {
            this.highlightMoveTargets();
            gState.switchPiece(this);
        }
        else
        {
            unhighlightMoveTargets();
            gState.switchPiece(null);
        }
    }
}