using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpidersFromMars
{
    public class Spider
    {
        public int xPos { get; set; }
        public int yPos { get; set; }
        private orientation _currOrientation;
        private int _currFrontDegreesFromUpward;
        public Wall myWall { get; set; }

        public Spider(int pxPos, int pyPos, orientation pOrientation, Wall pWall)
        {
            xPos = pxPos;
            yPos = pyPos;
            _currOrientation = pOrientation;
            switch (_currOrientation)
            {
                case orientation.up:
                    _currFrontDegreesFromUpward = 0;
                    break;
                case orientation.left:
                    _currFrontDegreesFromUpward = 270;
                    break;
                case orientation.right:
                    _currFrontDegreesFromUpward = 90;
                    break;
                case orientation.down:
                    _currFrontDegreesFromUpward = 180;
                    break;
            }
            myWall = pWall;
        }

        public Spider(string spiderPosition, Wall pwall)
        {
            if (Spider.CheckSpiderPosition(spiderPosition, pwall) == "")
            {
                List<string> spiderPositionParts = spiderPosition.Split(' ').ToList();
                xPos = int.Parse(spiderPositionParts[0]);
                yPos = int.Parse(spiderPositionParts[1]);
                switch (spiderPositionParts[2].ToUpper())
                {
                    case "LEFT":
                        _currOrientation = orientation.left;
                        _currFrontDegreesFromUpward = 270;
                        break;
                    case "RIGHT":
                        _currOrientation = orientation.right;
                        _currFrontDegreesFromUpward = 90;
                        break;
                    case "UP":
                        _currOrientation = orientation.up;
                        _currFrontDegreesFromUpward = 0;
                        break;
                    case "DOWN":
                        _currOrientation = orientation.down;
                        _currFrontDegreesFromUpward = 180;
                        break;
                }
            }
            else
            {
                // Default orientation is Up
                _currOrientation = orientation.up;
                _currFrontDegreesFromUpward = 0;
            }
            myWall = pwall;
        }

        public static string CheckSpiderPosition(string spiderPosition, Wall pwall)
        {
            int tmpInt;
            List<string> spiderPositionParts = spiderPosition.Split(' ').ToList();
            List<string> validOrientation = new List<string>() { "LEFT", "RIGHT", "UP", "DOWN" };
            spiderPositionParts.RemoveAll(s => s.Trim() == "");
            if (spiderPositionParts.Count != 3)
                return "Invalid input - please try again";
            if (!int.TryParse(spiderPositionParts[0], out tmpInt))
                return "Invalid X coordinate " + spiderPositionParts[0] + " in input - must be valid integer";
            if (tmpInt < pwall.left || tmpInt > pwall.right)
                return "Invalid X coordinate " + spiderPositionParts[0] + " in input - must be within wall left/right " + pwall.left.ToString() + "/" + pwall.right.ToString();
            if (!int.TryParse(spiderPositionParts[1], out tmpInt))
                return "Invalid Y coordinate " + spiderPositionParts[1] + " in input - must be valid integer";
            if (tmpInt < pwall.bottom || tmpInt > pwall.top)
                return "Invalid Y coordinate " + spiderPositionParts[1] + " in input - must be within wall bottom/top " + pwall.bottom.ToString() + "/" + pwall.top.ToString();
            if (!validOrientation.Contains(spiderPositionParts[2].ToUpper()))
                return "Invalid Orientation " + spiderPositionParts[2] + " in input - must be Left, Right, Up or Down";
            return "";
        }

        public string MoveSpider(char turnDirection, bool test)
        {
            switch (turnDirection.ToString().ToUpper())
            {
                case "L":
                    _currFrontDegreesFromUpward = (_currFrontDegreesFromUpward == 0) ? 270 : (_currFrontDegreesFromUpward - 90);
                    break;
                case "R":
                    _currFrontDegreesFromUpward = (_currFrontDegreesFromUpward == 270) ? 0 : (_currFrontDegreesFromUpward + 90);
                    break;
                case "F":
                    if (!test)
                    {
                        switch (_currFrontDegreesFromUpward)
                        {
                            case 0:
                                if (yPos < myWall.top)
                                    yPos += 1;
                                else
                                    return "Whoops - Spider went over top of wall!";
                                break;
                            case 90:
                                if (xPos < myWall.top)
                                    xPos += 1;
                                else
                                    return "Whoops - Spider went off right of wall!";
                                break;
                            case 180:
                                if (yPos > myWall.bottom)
                                    yPos -= 1;
                                else
                                    return "Whoops - Spider went off bottom of wall!";
                                break;
                            case 270:
                                if (xPos > myWall.left)
                                    xPos -= 1;
                                else
                                    return "Whoops - Spider went off left of wall!";
                                break;
                        }

                    }
                    break;
                default:
                    return "Invalid Turn / Move Command " + turnDirection.ToString() + " - must be F, R or L";
            }
            if (!test)
                switch (_currFrontDegreesFromUpward)
                {
                    case 0:
                        _currOrientation = orientation.up;
                        break;
                    case 90:
                        _currOrientation = orientation.right;
                        break;
                    case 180:
                        _currOrientation = orientation.down;
                        break;
                    case 270:
                        _currOrientation = orientation.left;
                        break;
                }
            return "";
        }


        public static string CheckAllCommands(string commandList, Wall pwall)
        {
            List<char> turnCommands = new List<char>();
            turnCommands.AddRange(commandList);
            //Spider tmpSpider = new Spider(0,0,orientation.up,pwall);
            Spider tmpSpider = new Spider(0, 0, orientation.up, pwall);

            foreach (char turnCommand in turnCommands)
                if (tmpSpider.MoveSpider(turnCommand, true) != "")
                    return tmpSpider.MoveSpider(turnCommand, true);
            return "";
        }

        public string RunAllCommands(string commandList)
        {
            if (CheckAllCommands(commandList, myWall) != "")
                return CheckAllCommands(commandList, myWall);
            List<char> turnCommands = new List<char>();
            turnCommands.AddRange(commandList);
            foreach (char turnCommand in turnCommands)
                if (MoveSpider(turnCommand, false) != "")
                    return MoveSpider(turnCommand, false);

            return SpiderPositionDetails();
        }

        public string SpiderPositionDetails()
        {
            string retSpiderPositionDetails = "";
            try
            {
                retSpiderPositionDetails += xPos.ToString() + " ";
                retSpiderPositionDetails += yPos.ToString() + " ";
                switch (_currOrientation)
                {
                    case orientation.up:
                        retSpiderPositionDetails += "Up";
                        break;
                    case orientation.down:
                        retSpiderPositionDetails += "Down";
                        break;
                    case orientation.right:
                        retSpiderPositionDetails += "Right";
                        break;
                    case orientation.left:
                        retSpiderPositionDetails += "Left";
                        break;
                }
            }
            catch (Exception ex)
            {
                return "Error reporting Spider Position details - " + ex.Message;
            }

            return retSpiderPositionDetails;
        }

    }


    public enum orientation
    {
        up,
        down,
        right,
        left
    }

}
