using Microsoft.EntityFrameworkCore;

namespace TaskTranning.Models
{
    public class CodeFirstDataContext : DbContext
    {
        public CodeFirstDataContext()
        {
        }

        public CodeFirstDataContext(DbContextOptions<CodeFirstDataContext> options) : base(options)
        {
        }
        
        public virtual DbSet<User> User { get; set; }
        
        public virtual DbSet<Store> Store { get; set; }
        
        public virtual DbSet<Product> Product { get; set; }
               
        public virtual DbSet<Brand> Brand { get; set; }
        
        public virtual DbSet<Category> Category { get; set; }
        
        public virtual DbSet<Stock> Stock { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().HasKey(st => new {st.ProductId, st.StoreId});
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "khanhtmd97@gmail.com",
                    PassWord = Infrastructure.SecurePasswordHasher.Hash("Khanh123"),
                    FullName = "Nguyen Van Khanh",
                    Phone = 0364606406,
                    Address = "18/11 Thuy Tu",
                    Picture = "logo.jpg",
                    IsActive = true,
                    StoreId = 1,
                    Role = 1
                },
                new User
                {
                    Id = 2,
                    Email = "hoaithu1486@gmail.com",
                    PassWord = Infrastructure.SecurePasswordHasher.Hash("Khanh123"),
                    FullName = "Tran Thi Hoai Thu",
                    Phone = 0364606406,
                    Address = "25/23/131 Tran Phu",
                    Picture = "logo.jpg",
                    IsActive = true,
                    StoreId = 2,
                    Role = 2
                    
                });
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Id = 1,
                    StoreName = "KhanhStore",
                    Phone = 0364606406,
                    Email = "khanhtmd99@gmail.com",
                    Street = "18/11 Thuy Tu",
                    City = "Hue City",
                    State = "",
                    ZipCode = "49000"
                },
                new Store
                    {
                        Id = 2,
                        StoreName = "LocStore",
                        Phone = 0364606406,
                        Email = "Loc97@gmail.com",
                        Street = "18/11 Thuy Tu",
                        City = "Hue City",
                        State = "",
                        ZipCode = "49000"
                    },
                new Store
                    {
                        Id = 3,
                        StoreName = "HuanStore",
                        Phone = 0364606406,
                        Email = "Huan97@gmail.com",
                        Street = "18/11 Thuy Tu",
                        City = "Hue City",
                        State = "",
                        ZipCode = "49000"
                    },
                new Store
                {
                    Id = 4,
                    StoreName = "TuanStore",
                    Phone = 0364606406,
                    Email = "Tuan97@gmail.com",
                    Street = "18/11 Thuy Tu",
                    City = "Hue City",
                    State = "",
                    ZipCode = "49000"
                }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Iphone"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Oppo"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Samsung"
                },
                new Category
                {
                    Id = 4,
                    CategoryName = "Nokia"
                },
                new Category
                {
                    Id = 5,
                    CategoryName = "Hawai"
                });
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Iphone 7",
                    BrandId = 1,
                    CategoryId = 1,
                    ModelYear = 1,
                    ListPrice = 154,
                    Picture = "logo.jpg"
                },new Product
                {
                    Id = 2,
                    ProductName = "Iphone X",
                    BrandId = 1,
                    CategoryId = 1,
                    ModelYear = 1,
                    ListPrice = 111,
                    Picture = "logo.jpg"
                },
                new Product
                {
                    Id = 3,
                    ProductName = "Oppo S10",
                    BrandId = 1,
                    CategoryId = 1,
                    ModelYear = 1,
                    ListPrice = 154,
                    Picture = "logo.jpg"
                },
                new Product
                {
                    Id = 4,
                    ProductName = "Samsung S10+",
                    BrandId = 1,
                    CategoryId = 1,
                    ModelYear = 1,
                    ListPrice = 176,
                    Picture = "logo.jpg"
                },
                new Product
                {
                    Id = 5,
                    ProductName = "Lumia",
                    BrandId = 1,
                    CategoryId = 1,
                    ModelYear = 1,
                    ListPrice = 189,
                    Picture = "logo.jpg"
                });

            modelBuilder.Entity<Brand>().HasData(
                new Brand
                {
                    Id = 1,
                    BrandName = "Pro"
                },
                new Brand
                {
                    Id = 2,
                    BrandName = "XMax"
                },
                new Brand
                {
                    Id = 3,
                    BrandName = "xxx"
                },
                new Brand
                {
                    Id = 4,
                    BrandName = "S+"
                });
            
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Stock>().ToTable("Stock");
            modelBuilder.Entity<Brand>().ToTable("Brand");
        }
    }
    
}