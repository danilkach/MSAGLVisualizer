namespace MSAGL
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.graphPanel = new System.Windows.Forms.Panel();
            this.selectionGroupBox = new System.Windows.Forms.GroupBox();
            this.removeEntityButton = new System.Windows.Forms.Button();
            this.selectedEntityColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedEntityValueChangeButton = new System.Windows.Forms.Button();
            this.selectedEntityValueTextbox = new System.Windows.Forms.TextBox();
            this.selectedEntityValueLabel = new System.Windows.Forms.Label();
            this.entityColorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addNodeButton = new System.Windows.Forms.Button();
            this.addNodeTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.graphAdjustmentGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.clearColorHighlighting = new System.Windows.Forms.Button();
            this.reloadGraph = new System.Windows.Forms.Button();
            this.clearGraphButton = new System.Windows.Forms.Button();
            this.undirectedGraphRadiobutton = new System.Windows.Forms.RadioButton();
            this.directedGraphRadioButton = new System.Windows.Forms.RadioButton();
            this.cancelButton = new System.Windows.Forms.Button();
            this.pickNodeListener = new System.ComponentModel.BackgroundWorker();
            this.textRepresentative = new System.Windows.Forms.TextBox();
            this.algorithmDataGridView = new System.Windows.Forms.DataGridView();
            this.aglorithmColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.executeButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clearTextInfoButton = new System.Windows.Forms.Button();
            this.selectionGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.graphAdjustmentGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.algorithmDataGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphPanel
            // 
            this.graphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphPanel.Location = new System.Drawing.Point(12, 67);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(732, 483);
            this.graphPanel.TabIndex = 0;
            // 
            // selectionGroupBox
            // 
            this.selectionGroupBox.Controls.Add(this.removeEntityButton);
            this.selectionGroupBox.Controls.Add(this.selectedEntityColor);
            this.selectionGroupBox.Controls.Add(this.label1);
            this.selectionGroupBox.Controls.Add(this.selectedEntityValueChangeButton);
            this.selectionGroupBox.Controls.Add(this.selectedEntityValueTextbox);
            this.selectionGroupBox.Controls.Add(this.selectedEntityValueLabel);
            this.selectionGroupBox.Enabled = false;
            this.selectionGroupBox.Location = new System.Drawing.Point(13, 13);
            this.selectionGroupBox.Name = "selectionGroupBox";
            this.selectionGroupBox.Size = new System.Drawing.Size(464, 48);
            this.selectionGroupBox.TabIndex = 1;
            this.selectionGroupBox.TabStop = false;
            // 
            // removeEntityButton
            // 
            this.removeEntityButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.removeEntityButton.Location = new System.Drawing.Point(382, 19);
            this.removeEntityButton.Name = "removeEntityButton";
            this.removeEntityButton.Size = new System.Drawing.Size(75, 20);
            this.removeEntityButton.TabIndex = 5;
            this.removeEntityButton.Text = "Удалить";
            this.removeEntityButton.UseVisualStyleBackColor = true;
            this.removeEntityButton.Click += new System.EventHandler(this.removeEntityButton_Click);
            // 
            // selectedEntityColor
            // 
            this.selectedEntityColor.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.selectedEntityColor.Location = new System.Drawing.Point(296, 19);
            this.selectedEntityColor.Name = "selectedEntityColor";
            this.selectedEntityColor.Size = new System.Drawing.Size(79, 20);
            this.selectedEntityColor.TabIndex = 4;
            this.selectedEntityColor.Text = "Выбрать...";
            this.selectedEntityColor.UseVisualStyleBackColor = true;
            this.selectedEntityColor.Click += new System.EventHandler(this.selectedEntityColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Цвет";
            // 
            // selectedEntityValueChangeButton
            // 
            this.selectedEntityValueChangeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.selectedEntityValueChangeButton.Location = new System.Drawing.Point(173, 19);
            this.selectedEntityValueChangeButton.Name = "selectedEntityValueChangeButton";
            this.selectedEntityValueChangeButton.Size = new System.Drawing.Size(79, 20);
            this.selectedEntityValueChangeButton.TabIndex = 2;
            this.selectedEntityValueChangeButton.Text = "ОК";
            this.selectedEntityValueChangeButton.UseVisualStyleBackColor = true;
            this.selectedEntityValueChangeButton.Click += new System.EventHandler(this.selectedEntityValueChangeButton_Click);
            // 
            // selectedEntityValueTextbox
            // 
            this.selectedEntityValueTextbox.Location = new System.Drawing.Point(67, 19);
            this.selectedEntityValueTextbox.MaxLength = 10;
            this.selectedEntityValueTextbox.Name = "selectedEntityValueTextbox";
            this.selectedEntityValueTextbox.Size = new System.Drawing.Size(100, 20);
            this.selectedEntityValueTextbox.TabIndex = 1;
            this.selectedEntityValueTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.selectedEntityValueTextbox_KeyDown);
            // 
            // selectedEntityValueLabel
            // 
            this.selectedEntityValueLabel.AutoSize = true;
            this.selectedEntityValueLabel.Location = new System.Drawing.Point(6, 22);
            this.selectedEntityValueLabel.Name = "selectedEntityValueLabel";
            this.selectedEntityValueLabel.Size = new System.Drawing.Size(55, 13);
            this.selectedEntityValueLabel.TabIndex = 0;
            this.selectedEntityValueLabel.Text = "Значение";
            // 
            // entityColorDialog
            // 
            this.entityColorDialog.AnyColor = true;
            this.entityColorDialog.Color = System.Drawing.Color.Blue;
            this.entityColorDialog.FullOpen = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addNodeButton);
            this.groupBox1.Controls.Add(this.addNodeTextbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(483, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 48);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавить вершину";
            // 
            // addNodeButton
            // 
            this.addNodeButton.Location = new System.Drawing.Point(173, 19);
            this.addNodeButton.MinimumSize = new System.Drawing.Size(79, 20);
            this.addNodeButton.Name = "addNodeButton";
            this.addNodeButton.Size = new System.Drawing.Size(79, 20);
            this.addNodeButton.TabIndex = 2;
            this.addNodeButton.Text = "ОК";
            this.addNodeButton.UseVisualStyleBackColor = true;
            this.addNodeButton.Click += new System.EventHandler(this.addNodeButton_Click);
            // 
            // addNodeTextbox
            // 
            this.addNodeTextbox.Location = new System.Drawing.Point(67, 19);
            this.addNodeTextbox.MaxLength = 10;
            this.addNodeTextbox.Name = "addNodeTextbox";
            this.addNodeTextbox.Size = new System.Drawing.Size(100, 20);
            this.addNodeTextbox.TabIndex = 1;
            this.addNodeTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addNodeTextbox_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Значение";
            // 
            // stateLabel
            // 
            this.stateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stateLabel.AutoSize = true;
            this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stateLabel.Location = new System.Drawing.Point(8, 553);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(165, 20);
            this.stateLabel.TabIndex = 7;
            this.stateLabel.Text = "Загрузка завершена";
            // 
            // graphAdjustmentGroupBox
            // 
            this.graphAdjustmentGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.graphAdjustmentGroupBox.Controls.Add(this.radioButton1);
            this.graphAdjustmentGroupBox.Controls.Add(this.clearColorHighlighting);
            this.graphAdjustmentGroupBox.Controls.Add(this.reloadGraph);
            this.graphAdjustmentGroupBox.Controls.Add(this.clearGraphButton);
            this.graphAdjustmentGroupBox.Controls.Add(this.undirectedGraphRadiobutton);
            this.graphAdjustmentGroupBox.Controls.Add(this.directedGraphRadioButton);
            this.graphAdjustmentGroupBox.Location = new System.Drawing.Point(751, 13);
            this.graphAdjustmentGroupBox.Name = "graphAdjustmentGroupBox";
            this.graphAdjustmentGroupBox.Size = new System.Drawing.Size(400, 89);
            this.graphAdjustmentGroupBox.TabIndex = 8;
            this.graphAdjustmentGroupBox.TabStop = false;
            this.graphAdjustmentGroupBox.Text = "Граф";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(233, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(64, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Дерево";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // clearColorHighlighting
            // 
            this.clearColorHighlighting.Location = new System.Drawing.Point(7, 54);
            this.clearColorHighlighting.Name = "clearColorHighlighting";
            this.clearColorHighlighting.Size = new System.Drawing.Size(190, 23);
            this.clearColorHighlighting.TabIndex = 4;
            this.clearColorHighlighting.Text = "Очистить выделение";
            this.clearColorHighlighting.UseVisualStyleBackColor = true;
            this.clearColorHighlighting.Click += new System.EventHandler(this.clearColorHighlighting_Click);
            // 
            // reloadGraph
            // 
            this.reloadGraph.Location = new System.Drawing.Point(199, 54);
            this.reloadGraph.Name = "reloadGraph";
            this.reloadGraph.Size = new System.Drawing.Size(190, 23);
            this.reloadGraph.TabIndex = 3;
            this.reloadGraph.Text = "Обновить разметку";
            this.reloadGraph.UseVisualStyleBackColor = true;
            this.reloadGraph.Click += new System.EventHandler(this.reloadGraph_Click);
            // 
            // clearGraphButton
            // 
            this.clearGraphButton.Location = new System.Drawing.Point(303, 21);
            this.clearGraphButton.Name = "clearGraphButton";
            this.clearGraphButton.Size = new System.Drawing.Size(86, 20);
            this.clearGraphButton.TabIndex = 2;
            this.clearGraphButton.Text = "Очистить";
            this.clearGraphButton.UseVisualStyleBackColor = true;
            this.clearGraphButton.Click += new System.EventHandler(this.clearGraphButton_Click);
            // 
            // undirectedGraphRadiobutton
            // 
            this.undirectedGraphRadiobutton.AutoSize = true;
            this.undirectedGraphRadiobutton.Checked = true;
            this.undirectedGraphRadiobutton.Location = new System.Drawing.Point(114, 22);
            this.undirectedGraphRadiobutton.Name = "undirectedGraphRadiobutton";
            this.undirectedGraphRadiobutton.Size = new System.Drawing.Size(113, 17);
            this.undirectedGraphRadiobutton.TabIndex = 1;
            this.undirectedGraphRadiobutton.TabStop = true;
            this.undirectedGraphRadiobutton.Text = "Ненаправленный";
            this.undirectedGraphRadiobutton.UseVisualStyleBackColor = true;
            this.undirectedGraphRadiobutton.CheckedChanged += new System.EventHandler(this.undirectedGraphRadiobutton_CheckedChanged);
            // 
            // directedGraphRadioButton
            // 
            this.directedGraphRadioButton.AutoSize = true;
            this.directedGraphRadioButton.Location = new System.Drawing.Point(7, 21);
            this.directedGraphRadioButton.Name = "directedGraphRadioButton";
            this.directedGraphRadioButton.Size = new System.Drawing.Size(101, 17);
            this.directedGraphRadioButton.TabIndex = 0;
            this.directedGraphRadioButton.TabStop = true;
            this.directedGraphRadioButton.Text = "Направленный";
            this.directedGraphRadioButton.UseVisualStyleBackColor = true;
            this.directedGraphRadioButton.CheckedChanged += new System.EventHandler(this.directedGraphRadioButton_CheckedChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(1075, 553);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // textRepresentative
            // 
            this.textRepresentative.AcceptsReturn = true;
            this.textRepresentative.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textRepresentative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRepresentative.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textRepresentative.Location = new System.Drawing.Point(6, 19);
            this.textRepresentative.Multiline = true;
            this.textRepresentative.Name = "textRepresentative";
            this.textRepresentative.ReadOnly = true;
            this.textRepresentative.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textRepresentative.Size = new System.Drawing.Size(387, 167);
            this.textRepresentative.TabIndex = 11;
            // 
            // algorithmDataGridView
            // 
            this.algorithmDataGridView.AllowUserToAddRows = false;
            this.algorithmDataGridView.AllowUserToDeleteRows = false;
            this.algorithmDataGridView.AllowUserToResizeColumns = false;
            this.algorithmDataGridView.AllowUserToResizeRows = false;
            this.algorithmDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.algorithmDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aglorithmColumn,
            this.executeButtonColumn});
            this.algorithmDataGridView.Location = new System.Drawing.Point(751, 108);
            this.algorithmDataGridView.MultiSelect = false;
            this.algorithmDataGridView.Name = "algorithmDataGridView";
            this.algorithmDataGridView.ReadOnly = true;
            this.algorithmDataGridView.RowHeadersVisible = false;
            this.algorithmDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.algorithmDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.algorithmDataGridView.Size = new System.Drawing.Size(399, 215);
            this.algorithmDataGridView.TabIndex = 0;
            this.algorithmDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.algorithmDataGridView_CellContentClick);
            // 
            // aglorithmColumn
            // 
            this.aglorithmColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.aglorithmColumn.HeaderText = "Алгоритм";
            this.aglorithmColumn.Name = "aglorithmColumn";
            this.aglorithmColumn.ReadOnly = true;
            this.aglorithmColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.aglorithmColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // executeButtonColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "Выполнить";
            this.executeButtonColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.executeButtonColumn.HeaderText = "";
            this.executeButtonColumn.Name = "executeButtonColumn";
            this.executeButtonColumn.ReadOnly = true;
            this.executeButtonColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.executeButtonColumn.Text = "Выполнить";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.clearTextInfoButton);
            this.groupBox3.Controls.Add(this.textRepresentative);
            this.groupBox3.Location = new System.Drawing.Point(751, 329);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 221);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Информация";
            // 
            // clearTextInfoButton
            // 
            this.clearTextInfoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearTextInfoButton.Location = new System.Drawing.Point(6, 192);
            this.clearTextInfoButton.Name = "clearTextInfoButton";
            this.clearTextInfoButton.Size = new System.Drawing.Size(387, 23);
            this.clearTextInfoButton.TabIndex = 12;
            this.clearTextInfoButton.Text = "Очистить";
            this.clearTextInfoButton.UseVisualStyleBackColor = true;
            this.clearTextInfoButton.Click += new System.EventHandler(this.clearTextInfoButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 580);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.algorithmDataGridView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.graphAdjustmentGroupBox);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.selectionGroupBox);
            this.Controls.Add(this.graphPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1179, 619);
            this.Name = "Form1";
            this.Text = " Графы и алгоритмы";
            this.selectionGroupBox.ResumeLayout(false);
            this.selectionGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.graphAdjustmentGroupBox.ResumeLayout(false);
            this.graphAdjustmentGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.algorithmDataGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel graphPanel;
        private System.Windows.Forms.GroupBox selectionGroupBox;
        private System.Windows.Forms.Button selectedEntityValueChangeButton;
        private System.Windows.Forms.TextBox selectedEntityValueTextbox;
        private System.Windows.Forms.Label selectedEntityValueLabel;
        private System.Windows.Forms.ColorDialog entityColorDialog;
        private System.Windows.Forms.Button selectedEntityColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button removeEntityButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addNodeButton;
        private System.Windows.Forms.TextBox addNodeTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.GroupBox graphAdjustmentGroupBox;
        private System.Windows.Forms.RadioButton undirectedGraphRadiobutton;
        private System.Windows.Forms.RadioButton directedGraphRadioButton;
        private System.Windows.Forms.Button clearGraphButton;
        private System.Windows.Forms.Button reloadGraph;
        private System.Windows.Forms.Button cancelButton;
        private System.ComponentModel.BackgroundWorker pickNodeListener;
        private System.Windows.Forms.TextBox textRepresentative;
        private System.Windows.Forms.DataGridView algorithmDataGridView;
        private System.Windows.Forms.Button clearColorHighlighting;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button clearTextInfoButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn aglorithmColumn;
        private System.Windows.Forms.DataGridViewButtonColumn executeButtonColumn;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

