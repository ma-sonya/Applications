#include <iostream>
#include <string>
#include <set>

using namespace std;

int factorial(int number)
{
    int res = 1;
    for (int i = 2; i <= number; i++)
    {
        res *= i;
    }
    return res;
}

int main()
{
    string word;
    cin >> word;
    int lenght = word.length();
    set<char> arr;
    int count = 0;
    for (int i = 0; i < lenght; i++)
    {
        if (arr.count(word[i]))
        {
            continue;
        }
        arr.insert(word[i]);
        count++;

    }

    return 0;
}
