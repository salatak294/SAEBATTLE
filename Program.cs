using System;

class Program
{
    static char[,] playerBoard = new char[10, 10];
    static char[,] computerBoard = new char[10, 10];

    static void Main()
    {
        InitializeBoards();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Ваша доска:");
            DisplayBoard(playerBoard);

            Console.WriteLine("\nДоска компьютера:");
            DisplayBoard(computerBoard, true);

            Console.Write("\nВведите координаты для выстрела (например, A3): ");
            string input = Console.ReadLine().ToUpper();

            if (input == "EXIT")
                break;

            if (input.Length == 2 && input[0] >= 'A' && input[0] <= 'J' && input[1] >= '0' && input[1] <= '9')
            {
                int row = input[1] - '0';//В ряду
                int col = input[0] - 'A';

                if (computerBoard[row, col] == 'X')
                {
                    Console.WriteLine("Попадание!");
                    computerBoard[row, col] = 'H';
                }
                else
                {
                    Console.WriteLine("Мимо!");
                    computerBoard[row, col] = 'M';
                }

                if (!CheckForWinner(computerBoard))
                {
                    ComputerMove();
                    if (CheckForWinner(playerBoard))
                    {
                        Console.Clear();
                        DisplayBoard(playerBoard);
                        Console.WriteLine("Вы проиграли! Доска компьютера:");
                        DisplayBoard(computerBoard, true);
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    DisplayBoard(playerBoard);
                    Console.WriteLine("Вы победили! Доска компьютера:");
                    DisplayBoard(computerBoard, true);
                    break;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите координаты в формате A3.");
            }
        }

        Console.WriteLine("Игра завершена. Нажмите любую клавишу для выхода.");
        Console.ReadKey();
    }

    static void InitializeBoards()
    {
        InitializeBoard(playerBoard);
        InitializeBoard(computerBoard);

        PlaceShips(playerBoard);
        PlaceShips(computerBoard);
    }

    static void InitializeBoard(char[,] board)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                board[i, j] = '~';
            }
        }
    }

    static void PlaceShips(char[,] board)
    {
       
        
        Random random = new Random();

        for (int shipLength = 5; shipLength >= 1; shipLength--)
        {
            bool shipPlaced = false;

            while (!shipPlaced)
            {
                int orientation = random.Next(2); 

                int row = random.Next(10);
                int col = random.Next(10);

                if (CanPlaceShip(board, row, col, shipLength, orientation))
                {
                    PlaceShip(board, row, col, shipLength, orientation);
                    shipPlaced = true;
                }
            }
        }
    }

    static bool CanPlaceShip(char[,] board, int row, int col, int length, int orientation)
    {
        if (orientation == 0)
        {
            if (col + length > 10)
                return false;
            for (int i = col; i < col + length; i++)
            {
                if (board[row, i] != '~')
                    return false;
            }
        }
        else
        {
            if (row + length > 10)
                return false;
            for (int i = row; i < row + length; i++)
            {
                if (board[i, col] != '~')
                    return false;
            }
        }

        return true;
    }

    static void PlaceShip(char[,] board, int row, int col, int length, int orientation)
    {
        for (int i = 0; i < length; i++)
        {
            if (orientation == 0)
                board[row, col + i] = 'O';
            else
                board[row + i, col] = 'O';
        }
    }

    static void DisplayBoard(char[,] board, bool hideShips = false)
    {
        Console.Write("  A B C D E F G H I J\n");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{i} ");
            for (int j = 0; j < 10; j++)
            {
                char displayChar = board[i, j];
                if (hideShips && displayChar == 'O')
                    displayChar = '~';

                Console.Write($"{displayChar} ");
            }
            Console.WriteLine();
        }
    }

    static void ComputerMove()
    {
        Random random = new Random();
        int row, col;

        do
        {
            row = random.Next(10);
            col = random.Next(10);
        } while (playerBoard[row, col] == 'H' || playerBoard[row, col] == 'M');

        if (playerBoard[row, col] == 'O')
        {
            Console.WriteLine($"Компьютер попал в ваши корабли на клетке {Convert.ToChar('A' + col)}{row}!");
            playerBoard[row, col] = 'H';
        }
        else
        {
            Console.WriteLine($"Компьютер промахнулся по клетке {Convert.ToChar('A' + col)}{row}.");
            playerBoard[row, col] = 'M';
        }
    }

    static bool CheckForWinner(char[,] board)
    {
        foreach (char cell in board)
        {
            if (cell == 'O')
                return false;
        }
        return true;
    }
    
}



