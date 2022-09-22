using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperClassLibrary
{
    public class GameBoard
    {
        public int Size { get; set; }
        double Difficulty { get; set; }
        public GameCell[,] Grid { get; set; }

        public GameBoard(int size)
        {
            //makes it able to get referenced throughout code
            Size = size;

            //makes board of gamecells with the size
            Grid = new GameCell[Size, Size];

            //populate with of individual row and col
            for (int i = 0; i < Size; i++)
            {
                //increases column number by 1
                for (int j = 0; j < Size; j++)
                {
                    Grid[i,j] = new GameCell();

                    Grid[i,j].Col = j;
                    Grid[i,j].Row = i;

                }
            }
        }

        public void SetupLiveNeighbors()
        {
            Random random = new Random();
            //uses difficulty int to populate or multiply the amount of bombs on the board
            //randomizes row and column
            //if there is a bomb already in that place then it throws it through the randomizer again
            //randomly places the bomb by turning the gamecell live to true and editing
            //edits the x and y and randomizes those
            //if bomb is alread live then dont set it

            //needs percent of bombs
            Difficulty = random.Next(20, 30);
            Difficulty = Difficulty / 100;

            for (
                int numOfBombs = 0; numOfBombs < (Difficulty * (Size*Size)); numOfBombs++)
            {

                int x = random.Next(0, Size);
                int y = random.Next(0, Size);

                if (Grid[x, y].Live)
                {
                    //reiterates through the loop until one unlive becomes live
                    numOfBombs--;
                }

                else
                {
                    Grid[x, y].Live = true;
                }
            }
        }

        //calculates if there are bombs inn the area
        public void calculateLiveNeighbors()
        {
            //checks area around to see if there are bombs surrounding the index

            //if there is a  bomb within that index then the counter goes up by one

            for (int i = 0; i < Size; i++)
            {
                //increases column number by 1
                for (int j = 0; j < Size; j++)
                {

                    int numOfBombs = 0;

                    if (!Grid[i, j].Live)
                    {

                        //checks top left corner ex: if looking at cell 0,0 then -1 -1 and sees if true(if it is within bounds)
                        if (i > 0 && j > 0) 
                        {
                            bool check = Grid[i - 1, j - 1].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                        //checks middle top
                        if (j > 0)
                        {
                            bool check = Grid[i, j - 1].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                        //checks top right
                        if (i < Size - 1 && j > 0)
                        {
                            bool check = Grid[i + 1, j - 1].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                        //checks left cell
                        if (i > 0)
                        {
                            bool check = Grid[i - 1, j].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                        //checks the right cell
                        if (i < Size - 1)
                        {
                            bool check = Grid[i + 1, j].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                        //checks bbottmom left cell
                        if (i > 0 && j < Size - 1)
                        {
                            bool check = Grid[i - 1, j + 1].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                        //checks bottom one below the cell
                        if (j < Size - 1)
                        {
                            bool check = Grid[i, j + 1].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                        //checks top right
                        if (i < Size - 1 && j < Size - 1)
                        {
                            bool check = Grid[i + 1, j + 1].Live;
                            if (check)
                            {
                                numOfBombs++;
                            }
                        }

                    }
                    Grid[i, j].NumOfNeighbors = numOfBombs;
                    //adds to specific index of num of neighbors
                }
            }

            //EX: selects index at 1,1 and then checks 0,0; 0,1; 0,2 then goes check the others beside to see if live
            //If it is live the counter goes up
            //then runs through the loop again to check every row and column

        }

        public void floodFill(int row, int col)
        {


            if (row < 0 || row > Size - 1 || col < 0 || col > Size - 1) { return; }

            Grid[row, col].Row = row;
            Grid[row, col].Col = col;

            if (Grid[row, col].NumOfNeighbors == 1 || Grid[row, col].NumOfNeighbors == 2) 
            {
                Grid[row, col].IsVisited = true;
            }

            if (Grid[row, col].NumOfNeighbors == 0 && !Grid[row, col].IsVisited)
            {
                Grid[row, col].IsVisited = true;
                floodFill(row + 1, col);
                floodFill(row - 1, col);
                floodFill(row, col - 1);
                floodFill(row, col + 1);
            }

            

            else
            {
                return;
            }
        }

    }
}
