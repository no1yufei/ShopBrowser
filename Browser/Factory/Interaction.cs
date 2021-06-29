using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Brower
{
    /// <summary>
    /// 和浏览器交互
    /// </summary>
    public class Interaction
    {
        public delegate void ResultHandler(object resultStr);
        
        private ResultHandler resultHandler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="vm">主窗口的模型视图</param>
        internal Interaction(ResultHandler resultHandler)
        {
            this.resultHandler = resultHandler;
        }
       
        public void InitHandler(ResultHandler resultHandler)
        {
            this.resultHandler = resultHandler;
        }
        /// <summary>
        /// 设置翻译结果
        /// </summary>
        /// <param name="result">翻译结果</param>
        public void SetResult(object result)
        {
            //result = result.Replace("\n", "").Replace("\r", "");
            if (null != resultHandler)
            {
                resultHandler(result);
            }
        }
    }
}
