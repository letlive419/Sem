Imports System.IO
'Name: Elvis Cruz
'Date: 02/19/2021
'I affirm that this program was created by Me. It Is solely my work And does Not include any work done by anyone Else.
Public Class frmListView

    Private strFileName As String
    Private intTotalRoomChanges As Integer
    Private dblTotalInvValue As Double
    Private intTotalInvCount As Integer
    Private intTotalNbrRegistered As Integer
    Private arrCategories As ArrayList
    Private stats As frmStats



    Private Const SEMINAR_ID As Integer = 0
    Private Const SUBJECT_AREA As Integer = 1
    Private Const TYPE As Integer = 2
    Private Const INSTRUCTOR As Integer = 3
    Private Const LENGTH As Integer = 4
    Private Const LOCATED As Integer = 5
    Private Const ROOM_CAPACITY As Integer = 6
    Private Const NBR_REGISTERED As Integer = 7

    Private Const COST_PER_HOUR As Integer = 8
    Private Const COST_PER_PERSON As Integer = 9
    Private Const ROOM_CHANGE As Integer = 10
    Private Const SEMINAR_TOTAL As Integer = 11

    Private Sub openFile()
        Dim intResult As Integer
        ofdOpen.InitialDirectory = Application.StartupPath
        ofdOpen.Filter = "All files (*.*)|*.*| Text files (*.txt)| *.txt"
        ofdOpen.FilterIndex = 2
        intResult = ofdOpen.ShowDialog
        If intResult = DialogResult.Cancel Then
            Exit Sub

        End If
        strFileName = ofdOpen.FileName
        Try
            ReadInputFile(strFileName)
        Catch exNotFound As FileNotFoundException
            MessageBox.Show(exNotFound.ToString)

        Catch exIOError As IOException
            MessageBox.Show(exIOError.ToString)

        Catch exOther As Exception
            MessageBox.Show(exOther.ToString)
        End Try
    End Sub
    Private Sub ReadInputFile(strIn As String)
        Dim fileIn As StreamReader
        Dim strLineIn As String
        Dim strFields() As String
        Dim i As Integer
        lvwInventory.Items.Clear()
        Try
            fileIn = New StreamReader(strIn)
            If Not fileIn.EndOfStream Then
                strLineIn = fileIn.ReadLine
                strFields = strLineIn.Split(",")
                For i = 0 To strFields.Length - 1
                    lvwInventory.columns.add(strFields(i))
                Next

                lvwInventory.Columns.Add("Cost per hour")
                lvwInventory.Columns.Add("Cost per person")
                lvwInventory.Columns.Add("Room change")
                lvwInventory.Columns.Add("Seminar total")


                With lvwInventory
                    .Columns(SEMINAR_ID).Width = 80
                    .Columns(SUBJECT_AREA).Width = 80
                    .Columns(TYPE).Width = 80
                    .Columns(INSTRUCTOR).Width = 80
                    .Columns(LENGTH).Width = 80
                    .Columns(LOCATED).Width = 80
                    .Columns(ROOM_CAPACITY).Width = 80
                    .Columns(NBR_REGISTERED).Width = 80
                    .Columns(COST_PER_HOUR).Width = 80
                    .Columns(COST_PER_PERSON).Width = 80
                    .Columns(ROOM_CHANGE).Width = 80
                    .Columns(SEMINAR_TOTAL).Width = 80
                    .Columns(SEMINAR_TOTAL).TextAlign = HorizontalAlignment.Right


                End With
            End If


            While Not fileIn.EndOfStream
                strLineIn = fileIn.ReadLine
                strFields = strLineIn.Split(",")
                Dim lviRow As New ListViewItem(strFields(0))

                For i = 1 To strFields.Length - 1
                    Dim lsiCol As New ListViewItem.ListViewSubItem

                    lsiCol.Text = strFields(i)

                    lviRow.SubItems.Add(lsiCol)
                Next

                Dim iCol As New ListViewItem.ListViewSubItem
                Select Case strFields(1)

                    Case "Marketing"
                        iCol.Text = FormatCurrency(30, 2)
                    Case "Accounting"
                        iCol.Text = FormatCurrency(35, 2)
                    Case "Information Systems"
                        iCol.Text = FormatCurrency(45, 2)
                    Case "Management"
                        iCol.Text = FormatCurrency(50, 2)
                    Case "Finance"
                        iCol.Text = FormatCurrency(55, 2)

                End Select
                FormatCurrency(iCol.Text, 2)

                Dim jCol As New ListViewItem.ListViewSubItem
                jCol.Text = FormatCurrency((strFields(4) * iCol.Text), 2)

                Dim kCol As New ListViewItem.ListViewSubItem
                If Convert.ToInt32(strFields(7)) > Convert.ToInt32(strFields(6)) Then
                    kCol.Text = "YES"


                Else
                    kCol.Text = "NO"
                End If

                Dim lCol As New ListViewItem.ListViewSubItem
                lCol.Text = FormatCurrency((Convert.ToInt32(strFields(7)) * jCol.Text), 2)




                lviRow.SubItems.Add(iCol)
                lviRow.SubItems.Add(jCol)
                lviRow.SubItems.Add(kCol)
                lviRow.SubItems.Add(lCol)
                lvwInventory.Items.Add(lviRow)

                UpdateStatistics(lviRow)
            End While

            fileIn.Close()
            fileIn.Dispose()




        Catch ex As Exception
            MessageBox.Show("ReadInputFile: " & ex.ToString)
        End Try
    End Sub

    Private Sub UpdateStatistics(aRow As ListViewItem)
        Dim blnFoundIt As Boolean

        For Each aCat As CCategory In arrCategories
            If aCat.CatName = aRow.SubItems(SUBJECT_AREA).Text Then
                aCat.TotalValue += aRow.SubItems(SEMINAR_TOTAL).Text
                aCat.TotalCount += 1
                aCat.TotalNbrCount += aRow.SubItems(NBR_REGISTERED).Text
                aCat.strRoom = aRow.SubItems(ROOM_CHANGE).Text
                If aCat.strRoom = "YES" Then
                    aCat.TotalRoomCount += 1


                End If


                blnFoundIt = True

                Exit For
            End If

        Next
        If Not blnFoundIt Then
            Dim newCat As New CCategory(aRow.SubItems(SUBJECT_AREA).Text, CDbl(aRow.SubItems(SEMINAR_TOTAL).Text), aRow.SubItems(NBR_REGISTERED).Text, aRow.SubItems(ROOM_CHANGE).Text)

            arrCategories.Add(newCat)


        End If



        intTotalNbrRegistered += aRow.SubItems(NBR_REGISTERED).Text
        dblTotalInvValue += aRow.SubItems(SEMINAR_TOTAL).Text
        intTotalInvCount += 1
        If aRow.SubItems(ROOM_CHANGE).Text = "YES" Then
            intTotalRoomChanges += 1


        End If



    End Sub

    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        openFile()
    End Sub


    Private Sub btnStats_Click(sender As Object, e As EventArgs) Handles btnStats.Click
        stats.lstStats.Items.Clear()
        With stats.lstStats
            .Items.Add("Total Revenue = " & FormatCurrency(dblTotalInvValue))
            .Items.Add("Total Nbr Registered = " & CStr(intTotalNbrRegistered))
            .Items.Add("Total Nbr Seminars " & CStr(intTotalInvCount))
            .Items.Add("Total Room Changes " & CStr(intTotalRoomChanges))
            For Each aCat As CCategory In arrCategories
                .Items.Add(aCat.CatName & ":")
                .Items.Add("  Total Revenue = " & FormatCurrency(aCat.TotalValue))
                .Items.Add("  # Of Sessions = " & CStr(aCat.TotalCount))
                .Items.Add("# Registered = " & CStr(aCat.TotalNbrCount))
                .Items.Add("# Room Changes = " & CStr(aCat.TotalRoomCount))

            Next

        End With
        stats.ShowDialog()
    End Sub

    Private Sub frmListView_Load(sender As Object, e As EventArgs) Handles Me.Load
        arrCategories = New ArrayList
        stats = New frmStats
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lvwInventory.Items.Clear()
        dblTotalInvValue = 0
        intTotalInvCount = 0
        intTotalRoomChanges = 0
        intTotalNbrRegistered = 0
        arrCategories = New ArrayList
    End Sub

End Class
