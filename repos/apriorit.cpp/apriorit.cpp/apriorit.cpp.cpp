#include <iostream>
#include <vector>
#include <string>

using namespace std;

vector<string> series(const string& word, size_t len)
{
    vector<string> res;
    if (len > word.length())
    {
        return res;
    }

    int iterations = word.length() - len;

    for (int i=0; i < iterations; i++)
    {
        res.push_back(word.substr(i, len + i));
    }

    return res;
}

int main()
{
    vector<string> a = series("12345", 3);
    for (int i = 0; i < 3; i++)
    {
        cout << a[i] << endl;
    }
    return 0;
}