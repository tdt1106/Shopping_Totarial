using Microsoft.EntityFrameworkCore;
using Shopping_Tutarial.Models;

namespace Shopping_Tutarial.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel { Name = "Macbook" , Slug = "macbook", Description = "Apple is Large Product in the world", Status = 1};
				CategoryModel pc = new CategoryModel { Name = "Pc", Slug = "pc", Description = "Samsung is Large Product in the world", Status = 1};
				
				BrandModel apple = new BrandModel { Name = "Apple", Slug = "apple", Description = "Apple is Large Brand in the world", Status = 1 };
				BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "samsung", Description = "Samsung is Large Brand in the world", Status = 1 };
				_context.Products.AddRange(
					new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is Best", Image = "1.jpg", Category = macbook, Brand = apple , Price = 1223 },
					new ProductModel { Name = "Pc", Slug = "pc", Description = "PC is Best", Image = "1.jpg", Category = pc, Brand =samsung , Price = 1223 }
				); 
				_context.SaveChanges();
			}	
		}
	}
}

//BUG:
//System.InvalidOperationException: 'The relationship
//from 'OrderDetails.Product' to 'ProductModel' with
//foreign key properties {'ProductId' : long} cannot
//target the primary key {'Id' : int} because it is not
//compatible. Configure a principal key or a set of foreign
//key properties with compatible types for this relationship.'
