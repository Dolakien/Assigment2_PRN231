﻿using BusinessObject.Models;
using DataAccessObject;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepo : ICatrgoryRepo
    {
        public bool addCategory(Category category)
            => CategoryDAO.Instance.addCategory(category);

        public List<Category> GetCategories()
            => CategoryDAO.Instance.GetCategories();

        public Category GetCategory(string id)
            => CategoryDAO.Instance.GetCategory(id);

        public bool removeCategory(string categoryId)
            => CategoryDAO.Instance.RemoveCategory(categoryId);


        public bool updateCategory(Category category)
            => CategoryDAO.Instance.updateCategory(category);
    }
}
