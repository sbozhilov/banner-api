using System;
using BannerServiceApi.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BannerServiceApi.DataBaseConnector
{
    /// <summary>
    /// MongoDB connector gets the handles for interacting with the collection
    /// </summary>
    public class MongoDataBaseConnector : IDataBaseConnector<IMongoCollection<BsonDocument>>
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<BsonDocument> _collection;
        private readonly Logger _logger = new Logger();

        /// <summary>
        /// Sets if not set the client <see cref="IMongoClient"/>
        /// </summary>
        private void GetMongoClient()
        {
            try
            {
                if (_client == null)
                {
                    //TODO use settings
                    _client = new MongoClient("mongodb://localhost:27017");
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }

        /// <summary>
        /// Sets a database link if not set <see cref="IMongoDatabase"/>
        /// </summary>
        private void GetDataBase()
        {
            try
            {
                if (_database == null)
                {
                    //TODO use settings
                    _database = _client.GetDatabase("banner");
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }

        /// <summary>
        /// Sets a collection if not set <see cref="IMongoCollection{TDocument}"/> <seealso cref="BsonDocument"/>
        /// </summary>
        private void GetCollection()
        {
            try
            {
                if (_collection == null)
                {
                    //TODO move to settings
                    _collection = _database.GetCollection<BsonDocument>("banners");
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IMongoCollection<BsonDocument> GetDataBaseClient()
        {
            try
            {
                GetMongoClient();
                GetDataBase();
                GetCollection();
                return _collection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}