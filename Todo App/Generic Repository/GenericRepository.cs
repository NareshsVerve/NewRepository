using Microsoft.EntityFrameworkCore;
using Todo_App.Models;

namespace Todo_App.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
       protected readonly ApplicationDbContext _context = null;
       
        private DbSet<T> table = null;

       /* public GenericRepository()
        {
            this._context = new ApplicationDbContext();
            table = _context.Set<T>();
        }*/
        public GenericRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
      
        public virtual T GetById(int id)
        {
            return table.Find(id);
        }
      
        public virtual void Insert(T obj)
        {
            table.Add(obj);
            Save();
        }
        public virtual void InsertMany(List<T> obj)
        {
            table.AddRange(obj);
            Save();
        }
        public virtual void Update(T obj)
        {
            
            table.Attach(obj);  
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }
        
        public virtual void Delete(int id)
        {
            T existing = table.Find(id);
            if (existing != null) {
            table.Remove(existing);
            }
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
