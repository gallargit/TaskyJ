using System;
using System.Windows.Forms;

namespace TaskyJ.Interface.Windows
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            var result = Business.TaskyJManager.Authenticate(txtUsername.Text, txtPassword.Text, "0.0.0.0");
            if (result == null)
                MessageBox.Show("Wrong login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                var f = new MainForm(result);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
                Hide();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
