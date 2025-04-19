using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
	public static class SpecificationEvaluator
	{
		public static IQueryable<T> GetQuery<T>(IQueryable<T> InputQuery, Specifications<T> specifications) where T : class
		{
			//Step01: Get The DbSet
			var query = InputQuery;
			//Step02: Critera
			if(specifications.Critera is not null)
				query = query.Where(specifications.Critera);
			//Step03: Include
			//foreach(var item  in specifications.IncludeExpression)
			//{
			//	query = query.Include(item);
			//}
			query = specifications.IncludeExpression.Aggregate
				(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
			#region For Sorting
			if(specifications.OrderBy is not null)
			{
				query = query.OrderBy (specifications.OrderBy);
			}
			else if(specifications.OrderByDescending is not null)
			{
				query = query.OrderByDescending (specifications.OrderByDescending);
			}
			if (specifications.IsPaginated)
			{
				query = query.Skip(specifications.Skip).Take(specifications.Take);
			}
			#endregion
			//Step04: retrun
			return query;
		}
	}
}
