using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BannerServiceApi.DataBaseConnector;
using BannerServiceApi.Helpers;
using BannerServiceApi.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace BannerServiceApi.DataBaseInterfacer
{
    /// <summary>
    /// MongoDB interface for CRUD
    /// </summary>
    public class MongoDataBaseInterfacer : IDataBaseInterfacer<IBanner>
    {
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 20);
        private readonly IDataBaseConnector<IMongoCollection<BsonDocument>> _databaseConnector = new MongoDataBaseConnector();
        private readonly Logger _logger = new Logger();

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task AddAsync(IBanner banner)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", banner.Id);
                var result = await _databaseConnector.GetDataBaseClient().Find(filter).ToListAsync();
                if (result.Count == 0)
                {
                    var document = banner.ToBsonDocument();
                    await _databaseConnector.GetDataBaseClient().InsertOneAsync(document);
                }

                await UpdateAsync(banner);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task UpdateAsync(IBanner banner)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("Id", banner.Id);

                var update = Builders<BsonDocument>.Update.Set("Html", banner.Html).CurrentDate("ModifiedTime");
                var result = await _databaseConnector.GetDataBaseClient().UpdateOneAsync(filter, update);
                if (!result.IsAcknowledged)
                {
                    throw new Exception($"Error: UpdateAsync of banner {banner.Id} Failed" +
                                        $" \n Banner html: {banner.Html}");
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task DeleteAsync(int bannerId)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", bannerId);
                await _databaseConnector.GetDataBaseClient().DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IBanner> ReadAsync(int bannerId)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", bannerId);
                var result = await _databaseConnector.GetDataBaseClient().Find(filter).ToListAsync();
                if (result.Count == 0)
                {
                    throw new Exception($"Error: Could not find a banner with id: {bannerId}! Failed)");
                }

                if (result.Count > 1)
                {
                    throw new Exception($"Error: Found {result.Count} banners with id: {bannerId}, id not unique! Failed)");
                }

                var element = result.FirstOrDefault();

                var banner = BsonSerializer.Deserialize<Banner>(element);
                return banner;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}