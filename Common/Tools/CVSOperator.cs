using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat
{
    public class CVSOperator
    {
        public static void CreateCSV(string fullPath,String[] cols)//table数据写入csv
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Create,
                System.IO.FileAccess.Write))
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        string data = "";
                        for (int i = 0; i < cols.Length; i++)//写入列名
                        {
                            data += cols[i];
                            if (i < cols.Length - 1)
                            {
                                data += ",";
                            }
                        }
                        sw.WriteLine(data);
                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            
            
        }
        public static  Dictionary<String, List<String>>  OpenCSV(string filePath)//从csv读取数据返回table
        {
            char[] splitChars = new char[] { ',', '\t' };
            Dictionary<String, List<String>> datas = new Dictionary<string, List<string>>();
            Dictionary<int, string> cols = new Dictionary<int, string>();
            try
            {
                System.Text.Encoding encoding = GetType(filePath); //Encoding.ASCII;//
                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding))
                    {
                        //记录每次读取的一行记录
                        string strLine = "";
                        //记录每行记录中的各字段内容
                        string[] aryLine = null;
                        string[] tableHead = null;
                        //标示列数
                        int columnCount = 0;
                        //标示是否是读取的第一行
                        bool IsFirst = true;
                        //逐行读取CSV中的数据
                        while ((strLine = sr.ReadLine()) != null)
                        {
                            strLine = strLine.Trim('"');
                            if (IsFirst == true)
                            {
                                strLine = strLine.ToLower();
                                tableHead = strLine.Split(splitChars);
                                IsFirst = false;
                                columnCount = tableHead.Length;
                                //创建列
                                for (int i = 0; i < columnCount; i++)
                                {
                                    datas.Add(tableHead[i].Trim().Trim('"'), new List<string>());
                                    cols.Add(i, tableHead[i].Trim().Trim('"'));
                                }
                            }
                            else
                            {
                                aryLine = strLine.Split(splitChars);
                                for (int j = 0; j < columnCount; j++)
                                {
                                    datas[cols[j]].Add(aryLine[j].Trim('"'));
                                }
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return datas;
        }
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型
        /// <param name="FILE_NAME">文件路径</param>
        /// <returns>文件的编码类型</returns>

        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(FILE_NAME, System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
                System.Text.Encoding r = GetType(fs);
                fs.Close();
                return r;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return System.Text.Encoding.Default;
        }

        /// 通过给定的文件流，判断文件的编码类型
        /// <param name="fs">文件流</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(System.IO.FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
            System.Text.Encoding reVal = System.Text.Encoding.Default;

            System.IO.BinaryReader r = new System.IO.BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = System.Text.Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = System.Text.Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = System.Text.Encoding.Unicode;
            }
            r.Close();
            return reVal;
        }

        /// 判断是否是不带 BOM 的 UTF8 格式
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;　 //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
    }
}
