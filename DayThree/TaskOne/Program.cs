
string[] inp = File.ReadAllLines("input");
int runningTotal = 0;

List<int> currentNums = new();
for (int i = 0; i < inp.Length; i++)
{
    for (int j = 0; j < inp[i].Length; j++)
    {
        // Must be a symbol
        currentNums.Clear();
        if (!int.TryParse($"{inp[i][j]}", out int _) && inp[i][j] != '.')
        {
            if (FindFullNumber(i - 1, j - 1, out int northwest))
            {
                currentNums.Add(northwest);
            }
            if (FindFullNumber(i, j - 1, out int west))
            {
                currentNums.Add(west);
            }
            if (FindFullNumber(i + 1, j - 1, out int southwest))
            {
                currentNums.Add(southwest);
            }
            if (FindFullNumber(i + 1, j, out int south))
            {
                currentNums.Add(south);
            }
            if (FindFullNumber(i + 1, j + 1, out int southeast))
            {
                currentNums.Add(southeast);
            }
            if (FindFullNumber(i, j + 1, out int east))
            {
                currentNums.Add(east);
            }
            if (FindFullNumber(i - 1, j + 1, out int northeast))
            {
                currentNums.Add(northeast);
            }
            if (FindFullNumber(i - 1, j, out int north))
            {
                currentNums.Add(north);
            }

            if (currentNums.Distinct().Count() == 2 && inp[i][j] == '*')
            {
                runningTotal += currentNums.Distinct().ToList()[0] * currentNums.Distinct().ToList()[1];
            }
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