<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FPPVerificationEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.FPPVerification.FPPVerificationEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function cplMain_EndCallback() {
            switch (cplMain.cpCallbackParam) {
                case "APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "APPROVE_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "REJECT_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
            }
        }
        function gvUploadDoc_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnProceed":
                    gvUploadDoc.GetRowValues(e.visibleIndex, "cust_prospect_doc_id;", btnProceedOnClick);
                    break;
                case "btnGridUploadDocDownload":
                    //cplMain.PerformCallback("DOWNLOAD;DOWNLOAD");
                    e.processOnServer = true;
                    break;
            }
        }
        function btnProceedOnClick() {
            apcFormProceed.Show();
        }
    </script>
    <dx:ASPxPopupControl ID="apcFormProceed" ClientInstanceName="apcFormProceed" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Silahkan pilih proses apa yang akan dilakukan ?" AllowDragging="True" PopupAnimationType="Fade"
        EnableCallbackAnimation="true" CloseAction="CloseButton"
        EnableViewState="False"
        Width="350px"
        Height="100px"
        FooterStyle-Wrap="False" ShowFooter="false" FooterText="" AllowResize="false">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" ColCount="2" Width="100%">
                    <Items>                       
                        <dx:LayoutItem ShowCaption="false" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Center">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                    <dx:ASPxButton ID="btnApprovetDoc" runat="server" Text="Approve" AutoPostBack="False" UseSubmitBehavior="false" ForeColor="Green" Width="100%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('APPROVE_CONFIRM'); apcFormProceed.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="false" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Center">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                    <dx:ASPxButton ID="btnRejectDoc" runat="server" Text="Reject" AutoPostBack="False" UseSubmitBehavior="false" ForeColor="Red" Width="100%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('REJECT_CONFIRM'); apcFormProceed.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="false" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Center">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                    <dx:ASPxButton ID="btnCancelAssign" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" ForeColor="Gray" Width="100%">
                                        <ClientSideEvents Click="function(s, e) { apcFormProceed.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True" Theme="Glass" EnableCallbackAnimation="true" Width="400px" Height="100px"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server" Theme="Aqua">
                    <Items>
                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None" ColCount="2">
                            <Items>
                                <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                            <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="" Width="100%">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                            <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false" Width="100">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide();cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                            <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="FPP Verification Entry" ColCount="5">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Width="100%">
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="100%" ColSpan="5">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxCardView ID="cvMain" ClientInstanceName="cvMain" runat="server" Width="100%" 
                                    OnDataBinding="cvMain_DataBinding" 
                                    OnCustomCallback="cvMain_CustomCallback" 
                                    OnCustomColumnDisplayText="cvMain_CustomColumnDisplayText" 
                                    OnHtmlCardPrepared="cvMain_HtmlCardPrepared" 
                                    OnCustomButtonCallback="cvMain_CustomButtonCallback" 
                                    OnCardLayoutCreated="cvMain_CardLayoutCreated" 
                                    EnableCallBacks="true" 
                                    Font-Size="9" Font-Names="Calibri" 
                                    ForeColor="DarkOliveGreen">
                                    <Styles>
                                        <Card Border-BorderStyle="None" BackColor="Transparent"></Card>
                                        <EmptyCard Border-BorderStyle="None"></EmptyCard>
                                    </Styles>
                                    <Columns>
                                        <dx:CardViewColumn FieldName="cust_prospect_id" Caption="cust_prospect_id" Visible="false"/>
                                        <dx:CardViewColumn FieldName="fpp_no" Caption="FPP No. "/>
                                        <dx:CardViewColumn FieldName="nama_cust" Caption="Nama Lengkap" />
                                        <dx:CardViewColumn FieldName="ktp_cust" Caption="KTP"/>
                                        <dx:CardViewColumn FieldName="gender_cust" Caption="Jenis Kelamin"/>
                                        <dx:CardViewColumn FieldName="marital_stat_cust" Caption="Status Pernikahan"/>
                                        <dx:CardViewColumn FieldName="tempat_lahir_cust" Caption="Tempat Lahir"/>
                                        <dx:CardViewDateColumn FieldName="tanggal_lahir_cust" Caption="Tanggal Lahir" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"/>
                                        <dx:CardViewColumn FieldName="alamat_ktp_cust" Caption="Alamat Sesuai KTP"/>
                                        <dx:CardViewColumn FieldName="kelurahan_ktp_cust" Caption="Kelurahan"/>
                                        <dx:CardViewColumn FieldName="kecamatan_ktp_cust" Caption="Kecamatan"/>
                                        <dx:CardViewColumn FieldName="kota_ktp_cust" Caption="Kota"/>
                                        <dx:CardViewColumn FieldName="zipcode_ktp_cust" Caption="Kode Pos"/>
                                        <dx:CardViewColumn FieldName="status_tinggal_cust" Caption="Status Tempat Tinggal"/>
                                        <dx:CardViewColumn FieldName="no_telp_cust" Caption="No. Telepon"/>
                                        <dx:CardViewColumn FieldName="email_cust" Caption="Email"/>
                                        <dx:CardViewColumn FieldName="nama_spouse" Caption="Nama Lengkap (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="ktp_spouse" Caption="KTP (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="warga_negara_spouse" Caption="Warga Negara (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="tempat_lahir_spouse" Caption="Tempat Lahir (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="tanggal_lahir_spouse" Caption="Tanggal Lahir (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="alamat_ktp_spouse" Caption="Alamat (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="kota_ktp_spouse" Caption="Kota (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="telp_spouse" Caption="Telepon (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="email_spouse" Caption="Email (Pasangan)"/>
                                        <dx:CardViewColumn FieldName="alamat_tinggal_cust" Caption="Alamat Tinggal"/>
                                        <dx:CardViewColumn FieldName="kota_tinggal_cust" Caption="Kota Tinggal"/>
                                        <dx:CardViewColumn FieldName="lama_tinggal_cust" Caption="Lama Tinggal (Tahun)"/>
                                        <dx:CardViewColumn FieldName="jarak_kantor_cust" Caption="Jarak ke kantor"/>
                                        <dx:CardViewColumn FieldName="jenis_pekerjaan_cust" Caption="Jenis Pekerjaan"/>
                                        <dx:CardViewColumn FieldName="group_mnc_cust" Caption="MNC Group / Non Group"/>
                                        <dx:CardViewColumn FieldName="status_karyawan_cust" Caption="Status Karyawan"/>
                                        <dx:CardViewColumn FieldName="jenis_profesi" Caption="Profesi"/>
                                        <dx:CardViewColumn FieldName="bidang_usaha" Caption="Bidang Usaha"/>
                                        <dx:CardViewColumn FieldName="pengalaman_kerja" Caption="Pengalaman Kerja (Tahun)"/>
                                        <dx:CardViewColumn FieldName="nama_perusahaan_cust" Caption="Nama Perusahaan"/>
                                        <dx:CardViewSpinEditColumn FieldName="penghasilan_cust" Caption="Penghasilan"  PropertiesSpinEdit-DisplayFormatString="#,0.00"/>
                                        <dx:CardViewSpinEditColumn FieldName="penghasilan_spouse" Caption="Penghasil Pasangan"  PropertiesSpinEdit-DisplayFormatString="#,0.00"/>
                                        <dx:CardViewSpinEditColumn FieldName="other_income_cust" Caption="Penghasilan Lain-lain"  PropertiesSpinEdit-DisplayFormatString="#,0.00"/>
                                        <dx:CardViewColumn FieldName="jenis_pekerjaan_spouse" Caption="Pekerjaan Pasangan"/>
                                        <dx:CardViewColumn FieldName="jumlah_tanggungan" Caption="Jumlah Tanggungan"/>
                                        <dx:CardViewColumn FieldName="pekerjaan_status_child1" Caption="Pekerjaan Anak 1"/>
                                        <dx:CardViewColumn FieldName="pekerjaan_status_child2" Caption="Pekerjaan Anak 2"/>
                                        <dx:CardViewColumn FieldName="pekerjaan_status_child3" Caption="Pekerjaan Anak 3"/>
                                        <dx:CardViewColumn FieldName="pekerjaan_status_child4" Caption="Pekerjaan Anak 4"/>
                                        <dx:CardViewColumn FieldName="status_aplikasi" Caption="Status Aplikasi"/>
                                        <dx:CardViewColumn FieldName="cre_by" Caption="cre_by" Visible="false"/>
                                        <dx:CardViewDateColumn FieldName="cre_dt" Caption="Tanggal Pengajuan" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"/>
                                        <dx:CardViewColumn FieldName="upd_by" Caption="upd_by" Visible="false"/>
                                        <dx:CardViewDateColumn FieldName="upd_dt" Caption="upd_dt" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false"/>
                                        <dx:CardViewColumn FieldName="npwp_cust" Caption="NPWP"/>
                                        <dx:CardViewColumn FieldName="hp_cust" Caption="Hp"/>
                                        <dx:CardViewColumn FieldName="mmn_cust" Caption="Nama Ibu Kandung"/>
                                        <dx:CardViewColumn FieldName="tenor" Caption="Tenor"/>
                                        <dx:CardViewColumn FieldName="education_cust" Caption="Pendidikan"/>
                                        <dx:CardViewColumn FieldName="hp_spouse" Caption="Hp"/>
                                        <dx:CardViewColumn FieldName="nama_penjamin" Caption="Nama (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="ktp_penjamin" Caption="KTP (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="warga_negara_penjamin" Caption="Warga Negara (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="gender_penjamin" Caption="Jenis Kelamin (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="marital_stat_penjamin" Caption="Status Pernikahan (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="tempat_lahir_penjamin" Caption="Tempat Lahir (Penjamin)"/>
                                        <dx:CardViewDateColumn FieldName="tgl_lahir_penjamin" Caption="Tgl Lahir (Penjamin)" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"/>
                                        <dx:CardViewColumn FieldName="curr_addr_penjamin" Caption="Alamat (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="curr_kota_penjamin" Caption="Kota (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="cur_addr_stat_penjamin" Caption="Status Tempat Tinggal (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="length_stay_penjamin" Caption="Jarak Ke Kantor (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="hp_penjamin" Caption="Hp (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="tlp_penjamin" Caption="Telepon (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="email_penjamin" Caption="Email (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="nama_spouse_penjamin" Caption="Pasangan (Penjamin)"/>
                                        <dx:CardViewColumn FieldName="nama_ec" Caption="Nama (Contact)"/>
                                        <dx:CardViewColumn FieldName="relationship_ec" Caption="Hubungan (Contact)"/>
                                        <dx:CardViewColumn FieldName="hp_ec" Caption="Hp (Contact)"/>
                                        <dx:CardViewColumn FieldName="telp_ec" Caption="Telepon (Contact)"/>
                                        <dx:CardViewColumn FieldName="email_ec" Caption="Email (Contact)"/>
                                        <dx:CardViewColumn FieldName="mitra_id" Caption="Mitra ID"/>
                                    </Columns>
                                    <CardLayoutProperties ColCount="1">
                                        <Items>
                                            <dx:CardViewColumnLayoutItem ColumnName="fpp_no"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="cre_dt"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="mitra_id"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="status_aplikasi"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tenor"></dx:CardViewColumnLayoutItem>
                                            <dx:EmptyLayoutItem Height="15"></dx:EmptyLayoutItem>
                                            <dx:CardViewColumnLayoutItem Caption="DATA CUSTOMER">
                                                <CaptionStyle ForeColor="DarkGreen" Font-Bold="true" Font-Underline="true"></CaptionStyle>
                                            </dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="nama_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="ktp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="npwp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="mmn_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="gender_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="education_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="marital_stat_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tempat_lahir_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tanggal_lahir_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="alamat_ktp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="kelurahan_ktp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="kecamatan_ktp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="kota_ktp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="zipcode_ktp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="status_tinggal_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="no_telp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="hp_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="email_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="alamat_tinggal_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="kota_tinggal_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="lama_tinggal_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="jarak_kantor_cust" Visible="false"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="jenis_pekerjaan_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="group_mnc_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="jenis_profesi"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="bidang_usaha"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="pengalaman_kerja"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="nama_perusahaan_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="penghasilan_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="penghasilan_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="other_income_cust"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="jenis_pekerjaan_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="jumlah_tanggungan"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="pekerjaan_status_child1"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="pekerjaan_status_child2"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="pekerjaan_status_child3"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="pekerjaan_status_child4"></dx:CardViewColumnLayoutItem>
                                            <dx:EmptyLayoutItem Height="15"></dx:EmptyLayoutItem>
                                            <dx:CardViewColumnLayoutItem Caption="DATA PASANGAN">
                                                <CaptionStyle ForeColor="DarkGreen" Font-Bold="true" Font-Underline="true"></CaptionStyle>
                                            </dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="nama_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="ktp_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="warga_negara_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tempat_lahir_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tanggal_lahir_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="alamat_ktp_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="kota_ktp_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="telp_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="hp_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="email_spouse"></dx:CardViewColumnLayoutItem>
                                            <dx:EmptyLayoutItem Height="15"></dx:EmptyLayoutItem>
                                            <dx:CardViewColumnLayoutItem Caption="DATA PENJAMIN">
                                                <CaptionStyle ForeColor="DarkGreen" Font-Bold="true" Font-Underline="true"></CaptionStyle>
                                            </dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="nama_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="ktp_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="warga_negara_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="gender_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="marital_stat_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tempat_lahir_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tgl_lahir_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="curr_addr_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="curr_kota_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="cur_addr_stat_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="length_stay_penjamin" Visible="false"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="hp_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="tlp_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="email_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="nama_spouse_penjamin"></dx:CardViewColumnLayoutItem>
                                            <dx:EmptyLayoutItem Height="15"></dx:EmptyLayoutItem>
                                            <dx:CardViewColumnLayoutItem Caption="EMERGENCY CONTACT">
                                                <CaptionStyle ForeColor="DarkGreen" Font-Bold="true" Font-Underline="true"></CaptionStyle>
                                            </dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="nama_ec"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="relationship_ec"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="hp_ec"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="telp_ec"></dx:CardViewColumnLayoutItem>
                                            <dx:CardViewColumnLayoutItem ColumnName="email_ec"></dx:CardViewColumnLayoutItem>
                                        </Items>
                                    </CardLayoutProperties>
                                    <SettingsPager EnableAdaptivity="true">
                                        <SettingsTableLayout ColumnCount="1" RowsPerPage="1" />
                                    </SettingsPager>
                                    <SettingsSearchPanel Visible="false" />
                                </dx:ASPxCardView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="None" Caption="" ColCount="5">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                <Items>
                    <dx:LayoutGroup GroupBoxDecoration="Box" ShowCaption="False" Caption="Upload Document" GroupBoxStyle-Caption-BackColor="WhiteSmoke">
                        <GroupBoxStyle>
                            <Caption ForeColor="#b19349" Font-Size="10" Font-Bold="false"></Caption>
                        </GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxGridView
                                                runat="server"
                                                ID="gvUploadDoc"
                                                ClientInstanceName="gvUploadDoc"
                                                Width="100%" KeyFieldName="cust_prospect_doc_id"
                                                AutoGenerateColumns="False"
                                                EnableCallBacks="true"
                                                EnablePagingCallbackAnimation="true"
                                                Font-Names="Calibri" Font-Size="9" ForeColor="#142e5d"
                                                OnDataBinding="gvUploadDoc_DataBinding" OnCustomCallback="gvUploadDoc_CustomCallback" OnCustomButtonCallback="gvUploadDoc_CustomButtonCallback" OnCustomButtonInitialize="gvUploadDoc_CustomButtonInitialize">
                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                <ClientSideEvents CustomButtonClick="function(s, e) { gvUploadDoc_CustomButtonClick(s, e); }"/>
                                                <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                <SettingsPager PageSize="1000" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="colID" Caption="ID." FieldName="cust_prospect_doc_id" ReadOnly="True" Visible="false">
                                                            <HeaderStyle Font-Bold="true" ForeColor="SlateGray"/>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colFileType" Caption="Type" FieldName="file_type" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                                            <HeaderStyle Font-Bold="true" ForeColor="SlateGray"/>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colFileName" Caption="File Name" FieldName="file_name" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                                            <HeaderStyle Font-Bold="true" ForeColor="SlateGray"/>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Name="colExt" Caption="Ext" FieldName="file_ext" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss" Visible="true">
                                                            <HeaderStyle Font-Bold="true" ForeColor="SlateGray"/>
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Name="colStat" Caption="Status" FieldName="doc_stat" ReadOnly="True" UnboundType="String">
                                                            <HeaderStyle Font-Bold="true" ForeColor="SlateGray"/>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Button"  Caption="" Width="5%">
                                                        <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnGridUploadDocDownload" Text="Download">
                                                                <Image Height="20px" Width="20px" Url="../../../Content/Images/download.png" ToolTip="Click here to download file."></Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Button"  Caption="" Width="5%">
                                                        <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnProceed" Text="Proceed" Styles-Style-ForeColor="Green" Styles-Style-DisabledStyle-ForeColor="Gray">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                        </dx:ASPxGridView>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:TabbedLayoutGroup>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
