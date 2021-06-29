using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using System.IO;
using CefSharp.WinForms;
using System.Web;
using System.Threading;
using Common.Browser;

namespace SharpBrowser
{
    public partial class BrowserTabForm : UserControl
    {
		public static string Branding = "SharpBrowser";

		private string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
		public static string UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36";
		public static string AcceptLanguage = "en-US,en;q=0.9";
		public static string HomepageURL = "https://www.baidu.com";
		public static string NewTabURL = "about:blank";
		public static string InternalURL = "sharpbrowser";
		public static string DownloadsURL = "sharpbrowser://storage/downloads.html";
		public static string FileNotFoundURL = "sharpbrowser://storage/errors/notFound.html";
		public static string CannotConnectURL = "sharpbrowser://storage/errors/cannotConnect.html";
		public static string SearchURL = "https://www.baidu.com/s?wd=";

		public bool WebSecurity = true;
		public bool CrossDomainSecurity = true;
		public bool WebGL = true;
		public bool ApplicationCache = true;

		public delegate void CloseTabHandler();
		public CloseTabHandler CloseTab;

		public delegate TabInfo OpenTabHandler(string url,string storename, bool focusNewTab = true, string refererUrl = null);
		public OpenTabHandler OpenTab;


		public BrowserTabForm(string url)        {
            InitializeComponent();
			InitTooltips(this.Controls);
			ConfigureBrowser();
			LoadURL(url);
			currentFullURL = url;
		}
		public BrowserTabForm(ChromeBrowser chromeBrowser,string url)
		{
			InitializeComponent();
			this.browserPannel.Controls.Remove(CurBrowser);
			CurBrowser.Dispose();
			CurBrowser = chromeBrowser;
			CurBrowser.Dock = DockStyle.Fill;
			this.browserPannel.Controls.Add(CurBrowser);
			InitTooltips(this.Controls);
			ConfigureBrowser();
			LoadURL(url);
			currentFullURL = url;
		}
		public BrowserTabForm()
		{
			string url = "";
			InitializeComponent();

			this.browserPannel.Controls.Remove(CurBrowser);
			CurBrowser.Dispose();
			CurBrowser = new ChromeBrowser();
			CurBrowser.Dock = DockStyle.Fill;
			this.browserPannel.Controls.Add(CurBrowser);

			InitTooltips(this.Controls);
			ConfigureBrowser();
			LoadURL(url);
			currentFullURL = url;
		}
		#region Tooltips & Hotkeys



		/// <summary>
		/// we activate all the tooltips stored in the Tag property of the buttons
		/// </summary>
		public void InitTooltips(System.Windows.Forms.Control.ControlCollection parent)
		{
			foreach (Control ui in parent)
			{
				Button btn = ui as Button;
				if (btn != null)
				{
					if (btn.Tag != null)
					{
						ToolTip tip = new ToolTip();
						tip.ReshowDelay = tip.InitialDelay = 200;
						tip.ShowAlways = true;
						tip.SetToolTip(btn, btn.Tag.ToString());
					}
				}
				Panel panel = ui as Panel;
				if (panel != null)
				{
					InitTooltips(panel.Controls);
				}
			}
		}

		#endregion

		#region Web Browser & Tabs

		

		private string currentFullURL;
		private string currentCleanURL;
		private string currentTitle;

		


		/// <summary>
		/// this is done every time a new tab is openede
		/// </summary>
		private void ConfigureBrowser()
		{

			BrowserSettings config = new BrowserSettings();

			config.FileAccessFromFileUrls = (!CrossDomainSecurity).ToCefState();
			config.UniversalAccessFromFileUrls = (!CrossDomainSecurity).ToCefState();
			config.WebSecurity = WebSecurity.ToCefState();
			config.WebGl = WebGL.ToCefState();
			config.ApplicationCache = ApplicationCache.ToCefState();

			CurBrowser.BrowserSettings = config;

			CurBrowser.StatusMessage += Browser_StatusMessage;
			CurBrowser.LoadingStateChanged += Browser_LoadingStateChanged;
			CurBrowser.TitleChanged += Browser_TitleChanged;
			CurBrowser.LoadError += Browser_LoadError;
			CurBrowser.AddressChanged += Browser_URLChanged;

			CurBrowser.MenuHandler = new ContextMenuHandler(this);
			//CurBrowser.LifeSpanHandler = lHandler;
			//CurBrowser.KeyboardHandler = kHandler;
			//CurBrowser.RequestHandler = rHandler;



		}


