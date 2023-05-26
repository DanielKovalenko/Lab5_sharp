
namespace Lab5_threads
{
    partial class MainForm
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
            this.btnToggleShop = new System.Windows.Forms.Button();
            this.btnAddProducts = new System.Windows.Forms.Button();
            this.labelStoreBalance = new System.Windows.Forms.Label();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // btnToggleShop
            // 
            this.btnToggleShop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnToggleShop.Location = new System.Drawing.Point(535, 113);
            this.btnToggleShop.Name = "btnToggleShop";
            this.btnToggleShop.Size = new System.Drawing.Size(154, 82);
            this.btnToggleShop.TabIndex = 0;
            this.btnToggleShop.Text = "Перемикач режиму роботи магазину";
            this.btnToggleShop.UseVisualStyleBackColor = true;
            this.btnToggleShop.Click += new System.EventHandler(this.btnToggleShop_Click);
            // 
            // btnAddProducts
            // 
            this.btnAddProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAddProducts.Location = new System.Drawing.Point(707, 113);
            this.btnAddProducts.Name = "btnAddProducts";
            this.btnAddProducts.Size = new System.Drawing.Size(154, 82);
            this.btnAddProducts.TabIndex = 1;
            this.btnAddProducts.Text = "Додавання продуктів до магазину";
            this.btnAddProducts.UseVisualStyleBackColor = true;
            this.btnAddProducts.Click += new System.EventHandler(this.btnAddProducts_Click);
            // 
            // labelStoreBalance
            // 
            this.labelStoreBalance.AutoSize = true;
            this.labelStoreBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.labelStoreBalance.Location = new System.Drawing.Point(530, 47);
            this.labelStoreBalance.Name = "labelStoreBalance";
            this.labelStoreBalance.Size = new System.Drawing.Size(183, 25);
            this.labelStoreBalance.TabIndex = 2;
            this.labelStoreBalance.Text = "Рахунок магазину:";
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.Location = new System.Drawing.Point(526, 259);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(369, 199);
            this.listBoxLogs.TabIndex = 3;
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Location = new System.Drawing.Point(12, 21);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(443, 437);
            this.dataGridViewProducts.TabIndex = 4;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(907, 500);
            this.Controls.Add(this.dataGridViewProducts);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.labelStoreBalance);
            this.Controls.Add(this.btnAddProducts);
            this.Controls.Add(this.btnToggleShop);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnToggleShop;
        private System.Windows.Forms.Button btnAddProducts;
        private System.Windows.Forms.Label labelStoreBalance;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
    }
}