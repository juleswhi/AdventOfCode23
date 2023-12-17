import argparse
from typing import List
from enum import Enum


class HandType(Enum):
    HIGH_CARD = 1
    ONE_PAIR = 2
    TWO_PAIR = 3
    THREE_KIND = 4
    FULL_HOUSE = 5
    FOUR_KIND = 6
    FIVE_KIND = 7


CARD_VALUES = {
    "2": 2,
    "3": 3,
    "4": 4,
    "5": 5,
    "6": 6,
    "7": 7,
    "8": 8,
    "9": 9,
    "T": 10,
    "J": 11,
    "Q": 12,
    "K": 13,
    "A": 14,
}

TYPE_VALUES = {
    HandType.HIGH_CARD: 1,
    HandType.ONE_PAIR: 2,
    HandType.TWO_PAIR: 3,
    HandType.THREE_KIND: 4,
    HandType.FULL_HOUSE: 5,
    HandType.FOUR_KIND: 6,
    HandType.FIVE_KIND: 7,
}

JOKER = "J"
JOKER_VALUE = 1


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("input_file")
    args = parser.parse_args()

    with open(args.input_file, "r") as file:
        lines = file.read().splitlines()

    print(f"part 1 solution: {part_one(lines)}")
    print(f"part 2 solution: {part_two(lines)}")


def part_one(lines: List[str]) -> int:
    hands = []
    bids = []
    for line in lines:
        hand, bid = line.split(" ")
        hands.append(hand)
        bids.append(int(bid))

    hand_bid_pairs = list(zip(hands, bids))
    hand_bid_pairs.sort(key=lambda pair: hand_to_score(pair[0]))

    total = 0
    for i, (_, bid) in enumerate(hand_bid_pairs):
        rank = i + 1
        total += rank * bid

    return total


def part_two(lines: List[str]) -> int:
    hands = []
    bids = []
    for line in lines:
        hand, bid = line.split(" ")
        hands.append(hand)
        bids.append(int(bid))

    hand_bid_pairs = list(zip(hands, bids))
    hand_bid_pairs.sort(key=lambda pair: hand_to_score(pair[0], True))

    total = 0
    for i, (_, bid) in enumerate(hand_bid_pairs):
        rank = i + 1
        total += rank * bid

    return total


# map each hand to a score such that better hands have higher scores
def hand_to_score(hand: str, use_joker=False) -> int:
    hand_type = get_hand_type(hand, use_joker)
    hand_tiebreaker = get_hand_tiebreaker(hand, use_joker)

    max_tiebreaker = get_hand_tiebreaker("AAAAA")
    score = TYPE_VALUES[hand_type] * max_tiebreaker + hand_tiebreaker
    return score


def get_hand_type(hand: str, use_joker=False) -> HandType:
    hand_counts = {}
    if use_joker:
        joker_count = 0
        for card in hand:
            if card == JOKER:
                joker_count += 1
            else:
                hand_counts[card] = hand_counts.get(card, 0) + 1

        if joker_count == 5:
            hand_counts["A"] = 5
        else:
            max_count = 0
            max_card = "A"
            for card, count in hand_counts.items():
                if count > max_count:
                    max_count = count
                    max_card = card

            hand_counts[max_card] += joker_count
    else:
        for card in hand:
            hand_counts[card] = hand_counts.get(card, 0) + 1

    if len(hand_counts) == 1:
        return HandType.FIVE_KIND

    if len(hand_counts) == 2:
        count = list(hand_counts.values())[0]
        if count == 1 or count == 4:
            return HandType.FOUR_KIND
        else:
            return HandType.FULL_HOUSE

    if len(hand_counts) == 3:
        for count in hand_counts.values():
            if count == 2:
                return HandType.TWO_PAIR

            if count == 3:
                return HandType.THREE_KIND

    if len(hand_counts) == 4:
        return HandType.ONE_PAIR

    return HandType.HIGH_CARD


# maps each hand to a tiebreaker between 0 and 10^10
def get_hand_tiebreaker(hand: str, use_joker=False) -> int:
    values = []
    for card in hand:
        if use_joker and card == JOKER:
            values.append(JOKER_VALUE)
        else:
            values.append(CARD_VALUES[card])

    tiebreaker = 0
    for value in values:
        tiebreaker = 100 * tiebreaker + value
    return tiebreaker


if __name__ == "__main__":
    main()