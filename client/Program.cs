// https://github.com/kekyo/FlashCap
using System.IO;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;

namespace QrScanner{

	class Program{
		static async Task<int> run_scan(){
			string path = Path.Combine(Environment.CurrentDirectory, "client/qr_scan2.py");
			Process p = new Process();
			p.StartInfo = new ProcessStartInfo("/usr/bin/bpython", path)
			{
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};
			p.Start();
			string output = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			Console.WriteLine($"{output}");
			if(!string.IsNullOrEmpty(output))
				await make_request(output);
			return 0;
		}


		static async Task make_request(string code){
			// Bypass SSL certificate validation (not secure)
			HttpClientHandler client_handler = new HttpClientHandler();
			client_handler.ServerCertificateCustomValidationCallback = (sender, client_handler, chain, ssl_policy_errors) => { return true; };

			HttpClient client = new(client_handler);
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json")
			);
			client.DefaultRequestHeaders.Add("User-Agent", "QR Scanner");
			var json = await client.GetStringAsync("https://localhost:7001/scan?code="+code);
			if(!string.IsNullOrEmpty(json))
				Console.WriteLine(json);
		}


		public static async Task<int> Main(string[] args){
			// QrReaderEmgu reader = new QrReaderEmgu();
			// reader.print_capture_devices();
			// reader.capture_frame();
			return await run_scan();
		}
	}
}
