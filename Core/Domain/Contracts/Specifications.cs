using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
	public abstract class Specifications<T> where T : class
	{
        public Specifications(Expression<Func<T, bool>> critera)
        {
            Critera = critera;   
        }
        //Where(p => p.Id == id) => Where Condition
        public Expression<Func<T, bool>>? Critera {  get;  private set; }
        //Include(P => P.ProductType).Include(P => P.ProductBrand) => List Of Includes
        public List<Expression<Func<T, object>>> IncludeExpression { get; private set; } = new();
		#region For Filteration And Sorting
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        #endregion
        #region For Pagination 
        public int Skip { get;  set; }
        public int Take { get; set; } 
        public bool IsPaginated {get ; set;}
         #endregion
        protected void AddInclude(Expression<Func<T, object>> expression)
            => IncludeExpression.Add(expression);
		protected void SetOrderBy(Expression<Func<T, object>> expression)
		   => OrderBy = expression;
		protected void SetOrderByDescending(Expression<Func<T, object>> expression)
			=> OrderByDescending = expression;

        protected void ApplyPagination(int PadeIndex , int PageSize)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PadeIndex-1) * PageSize;
        }
	}
}
