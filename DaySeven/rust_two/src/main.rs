use std::iter::zip;

fn main() {
    let args: Vec<String> = std::env::args().collect();

    let lines: Vec<String> = match std::fs::read_to_string(&args[1]) {
        Ok(val) => val
            .split("\n")
            .map(|x| x.to_string())
            .collect::<Vec<String>>(),
        Err(err) => {
            println!("There as an err: {}", err);
            return;
        }
    };

    let mut hands: Vec<&str> = Vec::new();
    let mut bids: Vec<usize> = Vec::new();

    for line in &lines {
        hands.push(line.split(" ").nth(0).unwrap());
        bids.push(line.split(" ").nth(1).unwrap().parse::<usize>().unwrap());
    }

    let mut hand_bid_pair: Vec<(&str, usize)> = zip(hands, bids).collect();

    hand_bid_pair.sort_by()
}

enum HandType {
    HighCard = 1,
    OnePair = 2,
    TwoPair = 3,
    ThreeKind = 4,
    FullHouse = 5,
    FourKind = 6,
    FiveKind = 7,
}

impl HandType {
    fn value(&self) -> u8 {
        match self {
            Self::HighCard => 1,
            Self::OnePair => 2,
            Self::TwoPair => 3,
            Self::ThreeKind => 4,
            Self::FullHouse => 5,
            Self::FourKind => 6,
            Self::FiveKind => 7,
        }
    }
}

enum CardValues {
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    T = 10,
    J = 11,
    Q = 12,
    K = 13,
    A = 14,
}
