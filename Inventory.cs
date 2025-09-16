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
        BindingSource showProductList = new BindingSource();
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

        // Custom Exceptions are called from CustomExceptions.cs
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new StringFormatException("Product name should only contain alphabetic characters.");
            return name;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))
                throw new NumberFormatException("Quantity must be entered as digits only.");
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Selling price should be a valid numeric amount.");
            return Convert.ToDouble(price);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(ProductTxt.Text);
                _Category = CategoryCmb.Text;
                _MfgDate = MfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = ExpDate.Value.ToString("yyyy-MM-dd");
                _Description = DescriptionTxt.Text;
                _Quantity = Quantity(QtyTxt.Text);
                _SellPrice = SellingPrice(SellPriceTxt.Text);

                showProductList.Add(new ProductClass(
                    _ProductName, _Category, _MfgDate,
                    _ExpDate, _SellPrice, _Quantity, _Description
                ));
                    
                DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataGrid.DataSource = showProductList;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProductTxt.Clear();
                CategoryCmb.SelectedIndex = -1;
                MfgDate.Value = DateTime.Now;
                ExpDate.Value = DateTime.Now;
                DescriptionTxt.Clear();
                QtyTxt.Clear();
                SellPriceTxt.Clear();
            }
        }
    }
}
