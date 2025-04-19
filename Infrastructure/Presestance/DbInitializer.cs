using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
	public class DbInitializer : IDbInitializer
	{
		private readonly StoreContext _storeContext;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
			_storeContext = storeContext;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		
		public async Task InitializerAsync()
		{
			try
			{
				//Create DataBase If It Dosn't Exist & Applying Any Pending Migrations
				if (_storeContext.Database.GetPendingMigrations().Any())
					await _storeContext.Database.MigrateAsync();
				//Appling Data Seeding
				#region ProductType
				if (!_storeContext.ProductTypes.Any())
				{
					//Read Types From File As String
					var typesData = await File.ReadAllTextAsync(@"C:\Users\abdos\OneDrive\Desktop\.net\C#\API\E-Commerce (1)\E-Commerce\Infrastructure\Presestance\Data\Seeding\types.json");
					//Transform Into C# Objects
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
					//Add To DB & Save Changes
					if (types is not null && types.Any())
					{
						await _storeContext.ProductTypes.AddRangeAsync(types);
						await _storeContext.SaveChangesAsync();
					}

				}
				#endregion
				#region ProductBrand
				if (!_storeContext.ProductBrands.Any())
				{
					//Read Types From File As String
					var brandsData = await File.ReadAllTextAsync(@"C:\Users\abdos\OneDrive\Desktop\.net\C#\API\E-Commerce (1)\E-Commerce\Infrastructure\Presestance\Data\Seeding\brands.json");
					//Transform Into C# Objects
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
					//Add To DB & Save Changes
					if (brands is not null && brands.Any())
					{
						await _storeContext.ProductBrands.AddRangeAsync(brands);
						await _storeContext.SaveChangesAsync();
					}

				}
				#endregion
				#region Product
				if (!_storeContext.Products.Any())
				{
					//Read Types From File As String
					var productData = await File.ReadAllTextAsync(@"C:\Users\abdos\OneDrive\Desktop\.net\C#\API\E-Commerce (1)\E-Commerce\Infrastructure\Presestance\Data\Seeding\products.json");
					//Transform Into C# Objects
					var products = JsonSerializer.Deserialize<List<Product>>(productData);
					//Add To DB & Save Changes
					if (products is not null && products.Any())
					{
						await _storeContext.Products.AddRangeAsync(products);
						await _storeContext.SaveChangesAsync();
					}

				}
				#endregion
				#region DeliveryMethods
				if (!_storeContext.DeliveryMethods.Any())
				{
					//Read Types From File As String
					var deliveryMethodsData = await File.ReadAllTextAsync(@"C:\Users\abdos\OneDrive\Desktop\.net\C#\API\E-Commerce (1)\E-Commerce\Infrastructure\Presestance\Data\Seeding\delivery.json");
					//Transform Into C# Objects
					var Methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
					//Add To DB & Save Changes
					if (Methods is not null && Methods.Any())
					{
						await _storeContext.DeliveryMethods.AddRangeAsync(Methods);
						await _storeContext.SaveChangesAsync();
					}

				}
				#endregion
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task InitializerIdentityAsync()
		{
			//Seed Default Role
			if(! _roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
				await _roleManager.CreateAsync(new IdentityRole("Admin"));
			}
			//Seed Default User
			if(!_userManager.Users.Any())
			{
				var SuperAdminUser = new User
				{
					DisplayName = "SuperAdminUser",
					Email = "SuperAdminUser@gmail.com",
					UserName = "SuperAdminUser",
					PhoneNumber = "1234567890"
				};
				var AdminUser = new User
				{
					DisplayName = "AdminUser",
					Email = "AdminUser@gmail.com",
					UserName = "AdminUser",
					PhoneNumber = "1234567890"
				};
				await _userManager.CreateAsync(SuperAdminUser, "Passw0rd");
				await _userManager.CreateAsync(AdminUser, "Passw0rd");
				//---------Set Role
				await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
				await _userManager.AddToRoleAsync(AdminUser, "Admin");

			}
		}
	}
}
