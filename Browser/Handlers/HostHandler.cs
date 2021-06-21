﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpBrowser {

	/// <summary>
	/// functions in this class are accessible by JS using the code `host.X()`
	/// </summary>
	internal class HostHandler {
		BrowserMainControl myForm;

		public HostHandler(BrowserMainControl form) {
			myForm = form;
		}
		public void addNewBrowserTab(string url, bool focusNewTab = true) {
			myForm.AddNewBrowserTab(url, focusNewTab);
		}
		public string getDownloads() {
			lock (myForm.downloads) {
				string x = JSON.Instance.ToJSON(myForm.downloads);
				return x;
			}
		}

		public bool cancelDownload(int downloadId) {
			lock (myForm.downloadCancelRequests) {
				if (!myForm.downloadCancelRequests.Contains(downloadId)) {
					myForm.downloadCancelRequests.Add(downloadId);
				}
			}
			return true;
		}
		public void refreshActiveTab() {
			throw new NotSupportedException();
			//myForm.RefreshActiveTab();
		}
	}

}
