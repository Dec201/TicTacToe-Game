using System;
using System.Linq;
using System.Threading;

namespace TickTack
{
    class Program
    {

        public static bool playerTwoTurn;
        public static bool startGame;
        public static float PlayerOneScore = 0;
        public static float PlayerTwoScore = 0;
        public static string PlayerOneName;
        public static string PlayerTwoName;


        public static char[,] mapNums = new char[3, 3]
        {
                { '1', '2', '3'} ,
                { '4', '5', '6'} ,
                { '7', '8', '9'}
         };

        public static void MapNumsReset()
        {
            mapNums[0, 0] = '1';
            mapNums[0, 1] = '2';
            mapNums[0, 2] = '3';
            mapNums[1, 0] = '4';
            mapNums[1, 1] = '5';
            mapNums[1, 2] = '6';
            mapNums[2, 0] = '7';
            mapNums[2, 1] = '8';
            mapNums[2, 2] = '9';
        }


        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Enter Player One's Name");
            PlayerOneName = Console.ReadLine();

            Console.WriteLine("Enter Player Two's Name");
            PlayerTwoName = Console.ReadLine();


            while (!startGame)
            {
                Console.Clear();
                TickTackMap();


                if (!playerTwoTurn)
                {
                    char userInput2 = TakeUserInput(PlayerOneName);
                    PlayerSelectionDisplay(userInput2);
                    playerTwoTurn = true;
                }
                else
                {
                    char userInput2 = TakeUserInput(PlayerTwoName);
                    PlayerSelectionDisplay(userInput2);
                    playerTwoTurn = false;
                }

                CheckWinDrawLoss();

            }

        }


        public static void TickTackMap()
        {

            Console.WriteLine($"Score Board \n{PlayerOneName} Counter (X) : Score - {PlayerOneScore}   \n" +
                $"{PlayerTwoName} Counter (O) : Score - {PlayerTwoScore} \n\n");

            Console.WriteLine("  {0}  |  {1}  |  {2}   ", mapNums[0, 0], mapNums[0, 1], mapNums[0, 2]);
            Console.WriteLine(" --- | --- | ---  ");
            Console.WriteLine("  {0}  |  {1}  |  {2}   ", mapNums[1, 0], mapNums[1, 1], mapNums[1, 2]);
            Console.WriteLine(" --- | --- | ---  ");
            Console.WriteLine("  {0}  |  {1}  |  {2}   ", mapNums[2, 0], mapNums[2, 1], mapNums[2, 2]);

            Console.WriteLine("\n\n");

        }


        public static void PlayerSelectionDisplay(char input)
        {
            Console.Clear();
            TickTackMap();
            Console.WriteLine($"You have selected number {input}.");
            Thread.Sleep(1000);
        }



        public static char TakeUserInput(string user)
        {

            bool success;
            char inputValue;

            do
            {

                Console.Write($"Please enter an number {user}: ");
                string input = Console.ReadLine().ToUpper();
                success = char.TryParse(input, out inputValue);

                var castedNums = mapNums.Cast<char>();
                if (castedNums.All(x => !x.Equals(inputValue)) || inputValue == ('X') || inputValue == ('O'))
                {
                    success = false;
                    Console.WriteLine("You have selected a taken number or typed a value outside 1-9");
                }


            } while (!success);



            for (int i = 0; i < mapNums.GetLength(0); i++)
            {
                for (int j = 0; j < mapNums.GetLength(1); j++)
                {
                    if (inputValue == mapNums[i, j] && user == PlayerOneName)
                    {
                        mapNums[i, j] = 'X';
                        return inputValue;
                    }
                    else if (inputValue == mapNums[i, j] && user == PlayerTwoName)
                    {
                        mapNums[i, j] = 'O';
                        return inputValue;
                    }

                }

            }
            return inputValue;
        }


        public static void CheckWinDrawLoss()
        {


            if (mapNums[0, 0] == mapNums[0, 1] && mapNums[0, 1] == mapNums[0, 2] ||
                mapNums[1, 0] == mapNums[1, 1] && mapNums[1, 1] == mapNums[1, 2] ||
                mapNums[2, 0] == mapNums[2, 1] && mapNums[2, 1] == mapNums[2, 2] ||
                mapNums[0, 0] == mapNums[1, 0] && mapNums[1, 0] == mapNums[2, 0] ||
                mapNums[1, 0] == mapNums[1, 1] && mapNums[1, 1] == mapNums[1, 2] ||
                mapNums[2, 0] == mapNums[2, 1] && mapNums[2, 1] == mapNums[2, 2] ||
                mapNums[0, 0] == mapNums[1, 1] && mapNums[1, 1] == mapNums[2, 2] ||
                mapNums[0, 2] == mapNums[1, 1] && mapNums[1, 1] == mapNums[2, 0]
                )
            {
                if (playerTwoTurn)
                {
                    Console.WriteLine($"{PlayerOneName} has won the game");
                    PlayerOneScore += 1;
                    QuitGame();
                    MapNumsReset();
                }
                else
                {
                    Console.WriteLine($"{PlayerTwoName} has won the game");
                    PlayerTwoScore += 1;
                    QuitGame();
                    MapNumsReset();
                }
            }


            if (mapNums.Cast<char>().All(x => x.Equals('X') || x.Equals('O')))
            {
                Console.WriteLine("The game ended in a draw");
                PlayerOneScore += 0.5f;
                PlayerTwoScore += 0.5f;
                QuitGame();
                MapNumsReset();
            }


        }



        public static void QuitGame()
        {
            Console.WriteLine("The game has finished, to continue press enter");
            Console.WriteLine("To quit tic-tac-toe type 'q' and press enter");

            string userResponse = Console.ReadLine().ToLower();

            if (userResponse == "q")
            {
                startGame = true;
            }
        }


    }
}
