using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.worker = new BackgroundWorker();
            // BackgroundWorker의 ReportProgress() 메서드 활용 여부, 보통 true
            this.worker.WorkerReportsProgress = true;
            // CancelAsync()로 BackgroundWorker를 멈출 수 있게 할지, 보통 true
            this.worker.WorkerSupportsCancellation = true;

            // BackgroundWorker가 UI스레드와 별개로 수행할 메서드
            this.worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            // ReportProgress() 메서드가 수행될 때 실행될 메서드
            this.worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            // ReportProgress()가 100으로 호출되면 마지막에 한 번 실행되는 메서드
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_Complete);
            txtExtension.Text = "*";
        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string directoryPath = txtDirectory.Text;
            string fileExtension = txtExtension.Text;

            if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
            {
                MessageBox.Show("유효한 디렉토리 경로를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                MessageBox.Show("확장자를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var files = Directory.GetFiles(directoryPath, $"*{fileExtension}", SearchOption.AllDirectories);
                lstResults.Items.Clear();
                foreach (var file in files)
                {
                    lstResults.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // 백그라운드 작업 실행 로직
        }

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void Worker_Complete(object sender, EventArgs e)
        {
              }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!this.worker.IsBusy)
            {
                this.worker.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
    }
}
