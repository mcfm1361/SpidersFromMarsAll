using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpidersFromMars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Add comment to change code / allow another commit
            // Another comment to test sidebranch1 commits
            string TerminateCmd = "";

            do
            {
                bool awaitFirstInputThisItem = true;
                string wallSize = "";
                do
                {
                    if (!awaitFirstInputThisItem)
                        Console.WriteLine(Wall.ValidateWallSize(wallSize));
                    Console.WriteLine("Enter Wall Size as X-Y coordinate of Bottom-Left Top=Right e.g. 0 0 10 15 :");
                    wallSize = Console.ReadLine();
                    awaitFirstInputThisItem = false;
                } while (Wall.ValidateWallSize(wallSize) != "");
                Wall wall = new Wall(wallSize);

                awaitFirstInputThisItem = true;
                string spiderPosition = "";
                do
                {
                    if (!awaitFirstInputThisItem)
                        Console.WriteLine(Spider.CheckSpiderPosition(spiderPosition, wall));
                    Console.WriteLine("Enter Spider Position as X-Y coordinate and Orientation e.g. 5 7 Left :");
                    Console.WriteLine("(Note - Orientation must be Left, Right, Up or Down)");
                    spiderPosition = Console.ReadLine();
                    awaitFirstInputThisItem = false;
                } while (Spider.CheckSpiderPosition(spiderPosition, wall) != "");
                Spider spider = new Spider(spiderPosition, wall);

                awaitFirstInputThisItem = true;
                string instructionList = "";
                do
                {
                    if (!awaitFirstInputThisItem)
                        Console.WriteLine(Spider.CheckAllCommands(instructionList, wall));
                    Console.WriteLine("Enter Instruction List as string of characters F,L or R e.g. FFLFRFLFF :");
                    instructionList = Console.ReadLine();
                    awaitFirstInputThisItem = false;
                } while (Spider.CheckAllCommands(instructionList, wall) != "");
                Console.WriteLine(spider.RunAllCommands(instructionList));

                Console.WriteLine("Enter 'x' to finish or any other key to retry");
                TerminateCmd = Console.ReadLine();

            } while (TerminateCmd != "x");

        }
    }
}
