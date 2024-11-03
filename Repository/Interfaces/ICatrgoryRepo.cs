using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICatrgoryRepo
    {
        public Category GetCategory(string id);
        public List<Category> GetCategories();
        public bool addCategory(Category category);
        public bool removeCategory(string categoryId);
        public bool updateCategory(Category category); 
    }
}
