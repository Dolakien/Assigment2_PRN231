using BusinessObject.Models;
using DataAccessObject;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JwelryRepo : IJewlryRepo
    {
        public bool addJwelry(SilverJewelry silverJewelry)
            => JwelryDAO.Instance.addJwelry(silverJewelry);

        public bool removeJwelry(SilverJewelry silverJewelry)
            => JwelryDAO.Instance.updateJwekry(silverJewelry);

        public bool updateJwelry(string jwelryId)
            => JwelryDAO.Instance.removeJwelry(jwelryId);

    }
}
