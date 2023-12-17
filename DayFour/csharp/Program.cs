using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

string[] inp = File.ReadAllLines("input");

// Read in all the scratch cards
foreach (var line in inp)
{
    var cards = line.Split(": ")[1];
    var winning = cards.Split(" | ")[0].Split(" ").ToList();
    winning.ForEach(x => x.ToString().Trim());
    winning.Select(x => int.Parse($"{x}"));

    var hand = cards.Split(" | ")[1].Split(" ").ToList();
    hand.ForEach(x => x.ToString().Trim());
    hand.Select(x => int.Parse($"{x}"));

    string index = "";

    foreach (var car in line.Split(": ")[0])
    {
        if (char.IsDigit(car))
        {
            index += car;
        }
    }

    int currentIndex = int.Parse(index);




    for (int i = 0; i < hand.Count; i++)
    {
        if (hand[i] == "") hand.Remove(hand[i]);
    }

    for (int i = 0; i < winning.Count; i++)
    {
        if (winning[i] == "") winning.Remove(winning[i]);
    }

    ScratchCard.Cards.Add(new ScratchCard(currentIndex, winning, hand) { Num = 1 });

}

foreach (var card in ScratchCard.Cards)
{
    int total = 0;
    foreach (var win in card.Winning)
    {
        foreach (var hand in card.Hand)
        {
            if (win != hand) continue;
            total += 1;
            break;
        }
    }

    for (int i = 1; i < total + 1; i++)
    {
        ScratchCard.AddCard(card.Index + i, card.Num);
    }

    Console.WriteLine($"Card {card.Index} Done, It generated, {card.Num * total} cards, Number of cards: {card.Num}, Number of matches: {total}");

}



foreach (var card in ScratchCard.Cards)
{
    Console.WriteLine(card.Index + ", " + card.Num);
}

Console.WriteLine($"Sum: {ScratchCard.Cards.Sum(x => x.Num)}");

class ScratchCard
{
    public ScratchCard(int Index, List<string> Winning, List<string> Hand)
    {
        this.Index = Index;
        this.Winning = Winning;
        this.Hand = Hand;
    }

    public static List<ScratchCard> Cards { get; set; } = new();
    public int Index { get; set; }
    public int Num { get; set; } = 1;
    public List<string> Winning { get; set; }
    public List<string> Hand { get; set; }
    public static void AddCard(int index, int times)
    {
        Cards[index - 1].Num += times;
    }
}