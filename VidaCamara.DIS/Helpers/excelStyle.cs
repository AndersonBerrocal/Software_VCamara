using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace VidaCamara.DIS.Helpers
{
    public partial class excelStyle
    {
        /// <summary>
        /// Crea un cell style 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="color"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public ICellStyle setFontText(short point, bool color, XSSFWorkbook book)
        {
            var font = book.CreateFont();
            font.FontName = "Calibri";
            font.Color = (IndexedColors.Black.Index);
            font.FontHeightInPoints = point;

            var style = book.CreateCellStyle();
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            if (color)
            {
                style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                style.FillPattern = FillPattern.SolidForeground;
            }
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            return style;
        }
    }
}
