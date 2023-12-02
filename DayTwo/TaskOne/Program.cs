string[] file = File.ReadAllLines("input");

List<Game> games = new();

foreach (var line in file)
{
    games.Add(Game.ParseLine(line));
}

List<(Game, int)> successful = new();

foreach(var line in games)
{
    int red = 1;
    int green = 1;
    int blue = 1;

    Console.Write(line.ID + "; ");
    foreach(var game in line.Games)
    {
        Console.Write($"({game.Item1}, {game.Item2}, {game.Item3}) ");
        if (game.Item1.Colour == 'r')
            if (game.Item1.Num > red) red = game.Item1.Num;
        if (game.Item1.Colour == 'g')
            if (game.Item1.Num > green) green = game.Item1.Num;
        if (game.Item1.Colour == 'b')
            if(game.Item1.Num > blue) blue = game.Item1.Num;

        if (game.Item2.Colour == 'r')
            if (game.Item2.Num > red) red = game.Item2.Num;
        if (game.Item2.Colour == 'g')
            if (game.Item2.Num > green) green = game.Item2.Num;
        if (game.Item2.Colour == 'b')
            if(game.Item2.Num > blue) blue = game.Item2.Num;

        if (game.Item3.Colour == 'r')
            if (game.Item3.Num > red) red = game.Item3.Num;
        if (game.Item3.Colour == 'g')
            if (game.Item3.Num > green) green = game.Item3.Num;
        if (game.Item3.Colour == 'b')
            if(game.Item3.Num > blue) blue = game.Item3.Num;

    }
    Console.WriteLine();

    successful.Add((line, red * green * blue));

    Console.WriteLine($"Power for {line.ID}: {red * green * blue}"); 
}


Console.WriteLine();

Console.WriteLine($"Total: {successful.Sum(x => x.Item2)}");



class Game
{
    public int ID { get; set; }
    public List<(Cube, Cube, Cube)> Games { get; set; } = new();

    public static Game ParseLine(string str)
    {
        Game game = new();
        // Get the line num
        for (int i = 0; i < str.Length; i++)
        {
            if (int.TryParse($"{str[i]}", out int num))
            {
                if (int.TryParse($"{str[i + 1]}", out int numTwo))
                {
                    if (int.TryParse($"{str[i + 2]}", out int numThree))
                    {
                        game.ID = int.Parse($"{num}{numTwo}{numThree}");
                        break;
                    }
                    else
                    {
                        game.ID = int.Parse($"{num}{numTwo}");
                        break;
                    }
                }
                else
                {
                    game.ID = num;
                    break;
                }
            }
        }

        // Get the individual games
        var games = str.Split(":")[1].Split(";");

        List<Cube> cubez = new();

        foreach (var line in games)
        {
            cubez.Clear();
            var split = line.Split(",");

            for (int i = 0; i < split.Length; i++)
            {
                if (int.TryParse($"{split[i][2]}", out int res))
                {
                    cubez.Add(new Cube(int.Parse($"{split[i][1]}{res}"), split[i][4]));
                }
                else
                {
                    cubez.Add(new Cube(int.Parse($"{split[i][1]}"), split[i][3]));
                }
            }

            while (cubez.Count != 3)
            {
                cubez.Add(new Cube());
            }

            game.Games.Add((cubez[0], cubez[1], cubez[2]));
        }

        return game;
    }

}

struct Cube
{
    public override string ToString() =>
        $"{Num},{Colour}";

    public Cube(int num, char colour)
    {
        Num = num;
        Colour = colour;
    }
    public Cube()
    {
        Num = 0;
        Colour = 'n';
    }
    public int Num { get; set; }
    public char Colour { get; set; }

    public bool ValidateCube()
    {
        switch(Colour)
        {
            case 'r':
                if (Num > 12) return false;
                break;
            case 'g':
                if (Num > 13) return false;
                break;
            case 'b':
                if (Num > 14) return false;
                break;
            default:break;
        }

        return true;
    }
}