using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models.Dtos;

namespace Tangy_Business.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        CategoryDto Create(CategoryDto categoryDto);
        CategoryDto Update(CategoryDto categoryDto);
        int Delete(int id);
        CategoryDto Get(int id);
        IEnumerable<CategoryDto> GetAll();

    }
}
