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

using EventSource4Net;
using System.Threading;
using System.Net;
using HomeGenie.Client.Data;
using Newtonsoft.Json;

namespace HomeGenie.Client
{
	public class ControlApi
	{
		public Action<List<Module>> GroupModulesUpdated;
		public Action LoadDataCompleted;
		public Action LoadDataError;

		private CancellationTokenSource cancellationToken;
		private EventSource eventClient;

		private WebClient serviceClient;

		private List<Module> dataModules;
		private List<Group> dataGroups;

		internal string serverAddress;
		internal NetworkCredential serverCredential;

		public ControlApi ()
		{

		}

		public void SetServer (string address)
		{
			SetServer (address, null, null);
		}

		public void SetServer (string address, string user, string pass)
		{
			this.serverAddress = address;
			if (user != null && user != "" && pass != null && pass != "") {
				this.serverCredential = new NetworkCredential (user, pass);
			}
		}

		public void Connect ()
		{
			Disconnect ();

			cancellationToken = new CancellationTokenSource ();
			eventClient = new EventSource (new Uri ("http://" + serverAddress + "/api/HomeAutomation.HomeGenie/Logging/RealTime.EventStream/"), serverCredential, 10000);
			eventClient.StateChanged += eventClient_StateChanged;
			eventClient.EventReceived += eventClient_EventReceived;
			eventClient.Start (cancellationToken.Token);

			serviceClient = new WebClient ();
			serviceClient.Credentials = serverCredential;
			serviceClient.DownloadStringCompleted += serviceClient_DownloadStringCompleted;

			DownloadData ();
		}

		public void Disconnect ()
		{
		}

		public List<Group> Groups {
			get { return dataGroups; }
		}

		public void ServiceCall (string apicall, Action callback)
		{
			WebClient client = new WebClient ();
			client.Credentials = serverCredential;
			client.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) => {
				callback ();
			};
			client.DownloadStringAsync (new Uri ("http://" + serverAddress + "/api/" + apicall));
		}


		private void DownloadData ()
		{
			serviceClient.DownloadStringAsync (new Uri ("http://" + serverAddress + "/api/HomeAutomation.HomeGenie/Config/Modules.List"), "modules");
		}

		private void serviceClient_DownloadStringCompleted (object sender, DownloadStringCompletedEventArgs e)
		{
			string actionTag = e.UserState.ToString ();
			switch (actionTag) {
			case "modules":
				dataModules = JsonConvert.DeserializeObject<List<Module>> (e.Result);
				serviceClient.DownloadStringAsync (new Uri ("http://" + serverAddress + "/api/HomeAutomation.HomeGenie/Config/Groups.List"), "groups");
				break;
			case "groups":
				dataGroups = JsonConvert.DeserializeObject<List<Group>> (e.Result);
                    //
                    // populate groups' modules
				foreach (Group g in dataGroups) {
					List<Module> groupModules = new List<Data.Module> ();
					foreach (Module m in g.Modules) {
						foreach (Module lm in dataModules) {
							if (m.Address == lm.Address && m.Domain == lm.Domain) {
								lm.SetHost (this);
								groupModules.Add (lm);
								break;
							}
						}
					}
					g.Modules.Clear ();
					g.Modules.AddRange (groupModules);
				}
                    //
				if (LoadDataCompleted != null)
					LoadDataCompleted ();
				break;
			}
		}

		private Module GetModule (string domain, string address)
		{
			Module module = null;
			if (dataModules != null) {
				module = dataModules.Find (m => m.Domain == domain && m.Address == address);
			}
			return module;
		}

		private void eventClient_EventReceived (object sender, ServerSentEventReceivedEventArgs e)
		{
			Event eventObject = JsonConvert.DeserializeObject<Event> (e.Message.Data);
			Module module = GetModule (eventObject.Domain, eventObject.Source);
			if (module != null) {
				ModuleParameter property = module.GetProperty (eventObject.Property);
				if (property != null) {
					module.SetProperty (property, eventObject.Value, eventObject.Timestamp);
				}
			}
		}

		private void eventClient_StateChanged (object sender, StateChangedEventArgs e)
		{
			//Console.WriteLine("New state: " + e.State.ToString());
		}

	}
}
