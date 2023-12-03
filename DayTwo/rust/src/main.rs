use std::env;
use std::fs;

fn main() {
    let args: Vec<String> = env::args().collect();

    if args.len() != 2 {
        println!("Usage: cargo run -- filename");
        return;
    }

    let file = fs::read_to_string(&args[1]).expect("Could not read the file.");

    let lines: Vec<&str> = file.split("\n").collect();

    let mut games: Vec<Game> = Vec::new();

    for line in lines {
        let mut current_game: Game = Game::new();
        let line_split = line.split(": ").collect::<Vec<&str>>();

        let chars: Vec<char> = line_split[0].chars().collect();

        let mut counter = 0;
        let mut nums: Vec<u8> = Vec::new();

        loop {
            if counter == chars.len() {
                break;
            }

            if chars[counter].is_digit(10) {
                nums.push(chars[counter] as u8);
            }

            counter += 1;
        }

        let num_string: String = nums.iter().map(|&x| x as char).collect::<String>();

        if !num_string.is_empty() {
            current_game.index = num_string.parse().unwrap();
        }

        let mut cubes: Vec<CubeCollection> = Vec::new();

        let game_sets: Vec<&str> = line_split[1].split("; ").collect();

        for set in game_sets {
            let indivdual_cube: Vec<&str> = set.split(", ").collect();
        }

        /*
        cubes.push(CubeCollection {
            red: (),
            green: (),
            blue: (),
        });
        */

        games.push(current_game);
    }

    for game in games {
        println!("{}", game.index);
    }
}

struct Game {
    index: u8,
    games: Vec<CubeCollection>,
}

impl Game {
    fn new() -> Game {
        Game {
            index: 0,
            games: Vec::new(),
        }
    }
}

struct CubeCollection {
    red: u8,
    green: u8,
    blue: u8,
}

impl CubeCollection {
    fn new() -> Game {
        Game {
            index: 0,
            games: Vec::new(),
        }
    }
}
