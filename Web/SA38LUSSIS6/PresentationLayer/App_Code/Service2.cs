using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
    using System.Data;
    using BusinessLogicLayer;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service2 : IService2
{
    ReportDescrepancyController rd = new ReportDescrepancyController();
    static string connection = "data source=(local); Integrated security=SSPI; initial catalog=sa38team6";
    static string outstanding = "outstanding";
    static string approved = "approved";
    
    public iStock[] StockList()
    
    {
        ReportDescrepancyController rd = new ReportDescrepancyController();
        List<Stock> list = new List<Stock>();
        list = rd.getAllStock();
        List<iStock> ilist = new List<iStock>();
        for (int i = 0; i < list.Count; i++) // Loop through List with for
        {
            iStock ist = new iStock();
            ist = ChangeStockEntitytoiStock(list[i]);
            ilist.Add(ist);
        }
        return (ilist.ToArray<iStock>());
    }

    public iStock[] StockByID(string id)
    {
        List<iStock> list = new List<iStock>();
        ReportDescrepancyController rd = new ReportDescrepancyController();
        Stock stk = new Stock();
        stk = rd.getStockByID(id);
        iStock istk = new iStock();
        istk = ChangeStockEntitytoiStock(stk);
        list.Add(istk);
        return (list.ToArray<iStock>());
    }
    public iStock ChangeStockEntitytoiStock(Stock s){
        iStock ist=new iStock();
        ist.ItemCode = s.ItemCode;
        ist.ItemDescription = s.ItemDescription;
        ist.UnitofMeasure = s.UnitOfMeasure;
        ist.AvailableQty = (int)s.AvailableQty;
        ist.BinNumber = s.BinNumber;
        ist.Price = (float)s.Price1;
        return ist;
    }
    public bool addDiscrepancy(iDiscrepancy ides)
    {
        ReportDescrepancyController rd = new ReportDescrepancyController();
        Discrepancy des = new Discrepancy();
        des = changeiDestoDesEntity(ides);

        return rd.insertDiscrepancy(des); 
    }
    public Discrepancy changeiDestoDesEntity(iDiscrepancy ides)
    {
        Discrepancy des = new Discrepancy();
        des.DateRaised = DateTime.Today;
        des.RaisedBy = ides.RaisedBy;
        des.TotalAmount = float.Parse(ides.TotalAmount);
        des.DiscrepancyStatus = "Pending Approval";
        des.DateUpdated = DateTime.Today;
        return des;
    }
    public bool addDiscrepancyDetail(iDiscrepancyDetail idisd)
    {
        ReportDescrepancyController rd = new ReportDescrepancyController();
        DiscrepancyDetail disd = new DiscrepancyDetail();
        disd = changeiDisdtoDisdEntity(idisd);
        
        return rd.insertDiscrepancyDetail(disd);
    }
    public DiscrepancyDetail changeiDisdtoDisdEntity(iDiscrepancyDetail idisd)
    {
        DiscrepancyDetail disd = new DiscrepancyDetail();
        ReportDescrepancyController rd = new ReportDescrepancyController();
        disd.DiscrepancyID = rd.getDiscrepancyID();
        disd.ItemCode = idisd.ItemCode;
        if (idisd.Quantity < 0)
        {
            disd.Quantity = -1 * idisd.Quantity;
        }
        else
        {
            disd.Quantity = idisd.Quantity;
        }
        disd.Amount = float.Parse(idisd.Amount);
        disd.IsAdded = bool.Parse(idisd.IsAdded);
        disd.Reason = idisd.Reason;
        disd.DeletedFlag = false;
        return disd;
    }
}





