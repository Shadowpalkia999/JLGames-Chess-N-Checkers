# JLGames Presents: Chess N Checkers

This is the readme file.

Welcome to Chess N Checkers! A game that let's you play either Chess or Checkers!

# Chess Game Design Notes
```
example of a move: e2e4
example of a position: rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR
example of a state: rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
example of state after first move complete: rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2
```

## ChessPosition (a class that holds various positions)
```
  STARTPOS
  SIMPLE1
  SIMPLE2
  TRICKY1
  TRICKY2
  ENDGAME1
  ENDGAME2
```

## ChessBoard  (a class that holds the state of the board and helps with display)
```
  setState
  getState
  move
```

## ChessPlayer (a class that represents the brain of the player, be they human or computer)
```
  setState
  getMove
```

What Classes will we have?

ChessEngine
  Methods:
    setPosition(FEN string)
    getMove() (returns algebraic move)

GameState (a class responsible for holding the state of the board and talking to the chess engine)
  Methods:
    reset()
    getBoard (returns FEN string)
    setBoard(FEN string)
    move(move) (e.g. a2a3)
  
GamePiece (a class responsible for representing one of the pieces on the board)
  Instance Variables:
    position (e.g. a2)
  Methods:
    move(dest square) (e.g. a3)
      (GamePiece will call the GameState object and send move a2a3)
    getAllowedMoves() (return an array of algebraic moves that are permissible)
   
move allowed?