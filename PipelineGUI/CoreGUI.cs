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
using Project2Simulator;
using Project2Simulator.ReorderBuffers;
using Project2Simulator.ReservationStations;

namespace CoreGui
{
    public partial class CoreGUI : Form
    {
        public Core FormCore;

        public CoreGUI(Core core)
        {
            FormCore = core;
            InitializeComponent();
            CoreIDLabel.Text = "Core ID: " + core.coreID.ID.ToString();
        }

        public void UpdateValues()
        {
            ClearForm();
            UpdateRegisters();
            makeReservationEntries();
            UpdateReorderBuffer();
            UpdateInstructionQueue();
            //Update Reservation Stations

        }

        private void UpdateInstructionQueue()
        {
            foreach (var instruction in FormCore.instructionQueue.Instructions)
            {
                InstructionQueueList.Items.Add(instruction.ToString());
            }
        }

        private void UpdateReorderBuffer()
        {
            foreach (ReorderBufferSlot slot in FormCore.reorderBuffer.bufferSlots)
            {
                ReorderBufferList.Items.Add(slot.ToString());
            }
        }

        private void UpdateRegisters()
        {
            PCBox.Text = Convert.ToString(FormCore.registerFile.PC.Value.Value, 16);
            SPBox.Text = Convert.ToString(FormCore.registerFile.SP.Value.Value, 16);

            rABox.Text = Convert.ToString(FormCore.registerFile[0].Value.Value, 16);
            rBBox.Text = Convert.ToString(FormCore.registerFile[1].Value.Value, 16);
            rCBox.Text = Convert.ToString(FormCore.registerFile[2].Value.Value, 16);
            rDBox.Text = Convert.ToString(FormCore.registerFile[3].Value.Value, 16);
            rEBox.Text = Convert.ToString(FormCore.registerFile[4].Value.Value, 16);
            rFBox.Text = Convert.ToString(FormCore.registerFile[5].Value.Value, 16);
            rGBox.Text = Convert.ToString(FormCore.registerFile[6].Value.Value, 16);
            rHBox.Text = Convert.ToString(FormCore.registerFile[7].Value.Value, 16);
            rIBox.Text = Convert.ToString(FormCore.registerFile[8].Value.Value, 16);
            rJBox.Text = Convert.ToString(FormCore.registerFile[9].Value.Value, 16);
            rKBox.Text = Convert.ToString(FormCore.registerFile[10].Value.Value, 16);
            rLBox.Text = Convert.ToString(FormCore.registerFile[11].Value.Value, 16);
            rMBox.Text = Convert.ToString(FormCore.registerFile[12].Value.Value, 16);

            sBox.Text = Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0010) >> 4), 2);
            oBox.Text = Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0008) >> 3), 2);
            eqBox.Text = Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0004) >> 2), 2);
            zBox.Text = Convert.ToString((((uint)FormCore.registerFile.FLAG.Value.Value & 0x0002) >> 1), 2);
            cBox.Text = Convert.ToString(((uint)FormCore.registerFile.FLAG.Value.Value & 0x0001), 2);

        }

        private void makeReservationEntries()
        {
            var MemoryRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.MEMORY_UNIT));
            var BranchRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.BRANCH_UNIT));
            var IntAddRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.INTEGER_ADDER));
            var MovRes = Array.FindAll(FormCore.reservationStations.reservationStations, (t => t.FunctionalUnit.Type == Project2Simulator.FunctionalUnits.FunctionalUnitType.MEMORY_UNIT));

            foreach (ReservationStation station in MemoryRes)
            {
                MemoryResList.Items.Add(station.ToString());
            }

            foreach (ReservationStation station in BranchRes)
            {
                BranchResList.Items.Add(station.ToString());
            }

            foreach (ReservationStation station in IntAddRes)
            {
                IntergerAdderResList.Items.Add(station.ToString());
            }

            foreach (ReservationStation station in MovRes)
            {
                MovUnitResList.Items.Add(station.ToString());
            }
        }

        private void ClearForm()
        {
            InstructionQueueList.Items.Clear();
            ReorderBufferList.Items.Clear();
            MemoryResList.Items.Clear();
            BranchResList.Items.Clear();
            IntergerAdderResList.Items.Clear();
            MovUnitResList.Items.Clear();
        }


    }
}
