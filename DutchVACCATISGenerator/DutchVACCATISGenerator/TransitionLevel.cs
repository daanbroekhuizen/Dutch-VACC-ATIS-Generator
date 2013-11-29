using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    public class TransitionLevel
    {
        public DataTable transitionLevel { get; set; }

        public TransitionLevel()
        {

            transitionLevel = new DataTable();

            generateColumns();

            generateRows();

        }

        public void generateRows()
        {
            transitionLevel.Rows.Add("1050", 40, 40, 40, 40, 40, 40, 40, 40, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30);
            transitionLevel.Rows.Add("1049", 40, 40, 40, 40, 40, 40, 40, 40, 40, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30);
            transitionLevel.Rows.Add("1048", 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30);


        }

        /// <summary>
        /// 
        /// </summary>
        public void generateColumns()
        {
             /*minus*/
            transitionLevel.Columns.Add("QNH", typeof(String));
            transitionLevel.Columns.Add("-20", typeof(int));
            transitionLevel.Columns.Add("-19", typeof(int));
            transitionLevel.Columns.Add("-18", typeof(int));
            transitionLevel.Columns.Add("-17", typeof(int));
            transitionLevel.Columns.Add("-16", typeof(int));
            transitionLevel.Columns.Add("-15", typeof(int));
            transitionLevel.Columns.Add("-14", typeof(int));
            transitionLevel.Columns.Add("-13", typeof(int));
            transitionLevel.Columns.Add("-12", typeof(int));
            transitionLevel.Columns.Add("-11", typeof(int));
            transitionLevel.Columns.Add("-10", typeof(int));
            transitionLevel.Columns.Add("-9", typeof(int));
            transitionLevel.Columns.Add("-8", typeof(int));
            transitionLevel.Columns.Add("-7", typeof(int));
            transitionLevel.Columns.Add("-6", typeof(int));
            transitionLevel.Columns.Add("-5", typeof(int));
            transitionLevel.Columns.Add("-4", typeof(int));
            transitionLevel.Columns.Add("-3", typeof(int));
            transitionLevel.Columns.Add("-2", typeof(int));
            transitionLevel.Columns.Add("-1", typeof(int));
            transitionLevel.Columns.Add("0", typeof(int));
            transitionLevel.Columns.Add("1", typeof(int));
            transitionLevel.Columns.Add("2", typeof(int));
            transitionLevel.Columns.Add("3", typeof(int));
            transitionLevel.Columns.Add("4", typeof(int));
            transitionLevel.Columns.Add("5", typeof(int));
            transitionLevel.Columns.Add("6", typeof(int));
            transitionLevel.Columns.Add("7", typeof(int));
            transitionLevel.Columns.Add("8", typeof(int));
            transitionLevel.Columns.Add("9", typeof(int));
            transitionLevel.Columns.Add("10", typeof(int));
            transitionLevel.Columns.Add("11", typeof(int));
            transitionLevel.Columns.Add("12", typeof(int));
            transitionLevel.Columns.Add("13", typeof(int));
            transitionLevel.Columns.Add("14", typeof(int));
            transitionLevel.Columns.Add("15", typeof(int));
            transitionLevel.Columns.Add("16", typeof(int));
            transitionLevel.Columns.Add("17", typeof(int));
            transitionLevel.Columns.Add("18", typeof(int));
            transitionLevel.Columns.Add("19", typeof(int));
            transitionLevel.Columns.Add("20", typeof(int));
            transitionLevel.Columns.Add("21", typeof(int));
            transitionLevel.Columns.Add("22", typeof(int));
            transitionLevel.Columns.Add("23", typeof(int));
            transitionLevel.Columns.Add("24", typeof(int));
            transitionLevel.Columns.Add("25", typeof(int));
            transitionLevel.Columns.Add("26", typeof(int));
            transitionLevel.Columns.Add("27", typeof(int));
            transitionLevel.Columns.Add("28", typeof(int));
            transitionLevel.Columns.Add("29", typeof(int));
            transitionLevel.Columns.Add("30", typeof(int));
            transitionLevel.Columns.Add("31", typeof(int));
            transitionLevel.Columns.Add("32", typeof(int));
            transitionLevel.Columns.Add("33", typeof(int));
            transitionLevel.Columns.Add("34", typeof(int));
            transitionLevel.Columns.Add("35", typeof(int));
            transitionLevel.Columns.Add("36", typeof(int));
            transitionLevel.Columns.Add("37", typeof(int));
            transitionLevel.Columns.Add("38", typeof(int));
            transitionLevel.Columns.Add("39", typeof(int));
            transitionLevel.Columns.Add("40", typeof(int));
        }

    }
}
