using CommonData.SysData;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Infterface
{
    public interface IAppPlug
    {
		event EventHandler GroupDataChanged;

		/// <summary>
		/// 是否添加工具图标按钮
		/// </summary>
		bool InButton
		{ get;
		}
		/// <summary>
		/// 是否加入菜单项
		/// </summary>
		bool InMenu
		{
			get;
		}
	    /// <summary>
		/// 是否多实例APP，多实例APP可以打开多个相同窗口
		/// </summary>
		bool MultiInstance
		{
			get;
		}
		/// <summary>
		/// 系统必须的APP，在系统启动的时候，会自动生成mandatory 为ture的app的实例
		/// </summary>
		bool Mandatory
		{
			get;
		}
		String Name
		{
			get;
		}

		Bitmap Icon
		{
			get;
		}

		String Menu
		{
			get;
		}
		string AppId
		{
			get;
		}

		AppType AppType
		{
			get;
		}
		bool IsStarted
		{
			get;
		}
		UserControl MainForm
		{
			get;
		}
		void Initialize(GroupConfigHelper config);

		bool Start();
		void Stop();
		
	}
	public enum AppType
	{
		Inner = 0,
		PopUp=1,
		Program,
	}
}

