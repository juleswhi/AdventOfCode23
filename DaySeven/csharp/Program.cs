

bool solve(string s1, string s2)
{

    return false;
}



var lines = File.ReadAllLines("test").ToList();

List<(string, int)> hands = new();
foreach (var line in lines)
{
    hands.Add((line.Split(" ")[0], int.Parse(line.Split(" ")[1])));
}


for (int i = 0; i < hands.Count - 1; i++)
{
    for (int j = 0; j < hands[i].Item1.Length; j++)
    {
        var str = hands[i].Item1;
        var next_str = hands[i + 1].Item1;

        Console.WriteLine($"{str} has type {get_type(str)}, and {next_str} has type {get_type(next_str)}");
        if (get_type(str) != get_type(next_str))
        {
            Console.WriteLine("Not the same, can order by type");
            if ((int)get_type(str) > (int)get_type(next_str))
            {
                (hands[i], hands[i + 1]) = (hands[i + 1], hands[i]);
            }
        }
        else
        {
            Console.WriteLine("The same");
            CardVal v1 = hands[i].Item1[j] switch
            {
                'A' => CardVal.A,
                'K' => CardVal.K,
                'Q' => CardVal.Q,
                'J' => CardVal.J,
                'T' => CardVal.T,
                var val => (CardVal)int.Parse(val.ToString())
            };

            CardVal v2 = hands[i + 1].Item1[j] switch
            {
                'A' => CardVal.A,
                'K' => CardVal.K,
                'Q' => CardVal.Q,
                'J' => CardVal.J,
                'T' => CardVal.T,
                var val => (CardVal)int.Parse(val.ToString())
            };



            if (v1 > v2)
            {
                // Console.WriteLine($"{v1} is bigger than {v2}");
                (hands[i], hands[i + 1]) = (hands[i + 1], hands[i]);
            }
        }
        // Sort by nu 
    }
}

foreach (var i in hands)
{
    Console.WriteLine(i);
}

HandType get_type(string str)
{
    Dictionary<char, int> count = new();
    foreach (var card in str)
    {
        if (count.ContainsKey(card))
        {
            count[card]++;
        }
        else
        {
            count.Add(card, 1);
        }
    }

    if (str.ToList().Any(x => count[x] == 5))
    {
        return HandType.FiveKind;
    }
    else if (str.ToList().Any(x => count[x] == 4))
    {
        return HandType.FourKind;
    }
    else if (str.ToList().Any(x => count[x] == 3) && str.ToList().Any(x => count[x] == 2))
    {
        return HandType.FullHouse;
    }
    else if (str.ToList().Any(x => count[x] == 3))
    {
        return HandType.ThreeKind;
    }
    else if (str.ToList().FindAll(x => count[x] == 2).Count == 2)
    {
        return HandType.TwoPair;
    }
    else if (str.ToList().Any(x => count[x] == 2))
    {
        return HandType.OnePair;
    }

    return HandType.HighCard;
}

enum HandType
{
    HighCard = 1,
    OnePair = 2,
    TwoPair = 3,
    ThreeKind = 4,
    FullHouse = 5,
    FourKind = 6,
    FiveKind = 7,
}

enum CardVal
{
    A = 14,
    K = 13,
    Q = 12,
    J = 11,
    T = 10,
    Nine = 9,
    Eight = 8,
    Seven = 7,
    Six = 6,
    Five = 5,
    Four = 4,
    Three = 3,
    Two = 2
}
