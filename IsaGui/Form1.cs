using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulator;

namespace IsaGui
{
    public partial class Form1 : Form
    {
        private Memory simMemory;
        private CPU simCpu;
        private string assemblyFilePath;
        private string binaryInFilePath;
        private Simulator.Encoder binaryEncoder = new Simulator.Encoder("");
        private Dictionary<int, int> InstructionMemAddrToGUIIndex = new Dictionary<int,int>();
        private Decoder.Decoder textDecoder;

        public Form1()
        {
            InitializeComponent();
            remakeCPU();
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            if (simCpu.halt.Value != 0)
                return;
            simCpu.RunClockCycle();
            updateRegisters();
            if (InstructionMemAddrToGUIIndex.ContainsKey(((simCpu.registers.PC1.Value & 0xF) << 16) | simCpu.registers.PC2.Value) == false)
            {
                MessageBox.Show("Trying to execute instruction outside of known instructions. Halting", "The OS is mad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                simCpu.halt.Value = 1;
                return;
            }

            if (InstructionMemAddrToGUIIndex.Count != 0)
                assemblyBox.SelectedIndex = InstructionMemAddrToGUIIndex[((simCpu.registers.PC1.Value & 0xF) << 16) | simCpu.registers.PC2.Value];
        }

        private void updateRegisters()
        {
            ALU1Box.Text = Convert.ToString(simCpu.alu.dest.Value, 2);
            ALU2Box.Text = Convert.ToString(simCpu.alu.op1.Value, 2);
            ALU3Box.Text = Convert.ToString(simCpu.alu.op2.Value, 2);

            PCBox.Text = Convert.ToString(simCpu.registers.PC1.Value, 2);
            PC2Box.Text = Convert.ToString(simCpu.registers.PC2.Value, 2);
            SP1Box.Text = Convert.ToString(simCpu.registers.SP1.Value, 2);
            SP2Box.Text = Convert.ToString(simCpu.registers.SP2.Value, 2);

            rABox.Text = Convert.ToString(simCpu.registers[0].Value, 2);
            rBBox.Text = Convert.ToString(simCpu.registers[1].Value, 2);
            rCBox.Text = Convert.ToString(simCpu.registers[2].Value, 2);
            rDBox.Text = Convert.ToString(simCpu.registers[3].Value, 2);
            rEBox.Text = Convert.ToString(simCpu.registers[4].Value, 2);
            rFBox.Text = Convert.ToString(simCpu.registers[5].Value, 2);
            rGBox.Text = Convert.ToString(simCpu.registers[6].Value, 2);
            rHBox.Text = Convert.ToString(simCpu.registers[7].Value, 2);
            rIBox.Text = Convert.ToString(simCpu.registers[8].Value, 2);
            rJBox.Text = Convert.ToString(simCpu.registers[9].Value, 2);
            rKBox.Text = Convert.ToString(simCpu.registers[10].Value, 2);

            sBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0010) >> 4), 2);
            oBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0008) >> 3), 2);
            eqBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0004) >> 2), 2);
            zBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0002) >> 1), 2);
            cBox.Text = Convert.ToString(((uint)simCpu.registers.FLAG.Value & 0x0001), 2);
        }

        private void openBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..");
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            remakeCPU();
            ResetGUI();

            binaryInFilePath = openFileDialog.FileName;
            textDecoder = new Decoder.Decoder(binaryInFilePath);
            string assemblyText = textDecoder.DecodedFile();

            StreamWriter sw = new StreamWriter(binaryInFilePath.Replace(".bin", ".sht"));
            sw.Write(assemblyText);
            sw.Close();

            assemblyFilePath = binaryInFilePath.Replace(".bin", ".txt");
            binaryEncoder.ChangeFiles(assemblyFilePath);
            binaryEncoder.EncodeFile();

            FeedInAssembly();

            InstallBinaryAndFixGUI();
            statsButton.Enabled = true;
        }

        private void openAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assemblyBox.Items.Clear();
            InstructionMemAddrToGUIIndex.Clear();
            binaryInFilePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..");
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            remakeCPU();
            ResetGUI();

            //Load into assembly list box
            assemblyFilePath = openFileDialog.FileName;

            binaryEncoder.ChangeFiles(assemblyFilePath);
            binaryInFilePath = binaryEncoder.EncodeFile();
            textDecoder = new Decoder.Decoder(binaryInFilePath);

            FeedInAssembly();

            InstallBinaryAndFixGUI();
            statsButton.Enabled = true;
        }

        private void ResetGUI()
        {
            assemblyBox.Items.Clear();
            InstructionMemAddrToGUIIndex.Clear();
            assemblyFilePath = null;

        }
        private void FeedInAssembly()
        {
            var fileStream = File.OpenRead(assemblyFilePath.Replace(".txt", ".remix"));
            var streamReader = new StreamReader(fileStream);
            string pulledLine;
            while ((pulledLine = streamReader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(pulledLine) == false && pulledLine[0] != '/')
                    assemblyBox.Items.Add(pulledLine);
            }

            assemblyBox.EndUpdate();
            fileStream.Close();
        }


        private void InstallBinaryAndFixGUI()
        {
            InstallBinary(assemblyFilePath.Replace(".txt", ".bin"));
            simCpu.halt.Value = 0;
            fixMemAddrToGUIIndex();
            if (InstructionMemAddrToGUIIndex.Count != 0)
                assemblyBox.SelectedIndex = InstructionMemAddrToGUIIndex[((simCpu.registers.PC1.Value & 0xF) << 16) | simCpu.registers.PC2.Value];
        }

        private void InstallBinary(string binaryInFilePath)
        {
            byte[] array = File.ReadAllBytes(binaryInFilePath);
            
            for (int i = 0; i < array.Length - 1; i = i + 2)
            {
                ushort pulledShort = (ushort)(array[i] << 8 | array[i + 1]);
                simMemory[i] = pulledShort;
            }

            string hexString = BitConverter.ToString(array);
            hexString = hexString.Replace("-", "");

            int j = 4;
            while (j < hexString.Length)
            {
                hexString = hexString.Insert(j, " ");
                j += 5;
            }

            BinaryTextBox.Text = hexString;
        }

        private void fixMemAddrToGUIIndex()
        {
            InstructionMemAddrToGUIIndex.Clear();
            int curMemAddress = 0;
            //InstructionMemAddrToGUIIndex
            for (int i = 0; i < binaryEncoder.inLengths.Count; i++)
            {
                if (binaryEncoder.inLengths[i] == -1)
                    continue;

                InstructionMemAddrToGUIIndex.Add(curMemAddress, i);
                curMemAddress += binaryEncoder.inLengths[i] / 8;
            }
        }

        private void remakeCPU()
        {
            simMemory = new Memory(0x100000, true);
            simCpu = new CPU(simMemory);
            simCpu.registers.SP1.Value = 0xF;
            simCpu.registers.SP2.Value = 0xFFFE;
            updateRegisters();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            while (simCpu.halt.Value == 0)
            {
                simCpu.RunClockCycle();
                if (InstructionMemAddrToGUIIndex.ContainsKey(((simCpu.registers.PC1.Value & 0xF) << 16) | simCpu.registers.PC2.Value) == false)
                {
                    MessageBox.Show("Trying to execute instruction outside of known instructions. Halting", "The OS is mad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    simCpu.halt.Value = 1;
                    continue;
                }

                if (InstructionMemAddrToGUIIndex.Count != 0)
                    assemblyBox.SelectedIndex = InstructionMemAddrToGUIIndex[((simCpu.registers.PC1.Value & 0xF) << 16) | simCpu.registers.PC2.Value];
            }
            updateRegisters();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            simCpu.halt.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(textDecoder.DecodedStats());
        }
    }
}
