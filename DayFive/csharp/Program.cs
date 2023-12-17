string[] input = File.ReadAllLines("input");

List<long> _seeds = input[0].Split(": ")[1].Split(" ").ToList().Select(x => long.Parse(x)).ToList();
List<(long, long)> Seeds = new();

for (int i = 0; i < _seeds.Count; i += 2)
{
    Seeds.Add((_seeds[i], _seeds[i] + _seeds[i + 1]));
}

foreach (var seed in Seeds)
{
    Console.WriteLine(seed);
}


Dictionary<int, List<List<long>>> mapDict = new();

// Outer dictionary - MapType and inner dict 
// Second outer - Bounds and range

MapType mapType = 0;
foreach (var line in input)
{
    if (!mapDict.ContainsKey((int)mapType))
    {
        mapDict.Add((int)mapType, new());
    }
    if (line == "")
    {
        mapType++;
        continue;
    }

    if (!char.IsDigit(line[0]))
    {
        continue;
    }


    List<long> nums = line.Split(" ").Select(long.Parse).ToList();
    List<long> num = new()
    {
        nums[0],
        nums[1],
        nums[2]
    };

    /*
    Console.WriteLine($"Dest: {num[0]}");
    Console.WriteLine($"Source: {num[1]}");
    Console.WriteLine($"Range: {num[2]}");
    */

    mapDict[(int)mapType].Add(num);
}

List<long> finalNums = new();

for (int i = 0; i < 8; i++)
{
    for (int j = 0; j < Seeds.Count; j++)
    {
        for (int l = 0; l < mapDict[i].Count; l++)
        {
            for (long m = Seeds[j].Item1; m < Seeds[j].Item1 + Seeds[j].Item2; m++)
            {
                if (mapDict[i][l][1] <= m && m < mapDict[i][l][1] + mapDict[i][l][2])
                {
                    // Console.WriteLine($"Seed: {Seeds[j]} is within range: {mapDict[i][l][1]} and {mapDict[i][l][1] + mapDict[i][l][2]}");
                    // Console.WriteLine($"Source is: {mapDict[i][l][1]}, Dest is: {mapDict[i][l][0]}, Range is: {mapDict[i][l][2]}");
                    finalNums.Add(m - mapDict[i][l][1] + mapDict[i][l][0]);
                    break;
                    // Console.WriteLine($"Seeds has become: {Seeds[j]}");
                }
            }
        }
    }
}


Console.WriteLine(finalNums.Min());

public enum MapType
{
    None,
    Soil,
    Fertilizer,
    Water,
    Light,
    Temperature,
    Humidity,
    Location
}
