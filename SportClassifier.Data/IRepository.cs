using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        IQueryable<T> All(string[] includes);
        T GetById(int id);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);
    }
}
