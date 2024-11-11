using Application.Interfaces;
using Application.Models.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GroupService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductGroupListVm>> GetProductGroupList()
        {
            var productGroups = await _context.ProductGroups
                .OrderBy(x => x.Number)
                .ProjectTo<ProductGroupListVm>(_mapper.ConfigurationProvider) 
                .ToListAsync();

            return productGroups;
        }

        public async Task<List<ProductGroupItemListVm>> GetProductGroupItemsByGroupId(Guid groupId)
        {
            var productGroupItems = await _context.ProductGroupItems
                .Where(x => x.ProductGroup.Id == groupId)
                .ProjectTo<ProductGroupItemListVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return productGroupItems;
        }
    }
}
