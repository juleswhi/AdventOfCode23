fn main() {
    let args: Vec<String> = std::env::args().collect();

    if args.len() != 2 {
        println!("Usage: cargo run -- <filename>");
        return;
    }

    let file = std::fs::read_to_string(&args[1]).unwrap();

    let lines = file.split("\n").collect::<Vec<&str>>();

    let mut i: isize = 0;
    let mut j: isize;
    let mut nums: Vec<isize> = Vec::new();

    loop {
        if i as usize >= lines.len() {
            break;
        }

        nums.clear();
        j = 0;

        loop {
            if j >= lines[i].chars().count() {
                break;
            }

            for x in vec![-1, 0, 1] {
                for y in vec![-1, 0, 1] {
                    if (check_direction(&lines, (i + x) as isize, (j + y) as isize)) {
                        nums.push(get_num(&lines, (i + x) as isize, (j + y) as isize));
                    }
                }
            }

            // Distinct from nums

            j += 1;
        }
        i += 1;
    }

    for num in nums {
        println!("{num}");
    }
}

fn check_direction(lines: &Vec<&str>, x: isize, y: isize) -> bool {
    if lines[x as usize]
        .chars()
        .nth(y as usize)
        .unwrap()
        .is_digit(10)
    {
        return true;
    }
    false
}

fn get_num(lines: &Vec<&str>, x: isize, y: isize) -> isize {
    lines[x as usize].chars().nth(y as usize).unwrap() as isize
}
