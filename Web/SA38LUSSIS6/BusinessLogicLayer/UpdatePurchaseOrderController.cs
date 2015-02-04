using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using BusinessLogicLayer.Exception_Package;
namespace BusinessLogicLayer
{

    public class UpdatePurchaseOrderController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();
        /** get all supplier names **/

        public List<String> getAllSupplierList()
        {
            var q = from s in context.Suppliers
                    select s.SupplierID;
            List<String> supplierList = q.ToList<String>();
            return supplierList;
        }

        /** get PO by search results **/
        public List<PurchaseOrder> getPOListBySuppAndDate(String supplierCode, DateTime PODate)
        {

         var q =   (from p in context.PurchaseOrders
             where (EntityFunctions.TruncateTime(PODate) == EntityFunctions.TruncateTime(p.DateRaised) )
             && (p.Supplier.Equals(supplierCode)) 
             select p);

            List<PurchaseOrder> POList = q.ToList<PurchaseOrder>();
            return POList;
        }

        /** get PO by Supplier **/

        public List<PurchaseOrder> getPOListBySupplier(String supplierCode)
        {
            var q = from p in context.PurchaseOrders
                    where p.Supplier.Equals(supplierCode)
                    select p;
            List<PurchaseOrder> POList = q.ToList<PurchaseOrder>();
            return POList;
          }

        /** get purchaseorder status **/
        public List<String> getPOStatus()
        {
            List<String> POStatus = new List<String>();
            POStatus.Add("Ordered");
            POStatus.Add("Delivered");
            POStatus.Add("Cancel");
            return POStatus;
        }

       /** get PO details **/
        public IQueryable getPODetail(int PONumber)
        {
            var q = from pd in context.PurchaseOrderDetails
                    where pd.PONumber == PONumber
                    select new
                    { pd.ItemCode,
                      pd.Stock.ItemCategory,
                      pd.Stock.ItemDescription,
                      pd.Stock.UnitOfMeasure,
                      pd.Quantity,
                      pd.Price
                    };
            return q;
        }
        
        /** update PO Detail **/
        public void updatePO(int PONumber, String deliveryNo, DateTime deliveryDate, String POStatus, String remarks)
        {
            var q = from p in context.PurchaseOrders
                    where p.PONumber == PONumber
                    select p;
            PurchaseOrder po = (PurchaseOrder)q.FirstOrDefault();
            po.DeliveryNo = deliveryNo;
            po.POStatus = POStatus;
            po.DeliveryDate = deliveryDate;
            po.Comments = remarks;
            context.SaveChanges();
        }

        public void updatePO(int PONumber, String deliveryNo, DateTime deliveryDate, String POStatus)
        {
            var q = from p in context.PurchaseOrders
                    where p.PONumber == PONumber
                    select p;
            PurchaseOrder po = (PurchaseOrder)q.FirstOrDefault();
            po.DeliveryNo = deliveryNo;
            po.DeliveryDate = deliveryDate;
            po.POStatus = POStatus;
            context.SaveChanges();
        }

        /** Cancel PO**/
        public void CancelPO(int PONumber, String POStatus, String remarks)
        {
            var q = from p in context.PurchaseOrders
                    where p.PONumber == PONumber
                    select p;
            PurchaseOrder po = (PurchaseOrder)q.FirstOrDefault();
            po.Comments = remarks;
            po.POStatus = POStatus;
            context.SaveChanges();
        }

        /** get PO **/
        public PurchaseOrder getPO(int PONumber)
        {
            var q = from p in context.PurchaseOrders
                    where p.PONumber == PONumber
                    select p;
            PurchaseOrder po = (PurchaseOrder)q.FirstOrDefault();
            return po;
        }
    }
}
