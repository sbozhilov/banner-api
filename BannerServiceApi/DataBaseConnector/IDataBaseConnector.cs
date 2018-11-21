namespace BannerServiceApi.DataBaseConnector
{
    internal interface IDataBaseConnector<out T>
    {
        /// <summary>
        /// Gets a client for the database
        /// </summary>
        /// <returns></returns>
        T GetDataBaseClient();
    }
}