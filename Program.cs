using System;

namespace guessing_game
{
    class Program
    {
        static void Main(string[] args)
        {
            int randomNumber = 0;
            int AIguess = 0;
            int coins = 100;
            bool roundOver = false;
            int tries = 1;
            Random r = new Random();
            //initializes game, game runs while the user still has coins
            while (coins > 0)
            {
                int rand = r.Next(1, 100);
                AIguess = r.Next(1, 100);
            //generates a prime number from 1-100
                while (!isPrime(rand))
                {
                    rand = r.Next(1, 100);
                }

                randomNumber = rand;
                roundOver = false;
                tries = 1;
                string difficulty = "";
                bool difficultyChosen = false;
                //ensures that the user enters an acceptable difficulty
                while (!difficultyChosen)
                {
                    Console.WriteLine("What difficulty do you want to play on> (Easy, Medium, Hard):");
                    difficulty = Console.ReadLine();
                    if (difficulty == "Easy" || difficulty == "Medium" || difficulty == "Hard")
                    {

                        difficultyChosen = true;
                        Console.WriteLine("\n");
                        Console.WriteLine($"--------Starting a {difficulty} difficulty game. Good Luck!-------");
                    }
                    else
                    {
                        Console.WriteLine("Invalid difficulty entry. Please try again");
                    }
                }

                while (roundOver == false && coins > 0)
                {   
                        //displays the correct answer, and allows the user to enter a wager for winning  the round.
                    AIguess = r.Next(1, 100);

                    Console.WriteLine("\n");
                    Console.WriteLine($"Answer: {randomNumber}");
                    Console.WriteLine($"How many coins do you want to bet? (You have {coins} coins):");
                    int bet = 0;
                    while (!int.TryParse(Console.ReadLine(), out bet) || bet > coins)
                    {
                        Console.WriteLine("That was invalid. Enter an integer wager within your budget.");
                        Console.WriteLine($"How many coins do you want to bet? (You have {coins} coins):");
                    }

                    Console.WriteLine($"Guess the number between 1 and 100!: ({9 - tries} tries remaining)");
                    int guess = 0;
                    while (!int.TryParse(Console.ReadLine(), out guess) || guess > 100)
                    {
                        Console.WriteLine("Invalid guess. Enter an integer from 1-100");
                        Console.WriteLine($"Guess the number between 1 and 100!: ({9 - tries} tries remaining)");
                    }
                    //on easy mode, the AI guesses from 1-100, on medium it guesses prime numbers and on Hard, its guessing pool gets cut in half
                    switch (difficulty)
                    {
                        case "Easy": AIguess = r.Next(1, 100); break;
                        case "Medium":
                            while (!isPrime(AIguess))
                            {
                                AIguess = r.Next(1, 100);
                            }; break;

                        case "Hard":
                            if (randomNumber <= 50)
                            {
                                while (!isPrime(AIguess))
                                {
                                    AIguess = r.Next(1, 50);
                                };
                            }
                            else
                            {
                                while (!isPrime(AIguess))
                                {
                                    AIguess = r.Next(51, 100);
                                };

                            }; break;
                        default:
                            break;

                    }

                        //ends the round after 8 wrong guesses
                    if (tries == 8)
                    {
                        Console.WriteLine($"-------Round Over! You took too many tries-------");
                        Console.WriteLine("\n");
                        roundOver = true;
                    }
                        //ends the round if the correct number is guessed, adds the wager to the users wallet
                    else if (guess == randomNumber)
                    {
                        coins = coins + bet;
                        roundOver = true;
                        Console.WriteLine($"------You guessed the correct number {randomNumber} with {tries} tries and finished with {coins} coins!-------");
                    }
                        // end the round if the AI guesses correctly, takes the wager from the users wallet
                    else if (AIguess == randomNumber)
                    {
                        Console.WriteLine($"-----The AI guessed {randomNumber} first! You Lose!-----");
                        Console.WriteLine("\n");
                         coins = coins - bet;
                        roundOver = true;
                    }
                    //gives the user a hint on an incorrect guess, takes the wager from the users wallet
                    else if (guess < randomNumber)
                    {
                        coins = coins - bet;
                        tries++;
                        Console.WriteLine($"You guessed too low and lost {bet} coins!");
                        Console.WriteLine($"The AI guessed {AIguess}");
                    }
                    //gives the user a hint on an incorrect guess, takes the wager from the users wallet
                    
                    else if (guess > randomNumber)
                    {
                        coins = coins - bet;
                        tries++;
                        Console.WriteLine($"You guessed to high and lost {bet} coins");
                        Console.WriteLine($"The AI guessed {AIguess}");

                    }

                }

            }
            Console.WriteLine("Game over! You ran out of coins!");

        }
        //function to check if a number is Prime
        static bool isPrime(int num)
        {
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    return false;
                }

            }
            return true;

        }
    }

}
