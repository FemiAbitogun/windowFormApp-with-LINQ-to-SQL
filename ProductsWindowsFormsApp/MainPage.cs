using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsWindowsFormsApp
{
    public partial class MainPage : Form
    {
        MasterDBContextDataContext db = new MasterDBContextDataContext();
        private  bool dataLoaded=false;
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = db.Products;
            var value = from product in db.Products select product.ProductName;
            comboBox1.DataSource = value;
            comboBox1.DisplayMember = "ProductName";
            comboBox1.SelectedIndex = -1;
            dataLoaded = true;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataLoaded)
            {
                var value = from product in db.Products where product.ProductName.Contains(comboBox1.Text) select product;
                dataGridView1.DataSource = value;
            }  
        }
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var value = from product in db.Products where product.ProductName.Contains(comboBox1.Text) select product;
            dataGridView1.DataSource = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> CustomProductList = new List<Product>();            CustomProductList = db.Products.ToList();
        
            Product _product = new Product { Id = 1234, ProductName = "IenumerableEX", SupplierId = 5678, Package = "IenumerableEXPKG", IsDiscontinued = true };


            CustomProductList.Add(_product);
            dataGridView1.DataSource = CustomProductList;
        }

        private void createBTN_Click(object sender, EventArgs e)
        {
            Create createFormObject = new Create();
            createFormObject.ShowDialog();
        }

        private void updateBTN_Click(object sender, EventArgs e)
          
        {
            //MessageBox.Show(dataGridView1.SelectedRows.Count.ToString());
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Create c = new Create();
                c.INSERT_BTN.Text = "Update";
                c.ID_TXTFIELD.ReadOnly = true;
                c.ID_TXTFIELD.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                c.PRODUCTNAME_TXTFIELD.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                c.SUPPLIERID_TXT.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                c.PACKAGE_TXTFIELD.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                c.UNITPRICE_TXTFIELD.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                var _value = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                // MessageBox.Show(_value);
                if (_value.ToLower() == "true")
                {
                    c.ISDISCOUNTED_CHKFIELD.Checked = true;
                }
                else
                {
                    c.ISDISCOUNTED_CHKFIELD.Checked = false;
                }
                c.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a row to carry out update operation!!!");
            }
             
        }
    }


    
}
