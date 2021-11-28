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
using PiplineSimulator;
using Project2Simulator;
using Project2Simulator.ReorderBuffers;
using Project2Simulator.ReservationStations;

namespace CoreGui
{

    public partial class CoreGUI : Form
    {
        public List<string> BranchStationCache = new List<string>();
        public List<string> MemoryStationCache = new List<string>();
        public List<string> IntAdderCache = new List<string>();
        public List<string> MovStationCache = new List<string>();
        public List<string> ReorderBufferCache = new List<string>();
        public Core FormCore;
        private readonly string ResLabel = String.Format(StringFormatService.GetReservationStationFormat(), 
            "Busy", 
            "Op",
            "Dest",
            "Val1",
            "Val2",
            "Val3",
            "Src1",
            "Src2",
            "Src3",
            "Addr"
            );

        private readonly string ReorderLabel = String.Format(StringFormatService.GetReorderBufferSlotFormat(),
            "ID",
            "StID",
            "Occ",
            "D1",
            "V1",
            "Valid",
            "D2",
            "V2",
            "Valid"
            );
         
        private readonly string InstructionLabel = String.Format(StringFormatService.GetInstructionFormat(),
            "OP",
            "D1",
            "D2",
            "Reg1",
            "Val1",
            "Reg2",
            "Val2",
            "Reg3",
            "Val3",
            "Addr",
            "Func"
            );


        public CoreGUI(Core core)
        {
            FormCore = core;
            InitializeComponent();

            CoreIDLabel.Text = "Core ID: " + core.coreID.ID.ToString();

            ReorderBufferList.DrawMode = DrawMode.OwnerDrawFixed;
            ReorderBufferList.DrawItem += ReorderBuffer_DrawItem;

            MemoryResList.DrawMode = DrawMode.OwnerDrawFixed;
            BranchResList.DrawMode = DrawMode.OwnerDrawFixed;
            IntergerAdderResList.DrawMode = DrawMode.OwnerDrawFixed;
            MovUnitResList.DrawMode = DrawMode.OwnerDrawFixed;

            MemoryResList.DrawItem += ReservationStation_DrawItem;
            BranchResList.DrawItem += ReservationStation_DrawItem;
            IntergerAdderResList.DrawItem += ReservationStation_DrawItem;
            MovUnitResList.DrawItem += ReservationStation_DrawItem;
            UpdateReorderBuffer();
            MakeReservationEntries();
            CachePreviousStations();
        }

        public void UpdateValues()
        {
            CachePreviousStations();
            ClearForm();
            UpdateRegisters();
            MakeReservationEntries();
            UpdateReorderBuffer();
            UpdateInstructionQueue();
            //Update Reservation Stations

        }

        private void UpdateInstructionQueue()
        {
            InstructionQueueList.Items.Add(InstructionLabel);
            foreach (var instruction in FormCore.instructionQueue.Instructions)
            {
                InstructionQueueList.Items.Add(instruction.ToString());
            }
        }

        private void UpdateReorderBuffer()
        {
            ReorderBufferCache.Clear();
            for (int i = 0; i < ReorderBufferList.Items.Count; i++)
            {
                ReorderBufferCache.Add(ReorderBufferList.Items[i].ToString());
            }
            ReorderBufferList.Items.Clear();
            ReorderBufferList.Items.Add(ReorderLabel);
            foreach (ReorderBufferSlot slot in FormCore.reorderBuffer.bufferSlots)
            {
                ReorderBufferList.Items.Add(slot.ToString());
            }

        }

