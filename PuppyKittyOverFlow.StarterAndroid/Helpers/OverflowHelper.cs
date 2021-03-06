﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace PuppyKittyOverFlow.StarterAndroid
{
	public static class OverflowHelper
	{
		public enum Animal
		{
			Cat,
			Dog,
			Otter
		}

		public async static Task<Stream> GetStreamAsync(string url)
		{
			var httpClient = new HttpClient();
			return await httpClient.GetStreamAsync(url);
		}

		private const string CatUrl = "http://catoverflow.com/api/query?limit=1&order=random";
		private const string DogUrl = "http://dogoverflow.com/api/query?limit=1&order=random";
		private const string OtterUrl = "http://otteroverflow.com/api/query?limit=1&order=random";
		public async static Task<String> GetPictureAsync(Animal animal)
		{
			try
			{
				var url = string.Empty;
				switch (animal)
				{
				case Animal.Cat: url = CatUrl; break;
				case Animal.Dog: url = DogUrl; break;
				default: url = OtterUrl; break;
				}

				var client = new HttpClient();
				if (client.DefaultRequestHeaders.CacheControl == null)
					client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

				client.DefaultRequestHeaders.CacheControl.NoCache = true;
				client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
				client.DefaultRequestHeaders.CacheControl.NoStore = true;
				client.Timeout = new TimeSpan(0, 0, 10);

				var imageUrl = await client.GetStringAsync(url);
				if (!string.IsNullOrWhiteSpace(imageUrl) && imageUrl.EndsWith("\n"))
					imageUrl = imageUrl.TrimEnd('\n');

				return imageUrl;


			}
			catch (Exception ex)
			{
				return string.Empty;

			}

		}
	}
}

