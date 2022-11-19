using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;

namespace Tangy_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public CategoryDto Create(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.CreatedDate = DateTime.Now;

            var addedCategory = _context.Categories.Add(category);
            _context.SaveChanges();

            return _mapper.Map<Category, CategoryDto>(addedCategory.Entity);
        }

        public int Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return _context.SaveChanges();
            }

            return 0;
        }

        public CategoryDto Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                return _mapper.Map<Category, CategoryDto>(category);
            }

            return new CategoryDto();
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(_context.Categories);
        }

        public CategoryDto Update(CategoryDto categoryDto)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == categoryDto.Id);
            if (category != null)
            {
                category.Name = categoryDto.Name;
                _context.Categories.Update(category);
                _context.SaveChanges();

                return _mapper.Map<Category, CategoryDto>(category);
            }

            return categoryDto;
        }
    }
}
