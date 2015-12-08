// Eric Chen 11381898

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Numerics;

namespace HW3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Eric Chen\nStudent ID: 11381898", "About Eric Chen's HW3");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = saveFileDialog1.FileName; // Get file name
            Save(filename);
        }

        // Takes a string file name and saves it using Streams
        void Save(string fileName)
        {
            FileStream fs = new FileStream(
                fileName,
                FileMode.Create,
                FileAccess.Write);
            Save(fs);
            fs.Dispose();
        }

        void Save(Stream s)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                using (StringReader sr = new StringReader(textBox1.Text))
                {
                    while (sr.Peek() >= 0)  // Learned use of Peek() from StackOverflow
                    {
                        sb.AppendLine(sr.ReadLine());
                    }
                }
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.WriteLine(sb);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = openFileDialog1.FileName; // Get file name
            Load(filename);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        // Takes a string file name and opens it using Streams
        void Load(string fileName)
        {
            using (FileStream fs = File.Open(
                fileName, 
                FileMode.Open, 
                FileAccess.Read))

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fs))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    textBox1.Text = line;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file could not be read:");
                MessageBox.Show(e.Message);
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void load50FibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(50);
            textBox1.Text = ftr.ReadToEnd();
        }

        private void load100FibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(100);
            textBox1.Text = ftr.ReadToEnd();
        }
    }

    class FibonacciTextReader : TextReader
    {
        int m_maxLines, m_start = 0;
        BigInteger currentNumber = 1, lastNumber = 1;   // Initial values since we start calculating after the 3rd number:'1'

        // Constructor function
        public FibonacciTextReader(int maxLines)
        {
            m_maxLines = maxLines;
        }

        //ReadLine calculates and returns the next fib number as a string.
        public override string ReadLine()
        {
            BigInteger result;
            
            if (m_start == 0)    // Handles the first number in the fibonacci sequence, 0
            {
                return "0";
            }
            if ((m_start == 1)||(m_start == 2))    // Second and third numbers are both 1
            {
                return "1";
            }
            else                // Calculate next fibonacci number
            {
                result = currentNumber + lastNumber;
                currentNumber = lastNumber;
                lastNumber = result;
                return result.ToString();
            }
        }

        // Repeatedly calls ReadLine() and concatenate all strings together and outputs the resulting string.
        public override string ReadToEnd()
        {
            StringBuilder sb = new StringBuilder();

            while (m_start < m_maxLines)    // Loop maxLines times: 50, 100
            {
                sb.Append(m_start+1).Append(": ").AppendLine(ReadLine());
                m_start++;
            }
            return sb.ToString();
        }
    }
}

