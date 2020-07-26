namespace SV_SalesRepApp_Backoffice
{
    partial class NewAccountForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.newApplicationDataGridView = new System.Windows.Forms.DataGridView();
            this.newAccountApplicationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.appDbDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.appDbDataSet = new SV_SalesRepApp_Backoffice.AppDbDataSet();
            this.newAccountApplicationTableAdapter = new SV_SalesRepApp_Backoffice.AppDbDataSetTableAdapters.NewAccountApplicationTableAdapter();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceListIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physicalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physical2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suburbDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physical4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gPSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postalCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telephoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellphoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.faxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repVisitFreqDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contactPersonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deliveredToDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deliveryEmailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marketSegmentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.receivedOnDateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processedOnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RejectLine = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newApplicationDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newAccountApplicationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appDbDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appDbDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unprocessed new account applications: ";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.newApplicationDataGridView);
            this.panel1.Location = new System.Drawing.Point(12, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 409);
            this.panel1.TabIndex = 2;
            // 
            // newApplicationDataGridView
            // 
            this.newApplicationDataGridView.AllowUserToAddRows = false;
            this.newApplicationDataGridView.AllowUserToDeleteRows = false;
            this.newApplicationDataGridView.AutoGenerateColumns = false;
            this.newApplicationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newApplicationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.accountNameDataGridViewTextBoxColumn,
            this.accountDescriptionDataGridViewTextBoxColumn,
            this.areaIdDataGridViewTextBoxColumn,
            this.groupIdDataGridViewTextBoxColumn,
            this.priceListIdDataGridViewTextBoxColumn,
            this.physicalDataGridViewTextBoxColumn,
            this.physical2DataGridViewTextBoxColumn,
            this.suburbDataGridViewTextBoxColumn,
            this.physical4DataGridViewTextBoxColumn,
            this.gPSDataGridViewTextBoxColumn,
            this.postalCodeDataGridViewTextBoxColumn,
            this.telephoneDataGridViewTextBoxColumn,
            this.cellphoneDataGridViewTextBoxColumn,
            this.faxDataGridViewTextBoxColumn,
            this.repVisitFreqDataGridViewTextBoxColumn,
            this.contactPersonDataGridViewTextBoxColumn,
            this.deliveredToDataGridViewTextBoxColumn,
            this.deliveryEmailDataGridViewTextBoxColumn,
            this.marketSegmentDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.repIdDataGridViewTextBoxColumn,
            this.processedDataGridViewCheckBoxColumn,
            this.receivedOnDateTimeDataGridViewTextBoxColumn,
            this.processedOnDataGridViewTextBoxColumn});
            this.newApplicationDataGridView.DataSource = this.newAccountApplicationBindingSource;
            this.newApplicationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newApplicationDataGridView.Location = new System.Drawing.Point(0, 0);
            this.newApplicationDataGridView.Name = "newApplicationDataGridView";
            this.newApplicationDataGridView.Size = new System.Drawing.Size(776, 409);
            this.newApplicationDataGridView.TabIndex = 0;
            // 
            // newAccountApplicationBindingSource
            // 
            this.newAccountApplicationBindingSource.DataMember = "NewAccountApplication";
            this.newAccountApplicationBindingSource.DataSource = this.appDbDataSetBindingSource;
            this.newAccountApplicationBindingSource.Filter = "Processed=0";
            this.newAccountApplicationBindingSource.Sort = "Id";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Fetch data from Azure";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(625, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Process selected line";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // appDbDataSetBindingSource
            // 
            this.appDbDataSetBindingSource.DataSource = this.appDbDataSet;
            this.appDbDataSetBindingSource.Position = 0;
            // 
            // appDbDataSet
            // 
            this.appDbDataSet.DataSetName = "AppDbDataSet";
            this.appDbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // newAccountApplicationTableAdapter
            // 
            this.newAccountApplicationTableAdapter.ClearBeforeFill = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.Frozen = true;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 10;
            // 
            // accountNameDataGridViewTextBoxColumn
            // 
            this.accountNameDataGridViewTextBoxColumn.DataPropertyName = "AccountName";
            this.accountNameDataGridViewTextBoxColumn.HeaderText = "AccountName";
            this.accountNameDataGridViewTextBoxColumn.Name = "accountNameDataGridViewTextBoxColumn";
            this.accountNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // accountDescriptionDataGridViewTextBoxColumn
            // 
            this.accountDescriptionDataGridViewTextBoxColumn.DataPropertyName = "AccountDescription";
            this.accountDescriptionDataGridViewTextBoxColumn.HeaderText = "AccountDescription";
            this.accountDescriptionDataGridViewTextBoxColumn.Name = "accountDescriptionDataGridViewTextBoxColumn";
            this.accountDescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // areaIdDataGridViewTextBoxColumn
            // 
            this.areaIdDataGridViewTextBoxColumn.DataPropertyName = "AreaId";
            this.areaIdDataGridViewTextBoxColumn.HeaderText = "AreaId";
            this.areaIdDataGridViewTextBoxColumn.Name = "areaIdDataGridViewTextBoxColumn";
            this.areaIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // groupIdDataGridViewTextBoxColumn
            // 
            this.groupIdDataGridViewTextBoxColumn.DataPropertyName = "GroupId";
            this.groupIdDataGridViewTextBoxColumn.HeaderText = "GroupId";
            this.groupIdDataGridViewTextBoxColumn.Name = "groupIdDataGridViewTextBoxColumn";
            this.groupIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceListIdDataGridViewTextBoxColumn
            // 
            this.priceListIdDataGridViewTextBoxColumn.DataPropertyName = "PriceListId";
            this.priceListIdDataGridViewTextBoxColumn.HeaderText = "PriceListId";
            this.priceListIdDataGridViewTextBoxColumn.Name = "priceListIdDataGridViewTextBoxColumn";
            this.priceListIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // physicalDataGridViewTextBoxColumn
            // 
            this.physicalDataGridViewTextBoxColumn.DataPropertyName = "Physical";
            this.physicalDataGridViewTextBoxColumn.HeaderText = "Physical";
            this.physicalDataGridViewTextBoxColumn.Name = "physicalDataGridViewTextBoxColumn";
            this.physicalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // physical2DataGridViewTextBoxColumn
            // 
            this.physical2DataGridViewTextBoxColumn.DataPropertyName = "Physical2";
            this.physical2DataGridViewTextBoxColumn.HeaderText = "Physical2";
            this.physical2DataGridViewTextBoxColumn.Name = "physical2DataGridViewTextBoxColumn";
            this.physical2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // suburbDataGridViewTextBoxColumn
            // 
            this.suburbDataGridViewTextBoxColumn.DataPropertyName = "Suburb";
            this.suburbDataGridViewTextBoxColumn.HeaderText = "Suburb";
            this.suburbDataGridViewTextBoxColumn.Name = "suburbDataGridViewTextBoxColumn";
            this.suburbDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // physical4DataGridViewTextBoxColumn
            // 
            this.physical4DataGridViewTextBoxColumn.DataPropertyName = "Physical4";
            this.physical4DataGridViewTextBoxColumn.HeaderText = "Physical4";
            this.physical4DataGridViewTextBoxColumn.Name = "physical4DataGridViewTextBoxColumn";
            this.physical4DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gPSDataGridViewTextBoxColumn
            // 
            this.gPSDataGridViewTextBoxColumn.DataPropertyName = "GPS";
            this.gPSDataGridViewTextBoxColumn.HeaderText = "GPS";
            this.gPSDataGridViewTextBoxColumn.Name = "gPSDataGridViewTextBoxColumn";
            this.gPSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // postalCodeDataGridViewTextBoxColumn
            // 
            this.postalCodeDataGridViewTextBoxColumn.DataPropertyName = "PostalCode";
            this.postalCodeDataGridViewTextBoxColumn.HeaderText = "PostalCode";
            this.postalCodeDataGridViewTextBoxColumn.Name = "postalCodeDataGridViewTextBoxColumn";
            this.postalCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // telephoneDataGridViewTextBoxColumn
            // 
            this.telephoneDataGridViewTextBoxColumn.DataPropertyName = "Telephone";
            this.telephoneDataGridViewTextBoxColumn.HeaderText = "Telephone";
            this.telephoneDataGridViewTextBoxColumn.Name = "telephoneDataGridViewTextBoxColumn";
            this.telephoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cellphoneDataGridViewTextBoxColumn
            // 
            this.cellphoneDataGridViewTextBoxColumn.DataPropertyName = "Cellphone";
            this.cellphoneDataGridViewTextBoxColumn.HeaderText = "Cellphone";
            this.cellphoneDataGridViewTextBoxColumn.Name = "cellphoneDataGridViewTextBoxColumn";
            this.cellphoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // faxDataGridViewTextBoxColumn
            // 
            this.faxDataGridViewTextBoxColumn.DataPropertyName = "Fax";
            this.faxDataGridViewTextBoxColumn.HeaderText = "Fax";
            this.faxDataGridViewTextBoxColumn.Name = "faxDataGridViewTextBoxColumn";
            this.faxDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // repVisitFreqDataGridViewTextBoxColumn
            // 
            this.repVisitFreqDataGridViewTextBoxColumn.DataPropertyName = "RepVisitFreq";
            this.repVisitFreqDataGridViewTextBoxColumn.HeaderText = "RepVisitFreq";
            this.repVisitFreqDataGridViewTextBoxColumn.Name = "repVisitFreqDataGridViewTextBoxColumn";
            this.repVisitFreqDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // contactPersonDataGridViewTextBoxColumn
            // 
            this.contactPersonDataGridViewTextBoxColumn.DataPropertyName = "Contact_Person";
            this.contactPersonDataGridViewTextBoxColumn.HeaderText = "Contact_Person";
            this.contactPersonDataGridViewTextBoxColumn.Name = "contactPersonDataGridViewTextBoxColumn";
            this.contactPersonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deliveredToDataGridViewTextBoxColumn
            // 
            this.deliveredToDataGridViewTextBoxColumn.DataPropertyName = "Delivered_To";
            this.deliveredToDataGridViewTextBoxColumn.HeaderText = "Delivered_To";
            this.deliveredToDataGridViewTextBoxColumn.Name = "deliveredToDataGridViewTextBoxColumn";
            this.deliveredToDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deliveryEmailDataGridViewTextBoxColumn
            // 
            this.deliveryEmailDataGridViewTextBoxColumn.DataPropertyName = "DeliveryEmail";
            this.deliveryEmailDataGridViewTextBoxColumn.HeaderText = "DeliveryEmail";
            this.deliveryEmailDataGridViewTextBoxColumn.Name = "deliveryEmailDataGridViewTextBoxColumn";
            this.deliveryEmailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // marketSegmentDataGridViewTextBoxColumn
            // 
            this.marketSegmentDataGridViewTextBoxColumn.DataPropertyName = "MarketSegment";
            this.marketSegmentDataGridViewTextBoxColumn.HeaderText = "MarketSegment";
            this.marketSegmentDataGridViewTextBoxColumn.Name = "marketSegmentDataGridViewTextBoxColumn";
            this.marketSegmentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // repIdDataGridViewTextBoxColumn
            // 
            this.repIdDataGridViewTextBoxColumn.DataPropertyName = "RepId";
            this.repIdDataGridViewTextBoxColumn.HeaderText = "RepId";
            this.repIdDataGridViewTextBoxColumn.Name = "repIdDataGridViewTextBoxColumn";
            this.repIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // processedDataGridViewCheckBoxColumn
            // 
            this.processedDataGridViewCheckBoxColumn.DataPropertyName = "Processed";
            this.processedDataGridViewCheckBoxColumn.HeaderText = "Processed";
            this.processedDataGridViewCheckBoxColumn.Name = "processedDataGridViewCheckBoxColumn";
            this.processedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // receivedOnDateTimeDataGridViewTextBoxColumn
            // 
            this.receivedOnDateTimeDataGridViewTextBoxColumn.DataPropertyName = "ReceivedOnDateTime";
            this.receivedOnDateTimeDataGridViewTextBoxColumn.HeaderText = "ReceivedOnDateTime";
            this.receivedOnDateTimeDataGridViewTextBoxColumn.Name = "receivedOnDateTimeDataGridViewTextBoxColumn";
            this.receivedOnDateTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // processedOnDataGridViewTextBoxColumn
            // 
            this.processedOnDataGridViewTextBoxColumn.DataPropertyName = "ProcessedOn";
            this.processedOnDataGridViewTextBoxColumn.HeaderText = "ProcessedOn";
            this.processedOnDataGridViewTextBoxColumn.Name = "processedOnDataGridViewTextBoxColumn";
            this.processedOnDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // RejectLine
            // 
            this.RejectLine.Location = new System.Drawing.Point(287, 3);
            this.RejectLine.Name = "RejectLine";
            this.RejectLine.Size = new System.Drawing.Size(163, 23);
            this.RejectLine.TabIndex = 5;
            this.RejectLine.Text = "Reject selected line";
            this.RejectLine.UseVisualStyleBackColor = true;
            this.RejectLine.Click += new System.EventHandler(this.RejectLine_Click);
            // 
            // NewAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RejectLine);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "NewAccountForm";
            this.ShowIcon = false;
            this.Text = "New Account applications";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NewAccountForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.newApplicationDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newAccountApplicationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appDbDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appDbDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView newApplicationDataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource appDbDataSetBindingSource;
        private AppDbDataSet appDbDataSet;
        private System.Windows.Forms.BindingSource newAccountApplicationBindingSource;
        private AppDbDataSetTableAdapters.NewAccountApplicationTableAdapter newAccountApplicationTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceListIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn physicalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn physical2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn suburbDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn physical4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gPSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn postalCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telephoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellphoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn faxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn repVisitFreqDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactPersonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deliveredToDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deliveryEmailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn marketSegmentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn repIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn processedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn receivedOnDateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn processedOnDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button RejectLine;
    }
}

