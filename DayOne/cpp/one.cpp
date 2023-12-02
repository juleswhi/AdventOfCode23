#include <iostream>
#include <fstream>
#include <stdint.h>
#include <string>

int main(int argc, char *argv[])
{
    if (argc != 2)
    {
        printf("Usage: %s filename", argv[0]);
        return 0;
    }

    std::ifstream infile(argv[1]);

    uint16_t running_total = 0;

    std::string line;
    while (std::getline(infile, line))
    {
        uint8_t leftNum = -1;
        uint8_t rightNum = -1;

        for (int i = 0; i < line.length(); i++)
        {
            if (std::isdigit(line[i]))
            {
                if (leftNum == -1)
                {
                    leftNum = line[i] - '0';
                    rightNum = line[i] - '0';
                }
                else
                    rightNum = line[i] = '0';
            }
        }
        running_total += (leftNum * 10) + rightNum;
    }

    std::cout << running_total << std::endl;

    return 0;
}
