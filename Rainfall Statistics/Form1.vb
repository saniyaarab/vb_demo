Option Strict On

Public Class Form1

    Dim strMonth() As String = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}
    Const intMAX_SUBSCRIPT As Integer = 11
    'Array to hold rainfall
    Dim decRainfall(intMAX_SUBSCRIPT) As Decimal

    Private Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click

        If lstOutput.Items.Count = 0 Then
            MessageBox.Show("Click on Input Monthly Rainfall button to enter data in order to view Satistics")
            Exit Sub
        End If

        'Other local variable
        Dim decTotal As Decimal ' to hold total 
        Dim decAverage As Decimal ' to hold the average
        Dim decMax As Decimal ' to hold to maximum rainfall
        Dim decMin As Decimal ' to hold minimum rainfall
        Dim intMaxId As Integer
        Dim strMaxMonth As String
        Dim intMinId As Integer
        Dim strMinMonth As String


        intMaxId = indexOfMaxValue(decRainfall)
        intMinId = indexOfMinValue(decRainfall)

        'Get the total rainfall
        decTotal = TotalArray(decRainfall)
        decAverage = AverageArray(decRainfall)
        decMax = decRainfall(intMaxId)
        strMaxMonth = strMonth(intMaxId)
        decMin = decRainfall(intMinId)
        strMinMonth = strMonth(intMinId)

        'display the result
        lblTotal.Text = "The total annual rainfall was " & decTotal.ToString()
        lblAvg.Text = "The average monthly rainfall was " & decAverage.ToString("n2")
        lblMax.Text = "The maximum monthly rainfall was " & decMax.ToString() & "(" & strMaxMonth & ")"
        lblMin.Text = "The minimum monthly rainfall was " & decMin.ToString() & "(" & strMinMonth & ")"

    End Sub

    Sub GetRainfallData(ByRef decRainfall() As Decimal)
        Dim intcount As Integer = 0 'loop counter set to 0

        'Fill decrainfall array from data enetered by user
        Do While intcount < decRainfall.Length
            Try
                Dim strUserInput As String = InputBox("Enter Rainfall for the Month of " & strMonth(intcount))
                If strUserInput = "" Then
                    MessageBox.Show("If no numeric value is entered then by default it will be zero for " & strMonth(intcount), "Validation Input Error", MessageBoxButtons.OK)
                    strUserInput = "0"
                End If
                If CDec(strUserInput) < 0 Then
                    MessageBox.Show("Please enter non negative numbers")
                    strUserInput = "0"
                End If
                'get rainfall for the month
                decRainfall(intcount) = CDec(strUserInput)
                intcount += 1
            Catch ex As Exception
                'display error message for invalid value
                MessageBox.Show("Enter a numeric value.")
            End Try
        Loop

    End Sub

    Function TotalArray(ByVal decValues() As Decimal) As Decimal
        Dim decTotal As Decimal = 0 ' accumulator
        Dim intCount As Integer 'loop counter

        'Calculate the total of the array's elements
        For intCount = 0 To (decValues.Length - 1)
            decTotal += decValues(intCount)
        Next
        Return decTotal

    End Function

    Function AverageArray(ByVal decValues() As Decimal) As Decimal
        Return TotalArray(decValues) / decValues.Length
    End Function

    Function indexOfMaxValue(ByRef decValues() As Decimal) As Integer
        Dim intcount As Integer ' loop counter
        Dim decMax As Decimal ' to hold max value
        Dim intMaxid As Integer
        decMax = decValues(0)

        'Search for the maximum value
        For intcount = 1 To (decValues.Length - 1)
            If decValues(intcount) > decMax Then
                decMax = decValues(intcount)
                intMaxid = intcount
            End If
        Next

        Return intMaxid
    End Function

    Function indexOfMinValue(ByRef decValues() As Decimal) As Integer
        Dim intcount As Integer ' loop counter
        Dim decMin As Decimal ' to hold min value
        Dim intMinId As Integer

        decMin = decValues(0)

        'search for min value in array
        For intcount = 1 To (decValues.Length - 1)
            If decValues(intcount) < decMin Then
                decMin = decValues(intcount)
                intMinId = intcount
            End If
        Next

        Return intMinId

    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lstOutput.Items.Clear()
        lblAvg.Text = String.Empty
        lblTotal.Text = String.Empty
        lblMin.Text = String.Empty
        lblMax.Text = String.Empty
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'clear existing list
        lstOutput.Items.Clear()

        ' To get the rainfall from user
        GetRainfallData(decRainfall)

        'Display the Rainfall statistics Header

        lstOutput.Items.Add("Monthly Rainfall Input")
        lstOutput.Items.Add("----------------------------")

        'display the array in listbox
        For intcount = 0 To intMAX_SUBSCRIPT
            lstOutput.Items.Add("Rainfall for " & strMonth(intcount) & " = " & decRainfall(intcount))
        Next

    End Sub
End Class

