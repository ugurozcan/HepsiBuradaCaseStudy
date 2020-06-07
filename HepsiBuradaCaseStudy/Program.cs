using System;

namespace HepsiBuradaCaseStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the boundary of the plateau as a number with a space between them:");
            var plateau = Console.ReadLine().ToString();
            Console.WriteLine("Enter the starting position as a number and the direction as a letter with a space between them:");
            var startPos = Console.ReadLine().ToString().ToUpper();
            Console.WriteLine("Enter the letter set indicating the direction of movement without any space between them:");
            var movementDirection = Console.ReadLine().ToString().ToUpper();

            var serv = new Services.NavigationService();
            Console.WriteLine($"You are at {serv.Navigate(new string[] { plateau, startPos, movementDirection })} now!");

            Console.WriteLine();
            Console.WriteLine("Press any key to close this window");
            Console.ReadLine();
        }
    }
}
