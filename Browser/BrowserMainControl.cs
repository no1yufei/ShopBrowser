using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CefSharp;
using CefSharp.WinForms;
using SharpBrowser.BrowserTabStrip;
using Timer = System.Windows.Forms.Timer;
using System.Drawing;
using System.Reflection;
using SharpBrowser;
using SharpBrowser.Factory;
using Common.Browser;

namespace SharpBrowser {

	/// <summary>
	/// The main SharpBrowser form, supporting multiple tabs.
	/// We used the x86 version of CefSharp V51, so the app works on 32-bit and 64-bit machines.
	/// If you would only like to support 64-bit machines, simply change the DLL references.
	/// </summary>
	public partial class BrowserMainControl : UserControl {

		//public static BrowserMainControl Instance;
  //      public static string Branding = "SharpBrowser";
		
        public BrowserMainControl() {
			//Instance = this;
			InitializeComponent();
			initBrowser();
		}

		private void MainForm_Load(object sender, EventArgs e) {
			InitHotkeys();
		}
	
		/// <summary>
		/// these hotkeys work when the user is focussed on the .NET form and its controls,
		/// AND when the user is focussed on the browser (CefSharp portion)
		/// </summary>
		private void InitHotkeys()
		{
			// browser hotkeys
			//KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.W, true);
			//KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.Escape, true);
			//KeyboardHandler.AddHotKey(this, AddBlankWindow, Keys.N, true);
			//KeyboardHandler.AddHotKey(this, AddBlankTab, Keys.T, true);
			////KeyboardHandler.AddHotKey(this, RefreshActiveTab, Keys.F5);
			////KeyboardHandler.AddHotKey(this, OpenDeveloperTools, Keys.F12);
			//KeyboardHandler.AddHotKey(this, NextTab, Keys.Tab, true);
			//KeyboardHandler.AddHotKey(this, PrevTab, Keys.Tab, true, true);

			//// search hotkeys
			////KeyboardHandler.AddHotKey(this, OpenSearch, Keys.F, true);
			////KeyboardHandler.AddHotKey(this, CloseSearch, Keys.Escape);
			////KeyboardHandler.AddHotKey(this, StopActiveTab, Keys.Escape);


		}
		/// <summary>
		/// 关闭所有Tab页面
		/// </summary>
		public void Clear()
		{
			List<TabInfo> tabs = new List<TabInfo>();
			foreach (BrowserTabStripItem tab in TabPages.Items)
			{
				tabs.Add((TabInfo)(tab.Tag));
			}
			foreach(TabInfo tb in tabs)
			{
				tb.BrowserTab.CurBrowser.Dispose();
				TabPages.Items.Remove(tb.Tab);
			}
		}
		#region Web Browser & Tabs

		private TabInfo newTab;
		private TabInfo downloadsTab;
        private string currentTitle;
	
		private HostHandler host;

	
		/// <summary>
		/// this is done just once, to globally initialize CefSharp/CEF
		/// </summary>
		private void initBrowser() 
		{
             InitDownloads();

			host = new HostHandler(this);

			AddNewBrowser(tabStrip1, ChromeBrowser.HomepageURL);
		}

		public void AddBlankBrowserTab() 
		{
			AddNewBrowserTab("","");
			//this.InvokeOnParent(delegate() {
			//	TxtURL.Focus();
			//});
		}
		private TabInfo getBrowserTabInfo(string url,string storename)
		{
			if (url.Length > 0)
			{
				// check if already exists
				foreach (BrowserTabStripItem tab in TabPages.Items)
				{
					TabInfo tab2 = (TabInfo)tab.Tag;
					if (tab2 != null
						&& (tab2.CurURL == url)
						&& (tab2.BrowserTab.CurBrowser is ChromeBrowser && (tab2.BrowserTab.CurBrowser as ChromeBrowser).StoreName == storename))
					{
						TabPages.SelectedItem = tab;
						return tab2;
					}
				}
			}
			return null;
		}
		public TabInfo AddNewBrowserTab(string url, ChromeBrowser chromBbrowser, bool focusNewTab = true, string refererUrl = null)
		{
			return (TabInfo)this.Invoke((Func<TabInfo>)delegate {

				TabInfo newTab = getBrowserTabInfo(url, chromBbrowser.StoreName);
				if(null == newTab)
				{
					BrowserTabStripItem tabStrip = new BrowserTabStripItem();
					tabStrip.Title = "新标签页";
					TabPages.Items.Insert(TabPages.Items.Count - 1, tabStrip);

					newTab = AddNewBrowser(chromBbrowser, tabStrip, url);
					this.newTab = newTab;
					newTab.RefererURL = refererUrl;
					if (focusNewTab) timer1.Enabled = true;
				}
				else
				{
					chromBbrowser.Dispose();
				}
				return newTab;
			});
		}
		public TabInfo AddNewBrowserTab(string url,string storename, bool focusNewTab = true, string refererUrl = null) {
			return (TabInfo)this.Invoke((Func<TabInfo>)delegate {
			TabInfo newTab = getBrowserTabInfo(url, storename);
				if (null == newTab)
				{
					BrowserTabStripItem tabStrip = new BrowserTabStripItem();
					tabStrip.Title = "新标签页";
					TabPages.Items.Insert(TabPages.Items.Count - 1, tabStrip);

					newTab = AddNewBrowser(tabStrip, url);
					this.newTab = newTab;
					newTab.RefererURL = refererUrl;
					if (focusNewTab) timer1.Enabled = true;
				}
				return newTab;
			});
		}
		private TabInfo AddNewBrowser(BrowserTabStripItem tabStrip, String url) {
			
			BrowserTabForm browserTabContent = new BrowserTabForm(url);
			return initNewBrowserTabContent(tabStrip, browserTabContent);
		}

