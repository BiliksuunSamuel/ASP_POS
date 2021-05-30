using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POS_ASP.Models;

namespace POS_ASP.Services
{
    public class Validation
    {

        public ProductModel ValidateProductDetails(ProductModel model)
        {
            int validPrice = 0;
            if (model.productName.Length <= 0)
            {
                model.isValid = false;
                model.ErrorMessage = "Please enter the product name";
                return model;
            }else if(!int.TryParse(model.SellingPrice,out validPrice))
            {
                model.isValid = false;
                model.ErrorMessage = "Invalid Selling Price only numbers required";
                return model;
            }
            else if (!int.TryParse(model.CostPrice, out validPrice))
            {
                model.isValid = false;
                model.ErrorMessage = "Invalid Cost Price only numbers required";
                return model;
            }
            else
            {
                model.isValid = true;
                model.ErrorMessage = "";
                return model;
            }
        }
    }
}