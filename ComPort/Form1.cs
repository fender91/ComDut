using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ComPort
{
    public partial class Form1 : Form
    {
        string dataOUT;
        string sendWith;

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e) // 초기화
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);

            btnOpen.Enabled = true;
            btnClose.Enabled = false;

            serialPort1.DtrEnable = false;
            chkDtrEnable.Checked = false;

            serialPort1.RtsEnable = false;
            chkRtsEnable.Checked = false;

            btnSendData.Enabled = false;

            chkWrite.Checked = true;
            chkWriteLine.Checked = false;
            sendWith = "Write";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxComPort.Text;

                //serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                //serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                //serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                //serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);

                /*
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = 8;
                serialPort1.StopBits = (StopBits)1;
                serialPort1.Parity = (Parity)0;
                */

                serialPort1.Open();
                progressBar1.Value = 100;

                btnOpen.Enabled = false;
                btnClose.Enabled = true;

                lblStatusCom.Text = "ON";

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOpen.Enabled = true;
                btnClose.Enabled = false;

                lblStatusCom.Text = "OFF";
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;

                btnOpen.Enabled = true;
                btnClose.Enabled = false;

                lblStatusCom.Text = "OFF";
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;

                if(sendWith == "WriteLine")
                {
                    serialPort1.WriteLine(dataOUT);
                }
                else if(sendWith == "Write")
                {
                    serialPort1.Write(dataOUT);
                }
                
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void chkDtrEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDtrEnable.Checked)
            {
                serialPort1.DtrEnable = true;
            }
            else { serialPort1.DtrEnable = false;  }
        }

        private void chkRtsEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRtsEnable.Checked)
            {
                serialPort1.RtsEnable = true;
            }
            else { serialPort1.RtsEnable = false; }
        }

        private void btnClearDataOut_Click(object sender, EventArgs e)
        {
            if(tBoxDataOut.Text != "")
            {
                tBoxDataOut.Text = "";
                //tBoxDataOut.Clear();
            }
        }

        private void tBoxDataOut_TextChanged(object sender, EventArgs e)
        {
            int dataOUTLength = tBoxDataOut.TextLength;
            lblDataOutLength.Text = string.Format("{0:00}", dataOUTLength);
            //lblDataOutLength.Text = dataOUTLength.ToString();
            if (chkUsingEnter.Checked)
            {
                tBoxDataOut.Text = tBoxDataOut.Text.Replace(Environment.NewLine, "");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Form1_Enter(object sender, EventArgs e)
        {
         
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 프로그램 종료 질의
            DialogResult dr = MessageBox.Show("종료?", "종료확인", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void chkUsingButton_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsingButton.Checked)
            {
                btnSendData.Enabled = true;
            }
            else { btnSendData.Enabled = false; }
        }

        private void tBoxDataOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (chkUsingEnter.Checked)
            {
                if(e.KeyCode == Keys.Enter)  // Keys.Enter = 13
                {
                    if (serialPort1.IsOpen)
                    {
                        dataOUT = tBoxDataOut.Text;
                        if (sendWith == "WriteLine")
                        {
                            serialPort1.WriteLine(dataOUT);
                        }
                        else if (sendWith == "Write")
                        {
                            serialPort1.Write(dataOUT);
                        }
                    }
                }
            }
        }

        private void chkWriteLine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWriteLine.Checked)
            {
                sendWith = "WriteLine";
                chkWrite.Checked = false;
                chkWriteLine.Checked = true;
            }
          
        }

        private void chkWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWrite.Checked)
            {
                sendWith = "Write";
                chkWrite.Checked = true;
                chkWriteLine.Checked = false;
            }
        }
    }
}
