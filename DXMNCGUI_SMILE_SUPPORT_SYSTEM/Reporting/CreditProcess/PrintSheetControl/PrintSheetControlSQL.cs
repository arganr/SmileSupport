using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.PrintSheetControl
{
    public class PrintSheetControlSQL : PrintSheetControlDB
    {
        string sQuery = "";
        public override DataSet LoadDataClient(string ClientID)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            SqlConnection mylocalconn = new SqlConnection(myLocalDBSetting.ConnectionString);

            DataSet dataSet = new DataSet();

            DataTable dtClient = new DataTable();
            DataTable dtAkteNotaris = new DataTable();
            DataTable dtPengurus = new DataTable();
            DataTable dtPemegangSaham = new DataTable();
            DataTable dtDetailAsset = new DataTable();

            string QueryClient = @"SELECT 
                                    CASE A.STATUS
                                    when '1' then 'Perorangan'
                                    else 'Corporate'			 
                                    END Tipe,
                                    A.Client,
                                    A.Name,
                                    A.REAL_NAME IdentityName,
                                    A.IBUKANDUNG IbuKandung,
                                    A.INBORNPLC TempatLahir,
                                    A.INBORNDT TanggalLahir,
                                    A.NPWP,
                                    A.INKTP KTP,
                                    A.INMAILADD4 Email,
                                    CASE A.RELIGION
                                    WHEN 1 THEN 'Islam'
                                    WHEN 2 THEN 'Kristen'
                                    WHEN 3 THEN 'Katholik'
                                    WHEN 4 THEN 'Budha'
                                    WHEN 5 THEN 'Hindu'
                                    WHEN 6 THEN 'Kong Hu Cu'
                                    ELSE '-'
                                    END Agama,
                                    CASE A.INMARITAL
                                    WHEN 1 THEN 'Belum Menikah'
                                    WHEN 2 THEN 'Menikah'
                                    WHEN 3 THEN 'Janda/Duda'
                                    END StatusNikah,
                                    CASE A.INGENDER
                                    WHEN 1 THEN 'Pria'
                                    WHEN 2 THEN 'Wanita'
                                    END Gender,
                                    A.INEDUCAT Pendidikan,
                                    A.ADDRESS1 + ' KEC. ' + A.KECAMATAN + ' KEL. ' + A.KELURAHAN + ' ' + A.KOTA + ' ' +  A.AREA_CODE AlamatKtp,
                                    A.BILL_ADDRESS1 + ' KEC. ' +  ' KEL. ' + A.KELURAHAN + ' ' + A.KOTA + ' ' +  A.AREA_CODE AlamatTagih,
                                    A.INCOMPANY CoyName,
                                    CASE ISNULL(A.INJOB, '')
                                    WHEN '1' THEN 'Employee'
                                    WHEN '2' THEN 'Entrepreneur'
                                    WHEN '3' THEN 'Profesional'
                                    WHEN '4' THEN 'Others'
                                    WHEN '5' THEN 'Executive'
                                    END AS Job,
                                    ISNULL(B.DESCRIP,'---') ClientGroup,
                                    A.INSPOUNAME SpouseName,
                                    A.INSPOUKT SpouseKtp,
                                    A.INSPOUBRDT SpouseTanggalLahir,
                                    A.INSPOUPLC SpouseTempatLahir,
                                    A.INSALARY Insalary,
                                    A.INADDR1 + ' KEC. ' + A.INSJOB_KECAMATAN + ' KEL. ' + A.INSJOB_KELURAHAN + ' ' + A.INSJOB_KOTA + ' ' +  A.INSJOBAREA_CODE AlamatKantor,
                                    A.AREA_CODES + A.PHONE Telepon,
                                    (SELECT(
                                    CASE C.STATUS
                                    WHEN '1' THEN '+62' + SUBSTRING(C.INMAILTELP,2,LEN(C.INMAILTELP) - 1 )
                                    WHEN '2' THEN '+62' + SUBSTRING(C.CONTACTHP,2,LEN(C.CONTACTHP) - 1 )
                                    END
                                    )
                                    FROM sys_client C
                                    WHERE 	( LEN(C.INMAILTELP) >= 10 or LEN(C.CONTACTHP) >= 10 )
                                    AND (C.CONTACTHP NOT LIKE '080%' OR C.INMAILTELP NOT LIKE '080%')
                                    AND CLIENT = @CLIENT) MobilePhone,
                                    --REGION CV/PT
                                    A.Salute1 JenisBadanUsaha,
                                    CASE ISNULL(A.COBISSTAT, '')
                                    WHEN  1 THEN 'PRIVATE'
                                    WHEN  2 THEN 'PUBLIC'
                                    WHEN  3 THEN 'BUMN'
                                    END StatusBadanUsaha,
                                    A.ADDRESS1 AddressSKD, A.AREA_CODE KodePosSKD,
                                    A.ADDRESS1 AddressNPWP, A.AREA_CODE KodePosNPWP,
                                    A.BILL_ADDRESS1 AddresssBILL, A.AREA_CODE KodePosBILL,
                                    ISNULL(B.DESCRIP,'---') [CorporateGroup],
                                    A.INMAILADD4 [CorporateEmail], A.CONTACT [CorporateContactPerson],
                                    A.CONTACTHP [CorporateContactPersonHp], A.CONTACTTLP [CorporateContactPersonTelp],
                                    A.SIUP, A.SIUP_TO_DT [SIUPExpTo],
                                    A.TDP, A.TDP_TO_DT [TDPExpTo],
                                    A.INBORNPLC [TempatPendirian], A.INKTP [NoAkta], A.INBORNDT [TglAkta],
                                    A.NOTARIS1 [Notaris], A.NO_SK [SKMenhukam], A.SKKHTglPendirian [TglSK],
                                    (SELECT TOP 1 Keterangan FROM [dbo].[REFFGOLDEBITUR] WHERE OJK_STATUS IS NULL AND SANDI = A.SIDGOLONGAN) [SLIKGolongan],
                                    (SELECT TOP 1 Keterangan FROM [dbo].[REFFSEKTOREKONOMI] WHERE OJK_STATUS IS NULL AND SANDI = A.SIDBIDUSAHA) [SLIKSektorEkonomi],
                                    (SELECT TOP 1 Keterangan FROM [dbo].[REFFGOLDEBITUR] WHERE OJK_STATUS=1 AND SANDI = A.GOLONGAN_OJK) [SIPPGolongan],
                                    (SELECT TOP 1 Keterangan FROM [dbo].[REFFSEKTOREKONOMI] WHERE OJK_STATUS=1 AND SANDI = A.SIDBIDUSAHA) [SIPPSektorEkonomi],
                                    (SELECT C_NAME FROM SYS_COMPANY WHERE C_CODE = (SELECT TOP 1 C_CODE FROM LS_APPLICATION WHERE LESSEE = @CLIENT)) AS RO
                                    FROM SYS_CLIENT A
                                    left join SYS_CLIENTGROUP B on A.GROUP_ = B.GROUP_
                                    WHERE CLIENT=@CLIENT
                                    ORDER BY A.NAME";

            string QueryAkteNotaris = @"SELECT 
                                        AKTEUBAH [NoAktaPerubahan],
                                        TGLUBAH [TglAktaPerubahan],
                                        NoSKMenKumHamUbah [SK],
                                        TglSKMenKumHamUbah [TglSK]
                                        FROM SYS_AKTE_PERUBAHAN_NOTARIS WHERE CLIENTNO=@CLIENTNO";

            string QueryPengurus = @"SELECT 
                                        NAMA [NamaPengurus],
                                        ALAMAT [AlamatPengurus],
                                        KTP [NIK],
                                        NPWP,
                                        ISNULL(B.KETERANGAN, '---') [Jabatan]
                                        FROM SID_PENGURUS
                                        LEFT JOIN REFFJABATAN B ON IDJABATAN=B.REFFJABATANID
                                        WHERE LOCALCODE=@LOCALCODE";

            string QueryPemegangSaham = @"SELECT 
                                            NAMA [NamaPemegangSaham],
                                            ISNULL(B.KETERANGAN, '---') [Jabatan],
                                            PANGSA + '%' [PorsiKepemilikan],
                                            ISNULL(modal, 0) [ModalDisetor],
                                            0 [ModalDasar]
                                            FROM SID_PENGURUS
                                            LEFT JOIN [dbo].[REFFJABATAN] B ON IDJABATAN=B.[REFFJABATANID] WHERE LOCALCODE=@LOCALCODE AND PANGSA IS NOT NULL";

            string QueryDetailAsset = @"select B.DocNo, A.ItemDescription, A.Year, A.Condition, A.AssetTypeDetail from [dbo].[ApplicationDetail] A 
										inner join [dbo].[Application] B ON A.DocKey = B.DocKey";

            using (SqlCommand cmdClient = new SqlCommand(QueryClient, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdClient);
                cmdClient.Parameters.Add("@CLIENT", SqlDbType.NVarChar);
                cmdClient.Parameters["@CLIENT"].Value = ClientID;
                adapter.Fill(dtClient);
            }
            using (SqlCommand cmdAkteNotaris = new SqlCommand(QueryAkteNotaris, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdAkteNotaris);
                cmdAkteNotaris.Parameters.Add("@CLIENTNO", SqlDbType.NVarChar);
                cmdAkteNotaris.Parameters["@CLIENTNO"].Value = ClientID;
                adapter.Fill(dtAkteNotaris);
            }
            using (SqlCommand cmdPengurus = new SqlCommand(QueryPengurus, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdPengurus);
                cmdPengurus.Parameters.Add("@LOCALCODE", SqlDbType.NVarChar);
                cmdPengurus.Parameters["@LOCALCODE"].Value = ClientID;
                adapter.Fill(dtPengurus);
            }
            using (SqlCommand cmdPemegangSaham = new SqlCommand(QueryPemegangSaham, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdPemegangSaham);
                cmdPemegangSaham.Parameters.Add("@LOCALCODE", SqlDbType.NVarChar);
                cmdPemegangSaham.Parameters["@LOCALCODE"].Value = ClientID;
                adapter.Fill(dtPemegangSaham);
            }
            using (SqlCommand cmdDetailAsset = new SqlCommand(QueryDetailAsset, mylocalconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdDetailAsset);
                adapter.Fill(dtDetailAsset);
            }

            dtClient.TableName = "CLIENT";
            dtAkteNotaris.TableName = "AKTE_NOTARIS";
            dtPengurus.TableName = "PENGURUS";
            dtPemegangSaham.TableName = "PEMEGANG_SAHAM";
            dtDetailAsset.TableName = "DETAIL_ASSET";

            DataColumn[] KeyClient = new DataColumn[1];
            KeyClient[0] = dtClient.Columns["SYS_CLIENTID"];
            dtClient.PrimaryKey = KeyClient;

            DataColumn[] KeyAkteNotaris = new DataColumn[1];
            KeyAkteNotaris[0] = dtAkteNotaris.Columns["ID"];
            dtAkteNotaris.PrimaryKey = KeyAkteNotaris;

            DataColumn[] KeyPengurus = new DataColumn[1];
            KeyPengurus[0] = dtPengurus.Columns["SID_PENGURUSID"];
            dtPengurus.PrimaryKey = KeyPengurus;

            DataColumn[] KeyPemegangSaham = new DataColumn[1];
            KeyPemegangSaham[0] = dtPemegangSaham.Columns["SID_PENGURUSID"];
            dtPemegangSaham.PrimaryKey = KeyPemegangSaham;

            DataColumn[] KeyDetailAsset = new DataColumn[1];
            KeyDetailAsset[0] = dtDetailAsset.Columns["LS_APPLIASSETID"];
            dtDetailAsset.PrimaryKey = KeyDetailAsset;

            dataSet.Tables.Add(dtClient);
            dataSet.Tables.Add(dtAkteNotaris);
            dataSet.Tables.Add(dtPengurus);
            dataSet.Tables.Add(dtPemegangSaham);
            dataSet.Tables.Add(dtDetailAsset);

            return dataSet;
        }
        public override DataTable LoadDataSheetControl()
        {
            myControlSheetTable.Clear();
            try
            {
                sQuery = @"SELECT * FROM [dbo].[SheetControl] ORDER BY DocDate DESC";

                myLocalDBSetting.LoadDataTable(myControlSheetTable, sQuery, true);
                DataColumn[] keyHeader = new DataColumn[0];
                keyHeader[0] = myControlSheetTable.Columns["DocNo"];
                myControlSheetTable.PrimaryKey = keyHeader;
            }
            catch
            { }
            return myControlSheetTable;
        }
        public override DataTable LoadDataKontrak()
        {
            myNoKontrakTable.Clear();
            sQuery = @"SELECT DocNo [NO APLIKASI], CIF [CLIENT], ClientName [DEBITUR], Status 
                        FROM [dbo].[Application]
                        WHERE Status NOT IN  ('DONE','CANCELLED','REJECTED')";
            myLocalDBSetting.LoadDataTable(myNoKontrakTable, sQuery, true);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myNoKontrakTable.Columns["NO APLIKASI"];
            myNoKontrakTable.PrimaryKey = keyHeader;
            return myNoKontrakTable;
        }
        public override DataTable LoadDataDocMand()
        {
            myDocMandTable.Clear();

            sQuery = @"SELECT * FROM [dbo].[DocumentMandatory]";

            myLocalDBSetting.LoadDataTable(myDocMandTable, sQuery, true);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myDocMandTable.Columns["DocKey"];
            myDocMandTable.PrimaryKey = keyHeader;
            return myDocMandTable;
        }
        public override DataTable LoadDataDocAdd()
        {
            myDocAddTable.Clear();

            sQuery = @"SELECT * FROM [dbo].[DocumentAdditional]";

            myLocalDBSetting.LoadDataTable(myDocAddTable, sQuery, true);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myDocAddTable.Columns["DocKey"];
            myDocAddTable.PrimaryKey = keyHeader;
            return myDocAddTable;
        }
        
        protected override void SaveSheetControl(DataTable dt, DataTable dtClient, DataTable dtAkteNotaris, DataTable dtPengurus, DataTable dtPemegangSaham, DataTable dtDetailAsset, SaveAction saveaction, string userName)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                #region INSERT PERORANGAN
                DataRow dataRow = dt.Rows[0];
                DataRow dataRowClient = dtClient.Rows[0];
                if (saveaction == SaveAction.Save)
                {
                    string sQuery = "";
                    sQuery = @"INSERT INTO [dbo].[SheetControl] 
                        (DocNo, DocDate, AppNo, Client, RO, 
                            Tipe, Name, IdentityName, IbuKandung, TempatLahir,
                                TanggalLahir, NPWP, KTP, Email, Agama,
                                    StatusNikah, Gender, Pendidikan, AlamatKtp,
                                        AlamatTagih, CoyName, Job, ClientGroup, SpouseName,
                                            SpouseKtp, SpouseTanggalLahir, SpouseTempatLahir, Insalary, AlamatKantor,
                                                Telepon, MobilePhone, JenisPengikatan,
                                                    CRNo, CRDate, CAMNo, CAMDate, 
                                                        DocMand, DocAddi, LegalConclution, UncompletedDoc, CreatedBy, CreatedDateTime, LastModifiedBy, LastModifiedDateTime, SLIKGolongan, SLIKSektorEkonomi, SIPPGolongan, SIPPSektorEkonomi,
                                                            FooterMadeBy, FooterMadeByPos, FooterApprovedBy, FooterApprovedByPos, FooterMarketing, FooterMarketingPos, FooterBusinessManager, FooterBusinessManagerPos) 
                    VALUES 
                        (@DocNo, @DocDate, @AppNo, @Client, @RO, 
                            @Tipe, @Name, @IdentityName, @IbuKandung, @TempatLahir,
                                @TanggalLahir, @NPWP, @KTP, @Email, @Agama,
                                    @StatusNikah, @Gender, @Pendidikan, @AlamatKtp,
                                        @AlamatTagih, @CoyName, @Job, @ClientGroup, @SpouseName,
                                            @SpouseKtp, @SpouseTanggalLahir, @SpouseTempatLahir, @Insalary, @AlamatKantor,
                                                @Telepon, @MobilePhone, @JenisPengikatan, @CRNo, @CRDate, @CAMNo, @CAMDate, @DocMand, 
                                                    @DocAddi, @LegalConclution, @UncompletedDoc, @CreatedBy, @CreatedDateTime, @LastModifiedBy, @LastModifiedDateTime, @SLIKGolongan, @SLIKSektorEkonomi, @SIPPGolongan, @SIPPSektorEkonomi,
                                                        @FooterMadeBy, @FooterMadeByPos, @FooterApprovedBy, @FooterApprovedByPos, @FooterMarketing, @FooterMarketingPos, @FooterBusinessManager, @FooterBusinessManagerPos)";

                    SqlCommand sqlCommand = new SqlCommand(sQuery);
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    DataRow[] myrowDocNo = myLocalDBSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='LK'", "", DataViewRowState.CurrentRows);
                    if (myrowDocNo != null)
                    {
                        dataRow["DocNo"] = Controllers.Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myLocalDBSetting.GetServerTime());
                    }
                    dataRow["DocDate"] = myLocalDBSetting.GetServerTime();
                    dataRow["CreatedBy"] = userName;
                    dataRow["CreatedDateTime"] = myLocalDBSetting.GetServerTime();
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = myLocalDBSetting.GetServerTime();

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.NVarChar, 30);
                    sqlParameter1.Value = dataRow["DocNo"];
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocDate", SqlDbType.DateTime);
                    sqlParameter2.Value = myLocalDBSetting.GetServerTime();
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                    sqlParameter3.Value = dataRow["AppNo"];
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Client", SqlDbType.NVarChar, 50);
                    sqlParameter4.Value = dataRowClient["Client"];
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@RO", SqlDbType.NVarChar, 100);
                    sqlParameter5.Value = dataRowClient["RO"];
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Tipe", SqlDbType.NVarChar, 50);
                    sqlParameter6.Value = dataRowClient["Tipe"];
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
                    sqlParameter7.Value = dataRowClient["Name"];
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@IdentityName", SqlDbType.NVarChar, 100);
                    sqlParameter8.Value = dataRowClient["IdentityName"];
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@IbuKandung", SqlDbType.NVarChar, 100);
                    sqlParameter9.Value = dataRowClient["IbuKandung"];
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@TempatLahir", SqlDbType.NVarChar, 100);
                    sqlParameter10.Value = dataRowClient["TempatLahir"];
                    sqlParameter10.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@TanggalLahir", SqlDbType.DateTime);
                    sqlParameter11.Value = dataRowClient["TanggalLahir"];
                    sqlParameter11.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@NPWP", SqlDbType.NVarChar, 50);
                    sqlParameter12.Value = dataRowClient["NPWP"];
                    sqlParameter12.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@KTP", SqlDbType.NVarChar, 50);
                    sqlParameter13.Value = dataRowClient["KTP"];
                    sqlParameter13.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50);
                    sqlParameter14.Value = dataRowClient["Email"];
                    sqlParameter14.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@Agama", SqlDbType.NVarChar, 50);
                    sqlParameter15.Value = dataRowClient["Agama"];
                    sqlParameter15.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@StatusNikah", SqlDbType.NVarChar, 50);
                    sqlParameter16.Value = dataRowClient["StatusNikah"];
                    sqlParameter16.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter17 = sqlCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 50);
                    sqlParameter17.Value = dataRowClient["Gender"];
                    sqlParameter17.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter18 = sqlCommand.Parameters.Add("@Pendidikan", SqlDbType.NVarChar, 100);
                    sqlParameter18.Value = dataRowClient["Pendidikan"];
                    sqlParameter18.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter19 = sqlCommand.Parameters.Add("@AlamatKtp", SqlDbType.NVarChar, 250);
                    sqlParameter19.Value = dataRowClient["AlamatKtp"];
                    sqlParameter19.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter20 = sqlCommand.Parameters.Add("@AlamatTagih", SqlDbType.NVarChar, 250);
                    sqlParameter20.Value = dataRowClient["AlamatTagih"];
                    sqlParameter20.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter21 = sqlCommand.Parameters.Add("@CoyName", SqlDbType.NVarChar, 100);
                    sqlParameter21.Value = dataRowClient["CoyName"];
                    sqlParameter21.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter22 = sqlCommand.Parameters.Add("@Job", SqlDbType.NVarChar, 100);
                    sqlParameter22.Value = dataRowClient["Job"];
                    sqlParameter22.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter23 = sqlCommand.Parameters.Add("@ClientGroup", SqlDbType.NVarChar, 100);
                    sqlParameter23.Value = dataRowClient["ClientGroup"];
                    sqlParameter23.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter24 = sqlCommand.Parameters.Add("@SpouseName", SqlDbType.NVarChar, 100);
                    sqlParameter24.Value = dataRowClient["SpouseName"];
                    sqlParameter24.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter25 = sqlCommand.Parameters.Add("@SpouseKtp", SqlDbType.NVarChar, 50);
                    sqlParameter25.Value = dataRowClient["SpouseKtp"];
                    sqlParameter25.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter26 = sqlCommand.Parameters.Add("@SpouseTanggalLahir", SqlDbType.DateTime);
                    sqlParameter26.Value = dataRowClient["SpouseTanggalLahir"];
                    sqlParameter26.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter27 = sqlCommand.Parameters.Add("@SpouseTempatLahir", SqlDbType.NVarChar, 100);
                    sqlParameter27.Value = dataRowClient["SpouseTempatLahir"];
                    sqlParameter27.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter28 = sqlCommand.Parameters.Add("@Insalary", SqlDbType.Decimal);
                    sqlParameter28.Value = dataRowClient["Insalary"];
                    sqlParameter28.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter29 = sqlCommand.Parameters.Add("@AlamatKantor", SqlDbType.NVarChar, 250);
                    sqlParameter29.Value = dataRowClient["AlamatKantor"];
                    sqlParameter29.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter30 = sqlCommand.Parameters.Add("@Telepon", SqlDbType.NVarChar, 25);
                    sqlParameter30.Value = dataRowClient["Telepon"];
                    sqlParameter30.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter31 = sqlCommand.Parameters.Add("@MobilePhone", SqlDbType.NVarChar, 25);
                    sqlParameter31.Value = dataRowClient["MobilePhone"];
                    sqlParameter31.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter32 = sqlCommand.Parameters.Add("@JenisPengikatan", SqlDbType.NVarChar, 50);
                    sqlParameter32.Value = dataRow["JenisPengikatan"];
                    sqlParameter32.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter33 = sqlCommand.Parameters.Add("@CRNo", SqlDbType.NVarChar);
                    sqlParameter33.Value = dataRow["CRNo"];
                    sqlParameter33.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter34 = sqlCommand.Parameters.Add("@CRDate", SqlDbType.DateTime);
                    sqlParameter34.Value = dataRow["CRDate"];
                    sqlParameter34.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter35 = sqlCommand.Parameters.Add("@CAMNo", SqlDbType.NVarChar);
                    sqlParameter35.Value = dataRow["CAMNo"];
                    sqlParameter35.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter36 = sqlCommand.Parameters.Add("@CAMDate", SqlDbType.DateTime);
                    sqlParameter36.Value = dataRow["CAMDate"];
                    sqlParameter36.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter37 = sqlCommand.Parameters.Add("@DocMand", SqlDbType.NVarChar);
                    sqlParameter37.Value = dataRow["DocMand"];
                    sqlParameter37.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter38 = sqlCommand.Parameters.Add("@DocAddi", SqlDbType.NVarChar);
                    sqlParameter38.Value = dataRow["DocAddi"];
                    sqlParameter38.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter39 = sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100);
                    sqlParameter39.Value = dataRow["CreatedBy"];
                    sqlParameter39.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter40 = sqlCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime);
                    sqlParameter40.Value = myLocalDBSetting.GetServerTime();
                    sqlParameter40.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter41 = sqlCommand.Parameters.Add("@LegalConclution", SqlDbType.NVarChar);
                    sqlParameter41.Value = dataRow["LegalConclution"];
                    sqlParameter41.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter42 = sqlCommand.Parameters.Add("@UncompletedDoc", SqlDbType.NVarChar);
                    sqlParameter42.Value = dataRow["UncompletedDoc"];
                    sqlParameter42.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter43 = sqlCommand.Parameters.Add("@LastModifiedBy", SqlDbType.NVarChar, 100);
                    sqlParameter43.Value = dataRow["LastModifiedBy"];
                    sqlParameter43.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter44 = sqlCommand.Parameters.Add("@LastModifiedDateTime", SqlDbType.DateTime);
                    sqlParameter44.Value = myLocalDBSetting.GetServerTime();
                    sqlParameter44.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter45 = sqlCommand.Parameters.Add("@SLIKGolongan", SqlDbType.NVarChar, 100);
                    sqlParameter45.Value = dataRowClient["SLIKGolongan"];
                    sqlParameter45.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter46 = sqlCommand.Parameters.Add("@SLIKSektorEkonomi", SqlDbType.NVarChar, 100);
                    sqlParameter46.Value = dataRowClient["SLIKSektorEkonomi"];
                    sqlParameter46.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter47 = sqlCommand.Parameters.Add("@SIPPGolongan", SqlDbType.NVarChar, 100);
                    sqlParameter47.Value = dataRowClient["SIPPGolongan"];
                    sqlParameter47.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter48 = sqlCommand.Parameters.Add("@SIPPSektorEkonomi", SqlDbType.NVarChar, 100);
                    sqlParameter48.Value = dataRowClient["SIPPSektorEkonomi"];
                    sqlParameter48.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter49 = sqlCommand.Parameters.Add("@FooterMadeBy", SqlDbType.NVarChar, 100);
                    sqlParameter49.Value = dataRow["FooterMadeBy"];
                    sqlParameter49.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter50 = sqlCommand.Parameters.Add("@FooterMadeByPos", SqlDbType.NVarChar, 50);
                    sqlParameter50.Value = dataRow["FooterMadeByPos"];
                    sqlParameter50.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter51 = sqlCommand.Parameters.Add("@FooterApprovedBy", SqlDbType.NVarChar, 100);
                    sqlParameter51.Value = dataRow["FooterApprovedBy"];
                    sqlParameter51.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter52 = sqlCommand.Parameters.Add("@FooterApprovedByPos", SqlDbType.NVarChar, 50);
                    sqlParameter52.Value = dataRow["FooterApprovedByPos"];
                    sqlParameter52.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter53 = sqlCommand.Parameters.Add("@FooterMarketing", SqlDbType.NVarChar, 100);
                    sqlParameter53.Value = dataRow["FooterMarketing"];
                    sqlParameter53.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter54 = sqlCommand.Parameters.Add("@FooterMarketingPos", SqlDbType.NVarChar, 50);
                    sqlParameter54.Value = dataRow["FooterMarketingPos"];
                    sqlParameter54.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter55 = sqlCommand.Parameters.Add("@FooterBusinessManager", SqlDbType.NVarChar, 100);
                    sqlParameter55.Value = dataRow["FooterBusinessManager"];
                    sqlParameter55.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter56 = sqlCommand.Parameters.Add("@FooterBusinessManagerPos", SqlDbType.NVarChar, 50);
                    sqlParameter56.Value = dataRow["FooterBusinessManagerPos"];
                    sqlParameter56.Direction = ParameterDirection.Input;
                    sqlCommand.ExecuteNonQuery();
                }
                #endregion
                #region INSERT BADAN USAHA CV / PT
                if (Convert.ToString(dataRowClient["Tipe"]).Contains("Corporate"))
                {
                    try
                    {
                        DataRow dataRowBadanUsaha = dtClient.Rows[0];
                        string sQueryBadanUsaha = @"INSERT INTO [dbo].[SheetControlBadanUsaha] (
                                                        AppNo, Name, NPWP, JenisBadanUsaha, StatusBadanUsaha, AddressSKD, KodePOSSKD,  AddressNPWP, KodePOSNPWP, AddresssBILL, KodePOSBILL,
                                                            CorporateGroup, CorporateEmail, CorporateContactPerson, CorporateContactPersonHp, CorporateContactPersonTelp, SIUP, SIUPExpTo,
                                                                 TDP, TDPExpTo, TempatPendirian, NoAkta, TglAkta, Notaris, SKMenhukam, TglSK,
                                                                    SLIKGolongan, SLIKSektorEkonomi, SIPPGolongan, SIPPSektorEkonomi)
                                                    VALUES
                                                        (@AppNo, @Name, @NPWP, @JenisBadanUsaha, @StatusBadanUsaha, @AddressSKD, @KodePOSSKD, @AddressNPWP, @KodePOSNPWP, @AddresssBILL, @KodePOSBILL,
                                                            @CorporateGroup, @CorporateEmail, @CorporateContactPerson, @CorporateContactPersonHp, @CorporateContactPersonTelp, @SIUP, @SIUPExpTo,
                                                               @TDP, @TDPExpTo, @TempatPendirian, @NoAkta, @TglAkta, @Notaris, @SKMenhukam, @TglSK,
                                                                    @SLIKGolongan, @SLIKSektorEkonomi, @SIPPGolongan, @SIPPSektorEkonomi)";
                        SqlCommand sqlCommandBadanUsaha = new SqlCommand(sQueryBadanUsaha);
                        sqlCommandBadanUsaha.Connection = myconn;
                        sqlCommandBadanUsaha.Transaction = trans;

                        SqlParameter sqlParameterBadanUsaha1 = sqlCommandBadanUsaha.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                        sqlParameterBadanUsaha1.Value = dataRow["AppNo"];
                        sqlParameterBadanUsaha1.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha2 = sqlCommandBadanUsaha.Parameters.Add("@Name", SqlDbType.NVarChar, 250);
                        sqlParameterBadanUsaha2.Value = dataRowBadanUsaha["Name"];
                        sqlParameterBadanUsaha2.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha3 = sqlCommandBadanUsaha.Parameters.Add("@NPWP", SqlDbType.NVarChar, 50);
                        sqlParameterBadanUsaha3.Value = dataRowBadanUsaha["NPWP"];
                        sqlParameterBadanUsaha3.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha4 = sqlCommandBadanUsaha.Parameters.Add("@JenisBadanUsaha", SqlDbType.NVarChar, 50);
                        sqlParameterBadanUsaha4.Value = dataRowBadanUsaha["JenisBadanUsaha"];
                        sqlParameterBadanUsaha4.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha5 = sqlCommandBadanUsaha.Parameters.Add("@StatusBadanUsaha", SqlDbType.NVarChar, 50);
                        sqlParameterBadanUsaha5.Value = dataRowBadanUsaha["StatusBadanUsaha"];
                        sqlParameterBadanUsaha5.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha6 = sqlCommandBadanUsaha.Parameters.Add("@AddressSKD", SqlDbType.NVarChar, 250);
                        sqlParameterBadanUsaha6.Value = dataRowBadanUsaha["AddressSKD"];
                        sqlParameterBadanUsaha6.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha7 = sqlCommandBadanUsaha.Parameters.Add("@AddressNPWP", SqlDbType.NVarChar, 250);
                        sqlParameterBadanUsaha7.Value = dataRowBadanUsaha["AddressNPWP"];
                        sqlParameterBadanUsaha7.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha8 = sqlCommandBadanUsaha.Parameters.Add("@AddresssBILL", SqlDbType.NVarChar, 250);
                        sqlParameterBadanUsaha8.Value = dataRowBadanUsaha["AddresssBILL"];
                        sqlParameterBadanUsaha8.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha9 = sqlCommandBadanUsaha.Parameters.Add("@CorporateGroup", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha9.Value = dataRowBadanUsaha["CorporateGroup"];
                        sqlParameterBadanUsaha9.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha10 = sqlCommandBadanUsaha.Parameters.Add("@CorporateEmail", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha10.Value = dataRowBadanUsaha["CorporateEmail"];
                        sqlParameterBadanUsaha10.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha11 = sqlCommandBadanUsaha.Parameters.Add("@CorporateContactPerson", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha11.Value = dataRowBadanUsaha["CorporateContactPerson"];
                        sqlParameterBadanUsaha11.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha12 = sqlCommandBadanUsaha.Parameters.Add("@CorporateContactPersonHp", SqlDbType.NVarChar, 20);
                        sqlParameterBadanUsaha12.Value = dataRowBadanUsaha["CorporateContactPersonHp"];
                        sqlParameterBadanUsaha12.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha13 = sqlCommandBadanUsaha.Parameters.Add("@CorporateContactPersonTelp", SqlDbType.NVarChar, 20);
                        sqlParameterBadanUsaha13.Value = dataRowBadanUsaha["CorporateContactPersonTelp"];
                        sqlParameterBadanUsaha13.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha14 = sqlCommandBadanUsaha.Parameters.Add("@SIUP", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha14.Value = dataRowBadanUsaha["SIUP"];
                        sqlParameterBadanUsaha14.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha15 = sqlCommandBadanUsaha.Parameters.Add("@SIUPExpTo", SqlDbType.DateTime);
                        sqlParameterBadanUsaha15.Value = dataRowBadanUsaha["SIUPExpTo"];
                        sqlParameterBadanUsaha15.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha16 = sqlCommandBadanUsaha.Parameters.Add("@TDP", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha16.Value = dataRowBadanUsaha["TDP"];
                        sqlParameterBadanUsaha16.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha17 = sqlCommandBadanUsaha.Parameters.Add("@TDPExpTo", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha17.Value = dataRowBadanUsaha["TDPExpTo"];
                        sqlParameterBadanUsaha17.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha18 = sqlCommandBadanUsaha.Parameters.Add("@TempatPendirian", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha18.Value = dataRowBadanUsaha["TempatPendirian"];
                        sqlParameterBadanUsaha18.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha19 = sqlCommandBadanUsaha.Parameters.Add("@NoAkta", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha19.Value = dataRowBadanUsaha["NoAkta"];
                        sqlParameterBadanUsaha19.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha20 = sqlCommandBadanUsaha.Parameters.Add("@TglAkta", SqlDbType.DateTime);
                        sqlParameterBadanUsaha20.Value = dataRowBadanUsaha["TglAkta"];
                        sqlParameterBadanUsaha20.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha21 = sqlCommandBadanUsaha.Parameters.Add("@Notaris", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha21.Value = dataRowBadanUsaha["Notaris"];
                        sqlParameterBadanUsaha21.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha22 = sqlCommandBadanUsaha.Parameters.Add("@SKMenhukam", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha22.Value = dataRowBadanUsaha["SKMenhukam"];
                        sqlParameterBadanUsaha22.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha23 = sqlCommandBadanUsaha.Parameters.Add("@TglSK", SqlDbType.DateTime);
                        sqlParameterBadanUsaha23.Value = dataRowBadanUsaha["TglSK"];
                        sqlParameterBadanUsaha23.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha24 = sqlCommandBadanUsaha.Parameters.Add("@SLIKGolongan", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha24.Value = dataRowBadanUsaha["SLIKGolongan"];
                        sqlParameterBadanUsaha24.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha25 = sqlCommandBadanUsaha.Parameters.Add("@SLIKSektorEkonomi", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha25.Value = dataRowBadanUsaha["SLIKSektorEkonomi"];
                        sqlParameterBadanUsaha25.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha26 = sqlCommandBadanUsaha.Parameters.Add("@SIPPGolongan", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha26.Value = dataRowBadanUsaha["SIPPGolongan"];
                        sqlParameterBadanUsaha26.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha27 = sqlCommandBadanUsaha.Parameters.Add("@SIPPSektorEkonomi", SqlDbType.NVarChar, 100);
                        sqlParameterBadanUsaha27.Value = dataRowBadanUsaha["SIPPSektorEkonomi"];
                        sqlParameterBadanUsaha27.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha28 = sqlCommandBadanUsaha.Parameters.Add("@KodePOSSKD", SqlDbType.NVarChar, 10);
                        sqlParameterBadanUsaha28.Value = dataRowBadanUsaha["KodePOSSKD"];
                        sqlParameterBadanUsaha28.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha29 = sqlCommandBadanUsaha.Parameters.Add("@KodePOSNPWP", SqlDbType.NVarChar, 10);
                        sqlParameterBadanUsaha29.Value = dataRowBadanUsaha["KodePOSNPWP"];
                        sqlParameterBadanUsaha29.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterBadanUsaha30 = sqlCommandBadanUsaha.Parameters.Add("@KodePOSBILL", SqlDbType.NVarChar, 10);
                        sqlParameterBadanUsaha30.Value = dataRowBadanUsaha["KodePOSBILL"];
                        sqlParameterBadanUsaha30.Direction = ParameterDirection.Input;

                        sqlCommandBadanUsaha.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new ArgumentException(ex.Message);
                    }
                }
                #endregion
                #region INSERT AKTE NOTARIS
                try
                {
                    foreach (DataRow dataRowAkteNotaris in dtAkteNotaris.Rows)
                    {
                        
                        string sQueryAkteNotaris = @"INSERT INTO [dbo].[SheetControlAkteNotaris] 
                                                        (AppNo, NoAktaPerubahan, TglAktaPerubahan, SK, TglSK)
                                                    VALUES
                                                        (@AppNo, @NoAktaPerubahan, @TglAktaPerubahan, @SK, @TglSK)";
                        SqlCommand sqlCommandAkteNotaris = new SqlCommand(sQueryAkteNotaris);
                        sqlCommandAkteNotaris.Connection = myconn;
                        sqlCommandAkteNotaris.Transaction = trans;

                        SqlParameter sqlParameterAkteNotaris1 = sqlCommandAkteNotaris.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                        sqlParameterAkteNotaris1.Value = dataRow["AppNo"];
                        sqlParameterAkteNotaris1.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterAkteNotaris2 = sqlCommandAkteNotaris.Parameters.Add("@NoAktaPerubahan", SqlDbType.NVarChar, 100);
                        sqlParameterAkteNotaris2.Value = dataRowAkteNotaris["NoAktaPerubahan"];
                        sqlParameterAkteNotaris2.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterAkteNotaris3 = sqlCommandAkteNotaris.Parameters.Add("@TglAktaPerubahan", SqlDbType.DateTime);
                        sqlParameterAkteNotaris3.Value = dataRowAkteNotaris["TglAktaPerubahan"];
                        sqlParameterAkteNotaris3.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterAkteNotaris4 = sqlCommandAkteNotaris.Parameters.Add("@SK", SqlDbType.NVarChar, 100);
                        sqlParameterAkteNotaris4.Value = dataRowAkteNotaris["SK"];
                        sqlParameterAkteNotaris4.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterAkteNotaris5 = sqlCommandAkteNotaris.Parameters.Add("@TglSK", SqlDbType.DateTime);
                        sqlParameterAkteNotaris5.Value = dataRowAkteNotaris["TglSK"];
                        sqlCommandAkteNotaris.ExecuteNonQuery();
                    }             
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new ArgumentException(ex.Message);
                }
                #endregion
                #region INSERT PENGURUS
                try
                {
                    foreach (DataRow dataRowPengurus in dtPengurus.Rows)
                    {

                        string sQueryPengurus = @"INSERT INTO [dbo].[SheetControlPengurus] 
                                                        (AppNo, NamaPengurus, AlamatPengurus, NIK, NPWP, Jabatan)
                                                            VALUES
                                                                (@AppNo, @NamaPengurus, @AlamatPengurus, @NIK, @NPWP, @Jabatan)";

                        SqlCommand sqlCommandPengurus = new SqlCommand(sQueryPengurus);
                        sqlCommandPengurus.Connection = myconn;
                        sqlCommandPengurus.Transaction = trans;

                        SqlParameter sqlParameterPengurus1 = sqlCommandPengurus.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                        sqlParameterPengurus1.Value = dataRow["AppNo"];
                        sqlParameterPengurus1.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPengurus2 = sqlCommandPengurus.Parameters.Add("@NamaPengurus", SqlDbType.NVarChar, 100);
                        sqlParameterPengurus2.Value = dataRowPengurus["NamaPengurus"];
                        sqlParameterPengurus2.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPengurus3 = sqlCommandPengurus.Parameters.Add("@AlamatPengurus", SqlDbType.NVarChar, 250);
                        sqlParameterPengurus3.Value = dataRowPengurus["AlamatPengurus"];
                        sqlParameterPengurus3.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPengurus4 = sqlCommandPengurus.Parameters.Add("@NIK", SqlDbType.NVarChar, 100);
                        sqlParameterPengurus4.Value = dataRowPengurus["NIK"];
                        sqlParameterPengurus4.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPengurus5 = sqlCommandPengurus.Parameters.Add("@NPWP", SqlDbType.NVarChar, 100);
                        sqlParameterPengurus5.Value = dataRowPengurus["NPWP"];
                        sqlParameterPengurus5.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPengurus6 = sqlCommandPengurus.Parameters.Add("@Jabatan", SqlDbType.NVarChar, 100);
                        sqlParameterPengurus6.Value = dataRowPengurus["Jabatan"];
                        sqlParameterPengurus6.Direction = ParameterDirection.Input;
                        sqlCommandPengurus.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new ArgumentException(ex.Message);
                }
                #endregion
                #region INSERT PEMEGANG SAHAM
                try
                {
                    foreach (DataRow dataRowPemegangSaham in dtPemegangSaham.Rows)
                    {
                        string sQueryPemegangSaham = @"INSERT INTO [dbo].[SheetControlPemegangSaham]
                                                        (AppNo, NamaPemegangSaham, PorsiKepemilikan, ModalDasar, ModalDisetor, Jabatan)
                                                            VALUES
                                                                (@AppNo, @NamaPemegangSaham, @PorsiKepemilikan, @ModalDasar, @ModalDisetor, @Jabatan)";

                        SqlCommand sqlCommandPemegangSaham = new SqlCommand(sQueryPemegangSaham);
                        sqlCommandPemegangSaham.Connection = myconn;
                        sqlCommandPemegangSaham.Transaction = trans;

                        SqlParameter sqlParameterPemegangSaham1 = sqlCommandPemegangSaham.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                        sqlParameterPemegangSaham1.Value = dataRow["AppNo"];
                        sqlParameterPemegangSaham1.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPemegangSaham2 = sqlCommandPemegangSaham.Parameters.Add("@NamaPemegangSaham", SqlDbType.NVarChar, 100);
                        sqlParameterPemegangSaham2.Value = dataRowPemegangSaham["NamaPemegangSaham"];
                        sqlParameterPemegangSaham2.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPemegangSaham3 = sqlCommandPemegangSaham.Parameters.Add("@PorsiKepemilikan", SqlDbType.NVarChar, 100);
                        sqlParameterPemegangSaham3.Value = dataRowPemegangSaham["PorsiKepemilikan"];
                        sqlParameterPemegangSaham3.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPemegangSaham4 = sqlCommandPemegangSaham.Parameters.Add("@ModalDasar", SqlDbType.Float);
                        sqlParameterPemegangSaham4.Value = dataRowPemegangSaham["ModalDasar"];
                        sqlParameterPemegangSaham4.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPemegangSaham5 = sqlCommandPemegangSaham.Parameters.Add("@ModalDisetor", SqlDbType.Float);
                        sqlParameterPemegangSaham5.Value = dataRowPemegangSaham["ModalDisetor"];
                        sqlParameterPemegangSaham5.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterPemegangSaham6 = sqlCommandPemegangSaham.Parameters.Add("@Jabatan", SqlDbType.NVarChar, 100);
                        sqlParameterPemegangSaham6.Value = dataRowPemegangSaham["Jabatan"];
                        sqlParameterPemegangSaham6.Direction = ParameterDirection.Input;
                        sqlCommandPemegangSaham.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new ArgumentException(ex.Message);
                }
                #endregion
                #region INSERT DETAIL ASSET
                try
                {
                    DataRow[] dataRowDetailAsset = dtDetailAsset.Select("DocNo='" + dataRow["AppNo"] + "'", "");
                    for (int i = 0; i < dataRowDetailAsset.Length; i++)
                    {
                        string sQueryDetailAsset = @"INSERT INTO [dbo].[SheetControlDetailAsset]
                                                    (AppNo, ItemDesc, Year, Condition, AssetTypeDetail)
                                                        VALUES
                                                            (@AppNo, @ItemDesc, @Year, @Condition, @AssetTypeDetail)";

                        SqlCommand sqlCommandDetailAsset = new SqlCommand(sQueryDetailAsset);
                        sqlCommandDetailAsset.Connection = myconn;
                        sqlCommandDetailAsset.Transaction = trans;

                        SqlParameter sqlParameterDetailAsset1 = sqlCommandDetailAsset.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                        sqlParameterDetailAsset1.Value = dataRow["AppNo"];
                        sqlParameterDetailAsset1.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterDetailAsset2 = sqlCommandDetailAsset.Parameters.Add("@ItemDesc", SqlDbType.NVarChar, 250);
                        sqlParameterDetailAsset2.Value = dataRowDetailAsset[i]["ItemDescription"];
                        sqlParameterDetailAsset2.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterDetailAsset3 = sqlCommandDetailAsset.Parameters.Add("@Year", SqlDbType.NVarChar, 5);
                        sqlParameterDetailAsset3.Value = dataRowDetailAsset[i]["Year"];
                        sqlParameterDetailAsset3.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterDetailAsset4 = sqlCommandDetailAsset.Parameters.Add("@Condition", SqlDbType.NVarChar, 20);
                        sqlParameterDetailAsset4.Value = dataRowDetailAsset[i]["Condition"];
                        sqlParameterDetailAsset4.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameterDetailAsset5 = sqlCommandDetailAsset.Parameters.Add("@AssetTypeDetail", SqlDbType.NVarChar, 50);
                        sqlParameterDetailAsset5.Value = dataRowDetailAsset[i]["AssetTypeDetail"];
                        sqlParameterDetailAsset5.Direction = ParameterDirection.Input;
                        sqlCommandDetailAsset.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new ArgumentException(ex.Message);
                }
                #endregion
                #region UPDATE RUNNING NUMBER
                if (saveaction == SaveAction.Save)
                {
                    myLocalDBSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType=?", "LK");
                }
                #endregion
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }
        protected override void UpdateSheetControl(DataTable dt, SaveAction saveaction, string userName)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                DataRow dataRow = dt.Rows[0];
                string sQuery = "";
                sQuery = @"UPDATE [dbo].[SheetControl] SET
                    DocDate=@DocDate, 
                    DocMand=@DocMand, 
                    DocAddi=@DocAddi,
                    LegalConclution=@LegalConclution,
                    UncompletedDoc=@UncompletedDoc,
                    CRNo = @CRNo,
                    CRDate = @CRDate,
                    CAMNo = @CAMNo,
                    CAMDate = @CAMDate,
                    JenisPengikatan=@JenisPengikatan,
                    FooterMadeBy=@FooterMadeBy,
                    FooterMadeByPos=@FooterMadeByPos,
                    FooterApprovedBy=@FooterApprovedBy,
                    FooterApprovedByPos=@FooterApprovedByPos,
                    FooterMarketing=@FooterMarketing,
                    FooterMarketingPos=@FooterMarketingPos,
                    FooterBusinessManager=@FooterBusinessManager,
                    FooterBusinessManagerPos=@FooterBusinessManagerPos
                    WHERE AppNo=@AppNo";

                SqlCommand sqlCommand = new SqlCommand(sQuery);
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                dataRow["DocDate"] = myLocalDBSetting.GetServerTime();

                SqlParameter sqlParameter = sqlCommand.Parameters.Add("@DocDate", SqlDbType.DateTime);
                sqlParameter.Value = myLocalDBSetting.GetServerTime();
                sqlParameter.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                sqlParameter2.Value = dataRow["AppNo"];
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@DocMand", SqlDbType.NVarChar);
                sqlParameter3.Value = dataRow["DocMand"];
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@DocAddi", SqlDbType.NVarChar);
                sqlParameter4.Value = dataRow["DocAddi"];
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@LegalConclution", SqlDbType.NVarChar);
                sqlParameter5.Value = dataRow["LegalConclution"];
                sqlParameter5.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@UncompletedDoc", SqlDbType.NVarChar);
                sqlParameter6.Value = dataRow["UncompletedDoc"];
                sqlParameter6.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@CRNo", SqlDbType.NVarChar);
                sqlParameter7.Value = dataRow["CRNo"];
                sqlParameter7.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@CRDate", SqlDbType.DateTime);
                sqlParameter8.Value = dataRow["CRDate"];
                sqlParameter8.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@CAMNo", SqlDbType.NVarChar);
                sqlParameter9.Value = dataRow["CAMNo"];
                sqlParameter9.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@CAMDate", SqlDbType.DateTime);
                sqlParameter10.Value = dataRow["CAMDate"];
                sqlParameter10.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@JenisPengikatan", SqlDbType.NVarChar);
                sqlParameter11.Value = dataRow["JenisPengikatan"];
                sqlParameter11.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@FooterMadeBy", SqlDbType.NVarChar, 100);
                sqlParameter12.Value = dataRow["FooterMadeBy"];
                sqlParameter12.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@FooterMadeByPos", SqlDbType.NVarChar, 50);
                sqlParameter13.Value = dataRow["FooterMadeByPos"];
                sqlParameter13.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@FooterApprovedBy", SqlDbType.NVarChar, 100);
                sqlParameter14.Value = dataRow["FooterApprovedBy"];
                sqlParameter14.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@FooterApprovedByPos", SqlDbType.NVarChar, 50);
                sqlParameter15.Value = dataRow["FooterApprovedByPos"];
                sqlParameter15.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@FooterMarketing", SqlDbType.NVarChar, 100);
                sqlParameter16.Value = dataRow["FooterMarketing"];
                sqlParameter16.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter17 = sqlCommand.Parameters.Add("@FooterMarketingPos", SqlDbType.NVarChar, 50);
                sqlParameter17.Value = dataRow["FooterMarketingPos"];
                sqlParameter17.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter18 = sqlCommand.Parameters.Add("@FooterBusinessManager", SqlDbType.NVarChar, 100);
                sqlParameter18.Value = dataRow["FooterBusinessManager"];
                sqlParameter18.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter19 = sqlCommand.Parameters.Add("@FooterBusinessManagerPos", SqlDbType.NVarChar, 50);
                sqlParameter19.Value = dataRow["FooterBusinessManagerPos"];
                sqlParameter19.Direction = ParameterDirection.Input;
                sqlCommand.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }
        protected override void DeleteSheetControlDetail(DataTable dt, SaveAction saveaction, string userName)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    string sQuery = "";
                    sQuery = @"DELETE [dbo].[SheetControlBadanUsaha] WHERE AppNo=@AppNo";

                    SqlCommand sqlCommand = new SqlCommand(sQuery);
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                    sqlParameter1.Value = dataRow["AppNo"];
                    sqlParameter1.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    string sQuery = "";
                    sQuery = @"DELETE [dbo].[SheetControlDetailAsset] WHERE AppNo=@AppNo";

                    SqlCommand sqlCommand = new SqlCommand(sQuery);
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                    sqlParameter1.Value = dataRow["AppNo"];
                    sqlParameter1.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    string sQuery = "";
                    sQuery = @"DELETE [dbo].[SheetControlPemegangSaham] WHERE AppNo=@AppNo";

                    SqlCommand sqlCommand = new SqlCommand(sQuery);
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                    sqlParameter1.Value = dataRow["AppNo"];
                    sqlParameter1.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    string sQuery = "";
                    sQuery = @"DELETE [dbo].[SheetControlPengurus] WHERE AppNo=@AppNo";

                    SqlCommand sqlCommand = new SqlCommand(sQuery);
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                    sqlParameter1.Value = dataRow["AppNo"];
                    sqlParameter1.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    string sQuery = "";
                    sQuery = @"DELETE [dbo].[SheetControlAkteNotaris] WHERE AppNo=@AppNo";

                    SqlCommand sqlCommand = new SqlCommand(sQuery);
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar, 30);
                    sqlParameter1.Value = dataRow["AppNo"];
                    sqlParameter1.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }
        protected override void SendCommentSheetControl(DataTable dt, SaveAction saveaction, string userName)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            DataRow dataRow = dt.Rows[0];
            object obj = myLocalDBSetting.ExecuteScalar("SELECT DocKey FROM dbo.Application WHERE DocNo=?", (object)dataRow["AppNo"]);
            if (obj != null && obj != DBNull.Value)
            {
                try
                {
                    string sQuery = "";
                    sQuery = @"INSERT INTO [dbo].[ApplicationCommentHistory] (SourceDocKey, DocNo, CommentBy, CommentNote, CommentDate) VALUES (@SourceDocKey, @DocNo, @CommentBy, @CommentNote, @CommentDate)";

                    SqlCommand sqlCommand = new SqlCommand(sQuery);
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@SourceDocKey", SqlDbType.Int);
                    sqlParameter1.Value = Convert.ToInt32(obj);
                    sqlParameter1.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.NVarChar, 20);
                    sqlParameter2.Value = dataRow["AppNo"];
                    sqlParameter2.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@CommentBy", SqlDbType.NVarChar, 20);
                    sqlParameter3.Value = userName;
                    sqlParameter3.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CommentNote", SqlDbType.NVarChar);
                    sqlParameter4.Value = dataRow["LegalConclution"];
                    sqlParameter4.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CommentDate", SqlDbType.DateTime);
                    sqlParameter5.Value = myDBSetting.GetServerTime();
                    sqlParameter5.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }
        }
    }
}