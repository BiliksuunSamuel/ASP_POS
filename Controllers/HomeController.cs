using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POS_ASP.Models;
using POS_ASP.Services;
using ZXing;
namespace POS_ASP.Controllers
{
    public class HomeController : Controller
    {
        Validation validation = new Validation();
        ProductModel product = new ProductModel();
        public ActionResult Index()
        {
            return View();
        }

        

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(FormCollection collection)
        {
            ProductModel model = new ProductModel();
            string method = collection["hidden"].ToString();
            
            switch (method)
            {
                case "search":
                     model.productID = collection["productID"].ToString();
                    //model.productID = "PID05";
                    model = product.LoadProductInfo(product.GetProductByProductID(model));

                    if (model.isValid)
                    {
                        ViewBag.pid = model.productID;
                        ViewBag.pname = model.productName;
                        ViewBag.psp = model.SellingPrice;
                        ViewBag.pcp = model.CostPrice;
                        ViewBag.result = model.ErrorMessage;
                        return View();
                    }
                    else
                    {
                        ViewBag.result = model.ErrorMessage;
                        return View();
                    }
                case "register":
                    string pname = collection["pname"].ToString();
                    string psp = collection["psp"].ToString();
                    string pcp = collection["pcp"].ToString();
                    model.productName = pname;
                    model.SellingPrice = psp;
                    model.CostPrice = pcp;

                    model = validation.ValidateProductDetails(model);
                    if (model.isValid)
                    {
                        model.productID = product.GenerateProductId(product.GetAllProduct());
                        string output = product.RegisterProduct(model);
                        ViewBag.results = output;
                        BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                        string qrname = string.Format("{0}.PNG", model.productName);
                        try
                        {
                            writer.Write(model.productID).Save(@"C:\Users\SAMUELBILLS\Desktop\CodersHub\POS_ASP\QRResources\" + qrname);
                        }
                        catch (Exception ex)
                        {

                            ViewBag.results = ex.Message;
                            return View();
                        }
                        System.Threading.Thread.Sleep(1000);
                        return View();
                    }
                    else
                    {
                        ViewBag.results = model.ErrorMessage;
                        return View();
                    }

                default:
                    string pnamed = collection["pname"].ToString();
                    string pspd = collection["psp"].ToString();
                    string pcpd = collection["pcp"].ToString();
                    model.productName = pnamed;
                    model.SellingPrice = pspd;
                    model.CostPrice = pcpd;

                    model = validation.ValidateProductDetails(model);
                    if (model.isValid)
                    {
                        model.productID = product.GenerateProductId(product.GetAllProduct());
                        string output = product.RegisterProduct(model);
                        ViewBag.results = output;
                        BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                        string qrname = string.Format("{0}.PNG", model.productName);
                        try
                        {
                            writer.Write(model.productID).Save(@"C:\Users\SAMUELBILLS\Desktop\CodersHub\POS_ASP\QRResources\" + qrname);
                        }
                        catch (Exception ex)
                        {

                            ViewBag.results = ex.Message;
                            return View();
                        }
                        System.Threading.Thread.Sleep(1000);
                        return View();
                    }
                    else
                    {
                        ViewBag.results = model.ErrorMessage;
                        return View();
                    };
            }


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

     

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}