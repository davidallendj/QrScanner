using Microsoft.AspNetCore.Mvc;
using QrScanner.Models;

namespace server.Controllers;

[ApiController]
[Route("")]
public class QrScannerController : ControllerBase
{
	const string db_connection_url			= @"mongodb://localhost:27017";
	const string db_name					= @"qr_scanner";
	const string col_codes					= @"codes";
	const string col_info					= @"info";

	public QrScannerController(){
		client = new MongoClient(db_connection_url);
		db = client.GetDatabase(db_name);
	}


	[HttpGet("ping")]
	public ActionResult<string> ping(){
		string message = "Ping received from server.";
		Console.WriteLine($"Sent message: {message}");
		return Ok(message);
	}


	[HttpGet("info")]
	public IActionResult info(){

		return Ok();
	}

	[HttpGet("scan")]
	public async Task<IActionResult> scan(string code){
		Console.WriteLine(code);
		return Ok();
	}


	public async Task<IActionResult> _store_cost(string code){

	}


	public async Task<T> _fetch_one<T>(string collection){
		Console.WriteLine($"_fetch_one(): Fetching first found data from '{db_name}.{col_codes}'");
		var col = db.GetCollection<T>(collection);
		return await col.Find(new BsonDocument{}).FirstOrDefaultAsync();
	}


	public async Task<ActionResult<QrCode>> _fetch_code<T>(string field, T value){
		Console.WriteLine($"_fetch_code(): Fetching QR code from '{db_name}.{col_codes}'");
		var col = db.GetCollection<QrCode>(col_codes);
		var filter = Builders<QrCode>.Filter.Eq(field, value);
		return await Ok(col.Find(filter).FirstOrDefaultAsync());
	}


	public async Task<ActionResult<List<QrCodes>>> _fetch_all_codes(){
		var col = db.GetCollection<SurveyInfo>(col_info);
		return await col.Find(new BsonDocument{}).ToListAsync();
	}
}
