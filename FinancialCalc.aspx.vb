Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page

    Public Shared view1Data As New DataTable
    Public Shared view2data As New DataTable


#Region "Multiview"
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
            MultiView1.SetActiveView(View1)
        End Sub

        Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
            MultiView1.SetActiveView(View2)
        End Sub
#End Region


        Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

            'variables in local scope
            Dim FV As Decimal, PV As Decimal, N As Decimal, IY As Decimal, PMT As Decimal,
           totalPrincipal As Decimal, startPrincipal As Decimal

            'error checking
            If RadioButtonList1.SelectedIndex = -1 Then
                TextBox1.Text = "Select beggining or end"
                Exit Sub
            End If

            If IsNumeric(TextBox1.Text) = False Then
                TextBox1.Text = "Check value"
                Exit Sub
            End If
            If IsNumeric(TextBox2.Text) = False Then
                TextBox2.Text = "Check value"
                Exit Sub
            End If
            If IsNumeric(TextBox3.Text) = False Then
                TextBox3.Text = "Check value"
                Exit Sub
            End If
            If IsNumeric(TextBox4.Text) = False Then
                TextBox4.Text = "Check value"
                Exit Sub
            End If

            'calculations
            N = TextBox1.Text
            startPrincipal = TextBox2.Text
            IY = TextBox3.Text / 100
            PMT = TextBox4.Text

            'calculate FV
            FV = startPrincipal
            totalPrincipal = startPrincipal

            'add payment at the beggining of the period
            If RadioButtonList1.SelectedIndex = 0 Then
                For count As Integer = 1 To N
                    totalPrincipal += PMT

                    FV += PMT
                    FV = FV * (1 + IY)
                Next
            End If

            'add pmt at the end of the period
            If RadioButtonList1.SelectedIndex = 1 Then
                For count As Integer = 1 To N
                    totalPrincipal += PMT

                    FV = FV * (1 + IY)
                    FV += PMT
                Next
            End If

            'find PV
            PV = FV / (1 + IY) ^ N

        'set to datatable

        view1Data.Rows(0).Item("Results") = FormatNumber(FV, 2)
        view1Data.Rows(1).Item("Results") = FormatNumber(PV, 2)
        view1Data.Rows(2).Item("Results") = FormatNumber(N, 2)
        view1Data.Rows(3).Item("Results") = FormatNumber(IY, 2)
        view1Data.Rows(4).Item("Results") = FormatNumber(PMT, 2)
        view1Data.Rows(5).Item("Results") = FormatNumber(startPrincipal, 2)
        view1Data.Rows(6).Item("Results") = FormatNumber(totalPrincipal, 2)

        'set to gridview
        GridView1.DataSource = view1Data
        GridView1.DataBind()


        End Sub

        Private Sub _Default_Init(sender As Object, e As EventArgs) Handles Me.Init

        TextBox1.Text = "10"
        TextBox2.Text = "20000"
        TextBox3.Text = "6"
        TextBox4.Text = "1000"

        If view1Data.Columns.Count > 0 Then Exit Sub


        'data table initialization
        With view1Data
            .Columns.Add("Input", GetType(String))
            .Columns.Add("Results", GetType(Decimal))
        End With

        Dim dr1 As DataRow = view1Data.NewRow
        Dim dr2 As DataRow = view1Data.NewRow
        Dim dr3 As DataRow = view1Data.NewRow
        Dim dr4 As DataRow = view1Data.NewRow
        Dim dr5 As DataRow = view1Data.NewRow
        Dim dr6 As DataRow = view1Data.NewRow
        Dim dr7 As DataRow = view1Data.NewRow

        dr1.Item("Input") = "FV (Future Value)"
        view1Data.Rows.Add(dr1)
        dr2.Item("Input") = "PV (Present Value)"
        view1Data.Rows.Add(dr2)
        dr3.Item("Input") = "N (Number of Periods)"
        view1Data.Rows.Add(dr3)
        dr4.Item("Input") = "I/Y (Interest Rate)"
        view1Data.Rows.Add(dr4)
        dr5.Item("Input") = "PMT (Periodic Payment)"
        view1Data.Rows.Add(dr5)
        dr6.Item("Input") = "Starting Investment"
        view1Data.Rows.Add(dr6)
        dr7.Item("Input") = "Total Principal"
        view1Data.Rows.Add(dr7)

        GridView1.DataSource = view1Data
        GridView1.DataBind()

        With view2data
            .Columns.Add("Input", GetType(String))
            .Columns.Add("Results", GetType(Decimal))
        End With

        dr1 = view2data.NewRow
        dr2 = view2data.NewRow
        dr3 = view2data.NewRow
        dr4 = view2data.NewRow
        dr5 = view2data.NewRow
        dr6 = view2data.NewRow

        dr1.Item("Input") = "Monthly Pay"
        view2data.Rows.Add(dr1)
        dr2.Item("Input") = "Home Price"
        view2data.Rows.Add(dr2)
        dr3.Item("Input") = "Loan Amount"
        view2data.Rows.Add(dr3)
        dr4.Item("Input") = "Down Payment"
        view2data.Rows.Add(dr4)
        dr5.Item("Input") = "Total Mortgage Payments"
        view2data.Rows.Add(dr5)
        dr6.Item("Input") = "Total Interest"
        view2data.Rows.Add(dr6)


        GridView2.DataSource = view2data
        GridView2.DataBind()

    End Sub




    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'variables in local scope
        Dim homeValue As Decimal, loanAmount As Decimal, downPayment As Decimal, totalPayments As Decimal, totalInterest As Decimal,
            loanTerm As Decimal, interestRate As Decimal, payment As Decimal, totalMortgagePayments

        'error checking
        If IsNumeric(TextBox5.Text) = False Then
            TextBox5.Text = "Check value"
            Exit Sub
        End If
        If IsNumeric(TextBox6.Text) = False Then
            TextBox6.Text = "Check value"
            Exit Sub
        End If
        If IsNumeric(TextBox7.Text) = False Then
            TextBox7.Text = "Check value"
            Exit Sub
        End If
        If IsNumeric(TextBox8.Text) = False Then
            TextBox8.Text = "Check value"
            Exit Sub
        End If

        'calculations
        homeValue = TextBox5.Text
        downPayment = TextBox6.Text
        loanTerm = TextBox7.Text
        interestRate = TextBox8.Text

        loanAmount = homeValue - downPayment

        payment = ((interestRate / 100 / 12) * loanAmount) / (1 - ((1 + (interestRate / 100 / 12)) ^ (-loanTerm * 12)))

        totalMortgagePayments = payment * loanTerm * 12
        totalInterest = totalMortgagePayments - homeValue

        'set to datatable
        view2data.Rows(0).Item("Results") = FormatNumber(payment, 2)
        view2data.Rows(1).Item("Results") = FormatNumber(homeValue, 2)
        view2data.Rows(2).Item("Results") = FormatNumber(loanAmount, 2)
        view2data.Rows(3).Item("Results") = FormatNumber(downPayment, 2)
        view2data.Rows(4).Item("Results") = FormatNumber(totalMortgagePayments, 2)
        view2data.Rows(5).Item("Results") = FormatNumber(totalInterest, 2)

        'set to gridview
        GridView2.DataSource = view2data
        GridView2.DataBind()

    End Sub
End Class
