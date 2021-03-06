﻿using System;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Diagnostics;

namespace CodeChecker.Models.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> entities;
        public const int MaxPerPage = 100;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        /// <summary>
        /// Max count of objects per page
        /// </summary>
        /// <returns></returns>
        protected virtual int GetMaxPerPage()
        {
            return MaxPerPage;
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

        public virtual IQueryable<T> Query()
        {
            return entities;
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

            Save();
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

        public IQueryable<T> GetPagedData(DataFilterViewModel filter)
        {
            return GetPagedData(Query(), filter);
        }

        public IQueryable<T> GetPagedData(IQueryable<T> queryable, DataFilterViewModel filter)
        {
            if (filter.Page < 1)
            {
                filter.Page = 1;
            }

            if (filter.Count > GetMaxPerPage())
            {
                filter.Count = GetMaxPerPage();
            }

            var offset = filter.Count * (filter.Page - 1);
            foreach (var item in filter.Filter)
            {
                if (item.Key != "null" && item.Value != "null")
                {
                    var type = typeof(T);
                    try
                    {
                        var property = type.GetProperty(Decode(item.Key));

                        if (property.ToString().Contains(typeof(long).Name))
                        {
                            queryable = queryable.Where($"{Decode(item.Key)} = {Decode(item.Value)}");
                        }else if (property.ToString().Contains(typeof(string).Name))
                        {
                            queryable = queryable.Where($"{Decode(item.Key)}.Contains(@0)", Decode(item.Value));
                        }
                    }
                    catch (Exception ex)
                    {
                        queryable = queryable.Where($"{Decode(item.Key)}.Contains(@0)", Decode(item.Value));
                    }
                }
            }

            foreach (var item in filter.Sorting)
            {
                queryable = queryable.AsQueryable().OrderBy($"{Decode(item.Key)} {Decode(item.Value)}");
            }

            return queryable.Skip(offset).Take(filter.Count);
        }

        private void Save()
        {
            _context.SaveChanges();
        }

        private static string Decode(string value)
        {
            return System.Net.WebUtility.UrlDecode(value);
        }
    }
}