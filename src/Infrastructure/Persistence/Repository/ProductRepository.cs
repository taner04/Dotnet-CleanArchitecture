namespace Infrastructure.Persistence.Repository
{
    [ServiceInjection(typeof(IProductRepository), ScopeType.AddTransient)]
    public sealed class ProductRepository : Repository<Product, ProductId>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
