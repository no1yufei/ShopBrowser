using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace ECHX.BLL
{
    /// <summary>
    /// WaterMark 的摘要说明
    /// </summary>
    ///  
    /// <param name="strCopyright">要加入的文字</param>
    /// <param name="strMarkPath">水印图片路径</param>
    /// <param name="strPhotoPath">要加水印的图片路径</param>
    /// <param name="strSavePath">处理后的图片路径</param>
    /// <param name="iMarkRightSpace">水印在修改图片中距左边的宽度</param>
    /// <param name="iMarkButtomSpace">水印在修改图片中距底部的高度</param>
    /// <param name="iDiaphaneity">水印图片的透明度</param>
    /// <param name="iFontRightSpace">文字</param>
    /// <param name="iFontButtomSpace">文字</param>
    /// <param name="iFontDiaphaneity">文字</param>
    /// <param name="bShowCopyright">是否显示文字</param>
    /// <param name="bShowMarkImage">是否显示水印图片</param>

    public class ImageWaterMark
    {
        #region param
        private string strCopyright, strMarkPath, strPhotoPath, strSavePath;
        private int iMarkRightSpace, iMarkButtomSpace, iDiaphaneity;
        private int iFontRightSpace = 0, iFontButtomSpace = 0, iFontDiaphaneity = 80;
        private int iFontSize = 10;
        private bool bShowCopyright = true, bShowMarkImage = true;
        #endregion

        #region WaterMark
        public ImageWaterMark()
        {
            this.strCopyright = "";
            this.strMarkPath = null;
            this.strPhotoPath = null;
            this.strSavePath = null;
            this.iDiaphaneity = 70;
            this.iMarkRightSpace = 0;
            this.iMarkButtomSpace = 0;
        }

        /// <summary>
        /// 主要用两样都加的
        /// </summary>
        /// <param name="copyright">要加入的文字</param>
        /// <param name="markPath">水印图片路径</param>
        /// <param name="photoPath">要加水印的图片路径</param>
        /// <param name="savePath">处理后的图片路径</param>
        public ImageWaterMark(string copyright, string markPath, string photoPath, string savePath)
        {
            this.strCopyright = copyright;
            this.strMarkPath = markPath;
            this.strPhotoPath = photoPath;
            this.strSavePath = savePath;
            this.iDiaphaneity = 70;
            this.iMarkRightSpace = 0;
            this.iMarkButtomSpace = 0;
        }
        #endregion

        #region property

        /// <summary>
        /// 设置是否显示水印文字
        /// </summary>
        public bool ShowCopyright
        {
            set { this.bShowCopyright = value; }
        }

        /// <summary>
        /// 设置是否显示水印图片
        /// </summary>
        public bool ShowMarkImage
        {
            set { this.bShowMarkImage = value; }
        }
        /// <summary>
        /// 获取或设置要加入的文字
        /// </summary>
        public string Copyright
        {
            set { this.strCopyright = value; }
        }

        /// <summary>
        /// 获取或设置加水印后的图片路径
        /// </summary>
        public string SavePath
        {
            get { return this.strSavePath; }
            set { this.strSavePath = value; }
        }

        /// <summary>
        /// 获取或设置水印图片路径
        /// </summary>
        public string MarkPath
        {
            get { return this.strMarkPath; }
            set { this.strMarkPath = value; }
        }

        /// <summary>
        /// 获取或设置要加水印图片的路径
        /// </summary>
        public string PhotoPath
        {
            get { return this.strPhotoPath; }
            set { this.strPhotoPath = value; }
        }

        /// <summary>
        /// 设置水印图片的透明度
        /// </summary>
        public int Diaphaneity
        {
            set
            {
                if (value > 0 && value <= 100)
                    this.iDiaphaneity = value;
            }
        }

        /// <summary>
        /// 设置水印字体的透明度0-255
        /// </summary>
        public int FontDiaphaneity
        {
            set
            {
                if (value >= 0 && value <= 255)
                    this.iFontDiaphaneity = value;
            }
        }

        /// <summary>
        /// 设置水印图片在修改图片中距左边的高度
        /// </summary>
        public int MarkRightSpace
        {
            set { this.iMarkRightSpace = value; }
        }

        /// <summary>
        /// 设置水印图片在修改图片中距底部的高度
        /// </summary>
        public int MarkButtomSpace
        {
            set { this.iMarkButtomSpace = value; }
        }

        /// <summary>
        /// 设置水印字体在修改图片中距左边的距离
        /// </summary>
        public int FontRightSpace
        {
            set { iFontRightSpace = value; }
        }

        /// <summary>
        /// 设置水印字体在修改图片中距底部的高度
        /// </summary>
        public int FontButtomSpace
        {
            set { iFontButtomSpace = value; }
        }

        #endregion


        /// <summary>
        /// 生成水印图片
        /// </summary>
        /// <returns></returns>
        public string CreateMarkPhoto()
        {
            string ret = null;
            try
            {
                Image retImg = null;
                int PhotoWidth = 0;
                int PhotoHeight = 0;
                using (Image gPhoto = Image.FromFile(this.strPhotoPath))
                {
                    PhotoWidth = gPhoto.Width;
                    PhotoHeight = gPhoto.Height;
                    retImg = new Bitmap(PhotoWidth, PhotoHeight);

                    using (Graphics grPhoto = Graphics.FromImage(retImg))
                    {
                        grPhoto.DrawImage(gPhoto, 0, 0, PhotoWidth, PhotoHeight);
                        if (bShowCopyright)
                        {

                            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                            //grPhoto.DrawImage(gPhoto, new Rectangle(0, 0, PhotoWidth, PhotoHeight), 0, 0, PhotoWidth, PhotoHeight, GraphicsUnit.Pixel);

                            Font crFont = new Font("楷体", iFontSize, FontStyle.Bold);
                            SizeF crSize = grPhoto.MeasureString(strCopyright, crFont);

                            //设置字体在图片中的位置

                            //float yPosFromBottom = PhotoHeight / 2;
                            //float xCenterOfImg = (PhotoWidth/2);
                            float yPosFromBottom = PhotoHeight - iFontButtomSpace - (crSize.Height);
                            float xCenterOfImg = PhotoWidth - iFontRightSpace - (crSize.Width / 2);
                            //设置字体居中

                            StringFormat StrFormat = new StringFormat();
                            StrFormat.Alignment = StringAlignment.Center;

                            //设置绘制文本的颜色和纹理 (Alpha=153)
                            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(this.iFontDiaphaneity, 0, 0, 0));



                            //背景位置(去掉了. 如果想用可以自己调一调 位置.)
                            //graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 255, 255)), (width - crSize.Width) / 2, (height - crSize.Height) / 2, crSize.Width, crSize.Height);

                            //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(120, 177, 171, 171));

                            ////将原点移动 到图片中点
                            //grPhoto.TranslateTransform(PhotoWidth / 2, PhotoHeight / 2);
                            ////以原点为中心 转 -45度
                            //grPhoto.RotateTransform(-45);

                            //g.DrawString(text, crFont, semiTransBrush, new PointF(0, 0));
                            //将版权信息绘制到图象上
                            grPhoto.DrawString(strCopyright, crFont, semiTransBrush2, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);

                        }
                        if (bShowMarkImage)
                        {
                            //创建一个需要填充水银的Image对象
                            Image imgWatermark = new Bitmap(strMarkPath);
                            int iMarkWidth = imgWatermark.Width;
                            int iMarkmHeight = imgWatermark.Height;

                            ImageAttributes imageAttributes = new ImageAttributes();

                            ColorMap colorMap = new ColorMap();

                            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                            ColorMap[] remapTable = { colorMap };

                            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = {
                          new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                          new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                          new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                          new float[] {0.0f, 0.0f, 0.0f, (float)iDiaphaneity/100f, 0.0f},
                          new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};
                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            grPhoto.DrawImage(imgWatermark, new Rectangle((PhotoWidth - iMarkRightSpace - (iMarkWidth / 2)), (PhotoHeight - iMarkButtomSpace - (iMarkmHeight / 2)), iMarkWidth, iMarkmHeight), 0, 0, iMarkWidth, iMarkmHeight, GraphicsUnit.Pixel, imageAttributes);
                            imgWatermark.Dispose();
                        }
                        gPhoto.Dispose();

                        ret = Path.GetDirectoryName(strPhotoPath) + "\\" + Path.GetFileNameWithoutExtension(strPhotoPath) + "_1" + Path.GetExtension(strPhotoPath);
                        retImg.Save(ret);
                        retImg.Save(strPhotoPath);
                        grPhoto.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;
        }
        
    }
}