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
    public partial class Create : Form
    {
        MasterDBContextDataContext db = new MasterDBContextDataContext();
        public Create()
        {
            InitializeComponent();
        }
       
        private void Create_Load(object sender, EventArgs e)
        {

        }

        private void INSERT_BTN_Click(object sender, EventArgs e)
        {
         
            if (ID_TXTFIELD.ReadOnly==false)
            {
                Product newProductObject = new Product
                {
                    Id = int.Parse(ID_TXTFIELD.Text),
                    ProductName = PRODUCTNAME_TXTFIELD.Text,
                    SupplierId = int.Parse(SUPPLIERID_TXT.Text),
                    Package = PACKAGE_TXTFIELD.Text,
                    UnitPrice = decimal.Parse(UNITPRICE_TXTFIELD.Text),
                    IsDiscontinued = ISDISCOUNTED_CHKFIELD.Checked
                };

                db.Products.InsertOnSubmit(newProductObject);
                //  db.SubmitChanges();
               // MessageBox.Show(ISDISCOUNTED_CHKFIELD.Checked.ToString());
            }
            else
            {
                int searchParams = int.Parse(ID_TXTFIELD.Text);
                var obj = db.Products.FirstOrDefault(data=>data.Id== searchParams);
                MessageBox.Show($"Original instance from DB : Product id={obj.Id}  Product  Name={obj.ProductName} Discounted ={obj.IsDiscontinued}");
            }
        }
           
    }
}
