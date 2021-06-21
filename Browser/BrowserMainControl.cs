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

namespace SharpBrowser {

	/// <summary>
	/// The main SharpBrowser form, supporting multiple tabs.
	/// We used the x86 version of CefSharp V51, so the app works on 32-bit and 64-bit machines.
	/// If you would only like to support 64-bit machines, simply change the DLL references.
	/// </summary>
	public partial class BrowserMainControl : UserControl {

		public static BrowserMainControl Instance;
        public static string Branding = "SharpBrowser";
		
        public BrowserMainControl() {

			Instance = this;

			InitializeComponent();

			InitBrowser();

		}

		private void MainForm_Load(object sender, EventArgs e) {

			InitAppIcon();
			InitHotkeys();
		}

		#region App Icon

		/// <summary>
		/// embedding the resource using the Visual Studio designer results in a blurry icon.
		/// the best way to get a non-blurry icon for Windows 7 apps.
		/// </summary>
		private void InitAppIcon() {
			//assembly = Assembly.GetAssembly(typeof(MainForm));
			//Icon = new Icon(GetResourceStream("sharpbrowser.ico"), new Size(64, 64));
		}
		
		public static Assembly assembly = null;
		public Stream GetResourceStream(string filename, bool withNamespace = true) {
			try {
				return assembly.GetManifestResourceStream("SharpBrowser.Resources." + filename);
			} catch (System.Exception ex) { }
			return null;
		}

		#endregion
		/// <summary>
		/// these hotkeys work when the user is focussed on the .NET form and its controls,
		/// AND when the user is focussed on the browser (CefSharp portion)
		/// </summary>
		private void InitHotkeys()
		{

			// browser hotkeys
			KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.W, true);
			KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.Escape, true);
			KeyboardHandler.AddHotKey(this, AddBlankWindow, Keys.N, true);
			KeyboardHandler.AddHotKey(this, AddBlankTab, Keys.T, true);
			//KeyboardHandler.AddHotKey(this, RefreshActiveTab, Keys.F5);
			//KeyboardHandler.AddHotKey(this, OpenDeveloperTools, Keys.F12);
			KeyboardHandler.AddHotKey(this, NextTab, Keys.Tab, true);
			KeyboardHandler.AddHotKey(this, PrevTab, Keys.Tab, true, true);