		private TabInfo AddNewBrowser(ChromeBrowser chromeBrowser, BrowserTabStripItem tabStrip, String url)
		{
			BrowserTabForm browserTabContent = new BrowserTabForm(chromeBrowser, url);
			return initNewBrowserTabContent(tabStrip, browserTabContent);
		}
		private TabInfo initNewBrowserTabContent(BrowserTabStripItem tabStrip, BrowserTabForm browserTabContent)
		{
			browserTabContent.OpenTab = AddNewBrowserTab;
			browserTabContent.CloseTab = CloseActiveTab;
			browserTabContent.TitleChanged = setTabTitle;

			// set layout
			browserTabContent.Dock = DockStyle.Fill;
			tabStrip.Controls.Add(browserTabContent);
			browserTabContent.BringToFront();


			// new tab obj
			TabInfo tab = new TabInfo
			{
				IsOpen = true,
				BrowserTab = browserTabContent,
				Tab = tabStrip,
				OrigURL = browserTabContent.InitURL,
				CurURL = browserTabContent.InitURL,
				Title = "新标签页",
				DateCreated = DateTime.Now
			};

			// save tab obj in tabstrip
			tabStrip.Tag = tab;

			if (browserTabContent.InitURL.StartsWith(ChromeBrowser.InternalURL + ":"))
			{
				browserTabContent.CurBrowser.JavascriptObjectRepository.Register("host", host, true, BindingOptions.DefaultBinder);
			}
			return tab;
		}
		private void setTabTitle(String title)
		{
			if (CurTab != null)
			{
				if(CurTab.BrowserTab.CurBrowser is ChromeBrowser)
				{
					title = "[" + (CurTab.BrowserTab.CurBrowser as ChromeBrowser).StoreName + "]" + title;
				}
				
				CurTab.Title = title;
				CurTab.Tab.Title = title;
			}
		}

		public void CloseActiveTab() {
			if (CurTab != null/* && TabPages.Items.Count > 2*/) {

				// remove tab and save its index
				int index = TabPages.Items.IndexOf(TabPages.SelectedItem);
				TabPages.RemoveTab(TabPages.SelectedItem);

				// keep tab at same index focussed
				if ((TabPages.Items.Count - 1) > index) {
					TabPages.SelectedItem = TabPages.Items[index];
				}
			}
		}

		private void OnTabClosed(object sender, EventArgs e) {
			
		}

		private void OnTabClosing(SharpBrowser.BrowserTabStrip.TabStripItemClosingEventArgs e) {

			// exit if invalid tab
			if (CurTab == null){
				e.Cancel = true;
				return;
			}

			// add a blank tab if the very last tab is closed!
			if (TabPages.Items.Count <= 2) {
				AddBlankBrowserTab();
				//e.Cancel = true;
			}

		}


		private bool IsOnFirstTab() {
			return TabPages.SelectedItem == TabPages.Items[0];
		}
		private bool IsOnLastTab() {
			return TabPages.SelectedItem == TabPages.Items[TabPages.Items.Count - 2];
		}

		private int CurIndex {
			get {
				return TabPages.Items.IndexOf(TabPages.SelectedItem);
			}
			set {
				TabPages.SelectedItem = TabPages.Items[value];
			}
		}
		private int LastIndex {
			get {
				return TabPages.Items.Count - 2;
			}
		}

		private void NextTab() {
			if (IsOnLastTab()) {
				CurIndex = 0;
			} else {
				CurIndex++;
			}
		}
		private void PrevTab() {
			if (IsOnFirstTab()) {
				CurIndex = LastIndex;
			} else {
				CurIndex--;
			}
		}

		

		public TabInfo CurTab {
			get {
				if (TabPages.SelectedItem != null && TabPages.SelectedItem.Tag != null) {
					return ((TabInfo)TabPages.SelectedItem.Tag);
				} else {
					return null;
				}
			}
		}

