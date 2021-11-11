namespace TaskyJ.Interface.Windows
{
	partial class CategoryJDialog
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
			this.lblId = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.picCategory = new System.Windows.Forms.PictureBox();
			this.lblCategoryImage = new System.Windows.Forms.Label();
			this.cmdChoosePic = new System.Windows.Forms.Button();
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
			this.txtName.Location = new System.Drawing.Point(130, 49);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(303, 31);
			this.txtName.TabIndex = 1;
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
			this.lblName.Location = new System.Drawing.Point(56, 49);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(68, 25);
			this.lblName.TabIndex = 4;
			this.lblName.Text = "Name";
			// 
			// cmdOK
			// 
			this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdOK.Location = new System.Drawing.Point(65, 173);
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
			this.cmdCancel.Location = new System.Drawing.Point(237, 173);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(143, 50);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// picCategory
			// 
			this.picCategory.Location = new System.Drawing.Point(132, 91);
			this.picCategory.Name = "picCategory";
			this.picCategory.Size = new System.Drawing.Size(50, 50);
			this.picCategory.TabIndex = 20;
			this.picCategory.TabStop = false;
			// 
			// lblCategoryImage
			// 
			this.lblCategoryImage.AutoSize = true;
			this.lblCategoryImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCategoryImage.Location = new System.Drawing.Point(56, 91);
			this.lblCategoryImage.Name = "lblCategoryImage";
			this.lblCategoryImage.Size = new System.Drawing.Size(70, 25);
			this.lblCategoryImage.TabIndex = 21;
			this.lblCategoryImage.Text = "Image";
			// 
			// cmdChoosePic
			// 
			this.cmdChoosePic.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdChoosePic.Location = new System.Drawing.Point(205, 91);
			this.cmdChoosePic.Name = "cmdChoosePic";
			this.cmdChoosePic.Size = new System.Drawing.Size(120, 50);
			this.cmdChoosePic.TabIndex = 22;
			this.cmdChoosePic.Text = "Choose";
			this.cmdChoosePic.UseVisualStyleBackColor = true;
			this.cmdChoosePic.Click += new System.EventHandler(this.cmdChoosePic_Click);
			// 
			// CategoryJDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(493, 257);
			this.Controls.Add(this.cmdChoosePic);
			this.Controls.Add(this.lblCategoryImage);
			this.Controls.Add(this.picCategory);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lblId);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtId);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CategoryJDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Category Detail";
			this.Load += new System.EventHandler(this.CategoryJDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.picCategory)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblId;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.PictureBox picCategory;
		private System.Windows.Forms.Label lblCategoryImage;
		private System.Windows.Forms.Button cmdChoosePic;
	}
}