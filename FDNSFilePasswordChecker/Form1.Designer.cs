namespace FDNSFilePasswordChecker
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 実行ボタン
        /// </summary>
        private System.Windows.Forms.Button btnSubmit;

        /// <summary>
        /// 選択フォルダのラベル
        /// </summary>
        private System.Windows.Forms.Label lblSourceFolder;

        /// <summary>
        /// 選択フォルダのテキストボックス
        /// </summary>
        private System.Windows.Forms.TextBox txtSourceFolder;

        /// <summary>
        /// 選択フォルダの参照ボタン
        /// </summary>
        private System.Windows.Forms.Button btnBrowseSource;

        /// <summary>
        /// ログ出力先のラベル
        /// </summary>
        private System.Windows.Forms.Label lblOutputFolder;

        /// <summary>
        /// ログ出力先のテキストボックス
        /// </summary>
        private System.Windows.Forms.TextBox txtOutputFolder;

        /// <summary>
        /// ログ出力先の参照ボタン
        /// </summary>
        private System.Windows.Forms.Button btnBrowseOutput;

        /// <summary>
        /// ログ出力先2（PDF）のラベル
        /// </summary>
        private System.Windows.Forms.Label lblOutputFolder2;

        /// <summary>
        /// ログ出力先2（PDF）のテキストボックス
        /// </summary>
        private System.Windows.Forms.TextBox txtOutputFolder2;

        /// <summary>
        /// ログ出力先2（PDF）の参照ボタン
        /// </summary>
        private System.Windows.Forms.Button btnBrowseOutput2;

        /// <summary>
        /// 複写先フォルダのラベル
        /// </summary>
        private System.Windows.Forms.Label lblCopyDestination;

        /// <summary>
        /// 複写先フォルダのテキストボックス
        /// </summary>
        private System.Windows.Forms.TextBox txtCopyDestination;

        /// <summary>
        /// 複写先フォルダの参照ボタン
        /// </summary>
        private System.Windows.Forms.Button btnBrowseCopyDestination;



        /// <summary>
        /// 結果を出力しないチェックボックス
        /// </summary>
        private System.Windows.Forms.CheckBox chkNoOutput;

        /// <summary>
        /// コメントを出力しないチェックボックス
        /// </summary>
        private System.Windows.Forms.CheckBox chkNoComment;

        /// <summary>
        /// 出力結果の文字をダブルクォーテーションで囲むチェックボックス
        /// </summary>
        private System.Windows.Forms.CheckBox chkDoubleQuote;

        /// <summary>
        /// タイトルラベル
        /// </summary>
        private System.Windows.Forms.Label lblTitle;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblSourceFolder = new System.Windows.Forms.Label();
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.lblOutputFolder2 = new System.Windows.Forms.Label();
            this.txtOutputFolder2 = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput2 = new System.Windows.Forms.Button();
            this.lblCopyDestination = new System.Windows.Forms.Label();
            this.txtCopyDestination = new System.Windows.Forms.TextBox();
            this.btnBrowseCopyDestination = new System.Windows.Forms.Button();

            this.chkNoOutput = new System.Windows.Forms.CheckBox();
            this.chkNoComment = new System.Windows.Forms.CheckBox();
            this.chkDoubleQuote = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(760, 30);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "PasswordChecker";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSourceFolder
            // 
            this.lblSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSourceFolder.AutoSize = true;
            this.lblSourceFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSourceFolder.Location = new System.Drawing.Point(20, 80);
            this.lblSourceFolder.Name = "lblSourceFolder";
            this.lblSourceFolder.Size = new System.Drawing.Size(65, 12);
            this.lblSourceFolder.TabIndex = 1;
            this.lblSourceFolder.Text = "選択フォルダ：";
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourceFolder.Location = new System.Drawing.Point(150, 100);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Size = new System.Drawing.Size(550, 25);
            this.txtSourceFolder.TabIndex = 2;
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBrowseSource.FlatAppearance.BorderSize = 0;
            this.btnBrowseSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseSource.ForeColor = System.Drawing.Color.White;
            this.btnBrowseSource.Location = new System.Drawing.Point(20, 98);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(120, 29);
            this.btnBrowseSource.TabIndex = 3;
            this.btnBrowseSource.Text = "参照";
            this.btnBrowseSource.UseVisualStyleBackColor = false;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOutputFolder.Location = new System.Drawing.Point(20, 140);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(65, 12);
            this.lblOutputFolder.TabIndex = 4;
            this.lblOutputFolder.Text = "ログ出力先1（office）：";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder.Location = new System.Drawing.Point(150, 160);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(550, 25);
            this.txtOutputFolder.TabIndex = 5;
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBrowseOutput.FlatAppearance.BorderSize = 0;
            this.btnBrowseOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseOutput.ForeColor = System.Drawing.Color.White;
            this.btnBrowseOutput.Location = new System.Drawing.Point(20, 158);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(120, 29);
            this.btnBrowseOutput.TabIndex = 6;
            this.btnBrowseOutput.Text = "ファイル指定";
            this.btnBrowseOutput.UseVisualStyleBackColor = false;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // lblOutputFolder2
            // 
            this.lblOutputFolder2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputFolder2.AutoSize = true;
            this.lblOutputFolder2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputFolder2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOutputFolder2.Location = new System.Drawing.Point(20, 200);
            this.lblOutputFolder2.Name = "lblOutputFolder2";
            this.lblOutputFolder2.Size = new System.Drawing.Size(65, 12);
            this.lblOutputFolder2.TabIndex = 7;
            this.lblOutputFolder2.Text = "ログ出力先2（PDF）：";
            // 
            // txtOutputFolder2
            // 
            this.txtOutputFolder2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder2.Location = new System.Drawing.Point(150, 220);
            this.txtOutputFolder2.Name = "txtOutputFolder2";
            this.txtOutputFolder2.Size = new System.Drawing.Size(550, 25);
            this.txtOutputFolder2.TabIndex = 8;
            // 
            // btnBrowseOutput2
            // 
            this.btnBrowseOutput2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseOutput2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBrowseOutput2.FlatAppearance.BorderSize = 0;
            this.btnBrowseOutput2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseOutput2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseOutput2.ForeColor = System.Drawing.Color.White;
            this.btnBrowseOutput2.Location = new System.Drawing.Point(20, 218);
            this.btnBrowseOutput2.Name = "btnBrowseOutput2";
            this.btnBrowseOutput2.Size = new System.Drawing.Size(120, 29);
            this.btnBrowseOutput2.TabIndex = 9;
            this.btnBrowseOutput2.Text = "ファイル指定";
            this.btnBrowseOutput2.UseVisualStyleBackColor = false;
            this.btnBrowseOutput2.Click += new System.EventHandler(this.btnBrowseOutput2_Click);
            // 
            // lblCopyDestination
            // 
            this.lblCopyDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopyDestination.AutoSize = true;
            this.lblCopyDestination.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyDestination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCopyDestination.Location = new System.Drawing.Point(20, 260);
            this.lblCopyDestination.Name = "lblCopyDestination";
            this.lblCopyDestination.Size = new System.Drawing.Size(65, 12);
            this.lblCopyDestination.TabIndex = 10;
            this.lblCopyDestination.Text = "複写先フォルダ：";
            // 
            // txtCopyDestination
            // 
            this.txtCopyDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCopyDestination.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopyDestination.Location = new System.Drawing.Point(150, 280);
            this.txtCopyDestination.Name = "txtCopyDestination";
            this.txtCopyDestination.Size = new System.Drawing.Size(550, 25);
            this.txtCopyDestination.TabIndex = 11;
            // 
            // btnBrowseCopyDestination
            // 
            this.btnBrowseCopyDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseCopyDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBrowseCopyDestination.FlatAppearance.BorderSize = 0;
            this.btnBrowseCopyDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCopyDestination.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseCopyDestination.ForeColor = System.Drawing.Color.White;
            this.btnBrowseCopyDestination.Location = new System.Drawing.Point(20, 278);
            this.btnBrowseCopyDestination.Name = "btnBrowseCopyDestination";
            this.btnBrowseCopyDestination.Size = new System.Drawing.Size(120, 29);
            this.btnBrowseCopyDestination.TabIndex = 12;
            this.btnBrowseCopyDestination.Text = "参照";
            this.btnBrowseCopyDestination.UseVisualStyleBackColor = false;
            this.btnBrowseCopyDestination.Click += new System.EventHandler(this.btnBrowseCopyDestination_Click);
            // 

            // 
            // chkNoOutput
            // 
            this.chkNoOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkNoOutput.AutoSize = true;
            this.chkNoOutput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNoOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkNoOutput.Location = new System.Drawing.Point(20, 320);
            this.chkNoOutput.Name = "chkNoOutput";
            this.chkNoOutput.Size = new System.Drawing.Size(180, 16);
            this.chkNoOutput.TabIndex = 14;
            this.chkNoOutput.Text = "結果を出力しない";
            this.chkNoOutput.UseVisualStyleBackColor = true;
            // 
            // chkNoComment
            // 
            this.chkNoComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkNoComment.AutoSize = true;
            this.chkNoComment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNoComment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkNoComment.Location = new System.Drawing.Point(20, 350);
            this.chkNoComment.Name = "chkNoComment";
            this.chkNoComment.Size = new System.Drawing.Size(180, 16);
            this.chkNoComment.TabIndex = 15;
            this.chkNoComment.Text = "コメントを出力しない";
            this.chkNoComment.UseVisualStyleBackColor = true;
            // 
            // chkDoubleQuote
            // 
            this.chkDoubleQuote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDoubleQuote.AutoSize = true;
            this.chkDoubleQuote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDoubleQuote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoubleQuote.Location = new System.Drawing.Point(20, 380);
            this.chkDoubleQuote.Name = "chkDoubleQuote";
            this.chkDoubleQuote.Size = new System.Drawing.Size(280, 16);
            this.chkDoubleQuote.TabIndex = 16;
            this.chkDoubleQuote.Text = "出力結果の文字をダブルクォーテーションで囲む";
            this.chkDoubleQuote.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(340, 420);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 40);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "実行";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.chkNoOutput);
            this.Controls.Add(this.chkNoComment);
            this.Controls.Add(this.chkDoubleQuote);
            this.Controls.Add(this.btnBrowseCopyDestination);
            this.Controls.Add(this.txtCopyDestination);
            this.Controls.Add(this.lblCopyDestination);
            this.Controls.Add(this.btnBrowseOutput2);
            this.Controls.Add(this.txtOutputFolder2);
            this.Controls.Add(this.lblOutputFolder2);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.btnBrowseSource);
            this.Controls.Add(this.txtSourceFolder);
            this.Controls.Add(this.lblSourceFolder);
            this.Controls.Add(this.btnSubmit);
            this.MinimumSize = new System.Drawing.Size(600, 520);
            this.Name = "Form1";
            this.Text = "PasswordChecker";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

