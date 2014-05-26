/*
    This file is part of HomeGenie Project source code.

    HomeGenie is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HomeGenie is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HomeGenie.  If not, see <http://www.gnu.org/licenses/>.  
*/

/*
 *     Author: Generoso Martello <gene@homegenie.it>
 *     Project Homepage: http://homegenie.it
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.IO;

namespace HomeGenie.Client
{
	public static class Utility
	{
		private static Dictionary<string, byte[]> imageCache = new Dictionary<string, byte[]> ();

		public static void DownloadImage (string url, NetworkCredential credential, Action<byte[]> callback)
		{
			if (imageCache.ContainsKey (url)) {
				callback (imageCache [url]);
			} else {
				try {
					WebRequest req = WebRequest.Create (url);
					req.Credentials = credential;
					Stream stream = req.GetResponse ().GetResponseStream ();
					byte[] img = ReadToEnd (stream);
					if (!imageCache.ContainsKey (url))
						imageCache.Add (url, img);
					callback (img);
					stream.Close ();
				} catch {
				}
			}
		}

		public static byte[] ReadToEnd (System.IO.Stream stream)
		{
			long originalPosition = 0;

			if (stream.CanSeek) {
				originalPosition = stream.Position;
				stream.Position = 0;
			}

			try {
				byte[] readBuffer = new byte[4096];

				int totalBytesRead = 0;
				int bytesRead;

				while ((bytesRead = stream.Read (readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0) {
					totalBytesRead += bytesRead;

					if (totalBytesRead == readBuffer.Length) {
						int nextByte = stream.ReadByte ();
						if (nextByte != -1) {
							byte[] temp = new byte[readBuffer.Length * 2];
							Buffer.BlockCopy (readBuffer, 0, temp, 0, readBuffer.Length);
							Buffer.SetByte (temp, totalBytesRead, (byte)nextByte);
							readBuffer = temp;
							totalBytesRead++;
						}
					}
				}

				byte[] buffer = readBuffer;
				if (readBuffer.Length != totalBytesRead) {
					buffer = new byte[totalBytesRead];
					Buffer.BlockCopy (readBuffer, 0, buffer, 0, totalBytesRead);
				}
				return buffer;
			} finally {
				if (stream.CanSeek) {
					stream.Position = originalPosition; 
				}
			}
		}
	}
}
