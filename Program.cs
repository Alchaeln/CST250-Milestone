// See https://aka.ms/new-console-template for more information
using MinesweeperClassLibrary;
using System.Data;
using System.Drawing;

Console.WriteLine("Enter a number for your size: ");
int size = int.Parse(Console.ReadLine());

GameBoard gameBoard = new GameBoard(size);

gameBoard.SetupLiveNeighbors();
gameBoard.calculateLiveNeighbors();

void printBoard()
{
    Console.Write("+");
    for (int numOnTop = 0; numOnTop < gameBoard.Size; numOnTop++) 
    {
        Console.Write(" " + numOnTop + " +");
    }

    Console.WriteLine("");
    Console.Write("+");

    for (int numOnTop = 0; numOnTop < gameBoard.Size; numOnTop++)
    {
        Console.Write("---+");
    }

    Console.WriteLine("");
    //populate with of individual row and col
    for (int i = 0; i < gameBoard.Size; i++)
    {
        Console.Write("|");
        //increases column number by 1
        for (int j = 0; j < gameBoard.Size; j++)
        {
            Console.Write(" ");
            if (gameBoard.Grid[i, j].Live)
            {
                Console.Write("X ");
            }

            else if (!gameBoard.Grid[i, j].Live)
            {
                Console.Write(gameBoard.Grid[i, j].NumOfNeighbors + " ");
            }
            //check if live neighbors

            else
            {
                Console.Write(" ");
            }

            Console.Write("|");
            ;
        }

        Console.WriteLine(" " + i);

        Console.Write("+");
        for (int numOnTop = 0; numOnTop < gameBoard.Size; numOnTop++)
        {
            Console.Write("---+");
        }

        Console.WriteLine("");
    }
}
//PRINT A CHEAT SHEET

//printBoard();

void printBoardDuringGame() 
{
    Console.Write("+");
    for (int numOnTop = 0; numOnTop < gameBoard.Size; numOnTop++)
    {
        Console.Write(" " + numOnTop + " +");
    }

    Console.WriteLine("");
    Console.Write("+");

    for (int numOnTop = 0; numOnTop < gameBoard.Size; numOnTop++)
    {
        Console.Write("---+");
    }

    Console.WriteLine("");
    //populate with of individual row and col
    for (int i = 0; i < gameBoard.Size; i++)
    {
        Console.Write("|");
        //increases column number by 1
        for (int j = 0; j < gameBoard.Size; j++)
        {
            //Marks the bombs
            Console.Write(" ");
            if (gameBoard.Grid[i, j].Live && gameBoard.Grid[i, j].IsVisited)
            {
                Console.Write("X ");
            }

            else if (gameBoard.Grid[i, j].IsVisited && gameBoard.Grid[i, j].NumOfNeighbors == 0)
            {
                Console.Write("  ");
            }

            //Marks the numbers after checking if the user inputs a location
            //Reveals the location and switches the ? for the number of neighbors
            else if (!gameBoard.Grid[i, j].Live && gameBoard.Grid[i, j].IsVisited)
            {
                Console.Write(gameBoard.Grid[i, j].NumOfNeighbors + " ");
            }
            //check if live neighbors

            else
            {
                Console.Write("? ");
            }

            Console.Write("|");
            ;
        }

        Console.WriteLine(" " + i);

        Console.Write("+");
        for (int numOnTop = 0; numOnTop < gameBoard.Size; numOnTop++)
        {
            Console.Write("---+");
        }

        Console.WriteLine("");
    }
}

printBoardDuringGame();

//checks how many bombs in game
int numOfBombs = 0;
int numOfVisits = 0;

foreach (GameCell gameCell in gameBoard.Grid)
{
    if (gameCell.Live) 
    { 
        numOfBombs++; 
    }
}

Console.WriteLine("ENTER YOUR ROW: ");
int row = int.Parse(Console.ReadLine());

Console.WriteLine("ENTER YOUR COL: ");
int col = int.Parse(Console.ReadLine());

if (gameBoard.Grid[row, col].NumOfNeighbors == 0)
{
    gameBoard.floodFill(row, col);
}

while (!gameBoard.Grid[row, col].Live)
{
    gameBoard.Grid[row, col].IsVisited = true;

    numOfVisits++ ;

    printBoardDuringGame();

    Console.WriteLine("ENTER YOUR ROW: ");
    row = int.Parse(Console.ReadLine());

    Console.WriteLine("ENTER YOUR COL: ");
    col = int.Parse(Console.ReadLine());

    if (gameBoard.Grid[row, col].NumOfNeighbors == 0)
    {
        gameBoard.floodFill(row, col);
    }

    bool lose = true;

    lose = gameBoard.Grid[row, col].Live;


    if (lose)
    {
        printBoard();
        Console.WriteLine("HAHAHAHAHAHH YOU LOST!");
        break;
    }

    //if everything else except for the bombs show then you can output win message
    //use a counter to tell how many livebombs

    //we would use size * size - numofbombs to see how many nonlive spaces then use the amount of non 
    //we use the 

    else if (numOfVisits + 1 == (size * size - numOfBombs))
    {
        printBoard();
        Console.WriteLine("YOU WIN!");
        break;
    }

}

//The main program should have a printBoard helper method that uses, for loops, the Console.write and Console.writeLine commands to display the contents of the Board as shown at the beginning of these instructions