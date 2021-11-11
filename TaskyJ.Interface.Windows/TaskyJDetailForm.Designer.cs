namespace TaskyJ.Interface.Windows
{
	partial class TaskyJDetailForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtId = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblId = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.lblDescription = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdUndo = new System.Windows.Forms.Button();
			this.lblCreated = new System.Windows.Forms.Label();
			this.txtCreated = new System.Windows.Forms.TextBox();
			this.lblComplete = new System.Windows.Forms.Label();
			this.cboComplete = new System.Windows.Forms.ComboBox();
			this.chkFinished = new System.Windows.Forms.CheckBox();
			this.lblFinished = new System.Windows.Forms.Label();
			this.txtFinished = new System.Windows.Forms.TextBox();
			this.cboPriority = new System.Windows.Forms.ComboBox();
			this.lblPriority = new System.Windows.Forms.Label();
			this.picCategory = new System.Windows.Forms.PictureBox();
			this.cboCategories = new System.Windows.Forms.ComboBox();
			this.lblCategory = new System.Windows.Forms.Label();
			this.dpkDeadlineDate = new System.Windows.Forms.DateTimePicker();
			this.lblDeadline = new System.Windows.Forms.Label();
			this.dpkDeadlineTime = new System.Windows.Forms.DateTimePicker();
			this.cmdClearDeadline = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picCategory)).BeginInit();
			this.SuspendLayout();
			// 
			// txtId
			// 
			this.txtId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtId.Location = new System.Drawing.Point(130, 12);
			this.txtId.Name = "txtId";
			this.txtId.ReadOnly = true;
			this.txtId.Size = new System.Drawing.Size(42, 31);
			this.txtId.TabIndex = 0;
			// 
			// txtName
			// 
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtName.Location = new System.Drawing.Point(130, 105);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(303, 31);
			this.txtName.TabIndex = 1;
			// 
			// txtDescription
			// 
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDescription.Location = new System.Drawing.Point(130, 146);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(303, 125);
			this.txtDescription.TabIndex = 2;
			// 
			// lblId
			// 
			this.lblId.AutoSize = true;
			this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblId.Location = new System.Drawing.Point(95, 15);
			this.lblId.Name = "lblId";
			this.lblId.Size = new System.Drawing.Size(29, 25);
			this.lblId.TabIndex = 3;
			this.lblId.Text = "Id";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(56, 105);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(68, 25);
			this.lblName.TabIndex = 4;
			this.lblName.Text = "Name";
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDescription.Location = new System.Drawing.Point(4, 146);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(120, 25);
			this.lblDescription.TabIndex = 5;
			this.lblDescription.Text = "Description";
			// 
			// cmdOK
			// 
			this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdOK.Location = new System.Drawing.Point(130, 295);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(120, 50);
			this.cmdOK.TabIndex = 6;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdCancel.Location = new System.Drawing.Point(290, 295);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(143, 50);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdDelete.Location = new System.Drawing.Point(625, 295);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(133, 50);
			this.cmdDelete.TabIndex = 9;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// cmdUndo
			// 
			this.cmdUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdUndo.Location = new System.Drawing.Point(469, 295);
			this.cmdUndo.Name = "cmdUndo";
			this.cmdUndo.Size = new System.Drawing.Size(120, 50);
			this.cmdUndo.TabIndex = 8;
			this.cmdUndo.Text = "Undo";
			this.cmdUndo.UseVisualStyleBackColor = true;
			this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
			// 
			// lblCreated
			// 
			this.lblCreated.AutoSize = true;
			this.lblCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCreated.Location = new System.Drawing.Point(492, 15);
			this.lblCreated.Name = "lblCreated";
			this.lblCreated.Size = new System.Drawing.Size(88, 25);
			this.lblCreated.TabIndex = 11;
			this.lblCreated.Text = "Created";
			// 
			// txtCreated
			// 
			this.txtCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCreated.Location = new System.Drawing.Point(586, 15);
			this.txtCreated.Name = "txtCreated";
			this.txtCreated.ReadOnly = true;
			this.txtCreated.Size = new System.Drawing.Size(240, 31);
			this.txtCreated.TabIndex = 10;
			// 
			// lblComplete
			// 
			this.lblComplete.AutoSize = true;
			this.lblComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblComplete.Location = new System.Drawing.Point(452, 98);
			this.lblComplete.Name = "lblComplete";
			this.lblComplete.Size = new System.Drawing.Size(128, 25);
			this.lblComplete.TabIndex = 13;
			this.lblComplete.Text = "% Complete";
			// 
			// cboComplete
			// 
			this.cboComplete.DropDownWidth = 121;
			this.cboComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboComplete.FormattingEnabled = true;
			this.cboComplete.Location = new System.Drawing.Point(586, 92);
			this.cboComplete.Name = "cboComplete";
			this.cboComplete.Size = new System.Drawing.Size(121, 33);
			this.cboComplete.TabIndex = 14;
			this.cboComplete.SelectedValueChanged += new System.EventHandler(this.cboComplete_SelectedValueChanged);
			// 
			// chkFinished
			// 
			this.chkFinished.AutoSize = true;
			this.chkFinished.Checked = true;
			this.chkFinished.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFinished.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkFinished.Location = new System.Drawing.Point(724, 95);
			this.chkFinished.Name = "chkFinished";
			this.chkFinished.Size = new System.Drawing.Size(102, 28);
			this.chkFinished.TabIndex = 15;
			this.chkFinished.Text = "Finished";
			this.chkFinished.UseVisualStyleBackColor = true;
			this.chkFinished.CheckedChanged += new System.EventHandler(this.chkFinished_CheckedChanged);
			// 
			// lblFinished
			// 
			this.lblFinished.AutoSize = true;
			this.lblFinished.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFinished.Location = new System.Drawing.Point(486, 185);
			this.lblFinished.Name = "lblFinished";
			this.lblFinished.Size = new System.Drawing.Size(94, 25);
			this.lblFinished.TabIndex = 17;
			this.lblFinished.Text = "Finished";
			this.lblFinished.Visible = false;
			// 
			// txtFinished
			// 
			this.txtFinished.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFinished.Location = new System.Drawing.Point(586, 182);
			this.txtFinished.Name = "txtFinished";
			this.txtFinished.ReadOnly = true;
			this.txtFinished.Size = new System.Drawing.Size(240, 31);
			this.txtFinished.TabIndex = 16;
			this.txtFinished.Visible = false;
			// 
			// cboPriority
			// 
			this.cboPriority.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cboPriority.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPriority.DropDownWidth = 121;
			this.cboPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboPriority.FormattingEnabled = true;
			this.cboPriority.Location = new System.Drawing.Point(586, 137);
			this.cboPriority.Name = "cboPriority";
			this.cboPriority.Size = new System.Drawing.Size(240, 33);
			this.cboPriority.TabIndex = 19;
			// 
			// lblPriority
			// 
			this.lblPriority.AutoSize = true;
			this.lblPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPriority.Location = new System.Drawing.Point(501, 141);
			this.lblPriority.Name = "lblPriority";
			this.lblPriority.Size = new System.Drawing.Size(79, 25);
			this.lblPriority.TabIndex = 18;
			this.lblPriority.Text = "Priority";
			// 
			// picCategory
			// 
			this.picCategory.Location = new System.Drawing.Point(190, 6);
			this.picCategory.Name = "picCategory";
			this.picCategory.Size = new System.Drawing.Size(50, 50);
			this.picCategory.TabIndex = 20;
			this.picCategory.TabStop = false;
			// 
			// cboCategories
			// 
			this.cboCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.cboCategories.DropDownHeight = 400;
			this.cboCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboCategories.FormattingEnabled = true;
			this.cboCategories.IntegralHeight = false;
			this.cboCategories.Location = new System.Drawing.Point(130, 60);
			this.cboCategories.Name = "cboCategories";
			this.cboCategories.Size = new System.Drawing.Size(321, 32);
			this.cboCategories.TabIndex = 21;
			this.cboCategories.SelectedIndexChanged += new System.EventHandler(this.cboCategories_SelectedIndexChanged);
			// 
			// lblCategory
			// 
			this.lblCategory.AutoSize = true;
			this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCategory.Location = new System.Drawing.Point(25, 65);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(99, 25);
			this.lblCategory.TabIndex = 22;
			this.lblCategory.Text = "Category";
			// 
			// dpkDeadlineDate
			// 
			this.dpkDeadlineDate.CustomFormat = "dd/MM/yyyy";
			this.dpkDeadlineDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dpkDeadlineDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dpkDeadlineDate.Location = new System.Drawing.Point(586, 56);
			this.dpkDeadlineDate.Name = "dpkDeadlineDate";
			this.dpkDeadlineDate.Size = new System.Drawing.Size(121, 29);
			this.dpkDeadlineDate.TabIndex = 23;
			this.dpkDeadlineDate.ValueChanged += new System.EventHandler(this.dpkDeadlineDate_ValueChanged);
			// 
			// lblDeadline
			// 
			this.lblDeadline.AutoSize = true;
			this.lblDeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDeadline.Location = new System.Drawing.Point(483, 56);
			this.lblDeadline.Name = "lblDeadline";
			this.lblDeadline.Size = new System.Drawing.Size(97, 25);
			this.lblDeadline.TabIndex = 24;
			this.lblDeadline.Text = "Deadline";
			// 
			// dpkDeadlineTime
			// 
			this.dpkDeadlineTime.CustomFormat = "hh:mm:ss";
			this.dpkDeadlineTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dpkDeadlineTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dpkDeadlineTime.Location = new System.Drawing.Point(713, 56);
			this.dpkDeadlineTime.Name = "dpkDeadlineTime";
			this.dpkDeadlineTime.ShowUpDown = true;
			this.dpkDeadlineTime.Size = new System.Drawing.Size(98, 29);
			this.dpkDeadlineTime.TabIndex = 25;
			// 
			// cmdClearDeadline
			// 
			this.cmdClearDeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdClearDeadline.Location = new System.Drawing.Point(817, 56);
			this.cmdClearDeadline.Name = "cmdClearDeadline";
			this.cmdClearDeadline.Size = new System.Drawing.Size(33, 34);
			this.cmdClearDeadline.TabIndex = 26;
			this.cmdClearDeadline.Text = "X";
			this.cmdClearDeadline.UseVisualStyleBackColor = true;
			this.cmdClearDeadline.Click += new System.EventHandler(this.cmdClearDeadline_Click);
			// 
			// TaskyJDetailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(868, 363);
			this.Controls.Add(this.cmdClearDeadline);
			this.Controls.Add(this.dpkDeadlineTime);
			this.Controls.Add(this.lblDeadline);
			this.Controls.Add(this.dpkDeadlineDate);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.cboCategories);
			this.Controls.Add(this.picCategory);
			this.Controls.Add(this.cboPriority);
			this.Controls.Add(this.lblPriority);
			this.Controls.Add(this.lblFinished);
			this.Controls.Add(this.txtFinished);
			this.Controls.Add(this.chkFinished);
			this.Controls.Add(this.cboComplete);
			this.Controls.Add(this.lblComplete);
			this.Controls.Add(this.lblCreated);
			this.Controls.Add(this.txtCreated);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.cmdUndo);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lblId);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtId);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TaskyJDetailForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Task Detail";
			this.Load += new System.EventHandler(this.TaskyJDetailForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.picCategory)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblId;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdDelete;
		private System.Windows.Forms.Button cmdUndo;
		private System.Windows.Forms.Label lblCreated;
		private System.Windows.Forms.TextBox txtCreated;
		private System.Windows.Forms.Label lblComplete;
		private System.Windows.Forms.ComboBox cboComplete;
		private System.Windows.Forms.CheckBox chkFinished;
		private System.Windows.Forms.Label lblFinished;
		private System.Windows.Forms.TextBox txtFinished;
		private System.Windows.Forms.ComboBox cboPriority;
		private System.Windows.Forms.Label lblPriority;
		private System.Windows.Forms.PictureBox picCategory;
		private System.Windows.Forms.ComboBox cboCategories;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.DateTimePicker dpkDeadlineDate;
		private System.Windows.Forms.Label lblDeadline;
		private System.Windows.Forms.DateTimePicker dpkDeadlineTime;
		private System.Windows.Forms.Button cmdClearDeadline;
	}
}