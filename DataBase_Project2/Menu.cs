using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase_Project2
{
    internal class Menu
    {
        private int menuSelect;
        private string[] options;
        private string prompt;

        public Menu(string prompt, string[] options)
        {
            this.prompt = prompt;
            this.options = options;
            this.menuSelect = 0;
        }

        private void DisplayOptions()
        {

            // Shows the promt you've chosen to display in the constructor
            Console.WriteLine(prompt);

            // Shows the menu options you've chosen to display in the array in the constructor
            for (int i = 0; i < options.Length; i++)
            {
                string menuOptions = options[i];

                // Highlights selected option in the meny
                if (i == menuSelect)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                // Shows << >> around the selected option
                Console.WriteLine($" << {menuOptions} >> ");
            }
            // resets the coloring so it doesn't bleed into any other part of the program when it's running
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                // For registering the keys pressed by the user
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                // Update MenuSelect based on arrow keys.
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    menuSelect--;
                    //(Looping effect so you can loop through the menu)
                    if (menuSelect == -1)
                    {
                        menuSelect = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    menuSelect++;
                    if (menuSelect == options.Length)
                    {
                        menuSelect = 0;
                    }
                }
                // As long as the Enter key is not pressed it will loop the menu
            } while (keyPressed != ConsoleKey.Enter);

            return menuSelect;
        }
    }
}
