using System.Threading.Tasks;

namespace BannerServiceApi.DataBaseInterfacer
{
    /// <summary>
    /// Generic interface for interacting with any database based on CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataBaseInterfacer<T>
    {
        /// <summary>
        /// Adds or updates a banner
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        Task AddAsync(T banner);

        /// <summary>
        /// Updates a banner with new Html
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        Task UpdateAsync(T banner);

        /// <summary>
        /// Deletes a banner on given id
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        Task DeleteAsync(int bannerId);

        /// <summary>
        /// Gets a banner on a given id
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        Task<T> ReadAsync(int bannerId);
    }
}