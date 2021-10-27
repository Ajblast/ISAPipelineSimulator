using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Runtime.InteropServices;

namespace Simulator
{


    /// <summary>
    /// Encoder
    /// This class will encode a file of commands written in team 5's ISA into the binary equivalents written in our ISA
    /// </summary>
    public class Encoder
    {
        Dictionary<string, byte> inDicCode;             //This is a dictionary that uses the name of an instruction as a key, the value is the code for that command
                                                        //in the form of a byte

        Dictionary<string, byte> regDicCode;            //This is a dictionary that uses the name of a register as a key, the value is the code for that register in 
                                                        //the form of a byte

        private string txtFilePath;                     //Holds the path to the original file which is being encoded
        private string remixFilePath;                   //Holds the path to the edited version of the original file, certain changes are made, such as extending macros, eliminating
                                                        //commas, and removing comments
        private string binFilePath;                     //Holds the path to the binary file that holds the encoded version of the original file
        private string infoFilePath;

        private StreamWriter sw;
        private StreamReader sr;
        private BinaryWriter bw;

        List<string> labels;                            //This holds all of the labels that are intitialized in the remix file, the int in the same position in the labelPositions
                                                        //list holds the address of the label
        List<int> labelPositions;                       //This holds all of the positions of labels that are initialized in the remix file, the string in the same position in the
                                                        //labels list holds the corresponding label.
        public List<int> inLengths;                     //This list holds the length of every line (instruction or not) in the remix file in bits. If a line holds a label, the corresponding
                                                        //value in inLengths will be -1

        /// <summary>
        /// Initializes a new instance of the <see cref="Encoder"/> class.
        /// </summary>
        /// <param name="filePath">The file path to the instruction file.</param>
        /// <param name-"infoFile">The file path to the info file</param>
        /// <param name="outputPath">The file path the binary file will go.</param>
        public Encoder(string filePath)
        {
            txtFilePath = filePath;
            remixFilePath = filePath.Replace(".txt", ".remix");
            binFilePath = filePath.Replace(".txt", ".bin");
            infoFilePath = System.IO.Path.GetFullPath("ISAInput.txt");

            labels = new List<string>();
            labelPositions = new List<int>();
            inLengths = new List<int>();

            inDicCode = new Dictionary<string, byte>();
            sr = new StreamReader(infoFilePath);
            string temp;
            string[] tempArr;
            for (int i = 0; i < 35; i++)
            {
                temp = sr.ReadLine();
                tempArr = temp.Split(' ');
                inDicCode.Add(tempArr[0], tempArr[1]);
            }

            regDicCode = new Dictionary<string, byte>();
            for (int i = 0; i < 16; i++)
            {
                temp = sr.ReadLine();
                tempArr = temp.Split(' ');
                inDicCode.Add(tempArr[0], tempArr[1]);
            }
            sr.Close();
        }

        /// <summary>
        /// This method allows you to shift the focus of this class to another file
        /// It sets the necessary values and changes the txtFilePath to the new file path
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void ChangeFiles(string filePath)
        {
            txtFilePath = filePath;
            remixFilePath = filePath.Replace(".txt", ".remix");
            binFilePath = filePath.Replace(".txt", ".bin");

            labels = new List<string>();
            labelPositions = new List<int>();
            inLengths = new List<int>();

            sr = new StreamReader(txtFilePath);
            sw = new StreamWriter(remixFilePath);
        }

        /// <summary>
        /// This method will encode an input file containing instructions from our ISA into binary
        /// </summary>
        /// <returns>binary - the path to the binary file created by this method</returns>
        public string EncodeFile()
        {
            FirstPass();
            SecondPass();
            ThirdPass();
            FourthPass();
            return binFilePath;
        }

        /// <summary>
        /// The first pass through the text file that contains instructions from our ISA
        /// What this pass does is expand any jmp macro calls into a cmp and je command.
        /// This pass will also replace ", " with " " in the file
        /// </summary>
        public void FirstPass()
        {
            string[] arr;
            string input = sr.ReadLine();

            while (input != null)
            {
                input = input.Replace(", ", " ");
                if (input.Contains("//"))    //This will remove any comments present in the code
                {
                    input = input.Remove(input.IndexOf("//"), input.Length - input.IndexOf("//"));
                }

                input = input.Trim();
                arr = input.Split(' ');

                if (arr[0].Equals("jmp"))   //jmp is the only macro currently present in our ISA, it expands out to the commands below
                {
                    sw.WriteLine("lda " + arr[1]);
                    sw.WriteLine("cmp rA rA");
                    sw.WriteLine("jz");
                }
                else
                {
                    if (!input.Equals(""))   //A line may be empty if it only contained a comment, for my sake I've excluded these lines
                        sw.WriteLine(input);
                }
                input = sr.ReadLine();
            }
            sr.Close();
            sw.Close();
        }

