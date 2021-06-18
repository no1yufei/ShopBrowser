using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ECHX.BLL;

namespace Common.Tools
{
    public class ImageHelper
    {
        public static string DefaultImageMark = Environment.CurrentDirectory + "\\defualtmark.png";
        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="imgPath">原图片地址</param>
        /// <param name="sImgPath">水印图片地址</param>
        /// <returns>resMsg[0] 成功,失败 </returns>
        public static string[] AddWaterMark(string imgPath, string sImgPath)
        {
            string[] resMsg = new[] { "成功", sImgPath };
            using (Image image = Image.FromFile(imgPath))
            {
                try
                {
                    Bitmap bitmap = new Bitmap(image);

                    int width = bitmap.Width, height = bitmap.Height;
                    //水印文字
                    string text = "DLT-Reserved";

                    Graphics g = Graphics.FromImage(bitmap);

                    g.DrawImage(bitmap, 0, 0);

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    g.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);

                    Font crFont = new Font("微软雅黑", 120, FontStyle.Bold);
                    SizeF crSize = new SizeF();
                    crSize = g.MeasureString(text, crFont);

                    //背景位置(去掉了. 如果想用可以自己调一调 位置.)
                    //graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 255, 255)), (width - crSize.Width) / 2, (height - crSize.Height) / 2, crSize.Width, crSize.Height);

                    SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(120, 177, 171, 171));

                    //将原点移动 到图片中点
                    g.TranslateTransform(width / 2, height / 2);
                    //以原点为中心 转 -45度
                    g.RotateTransform(-45);
                    g.TranslateTransform(0, 0);
                    g.DrawString(text, crFont, semiTransBrush, new PointF(0, 0));

                    //保存文件
                    bitmap.Save(sImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                catch (Exception e)
                {

                    resMsg[0] = "失败";
                    resMsg[1] = e.Message;
                }
            }

            return resMsg;
        }
        public static string AddWaterMark(string sImgPath,string markText,string markimage = null)
        {
            ImageWaterMark waterMark = new ImageWaterMark(markText, markimage, sImgPath,"");
            waterMark.ShowCopyright = markText != null && markText.Length > 0;
            waterMark.ShowMarkImage = markimage != null && markimage.Length > 15;
            waterMark.Diaphaneity = 50;
            waterMark.FontButtomSpace = 0;
            waterMark.MarkButtomSpace = 100;
            waterMark.MarkRightSpace = 100;
            return waterMark.CreateMarkPhoto();
        }
       
        public static void ResizeAllImgInFoloder(string srcFold, string desFold, int maxSize)
        {
            var ret = GetImages(srcFold, "*.gif", "*.jpg", "*.png");
            foreach (var v in ret)
            {
                ResizeImage(v, desFold, maxSize);
            }

            var folds = Directory.GetDirectories(srcFold);
            foreach (var v in folds)
            {
                ResizeAllImgInFoloder(v, desFold, maxSize);
            }
        }

        public static void ResizeImage(string srcPath, string desFold, int maxSize)
        {
            //string currentPath = srcPath.Substring(0, srcPath.LastIndexOf("\\"));

            string currentPath = desFold;

            string fileName = srcPath.Substring(srcPath.LastIndexOf("\\") + 1, srcPath.Length - srcPath.LastIndexOf("\\") - 1);
            string newFileName = "new" + fileName;
            Image img = Image.FromFile(srcPath);
            int width = maxSize;
            Image newImg = new Bitmap(width, img.Height * width / img.Width);
            Graphics g = Graphics.FromImage(newImg);
            g.DrawImage(img, 0, 0, width, img.Height * maxSize / img.Width);
            newImg.Save(currentPath + newFileName);
            img.Dispose();
            newImg.Dispose();
        }
        public static void ResizeImage(string srcPath, int sizeRate)
        {
            try
            {
                Image img = Image.FromFile(srcPath);
                Image newImg = new Bitmap((int)Math.Ceiling(img.Width * sizeRate / 100.0), (int)Math.Ceiling(img.Height * sizeRate / 100.0));
                Graphics g = Graphics.FromImage(newImg);
                g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
                img.Dispose();
                newImg.Save(srcPath);
                newImg.Dispose();
            }
            catch(Exception xe)
            {
                Console.WriteLine(xe.Message);
            }
        }
        private static string[] GetImages(string dirPath, params string[] searchPatterns)
        {
            if (searchPatterns.Length <= 0)
            {
                return null;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                FileInfo[][] fis = new FileInfo[searchPatterns.Length][];
                int count = 0;
                for (int i = 0; i < searchPatterns.Length; i++)
                {
                    FileInfo[] fileInfos = di.GetFiles(searchPatterns[i]);
                    fis[i] = fileInfos;
                    count += fileInfos.Length;
                }
                string[] files = new string[count];
                int n = 0;
                for (int i = 0; i <= fis.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < fis[i].Length; j++)
                    {
                        string temp = fis[i][j].FullName;
                        files[n] = temp;
                        n++;
                    }
                }
                return files;
            }
        }
    }
}
