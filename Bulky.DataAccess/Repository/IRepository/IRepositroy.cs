using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    /// <summary>
    /// this is the generi interface for basic repositories method use this for extend other other interfaces 
    /// </summary>
    /// <typeparam name="T">T is Domain Model witch has table in database</typeparam>
    public interface IRepository<T> where T : class  
    {
        /// <summary>
        /// return all the enitiy from Table
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// retrun one entity that match ther filetr
        /// </summary>
        /// <param name="filter">a lambda expression for finding the expresion</param>
        /// <returns></returns>
        T? Get(Expression<Func<T,bool>> filter);
        /// <summary>
        /// add entity to table  
        /// </summary>
        /// <param name="entity"> a single entity</param>
        void Add(T entity);
        /// <summary>
        /// Remove entity from table 
        /// </summary>
        /// <param name="entity"> a single entity</param>
        void Remove(T entity);
        /// <summary>
        /// remove all the enities from table in database 
        /// </summary>
        /// <param name="entities">a collection of entity</param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
