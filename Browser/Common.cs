using SharpBrowser.BrowserTabStrip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBrowser
{
	public class TabInfo
	{

		public bool IsOpen;

		public string OrigURL;
		public string CurURL;
		public string Title;

		public string RefererURL;

		public DateTime DateCreated;

		public BrowserTabStripItem Tab;
		public BrowserTabForm BrowserTab;

	}

	/// <summary>
	/// POCO for holding hotkey data
	/// </summary>
	public class SharpHotKey
	{

		public Keys Key;
		public int KeyCode;
		public bool Ctrl;
		public bool Shift;
		public bool Alt;

		public Action Callback;

		public SharpHotKey(Action callback, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
		{
			Callback = callback;
			Key = key;
			KeyCode = (int)key;
			Ctrl = ctrl;
			Shift = shift;
			Alt = alt;
		}

	}
}
