namespace SimpleNFC
{
    partial class Form1
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
            this.getUidBtn = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.readBlockBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.writeBtn = new System.Windows.Forms.Button();
            this.readAllSectorBtn = new System.Windows.Forms.Button();
            this.numericReadSector = new System.Windows.Forms.NumericUpDown();
            this.numericWriteSector = new System.Windows.Forms.NumericUpDown();
            this.turnOffBuzzerBtn = new System.Windows.Forms.Button();
            this.turnOnBuzzerBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNrp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.readBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericReadSector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWriteSector)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // getUidBtn
            // 
            this.getUidBtn.Location = new System.Drawing.Point(16, 29);
            this.getUidBtn.Name = "getUidBtn";
            this.getUidBtn.Size = new System.Drawing.Size(75, 23);
            this.getUidBtn.TabIndex = 0;
            this.getUidBtn.Text = "Get UID";
            this.getUidBtn.UseVisualStyleBackColor = true;
            this.getUidBtn.Click += new System.EventHandler(this.getUidBtn_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(384, 61);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(231, 508);
            this.messageBox.TabIndex = 3;
            this.messageBox.Text = "";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(384, 599);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 4;
            this.clearBtn.Text = "Clear Log";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // readBlockBtn
            // 
            this.readBlockBtn.Location = new System.Drawing.Point(111, 89);
            this.readBlockBtn.Name = "readBlockBtn";
            this.readBlockBtn.Size = new System.Drawing.Size(75, 23);
            this.readBlockBtn.TabIndex = 6;
            this.readBlockBtn.Text = "Read";
            this.readBlockBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.readBlockBtn.UseVisualStyleBackColor = true;
            this.readBlockBtn.Click += new System.EventHandler(this.readBlockBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Block number :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Input text :";
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(16, 52);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(126, 20);
            this.textBoxInput.TabIndex = 9;
            // 
            // writeBtn
            // 
            this.writeBtn.Location = new System.Drawing.Point(208, 50);
            this.writeBtn.Name = "writeBtn";
            this.writeBtn.Size = new System.Drawing.Size(75, 23);
            this.writeBtn.TabIndex = 10;
            this.writeBtn.Text = "Write";
            this.writeBtn.UseVisualStyleBackColor = true;
            this.writeBtn.Click += new System.EventHandler(this.writeBtn_Click);
            // 
            // readAllSectorBtn
            // 
            this.readAllSectorBtn.Location = new System.Drawing.Point(192, 89);
            this.readAllSectorBtn.Name = "readAllSectorBtn";
            this.readAllSectorBtn.Size = new System.Drawing.Size(92, 23);
            this.readAllSectorBtn.TabIndex = 11;
            this.readAllSectorBtn.Text = "Read All Block";
            this.readAllSectorBtn.UseVisualStyleBackColor = true;
            this.readAllSectorBtn.Click += new System.EventHandler(this.readAllSectorBtn_Click);
            // 
            // numericReadSector
            // 
            this.numericReadSector.Location = new System.Drawing.Point(16, 91);
            this.numericReadSector.Name = "numericReadSector";
            this.numericReadSector.Size = new System.Drawing.Size(89, 20);
            this.numericReadSector.TabIndex = 12;
            this.numericReadSector.ValueChanged += new System.EventHandler(this.numericReadSector_ValueChanged);
            // 
            // numericWriteSector
            // 
            this.numericWriteSector.Location = new System.Drawing.Point(148, 52);
            this.numericWriteSector.Name = "numericWriteSector";
            this.numericWriteSector.Size = new System.Drawing.Size(54, 20);
            this.numericWriteSector.TabIndex = 13;
            this.numericWriteSector.ValueChanged += new System.EventHandler(this.numericWriteSector_ValueChanged);
            // 
            // turnOffBuzzerBtn
            // 
            this.turnOffBuzzerBtn.Location = new System.Drawing.Point(16, 34);
            this.turnOffBuzzerBtn.Name = "turnOffBuzzerBtn";
            this.turnOffBuzzerBtn.Size = new System.Drawing.Size(89, 23);
            this.turnOffBuzzerBtn.TabIndex = 14;
            this.turnOffBuzzerBtn.Text = "Turn Off Buzzer";
            this.turnOffBuzzerBtn.UseVisualStyleBackColor = true;
            this.turnOffBuzzerBtn.Click += new System.EventHandler(this.turnOffBuzzerBtn_Click);
            // 
            // turnOnBuzzerBtn
            // 
            this.turnOnBuzzerBtn.Location = new System.Drawing.Point(111, 34);
            this.turnOnBuzzerBtn.Name = "turnOnBuzzerBtn";
            this.turnOnBuzzerBtn.Size = new System.Drawing.Size(75, 23);
            this.turnOnBuzzerBtn.TabIndex = 15;
            this.turnOnBuzzerBtn.Text = "Turn On Buzzer";
            this.turnOnBuzzerBtn.UseVisualStyleBackColor = true;
            this.turnOnBuzzerBtn.Click += new System.EventHandler(this.turnOnBuzzerBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.getUidBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericReadSector);
            this.groupBox1.Controls.Add(this.readBlockBtn);
            this.groupBox1.Controls.Add(this.readAllSectorBtn);
            this.groupBox1.Location = new System.Drawing.Point(42, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 130);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Read Panel";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxInput);
            this.groupBox2.Controls.Add(this.numericWriteSector);
            this.groupBox2.Controls.Add(this.writeBtn);
            this.groupBox2.Location = new System.Drawing.Point(42, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 90);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Write Panel";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.turnOffBuzzerBtn);
            this.groupBox3.Controls.Add(this.turnOnBuzzerBtn);
            this.groupBox3.Location = new System.Drawing.Point(42, 307);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 75);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Button";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(381, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Message Log";
            // 
            // textBoxNrp
            // 
            this.textBoxNrp.Location = new System.Drawing.Point(58, 466);
            this.textBoxNrp.Name = "textBoxNrp";
            this.textBoxNrp.Size = new System.Drawing.Size(170, 20);
            this.textBoxNrp.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 447);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "NRP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 493);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(58, 509);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(170, 20);
            this.textBoxName.TabIndex = 23;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.saveBtn);
            this.groupBox4.Location = new System.Drawing.Point(44, 429);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(198, 140);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Data";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(109, 106);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // readBtn
            // 
            this.readBtn.Location = new System.Drawing.Point(14, 29);
            this.readBtn.Name = "readBtn";
            this.readBtn.Size = new System.Drawing.Size(170, 23);
            this.readBtn.TabIndex = 27;
            this.readBtn.Text = "Read";
            this.readBtn.UseVisualStyleBackColor = true;
            this.readBtn.Click += new System.EventHandler(this.readBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.readBtn);
            this.groupBox5.Location = new System.Drawing.Point(44, 581);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 100);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Read Data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 708);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNrp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Name = "Form1";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.numericReadSector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWriteSector)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getUidBtn;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button readBlockBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button writeBtn;
        private System.Windows.Forms.Button readAllSectorBtn;
        private System.Windows.Forms.NumericUpDown numericReadSector;
        private System.Windows.Forms.NumericUpDown numericWriteSector;
        private System.Windows.Forms.Button turnOffBuzzerBtn;
        private System.Windows.Forms.Button turnOnBuzzerBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNrp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button readBtn;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}

