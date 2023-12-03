
string[] inp = File.ReadAllLines("input");
int runningTotal = 0;

List<int> currentNums = new();
for (int i = 0; i < inp.Length; i++)
{
    for (int j = 0; j < inp[i].Length; j++)
    {
        // Must be a symbol
        currentNums.Clear();
        if (!(!int.TryParse($"{inp[i][j]}", out int _) && inp[i][j] != '.')) continue;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (FindFullNumber(i + x, j + y, out int output))
                {
                    currentNums.Add(output);
                }
            }
        }

        if (currentNums.Distinct().Count() == 2 && inp[i][j] == '*')
        {
            runningTotal += currentNums.Distinct().ToList()[0] * currentNums.Distinct().ToList()[1];
        }
    }


}

Console.WriteLine(runningTotal);


bool FindFullNumber(int i, int j, out int output)
{
    string num = "";
    if (int.TryParse($"{inp[i][j]}", out int _))
    {
        num = $"{inp[i][j]}";
    }
    else
    {
        output = -1;
        return false;
    }
    if (int.TryParse($"{inp[i][j - 1]}", out int _))
    {
        num = $"{inp[i][j - 1]}{num}";
        if (int.TryParse($"{inp[i][j - 2]}", out int _))
        {
            num = $"{inp[i][j - 2]}{num}";
            output = int.Parse(num);
            return true;
        }
        else if (int.TryParse($"{inp[i][j + 1]}", out int _))
        {
            num += $"{inp[i][j + 1]}";
            output = int.Parse(num);
            return true;
        }
        output = int.Parse(num);
        return true;
    }
    else
    { // No num to left -- Must be to right
        if (int.TryParse($"{inp[i][j + 1]}", out int _))
        {
            num += $"{inp[i][j + 1]}";
            if (int.TryParse($"{inp[i][j + 2]}", out int _))
            {
                num += $"{inp[i][j + 2]}";
            }
            output = int.Parse(num);
            return true;
        }
        else
        {
            output = int.Parse(num);
            return true;
        }
    }
}