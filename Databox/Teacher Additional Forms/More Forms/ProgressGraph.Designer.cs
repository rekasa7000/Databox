
namespace Databox.Teacher_Additional_Forms.More_Forms
{
    partial class ProgressGraph
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblStudent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new FontAwesome.Sharp.IconButton();
            this.panelBargraphs = new System.Windows.Forms.Panel();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtComment = new MaterialSkin.Controls.MaterialMaskedTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLetterGrade = new MaterialSkin.Controls.MaterialLabel();
            this.lblPercentageCorrectAnswers = new MaterialSkin.Controls.MaterialLabel();
            this.lblLowestScore = new MaterialSkin.Controls.MaterialLabel();
            this.lblHighestScore = new MaterialSkin.Controls.MaterialLabel();
            this.lblWeightedAverage = new MaterialSkin.Controls.MaterialLabel();
            this.lblCumulativeScore = new MaterialSkin.Controls.MaterialLabel();
            this.lblAverageScore = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.Metrics = new MaterialSkin.Controls.MaterialLabel();
            this.panelPiechart = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.chartPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.panelTop.Controls.Add(this.panelTitle);
            this.panelTop.Controls.Add(this.btnCancel);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(990, 50);
            this.panelTop.TabIndex = 58;
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.lblStudent);
            this.panelTitle.Controls.Add(this.label1);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTitle.Location = new System.Drawing.Point(104, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(10);
            this.panelTitle.Size = new System.Drawing.Size(886, 50);
            this.panelTitle.TabIndex = 3;
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStudent.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudent.ForeColor = System.Drawing.Color.White;
            this.lblStudent.Location = new System.Drawing.Point(306, 10);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(0, 30);
            this.lblStudent.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Academic  Progress of student";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnCancel.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.btnCancel.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancel.IconSize = 30;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 50);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Back";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelBargraphs
            // 
            this.panelBargraphs.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBargraphs.Location = new System.Drawing.Point(0, 0);
            this.panelBargraphs.Name = "panelBargraphs";
            this.panelBargraphs.Size = new System.Drawing.Size(486, 953);
            this.panelBargraphs.TabIndex = 0;
            // 
            // chartPanel
            // 
            this.chartPanel.AutoScroll = true;
            this.chartPanel.AutoSize = true;
            this.chartPanel.Controls.Add(this.panel1);
            this.chartPanel.Controls.Add(this.panelPiechart);
            this.chartPanel.Controls.Add(this.panelBargraphs);
            this.chartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPanel.Location = new System.Drawing.Point(0, 50);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(990, 470);
            this.chartPanel.TabIndex = 59;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txtComment);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(486, 461);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(487, 492);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRelease);
            this.panel3.Controls.Add(this.btnView);
            this.panel3.Controls.Add(this.btnGenerate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(5, 281);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(100, 25, 100, 10);
            this.panel3.Size = new System.Drawing.Size(477, 206);
            this.panel3.TabIndex = 13;
            // 
            // btnRelease
            // 
            this.btnRelease.AutoSize = true;
            this.btnRelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnRelease.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRelease.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.ForeColor = System.Drawing.Color.White;
            this.btnRelease.Location = new System.Drawing.Point(100, 125);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(277, 50);
            this.btnRelease.TabIndex = 17;
            this.btnRelease.Text = "Release Report";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnView
            // 
            this.btnView.AutoSize = true;
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnView.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnView.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(100, 75);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(277, 50);
            this.btnView.TabIndex = 16;
            this.btnView.Text = "View Report";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.AutoSize = true;
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerate.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(100, 25);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(277, 50);
            this.btnGenerate.TabIndex = 15;
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtComment
            // 
            this.txtComment.AllowPromptAsInput = true;
            this.txtComment.AnimateReadOnly = false;
            this.txtComment.AsciiOnly = false;
            this.txtComment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtComment.BeepOnError = false;
            this.txtComment.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.txtComment.Depth = 0;
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtComment.HidePromptOnLeave = false;
            this.txtComment.HideSelection = true;
            this.txtComment.Hint = "Comments and Recommendation";
            this.txtComment.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.txtComment.LeadingIcon = null;
            this.txtComment.Location = new System.Drawing.Point(5, 233);
            this.txtComment.Mask = "";
            this.txtComment.MaxLength = 32767;
            this.txtComment.MouseState = MaterialSkin.MouseState.OUT;
            this.txtComment.Name = "txtComment";
            this.txtComment.PasswordChar = '\0';
            this.txtComment.PrefixSuffixText = null;
            this.txtComment.PromptChar = '_';
            this.txtComment.ReadOnly = false;
            this.txtComment.RejectInputOnFirstFailure = false;
            this.txtComment.ResetOnPrompt = true;
            this.txtComment.ResetOnSpace = true;
            this.txtComment.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtComment.SelectedText = "";
            this.txtComment.SelectionLength = 0;
            this.txtComment.SelectionStart = 0;
            this.txtComment.ShortcutsEnabled = true;
            this.txtComment.Size = new System.Drawing.Size(477, 48);
            this.txtComment.SkipLiterals = true;
            this.txtComment.TabIndex = 12;
            this.txtComment.TabStop = false;
            this.txtComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtComment.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.txtComment.TrailingIcon = null;
            this.txtComment.UseSystemPasswordChar = false;
            this.txtComment.ValidatingType = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblLetterGrade);
            this.panel2.Controls.Add(this.lblPercentageCorrectAnswers);
            this.panel2.Controls.Add(this.lblLowestScore);
            this.panel2.Controls.Add(this.lblHighestScore);
            this.panel2.Controls.Add(this.lblWeightedAverage);
            this.panel2.Controls.Add(this.lblCumulativeScore);
            this.panel2.Controls.Add(this.lblAverageScore);
            this.panel2.Controls.Add(this.materialLabel1);
            this.panel2.Controls.Add(this.Metrics);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(477, 228);
            this.panel2.TabIndex = 11;
            // 
            // lblLetterGrade
            // 
            this.lblLetterGrade.AutoSize = true;
            this.lblLetterGrade.Depth = 0;
            this.lblLetterGrade.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLetterGrade.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblLetterGrade.Location = new System.Drawing.Point(10, 172);
            this.lblLetterGrade.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblLetterGrade.Name = "lblLetterGrade";
            this.lblLetterGrade.Size = new System.Drawing.Size(5, 19);
            this.lblLetterGrade.TabIndex = 18;
            this.lblLetterGrade.Text = " ";
            // 
            // lblPercentageCorrectAnswers
            // 
            this.lblPercentageCorrectAnswers.AutoSize = true;
            this.lblPercentageCorrectAnswers.Depth = 0;
            this.lblPercentageCorrectAnswers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPercentageCorrectAnswers.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPercentageCorrectAnswers.Location = new System.Drawing.Point(10, 153);
            this.lblPercentageCorrectAnswers.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPercentageCorrectAnswers.Name = "lblPercentageCorrectAnswers";
            this.lblPercentageCorrectAnswers.Size = new System.Drawing.Size(5, 19);
            this.lblPercentageCorrectAnswers.TabIndex = 16;
            this.lblPercentageCorrectAnswers.Text = " ";
            // 
            // lblLowestScore
            // 
            this.lblLowestScore.AutoSize = true;
            this.lblLowestScore.Depth = 0;
            this.lblLowestScore.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLowestScore.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblLowestScore.Location = new System.Drawing.Point(10, 134);
            this.lblLowestScore.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblLowestScore.Name = "lblLowestScore";
            this.lblLowestScore.Size = new System.Drawing.Size(5, 19);
            this.lblLowestScore.TabIndex = 15;
            this.lblLowestScore.Text = " ";
            // 
            // lblHighestScore
            // 
            this.lblHighestScore.AutoSize = true;
            this.lblHighestScore.Depth = 0;
            this.lblHighestScore.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHighestScore.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblHighestScore.Location = new System.Drawing.Point(10, 115);
            this.lblHighestScore.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblHighestScore.Name = "lblHighestScore";
            this.lblHighestScore.Size = new System.Drawing.Size(5, 19);
            this.lblHighestScore.TabIndex = 14;
            this.lblHighestScore.Text = " ";
            // 
            // lblWeightedAverage
            // 
            this.lblWeightedAverage.AutoSize = true;
            this.lblWeightedAverage.Depth = 0;
            this.lblWeightedAverage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWeightedAverage.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblWeightedAverage.Location = new System.Drawing.Point(10, 96);
            this.lblWeightedAverage.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblWeightedAverage.Name = "lblWeightedAverage";
            this.lblWeightedAverage.Size = new System.Drawing.Size(5, 19);
            this.lblWeightedAverage.TabIndex = 13;
            this.lblWeightedAverage.Text = " ";
            // 
            // lblCumulativeScore
            // 
            this.lblCumulativeScore.AutoSize = true;
            this.lblCumulativeScore.Depth = 0;
            this.lblCumulativeScore.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCumulativeScore.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCumulativeScore.Location = new System.Drawing.Point(10, 77);
            this.lblCumulativeScore.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCumulativeScore.Name = "lblCumulativeScore";
            this.lblCumulativeScore.Size = new System.Drawing.Size(5, 19);
            this.lblCumulativeScore.TabIndex = 12;
            this.lblCumulativeScore.Text = " ";
            // 
            // lblAverageScore
            // 
            this.lblAverageScore.AutoSize = true;
            this.lblAverageScore.Depth = 0;
            this.lblAverageScore.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAverageScore.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAverageScore.Location = new System.Drawing.Point(10, 58);
            this.lblAverageScore.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAverageScore.Name = "lblAverageScore";
            this.lblAverageScore.Size = new System.Drawing.Size(5, 19);
            this.lblAverageScore.TabIndex = 11;
            this.lblAverageScore.Text = " ";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(10, 39);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(5, 19);
            this.materialLabel1.TabIndex = 20;
            this.materialLabel1.Text = " ";
            // 
            // Metrics
            // 
            this.Metrics.AutoSize = true;
            this.Metrics.Depth = 0;
            this.Metrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.Metrics.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.Metrics.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.Metrics.Location = new System.Drawing.Point(10, 10);
            this.Metrics.MouseState = MaterialSkin.MouseState.HOVER;
            this.Metrics.Name = "Metrics";
            this.Metrics.Size = new System.Drawing.Size(156, 29);
            this.Metrics.TabIndex = 19;
            this.Metrics.Text = " Score Metrics";
            // 
            // panelPiechart
            // 
            this.panelPiechart.AutoScroll = true;
            this.panelPiechart.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPiechart.Location = new System.Drawing.Point(486, 0);
            this.panelPiechart.Name = "panelPiechart";
            this.panelPiechart.Size = new System.Drawing.Size(487, 461);
            this.panelPiechart.TabIndex = 2;
            // 
            // ProgressGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 520);
            this.Controls.Add(this.chartPanel);
            this.Controls.Add(this.panelTop);
            this.Name = "ProgressGraph";
            this.Text = "ProgressGraph";
            this.Load += new System.EventHandler(this.ProgressGraph_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.chartPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnCancel;
        private System.Windows.Forms.Panel panelBargraphs;
        private System.Windows.Forms.Panel chartPanel;
        private System.Windows.Forms.Panel panelPiechart;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtComment;
        private System.Windows.Forms.Panel panel2;
        private MaterialSkin.Controls.MaterialLabel lblLetterGrade;
        private MaterialSkin.Controls.MaterialLabel lblPercentageCorrectAnswers;
        private MaterialSkin.Controls.MaterialLabel lblLowestScore;
        private MaterialSkin.Controls.MaterialLabel lblHighestScore;
        private MaterialSkin.Controls.MaterialLabel lblWeightedAverage;
        private MaterialSkin.Controls.MaterialLabel lblCumulativeScore;
        private MaterialSkin.Controls.MaterialLabel lblAverageScore;
        private MaterialSkin.Controls.MaterialLabel Metrics;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnRelease;
    }
}