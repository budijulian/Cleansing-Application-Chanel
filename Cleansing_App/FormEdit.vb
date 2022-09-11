Public Class FormEdit
    Public indexRows As Integer
    Private Sub changeData()
        Dim fm As Form1 = CType(Me.Owner, Form1)

        If fm.tabcontrol1.SelectedIndex.ToString = 0 Then
        ElseIf fm.tabcontrol1.SelectedIndex.ToString = 1 Then
            'table success
            'multifinance code
            fm.TableFinal.Rows(indexRows).Item(79) = txt_multifinance_customer.Text
            'data nasabah
            fm.TableFinal.Rows(indexRows).Item(5) = txt_name.Text
            fm.TableFinal.Rows(indexRows).Item(6) = txt_name.Text
            fm.TableFinal.Rows(indexRows).Item(7) = txt_name.Text
            fm.TableFinal.Rows(indexRows).Item(14) = txt_id_card.Text
            fm.TableFinal.Rows(indexRows).Item(23) = txt_date_birth.Text
            fm.TableFinal.Rows(indexRows).Item(28) = txt_gender.Text
            fm.TableFinal.Rows(indexRows).Item(33) = txt_mother_name.Text
            fm.TableFinal.Rows(indexRows).Item(35) = txt_place_birth.Text
            fm.TableFinal.Rows(indexRows).Item(39) = txt_occupation.Text
            fm.TableFinal.Rows(indexRows).Item(74) = txt_source_of_fund.Text
            'data wilayah home
            fm.TableFinal.Rows(indexRows).Item(47) = txt_dati_home.Text
            fm.TableFinal.Rows(indexRows).Item(51) = txt_post_code_home.Text
            fm.TableFinal.Rows(indexRows).Item(52) = txt_village_home.Text
            fm.TableFinal.Rows(indexRows).Item(53) = txt_sub_district_home.Text
            'data wilayah terkini 
            fm.TableFinal.Rows(indexRows).Item(65) = txt_dati_home.Text
            fm.TableFinal.Rows(indexRows).Item(69) = txt_post_code_home.Text
            fm.TableFinal.Rows(indexRows).Item(70) = txt_village_home.Text
            fm.TableFinal.Rows(indexRows).Item(71) = txt_sub_district_home.Text
            'data wilaya office
            fm.TableFinal.Rows(indexRows).Item(56) = txt_dati_office.Text
            fm.TableFinal.Rows(indexRows).Item(60) = txt_post_code_office.Text
            fm.TableFinal.Rows(indexRows).Item(61) = txt_village_office.Text
            fm.TableFinal.Rows(indexRows).Item(62) = txt_sub_district_office.Text

            fm.TableFinal.AcceptChanges()
            fm.load_cif_success.DataSource = fm.TableFinal

        ElseIf fm.tabcontrol1.SelectedIndex.ToString = 2 Then
            'table takedown
            'table success
            'multifinance code
            fm.TableTakeDown.Rows(indexRows).Item(79) = txt_multifinance_customer.Text
            'data nasabah
            fm.TableTakeDown.Rows(indexRows).Item(5) = txt_name.Text
            fm.TableTakeDown.Rows(indexRows).Item(6) = txt_name.Text
            fm.TableTakeDown.Rows(indexRows).Item(7) = txt_name.Text
            fm.TableTakeDown.Rows(indexRows).Item(14) = txt_id_card.Text
            fm.TableTakeDown.Rows(indexRows).Item(23) = txt_date_birth.Text
            fm.TableTakeDown.Rows(indexRows).Item(28) = txt_gender.Text
            fm.TableTakeDown.Rows(indexRows).Item(33) = txt_mother_name.Text
            fm.TableTakeDown.Rows(indexRows).Item(35) = txt_place_birth.Text
            fm.TableTakeDown.Rows(indexRows).Item(39) = txt_occupation.Text
            fm.TableTakeDown.Rows(indexRows).Item(74) = txt_source_of_fund.Text
            'data wilayah home
            fm.TableTakeDown.Rows(indexRows).Item(47) = txt_dati_home.Text
            fm.TableTakeDown.Rows(indexRows).Item(51) = txt_post_code_home.Text
            fm.TableTakeDown.Rows(indexRows).Item(52) = txt_village_home.Text
            fm.TableTakeDown.Rows(indexRows).Item(53) = txt_sub_district_home.Text
            'data wilayah terkini 
            fm.TableTakeDown.Rows(indexRows).Item(65) = txt_dati_home.Text
            fm.TableTakeDown.Rows(indexRows).Item(69) = txt_post_code_home.Text
            fm.TableTakeDown.Rows(indexRows).Item(70) = txt_village_home.Text
            fm.TableTakeDown.Rows(indexRows).Item(71) = txt_sub_district_home.Text
            'data wilaya office
            fm.TableTakeDown.Rows(indexRows).Item(56) = txt_dati_office.Text
            fm.TableTakeDown.Rows(indexRows).Item(60) = txt_post_code_office.Text
            fm.TableTakeDown.Rows(indexRows).Item(61) = txt_village_office.Text
            fm.TableTakeDown.Rows(indexRows).Item(62) = txt_sub_district_office.Text

            fm.TableTakeDown.AcceptChanges()
            fm.load_cif_failed.DataSource = fm.TableTakeDown
        End If
    End Sub
    Private Sub btn_success_Click(sender As Object, e As EventArgs) Handles btn_success.Click
        Try
            'update data
            changeData()
            'move data other datatable
            Dim fm As Form1 = CType(Me.Owner, Form1)
            If fm.tabcontrol1.SelectedIndex.ToString = 0 Then
            ElseIf fm.tabcontrol1.SelectedIndex.ToString = 1 Then
                'table success
                'copy column to datatable
                If fm.TableFinal.Rows.Count <= 0 Then
                    'last column not include from TableTemp
                    For j = 0 To fm.TableTemp.Columns.Count - 1
                        fm.TableFinal.Columns.Add(fm.TableTemp.Columns(j).ColumnName)
                    Next
                End If
                fm.TableTakeDown.ImportRow(fm.TableFinal.Rows(indexRows))
                fm.TableFinal.Rows.RemoveAt(indexRows)
                fm.TableFinal.AcceptChanges()
                fm.C_rows2.Text = fm.TableFinal.Rows.Count
                fm.C_rows3.Text = fm.TableTakeDown.Rows.Count
            ElseIf fm.tabcontrol1.SelectedIndex.ToString = 2 Then
                'table takedown
                'copy column to datatable
                If fm.TableTakeDown.Rows.Count <= 0 Then
                    'last column not include from TableTemp
                    For j = 0 To fm.TableTemp.Columns.Count - 1
                        fm.TableTakeDown.Columns.Add(fm.TableTemp.Columns(j).ColumnName)

                    Next
                End If
                fm.TableFinal.ImportRow(fm.TableTakeDown.Rows(indexRows))
                fm.TableTakeDown.Rows.RemoveAt(indexRows)
                fm.TableTakeDown.AcceptChanges()
                fm.C_rows3.Text = fm.TableTakeDown.Rows.Count
                fm.C_rows2.Text = fm.TableFinal.Rows.Count
            End If

            Me.Close()
            Me.Dispose()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub FormEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fm As Form1 = CType(Me.Owner, Form1)
        If fm.tabcontrol1.SelectedIndex.ToString = 0 Then
        ElseIf fm.tabcontrol1.SelectedIndex.ToString = 1 Then
            btn_success.Text = "Takedown"
        ElseIf fm.tabcontrol1.SelectedIndex.ToString = 2 Then
            btn_success.Text = "Success"
        End If
    End Sub
End Class