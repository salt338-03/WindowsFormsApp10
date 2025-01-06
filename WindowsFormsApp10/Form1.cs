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
                MessageBox.Show("디렉토리 경로를 입력하세요.");
                return;
            }

            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                MessageBox.Show("확장자를 입력하세요.");
                return;
            }

            try
            {
                var files = Directory.GetFiles(directoryPath, $"*{fileExtension}", SearchOption.AllDirectories);
                //* 와일드카드 GetFiles(string path, string searchPattern...) 
                //path (필수)  검색할 디렉토리의 경로를 지정합니다.
                //searchPattern (선택) 파일 이름이나 확장자를 기반으로 필터링하는 문자열입니다. 기본값은 "*"으로, 모든 파일을 의미합니다.
                //SearchOption.TopDirectoryOnly: 지정된 디렉토리 내 파일만 검색.
                //SearchOption.AllDirectories: 하위 디렉토리까지 포함하여 검색.

                lstResults.Items.Clear();
                foreach (var file in files)
                {
                    lstResults.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
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
