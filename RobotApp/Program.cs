// See https://aka.ms/new-console-template for more information
using System.Text;
string workingDirectory = Environment.CurrentDirectory;
string[] lines = File.ReadAllLines($"{workingDirectory}\\commands.txt", Encoding.UTF8);
Robot robot = new Robot();

foreach (string line in lines)
{
    if (line.StartsWith("NEWTEST"))
    {
        robot = new Robot();
        Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\NEWTEST\\\\\\\\\\\\\\\\\\\\\\\\");
    }
    if (line.StartsWith('P'))
    {
        string[] parts = line.Split('|');
        if (parts.Length != 4)
        {
            continue;
        }
        if (!int.TryParse(parts[1], out var x) || !int.TryParse(parts[2], out var y))
        {
            continue;
        }
        switch (parts[3])
        {
            case "W":
                robot.Place(x, y, Direction.WEST);
                break;
            case "E":
                robot.Place(x, y, Direction.EAST);
                break;
            case "N":
                robot.Place(x, y, Direction.NORTH);
                break;
            case "S":
                robot.Place(x, y, Direction.SOUTH);
                break;
        }
    }
    else if (line.StartsWith('M'))
    {
        robot.Move();
    }
    else if (line.StartsWith('L'))
    {
        robot.Left();
    }
    else if (line.StartsWith('R'))
    {
        robot.Right();
    }
    else if (line.StartsWith("O"))
    {
        robot.Report();
    }
}

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Face { get; set; }

    public bool IsPlaced { get; set; }
    public void Place(int x, int y, Direction direction)
    {
        if (x < 0 || y < 0 || x > 5 || y > 5)
        {
            return;
        }
        X = x;
        Y = y;
        Face = direction;
        IsPlaced = true;
    }

    public void Move()
    {
        if (!IsPlaced)
        {
            return;
        }

        switch (Face)
        {
            case Direction.NORTH:
                if (Y + 1 > 5)
                {
                    return;
                }
                else
                {
                    Y++;
                }
                break;
            case Direction.SOUTH:
                if (Y - 1 < 0)
                {
                    return;
                }
                else
                {
                    Y--;
                }
                break;
            case Direction.EAST:
                if (X + 1 > 5)
                {
                         return;
                }
                else
                {
                    X++;
                }
                break;
            case Direction.WEST:
                if (X - 1 < 0)
                {
                    return;
                }
                else
                {
                    X--;
                }
                break;
            default:
                return;
        }
    }

    public void Left()
    {
        if (!IsPlaced)
        {
            return;
        }
        switch (Face)
        {
            case Direction.EAST:
                Face = Direction.NORTH;
                break;
            case Direction.NORTH:
                Face = Direction.WEST;
                break;
            case Direction.WEST:
                Face = Direction.SOUTH;
                break;
            case Direction.SOUTH:
                Face = Direction.EAST;
                break;
        }
        
    }

    public void Right()
    {
        if (!IsPlaced)
        {
            return;
        }
        switch (Face)
        {
            case Direction.EAST:
                Face = Direction.SOUTH;
                break;
            case Direction.SOUTH:
                Face = Direction.WEST;
                break;
            case Direction.WEST:
                Face = Direction.NORTH;
                break;
            case Direction.NORTH:
                Face = Direction.EAST;
                break;
        }
    }

    public void Report()
    {
        if (!IsPlaced)
        {
            Console.WriteLine($"Robot Not Placed yet");
            return;
        }
        Console.WriteLine($"{X},{Y},{Face}");
    }
}

public enum Direction
{
    NORTH, SOUTH, WEST, EAST
}