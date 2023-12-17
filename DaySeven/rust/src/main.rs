use std::cmp::PartialEq;
use std::cmp::*;
use std::collections::HashMap;
use std::hash::{Hash, Hasher};

fn main() {
    let args: Vec<String> = std::env::args().collect();
    if args.len() != 2 {
        println!("Usage: cargo run -- <filename>");
        return;
    }

    let lines: Vec<String> = match std::fs::read_to_string(&args[1]) {
        Ok(val) => val.split("\n").map(|x| x.to_string()).collect(),
        Err(err) => {
            println!("Could not read the file correctly: {err}");
            return;
        }
    };

    let mut hands: Vec<Hand> = Vec::new();

    for line in lines {
        let split: Vec<&str> = line.split(" ").collect();

        hands.push(Hand::from(
            &split[0],
            split[1].trim().parse::<usize>().unwrap(),
        ));
    }

    // hands.sort_by(|x, y| (x.hand_type as u8).cmp(&(y.hand_type as u8)));

    hands.sort_by(|x, y| {
        if x.hand_type == y.hand_type {
            let mut x_iter = x.cards.iter();
            let mut y_iter = y.cards.iter();
            loop {
                match (x_iter.next(), y_iter.next()) {
                    (Some(x_card), Some(y_card)) => {
                        let x_value = x_card.value();
                        let y_value = y_card.value();
                        if x_value != y_value {
                            return y_value.cmp(&x_value);
                        }
                    }
                    (None, None) => break,
                    (Some(_), None) => return Ordering::Less,
                    (None, Some(_)) => return Ordering::Greater,
                }
            }
            Ordering::Equal
        } else {
            (x.hand_type as u8).cmp(&(y.hand_type as u8))
        }
    });

    let mut sum = 0;
    for i in hands.iter().enumerate() {
        println!(
            "Index: {}, Bid: {}, Adding: {}, Sum: {}",
            i.0,
            i.1.bid,
            (i.0 + 1) * i.1.bid,
            sum
        );
        sum += (i.0 + 1) * i.1.bid;
    }

    println!("{sum}");
}

struct Hand {
    cards: Vec<CardType>,
    hand_type: HandType,
    bid: usize,
}

impl Hand {
    fn _new() -> Hand {
        Hand {
            cards: Vec::new(),
            hand_type: HandType::FiveKind,
            bid: 0,
        }
    }
    fn from(line: &str, bid: usize) -> Hand {
        let mut cards: Vec<CardType> = Vec::new();

        for card in line.chars() {
            let card_type: CardType = match card {
                'A' => CardType::A,
                'K' => CardType::K,
                'Q' => CardType::Q,
                'J' => CardType::J,
                'T' => CardType::T,
                _ => CardType::NUM(card as u8),
            };
            cards.push(card_type);
        }

        let cards_copy = cards.clone();
        let mut hand_type: HandType = HandType::HighCard;

        let mut counts: HashMap<CardType, usize> = HashMap::new();

        for card in cards {
            *counts.entry(card).or_insert(0) += 1;
        }

        if counts.values().any(|&count| count == 5) {
            hand_type = HandType::FiveKind;
        } else if counts.values().any(|&count| count == 4) {
            hand_type = HandType::FourKind;
        } else if counts.values().any(|&count| count == 3)
            && counts.values().any(|&count| count == 2)
        {
            hand_type = HandType::FullHouse;
        } else if counts.values().any(|&count| count == 3) {
            hand_type = HandType::ThreeKind;
        } else if counts.values().filter(|&count| *count == 2).count() == 2 {
            hand_type = HandType::TwoPair;
        } else if counts.values().any(|&count| count == 2) {
            hand_type = HandType::OnePair;
        }

        Hand {
            cards: cards_copy,
            hand_type,
            bid,
        }
    }
}

#[derive(Clone)]
enum CardType {
    A,
    K,
    Q,
    J,
    T,
    NUM(u8),
}

impl CardType {
    fn value(&self) -> u8 {
        match self {
            CardType::A => 1,
            CardType::K => 2,
            CardType::Q => 3,
            CardType::J => 4,
            CardType::T => 5,
            CardType::NUM(n) => *n,
        }
    }
}

impl PartialEq for CardType {
    fn eq(&self, other: &Self) -> bool {
        match (self, other) {
            (CardType::A, CardType::A) => true,
            (CardType::K, CardType::K) => true,
            (CardType::Q, CardType::Q) => true,
            (CardType::J, CardType::J) => true,
            (CardType::T, CardType::T) => true,
            (CardType::NUM(n1), CardType::NUM(n2)) => n1 == n2,
            _ => false,
        }
    }
}

impl Eq for CardType {}

impl Hash for CardType {
    fn hash<H: Hasher>(&self, state: &mut H) {
        match self {
            CardType::A => 0.hash(state),
            CardType::K => 1.hash(state),
            CardType::Q => 2.hash(state),
            CardType::J => 3.hash(state),
            CardType::T => 4.hash(state),
            CardType::NUM(n) => n.hash(state),
        }
    }
}

#[derive(Copy, Clone, PartialEq)]
enum HandType {
    FiveKind = 7,
    FourKind = 6,
    FullHouse = 5,
    ThreeKind = 4,
    TwoPair = 3,
    OnePair = 2,
    HighCard = 1,
}
