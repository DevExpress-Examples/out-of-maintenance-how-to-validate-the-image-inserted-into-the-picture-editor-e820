using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace PictureValidation
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private DevExpress.XtraEditors.PictureEdit pictureEdit1;
		private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(12, 23);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(96, 96);
            this.pictureEdit1.TabIndex = 0;
            this.pictureEdit1.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.pictureEdit1_EditValueChanging);
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "textEdit1";
            this.textEdit1.Location = new System.Drawing.Point(123, 23);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(96, 20);
            this.textEdit1.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(189, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Right-click PictureEdit to load a Bitmap .";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(231, 132);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.pictureEdit1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void pictureEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e) {
			const int ImgWidth = 24;
			const int ImgHeight = 24;
			ImageFormat ImgFormat = ImageFormat.Bmp;
			const int ImgMaxSize = 20 * 1024;  // 20kb
            
			if(e.NewValue == null) return;
			
			try {
				byte[] data = (byte[])e.NewValue;
				if(data.Length > ImgMaxSize) {
					e.Cancel = true;
					return;
				}
			
				MemoryStream stream = new MemoryStream();
				stream.Write(data, 0, data.Length);
				stream.Position = 0;
				Image img = Image.FromStream(stream);
				if(img != null)
					if (img.Width != ImgWidth || img.Height != ImgHeight
						|| !img.RawFormat.Equals(ImgFormat)) {
						e.Cancel = true;
						return;
					}

                textEdit1.Text = "Loaded.";
			}
			finally {
				if(e.Cancel) {
					string msg = "The image you are trying to load cannot be accepted." +
						"\nThe image size must be {0}x{1}, less than {2} kb.  The image format must be {3}.";
					msg = String.Format(msg, ImgWidth, ImgHeight, ImgMaxSize / 1024, ImgFormat);
                    textEdit1.Text = "Loading failed.";
					MessageBox.Show(msg);
				}
			}
		}
	}
}
