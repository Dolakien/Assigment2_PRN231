using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class JwelryDAO
    {
        private SilverJewelry2023DbContext _context;
        private static JwelryDAO instance;

        public JwelryDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public static JwelryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JwelryDAO();
                }
                return instance;
            }
        }


        public List<SilverJewelry> GetSilverJewelries()
        {
            return _context.SilverJewelries.ToList();
        }

        public bool addJwelry(SilverJewelry silverJewelry) {
            bool result = false;
            SilverJewelry silverJewelry1 = this.GetSilverJewelry(silverJewelry.SilverJewelryId);
            if (silverJewelry1 == null)
            {
                try
                {
                    _context.SilverJewelries.Add(silverJewelry);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public bool updateJwekry(SilverJewelry silverJewelry) {
            bool result = false;
            SilverJewelry silverJewelry1 = this.GetSilverJewelry(silverJewelry.SilverJewelryId);
            if (silverJewelry1 != null)
            {
                try
                {
                    _context.Entry(silverJewelry1).CurrentValues.SetValues(silverJewelry);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public bool removeJwelry(string jwelryId) {
            bool result = false;
            SilverJewelry silverJewelry1 = this.GetSilverJewelry(jwelryId);
            if (silverJewelry1 != null)
            {
                try
                {
                    _context.SilverJewelries.Remove(silverJewelry1);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public SilverJewelry GetSilverJewelry(string jwelryId) {
            return _context.SilverJewelries.SingleOrDefault(m => m.SilverJewelryId.Equals(jwelryId));

        }

        public List<SilverJewelry> SearchSilverJewelry(string? nameSearchTerm, decimal? metalWeight)
        {
            var query = _context.SilverJewelries.AsQueryable();

            if (!string.IsNullOrEmpty(nameSearchTerm))
            {
                query = query.Where(s => s.SilverJewelryName.Contains(nameSearchTerm));
            }

            if (metalWeight.HasValue)
            {
                query = query.Where(s => s.MetalWeight == metalWeight.Value);
            }

            return query.ToList();
        }

    }
}