        /// <summary>
        /// The second pass through the text file
        /// This pass will find the size of each instruction being read from the file
        /// </summary>
        public void SecondPass()
        {
            sr = new StreamReader(remixFilePath);       //We'll only need to read from the remix file from here on out
            string input = sr.ReadLine();
            string[] arr;

            while (input != null)
            {
                arr = input.Split(' ');
                int x;                                  //This int will hold the size of a command
                if (inDicCode.ContainsKey(arr[0]))
                {
                    inLengths.Add(32);

                    /* Didn't want to just delete this in case we want to go back to hybrid instruction length in the future
                    if (x == 0)                          //This if block will deal with any of the placeholder 0's that represent hybrid commands
                    {                                   //Since there are only 2 hybrid commands, there's only 2 cases
                        if (arr.Length == 2)             //if the command has 1 operand (push)
                        {
                            if (arr[1].Contains("#"))    //if the operand is an immediate
                            {
                                inLengths.Add(32);
                            }
                            else
                            {
                                inLengths.Add(16);
                            }
                        }
                        else                             //(mov)
                        {
                            if (arr[2].Contains("#"))
                            {
                                inLengths.Add(32);
                            }
                            else
                            {
                                inLengths.Add(16);
                            }
                        }
                    }
                    else                                                            //The length is known for the rest of the commands so we don't need to worry about granularity
                    {
                        inLengths.Add(x);
                    }
                    */

                }
                else                                                                //If we reach this else statement, 1 of 2 things has occurred
                {
                    if (arr[0].Substring(arr[0].Length - 1, 1).Equals(":"))         //either the line contains a label, for which we'll put the placeholder -1
                    {
                        inLengths.Add(-1);
                    }
                    else                                                            //Or the command is unrecognized
                    {
                        sr.Close();
                        throw new Exception("Unrecognized command: " + arr[0]);
                    }
                }
                input = sr.ReadLine();
            }
            sr.Close();
        }

        /// <summary>
        /// The third pass through the file
        /// This will resolve any labels in the file to their subsequent address in the file
        /// </summary>
        public void ThirdPass()
        {
            sr = new StreamReader(remixFilePath);
            int counter = 0;                                                //This counter holds the current offset for a command
            for (int i = 0; i < inLengths.Count; i++)                        //Every line has a corresponding entry in the array, so we'll just loop through all of the items in inLengths
            {
                string input = sr.ReadLine();
                if (inLengths[i] == -1)                                     //If we come across a label we can just add a new string to labels and add a new int in positions
                {
                    labels.Add(input.Substring(0, input.Length - 1));
                    labelPositions.Add(counter);
                }
                else                                                        //If we haven't come across a label then we just add to the counter and keep moving
                {
                    counter += inLengths[i] / 8;
                }
            }
            sr.Close();
        }

        /// <summary>
        /// The fourth pass through the file
        /// This method is where the class actually writes the binary form of the instructions into a binary file
        /// </summary>
        public void FourthPass()
        {
            sr = new StreamReader(remixFilePath);
            bw = new BinaryWriter(File.OpenWrite(binFilePath));
            string input;
            for (int i = 0; i < inLengths.Count; i++)
            {
                input = sr.ReadLine();
                if (inLengths[i] == 32)
                {
                    uint temp = Build32BitIn(input);
                    bw.Write((byte)((temp & 0xFF000000) >> 24));
                    bw.Write((byte)((temp & 0x00FF0000) >> 16));
                    bw.Write((byte)((temp & 0x0000FF00) >> 8));
                    bw.Write((byte)((temp & 0x000000FF)));
                }
                else if (inLengths[i] == 16)
                {
                    uint temp = Build16BitIn(input);
                    bw.Write((byte)((temp & 0xFF000000) >> 24));
                    bw.Write((byte)((temp & 0x00FF0000) >> 16));
                    bw.Write((byte)((temp & 0x0000FF00) >> 8));
                    bw.Write((byte)((temp & 0x000000FF)));
                }
            }
            sr.Close();
            bw.Close();           
        }

