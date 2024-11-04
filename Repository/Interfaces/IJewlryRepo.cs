using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IJewlryRepo
    {
        public SilverJewelry GetJwelry(string id);
        public List<SilverJewelry> GetJwelries();
        public bool addJwelry(SilverJewelry silverJewelry);
        public bool removeJwelry(string jwelryId);
        public bool updateJwelry(SilverJewelry silverJewelry);
        public List<SilverJewelry> SearchSilverJewelry(string? nameSearchTerm, decimal? metalWeight);
    }
}
