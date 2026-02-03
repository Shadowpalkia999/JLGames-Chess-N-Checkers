using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Pawn : GamePiece
{
    private bool firstMove;
    private int BLACK_START_ROW = 1;
    private int WHITE_START_ROW = 6;

    void Start()
    {
        firstMove = true;
        this.setFENCode("p");

        List<int[]> movePath = null;
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{-1,0});
        relativeMoves.Add(movePath);
        
        movePath = new List<int[]>();
        movePath.Add(new int[]{1,0});
        relativeMoves.Add(movePath);
    }

    override public bool canCapture(GamePiece piece)
    {
        int offset;
        if (getColor().Equals(COLOR_BLACK))
        {
            offset = - 1;
        }
        else
        {
            offset = 1;
        }

        if (getCoords()[0] == piece.getCoords()[0] + offset)
        {
            if (getCoords()[1] == piece.getCoords()[1] - 1 ||
                getCoords()[1] == piece.getCoords()[1] + 1
            )
            {
                return true;
            }
        }

        return false;            
    }

    override public List<List<int[]>> getRelativeMoves()
    {
        //constrain the list of moves based on piece color
        List<List<int[]>> relMoves = new List<List<int[]>>();
        List<int[]> constrainedMoves = new List<int[]>();
        GameState gState = getGameState();

        if (getColor().Equals(COLOR_BLACK))
        {
            constrainedMoves.Add(new int[] { 1, 0 });
            if (firstMove && getCoords()[0] == BLACK_START_ROW)
            {
                constrainedMoves.Add(new int[] { 2, 0 });
            }
            relMoves.Add(constrainedMoves);    

            //add in diagonal moves for capturing
            int[] relsquare = new int[]{ 1, -1 };
            int[] c = getCoordsForRelativeSquare(relsquare);
            UnityEngine.Debug.Log("checking diagonal square: (" + c[0] + ", " + c[1] + ")");
            if (gState.isSpaceOccupied(c[0], c[1]))
            {
                UnityEngine.Debug.Log("square is occupied");
                //add the diagonal square to the constrained move list as long as it is opposite color
                GamePiece p = gState.getGamePieceAtCoords(c);
                if (! p.getColor().Equals(getColor())) 
                {
                    UnityEngine.Debug.Log("Adding relative square: " + relsquare);
                    constrainedMoves = new List<int[]>();
                    constrainedMoves.Add(relsquare);
                    relMoves.Add(constrainedMoves);    
                }
            }

            relsquare = new int[]{ 1, 1 };
            c = getCoordsForRelativeSquare(relsquare);
            UnityEngine.Debug.Log("checking diagonal square: (" + c[0] + ", " + c[1] + ")");
            if (gState.isSpaceOccupied(c[0], c[1]))
            {
                UnityEngine.Debug.Log("square is occupied");
                //add the diagonal square to the constrained move list as long as it is opposite color
                GamePiece p = gState.getGamePieceAtCoords(c);
                UnityEngine.Debug.Log("GamePiece: " + p);
                UnityEngine.Debug.Log("GamePiece color: " + p.getColor());
                if (! p.getColor().Equals(getColor())) 
                {
                    UnityEngine.Debug.Log("Adding relative square: " + relsquare);
                    constrainedMoves = new List<int[]>();
                    constrainedMoves.Add(relsquare);
                    relMoves.Add(constrainedMoves);    
                }             
            }
        }
        else
        {
            constrainedMoves.Add(new int[]{ -1, 0 });
            if (firstMove && getCoords()[0] == WHITE_START_ROW)
            {
                constrainedMoves.Add(new int[] { -2, 0 });
            }
            relMoves.Add(constrainedMoves);    

            //add in diagonal moves for capturing
            int[] relsquare = new int[]{ -1, -1 };
            int[] c = getCoordsForRelativeSquare(relsquare);
            UnityEngine.Debug.Log("checking diagonal square: (" + c[0] + ", " + c[1] + ")");
            if (gState.isSpaceOccupied(c[0], c[1]))
            {
                UnityEngine.Debug.Log("square is occupied");
                //add the diagonal square to the constrained move list as long as it is opposite color
                GamePiece p = gState.getGamePieceAtCoords(c);
                if (! p.getColor().Equals(getColor())) 
                {
                    UnityEngine.Debug.Log("Adding relative square: " + relsquare);
                    constrainedMoves = new List<int[]>();
                    constrainedMoves.Add(relsquare);
                    relMoves.Add(constrainedMoves);    
                }
            }

            relsquare = new int[]{ -1, 1 };
            c = getCoordsForRelativeSquare(relsquare);
            UnityEngine.Debug.Log("checking diagonal square: (" + c[0] + ", " + c[1] + ")");
            if (gState.isSpaceOccupied(c[0], c[1]))
            {
                UnityEngine.Debug.Log("square is occupied");
                //add the diagonal square to the constrained move list as long as it is opposite color
                GamePiece p = gState.getGamePieceAtCoords(c);
                UnityEngine.Debug.Log("GamePiece: " + p);
                UnityEngine.Debug.Log("GamePiece color: " + p.getColor());
                if (! p.getColor().Equals(getColor())) 
                {
                    UnityEngine.Debug.Log("Adding relative square: " + relsquare);
                    constrainedMoves = new List<int[]>();
                    constrainedMoves.Add(relsquare);
                    relMoves.Add(constrainedMoves);    
                }             
            }
        }
        
        return relMoves;
    }
    override public void move(string targetSquare)
    {
        base.move(targetSquare);
        firstMove = false;
    }
}