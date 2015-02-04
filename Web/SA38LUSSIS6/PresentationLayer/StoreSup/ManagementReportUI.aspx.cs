/* Development of Management Reports Page */
/* Developer: Vignesh Sridharan */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace PresentationLayer.StoreSup
{
    public partial class ManagementReportsUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            String dFromSt = (dtpFrom.Text) + " 12:00:00 AM";
            String dToSt = (dtpTo.Text) + " 11:59:59 PM";

            DateTime dFrom = DateTime.ParseExact(dFromSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            DateTime dTo = DateTime.ParseExact(dToSt, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            dsManagementReports ds = new dsManagementReports();


            /* For Orders By Category - Report*/

            if (ddlReport.SelectedValue == "OBC")
            {
                dsManagementReportsTableAdapters.sp_OrdersByCategoryTableAdapter ta = new dsManagementReportsTableAdapters.sp_OrdersByCategoryTableAdapter();

                if (optTable.Checked)
                {
                    crOBCategoryTable rp = new crOBCategoryTable();
                    ta.Fill(ds.sp_OrdersByCategory, dFrom, dTo);
                    rp.SetDataSource(ds);
                    crvOBCtbl.ReportSource = rp;
                    MultiView1.SetActiveView(OBCtblView);
                }
                else
                {
                    crOBCategoryChart rpc = new crOBCategoryChart();
                    ta.Fill(ds.sp_OrdersByCategory, dFrom, dTo);
                    rpc.SetDataSource(ds);
                    crvOBCCht.ReportSource = rpc;
                    MultiView1.SetActiveView(OBCchtView);
                }
            }

            /* For Orders By Department - Report*/

            else if (ddlReport.SelectedValue == "OBD")
            {
                dsManagementReportsTableAdapters.sp_OrdersByDepartmentCTGTableAdapter ta = new dsManagementReportsTableAdapters.sp_OrdersByDepartmentCTGTableAdapter();

                if (optTable.Checked)
                {
                    crOBDeptTable rp = new crOBDeptTable();
                    ta.Fill(ds.sp_OrdersByDepartmentCTG, dFrom, dTo);
                    rp.SetDataSource(ds);
                    crvOBDtbl.ReportSource = rp;
                    MultiView1.SetActiveView(OBDtblView);
                }
                else
                {
                    crOBDeptChart rpc = new crOBDeptChart();
                    ta.Fill(ds.sp_OrdersByDepartmentCTG, dFrom, dTo);
                    rpc.SetDataSource(ds);
                    crvOBDcht.ReportSource = rpc;
                    MultiView1.SetActiveView(OBDchtView);
                }
            }

            /* For Orders By Suppliers - Report*/

            else if (ddlReport.SelectedValue == "OBS")
            {
                dsManagementReportsTableAdapters.sp_OrdersBySuppliersTableAdapter ta = new dsManagementReportsTableAdapters.sp_OrdersBySuppliersTableAdapter();

                if (optTable.Checked)
                {
                    crOBSuppliersTable rp = new crOBSuppliersTable();

                    ta.Fill(ds.sp_OrdersBySuppliers, dFrom, dTo);
                    rp.SetDataSource(ds);
                    crvOBStbl.ReportSource = rp;
                    MultiView1.SetActiveView(OBStblView);
                }
                else
                {
                    crOBSuppliersChart rpc = new crOBSuppliersChart();
                    ta.Fill(ds.sp_OrdersBySuppliers, dFrom, dTo);
                    rpc.SetDataSource(ds);
                    crvOBScht.ReportSource = rpc;
                    MultiView1.SetActiveView(OBSchtView);
                }
            }
        }
    }
}