using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using NPOI.SS.Util;

namespace Helper
{
    public class ExcelClass
    {
        public short FontHeightInPoints = 13;
        public short row_height = 25;
        public short col_width = 100;
        public bool Border = true;
        public DataTable dataTable = null;
        public string FilePath = "..\\";
        public string FileName = "excle.xls";
        public ExcelTitleInfos TitleInfos = new ExcelTitleInfos()
        {
            sheetName = "表格导出",
            sheetCreatedDate = DateTime.Now
        };
        public List<ColumnNamePosition> ColNamePosition = null;

        private ICellStyle StyleDefault;
        private ICellStyle decimalMoney;
        private ICellStyle headStyleDefault;
        private int start_row = 3;//数据开始行数,包含列头
        private short Row_Height;
        private IWorkbook hssfWorkBook;
        private ISheet mySheet;

        public ExcelClass()
        {

        }
        public void DataTableToExcel()
        {
            if (dataTable == null)
            {
                throw new Exception("dataTable == null");
            }
            if (ColNamePosition == null)
            {
                throw new Exception("ColNamePosition == null");
            }
            hssfWorkBook = new HSSFWorkbook();
            mySheet = hssfWorkBook.CreateSheet(TitleInfos.sheetName);//创建sheet

            Row_Height = (short)(20 * row_height);//设置单元格高度

            //设置字符串格式,字号,居中
            StyleDefault = createCellDefaultStyle();
            //设置格式__金额格式
            decimalMoney = createCellDecimalMoneyStyle();
            //标题样式
            headStyleDefault = createCellHeadDefaultStyle();

            mySheet.CreateRow(start_row).Height = Row_Height;//创建第n行

            createSheetColumn();
            createContent();

            createHeadTow();
            createHeadThree();
            createHeadOne();

            //冻结标题行
            mySheet.CreateFreezePane(0, 1 + start_row);

            //自动调整列宽
            int colums = mySheet.GetRow(start_row).LastCellNum;
            try
            {
                for (int i = 0; i < colums; i++)
                {
                    mySheet.AutoSizeColumn(i);
                }
            }
            catch { }
        }
        public void WriteExcel()
        {
            try
            {
                FileStream file;
                file = new FileStream(FilePath + FileName, FileMode.Create);
                hssfWorkBook.Write(file);
                file.Close();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public List<string> ReadSheets(string path)
        {
            return new List<string>();
        }
        public DataTable ReadSheetBy(string SheetName)
        {
            return new DataTable();
        }

        private int getColumnNamePosition(List<ColumnNamePosition> nameList, string columnName)
        {
            int i = -1;
            int ___i = 0;
            //nameList.OrderBy(p => p.position);
            foreach (ColumnNamePosition names in nameList)
            {
                if (names.EN_keyName == columnName)
                {
                    i = ___i;
                    break;
                }
                ___i++;
            }
            return i;
        }
        private ICellStyle createCellDefaultStyle()
        {
            NPOI.SS.UserModel.IFont font = hssfWorkBook.CreateFont();//设置字号
            font.FontHeightInPoints = FontHeightInPoints;

            ICellStyle StyleDefault = hssfWorkBook.CreateCellStyle();
            StyleDefault.Alignment = HorizontalAlignment.CENTER_SELECTION;
            StyleDefault.VerticalAlignment = VerticalAlignment.CENTER;
            StyleDefault.SetFont(font);
            StyleDefault.WrapText = false;
            //合并完标题头,给单元格加边框
            if (Border)
            {
                StyleDefault.BorderLeft = BorderStyle.THIN;
                StyleDefault.BorderRight = BorderStyle.THIN;
                StyleDefault.BorderTop = BorderStyle.THIN;
                StyleDefault.BorderBottom = BorderStyle.THIN;
            }
            return StyleDefault;
        }
        private ICellStyle createCellDecimalMoneyStyle()
        {
            ICellStyle decimalMoney = hssfWorkBook.CreateCellStyle();
            decimalMoney.CloneStyleFrom(createCellDefaultStyle());
            decimalMoney.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
            return decimalMoney;
        }
        private ICellStyle createCellHeadDefaultStyle()
        {
            ICellStyle headStyleDefault = hssfWorkBook.CreateCellStyle();
            headStyleDefault.CloneStyleFrom(StyleDefault);
            return headStyleDefault;
        }

        private void createSheetColumn()
        {
            #region 创建标题列
            //取得列标题
            int colIndex = -1;
            //循环列头翻译数据
            for (int i = 0; i < ColNamePosition.Count; i++)
            {
                //从表中找到符合的列头
                if (dataTable.Columns.IndexOf(ColNamePosition[i].EN_keyName) > -1)
                {
                    colIndex++;
                    //添加到Excel
                    ICell iRowCol = mySheet.GetRow(start_row).CreateCell(colIndex);
                    iRowCol.SetCellValue(ColNamePosition[i].CN_keyName);
                    iRowCol.CellStyle = StyleDefault;
                }
            }
            #endregion

        }

        private void createContent()
        {
            //导入数据
            int rowIndex = 0, colIndex = -1;
            //取得表格中的数据	
            foreach (DataRow row in dataTable.Rows)
            {
                mySheet.CreateRow(++rowIndex + start_row).Height = Row_Height;//创建行,并设置高度
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (isShowTableColumn(col.ColumnName))
                    {
                        //从nameList获取该列的位置,找不到为-1
                        colIndex = getColumnNamePosition(ColNamePosition, col.ColumnName);
                        if (colIndex == -1)
                            continue;
                        try
                        {
                            //创建单元格
                            ICell cell = mySheet.GetRow(rowIndex + start_row).CreateCell(colIndex);
                            cell.CellStyle.WrapText = false;
                            cell.CellStyle = StyleDefault;

                            object tableCellValue = row[col.ColumnName];
                            setSheetCellValue(tableCellValue, cell);
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                }
            }
        }
        private bool isShowTableColumn(string ColumnName)
        {
            foreach (ColumnNamePosition names in ColNamePosition)
            {
                if (names.EN_keyName == ColumnName)
                {
                    return true;
                }
            }
            return false;
        }
        private void setSheetCellValue(object tableCellValue, ICell cell)
        {
            switch (tableCellValue.GetType().ToString())
            {
                case "System.DBNull":
                    cell.SetCellValue("");
                    break;
                case "System.Double":
                    cell.SetCellValue(Convert.ToDouble(tableCellValue));
                    break;
                case "System.Int32":
                    cell.SetCellValue(Convert.ToInt32(tableCellValue));
                    break;
                case "System.Decimal":
                    cell.SetCellValue(Convert.ToDouble(tableCellValue));
                    cell.CellStyle = decimalMoney;
                    break;
                case "System.DateTime":
                    DateTime _temp_date = DateTime.Now;
                    if (DateTime.TryParse(tableCellValue.ToString(), out _temp_date))
                    {
                        cell.SetCellValue(_temp_date.ToString("yyyy年MM月dd日"));
                    }
                    else
                    {
                        cell.SetCellValue(tableCellValue.ToString());
                    }
                    break;
                case "System.String":
                    cell.SetCellValue(tableCellValue.ToString());
                    break;
                case "System.Boolean":
                    cell.SetCellValue(tableCellValue.ToString() == "True" ? "是" : "否");
                    break;
                default:
                    cell.SetCellValue(tableCellValue.ToString());
                    break;
            }
        }

        private void createHeadOne()
        {
            mySheet.CreateRow(0).Height = Row_Height;
            ICell first_cell_title = mySheet.GetRow(0).CreateCell(0);

            string title_value = TitleInfos.sheetName;
            if (!string.IsNullOrEmpty(TitleInfos.UnitNameS)
                || !string.IsNullOrEmpty(TitleInfos.DormitoryNameS))//宿舍和单位不为空
            {
                mySheet.GetRow(0).Height = (short)(Row_Height * 3);//设置标题高度
                title_value = TitleInfos.UnitNameS + "\r\n"
                    + TitleInfos.DormitoryNameS + "\r\n"
                    + TitleInfos.sheetName;
            }
            first_cell_title.SetCellValue(title_value);

            first_cell_title.CellStyle = headStyleDefault;
            first_cell_title.CellStyle.WrapText = true;

            //合并第一行
            mySheet.AddMergedRegion(
                new CellRangeAddress(0, 0, 0, mySheet.GetRow(start_row).LastCellNum - 1));
        }
        private void createHeadTow()
        {
            mySheet.CreateRow(1).Height = Row_Height;
            ICell cell_2_title = mySheet.GetRow(1).CreateCell(0);

            cell_2_title.SetCellValue(TitleInfos.limitsDateS + TitleInfos.limitsGradeS + TitleInfos.limitsTermS);//周期范围,年级,学期

            cell_2_title.CellStyle = headStyleDefault;
            mySheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, mySheet.GetRow(start_row).LastCellNum - 1));
        }
        private void createHeadThree()
        {
            mySheet.CreateRow(2).Height = Row_Height;
            ICell cell_3_title = mySheet.GetRow(2).CreateCell(0);

            cell_3_title.SetCellValue("填报日期：" + DateTime.Now.ToString("yyyy年MM月dd日"));

            ICellStyle head3 = hssfWorkBook.CreateCellStyle();
            head3.CloneStyleFrom(headStyleDefault);
            head3.Alignment = HorizontalAlignment.RIGHT;
            cell_3_title.CellStyle = head3;
            mySheet.AddMergedRegion(new CellRangeAddress(2, 2, 0, mySheet.GetRow(start_row).LastCellNum - 1));
        }
    }

    public class ExcelTitleInfos
    {
        public string sheetName { get; set; }
        public string limitsDateS { get; set; }
        public string limitsGradeS { get; set; }
        public string limitsTermS { get; set; }
        public DateTime sheetCreatedDate { get; set; }
        public string DormitoryNameS { get; set; }
        public string UnitNameS { get; set; }
    }
    public class ColumnNamePosition
    {
        public string EN_keyName { get; set; }
        public string CN_keyName { get; set; }
        public int Position { get; set; }
    }
}