using System;
using System.Drawing;
using System.Windows.Forms;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Interface.Windows
{
    public partial class CategoryJDialog : Form
    {
        DBCategoryJ internalCat = null;

        public CategoryJDialog()
        {
            InitializeComponent();
        }

        public CategoryJDialog(DBCategoryJ t)
        {
            internalCat = t;
            InitializeComponent();
        }

        private void CategoryJDialog_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            if (internalCat != null)
            {
                RestoreCat();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Business.TaskyJManager.PushCategory(GetCat());
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdChoosePic_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picCategory.Image = new Bitmap(open.FileName);
            }
        }

        private void RestoreCat()
        {
            txtId.Text = internalCat.ID.ToString();
            txtName.Text = internalCat.Name;
            picCategory.Image = internalCat.AsWindowsBitmap();
        }

        private DBCategoryJ GetCat()
        {
            var cat = new DBCategoryJ();
            int idd = 0;
            int.TryParse(txtId.Text, out idd);
            cat.ID = idd;
            cat.Name = txtName.Text;
            if (picCategory.Image != null)
                cat.IconBase64 = Convert.ToBase64String(converterDemo(picCategory.Image));
            else
                cat.IconBase64 = null;

            return cat;
        }

        public static byte[] converterDemo(Image x)
        {
            ImageConverter ic = new ImageConverter();
            byte[] xByte = (byte[])ic.ConvertTo(x, typeof(byte[]));
            return xByte;
        }

    }

}
