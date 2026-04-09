using prakt15.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.data.repositories.BrandsRepository
{
    public interface IBrand_Repository
    {
        public void GetAll();
        public void DeleteBrand(Brand brand);
        public void EditBrand();
        public void AddBrand(Brand brand);

    }
}
