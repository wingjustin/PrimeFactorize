namespace PrimeFactorize
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            factorBtn = new Button();
            resultDialogBox = new RichTextBox();
            numericUpDown1 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 42);
            label1.Name = "label1";
            label1.Size = new Size(81, 23);
            label1.TabIndex = 0;
            label1.Text = "Number";
            // 
            // factorBtn
            // 
            factorBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            factorBtn.Location = new Point(577, 36);
            factorBtn.Name = "factorBtn";
            factorBtn.Size = new Size(168, 34);
            factorBtn.TabIndex = 2;
            factorBtn.Text = "Factorize";
            factorBtn.UseVisualStyleBackColor = true;
            factorBtn.Click += factorBtn_Click;
            // 
            // resultDialogBox
            // 
            resultDialogBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            resultDialogBox.BackColor = SystemColors.Info;
            resultDialogBox.DetectUrls = false;
            resultDialogBox.ForeColor = SystemColors.InfoText;
            resultDialogBox.Location = new Point(44, 94);
            resultDialogBox.Name = "resultDialogBox";
            resultDialogBox.ReadOnly = true;
            resultDialogBox.Size = new Size(701, 251);
            resultDialogBox.TabIndex = 3;
            resultDialogBox.Text = "";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numericUpDown1.Location = new Point(149, 40);
            numericUpDown1.Maximum = new decimal(new int[] { -159383553, 46653770, 5421, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(397, 30);
            numericUpDown1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(782, 386);
            Controls.Add(numericUpDown1);
            Controls.Add(resultDialogBox);
            Controls.Add(factorBtn);
            Controls.Add(label1);
            MinimumSize = new Size(604, 442);
            Name = "Form1";
            Text = "Prime Factorization";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button factorBtn;
        private RichTextBox resultDialogBox;
        private NumericUpDown numericUpDown1;
    }
}
