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

    private float squareSize = 2.35f;
    private GameObject[,] gameBoard = new GameObject[8, 8];
    
    void Start()
    {
        //setBoard("8/1B6/8/5p2/8/8/5Qrq/1K1R2bk w - - 0 1");
        setBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
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

                    GameObject pieceObj = Instantiate(prefab, new Vector3(((-3.5f + col) * squareSize), 0.5f, ((3.5f - row) * squareSize)), Quaternion.identity);
                    gameBoard[row, col] = pieceObj;
                    GamePiece piece = pieceObj.GetComponent<GamePiece>();
                    piece.setColor(color);
                    col++;
                }
                pointer++;
            } while (col < 8);

            row++;
        }
    }
}