using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.RDW.Data.Entities.ImportJson
{
    public static class Mapper
    {

        public static IEnumerable<string> GetMerken(this IEnumerable<Voertuig> voertuigen)
        {
            return voertuigen.Select(x => x.merk).Distinct().ToList();        }

        public static IEnumerable<string> GetVoertuigSoorten(this IEnumerable<Voertuig> voertuigen)
        {
            return voertuigen.Select(x => x.voertuigsoort).Distinct().ToList();
        }

        public static IEnumerable<(string merk, string handelsbenaming)> GetHandelsbenamingen(this IEnumerable<Voertuig> voertuigen)
        {
            var list = new List<(string merk, string handelsbenaming)>();

            foreach(var voertuig in voertuigen)
            {
                if (!list.Contains((voertuig.merk, voertuig.handelsbenaming)))
                {
                    list.Add((voertuig.merk, voertuig.handelsbenaming));
                }
            }

            return list;
        }
    }
}
