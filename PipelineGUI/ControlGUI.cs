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
using Project2Simulator.Registers;
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

        private List<TextBox> lookupInputs;
        private List<TextBox> lookupOutputs;

        public ControlGUI()
        {
            memoryCycleTimes = new MemoryCycleTimes();
            memoryCycleTimes.AtomicLoadMemory = 1;
            memoryCycleTimes.AtomicOperation = 1;
            memoryCycleTimes.AtomicStorMemory = 1;
            memoryCycleTimes.LoadMemory = 1;
            memoryCycleTimes.StorMemory = 1;

            Counts = new ReservationStationCounts();
            Counts.BranchUnit = 1;
            Counts.IntegerAdder = 4;
            Counts.MemoryUnit = 1;
            Counts.MovementUnit = 2;
            InitializeComponent();

            lookupInputs = new List<TextBox>() {
                Lookup1Input,
                Lookup2Input,
                Lookup3Input,
                Lookup4Input,
                Lookup5Input,
                Lookup6Input,
                Lookup7Input
            };
            lookupOutputs = new List<TextBox>() {
                Lookup1Output,
                Lookup2Output,
                Lookup3Output,
                Lookup4Output,
                Lookup5Output,
                Lookup6Output,
                Lookup7Output
            };

            AtomicGui = new AtomicGUI();
            AtomicGui.Show();
            RemakeCPU();

        }

        private void ImportAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
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

            UpdateLookup();
        }
        private void ImportBinaryToolStripMenuItem_Click(object sender, EventArgs e)
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

            UpdateLookup();
        }

        private void InstallBinary(string binaryFilepath)
        {
            byte[] array = File.ReadAllBytes(binaryFilepath);

            for (int i = 0; i < array.Length - 3; i += 4)
            {
                uint pulledValue = 0;
                pulledValue |= (uint) array[i + 0] << 24;
                pulledValue |= (uint) array[i + 1] << 16;
                pulledValue |= (uint) array[i + 2] << 8;
                pulledValue |= (uint) array[i + 3] << 0;

                Cpu.memory[i] = new RegisterValue(pulledValue);
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            CoreStep();
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
                CoreGUIs[i].UpdateValues();
                CoreGUIs[i].Show();
            }   

            AtomicGui.UpdateAtomics(Cpu.THEMMU);

            UpdateLookup();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (IsRunning == true)
                return;

            IsRunning = true;
            Stop = false;
            Thread t = new Thread(new ThreadStart(RunCores));
            t.Start();
        }
        
        private void RunCores()
        {
            // Yes, I threw an exception in running code, but I wanted to get your attention.
            // Add a small textbox or something to control the amount of seconds between thread updates.
            // You could also just ignore this because this project is stupid at this point, and just keep it at 1 second between updates.
            // Only reason I suggest this is when we show off the project, own personal demonstration could use the 1 second, then we could make it like 1 ms when doing the
            // the big demonstration.
            //throw new NotImplementedException();

            while (Stop == false)
            {
                CoreStep();

                // The thread needs to sleep otherwise it will just go turbo
                Thread.Sleep(threadSleepMS);
            }

            IsRunning = false;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            IsRunning = false;
        }
        private void LookupButton_Click(object sender, EventArgs e)
        {
            UpdateLookup();
        }

        private void CoreStep()
        {
            Cpu.Cycle();

            // Update Visuals
            foreach (CoreGUI Gui in CoreGUIs)
                Gui.UpdateValues();
            
            AtomicGui.UpdateAtomics(Cpu.THEMMU);

            UpdateLookup();
        }
        private void UpdateLookup()
        {
            int address;

            foreach (var box in lookupOutputs)
                box.Text = String.Empty;

            for (int i = 0; i < lookupInputs.Count; i++)
                if (lookupInputs[i].Text != String.Empty && Int32.TryParse(lookupInputs[i].Text, out address))
                    lookupOutputs[i].Text = ToHexString(Cpu.memory[address].Value);
        }

        private static string ToHexString(uint val)
        {
            return String.Format("{0:X8}", val);
        }
    }
}
