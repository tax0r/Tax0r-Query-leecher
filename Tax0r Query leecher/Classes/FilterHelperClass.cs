using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax0r_Query_leecher.Classes
{
    class FilterHelperClass
    {

        List<string> filters = new List<string>();

        public void addFilters(string filter)
        {
            filters.Add(filter);
        }

        public Boolean isFiltered(string url)
        {
            foreach(string filter in filters)
            {
                if (!url.Contains(filter))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }
}
