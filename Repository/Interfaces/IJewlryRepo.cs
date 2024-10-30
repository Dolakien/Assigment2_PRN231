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
        public bool addJwelry(SilverJewelry silverJewelry);
        public bool removeJwelry(SilverJewelry silverJewelry);
        public bool updateJwelry(string jwelryId);
    }
}
