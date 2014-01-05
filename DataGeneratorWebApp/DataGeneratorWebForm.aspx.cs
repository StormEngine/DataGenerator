using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataGeneratorWebApp
{
    public partial class DataGeneratorWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CosineChart1.ChartAreas[0].AxisX.LabelStyle.Format = "hh:mm:ss.ffff";
        } // END protected void Page_Load(object sender, EventArgs e)

        protected void drpDwnLstInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT CosineOfCurrentAngle, TimeOfCosineOfCurrentAngle 
                               FROM CosineTest2
                               WHERE (IntervalAtWhichCosineIsTaken = " + drpDwnLstInterval.SelectedValue.ToString() + ")" +
                               @"ORDER BY TimeOfCosineOfCurrentAngle";
            
            CosineChartSqlDataSource1.SelectCommand = sqlSelect;
        } // END protected void drpDwnLstInterval_SelectedIndexChanged(object sender, EventArgs e)
    } // END public partial class DataGeneratorWebForm : System.Web.UI.Page
} // namespace DataGeneratorWebApp