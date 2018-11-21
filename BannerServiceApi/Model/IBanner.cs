using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BannerServiceApi.Model
{
    /// <summary>
    /// Banner interface
    /// </summary>
    public interface IBanner
    {
        /// <summary>
        /// Unique id of the banner, also utilized as a database identifier
        /// </summary>
        [BsonId]
        int Id { get; set; }

        /// <summary>
        /// Banner Html code stored as string
        /// </summary>
        [BsonElement]
        string Html { get; set; }

        /// <summary>
        /// Create time represents the time when the object was created
        /// </summary>
        [BsonDateTimeOptions]
        DateTime CreateTime { get; set; }

        /// <summary>
        /// Modified time represents last time the object was modified
        /// </summary>
        [BsonDateTimeOptions]
        DateTime? ModifiedTime { get; set; }
    }
}