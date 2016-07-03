﻿using System;
using Google.Maps;
using UIKit;
using System.Threading.Tasks;
using Foundation;
using ATileLayer = Google.Maps.TileLayer;

namespace Xamarin.Forms.GoogleMaps.iOS
{
	internal class NAsyncTileLayer : ATileLayer
	{
		private Func<int, int, int, Task<byte[]>> _tileImageAsync;

		public NAsyncTileLayer(Func<int, int, int, Task<byte[]>> tileImageAsync) : base()
		{
			_tileImageAsync = tileImageAsync;
		}

		public override void RequestTile(nuint x, nuint y, nuint zoom, ITileReceiver receiver)
		{
			_tileImageAsync((int)x, (int)y, (int)zoom).ContinueWith((Task<byte[]> task) => {
				var imgByte = task.Result;
				var image = new UIImage(NSData.FromArray(imgByte));
				receiver.ReceiveTile(x, y, zoom, image);
			});
		}
    }
}

