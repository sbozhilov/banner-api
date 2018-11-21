using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace BannerServiceApi.Model
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    [Serializable]
    [DataContract]
    public class Banner : IBanner
    {
        public Banner(int id, string html, DateTime createTime, DateTime modifiedTime)
        {
            Id = id;
            Html = html;
            CreateTime = createTime;
            ModifiedTime = modifiedTime;
            if (!BsonClassMap.IsClassMapRegistered(typeof(Banner)))
            {
                BsonClassMap.RegisterClassMap<Banner>();
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        [BsonId]
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        [BsonElement]
        [DataMember]
        public string Html { get; set; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        [BsonDateTimeOptions]
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        [BsonDateTimeOptions]
        [DataMember]
        public DateTime? ModifiedTime { get; set; }
    }
}