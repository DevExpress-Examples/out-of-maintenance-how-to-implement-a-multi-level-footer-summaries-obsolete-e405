Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Collections
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data

Namespace Q33608
	Partial Public Class Form1
		Inherits Form
		Private Const TotalSummaryLevelCount As Integer = 2

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'nwindDataSet.Order_Details' table. You can move, or remove it, as needed.
			Me.order_DetailsTableAdapter.Fill(Me.nwindDataSet.Order_Details)

		End Sub

		Private Sub gridView1_CustomDrawFooter(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles gridView1.CustomDrawFooter
			Dim location As New Point(e.Bounds.X, e.Bounds.Y - e.Bounds.Height * (TotalSummaryLevelCount - 1))
			Dim size As New Size(e.Bounds.Width, e.Bounds.Height * TotalSummaryLevelCount)
			e.Info.Bounds = New Rectangle(location, size)
		End Sub

		Private Sub gridView1_TopRowChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.TopRowChanged
			Dim view As GridView = CType(sender, GridView)
			view.InvalidateRows()
			view.InvalidateFooter()
		End Sub

		Private value1 As New TotalSummaryValue()
		Private value2 As New TotalSummaryValue()
		Private counter As Integer

		Private Sub gridView1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As CustomSummaryEventArgs) Handles gridView1.CustomSummaryCalculate
			If e.IsTotalSummary Then
				If e.SummaryProcess = CustomSummaryProcess.Start Then
					If (CType(e.Item, GridColumnSummaryItem)).FieldName = "Quantity" Then
						value1.Level1 = 0
						value1.Level2 = 0
					Else
						value2.Level1 = 0
						value2.Level2 = 0
					End If
					counter = 0
				End If
				If e.SummaryProcess = CustomSummaryProcess.Calculate Then
					If (CType(e.Item, GridColumnSummaryItem)).FieldName = "Quantity" Then
						value1.Level1 = CInt(Fix(value1.Level1)) + CShort(Fix(e.FieldValue))
						value1.Level2 = CInt(Fix(value1.Level2)) + CShort(Fix(e.FieldValue))
					Else
						value2.Level1 = Convert.ToDecimal(value2.Level1) + CDec(e.FieldValue)
						value2.Level2 = Convert.ToDecimal(value2.Level2) + CDec(e.FieldValue)
					End If
					counter += 1
				End If
				If e.SummaryProcess = CustomSummaryProcess.Finalize Then
					If (CType(e.Item, GridColumnSummaryItem)).FieldName = "Quantity" Then
						value1.Level2 = CInt(Fix(value1.Level2)) / counter
						e.TotalValue = value1
					Else
						value2.Level2 = Math.Round(CDec(value2.Level2) / counter)
						e.TotalValue = value2
					End If
				End If
			End If
		End Sub

		Private Sub gridView1_CustomDrawFooterCell(ByVal sender As Object, ByVal e As FooterCellCustomDrawEventArgs) Handles gridView1.CustomDrawFooterCell
			Dim value As TotalSummaryValue = CType(e.Info.Value, TotalSummaryValue)
			Dim location As New Point(e.Bounds.X, e.Bounds.Y - e.Bounds.Height - 1)
			Dim secondLevelBounds As New Rectangle(location, e.Bounds.Size)
			Dim temp As Rectangle = e.Info.Bounds
			e.Info.Bounds = secondLevelBounds
			e.Info.DisplayText = String.Format("Aver: {0}", value.Level2)
			e.Painter.DrawObject(e.Info)
			e.Info.Bounds = temp
			e.Info.DisplayText = String.Format("Total: {0}", value.Level1)
		End Sub
	End Class

	Public Class TotalSummaryValue
		Private level1value As Object
		Private level2value As Object

		Public Property Level1() As Object
			Get
				Return level1value
			End Get
			Set(ByVal value As Object)
				level1value = value
			End Set
		End Property

		Public Property Level2() As Object
			Get
				Return level2value
			End Get
			Set(ByVal value As Object)
				level2value = value
			End Set
		End Property
	End Class
End Namespace