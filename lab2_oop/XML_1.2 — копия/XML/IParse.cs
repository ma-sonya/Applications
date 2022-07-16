using System.Collections.Generic;

namespace XML
{
    interface IParse
    {
        List<Search> AnalizeFile(Search mySearch, string path);
    }
}
