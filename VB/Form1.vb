Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace PictureValidation
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private WithEvents pictureEdit1 As DevExpress.XtraEditors.PictureEdit
		Private textEdit1 As DevExpress.XtraEditors.TextEdit
		Private labelControl1 As DevExpress.XtraEditors.LabelControl
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.pictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
			Me.textEdit1 = New DevExpress.XtraEditors.TextEdit()
			Me.labelControl1 = New DevExpress.XtraEditors.LabelControl()
			CType(Me.pictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.textEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' pictureEdit1
			' 
			Me.pictureEdit1.Location = New System.Drawing.Point(12, 23)
			Me.pictureEdit1.Name = "pictureEdit1"
			Me.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
			Me.pictureEdit1.Size = New System.Drawing.Size(96, 96)
			Me.pictureEdit1.TabIndex = 0
'			Me.pictureEdit1.EditValueChanging += New DevExpress.XtraEditors.Controls.ChangingEventHandler(Me.pictureEdit1_EditValueChanging);
			' 
			' textEdit1
			' 
			Me.textEdit1.EditValue = "textEdit1"
			Me.textEdit1.Location = New System.Drawing.Point(123, 23)
			Me.textEdit1.Name = "textEdit1"
			Me.textEdit1.Size = New System.Drawing.Size(96, 20)
			Me.textEdit1.TabIndex = 1
			' 
			' labelControl1
			' 
			Me.labelControl1.Location = New System.Drawing.Point(13, 4)
			Me.labelControl1.Name = "labelControl1"
			Me.labelControl1.Size = New System.Drawing.Size(189, 13)
			Me.labelControl1.TabIndex = 2
			Me.labelControl1.Text = "Right-click PictureEdit to load a Bitmap ."
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(231, 132)
			Me.Controls.Add(Me.labelControl1)
			Me.Controls.Add(Me.textEdit1)
			Me.Controls.Add(Me.pictureEdit1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.pictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.textEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Sub pictureEdit1_EditValueChanging(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles pictureEdit1.EditValueChanging
			Const ImgWidth As Integer = 24
			Const ImgHeight As Integer = 24
			Dim ImgFormat As ImageFormat = ImageFormat.Bmp
			Const ImgMaxSize As Integer = 20 * 1024 ' 20kb

			If e.NewValue Is Nothing Then
				Return
			End If

			Try
				Dim data() As Byte = CType(e.NewValue, Byte())
				If data.Length > ImgMaxSize Then
					e.Cancel = True
					Return
				End If

				Dim stream As New MemoryStream()
				stream.Write(data, 0, data.Length)
				stream.Position = 0
				Dim img As Image = Image.FromStream(stream)
				If img IsNot Nothing Then
					If img.Width <> ImgWidth OrElse img.Height <> ImgHeight OrElse (Not img.RawFormat.Equals(ImgFormat)) Then
						e.Cancel = True
						Return
					End If
				End If

				textEdit1.Text = "Loaded."
			Finally
				If e.Cancel Then
					Dim msg As String = "The image you are trying to load cannot be accepted." & Constants.vbLf & "The image size must be {0}x{1}, less than {2} kb.  The image format must be {3}."
					msg = String.Format(msg, ImgWidth, ImgHeight, ImgMaxSize \ 1024, ImgFormat)
					textEdit1.Text = "Loading failed."
					MessageBox.Show(msg)
				End If
			End Try
		End Sub
	End Class
End Namespace
