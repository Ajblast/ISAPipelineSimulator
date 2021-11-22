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
using CoreGui;
using Project2Simulator;

namespace PipelineGUI
{
    public partial class ControlGUI : Form
    {

        private string assemblyFilePath;
        private string binaryInFilePath;
        private Decoder textDecoder;
        private Encoder binaryEncoder = new Encoder("");

        public CPU Cpu;
        public CoreGUI[] CoreGUIs;
        public ControlGUI()
        {
            Cpu = new CPU();
            InitializeComponent();
            CoreGUIs = new CoreGUI[Cpu.GetCoreCount()];
            for (int i = 0; i < Cpu.GetCoreCount(); i++)
            {
                CoreGUIs[i] = new CoreGUI(Cpu.GetCores()[i]);
            }
            foreach (CoreGUI coreGui in CoreGUIs)
            {
                coreGui.Show();
            }

        }

        private void importAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binaryInFilePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..");
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            remakeCPU();

            //Load into assembly list box
            assemblyFilePath = openFileDialog.FileName;

            binaryEncoder.ChangeFiles(assemblyFilePath);
            binaryInFilePath = binaryEncoder.EncodeFile();
            textDecoder = new Decoder(binaryInFilePath);

            InstallBinary(assemblyFilePath.Replace(".txt", ".bin"));
        }



        private void importBinaryToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void InstallBinary(string binaryInFilePath)
        {
            byte[] array = File.ReadAllBytes(binaryInFilePath);

            for (int i = 0; i < array.Length - 1; i = i + 2)
            {
                ushort pulledShort = (ushort)(array[i] << 8 | array[i + 1]);
                simMemory[i] = pulledShort;
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            Cpu.Cycle();
        }

        private void remakeCPU()
        {
            throw new NotImplementedException();
        }
    }
}