		private static string GetAppDir(string name)
		{
			string winXPDir = @"C:\Documents and Settings\All Users\Application Data\";
			if (Directory.Exists(winXPDir))
			{
				return winXPDir + Branding + @"\" + name + @"\";
			}
			return @"C:\ProgramData\" + Branding + @"\" + name + @"\";

		}

		private void LoadURL(string url)
		{
			Uri outUri;
			string newUrl = url;
			string urlLower = url.Trim().ToLower();

			// UI
			setTabTitle(CurBrowser, "Loading...");

			// load page
			if (urlLower == "localhost")
			{

				newUrl = "http://localhost/";

			}
			else if (url.CheckIfFilePath() || url.CheckIfFilePath2())
			{

				newUrl = url.PathToURL();

			}
			else
			{

				Uri.TryCreate(url, UriKind.Absolute, out outUri);

				if (!(urlLower.StartsWith("http") || urlLower.StartsWith(InternalURL)))
				{
					if (outUri == null || outUri.Scheme != Uri.UriSchemeFile) newUrl = "http://" + url;
				}

				if (urlLower.StartsWith(InternalURL + ":") ||

					// load URL if it seems valid
					(Uri.TryCreate(newUrl, UriKind.Absolute, out outUri)
					 && ((outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps) && newUrl.Contains(".") || outUri.Scheme == Uri.UriSchemeFile)))
				{

				}
				else
				{

					// run search if unknown URL
					newUrl = SearchURL + HttpUtility.UrlEncode(url);

				}

			}

			// load URL
			CurBrowser.Load(newUrl);

			// set URL in UI
			SetFormURL(newUrl);

			// always enable back btn
			EnableBackButton(true);
			EnableForwardButton(false);

			EnableBooknotw(url.Length < 1);
		}



		private void SetFormURL(string URL)
		{

			currentFullURL = URL;
			currentCleanURL = CleanURL(URL);

			TxtURL.Text = currentCleanURL;

			//CurTab.CurURL = currentFullURL;

			CloseSearch();

		}

		private string CleanURL(string url)
		{
			if (url.BeginsWith("about:"))
			{
				return "";
			}
			url = url.RemovePrefix("http://");
			url = url.RemovePrefix("https://");
			url = url.RemovePrefix("file://");
			url = url.RemovePrefix("/");
			return url.DecodeURL();
		}
		public static bool IsBlank(string url)
		{
			return (url == "" || url == "about:blank");
		}
		private bool IsBlankOrSystem(string url)
		{
			return (url == "" || url.BeginsWith("about:") || url.BeginsWith("chrome:") || url.BeginsWith(InternalURL + ":"));
		}

		

		public void RefreshURL()
		{
			CurBrowser.Load(CurBrowser.Address);
		}

		
		private void StopLoading()
		{
			CurBrowser.Stop();
		}

		

		private void Browser_URLChanged(object sender, AddressChangedEventArgs e)
		{
			InvokeIfNeeded(() => {

				// if current tab
				if (sender == CurBrowser)
				{

					if (!Utils.IsFocussed(TxtURL))
					{
						SetFormURL(e.Address);
					}

					EnableBackButton(CurBrowser.CanGoBack);
					EnableForwardButton(CurBrowser.CanGoForward);

					setTabTitle((ChromiumWebBrowser)sender, "Loading...");

					BtnRefresh.Visible = false;
					BtnStop.Visible = true;

					//CurTab.DateCreated = DateTime.Now;

				}

			});
		}

		private void Browser_LoadError(object sender, LoadErrorEventArgs e)
		{
			// ("Load Error:" + e.ErrorCode + ";" + e.ErrorText);
		}

		private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
		{
			InvokeIfNeeded(() => {

				ChromiumWebBrowser browser = (ChromiumWebBrowser)sender;

				setTabTitle(browser, e.Title);

			});
		}

		public delegate void TitleChangedHandler(string title);
		public TitleChangedHandler TitleChanged;
		private void setTabTitle(ChromiumWebBrowser browser, string text)
		{
			text = text.Trim();
			if (IsBlank(text))
			{
				text = "新标签页";
			}

			// save text
			browser.Tag = text;

			

			// if current tab
			if (TitleChanged != null)
			{
				TitleChanged(text);

			}
		}

		private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
		{
			if (sender == CurBrowser)
			{

				EnableBackButton(e.CanGoBack);
				EnableForwardButton(e.CanGoForward);

				if (e.IsLoading)
				{

					// set title
					//setTabTitle();

				}
				else
				{

					// after loaded / stopped
					InvokeIfNeeded(() => {
						BtnRefresh.Visible = true;
						BtnStop.Visible = false;
					});
				}
			}
		}

		

		private void Browser_StatusMessage(object sender, StatusMessageEventArgs e)
		{
		}

