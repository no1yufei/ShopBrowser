

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Tools
{
    public class PDFHelper
    {
        static public void MergePDFFiles(string[] sourcePdfList, string outMergeFile)
        {
            using (
                    Stream outputPdfStream = new FileStream(outMergeFile, FileMode.Create, FileAccess.Write,
                        FileShare.None))
            {
                var document = new Document();
                PdfCopy copy = null;
                try
                {
                    copy = new PdfCopy(document, outputPdfStream);
                }
                catch { }

                copy.SetMergeFields();
                document.Open();
                for (int i = 0; i < sourcePdfList.Length; i++)
                {
                    copy.AddDocument(new PdfReader(sourcePdfList[i]));
                }
                try
                {
                    copy.Close();
                }
                catch { }
            }
        }


    }
}
