using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodedHomes.DataContracts;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CodedHomes.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {

        //props
        protected DbSet<T> DBSet { get; set; }
        protected DbContext Context { get; set; }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of a data context is required to use this repository");
            }

            this.Context = context;
            this.DBSet = Context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.DBSet;
        }

        public T GetById(int id)
        {
            return this.DBSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                DBSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DBSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            DBSet.Attach(entity);
            DBSet.Remove(entity);
            
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
                this.Delete(entity);
        }

        public void Detach(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);

            entry.State = EntityState.Detached;
        }
    }
}