        private void UpdateRegisters()
        {
            SetAndCompareTextBox(PCBox,Convert.ToString(FormCore.registerFile.PC.Value.Value, 16));
            SetAndCompareTextBox(SPBox, Convert.ToString(FormCore.registerFile.SP.Value.Value, 16));

            SetAndCompareTextBox(rABox, Convert.ToString(FormCore.registerFile[0].Value.Value, 16));
            SetAndCompareTextBox(rBBox, Convert.ToString(FormCore.registerFile[1].Value.Value, 16));
            SetAndCompareTextBox(rCBox, Convert.ToString(FormCore.registerFile[2].Value.Value, 16));
            SetAndCompareTextBox(rDBox, Convert.ToString(FormCore.registerFile[3].Value.Value, 16));
            SetAndCompareTextBox(rEBox, Convert.ToString(FormCore.registerFile[4].Value.Value, 16));
            SetAndCompareTextBox(rFBox, Convert.ToString(FormCore.registerFile[5].Value.Value, 16));
            SetAndCompareTextBox(rGBox, Convert.ToString(FormCore.registerFile[6].Value.Value, 16));
            SetAndCompareTextBox(rHBox, Convert.ToString(FormCore.registerFile[7].Value.Value, 16));
            SetAndCompareTextBox(rIBox, Convert.ToString(FormCore.registerFile[8].Value.Value, 16));
            SetAndCompareTextBox(rJBox, Convert.ToString(FormCore.registerFile[9].Value.Value, 16));
            SetAndCompareTextBox(rKBox, Convert.ToString(FormCore.registerFile[10].Value.Value, 16));
            SetAndCompareTextBox(rLBox, Convert.ToString(FormCore.registerFile[11].Value.Value, 16));
            SetAndCompareTextBox(rMBox, Convert.ToString(FormCore.registerFile[12].Value.Value, 16));

            SetAndCompareTextBox(sBox, Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0010) >> 4), 2));
            SetAndCompareTextBox(oBox, Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0008) >> 3), 2));
            SetAndCompareTextBox(eqBox, Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0004) >> 2), 2));
            SetAndCompareTextBox(zBox, Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0002) >> 1), 2));
            SetAndCompareTextBox(cBox, Convert.ToString(((uint)FormCore.registerFile.FLAG.Value.Value & 0x0001), 2));

        }

        private void MakeReservationEntries()
        {
            var MemoryRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.MEMORY_UNIT));
            var BranchRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.BRANCH_UNIT));
            var IntAddRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.INTEGER_ADDER));
            var MovRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.MOVEMENT_UNIT));
            
            MemoryResList.Items.Add(ResLabel);
            foreach (ReservationStation station in MemoryRes)
            {
                MemoryResList.Items.Add(station.ToString());
            }

            BranchResList.Items.Add(ResLabel);
            foreach (ReservationStation station in BranchRes)
            {
                BranchResList.Items.Add(station.ToString());
            }

            IntergerAdderResList.Items.Add(ResLabel);
            foreach (ReservationStation station in IntAddRes)
            {
                IntergerAdderResList.Items.Add(station.ToString());
            }

            MovUnitResList.Items.Add(ResLabel);
            foreach (ReservationStation station in MovRes)
            {
                MovUnitResList.Items.Add(station.ToString());
            }
        }

        private void CachePreviousStations()
        {
            MemoryStationCache.Clear();
            BranchStationCache.Clear();
            IntAdderCache.Clear();
            MovStationCache.Clear();
            for (int i = 0; i < MemoryResList.Items.Count; i++)
            {
                MemoryStationCache.Add(MemoryResList.Items[i].ToString());
            }

            for (int i = 0; i < BranchResList.Items.Count; i++)
            {
                BranchStationCache.Add(BranchResList.Items[i].ToString());
            }

            for (int i = 0; i < IntergerAdderResList.Items.Count; i++)
            {
                IntAdderCache.Add(IntergerAdderResList.Items[i].ToString());
            }

            for (int i = 0; i < MovUnitResList.Items.Count; i++)
            {
                MovStationCache.Add(MovUnitResList.Items[i].ToString());
            }

        }

        private void ClearForm()
        {
            InstructionQueueList.Items.Clear();
            MemoryResList.Items.Clear();
            BranchResList.Items.Clear();
            IntergerAdderResList.Items.Clear();
            MovUnitResList.Items.Clear();
        }

        //Returns true if equal, false if not equal
        private void SetAndCompareTextBox(TextBox compBox, string newEntry)
        {
            bool equal = false;
            if (compBox.Text == newEntry)
                equal = true;
            compBox.Text = newEntry;
            if (equal)
                compBox.ForeColor = Color.Black;
            else
                compBox.ForeColor = Color.Red;
        }


        private void ReorderBuffer_DrawItem(object sender, DrawItemEventArgs e)
        {
            string selectedItem = ReorderBufferList.Items[e.Index].ToString();

            Font font = new Font("Courier New", 13);
            SolidBrush solidBrush;
            if (ReorderBufferList.Items[e.Index].ToString() != ReorderBufferCache[e.Index].ToString())
                solidBrush = new SolidBrush(Color.Red);
            else
                solidBrush = new SolidBrush(Color.Black);


            int left = e.Bounds.Left;
            int top = e.Bounds.Top;

            e.DrawBackground();

            e.Graphics.DrawString(selectedItem, font, solidBrush, left, top);
        }

        private void ReservationStation_DrawItem(object sender, DrawItemEventArgs e)
        {

            SolidBrush textBrush = new SolidBrush(Color.Black);
            if (sender.Equals(MemoryResList))
            {
                if (MemoryResList.Items[e.Index].ToString() != MemoryStationCache[e.Index].ToString())
                    textBrush = new SolidBrush(Color.Red);

            }
            else if(sender.Equals(IntergerAdderResList))
            {
                if (IntergerAdderResList.Items[e.Index].ToString() != IntAdderCache[e.Index].ToString())
                    textBrush = new SolidBrush(Color.Red);
            }
            else if (sender.Equals(MovUnitResList))
            {
                if (MovUnitResList.Items[e.Index].ToString() != MovStationCache[e.Index].ToString())
                    textBrush = new SolidBrush(Color.Red);
            }
            else if (sender.Equals(BranchResList))
            {
                if (BranchResList.Items[e.Index].ToString() != BranchStationCache[e.Index].ToString())
                    textBrush = new SolidBrush(Color.Red);
            }


            e.DrawBackground();
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            ListBox lb = (ListBox)sender;
            g.DrawString(lb.Items[e.Index].ToString(), e.Font, textBrush, new PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }



    }

}
