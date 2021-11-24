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
using Project2Simulator.FunctionalUnits;
using Project2Simulator.ReservationStations;

namespace PipelineGUI
{
    public partial class ControlGUI : Form
    {
        public CPU Cpu;
        public CoreGUI[] CoreGUIs;
        public AtomicGUI AtomicGui;

        public ReservationStationCounts Counts;
        public MemoryCycleTimes memoryCycleTimes;

        public bool IsRunning = false;
        public bool Stop = false;
        public int threadSleepMS = 1000;    // TODO: Make this an actual variable people can change in the gui

        public ControlGUI()
        {
            memoryCycleTimes = new MemoryCycleTimes();
            Counts = new ReservationStationCounts();
            Counts.BranchUnit = 1;
            Counts.IntegerAdder = 4;
            Counts.MemoryUnit = 1;
            Counts.MovementUnit = 2;
            InitializeComponent();

            AtomicGui = new AtomicGUI();
            AtomicGui.Show();
            RemakeCPU();
        }

        private void importAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..");
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            RemakeCPU();

            //Load into assembly list box
            string assemblyFilePath = openFileDialog.FileName;
            string binaryInFilePath = Path.ChangeExtension(assemblyFilePath, ".bin");

            Encoder.Encode(assemblyFilePath, binaryInFilePath);

            InstallBinary(binaryInFilePath);
        }



        private void importBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..");
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            RemakeCPU();

            // Removed the decoding stuff because we aren't doing it anymore. Just load the binary as it is. If something fails, well too bad

            string binaryInFilePath = openFileDialog.FileName;

            InstallBinary(binaryInFilePath);
        }

        private void InstallBinary(string binaryFilepath)
        {
            byte[] array = File.ReadAllBytes(binaryFilepath);

            for (int i = 0; i < array.Length - 3; i = i + 4)
            {
                uint pulledValue = 0;
                pulledValue |= (uint) array[i + 0] << 24;
                pulledValue |= (uint) array[i + 1] << 16;
                pulledValue |= (uint) array[i + 2] << 8;
                pulledValue |= (uint) array[i + 3] << 0;

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

        private void RemakeCPU()
        {
            Cpu = new CPU(Counts, memoryCycleTimes);
            if(CoreGUIs != null)
                for (int i = 0; i < CoreGUIs.Length; i++)
                {
                    CoreGUIs[i].Dispose();
                }

            CoreGUIs = new CoreGUI[Cpu.GetCoreCount()];
            for (int i = 0; i < Cpu.GetCoreCount(); i++)
            {
                Core tempCore = Cpu.GetCores()[i];
                CoreGUIs[i] = new CoreGUI(tempCore);
                CoreGUIs[i].Show();
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
            // Yes, I threw an exception in running code, but I wanted to get your attention.
            // Add a small textbox or something to control the amount of seconds between thread updates.
            // You could also just ignore this because this project is stupid at this point, and just keep it at 1 second between updates.
            // Only reason I suggest this is when we show off the project, own personal demonstration could use the 1 second, then we could make it like 1 ms when doing the
            // the big demonstration.
            throw new NotImplementedException();

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

                // The thread needs to sleep otherwise it will just go turbo
                Thread.Sleep(threadSleepMS);
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
