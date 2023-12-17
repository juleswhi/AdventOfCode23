fn main() {
    let args: Vec<String> = std::env::args().collect();

    if args.len() != 2 {
        println!("Usage: cargo run -- <filename>");
        return;
    }

    let lines: Vec<String> = match std::fs::read_to_string(&args[1]) {
        Ok(val) => val
            .split("\n")
            .map(|x| x.to_string())
            .collect::<Vec<String>>(),
        Err(err) => {
            println!("Could not read the file correctly {err}");
            return;
        }
    };

    let time: usize = parse_line(&lines[0]).unwrap();
    let distance: usize = parse_line(&lines[1]).unwrap();

    let mut total: usize = 0;

    for i in 0..=time {
        if ((time - i) * i) > distance {
            total += 1;
        }
    }

    println!("{}", total);
}

fn parse_line(line: &String) -> Result<usize, &'static str> {
    line.split(":")
        .nth(1)
        .ok_or("Error reading the file")
        .and_then(|x| {
            x.split_whitespace()
                .filter(|x| !x.is_empty())
                .collect::<Vec<&str>>()
                .join("")
                .parse::<usize>()
                .map_err(|_| "Failed to parse")
        })
}
