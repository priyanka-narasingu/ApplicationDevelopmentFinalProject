/* Developer: Vignesh Sridharan*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.StoreClerk
{
    public partial class StationeryRetrievalReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dsGenerateStationeryRetrieval ds = new dsGenerateStationeryRetrieval();
            dsGenerateStationeryRetrievalTableAdapters.v_GenerateStationeryRetrievalTableAdapter ta = new dsGenerateStationeryRetrievalTableAdapters.v_GenerateStationeryRetrievalTableAdapter();
            crGenerateStationeryRetrieval rp = new crGenerateStationeryRetrieval();

            ta.Fill(ds.v_GenerateStationeryRetrieval);
            rp.SetDataSource(ds);
            crvGenerateStationeryRetrieval.ReportSource = rp;
        }
    }
}