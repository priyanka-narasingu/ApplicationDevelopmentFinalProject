using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using BusinessLogicLayer.Exception_Package;

namespace BusinessLogicLayer
{
    public class CreatePurchaseOrderController
    {
        SA38ADTeam6Entities context = new SA38ADTeam6Entities();
        DataTable dt = new DataTable();
        bool success = true;
        String errorMsg;

        /** Get all the items below reorder level **/
        public List<Stock> getItemsBelowReorder()
        {
            var q = from i in context.Stocks
                    where (i.AvailableQty <= i.ReorderLevel) && (i.DeletedFlag == false)
                    select i;
            List<Stock> stockList = q.ToList<Stock>();
            return stockList;
        }

        /** Find the reorder quantity from approved and outstanding requests **/
        public DataTable calculateReorderQty(List<Stock> stockList)
        {
            int itemCount;
            int reorderQty = 0;
            int key = 0;
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("ItemCode", typeof(String)),
                                new DataColumn("ItemCategory", typeof(string)),
                                new DataColumn("ItemDescription",typeof(string)),
                                new DataColumn("UnitOfMeasure",typeof(string)),
                                new DataColumn("MinReorderQty",typeof(int)),
                                new DataColumn("Supplier",typeof(String)),
                                new DataColumn("Price",typeof(double)),
                                new DataColumn("key",typeof(int))
                               });

            var q = from rd in context.RequestDetails
                    where rd.Request.RequestStatus.Equals("Approved") ||
                          rd.Request.RequestStatus.Equals("Outstanding") &&
                          (rd.DeletedFlag == false)
                    select rd;
            List<RequestDetail> rdList = q.ToList<RequestDetail>();
            if (!(rdList.Count == 0))
            {
                foreach (var i in stockList)
                {
                    itemCount = 0;
                    foreach (RequestDetail r in rdList)
                    {
                        if (i.ItemCode.Equals(r.ItemCode))
                        {
                            itemCount += Convert.ToInt32(r.Quantity);
                        }
                    }

                    reorderQty = checkForReorderQty(i, itemCount);
                    key++;
                    dt.Rows.Add(i.ItemCode, i.ItemCategory, i.ItemDescription, i.UnitOfMeasure, reorderQty, "",0,key);
                    dt.AcceptChanges();
                }

            }
            else
            {
                foreach (var i in stockList)
                {
                    key++;
                    dt.Rows.Add(i.ItemCode, i.ItemCategory, i.ItemDescription, i.UnitOfMeasure, i.MinReorderQty, "", 0,key);
                    dt.AcceptChanges();
                }
            }

            return dt;
        }

        /** calculate reorder quantity **/
        public int checkForReorderQty(Stock item, int itemCount)
        {
            int reorderdQty = 0;
            int difference = 0;
            if (itemCount > (item.MinReorderQty + item.AvailableQty))
            {
                reorderdQty = Convert.ToInt32(itemCount - (item.MinReorderQty + item.AvailableQty) + item.ReorderLevel);
            }
            else if (itemCount <= (item.MinReorderQty + item.AvailableQty))
            {
                difference = Convert.ToInt32((item.MinReorderQty + item.AvailableQty) - itemCount);
                if (difference < reorderdQty)
                {
                    reorderdQty = Convert.ToInt32(item.ReorderLevel - difference);
                }
                else
                    reorderdQty = Convert.ToInt32(item.MinReorderQty);

            }

            return reorderdQty;
        }

        /** get item details **/
        public Stock getItemDetails(String itemCode)
        {
            var q = (from s in context.Stocks
                     where s.ItemCode.Equals(itemCode)
                     select s).First();
            return q;
        }


        /** get supplier list for item **/
        public List<String> getSupplierList(String itemCode)
        {

            var q = (from s in context.Stocks
                     where s.ItemCode.Equals(itemCode)
                     select new
                     {
                         s.Supplier1,
                         s.Supplier2,
                         s.Supplier3
                     }).First();

            List<String> supplierList = new List<String>();

            supplierList.Add(q.Supplier1);
            supplierList.Add(q.Supplier2);
            supplierList.Add(q.Supplier3);
            return supplierList;
        }

        /** get the price for supplier **/
        public double getPriceForSupplier(String itemCode, String supplierCode)
        {
            double price = 0;
            var item = (from i in context.Stocks
                        where i.ItemCode.Equals(itemCode)
                        select i).First();

            if (item.Supplier1.Equals(supplierCode))
            {
                price = Convert.ToDouble(item.Price1);
            }
            else if (item.Supplier2.Equals(supplierCode))
            {
                price = Convert.ToDouble(item.Price2);
            }
            else if (item.Supplier3.Equals(supplierCode))
            {
                price = Convert.ToDouble(item.Price2);
            }

            return price;

        }

        /** Get item category **/
        public List<String> getItemCategory()
        {
            var q = (from x in context.Stocks
                     select x.ItemCategory).Distinct();
            List<String> icat = (List<String>)q.ToList();
            return icat;
        }

        /** create purchase order **/
        public void createPO(String supplierCode, double totalAmount)
        {
            PurchaseOrder addPO = new PurchaseOrder();
            addPO.Supplier = supplierCode;
            addPO.TotalAmount = totalAmount;
            addPO.POStatus = "Ordered";
            addPO.DateRaised = DateTime.Now;
            addPO.DateUpdated = DateTime.Now;
            addPO.DeletedFlag = false;
            try
            {
                context.AddToPurchaseOrders(addPO);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                errorMsg = "Failed to create purchase order";
                throw new CreateFailedException(errorMsg);
            }
        }

        /** get the last PO from database **/
        public int getLatestPOID()
        {
            var q = from p in context.PurchaseOrders
                     orderby p.PONumber descending
                     select p.PONumber;
            int PONumber = (int)q.FirstOrDefault();
            return PONumber;
        }
        /** create purchase order detail **/

        public void createPODetail(int PONumber, String itemCode, int qty, double price)
        {
            PurchaseOrderDetail addPODetail = new PurchaseOrderDetail();
            addPODetail.PONumber = PONumber;
            addPODetail.ItemCode = itemCode;
            addPODetail.Quantity = qty;
            addPODetail.Price = price;
            addPODetail.DeletedFlag = false;
            try
            {
                context.AddToPurchaseOrderDetails(addPODetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                errorMsg = "Failed to create purchase order";
                throw new CreateFailedException(errorMsg);
            }
        }


    }
}
