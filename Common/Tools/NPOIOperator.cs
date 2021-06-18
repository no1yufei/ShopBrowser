using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat.Tools
{
    public class NPOIOperator
    {
        //------------【函数：将表格控件保存至Excel文件(新建/替换)】------------    

        //filePath要保存的目标Excel文件路径名
        //datagGridView要保存至Excel的表格控件
        //------------------------------------------------------------------------
        public static bool SaveToExcelNew(string filePath, DataGridView dataGridView)
        {
            bool result = true;

            FileStream fs = null;//创建一个新的文件流
            HSSFWorkbook workbook = null;//创建一个新的Excel文件
            ISheet sheet = null;//为Excel创建一张工作表

            //定义行数、列数、与当前Excel已有行数
            int rowCount = dataGridView.RowCount;//记录表格中的行数
            int colCount = dataGridView.ColumnCount;//记录表格中的列数

            //为了防止出错，这里应该判定一下文件与文件是否存在

            //创建工作表
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                workbook = new HSSFWorkbook();
                sheet = workbook.CreateSheet("Sheet1");
                IRow row = sheet.CreateRow(0);
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[0].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);//创建列
                        cell.SetCellValue(dataGridView.Columns[j].HeaderText.ToString());//更改单元格值                  
                    }
                }
            }
            catch
            {
                result = false;
                return result;
            }

            for (int i = 0; i < rowCount; i++)      //行循环
            {
                //防止行数超过Excel限制
                if (i >= 65536)
                {
                    result = false;
                    break;
                }
                IRow row = sheet.CreateRow(1 + i);  //创建行
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);//创建列
                        cell.SetCellValue(dataGridView.Rows[i].Cells[j].Value.ToString());//更改单元格值                  
                    }
                }
            }
            try
            {
                workbook.Write(fs);
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                workbook = null;
            }
            return result;
        }
        //------------【函数：将表格控件保存至Excel文件(添加/新建)】------------    
        //filePath要保存的目标Excel文件路径名
        //datagGridView要保存至Excel的表格控件
        //------------------------------------------------
        public static bool SaveToExcelAdd(string filePath, DataGridView dataGridView)
        {
            bool result = true;

            FileStream fs = null;//创建一个新的文件流
            HSSFWorkbook workbook = null;//创建一个新的Excel文件
            ISheet sheet = null;//为Excel创建一张工作表

            //定义行数、列数、与当前Excel已有行数
            int rowCount = dataGridView.RowCount;//记录表格中的行数
            int colCount = dataGridView.ColumnCount;//记录表格中的列数
            int numCount = 0;//Excell最后一行序号

            //为了防止出错这里应该判断文件夹是否存在

            //判断文件是否存在
            if (!File.Exists(filePath))
            {
                try
                {
                    fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet1");
                    IRow row = sheet.CreateRow(0);
                    for (int j = 0; j < colCount; j++)  //列循环
                    {
                        if (dataGridView.Columns[j].Visible && dataGridView.Rows[0].Cells[j].Value != null)
                        {
                            ICell cell = row.CreateCell(j);//创建列
                            cell.SetCellValue(dataGridView.Columns[j].HeaderText.ToString());//更改单元格值                  
                        }
                    }
                    workbook.Write(fs);
                }
                catch
                {
                    result = false;
                    return result;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                    workbook = null;
                }
            }
            //创建指向文件的工作表
            try
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                workbook = new HSSFWorkbook(fs);//.xls
                sheet = workbook.GetSheetAt(0);
                if (sheet == null)
                {
                    result = false;
                    return result;
                }
                numCount = sheet.LastRowNum + 1;
            }
            catch
            {
                result = false;
                return result;
            }

            for (int i = 0; i < rowCount; i++)      //行循环
            {
                //防止行数超过Excel限制
                if (numCount + i >= 65536)
                {
                    result = false;
                    break;
                }
                IRow row = sheet.CreateRow(numCount + i);  //创建行
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);//创建列
                        cell.SetCellValue(dataGridView.Rows[i].Cells[j].Value.ToString());//更改单元格值                  
                    }
                }
            }
            try
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                workbook.Write(fs);
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                workbook = null;
            }
            return result;
        }

        public static bool SaveDataTableToExcel(string filePath, DataTable dt)
        {
            bool result = true;

            FileStream fs = null;//创建一个新的文件流
            HSSFWorkbook workbook = null;//创建一个新的Excel文件
            ISheet sheet = null;//为Excel创建一张工作表

            //定义行数、列数、与当前Excel已有行数
            int rowCount = dt.Rows.Count;//记录表格中的行数
            int colCount = Math.Min(dt.Columns.Count,255);//记录表格中的列数

            //为了防止出错，这里应该判定一下文件与文件是否存在

            //创建工作表
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                workbook = new HSSFWorkbook();
                sheet = workbook.CreateSheet("Sheet1");
                //IRow row = sheet.CreateRow(0);
                //for (int j = 0; j < colCount; j++)  //列循环
                //{
                //    if (dt.Rows[0][j] != null)
                //    {
                //        ICell cell = row.CreateCell(j);//创建列
                //        cell.SetCellValue(dt.Columns[j].head.HeaderText.ToString());//更改单元格值                  
                //    }
                //}
            }
            catch
            {
                result = false;
                return result;
            }
            IRow rowHead = sheet.CreateRow(0);  //创建表头行
            for (int j = 0; j < colCount; j++)  //列循环
            {
                if (dt.Columns[j].ColumnName != null && !string.IsNullOrEmpty(dt.Columns[j].ColumnName.ToString()))
                {
                    string headName = dt.Columns[j].ColumnName.ToString();
                    ICell cell = rowHead.CreateCell(j);//创建列
                    cell.SetCellValue(headName);//更改单元格值
                }
            }

            for (int i = 0; i < rowCount; i++)      //行循环
            {
                //防止行数超过Excel限制
                if (i >= 65536)
                {
                    result = false;
                    break;
                }
                IRow row = sheet.CreateRow(1 + i);  //创建行
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    
                    if (dt.Rows[i][j] != null&& !string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        ICell cell = row.CreateCell(j);//创建列
                        if (dt.Columns[j].DataType == typeof(double))
                        {
                            cell.SetCellValue(double.Parse(dt.Rows[i][j].ToString()));
                        }
                        else
                        {
                            cell.SetCellValue(dt.Rows[i][j].ToString());//更改单元格值
                        }
                        //if (dt.Columns[j].DataType == typeof(string))
                        //{
                            
                            
                        //}
                        
                    }
                }
            }
            try
            {
                workbook.Write(fs);
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                workbook = null;
            }
            return result;
        }

        /// <summary>
        /// Excel转换成DataTable（.xls）
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            var dt = new DataTable();
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var hssfworkbook = new HSSFWorkbook(file);//支持2003
                var sheet = hssfworkbook.GetSheetAt(0);
                for (var j = 0; j < 250; j++)
                {
                    dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                }
                var rows = sheet.GetRowEnumerator();
                while (rows.MoveNext())
                {
                    var row = (HSSFRow)rows.Current;
                    var dr = dt.NewRow();
                    for (var i = 0; i < row.LastCellNum; i++)
                    {
                        var cell = row.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    dr[i] = "[null]";
                                    break;
                                case CellType.Boolean:
                                    dr[i] = cell.BooleanCellValue;
                                    break;
                                case CellType.Numeric:
                                    dr[i] = cell.ToString();
                                    break;
                                case CellType.String:
                                    dr[i] = cell.StringCellValue;
                                    break;
                                case CellType.Error:
                                    dr[i] = cell.ErrorCellValue;
                                    break;
                                case CellType.Formula:
                                    try
                                    {
                                        dr[i] = cell.NumericCellValue;
                                    }
                                    catch
                                    {
                                        dr[i] = cell.StringCellValue;
                                    }
                                    break;
                                default:
                                    dr[i] = "=" + cell.CellFormula;
                                    break;
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// Excel转换成DataSet（.xlsx/.xls）
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath, out string strMsg)
        {
            strMsg = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string fileType = Path.GetExtension(filePath).ToLower();
            string fileName = Path.GetFileName(filePath).ToLower();
            try
            {
                ISheet sheet = null;
                int sheetNumber = 0;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                if (fileType == ".xlsx")
                {
                    // 2007版本
                    XSSFWorkbook workbook = new XSSFWorkbook(fs);
                    sheetNumber = workbook.NumberOfSheets;
                    for (int i = 0; i < sheetNumber; i++)
                    {
                        string sheetName = workbook.GetSheetName(i);
                        sheet = workbook.GetSheet(sheetName);
                        if (sheet != null)
                        {
                            dt = GetSheetDataTable(sheet, out strMsg);
                            if (dt != null)
                            {
                                dt.TableName = sheetName.Trim();
                                ds.Tables.Add(dt);
                            }
                            else
                            {
                                MessageBox.Show("Sheet数据获取失败，原因：" + strMsg);
                            }
                        }
                    }
                }
                else if (fileType == ".xls")
                {
                    // 2003版本
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    sheetNumber = workbook.NumberOfSheets;
                    for (int i = 0; i < sheetNumber; i++)
                    {
                        string sheetName = workbook.GetSheetName(i);
                        sheet = workbook.GetSheet(sheetName);
                        if (sheet != null)
                        {
                            dt = GetSheetDataTable(sheet, out strMsg);
                            if (dt != null)
                            {
                                dt.TableName = sheetName.Trim();
                                ds.Tables.Add(dt);
                            }
                            else
                            {
                                MessageBox.Show("Sheet数据获取失败，原因：" + strMsg);
                            }
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取sheet表对应的DataTable
        /// </summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        private static DataTable GetSheetDataTable(ISheet sheet, out string strMsg)
        {
            strMsg = "";
            DataTable dt = new DataTable();
            string sheetName = sheet.SheetName;
            int startIndex = 0;// sheet.FirstRowNum;
            int lastIndex = sheet.LastRowNum;
            //最大列数
            int cellCount = 0;
            IRow maxRow = sheet.GetRow(0);
            for (int i = startIndex; i <= lastIndex; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null && cellCount < row.LastCellNum)
                {
                    cellCount = row.LastCellNum;
                    maxRow = row;
                }
            }
            //列名设置
            try
            {
                for (int i = 0; i < maxRow.LastCellNum; i++)//maxRow.FirstCellNum
                {
                    dt.Columns.Add(Convert.ToChar(((int)'A') + i).ToString());
                    //DataColumn column = new DataColumn("Column" + (i + 1).ToString());
                    //dt.Columns.Add(column);
                }
            }
            catch
            {
                strMsg = "工作表" + sheetName + "中无数据";
                return null;
            }
            //数据填充
            for (int i = startIndex; i <= lastIndex; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow drNew = dt.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < row.LastCellNum; ++j)
                    {
                        if (row.GetCell(j) != null)
                        {
                            ICell cell = row.GetCell(j);
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    drNew[j] = "";
                                    break;
                                case CellType.Numeric:
                                    short format = cell.CellStyle.DataFormat;
                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理 
                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                        drNew[j] = cell.DateCellValue;
                                    else
                                        drNew[j] = cell.NumericCellValue;
                                    if (cell.CellStyle.DataFormat == 177 || cell.CellStyle.DataFormat == 178 || cell.CellStyle.DataFormat == 188)
                                        drNew[j] = cell.NumericCellValue.ToString("#0.00");
                                    break;
                                case CellType.String:
                                    drNew[j] = cell.StringCellValue;
                                    break;
                                case CellType.Formula:
                                    try
                                    {
                                        drNew[j] = cell.NumericCellValue;
                                        if (cell.CellStyle.DataFormat == 177 || cell.CellStyle.DataFormat == 178 || cell.CellStyle.DataFormat == 188)
                                            drNew[j] = cell.NumericCellValue.ToString("#0.00");
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            drNew[j] = cell.StringCellValue;
                                        }
                                        catch { }
                                    }
                                    break;
                                default:
                                    drNew[j] = cell.StringCellValue;
                                    break;
                            }
                        }
                    }
                }
                dt.Rows.Add(drNew);
            }
            return dt;
        }
    }
}
