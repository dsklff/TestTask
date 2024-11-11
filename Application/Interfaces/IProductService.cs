namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> ImportProductsFromExcel(Stream fileStream);
    }
}
