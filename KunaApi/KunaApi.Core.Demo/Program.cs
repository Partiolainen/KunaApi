using System;
using System.Linq;

namespace KunaApi.Core.Demo
{
	static class Program
	{
		private static string _publicKey = ""; // Your public key
		private static string _secret = ""; // Your secret key

		static void Main(string[] args)
		{
			// Public methods:
			var time = KunaApiClient.GetServerTimestamp();
			$"Server timestamp: {time}".Log();
			
			var ticker = KunaApiClient.GetTicker(KunaMarket.BtcUah);
			($"BTC/UAH ticker:\n" +
			 $" Amount: {ticker.Amount}\n" +
			 $" Buy: {ticker.Buy}\n" +
			 $" Sell: {ticker.Sell}\n" +
			 $" High: {ticker.High}\n" +
			 $" Low: {ticker.Low}\n" +
			 $" Last: {ticker.Last}\n" +
			 $" Volume{ticker.Volume}\n" +
			 $" Timestamp: {ticker.Timestamp}").Log();

			var orderBook = KunaApiClient.GetOrderBook(KunaMarket.BtcUah);
			$"BTC/UAH order book top:\n Ask: {orderBook.Asks.FirstOrDefault()?.Price} UAH\n Bid: {orderBook.Bids.FirstOrDefault()?.Price} UAH".Log();

			var marketTrades = KunaApiClient.GetTrades(KunaMarket.BtcUah);
			($"BTC/UAH market last trade price:\n" +
			 $" {marketTrades.FirstOrDefault()?.Price} UAH").Log();

			//User methods:
			var kunaApi = new KunaApiClient(_publicKey, _secret);
			var userInfo = kunaApi.GetUserInfo();
			var placedOrder = kunaApi.PlaceOrder(KunaMarket.BtcUah, "buy", 0.0001, 100);
			var cancelledOrder = kunaApi.CancelOrder(placedOrder);
			// var cancelledOrder = kunaApi.CancelOrder(placedOrder.Id); - the same
			var activeOrders = kunaApi.ActiveOrders(KunaMarket.BtcUah);
			var userTrades = kunaApi.UserTrades(KunaMarket.BtcUah);

			Console.ReadKey();
		}

		static void Log(this string logtext)
		{
			Console.WriteLine($"[{DateTime.Now:G}] {logtext}");
			Console.WriteLine();
		}
	}
}