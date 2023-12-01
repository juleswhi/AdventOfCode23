int runningTotal = 0;
string[] inp = File.ReadAllLines("input.txt");

Dictionary<string, int> words = new() {
    { "one", 1 },
    { "two", 2 },
    { "three", 3},
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9}
};

List<int> nums = new();
for (int i = 0; i < inp.Length; i++)
{
    nums.Clear();
    for (int j = 0; j < inp[i].Length; j++)
    {
        foreach (var word in words)
        {
            if (word.Key[0] != inp[i][j]) continue;

            for (int k = 0; k < word.Key.Length; k++)
            {
                if (j + k >= inp[i].Length) break;
                if (inp[i][j + k] != word.Key[k]) break;

                if (k == word.Key.Length - 1)
                {
                    nums.Add(word.Value);
                }
            }
        }
        if (int.TryParse($"{inp[i][j]}", out int num))
        {
            nums.Add(num);
        }
    }
    int.TryParse($"{nums[0]}{nums[^1]}", out int final);
    runningTotal += final;
}

Console.WriteLine(runningTotal);
