using Application.Models.ViewModels;

namespace Application.Interfaces
{
    public interface IGroupService
    {
        Task<List<ProductGroupListVm>> GetProductGroupList();
        Task<List<ProductGroupItemListVm>> GetProductGroupItemsByGroupId(Guid groupId);
    }
}
