Imports System.Data.Common
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization

Public Class Form1
    Dim d(100)
    Dim suit(3) As String
    Dim card As New ArrayList
    Dim pn, pf, pn1 As New ArrayList
    Dim bn, bf, bn1 As New ArrayList
    Dim idx As Integer
    Dim gno As Integer
    Sub rdata()
        FileOpen(7, "C:\Users\dora0\Desktop\7.txt", OpenMode.Input)
        Input(7, gno)
        idx = 0
        Do While Not EOF(7)
            idx += 1
            Input(7, d(idx))
        Loop
        FileClose(7)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call rdata()
        Call sp1()
        Call sp2()
        Call sp3()
        Call wdata()
    End Sub
    Sub sp1()
        Dim ba1() As Byte = {226, 153, 160}
        Dim ba2() As Byte = {226, 153, 165}
        Dim ba3() As Byte = {226, 153, 166}
        Dim ba4() As Byte = {226, 153, 163}
        suit(0) = System.Text.Encoding.UTF8.GetString(ba1)
        suit(1) = System.Text.Encoding.UTF8.GetString(ba2)
        suit(2) = System.Text.Encoding.UTF8.GetString(ba3)
        suit(3) = System.Text.Encoding.UTF8.GetString(ba4)
    End Sub
    Sub sp2()
        For i = 1 To idx
            Dim t = Int(Val(d(i)) * 52)
            If card.IndexOf(t) = -1 Then
                card.Add(t)
            End If
        Next
    End Sub
    Sub sp3()
        For i = 0 To gno * 2 - 1
            Dim f = card(i) \ 13
            Dim n = card(i) Mod 13 + 1
            Dim disp = {"*", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"}
            Dim n1 = disp(n)
            If n = 1 Then n = n + 13
            If i Mod 2 = 0 Then
                pn.Add(n)
                pf.Add(f)
                pn1.Add(n1)
            Else
                bn.Add(n)
                bf.Add(f)
                bn1.Add(n1)
            End If
        Next
    End Sub
    Sub wdata()
        Dim table As New DataTable
        table.Columns.Add("序號")
        table.Columns.Add("玩家")
        table.Columns.Add("莊家")
        table.Columns.Add("結果")
        For i = 0 To gno - 1
            Dim tr As DataRow = table.NewRow
            tr(0) = i + 1
            tr(1) = suit(pf(i)) & pn1(i)
            tr(2) = suit(bf(i)) & bn1(i)
            If pn(i) > bn(i) Then tr(3) = "玩家贏"
            If pn(i) < bn(i) Then tr(3) = "莊家贏"
            If pn(i) = bn(i) Then tr(3) = "平手"
            table.Rows.Add(tr)
        Next
        dgv.DataSource = table
    End Sub
End Class
