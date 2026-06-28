using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

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
    public GameObject checkPanel;
    public GameObject resultPanel;

    private GamePiece whiteKingPiece;
    private GamePiece blackKingPiece;

    public int MIN_ROW_COORD = 0;
    public int MAX_ROW_COORD = 7;
    public int MIN_COL_COORD = 0;
    public int MAX_COL_COORD = 7;

    private static float squareSize = 2.35f;
    private GameObject[,] gameBoard = new GameObject[8, 8];

    private GamePiece selectedPiece = null;

    private int turnCounter = 0;

    public TMP_Text turnText;
    public TMP_Text checkText;
    public TMP_Text resultText;
    
    void Start()
    {
        setBoard("8/1B6/8/5p2/8/8/1P3Qrq/1K1R2bk w - - 0 1");
        //setBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        //setBoard("3k4/b7/8/3n4/8/p7/8/4K3 w KQkq - 0 1");

        changeTurn();
        checkPanel.SetActive(false);
        resultPanel.SetActive(false);
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
                    if (nextChar == 'k')
                    {
                        blackKingPiece = piece;
                    }
                    if (nextChar == 'K')
                    {
                        whiteKingPiece = piece;
                    }
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
        int[] coords = { row, col };
        if (coords[0] < MIN_ROW_COORD || 
            coords[0] > MAX_ROW_COORD ||
            coords[1] < MIN_COL_COORD ||
            coords[1] > MAX_COL_COORD)
        {
            return false;
        }

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
        if (coords[0] < MIN_ROW_COORD || 
            coords[0] > MAX_ROW_COORD ||
            coords[1] < MIN_COL_COORD ||
            coords[1] > MAX_COL_COORD)
        {
            throw new System.IndexOutOfRangeException("Coordinates outside of board boundary.");
        }
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
    public GameObject getTargetGameObject(int row, int col)
    {
        return gameBoard[row, col];
    }
    public void changeTurn()
    {
        turnCounter++;
        string turn = getTurn();
        turnText.text = "Turn: " + turn;
    }
    public string getTurn()
    {
        int turnRemainder = turnCounter % 2;
        if (turnRemainder == 1)
        {
            //turn is white
            return GamePiece.COLOR_WHITE;
        }
        else
        {
            //turn is black
            return GamePiece.COLOR_BLACK;
        }
    }
    public bool canMove(string sourceSquare, string targetSquare)
    {
        UnityEngine.Debug.Log("Checking canMove for sourceSquare, targetSquare: " + sourceSquare + ", " + targetSquare);
        //Move piece in gameBoard array
        int[] currentCoords = GamePiece.positionToCoords(sourceSquare);
        int[] destCoords = GamePiece.positionToCoords(targetSquare);
        gameBoard[destCoords[0], destCoords[1]] = gameBoard[currentCoords[0], currentCoords[1]];
        gameBoard[currentCoords[0], currentCoords[1]] = null;

        //Evaluate board for putting self-in-check
        bool chk = isCheck(getTurn());
        //Return piece to sourceSquare
        gameBoard[currentCoords[0], currentCoords[1]] = gameBoard[destCoords[0], destCoords[1]];
        gameBoard[destCoords[0], destCoords[1]] = null;
        if (chk)
        {
            UnityEngine.Debug.Log("YOU CAN'T PUT YOURSELF IN CHECK!");
            return false;
        }
        return true;
    }
    public bool isCheck(string color)
    {
        if (color == GamePiece.COLOR_WHITE)
        {
            string wKingPosition = whiteKingPiece.getPosition();
            List<GamePiece> blackPieces = getPieces(GamePiece.COLOR_BLACK);
            foreach (GamePiece blackPiece in blackPieces)
            {
                List<List<string>> moves = blackPiece.getMoves();
                foreach (List<string> path in moves)
                {
                    foreach (string square in path)
                    {
                        if (square == wKingPosition)
                        {
                            UnityEngine.Debug.Log("Found someone checking the white king from square: " + blackPiece.getPosition());
                            return true;
                        }
                    }
                }
            }
        }

        if (color == GamePiece.COLOR_BLACK)
        {
            string bKingPosition = blackKingPiece.getPosition();
            List<GamePiece> whitePieces = getPieces(GamePiece.COLOR_WHITE);
            foreach (GamePiece whitePiece in whitePieces)
            {
                List<List<string>> moves = whitePiece.getMoves();
                foreach (List<string> path in moves)
                {
                    foreach (string square in path)
                    {
                        if (square == bKingPosition)
                        {
                            UnityEngine.Debug.Log("Found someone checking the black king from square: " + whitePiece.getPosition());
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public List<GamePiece> getPieces(string color)
    {
        List<GamePiece> pieces = new List<GamePiece>();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                GameObject gObj = gameBoard[row, col];
                if (gObj != null)
                {
                    GamePiece piece = gObj.GetComponent<GamePiece>();
                    if (piece.getColor() == color)
                    {
                        pieces.Add(piece);
                    }
                }            
            }
        }

        return pieces;
    }
}