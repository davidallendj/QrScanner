using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QrScanner.Models;

[BsonIgnoreExtraElements]
public class QrCode
{
	public ObjectId _id { get; set; }
	public string data { get; set; }
}
