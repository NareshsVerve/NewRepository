using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Todo_App.Generic_Repository
{
    public interface IGenericRepository<T>where T:class 
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void InsertMany(List<T> obj);
        void Update(T obj);
        void Delete(int id);
        void Save();
    }
}
