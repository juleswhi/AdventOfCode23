Dictionary<char, List<v2>> dirs = new() {
    {
        '|', new() {
        new(0, 1), new(0, -1),
    }
    },
    {
        '-', new() {
            new(1,0), new(-1, 0)
        }
    },
    {
        'L', new() {
            new(0,1), new(1, 0)
        }
    },
    {
        'J', new() {
            new(0,-1), new(-1, 0)
        }
    },
    {
        '7', new() {
            new(0, -1), new(-1, 0)
        }
    },
    {
        'F', new() {
            new(0, -1), new(1, 0)
        }
    }
};





var file = File.ReadAllLines("test").ToList();

char[,] map = new char[file[0].Length, file.Count];
v2 start = new(0, 0);


for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        map[i, j] = file[i][j];
        if (map[i, j] == 'S') start = new(i, j);
    }
}

List<(v2, int)> nums = new()
{
    (start, 0)
};

v2 prev = start;

get_initial(ref nums);
while (true)
{
    var current = nums[^1].Item1;




    if (nums.Count > 10) break;
    // look in all directions.
    // Check if valid
}


foreach (var n in nums)
{
    Console.WriteLine($"{n.Item1},{n.Item2}");
}

Console.WriteLine($"{prev.x},{prev.y}");


void get_initial(ref List<(v2, int)> nums)
{
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            if (!dirs.TryGetValue(map[i, j], out List<v2>? v)) continue;

            foreach (var d in v)
            {
                if ((i + d.y != prev.x) || (j + d.x != prev.y)) continue;

                Console.WriteLine($"Char: {map[i + d.y, j + d.x]} Found: {map[i, j]}");

                prev = new(i, j);
                nums.Add((prev, nums[^1].Item2 + 1));
                return;
            }
        }
    }

}


record v2(int x, int y);