namespace GoolagScanner
{
    partial class SearchDialog
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
            this.Term = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NameCheckBox = new System.Windows.Forms.CheckBox();
            this.QueryCheckBox = new System.Windows.Forms.CheckBox();
            this.CommentCheckBox = new System.Windows.Forms.CheckBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Term
            // 
            this.Term.Location = new System.Drawing.Point(73, 12);
            this.Term.Name = "Term";
            this.Term.Size = new System.Drawing.Size(200, 20);
            this.Term.TabIndex = 0;
            this.Term.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TermKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search for:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search in:";
            // 
            // NameCheckBox
            // 
            this.NameCheckBox.AutoSize = true;
            this.NameCheckBox.Location = new System.Drawing.Point(73, 38);
            this.NameCheckBox.Name = "NameCheckBox";
            this.NameCheckBox.Size = new System.Drawing.Size(80, 17);
            this.NameCheckBox.TabIndex = 3;
            this.NameCheckBox.Text = "Dork Name";
            this.NameCheckBox.UseVisualStyleBackColor = true;
            // 
            // QueryCheckBox
            // 
            this.QueryCheckBox.AutoSize = true;
            this.QueryCheckBox.Location = new System.Drawing.Point(73, 61);
            this.QueryCheckBox.Name = "QueryCheckBox";
            this.QueryCheckBox.Size = new System.Drawing.Size(54, 17);
            this.QueryCheckBox.TabIndex = 4;
            this.QueryCheckBox.Text = "Query";
            this.QueryCheckBox.UseVisualStyleBackColor = true;
            // 
            // CommentCheckBox
            // 
            this.CommentCheckBox.AutoSize = true;
            this.CommentCheckBox.Location = new System.Drawing.Point(73, 84);
            this.CommentCheckBox.Name = "CommentCheckBox";
            this.CommentCheckBox.Size = new System.Drawing.Size(70, 17);
            this.CommentCheckBox.TabIndex = 5;
            this.CommentCheckBox.Text = "Comment";
            this.CommentCheckBox.UseVisualStyleBackColor = true;
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(288, 12);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(74, 23);
            this.FindButton.TabIndex = 1;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(288, 43);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(74, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 107);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.CommentCheckBox);
            this.Controls.Add(this.QueryCheckBox);
            this.Controls.Add(this.NameCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Term);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SearchDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Dork";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Term;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox NameCheckBox;
        private System.Windows.Forms.CheckBox QueryCheckBox;
        private System.Windows.Forms.CheckBox CommentCheckBox;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.Button Cancel;
    }
}