		public int CurTabLoadingDur {
			get {
				if (TabPages.SelectedItem != null && TabPages.SelectedItem.Tag != null) {
					int loadTime = (int)(DateTime.Now - CurTab.DateCreated).TotalMilliseconds;
					return loadTime;
				} else {
					return 0;
				}
			}
		}

	
		private void OnTabsChanged(TabStripItemChangedEventArgs e) {


			BrowserTabForm tabContent = null;
			try
			{
				tabContent = ((BrowserTabForm)e.Item.Controls[0]);
			}
			catch (System.Exception ex) { }


			if (e.ChangeType == BrowserTabStripItemChangeTypes.SelectionChanged)
			{
				if (TabPages.SelectedItem == tabStripAdd)
				{
					AddBlankBrowserTab();
				}
			}

			if (e.ChangeType == BrowserTabStripItemChangeTypes.Removed)
			{
				if (downloadsTab != null && e.Item == downloadsTab.Tab)
				{
					downloadsTab = null;
				}
				if (tabContent != null)
				{ 
					tabContent.Dispose();
				}
			}

			if (e.ChangeType == BrowserTabStripItemChangeTypes.Changed)
			{
				if (tabContent != null)
				{
					//if (tabContent != "about:blank")
					{
						tabContent.Focus();
					}
				}
			}

		}

		private void timer1_Tick(object sender, EventArgs e) {
			TabPages.SelectedItem = newTab.Tab;
			timer1.Enabled = false;
		}

		private void menuCloseTab_Click(object sender, EventArgs e) {
			CloseActiveTab();
		}

		private void menuCloseOtherTabs_Click(object sender, EventArgs e) {
			List<BrowserTabStripItem> listToClose = new List<BrowserTabStripItem>();
			foreach (BrowserTabStripItem tab in TabPages.Items) {
				if (tab != tabStripAdd && tab != TabPages.SelectedItem) listToClose.Add(tab);
			}
			foreach (BrowserTabStripItem tab in listToClose) {
				TabPages.RemoveTab(tab);
			}

		}

		public List<int> CancelRequests {
			get {
				return downloadCancelRequests;
			}
		}

		private void tabPages_MouseClick(object sender, MouseEventArgs e) {
			/*if (e.Button == System.Windows.Forms.MouseButtons.Right) {
				tabPages.GetTabItemByPoint(this.mouse
			}*/
		}

		#endregion

		#region Download Queue

		public void BrowserClosing() {

			// ask user if they are sure
			if (DownloadsInProgress()) {
				if (MessageBox.Show("Downloads are in progress. Cancel those and exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) {
					//e.Cancel = true;
					return;
				}
			}

			// dispose all browsers
			try {
				foreach (TabPage tab in TabPages.Items) {
					ChromiumWebBrowser browser = (ChromiumWebBrowser)tab.Controls[0];
					browser.Dispose();
				}
			} catch (System.Exception ex) { }

		}

		public Dictionary<int, DownloadItem> downloads;
		public Dictionary<int, string> downloadNames;
		public List<int> downloadCancelRequests;

		/// <summary>
		/// we must store download metadata in a list, since CefSharp does not
		/// </summary>
		private void InitDownloads() {

			downloads = new Dictionary<int, DownloadItem>();
			downloadNames = new Dictionary<int, string>();
			downloadCancelRequests = new List<int>();
		}

		public Dictionary<int, DownloadItem> Downloads {
			get {
				return downloads;
			}
		}

		public void UpdateDownloadItem(DownloadItem item) {
			lock (downloads) {

				// SuggestedFileName comes full only in the first attempt so keep it somewhere
				if (item.SuggestedFileName != "") {
					downloadNames[item.Id] = item.SuggestedFileName;
				}

				// Set it back if it is empty
				if (item.SuggestedFileName == "" && downloadNames.ContainsKey(item.Id)) {
					item.SuggestedFileName = downloadNames[item.Id];
				}

				downloads[item.Id] = item;

				//UpdateSnipProgress();
			}
		}

		public string CalcDownloadPath(DownloadItem item) {
			return item.SuggestedFileName;
		}

		public bool DownloadsInProgress() {
			foreach (DownloadItem item in downloads.Values) {
				if (item.IsInProgress) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// open a new tab with the downloads URL
		/// </summary>
		

		public void OpenDownloadsTab() {
			if (downloadsTab != null && (downloadsTab.BrowserTab.CurBrowser.Address == ChromeBrowser.DownloadPageURL))
			{
				TabPages.SelectedItem = downloadsTab.Tab;
			}
			else
			{
				downloadsTab = AddNewBrowserTab(ChromeBrowser.DownloadPageURL, "");
			}
		}

		#endregion

		

      
    }
}

/// <summary>
/// POCO created for holding data per tab
/// </summary>