			// search hotkeys
			//KeyboardHandler.AddHotKey(this, OpenSearch, Keys.F, true);
			//KeyboardHandler.AddHotKey(this, CloseSearch, Keys.Escape);
			//KeyboardHandler.AddHotKey(this, StopActiveTab, Keys.Escape);


		}

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
		private DownloadHandler dHandler;
		private ContextMenuHandler mHandler;
		private LifeSpanHandler lHandler;
		private KeyboardHandler kHandler;
		private RequestHandler rHandler;
		/// <summary>
		/// this is done just once, to globally initialize CefSharp/CEF
		/// </summary>
		private void InitBrowser() {

			CefSharpSettings.LegacyJavascriptBindingEnabled = true;
			CefSharpSettings.WcfEnabled = false;

			CefSettings settings = new CefSettings();

			settings.RegisterScheme(new CefCustomScheme {
				SchemeName = BrowserTabForm.InternalURL,
				SchemeHandlerFactory = new SchemeHandlerFactory()
			});

			settings.UserAgent = BrowserTabForm.UserAgent;
			settings.AcceptLanguageList = BrowserTabForm.AcceptLanguage;

			settings.IgnoreCertificateErrors = true;
			
			settings.CachePath = GetAppDir("Cache");

			Cef.Initialize(settings);

			InitDownloads();

			dHandler = new DownloadHandler(this);
			lHandler = new LifeSpanHandler(this);
			//mHandler = new ContextMenuHandler(this);
		    kHandler = new KeyboardHandler(this);
			rHandler = new RequestHandler(this);

			host = new HostHandler(this);

			AddNewBrowser(tabStrip1, BrowserTabForm.HomepageURL);

		}

		


		private static string GetAppDir(string name) {
			string winXPDir = @"C:\Documents and Settings\All Users\Application Data\";
			if (Directory.Exists(winXPDir)) {
				return winXPDir + Branding + @"\" + name + @"\";
			}
			return @"C:\ProgramData\" + Branding + @"\" + name + @"\";

		}

		

		public void AddBlankWindow() {

			// open a new instance of the browser

			ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath, "");
			//info.WorkingDirectory = workingDir ?? exePath.GetPathDir(true);
			info.LoadUserProfile = true;

			info.UseShellExecute = false;
			info.RedirectStandardError = true;
			info.RedirectStandardOutput = true;
			info.RedirectStandardInput = true;

			Process.Start(info);
		}
		public void AddBlankTab() {
			AddNewBrowserTab("");
			//this.InvokeOnParent(delegate() {
			//	TxtURL.Focus();
			//});
		}

		public TabInfo AddNewBrowserTab(string url, bool focusNewTab = true, string refererUrl = null) {
			return (TabInfo)this.Invoke((Func<TabInfo>)delegate {
              if(url.Length > 0)
				{
					// check if already exists
					foreach (BrowserTabStripItem tab in TabPages.Items)
					{
						TabInfo tab2 = (TabInfo)tab.Tag;
						if (tab2 != null && (tab2.CurURL == url))
						{
							TabPages.SelectedItem = tab;
							return tab2;
						}
					}
				}
				

				BrowserTabStripItem tabStrip = new BrowserTabStripItem();
				tabStrip.Title = "新标签页";
				TabPages.Items.Insert(TabPages.Items.Count - 1, tabStrip);
				
				TabInfo newTab = AddNewBrowser(tabStrip, url);
				this.newTab = newTab;
				newTab.RefererURL = refererUrl;
				if (focusNewTab) timer1.Enabled = true;
				return newTab;
			});
		}
		private TabInfo AddNewBrowser(BrowserTabStripItem tabStrip, String url) {
			
			BrowserTabForm browser = new BrowserTabForm(url);
			browser.OpenTab = AddNewBrowserTab;
			browser.CloseTab = CloseActiveTab;
			browser.TitleChanged = setTabTitle;

			// set layout
			browser.Dock = DockStyle.Fill;
			tabStrip.Controls.Add(browser);
			browser.BringToFront();

			// add events

			browser.CurBrowser.DownloadHandler = dHandler;
			browser.CurBrowser.LifeSpanHandler = lHandler;
			browser.CurBrowser.KeyboardHandler = kHandler;
			browser.CurBrowser.RequestHandler = rHandler;

			// new tab obj
			TabInfo tab = new TabInfo {
				IsOpen = true,
				BrowserTab = browser,
				Tab = tabStrip,
				OrigURL = url,
				CurURL = url,
				Title = "新标签页",
				DateCreated = DateTime.Now
			};

			// save tab obj in tabstrip
			tabStrip.Tag = tab;

			if (url.StartsWith(BrowserTabForm.InternalURL + ":"))
			{
				browser.CurBrowser.JavascriptObjectRepository.Register("host", host, true, BindingOptions.DefaultBinder);
			}
			return tab;
		}

		private void setTabTitle(String title)
		{
			if (CurTab != null)
			{
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
				AddBlankTab();
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


			ChromiumWebBrowser browser = null;
			try
			{
				browser = ((ChromiumWebBrowser)e.Item.Controls[0]);
			}
			catch (System.Exception ex) { }


			if (e.ChangeType == BrowserTabStripItemChangeTypes.SelectionChanged)
			{
				if (TabPages.SelectedItem == tabStripAdd)
				{
					AddBlankTab();
				}
			}

			if (e.ChangeType == BrowserTabStripItemChangeTypes.Removed)
			{
				if (downloadsTab != null && e.Item == downloadsTab.Tab)
				{
					downloadsTab = null;
				}
				if (browser != null)
				{ 
					browser.Dispose();
				}
			}

			if (e.ChangeType == BrowserTabStripItemChangeTypes.Changed)
			{
				//if (browser != null)
				//{
				//	if (currentFullURL != "about:blank")
				//	{
				//		browser.Focus();
				//	}
				//}
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
			if (downloadsTab != null && (downloadsTab.BrowserTab.CurBrowser.Address ==BrowserTabForm.DownloadsURL))
			{
				TabPages.SelectedItem = downloadsTab.Tab;
			}
			else
			{
				downloadsTab = AddNewBrowserTab(BrowserTabForm.DownloadsURL);
			}
		}

		#endregion

		

      
    }
}

/// <summary>
/// POCO created for holding data per tab
/// </summary>
