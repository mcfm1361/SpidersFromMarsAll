using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpidersFromMars
{
    public class Wall
    {

        public int left { get; set; }
        public int right { get; set; }
        public int bottom { get; set; }
        public int top { get; set; }

        public Wall(int pleft, int pbottom, int pright,  int ptop)
        {
            left = pleft;
            bottom = pbottom;
            right = pright;
            top = ptop;
        }

        public Wall(string wallSize)
        {
            if (ValidateWallSize(wallSize) == "")
            {
                List<string> wallSizeInputs = wallSize.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                left = 0;
                bottom = 0;
                right = int.Parse(wallSizeInputs[0]);
                top = int.Parse(wallSizeInputs[1]);
            }
        }

        public static string ValidateWallSize(string wallSize)
        {
            int tmpInt;
            List<string> wallSizeInputs = wallSize.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (wallSizeInputs.Count != 2)
                return "Must enter 2 numbers for wall size!";
            foreach (string wallSizeInput in wallSizeInputs)
            {
                if (!int.TryParse(wallSizeInput, out tmpInt))
                    return "Must enter 2 valid integers for wall size!"; 
                if (tmpInt <= 0)
                    return "Must enter 2 positive integers for wall size!";
            }
            return "";
        }

    }
}
