using mcare.MongoData.Interface;
using mcare.MongoData.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mcare.API.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<Category> CreateCategory(Category category)
        {
            category.SubCategories = GenerateBsonIdForSubCategories(category.SubCategories);
            await categoryRepository.CreateSync(category);
            return category;
        }

        private List<SubCategory> GenerateBsonIdForSubCategories(List<SubCategory> subCategory)
        {
            //var newSubCategory = new List<SubCategory
            //subCategory.Id = ObjectId.GenerateNewId().ToString();
            if (subCategory != null)
            {
                //foreach (var subCategoryLevel2 in subCategory)
                for (var i = 0; i < subCategory.Count; i++ )
                {
                    if (!string.IsNullOrEmpty(subCategory[i].Name))
                    {
                        subCategory[i].Id = ObjectId.GenerateNewId();
                        subCategory[i].SubCategories = GenerateBsonIdForSubCategories(subCategory[i].SubCategories);
                    }
                    else
                    {
                        subCategory[i] = null;
                    }
                }
                return subCategory;
            }
            return null;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<List<Category>> GetAll()
        {
            var categories = await categoryRepository.ListAll();
            return categories;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<Category> Get(string id)
        {
            var category = await categoryRepository.Get(id);
            return category;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<bool> Update(Category category)
        {
            await categoryRepository.Update(category.Id.ToString(), category);
            return true;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            await categoryRepository.Delete(id);
            return true;
        }
    }
}
