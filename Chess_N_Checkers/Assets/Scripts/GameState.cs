using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
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

    private static float squareSize = 2.35f;
    private GameObject[,] gameBoard = new GameObject[8, 8];

    private GamePiece selectedPiece = null;
    
    void Start()
    {
        setBoard("8/1B6/8/5p2/8/8/1P3Qrq/1K1R2bk w - - 0 1");
        //setBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        //setBoard("3k4/b7/8/3n4/8/p7/8/4K3 w KQkq - 0 1");
    }
    void Update()
    {

    }

    public string getBoard()
    {
        StringBuilder FEN = new StringBuilder();
        int spaceCount = 0;

        for (int row = 0; row < 8; row++)
        {
            spaceCount = 0;
            for (int col = 0; col < 8; col++)
            {
                GameObject pieceObj = gameBoard[row, col];
                if (pieceObj == null)
                {
                    spaceCount++;
                }
                else
                {
                    if (spaceCount > 0)
                    {
                        FEN.Append(spaceCount.ToString());
                        spaceCount = 0;
                    }

                    GamePiece piece = pieceObj.GetComponent<GamePiece>();
                    FEN.Append(piece.getFENCode());
                }

                if (spaceCount > 0 && col == 7)
                {
                    FEN.Append(spaceCount.ToString());
                    spaceCount = 0;
                }
            }

            if (row < 7)
            {
                FEN.Append("/");
            }
        }

        return FEN.ToString();
    }
    
    public void setBoard(string FEN)
    {
        //discard leading spaces
        FEN = FEN.TrimStart();
        //just grab the board map part
        string[] FENParts = FEN.Split(' ');
        FEN = FENParts[0];

        string[] rowStrs = FEN.Split('/');
        int row = 0;
        foreach (string rowStr in rowStrs)
        {
            int pointer = 0;
            int spaceCount = 0;
            int col = 0;

            do
            {
                char nextChar = rowStr[pointer];
                if (nextChar >= '1' && nextChar <= '8')
                {
                    spaceCount = nextChar - '0'; //converts char to int
                    for (int s = 0; s < spaceCount; s++)
                    {
                        if (gameBoard[row, col] != null)
                        {
                            Destroy(gameBoard[row, col]);
                        }
                        gameBoard[row, col] = null;
                        col++;
                    }
                }
                else
                {
                    if (gameBoard[row, col] != null)
                    {
                        Destroy(gameBoard[row, col]);
                    }

                    GameObject prefab = null;
                    string color = "";
                    switch (nextChar)
                    {
                        case 'k':
                            prefab = bKing;
                            color = GamePiece.COLOR_BLACK;
                            break;
                        case 'q':
                            prefab = bQueen;
                            color = GamePiece.COLOR_BLACK;
                            break;
                        case 'b':
                            prefab = bBishop;
                            color = GamePiece.COLOR_BLACK;
                            break;
                        case 'n':
                            prefab = bKnight;
                            color = GamePiece.COLOR_BLACK;
                            break;
                        case 'r':
                            prefab = bRook;
                            color = GamePiece.COLOR_BLACK;
                            break;
                        case 'p':
                            prefab = bPawn;
                            color = GamePiece.COLOR_BLACK;
                            break;
                        case 'K':
                            prefab = wKing;
                            color = GamePiece.COLOR_WHITE;
                            break;
                        case 'Q':
                            prefab = wQueen;
                            color = GamePiece.COLOR_WHITE;
                            break;
                        case 'B':
                            prefab = wBishop;
                            color = GamePiece.COLOR_WHITE;
                            break;
                        case 'N':
                            prefab = wKnight;
                            color = GamePiece.COLOR_WHITE;
                            break;
                        case 'R':
                            prefab = wRook;
                            color = GamePiece.COLOR_WHITE;
                            break;
                        case 'P':
                            prefab = wPawn;
                            color = GamePiece.COLOR_WHITE;
                            break;
                    }

                    UnityEngine.Debug.Log("instantiating game piece");
                    GameObject pieceObj = Instantiate(prefab, positionToVector3(GamePiece.coordsToPosition(row, col)), Quaternion.identity);
                    gameBoard[row, col] = pieceObj;
                    GamePiece piece = pieceObj.GetComponent<GamePiece>();
                    piece.setGameState(this);
                    piece.setColor(color);
                    piece.setCoords(row, col);
                    col++;
                }
                pointer++;
            } while (col < 8);

            row++;
        }
    }
    public bool isSpaceOccupied(int row, int col)
    {
        if (gameBoard[row, col] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public GamePiece getGamePieceAtCoords(int[] coords)
    {
        return gameBoard[coords[0], coords[1]].GetComponent<GamePiece>();
    }

    public static Vector3 positionToVector3(string position)
    {
        int[] coords = GamePiece.positionToCoords(position);
        return new Vector3(((-3.5f + coords[1]) * squareSize), 0.2f, ((3.5f - coords[0]) * squareSize));
    }
    public GamePiece getSelectedPiece()
    {
        return this.selectedPiece;
    }
    public void setSelectedPiece(GamePiece selectedPiece)
    {
        this.selectedPiece = selectedPiece;
    }
    public void switchPiece(GamePiece newPiece)
    {
        //Call the gameobject variable's unhighlightMoveTargets upon clicking another piece.
        GamePiece g = getSelectedPiece();
        UnityEngine.Debug.Log("The Selected Piece was: " + g);
        if (g != null)
        {
            g.unhighlightMoveTargets();
        }
        setSelectedPiece(newPiece);
    }
    public void movePiece(int[] fromCoords, int[] toCoords)
    {
        gameBoard[toCoords[0], toCoords[1]] = gameBoard[fromCoords[0], fromCoords[1]];
        gameBoard[fromCoords[0], fromCoords[1]] = null;
    }
}