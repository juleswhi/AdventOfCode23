fn main() {
    let args: Vec<String> = std::env::args().collect();
    let file: String = std::fs::read_to_string(&args[1]).unwrap();
    let lines: Vec<&str> = file.split("\n").collect();
}
