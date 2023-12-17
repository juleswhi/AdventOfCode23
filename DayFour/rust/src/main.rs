fn main() {
    let args: Vec<String> = std::env::args().collect();

    if args.len() != 2 {
        println!("Usage: cargo run -- <filename>");
        return;
    }

    let file: String = std::fs::read_to_string(&args[1]).unwrap();

    let lines: Vec<&str> = file.split("\n").collect();

    for line in lines {
        let mut line_split = line.split(": ");

        // let index = &line_split.nth(0).unwrap();
        let full_cards = &line_split
            .nth(1)
            .unwrap()
            .split(" | ")
            .map(|x| x.trim())
            .collect::<Vec<&str>>();

        let winning = &full_cards[0];
        let ours = &full_cards[1];

        

    }
}
