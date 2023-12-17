var file = File.ReadAllLines("input.txt");

long task_one(string[] lines)
{
    long sum = 0;

    foreach (var line in lines)
    {
        List<List<int>> levels = new()
    {
        line.Split(" ").ToList().Select(x => int.Parse(x)).ToList()
    };

        while (true)
        {
            if (levels[^1].All(x => x == 0))
            {
                break;
            }
            levels.Add(get_diffs(levels[^1]));
        }




        // Console.WriteLine(levels.Count);
        for (var i = 1; i <= levels.Count; i++)
        {
            // Console.WriteLine($"{levels[^i][^1]} + {levels[^(i == 1 ? 1 : i - 1)][^2]}");
            levels[^i].Add(levels[^i][^1] + levels[^(i == 1 ? 1 : i - 1)][^1]);
        }

        foreach (var list in levels)
        {
            foreach (var num in list)
            {
                Console.Write($"{num},");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        sum += levels[0][^1];


    }

    return sum;
}

long task_two(string[] lines)
{
    long sum = 0;
    foreach (var line in lines)
    {
        List<List<int>> levels = new()
        {
            line.Split(" ").ToList().Select(x => int.Parse(x)).ToList()
        };

        while (true)
        {
            if (levels[^1].All(x => x == 0))
            {
                break;
            }
            levels.Add(get_diffs(levels[^1]));
        }

        for (var i = 1; i <= levels.Count; i++)
        {
            levels[^i].Insert(0, levels[^i][0] - levels[^(i == 1 ? 1 : i - 1)][0]);
        }

        foreach (var list in levels)
        {
            foreach (var num in list)
            {
                Console.Write($"{num},");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        sum += levels[0][0];

    }
    return sum;
}

Console.WriteLine($"Task One: {task_one(file)}");
Console.WriteLine($"Task Two: {task_two(file)}");

List<int> get_diffs(List<int> nums)
{
    List<int> l1 = new();

    for (var i = 0; i < nums.Count - 1; i++)
    {
        l1.Add(nums[i + 1] - nums[i]);
    }

    return l1;
}