        /// <summary>
        /// Encodes a 16 bit command from our ISA into binary
        /// The ISA has changed to a fixed length instruction format inwhich each instruction is 32 bits long, thus 16 bits will containing 0 will be appended to the end of these instructions
        /// </summary>
        /// <param name="input">The instruction to be encoded.</param>
        /// <returns>code - an unsigned 16 bit integer containing the binary version of the instruction</returns>
        /// <exception cref="Exception">Something went wrong in BuildByteArray2</exception>
        private uint Build16BitIn(string input)
        {
            string[] arr = input.Split();
            byte Op1;
            byte Op2;
            byte immediate;
            byte opcode;
            uint code = 0;
            if (inDicCode.TryGetValue(arr[0], out opcode))
            {
                code |= (ushort)((opcode & 0x7F) << 25);
                switch (arr.Length)
                {
                    case 3:                                         //for cmp and mov reg

                        immediate = 0;
                        Op1 = regDicCode[arr[1]];
                        Op2 = regDicCode[arr[2]];
                        code |= (uint)((immediate & 0x1) << 24);
                        code |= (uint)((Op1 & 0xF) << 20);
                        code |= (uint)((Op2 & 0xF) << 16);
                        break;
                    case 2:                                         //for pop and push reg
                        immediate = 0;
                        Op1 = regDicCode[arr[1]];
                        code |= (uint)((immediate & 0x1) << 24);
                        code |= (uint)((Op1 & 0xF) << 20);
                        code |= (uint)((0x00 & 0xF) << 16);
                        break;
                    case 1:                                          //for all jumps, nop, and halt
                        immediate = 0;
                        code |= (uint)((immediate & 0x1) << 24);
                        code |= (uint)((0x00 & 0xFF) << 16);
                        break;
                }
                code |= (uint)(0x0000000000000000 & 0xFFFF);
                return code;
            }
            else
                throw new Exception("Something went wrong in BuildByteArray2");

        }

