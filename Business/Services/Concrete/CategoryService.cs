using Business.Services.Abstract;
using Business.ViewModels;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IActionContextAccessor _accessor;
        private ModelStateDictionary _modelState;
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IActionContextAccessor accessor, ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _accessor = accessor;
            _repository = repository;
            _modelState = accessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAsync(CategoryCreateVM model)
        {
            if (!_modelState.IsValid)
            {
                return false;
            }

            Category category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.UtcNow.AddHours(4)
            };

            await _repository.Create(category);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<CategoryIndexVM>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            var categoriesVM = new List<CategoryIndexVM>();

            foreach (var item in categories)
            {
                categoriesVM.Add(new CategoryIndexVM { Id = item.Id, Title = item.Title, CreatedAt = item.CreatedAt });
            }

            return categoriesVM;
        }

        public async Task<bool> Update(CategoryUpdateVM model, int id)
        {
            if (!_modelState.IsValid)
            {
                return false;
            }
            var category = await _repository.GetAsync(id);
            if (category == null)
            {
                return false;
            }
            category.Title = model.Title;
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _repository.GetAsync(id);
            if (category is null)
            {
                return false;
            }
            _repository.Delete(category);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
