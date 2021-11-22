using Project2Simulator;
using Project2Simulator.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PipelineGUI
{
    public partial class AtomicGUI : Form
    {
        public AtomicGUI()
        {
            InitializeComponent();
        }

        public void UpdateAtomics(MMU mMu)
        {
            MemoryListBox.Items.Clear();
            var AtomicAddresses = mMu.AtomicAddresses.AsEnumerable();
            foreach (KeyValuePair<Address, CoreID> keyPair in AtomicAddresses)
            {
                MemoryListBox.Items.Add(keyPair.ToString());
                //TODO: This looks bad, format the string
            }
        }
    }
}
