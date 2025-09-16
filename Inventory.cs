using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Inventory.CustomExceptions;

namespace Inventory
{
    public partial class Inventory : Form
    {
        CustomExceptions exceptions = new CustomExceptions();
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        public Inventory()
        {
            InitializeComponent();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = 
            {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
            };

            foreach(string category in ListOfProductCategory)
            {
                CategoryCmb.Items.Add(category);
            }

        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new StringFormatException("Product name must only contain letters.");
            return name;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))  // fixed regex: must match one or more digits
                throw new NumberFormatException("Quantity must be a valid number.");
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price, @"^(\d+(\.\d{1,2})?)$")) // e.g. 100 or 99.99
                throw new CurrencyFormatException("Selling price must be a valid currency value.");
            return Convert.ToDouble(price);
        }


    }


}
