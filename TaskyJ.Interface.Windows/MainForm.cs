using System;
using System.Linq;
using System.Windows.Forms;
using TaskyJ.Business;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Interface.Windows
{
    public partial class MainForm : Form
    {
        //TODO login
        private DBSessionJ CurrentSession = null;

        public MainForm(DBSessionJ session)
        {
            InitializeComponent();
            CurrentSession = session;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReloadTasks();
            chkShowDeleted.Appearance = Appearance.Button;

            new ToolTip().SetToolTip(cmdAdd, "Add new");
            new ToolTip().SetToolTip(cmdRefresh, "Refresh");
            new ToolTip().SetToolTip(chkShowDeleted, "Show deleted");
        }

        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                var j = (DBTaskJ)lstTasks.SelectedItem;
                if (j.ID > 0)
                {
                    new TaskyJDetailForm(CurrentSession, TaskyJManager.GetTaskById(j.ID)).ShowDialog();
                    ReloadTasks();
                }
            }
        }

        private void ReloadTasks()
        {
            lstTasks.Items.Clear();
            lstTasks.Items.AddRange(TaskyJManager.RetrieveTasks(CurrentSession).ToArray());
            if (chkShowDeleted.Checked)
            {
                var split = new DBTaskJ
                {
                    ID = -1,
                    Name = "---- Deleted ----"
                };
                lstTasks.Items.Add(split);
                lstTasks.Items.AddRange(TaskyJManager.RetrieveDeletedTasks(CurrentSession).ToArray());
                //if empty remove the "split" line
                if (lstTasks.Items[lstTasks.Items.Count - 1] == split)
                {
                    lstTasks.Items.Remove(split);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            ReloadTasks();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            new TaskyJDetailForm(CurrentSession, null).ShowDialog();
            ReloadTasks();
        }

        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ReloadTasks();
        }
    }
}
