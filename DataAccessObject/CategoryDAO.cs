using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class CategoryDAO
    {
        private SilverJewelry2023DbContext _context;
        private static CategoryDAO instance;

        public CategoryDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        public Category GetCategory(string id)
        {
            return _context.Categories.SingleOrDefault(m => m.CategoryId.Equals(id));
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public bool addCategory(Category category)
        {
            bool result = false;
            Category category1 = this.GetCategory(category.CategoryId);
            if (category1 == null)
            {
                try
                {
                    _context.Categories.Add(category);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public bool UpdateCategory(Category category)
        {
            bool result = false;
            Category category1 = this.GetCategory(category.CategoryId);
            if (category1 != null)
            {
                try
                {
                    _context.Entry(category1).CurrentValues.SetValues(category);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public bool RemoveCategory(string categoryId)
        {
            bool result = false;
            Category category1 = this.GetCategory(categoryId);
            if (category1 != null)
            {
                try
                {
                    _context.Categories.Remove(category1);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
    }
}