        /// <summary>
        /// Encodes a 32 bit command from our ISA into binary
        /// </summary>
        /// <param name="input">The command to be encoded.</param>
        /// <returns>code - an unsigned 32 bit integer containing the encoded version of a command</returns>
        /// <exception cref="Exception">Something went wrong in BuildByteArray2</exception>
        private uint Build32BitIn(string input)
        {
            string[] arr = input.Split();
            byte Op1;
            byte Op2;
            byte Op3;
            ushort imm;
            uint mem;
            byte immediate;
            byte opcode;
            uint code = 0;
            if (inDicCode.TryGetValue(arr[0], out opcode))
            {
                code |= (uint)((opcode & 0x7F) << 25);
                switch (arr.Length)
                {
                    case 4:                                               //For arithmetic instructions with immediates or registers or load/stor register
                        if (arr[3].Contains("#"))                         //for arithmetic imm
                        {
                            immediate = 1;
                            Op1 = regDicCode[arr[1]];
                            Op2 = regDicCode[arr[2]];
                            imm = UInt16.Parse(arr[3].Substring(1));
                            code |= (uint)((immediate & 0x1) << 24);
                            code |= (uint)((Op1 & 0xF) << 20);
                            code |= (uint)((Op2 & 0xF) << 16);
                            code |= (uint)(imm & 0xFFFF);

                        }
                        else                                                //for arithmetic reg and load/stor register
                        {
                            if (opcode == 14 || opcode == 15)               //for load/stor register
                            {
                                immediate = 0;
                                Op1 = regDicCode[arr[1]];
                                Op2 = regDicCode[arr[2]];
                                Op3 = regDicCode[arr[3]];
                                code |= (uint)((immediate & 0x1) << 24);
                                code |= (uint)((Op1 & 0xF) << 20);
                                code |= (uint)((Op2 & 0xF) << 16);
                                code |= (uint)((0x000 & 0xFFF) << 4);
                                code |= (uint)(Op3 & 0xF);
                            }
                            else                                            //for arithmetic reg
                            {
                                immediate = 0;
                                Op1 = regDicCode[arr[1]];
                                Op2 = regDicCode[arr[2]];
                                Op3 = regDicCode[arr[3]];
                                code |= (uint)((immediate & 0x1) << 24);
                                code |= (uint)((Op1 & 0xF) << 20);
                                code |= (uint)((Op2 & 0xF) << 16);
                                code |= (uint)((0x000 & 0xFFF) << 4);
                                code |= (uint)(Op3 & 0xF);
                            }
                        }
                        break;
                    case 3:                                     //For load or stor with immediate, or neg, or mov imm
                        if (arr[1].Contains("<"))               //for stor imm
                        {
                            immediate = 1;
                            Op1 = regDicCode[arr[2]];
                            mem = (uint)labelPositions[labels.IndexOf(arr[1].Substring(1, arr[1].Length - 2))];
                            code |= (uint)((immediate & 0x1) << 24);
                            code |= (uint)((Op1 & 0xF) << 20);
                            code |= (uint)(mem & 0xFFFFF);
                        }
                        else if (arr[1].Contains("#"))          //for stor imm
                        {
                            immediate = 1;
                            Op1 = regDicCode[arr[2]];
                            mem = UInt32.Parse(arr[1].Substring(1));
                            code |= (uint)((immediate & 0x1) << 24);
                            code |= (uint)((Op1 & 0xF) << 20);
                            code |= (uint)(mem & 0xFFFFF);

                        }
                        else if (arr[2].Contains("<"))          //for load imm
                        {
                            immediate = 1;
                            Op1 = regDicCode[arr[1]];
                            mem = (uint)labelPositions[labels.IndexOf(arr[2].Substring(1, arr[1].Length - 2))];
                            code |= (uint)((immediate & 0x1) << 24);
                            code |= (uint)((Op1 & 0xF) << 20);
                            code |= (uint)(mem & 0xFFFFF);
                        }
                        else if (arr[2].Contains("#"))          //for load imm, neg imm, mov imm
                        {
                            if (opcode == 14)                   //for load imm
                            {
                                immediate = 1;
                                Op1 = regDicCode[arr[1]];
                                mem = UInt32.Parse(arr[2].Substring(1));
                                code |= (uint)((immediate & 0x1) << 24);
                                code |= (uint)((Op1 & 0xF) << 20);
                                code |= (uint)(mem & 0xFFFFF);

                            }
                            else                                //for neg imm, mov imm
                            {
                                immediate = 1;
                                Op1 = regDicCode[arr[1]];
                                imm = UInt16.Parse(arr[2].Substring(1));
                                code |= (uint)((immediate & 0x1) << 24);
                                code |= (uint)((Op1 & 0xF) << 20);
                                code |= (uint)((0x0 & 0xF) << 16);
                                code |= (uint)(imm & 0xFFFF);
                            }
                        }
                        else                                    //for neg reg
                        {
                            immediate = 0;
                            Op1 = regDicCode[arr[1]];
                            Op2 = regDicCode[arr[2]];
                            code |= (uint)((immediate & 0x1) << 24);
                            code |= (uint)((Op1 & 0xF) << 20);
                            code |= (uint)((Op2 & 0xF) << 16);
                            code |= (uint)(0x0000 & 0xFFFF);
                        }
                        break;
                    case 2:                                                 //for lda and push imm
                        if (arr[1].Contains("#"))
                        {
                            if (opcode == 30)                                //for lda with a register
                            {
                                immediate = 1;
                                mem = UInt32.Parse(arr[1].Substring(1));
                                code |= (uint)((immediate & 0x1) << 24);
                                code |= (uint)((0x0 & 0xF) << 20);
                                code |= (uint)(mem & 0xFFFFF);
                            }
                            else                                            //for push imm
                            {
                                immediate = 1;
                                imm = UInt16.Parse(arr[1].Substring(1));
                                code |= (uint)((immediate & 0x1) << 24);
                                code |= (uint)((0x00 & 0xFF) << 16);
                                code |= (uint)(imm & 0xFFFF);
                            }
                        }
                        else                                                //for lda with a label
                        {
                            immediate = 1;
                            mem = (uint)labelPositions[labels.IndexOf(arr[1].Substring(1, arr[1].Length - 2))];
                            code |= (uint)((immediate & 0x1) << 24);
                            code |= (uint)((0x0 & 0xF) << 20);
                            code |= (uint)(mem & 0xFFFFF);
                        }
                        break;
                }
                return code;
            }
            else
                throw new Exception("Something went wrong in BuildByteArray2");
        }

        /*
        static void Main(string[] args)
        {
            Encoder x = new Encoder(@"\Users\isaia\source\repos\CAProjectEncoder\CAProjectEncoder\hello.txt");
            Console.WriteLine(x.EncodeFile());

        }
        */
    }
}
