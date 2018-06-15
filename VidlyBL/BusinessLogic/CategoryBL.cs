using System.Linq;
using System.Collections.Generic;
using DAL = VidlyDB;
using Models = VidlyModels.Models;
using VidlyBL.GenericTypeMapping;
using VidlyBL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidlyBL.BusinessLogic
{
    public class CategoryBL
    {
        DAL.VidlyEntities _context = VidlyEntitiesSingleton.Instance;

        static CategoryBL()
        {
            ObjectAdapterConfigurations.RegisterConfigurations();
        }

        public IList<Models.Category> GetAllCategories()
        {
            IList<Models.Category> categories = new List<Models.Category>();
            foreach (DAL.MovieCategory item in _context.MovieCategories)
            {
                categories.Add(Mapper<DAL.MovieCategory, Models.Category>.Instance.Map(item));
            }
            return categories;
        }
        public DAL.MovieCategory GetCategoryById(int id)
        {
            DAL.MovieCategory movieCategory = _context.MovieCategories.Where(x => x.CategoryId == id).FirstOrDefault();
            return movieCategory;
        }

        public Models.Category GetCategoryModelById(int id)
        {
            return Mapper<DAL.MovieCategory, Models.Category>.Instance.Map(_context.MovieCategories.Where(x => x.CategoryId == id).First());
        }
    }
}
