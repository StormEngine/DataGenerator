<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataGeneratorWebForm.aspx.cs" Inherits="DataGeneratorWebApp.DataGeneratorWebForm" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblInterval" runat="server" Text="Interval in Ticks:  "></asp:Label>
        <asp:DropDownList ID="drpDwnLstInterval" runat="server" AutoPostBack="True" 
            DataSourceID="IntervalSqlDataSource1" 
            DataTextField="IntervalAtWhichCosineIsTaken" 
            DataValueField="IntervalAtWhichCosineIsTaken" Height="25px" Width="130px" 
            onselectedindexchanged="drpDwnLstInterval_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="IntervalSqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:tempdbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [IntervalAtWhichCosineIsTaken] FROM [CosineTest2]">
        </asp:SqlDataSource>
    
    </div>
    <asp:Chart ID="CosineChart1" runat="server" 
        DataSourceID="CosineChartSqlDataSource1" Height="500px" Width="800px">
        <series>
            <asp:Series ChartType="Point" Name="Series1" 
                XValueMember="TimeOfCosineOfCurrentAngle" XValueType="Time" 
                YValueMembers="CosineOfCurrentAngle" YValueType="Double">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
    <asp:SqlDataSource ID="CosineChartSqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:tempdbConnectionString %>" 
        SelectCommand="SELECT [CosineOfCurrentAngle], [TimeOfCosineOfCurrentAngle] FROM [CosineTest2] WHERE ([IntervalAtWhichCosineIsTaken] = @IntervalAtWhichCosineIsTaken) ORDER BY [TimeOfCosineOfCurrentAngle]">
        <SelectParameters>
            <asp:Parameter DefaultValue="10000000" Name="IntervalAtWhichCosineIsTaken" 
                Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
     <div style="width: 100%; height: 400px; overflow: scroll">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Caption="SUMMARY" CaptionAlign="Top" CellPadding="4" 
        DataSourceID="CosineChartSqlDataSource1" ForeColor="#333333" GridLines="None" 
             Width="517px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="TimeOfCosineOfCurrentAngle" 
                DataFormatString="{0:MM/dd/yyyy hh:mm:ss.ffffff}" 
                HeaderText="TimeOfCosineOfCurrentAngle" 
                SortExpression="TimeOfCosineOfCurrentAngle">
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
            </asp:BoundField>
            <asp:BoundField DataField="CosineOfCurrentAngle" 
                HeaderText="CosineOfCurrentAngle" SortExpression="CosineOfCurrentAngle">
            <FooterStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
            </asp:BoundField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    </div>
    </form>
</body>
</html>
