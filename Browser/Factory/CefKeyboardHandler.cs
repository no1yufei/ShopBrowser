//using CefSharp;
using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Brower
{
    public class CefKeyboardHandler : IKeyboardHandler
    {
        public delegate void KeyHandler(IWebBrowser browser, KeyType type, int code, CefEventFlags modifiers, bool isSystemKey);

        public event KeyHandler KeyArrived;

        #region Implementation of IKeyboardHandler
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            if (KeyArrived != null)
            {
                this.KeyArrived(browserControl, type, windowsKeyCode, modifiers, isSystemKey);
            }
            return false;
        }
        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers,
            bool isSystemKey, ref bool isKeyboardShortcut)
        {
            return false;
        }
        #endregion





    }
}
