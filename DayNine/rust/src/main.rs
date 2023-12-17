fn main() {
    let file = std::fs::read_to_string("test").unwrap();
    let lines: Vec<&str> = file.split("\n").collect();
    let mut sum = 0;
    for line in lines.into_iter() {
        let nums: Vec<i32> = line
            .split(" ")
            .into_iter()
            .map(|x| x.parse::<i32>().unwrap())
            .collect();

        let mut levels: Vec<Vec<i32>> = Vec::new();
        levels.push(nums);

        loop {
            if levels.last().unwrap().into_iter().all(|x| *x == 0) {
                break;
            }
            levels.push(get_diffs(levels.last().unwrap()));
        }
        let len: usize = (&levels).len();
        for i in 1..len {
            let l1 = levels[len - 1][0];
            let l2 = levels[len - if i == 1 { 1 } else { i - 1 }][0];
            levels[len - 1].insert(0, l1 - l2)
        }

        for i in &levels {
            for j in i.iter() {
                print!("{},", j);
            }
            println!();
        }

        sum += levels[0][0];
    }

    println!("Sum: {sum}");
}

fn get_diffs(nums: &Vec<i32>) -> Vec<i32> {
    let mut v1: Vec<i32> = Vec::new();

    for i in 0..nums.len() - 1 {
        v1.push(nums[i + 1] - nums[i]);
    }

    v1
}
