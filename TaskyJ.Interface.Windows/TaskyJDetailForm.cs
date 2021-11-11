using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Interface.Windows
{

    public partial class TaskyJDetailForm : Form
    {
        DBTaskJ internalTask = null;
        //TODO login
        private DBSessionJ CurrentSession = null;

        private static void cboDrawImageAndText_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;
            ComboBox cbo = sender as ComboBox;
            ImageAndTextItem item = (ImageAndTextItem)cbo.Items[e.Index];
            item.MeasureItem(e);
        }

        private static void cboDrawImageAndText_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ComboBox cbo = sender as ComboBox;
                ImageAndTextItem item = (ImageAndTextItem)cbo.Items[e.Index];
                item.DrawItem(e);
            }
        }

        public TaskyJDetailForm(DBSessionJ session)
        {
            InitializeComponent();
            CurrentSession = session;
        }

        public TaskyJDetailForm(DBSessionJ session, DBTaskJ t)
        {
            InitializeComponent();
            internalTask = t;
            CurrentSession = session;
        }

        private void TaskyJDetailForm_Load(object sender, EventArgs e)
        {
            dpkDeadlineDate.CustomFormat = " ";
            dpkDeadlineTime.CustomFormat = " ";
            dpkDeadlineTime.Enabled = false;
            cboComplete.Items.Clear();
            for (int i = 0; i <= 100; i += 5)
            {
                cboComplete.Items.Add(i);
            }
            cboPriority.Items.Clear();
            foreach (DBTaskJ.TaskPriority item in Enum.GetValues(typeof(DBTaskJ.TaskPriority)))
            {
                cboPriority.Items.Add(item);
            }
            LoadCategories();
            if (internalTask != null)
            {
                RestoreTask();
                if (internalTask.Deleted)
                {
                    cmdOK.Enabled = cmdUndo.Enabled = false;
                    txtDescription.Enabled = false;
                    txtCreated.Enabled = false;
                    txtFinished.Enabled = false;
                    txtName.Enabled = false;
                    cboComplete.Enabled = false;
                    chkFinished.Enabled = false;
                    cboPriority.Enabled = false;
                    cboCategories.Enabled = false;
                    dpkDeadlineDate.Enabled = dpkDeadlineTime.Enabled = false;
                    cmdDelete.Text = "Restore";
                }
            }
            else
            {
                cmdDelete.Enabled = cmdUndo.Enabled = false;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            var t = new DBTaskJ();
            int.TryParse(txtId.Text, out int idd);
            t.ID = idd;
            t.Name = txtName.Text;
            t.Description = txtDescription.Text;
            try
            {
                if (string.IsNullOrWhiteSpace(cboComplete.Text))
                    t.Completed = 0;
                else
                    t.Completed = Convert.ToByte(cboComplete.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (internalTask != null)
            {
                t.CreationDate = internalTask.CreationDate;
                t.FinishDate = internalTask.FinishDate;
            }
            t.SetPriority(cboPriority.SelectedIndex);
            t.IDCategory = null;
            if (cboCategories.SelectedItem != null)
                if (((ImageAndTextItem)cboCategories.SelectedItem).Id > 0)
                    t.IDCategory = ((ImageAndTextItem)cboCategories.SelectedItem).Id;

            if (dpkDeadlineDate.CustomFormat != " ")
                t.Deadline = dpkDeadlineDate.Value.Date.AddHours(dpkDeadlineTime.Value.Hour).AddMinutes(dpkDeadlineTime.Value.Minute).AddSeconds(dpkDeadlineTime.Value.Second);
            else
                t.Deadline = null;

            Business.TaskyJManager.PushTask(t, CurrentSession);

            Close();
        }

        private void cmdUndo_Click(object sender, EventArgs e)
        {
            RestoreTask();
        }

        private void RestoreTask()
        {
            txtId.Text = internalTask.ID.ToString();
            txtName.Text = internalTask.Name;
            txtDescription.Text = internalTask.Description;
            txtCreated.Text = internalTask.CreationDate.ToString();
            cboComplete.Text = internalTask.Completed.ToString();
            chkFinished.Visible = (cboComplete.Text == "100");
            cboPriority.SelectedIndex = ((byte)internalTask.Priority);
            if (internalTask.Completed == 100)
            {
                txtFinished.Text = internalTask.FinishDate.Value.ToString();
                lblFinished.Visible = txtFinished.Visible = true;
            }
            if (internalTask.Category != null)
            {
                picCategory.Image = internalTask.Category.AsWindowsBitmap();
                cboCategories.SelectedIndex = cboCategories.Items.Cast<ImageAndTextItem>().Select(i => i.Id).ToList().IndexOf(internalTask.Category.ID);
            }
            if (internalTask.Deadline != null)
            {
                dpkDeadlineDate.Value = internalTask.Deadline.Value;
                dpkDeadlineTime.Value = internalTask.Deadline.Value;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (internalTask != null)
            {
                if (internalTask.Deleted == false)
                {
                    if (MessageBox.Show("Are you sure?", "Delete task", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Business.TaskyJManager.RemoveTask(internalTask.ID);
                }
                else
                {
                    if (MessageBox.Show("Are you sure?", "Restore task", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Business.TaskyJManager.RestoreTask(internalTask.ID);
                }
                Close();
            }
        }

        private void chkFinished_CheckedChanged(object sender, EventArgs e)
        {
            chkFinished.Checked = true;
        }

        private void cboComplete_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboComplete.SelectedItem != null)
                chkFinished.Visible = (cboComplete.Text == "100");
        }

        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategories.SelectedItem == null)
            {
                if (this.internalTask != null)
                {
                    internalTask.Category = null;
                    picCategory.Image = null;
                }
            }
            else
            {
                int id = ((ImageAndTextItem)cboCategories.SelectedItem).Id;
                if (id <= 0)
                {
                    if (id == -2)
                    {
                        if (new CategoryJDialog().ShowDialog() == DialogResult.OK)
                        {
                            LoadCategories();
                            cboCategories.SelectedIndex = cboCategories.Items.Count - 1;
                        }
                    }
                    else
                    {
                        if (internalTask != null)
                            internalTask.Category = null;
                        cboCategories.SelectedItem = null;
                    }
                }
                else
                {
                    picCategory.Image = ((ImageAndTextItem)cboCategories.SelectedItem).Picture;
                }
            }
        }

        private void dpkDeadlineDate_ValueChanged(object sender, EventArgs e)
        {
            if (dpkDeadlineDate.CustomFormat == " ")
            {
                dpkDeadlineDate.CustomFormat = "dd/MM/yyyy";
                dpkDeadlineTime.CustomFormat = "HH:mm:ss";
                dpkDeadlineTime.Value = new DateTime((DateTime.Now.Ticks / 1000000000) * 1000000000, DateTimeKind.Unspecified);
                dpkDeadlineTime.Enabled = true;
            }
        }

        private void cmdClearDeadline_Click(object sender, EventArgs e)
        {
            dpkDeadlineDate.CustomFormat = dpkDeadlineTime.CustomFormat = " ";
        }

        private void LoadCategories()
        {
            cboCategories.Items.Clear();
            var font = new Font(cboCategories.Font.Name, cboCategories.Font.Size);
            List<ImageAndTextItem> lstCat = new List<ImageAndTextItem>();
            foreach (var cat in TaskyJ.Business.TaskyJManager.RetrieveCategories())
            {
                lstCat.Add(new ImageAndTextItem(cat.AsWindowsBitmap(), cat.ID, cat.Name + "\n", font));
            }
            cboCategories.DrawMode = DrawMode.OwnerDrawVariable;
            cboCategories.Items.Clear();
            lstCat.Insert(0, new ImageAndTextItem(new Bitmap(1, 1), -2, "<<Add new>>\n", font));
            lstCat.Insert(0, new ImageAndTextItem(new Bitmap(1, 1), -1, "<<Delete>>\n", font));
            cboCategories.Items.AddRange(lstCat.ToArray());
            cboCategories.MeasureItem += cboDrawImageAndText_MeasureItem;
            cboCategories.DrawItem += cboDrawImageAndText_DrawItem;
        }

    }

    public static class ExtensionMethods
    {
        public static Bitmap AsWindowsBitmap(this DBCategoryJ cat)
        {
            if (cat == null)
                return new Bitmap(1, 1);
            if (string.IsNullOrEmpty(cat.IconBase64))
                return new Bitmap(1, 1);
            using (var memStream = new MemoryStream(Convert.FromBase64String(cat.IconBase64)))
            {
                return new Bitmap(Image.FromStream(memStream));
            }
        }
    }

}
