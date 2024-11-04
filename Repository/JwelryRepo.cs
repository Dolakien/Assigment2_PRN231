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

        public List<SilverJewelry> GetJwelries()
            => JwelryDAO.Instance.GetSilverJewelries();

        public SilverJewelry GetJwelry(string id)
            => JwelryDAO.Instance.GetSilverJewelry(id);

        public bool removeJwelry(string jwelryId)
            => JwelryDAO.Instance.removeJwelry(jwelryId);

        public List<SilverJewelry> SearchSilverJewelry(string? nameSearchTerm, decimal? metalWeight)
            => JwelryDAO.Instance.SearchSilverJewelry(nameSearchTerm, metalWeight);

        public bool updateJwelry(SilverJewelry silverJewelry)
            => JwelryDAO.Instance.updateJwekry(silverJewelry);

    }
}
