<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Q33608/Form1.cs) (VB: [Form1.vb](./VB/Q33608/Form1.vb))
* [Program.cs](./CS/Q33608/Program.cs) (VB: [Program.vb](./VB/Q33608/Program.vb))
<!-- default file list end -->
# How to implement a multi-level footer summaries (Obsolete)
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e405)**
<!-- run online end -->


<p><strong>Starting from version 2011 vol 1, this feature is natively supported by the GridView. See the <a href="https://www.devexpress.com/Support/Center/p/AS2570">Ability to show several footer summary items for the same column</a> <u>suggestion for details.</u></strong><br /><br />This example demonstrates how to display more than one summary item under the same column. To implement this you need to handle the CustomDrawFooter and CustomDrawFooterCell events to manually draw footers. Also, you need to manually calculate additional summary within the CustomSummaryCalculate event. Please note that you need to call the GridView.InvalidateRows and the GridView.InvalidateFooter methods to force the GridView to properly repaint itself.</p>
<p><br /><u></u></p>

<br/>


