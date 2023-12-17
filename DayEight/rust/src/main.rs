use std::collections::HashMap;

fn main() {
    let args: Vec<String> = std::env::args().collect();
    let lines: Vec<String> = match std::fs::read_to_string(&args[1]) {
        Ok(val) => val.split("\n").map(|x| x.to_string()).collect(),
        Err(err) => {
            println!("Could not find file, {}", err);
            return;
        }
    };

    let mut dirs: Vec<bool> = Vec::new();
    for dir in lines[0].chars() {
        dirs.push(match dir {
            'R' => true,
            'L' => false,
            _ => break,
        })
    }

    let mut map: HashMap<String, (String, String)> = HashMap::new();

    for line in lines.iter().skip(2) {
        // println!("{}", line);
        let mut v1: String = String::new();
        let mut v2: String = String::new();
        let mut key: String = String::new();
        for c in line.chars() {
            if c.is_whitespace() {
                break;
            }
            key.push(c);
        }

        let mut white_num: u8 = 0;
        let mut found_comma: bool = false;

        for c in line.chars().skip_while(|x| {
            if x.is_whitespace() {
                white_num += 1;
            }
            if white_num == 2 {
                return false;
            }
            true
        }) {
            if c == ',' {
                found_comma = true;
                continue;
            }

            if found_comma {
                v2.push(c);
            } else {
                v1.push(c);
            }
        }

        v1 = v1
            .chars()
            .filter(|x| x.is_ascii_alphabetic() || x.is_digit(10))
            .collect();
        v2 = v2
            .chars()
            .filter(|x| x.is_ascii_alphabetic() || x.is_digit(10))
            .collect();

        map.insert(key, (v1, v2));
    }

    println!("Directions: ");

    for dir in &dirs {
        print!("{}, ", dir);
    }
    println!("");

    let mut counter: isize = 0;

    let mut current_keys: Vec<&String> = Vec::new();

    for kvp in &map {
        if kvp.0.chars().collect::<Vec<char>>()[2] == 'A' {
            current_keys.push(&kvp.0);
        }
    }

    println!("Amount of keys found initially: {}", current_keys.len());

    for kvp in &map {
        println!("Key: {}, ( {}, {} )", kvp.0, kvp.1 .0, kvp.1 .1);
    }

    let mut lcms: Vec<usize> = Vec::new();

    let mut dir_index: usize = 0;

    loop {
        let current_dir: &bool = &dirs[dir_index];

        if current_keys.iter().all(|x| match x.chars().nth(2) {
            Some('Z') => true,
            Some(_) => false,
            None => false,
        }) {
            break;
        }

        counter += 1;

        let mut indicies: Vec<usize> = Vec::new();

        for key in &mut current_keys {
            if key.chars().collect::<Vec<char>>()[2] == 'Z' {
                println!("Found a final: {}", key);
                indicies.push(counter as usize);
            }
            let next: &String = match current_dir {
                true => &map[*key].1,
                false => &map[*key].0,
            };
            *key = next;
        }

        current_keys = current_keys
            .iter()
            .enumerate()
            .filter(|x| !indicies.iter().any(|z| *z == x.0))
            .map(|x| *x.1)
            .collect::<Vec<&String>>();

        for i in indicies {
            lcms.push(i);
            println!("Pushed: {} to LCMS", i);
            println!(
                "Number left being tracked: {}\nNumber in lcms: {}",
                current_keys.len(),
                lcms.len()
            );
        }

        if dir_index == dirs.len() - 1 {
            dir_index = 0;
        } else {
            dir_index += 1;
        }
        // println!("{}", dir_index);
    }

    println!("{}", counter);
}
