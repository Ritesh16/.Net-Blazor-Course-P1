﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<CategoryDto> Create(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.CreatedDate = DateTime.Now;

            var addedCategory = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDto>(addedCategory.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<CategoryDto> Get(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                return _mapper.Map<Category, CategoryDto>(category);
            }

            return new CategoryDto();
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(await _context.Categories.ToListAsync());
        }

        public async Task<CategoryDto> Update(CategoryDto categoryDto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryDto.Id);
            if (category != null)
            {
                category.Name = categoryDto.Name;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return _mapper.Map<Category, CategoryDto>(category);
            }

            return categoryDto;
        }
    }
}
