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
using CoreGui;
using Project2Simulator;
using Project2Simulator.ReservationStations;

namespace PipelineGUI
{
    public partial class ControlGUI : Form
    {

        private string assemblyFilePath;
        private string binaryInFilePath;
        private Decoder textDecoder;
        private Encoder binaryEncoder;// = new Encoder("");

        public CPU Cpu;
        public CoreGUI[] CoreGUIs;
        public AtomicGUI AtomicGui;
        public ReservationStationCounts Counts;
        public bool IsRunning = false;
        public bool Stop = false;
        public ControlGUI()
        {
            Counts = new ReservationStationCounts();
            Counts.BranchUnit = 1;
            Counts.IntegerAdder = 4;
            Counts.MemoryUnit = 1;
            Counts.MovementUnit = 2;
            Cpu = new CPU(Counts);
            InitializeComponent();
            CoreGUIs = new CoreGUI[Cpu.GetCoreCount()];
            for (int i = 0; i < Cpu.GetCoreCount(); i++)
            {
                Core tempCore = Cpu.GetCores()[i];
                CoreGUIs[i] = new CoreGUI(Cpu.GetCores()[i]);
                CoreGUIs[i].setCoreID(tempCore.coreID.ID);
            }
            foreach (CoreGUI coreGui in CoreGUIs)
            {
                coreGui.Show();
            }
            AtomicGui = new AtomicGUI();
            AtomicGui.Show();
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

            binaryInFilePath = openFileDialog.FileName;
            textDecoder = new Decoder(binaryInFilePath);
            string assemblyText = textDecoder.DecodedFile();

            StreamWriter sw = new StreamWriter(binaryInFilePath.Replace(".bin", ".sht"));
            sw.Write(assemblyText);
            sw.Close();

            assemblyFilePath = binaryInFilePath.Replace(".bin", ".txt");
            binaryEncoder.ChangeFiles(assemblyFilePath);
            binaryEncoder.EncodeFile();

            InstallBinary(assemblyFilePath.Replace(".txt", ".bin"));
        }

        private void InstallBinary(string binaryInFilePath)
        {
            byte[] array = File.ReadAllBytes(binaryInFilePath);

            for (int i = 0; i < array.Length - 3; i = i + 4)
            {
                uint pulledValue = (uint)(((ushort)(array[i] << 8 | array[i + 1]) << 16) | (ushort)(array[i + 2] << 8 | array[i + 3]));
                Cpu.memory[i].Value = pulledValue;
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            foreach (Core core in Cpu.GetCores())
            {
                core.Cycle();
            }
            foreach (CoreGUI Gui in CoreGUIs)
            {
                Gui.UpdateValues();
            }
            AtomicGui.UpdateAtomics(Cpu.THEMMU);
        }

        private void remakeCPU()
        {
            Cpu = new CPU(Counts);
            for (int i = 0; i < Cpu.GetCoreCount(); i++)
            {
                CoreGUIs[i] = new CoreGUI(Cpu.GetCores()[i]);
                CoreGUIs[i].UpdateValues();
            }
            AtomicGui.UpdateAtomics(Cpu.THEMMU);
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (IsRunning == true)
                return;
            Stop = false;
            Thread t = new Thread(new ThreadStart(RunCores));
        }
        
        private void RunCores()
        {
            while (Stop == false)
            {
                foreach (Core core in Cpu.GetCores())
                {
                    core.Cycle();
                }
                foreach (CoreGUI Gui in CoreGUIs)
                {
                    Gui.UpdateValues();
                }
                AtomicGui.UpdateAtomics(Cpu.THEMMU);
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            IsRunning = false;
        }

        private void LookupButton_Click(object sender, EventArgs e)
        {
            Lookup1Output.Text = String.Empty;
            Lookup2Output.Text = String.Empty;
            Lookup3Output.Text = String.Empty;

            int Lookup1Addr;
            if(Lookup1Input.Text != String.Empty && Int32.TryParse(Lookup1Input.Text, out Lookup1Addr))
            {
                Lookup1Output.Text = ToHexString(Cpu.memory[Lookup1Addr].Value);
            }

            int Lookup2Addr;
            if (Lookup2Input.Text != String.Empty && Int32.TryParse(Lookup2Input.Text, out Lookup2Addr))
            {
                Lookup2Output.Text = ToHexString(Cpu.memory[Lookup2Addr].Value);
            }

            int Lookup3Addr;
            if (Lookup3Input.Text != String.Empty && Int32.TryParse(Lookup3Input.Text, out Lookup3Addr))
            {
                Lookup2Output.Text = ToHexString(Cpu.memory[Lookup3Addr].Value);
            }

        }



        private string ToHexString(uint val)
        {
            return Convert.ToString(val, 16);
        }
    }
}
