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
    abstract public class AppPlugBase : IAppPlug, IBilling
    {
        virtual public bool InButton { get { return false; } }

        virtual public bool InMenu { get { return true; } }

        virtual public bool MultiInstance { get { return false; } }
        virtual  public bool Mandatory { get { return false; } }

        virtual public string Name { get { return "未命名"; } }

        virtual public Bitmap Icon => throw new NotImplementedException();

        virtual public string Menu { get { return ""; } }

        virtual public string AppId => throw new NotImplementedException();

        virtual public AppType AppType { get { return AppType.Inner; } }

        virtual public bool IsStarted => throw new NotImplementedException();

        virtual public UserControl MainForm => throw new NotImplementedException();

       

        public event EventHandler GroupDataChanged;

        virtual public void Initialize(GroupConfigHelper config)
        {
            throw new NotImplementedException();
        }

        virtual public bool Start()
        {
            return true;
        }

        virtual public void Stop()
        {
            
        }
    }
}
