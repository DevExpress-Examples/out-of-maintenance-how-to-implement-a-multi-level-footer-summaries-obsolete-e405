using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;

namespace Q33608
{
    public partial class Form1 : Form
    {
        private const int TotalSummaryLevelCount = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nwindDataSet.Order_Details' table. You can move, or remove it, as needed.
            this.order_DetailsTableAdapter.Fill(this.nwindDataSet.Order_Details);

        }

        private void gridView1_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            Point location = new Point(e.Bounds.X, e.Bounds.Y - e.Bounds.Height * (TotalSummaryLevelCount - 1));
            Size size = new Size(e.Bounds.Width, e.Bounds.Height * TotalSummaryLevelCount);
            e.Info.Bounds = new Rectangle(location, size);
        }

        private void gridView1_TopRowChanged(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            view.InvalidateRows();
            view.InvalidateFooter();
        }

        TotalSummaryValue value1 = new TotalSummaryValue();
        TotalSummaryValue value2 = new TotalSummaryValue();
        int counter;

        private void gridView1_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    if (((GridColumnSummaryItem)e.Item).FieldName == "Quantity")
                    {
                        value1.Level1 = 0;
                        value1.Level2 = 0;
                    }
                    else
                    {
                        value2.Level1 = 0;
                        value2.Level2 = 0;
                    }
                    counter = 0;
                }
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (((GridColumnSummaryItem)e.Item).FieldName == "Quantity")
                    {
                        value1.Level1 = (int)value1.Level1 + (Int16)e.FieldValue;
                        value1.Level2 = (int)value1.Level2 + (Int16)e.FieldValue;
                    }
                    else
                    {
                        value2.Level1 = Convert.ToDecimal(value2.Level1) + (decimal)e.FieldValue;
                        value2.Level2 = Convert.ToDecimal(value2.Level2) + (decimal)e.FieldValue;
                    }
                    counter++;
                }
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridColumnSummaryItem)e.Item).FieldName == "Quantity")
                    {
                        value1.Level2 = (int)value1.Level2 / counter;
                        e.TotalValue = value1;
                    }
                    else
                    {
                        value2.Level2 = Math.Round((decimal)value2.Level2 / counter);
                        e.TotalValue = value2;
                    }
                }
            }
        }

        private void gridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            TotalSummaryValue value = (TotalSummaryValue)e.Info.Value;
            Point location = new Point(e.Bounds.X, e.Bounds.Y - e.Bounds.Height - 1);
            Rectangle secondLevelBounds = new Rectangle(location, e.Bounds.Size);
            Rectangle temp = e.Info.Bounds;
            e.Info.Bounds = secondLevelBounds;
            e.Info.DisplayText = string.Format("Aver: {0}", value.Level2);
            e.Painter.DrawObject(e.Info);
            e.Info.Bounds = temp;
            e.Info.DisplayText = string.Format("Total: {0}", value.Level1);
        }
    }

    public class TotalSummaryValue
    {
        private object level1value;
        private object level2value;

        public object Level1
        {
            get { return level1value; }
            set { level1value = value; }
        }

        public object Level2
        {
            get { return level2value; }
            set { level2value = value; }
        }
    }
}