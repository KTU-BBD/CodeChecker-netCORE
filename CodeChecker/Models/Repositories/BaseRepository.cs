using System;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> entities;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        /// <summary>
        /// Returns one element if its found. Throws error if element cannot be found
        /// </summary>
        /// <param name="id">Id of searchable model</param>
        /// <returns></returns>
        public T Get(long id)
        {
            return entities.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Inserts and saves data in table
        /// </summary>
        /// <param name="entity">Entity which should be saved in database</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entities.Add(entity);
            Save();
        }

        /// <summary>
        /// Updates given entity information
        /// </summary>
        /// <param name="entity">Entity which should be updated in database</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Update(entity);
            Save();
        }

        /// <summary>
        /// Deletes given object from the database. If object is soft deletable, then it deletes it
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // Checks if entity inherited soft-deletable class
            if (entity is SoftDeletable)
            {
                (entity as SoftDeletable).DeletedAt = DateTime.Now;
            }
            else
            {
                entities.Remove(entity);
            }

            Update(entity);
        }

        public void Recover(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // Checks if entity inherited soft-deletable class
            if (entity is SoftDeletable)
            {
                (entity as SoftDeletable).DeletedAt = null;
                Update(entity);
            }
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }
}