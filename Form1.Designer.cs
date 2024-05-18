
using SeminarskaPraksa.Assets;

namespace SeminarskaPraksa
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
            tbOutputBox = new DarkTextBox();
            btnThreading1_Tasks = new DarkButton();
            btnThreading2_Timer = new DarkButton();
            btnThreading3_Token = new DarkButton();
            btnThreading4_RaceCondition = new DarkButton();
            btnThreading5_ProxyAndDecorator = new DarkButton();
            btnThreading6_TaskBuilder = new DarkButton();
            btnThreading7_Barrier = new DarkButton();
            btnThreading8_Semaphore = new DarkButton();
            btnThreading9_TasksInForms = new DarkButton();
            btnClearText = new DarkButton();
            btnSemaphoreInjectionWrapper = new DarkButton();
            SuspendLayout();
            // 
            // tbOutputBox
            // 
            tbOutputBox.AllowDrop = true;
            tbOutputBox.BackColor = SystemColors.InfoText;
            tbOutputBox.BorderColor = Color.FromArgb(70, 70, 70);
            tbOutputBox.BorderStyle = BorderStyle.FixedSingle;
            tbOutputBox.Font = new Font("Arial", 9F);
            tbOutputBox.ForeColor = SystemColors.Window;
            tbOutputBox.Location = new Point(319, 14);
            tbOutputBox.Multiline = true;
            tbOutputBox.Name = "tbOutputBox";
            tbOutputBox.ScrollBars = ScrollBars.Vertical;
            tbOutputBox.Size = new Size(566, 471);
            tbOutputBox.TabIndex = 1;
            // 
            // btnThreading1_Tasks
            // 
            btnThreading1_Tasks.BackColor = SystemColors.InfoText;
            btnThreading1_Tasks.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading1_Tasks.FlatStyle = FlatStyle.Flat;
            btnThreading1_Tasks.Font = new Font("Arial", 8F);
            btnThreading1_Tasks.ForeColor = SystemColors.HighlightText;
            btnThreading1_Tasks.Location = new Point(12, 121);
            btnThreading1_Tasks.Name = "btnThreading1_Tasks";
            btnThreading1_Tasks.Size = new Size(301, 29);
            btnThreading1_Tasks.TabIndex = 0;
            btnThreading1_Tasks.Text = "Threading1_Tasks";
            btnThreading1_Tasks.UseVisualStyleBackColor = false;
            btnThreading1_Tasks.Click += OnBtnThreading1_Tasks_Click;
            // 
            // btnThreading2_Timer
            // 
            btnThreading2_Timer.BackColor = SystemColors.InfoText;
            btnThreading2_Timer.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading2_Timer.FlatStyle = FlatStyle.Flat;
            btnThreading2_Timer.Font = new Font("Arial", 8F);
            btnThreading2_Timer.ForeColor = SystemColors.HighlightText;
            btnThreading2_Timer.Location = new Point(12, 156);
            btnThreading2_Timer.Name = "btnThreading2_Timer";
            btnThreading2_Timer.Size = new Size(301, 29);
            btnThreading2_Timer.TabIndex = 2;
            btnThreading2_Timer.Text = "Threading2_Timer";
            btnThreading2_Timer.UseVisualStyleBackColor = false;
            btnThreading2_Timer.Click += OnBtnThreading2_Timer_Click;
            // 
            // btnThreading3_Token
            // 
            btnThreading3_Token.BackColor = SystemColors.InfoText;
            btnThreading3_Token.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading3_Token.FlatStyle = FlatStyle.Flat;
            btnThreading3_Token.Font = new Font("Arial", 8F);
            btnThreading3_Token.ForeColor = SystemColors.HighlightText;
            btnThreading3_Token.Location = new Point(12, 191);
            btnThreading3_Token.Name = "btnThreading3_Token";
            btnThreading3_Token.Size = new Size(301, 29);
            btnThreading3_Token.TabIndex = 3;
            btnThreading3_Token.Text = "Threading3_Token";
            btnThreading3_Token.UseVisualStyleBackColor = false;
            btnThreading3_Token.Click += OnBtnThreading3_Token_Click;
            // 
            // btnThreading4_RaceCondition
            // 
            btnThreading4_RaceCondition.BackColor = SystemColors.InfoText;
            btnThreading4_RaceCondition.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading4_RaceCondition.FlatStyle = FlatStyle.Flat;
            btnThreading4_RaceCondition.Font = new Font("Arial", 8F);
            btnThreading4_RaceCondition.ForeColor = SystemColors.HighlightText;
            btnThreading4_RaceCondition.Location = new Point(12, 226);
            btnThreading4_RaceCondition.Name = "btnThreading4_RaceCondition";
            btnThreading4_RaceCondition.Size = new Size(301, 29);
            btnThreading4_RaceCondition.TabIndex = 4;
            btnThreading4_RaceCondition.Text = "Threading4_RaceCondition";
            btnThreading4_RaceCondition.UseVisualStyleBackColor = false;
            btnThreading4_RaceCondition.Click += OnBtnThreading4_RaceCondition_Click;
            // 
            // btnThreading5_ProxyAndDecorator
            // 
            btnThreading5_ProxyAndDecorator.BackColor = SystemColors.InfoText;
            btnThreading5_ProxyAndDecorator.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading5_ProxyAndDecorator.FlatStyle = FlatStyle.Flat;
            btnThreading5_ProxyAndDecorator.Font = new Font("Arial", 8F);
            btnThreading5_ProxyAndDecorator.ForeColor = SystemColors.HighlightText;
            btnThreading5_ProxyAndDecorator.Location = new Point(12, 261);
            btnThreading5_ProxyAndDecorator.Name = "btnThreading5_ProxyAndDecorator";
            btnThreading5_ProxyAndDecorator.Size = new Size(301, 29);
            btnThreading5_ProxyAndDecorator.TabIndex = 5;
            btnThreading5_ProxyAndDecorator.Text = "Threading5_ProxyAndDecorator";
            btnThreading5_ProxyAndDecorator.UseVisualStyleBackColor = false;
            btnThreading5_ProxyAndDecorator.Click += OnThreading5_ProxyAndDecorator;
            // 
            // btnThreading6_TaskBuilder
            // 
            btnThreading6_TaskBuilder.BackColor = SystemColors.InfoText;
            btnThreading6_TaskBuilder.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading6_TaskBuilder.FlatStyle = FlatStyle.Flat;
            btnThreading6_TaskBuilder.Font = new Font("Arial", 8F);
            btnThreading6_TaskBuilder.ForeColor = SystemColors.HighlightText;
            btnThreading6_TaskBuilder.Location = new Point(12, 296);
            btnThreading6_TaskBuilder.Name = "btnThreading6_TaskBuilder";
            btnThreading6_TaskBuilder.Size = new Size(301, 29);
            btnThreading6_TaskBuilder.TabIndex = 6;
            btnThreading6_TaskBuilder.Text = "Threading6_TaskBuilder";
            btnThreading6_TaskBuilder.UseVisualStyleBackColor = false;
            btnThreading6_TaskBuilder.Click += OnThreading6_TaskBuilder_Click;
            // 
            // btnThreading7_Barrier
            // 
            btnThreading7_Barrier.BackColor = SystemColors.InfoText;
            btnThreading7_Barrier.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading7_Barrier.FlatStyle = FlatStyle.Flat;
            btnThreading7_Barrier.Font = new Font("Arial", 8F);
            btnThreading7_Barrier.ForeColor = SystemColors.HighlightText;
            btnThreading7_Barrier.Location = new Point(12, 331);
            btnThreading7_Barrier.Name = "btnThreading7_Barrier";
            btnThreading7_Barrier.Size = new Size(301, 29);
            btnThreading7_Barrier.TabIndex = 7;
            btnThreading7_Barrier.Text = "Threading7_Barrier";
            btnThreading7_Barrier.UseVisualStyleBackColor = false;
            btnThreading7_Barrier.Click += OnThreading7_Barrier_Click;
            // 
            // btnThreading8_Semaphore
            // 
            btnThreading8_Semaphore.BackColor = SystemColors.InfoText;
            btnThreading8_Semaphore.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading8_Semaphore.FlatStyle = FlatStyle.Flat;
            btnThreading8_Semaphore.Font = new Font("Arial", 8F);
            btnThreading8_Semaphore.ForeColor = SystemColors.HighlightText;
            btnThreading8_Semaphore.Location = new Point(12, 366);
            btnThreading8_Semaphore.Name = "btnThreading8_Semaphore";
            btnThreading8_Semaphore.Size = new Size(301, 29);
            btnThreading8_Semaphore.TabIndex = 8;
            btnThreading8_Semaphore.Text = "Threading8_Semaphore";
            btnThreading8_Semaphore.UseVisualStyleBackColor = false;
            btnThreading8_Semaphore.Click += OnThreading8_Semaphore_Click;
            // 
            // btnThreading9_TasksInForms
            // 
            btnThreading9_TasksInForms.BackColor = SystemColors.InfoText;
            btnThreading9_TasksInForms.BorderColor = Color.FromArgb(70, 70, 70);
            btnThreading9_TasksInForms.FlatStyle = FlatStyle.Flat;
            btnThreading9_TasksInForms.Font = new Font("Arial", 8F);
            btnThreading9_TasksInForms.ForeColor = SystemColors.HighlightText;
            btnThreading9_TasksInForms.Location = new Point(12, 401);
            btnThreading9_TasksInForms.Name = "btnThreading9_TasksInForms";
            btnThreading9_TasksInForms.Size = new Size(301, 29);
            btnThreading9_TasksInForms.TabIndex = 9;
            btnThreading9_TasksInForms.Text = "Threading9_TasksInForms";
            btnThreading9_TasksInForms.UseVisualStyleBackColor = false;
            btnThreading9_TasksInForms.Click += OnThreading9_TasksInForms_Click;
            // 
            // btnClearText
            // 
            btnClearText.BackColor = SystemColors.InfoText;
            btnClearText.BorderColor = Color.FromArgb(70, 70, 70);
            btnClearText.FlatStyle = FlatStyle.Flat;
            btnClearText.Font = new Font("Segoe UI", 12F);
            btnClearText.ForeColor = SystemColors.HighlightText;
            btnClearText.Location = new Point(12, 13);
            btnClearText.Name = "btnClearText";
            btnClearText.Size = new Size(301, 53);
            btnClearText.TabIndex = 15;
            btnClearText.Text = "CLEAR DISPLAY";
            btnClearText.UseVisualStyleBackColor = false;
            btnClearText.Click += OnBtnClearText_Click;
            // 
            // btnSemaphoreInjectionWrapper
            // 
            btnSemaphoreInjectionWrapper.BackColor = SystemColors.InfoText;
            btnSemaphoreInjectionWrapper.BorderColor = Color.FromArgb(70, 70, 70);
            btnSemaphoreInjectionWrapper.FlatStyle = FlatStyle.Flat;
            btnSemaphoreInjectionWrapper.Font = new Font("Arial", 8F);
            btnSemaphoreInjectionWrapper.ForeColor = SystemColors.HighlightText;
            btnSemaphoreInjectionWrapper.Location = new Point(12, 436);
            btnSemaphoreInjectionWrapper.Name = "btnSemaphoreInjectionWrapper";
            btnSemaphoreInjectionWrapper.Size = new Size(301, 29);
            btnSemaphoreInjectionWrapper.TabIndex = 16;
            btnSemaphoreInjectionWrapper.Text = "SemaphoreInjectionWrapper";
            btnSemaphoreInjectionWrapper.UseVisualStyleBackColor = false;
            btnSemaphoreInjectionWrapper.Click += OnSemaphoreInjectionWrapper_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(897, 497);
            Controls.Add(btnSemaphoreInjectionWrapper);
            Controls.Add(btnClearText);
            Controls.Add(btnThreading9_TasksInForms);
            Controls.Add(btnThreading8_Semaphore);
            Controls.Add(btnThreading7_Barrier);
            Controls.Add(btnThreading6_TaskBuilder);
            Controls.Add(btnThreading5_ProxyAndDecorator);
            Controls.Add(btnThreading4_RaceCondition);
            Controls.Add(btnThreading3_Token);
            Controls.Add(btnThreading2_Timer);
            Controls.Add(tbOutputBox);
            Controls.Add(btnThreading1_Tasks);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private DarkTextBox tbOutputBox;
        private DarkButton btnClearText;
        private DarkButton btnThreading1_Tasks;
        private DarkButton btnThreading2_Timer;
        private DarkButton btnThreading3_Token;
        private DarkButton btnThreading4_RaceCondition;
        private DarkButton btnThreading5_ProxyAndDecorator;
        private DarkButton btnThreading6_TaskBuilder;
        private DarkButton btnThreading7_Barrier;
        private DarkButton btnThreading8_Semaphore;
        private DarkButton btnThreading9_TasksInForms;
        private DarkButton btnSemaphoreInjectionWrapper;
    }
}
