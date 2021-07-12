using CefSharp;

namespace SharpBrowser {
	internal class MySchemeHandlerFactory : ISchemeHandlerFactory {

		public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request) {
			return new MySchemeHandler();
		}
	}
}