		public void WaitForBrowserToInitialize(ChromiumWebBrowser browser)
		{
			while (!browser.IsBrowserInitialized)
			{
				Thread.Sleep(100);
			}
		}

		private void EnableBackButton(bool canGoBack)
		{
			InvokeIfNeeded(() => BtnBack.Enabled = canGoBack);
		}
		private void EnableForwardButton(bool canGoForward)
		{
			InvokeIfNeeded(() => BtnForward.Enabled = canGoForward);
		}

		private void EnableBooknotw(bool showBookNote)
		{
			InvokeIfNeeded(() => booknotePanel.Visible = showBookNote);
		}


		private void bBack_Click(object sender, EventArgs e)
		{
			CurBrowser.Back();
		}

		private void bForward_Click(object sender, EventArgs e)
		{
			CurBrowser.Forward();
		}

		private void txtUrl_TextChanged(object sender, EventArgs e)
		{

		}
		

		private void bDownloads_Click(object sender, EventArgs e)
		{
			if(OpenTab != null)
			{
				OpenTab(DownloadsURL,"");
			}
		}

		private void bRefresh_Click(object sender, EventArgs e)
		{
			RefreshURL();
		}

		private void bStop_Click(object sender, EventArgs e)
		{
			StopLoading();
		}
		private void TxtURL_KeyDown(object sender, KeyEventArgs e)
		{

			// if ENTER or CTRL+ENTER pressed
			if (e.IsHotkey(Keys.Enter) || e.IsHotkey(Keys.Enter, true))
			{
				LoadURL(TxtURL.Text);

				// im handling this
				e.Handled = true;
				e.SuppressKeyPress = true;

				// defocus from url textbox
				this.Focus();
			}

			// if full URL copied
			if (e.IsHotkey(Keys.C, true) && Utils.IsFullySelected(TxtURL))
			{

				// copy the real URL, not the pretty one
				Clipboard.SetText(CurBrowser.Address, TextDataFormat.UnicodeText);

				// im handling this
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void txtUrl_Click(object sender, EventArgs e)
		{
			if (!Utils.HasSelection(TxtURL))
			{
				TxtURL.SelectAll();
			}
		}

		private void OpenDeveloperTools()
		{
			CurBrowser.ShowDevTools();
		}


		#endregion



		

		#region Search Bar
		public void InvokeIfNeeded(Action action)
		{
			if (this.InvokeRequired)
			{
				this.BeginInvoke(action);
			}
			else
			{
				action.Invoke();
			}
		}

		bool searchOpen = false;
		string lastSearch = "";

		private void OpenSearch()
		{
			if (!searchOpen)
			{
				searchOpen = true;
				InvokeIfNeeded(delegate () {
					PanelSearch.Visible = true;
					TxtSearch.Text = lastSearch;
					TxtSearch.Focus();
					TxtSearch.SelectAll();
				});
			}
			else
			{
				InvokeIfNeeded(delegate () {
					TxtSearch.Focus();
					TxtSearch.SelectAll();
				});
			}
		}
		private void CloseSearch()
		{
			if (searchOpen)
			{
				searchOpen = false;
				InvokeIfNeeded(delegate () {
					PanelSearch.Visible = false;
					CurBrowser.GetBrowser().StopFinding(true);
				});
			}
		}

		private void BtnClearSearch_Click(object sender, EventArgs e)
		{
			CloseSearch();
		}

		private void BtnPrevSearch_Click(object sender, EventArgs e)
		{
			FindTextOnPage(false);
		}
		private void BtnNextSearch_Click(object sender, EventArgs e)
		{
			FindTextOnPage(true);
		}

		private void FindTextOnPage(bool next = true)
		{
			bool first = lastSearch != TxtSearch.Text;
			lastSearch = TxtSearch.Text;
			if (lastSearch.CheckIfValid())
			{
				CurBrowser.GetBrowser().Find(0, lastSearch, true, false, !first);
			}
			else
			{
				CurBrowser.GetBrowser().StopFinding(true);
			}
			TxtSearch.Focus();
		}

		private void TxtSearch_TextChanged(object sender, EventArgs e)
		{
			FindTextOnPage(true);
		}

		private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.IsHotkey(Keys.Enter))
			{
				FindTextOnPage(true);
			}
			if (e.IsHotkey(Keys.Enter, true) || e.IsHotkey(Keys.Enter, false, true))
			{
				FindTextOnPage(false);
			}
		}



		#endregion

		#region Home Button
		private void BtnHome_Click(object sender, EventArgs e)
		{
			CurBrowser.Load(HomepageURL);
		}
		#endregion
	}
}
