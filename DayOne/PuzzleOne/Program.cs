int runningTotal = 0;
string[] inp = File.ReadAllLines("input.txt");

List<int> nums = new();
for (int i = 0; i < inp.Length; i++)
{
    nums.Clear();
    for (int j = 0; j < inp[i].Length; j++)
    {
        if (int.TryParse($"{inp[i][j]}", out int num))
        {
            nums.Add(num);
        }
    }
    int.TryParse($"{nums[0]}{nums[^1]}", out int final);
    runningTotal += final;
}

Console.WriteLine(runningTotal);
