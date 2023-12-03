fn main() {
    let args: Vec<String> = std::env::args().collect();

    if args.len() != 2 {
        println!("Usage: cargo run -- <filename>");
    }

    let file = std::fs::read_to_string(&args[1]).unwrap();

    let lines = file.split("\n");

    for i in lines.enumerate() {
        for j in i.1.chars().enumerate() {}
    }
}
