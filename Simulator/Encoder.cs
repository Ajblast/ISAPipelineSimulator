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
        Dictionary<string, int> inDicSize;              //This is a dictionary that will uses the name of an instruction as a key, the value is the size of that command
                                                        //0 is used as a size placeholder for hybrid commands

        Dictionary<string, byte> inDicCode;             //This is a dictionary that uses the name of an instruction as a key, the value is the code for that command
                                                        //in the form of a byte

        Dictionary<string, byte> regDicCode;            //This is a dictionary that uses the name of a register as a key, the value is the code for that register in 
                                                        //the form of a byte

        private string txtFilePath;                     //Holds the path to the original file which is being encoded
        private string remixFilePath;                   //Holds the path to the edited version of the original file, certain changes are made, such as extending macros, eliminating
                                                        //commas, and removing comments
        private string binFilePath;                     //Holds the path to the binary file that holds the encoded version of the original file

        private StreamWriter sw;
        private StreamReader sr;
        private BinaryWriter bw;

        List<string> labels;                            //This holds all of the labels that are intitialized in the remix file, the int in the same position in the labelPositions
                                                        //list holds the address of the label
        List<int> labelPositions;                       //This holds all of the positions of labels that are initialized in the remix file, the string in the same position in the
                                                        //labels list holds the corresponding label.
        public List<int> inLengths;                            //This list holds the length of every line (instruction or not) in the remix file in bits. If a line holds a label, the corresponding
                                                        //value in inLengths will be -1

        /// <summary>
        /// Initializes a new instance of the <see cref="Encoder"/> class.
        /// </summary>
        /// <param name="filePath">The file path to the instruction file.</param>
        /// <param name="outputPath">The file path the binary file will go.</param>
        public Encoder(string filePath)
        {
            txtFilePath = filePath;
            remixFilePath = filePath.Replace(".txt", ".remix");
            binFilePath = filePath.Replace(".txt", ".bin"); ;

            labels = new List<string>();
            labelPositions = new List<int>();
            inLengths = new List<int>();

            inDicSize = new Dictionary<string, int>();
            inDicSize.Add("nop", 16);
            inDicSize.Add("add", 32);
            inDicSize.Add("addc", 32);
            inDicSize.Add("subb", 32);
            inDicSize.Add("and", 32);
            inDicSize.Add("or", 32);
            inDicSize.Add("nor", 32);
            inDicSize.Add("shl", 32);
            inDicSize.Add("shr", 32);
            inDicSize.Add("shar", 32);
            inDicSize.Add("ror", 32);
            inDicSize.Add("rol", 32);
            inDicSize.Add("rorc", 32);
            inDicSize.Add("rolc", 32);
            inDicSize.Add("load", 32);
            inDicSize.Add("stor", 32);
            inDicSize.Add("mov", 0);
            inDicSize.Add("push", 0);
            inDicSize.Add("pop", 16);
            inDicSize.Add("cmp", 16);
            inDicSize.Add("jz", 16);
            inDicSize.Add("jnz", 16);
            inDicSize.Add("jg", 16);
            inDicSize.Add("jge", 16);
            inDicSize.Add("jl", 16);
            inDicSize.Add("jle", 16);
            inDicSize.Add("ja", 16);
            inDicSize.Add("jae", 16);
            inDicSize.Add("jb", 16);
            inDicSize.Add("jbe", 16);
            inDicSize.Add("lda", 32);
            inDicSize.Add("neg", 32);
            inDicSize.Add("xor", 32);
            inDicSize.Add("sub", 32);
            inDicSize.Add("halt", 16);

            inDicCode = new Dictionary<string, byte>();
            inDicCode.Add("nop", 0);
            inDicCode.Add("add", 1);
            inDicCode.Add("addc", 2);
            inDicCode.Add("subb", 3);
            inDicCode.Add("and", 4);
            inDicCode.Add("or", 5);
            inDicCode.Add("nor", 6);
            inDicCode.Add("shl", 7);
            inDicCode.Add("shr", 8);
            inDicCode.Add("shar", 9);
            inDicCode.Add("ror", 10);
            inDicCode.Add("rol", 11);
            inDicCode.Add("rorc", 12);
            inDicCode.Add("rolc", 13);
            inDicCode.Add("load", 14);
            inDicCode.Add("stor", 15);
            inDicCode.Add("mov", 16);
            inDicCode.Add("push", 17);
            inDicCode.Add("pop", 18);
            inDicCode.Add("cmp", 19);
            inDicCode.Add("jz", 20);
            inDicCode.Add("jnz", 21);
            inDicCode.Add("jg", 22);
            inDicCode.Add("jge", 23);
            inDicCode.Add("jl", 24);
            inDicCode.Add("jle", 25);
            inDicCode.Add("ja", 26);
            inDicCode.Add("jae", 27);
            inDicCode.Add("jb", 28);
            inDicCode.Add("jbe", 29);
            inDicCode.Add("lda", 30);
            inDicCode.Add("neg", 31);
            inDicCode.Add("xor", 32);
            inDicCode.Add("sub", 33);
            inDicCode.Add("halt", 127);

            regDicCode = new Dictionary<string, byte>();
            regDicCode.Add("rA", 0);
            regDicCode.Add("rB", 1);
            regDicCode.Add("rC", 2);
            regDicCode.Add("rD", 3);
            regDicCode.Add("rE", 4);
            regDicCode.Add("rF", 5);
            regDicCode.Add("rG", 6);
            regDicCode.Add("rH", 7);
            regDicCode.Add("rI", 8);
            regDicCode.Add("rJ", 9);
            regDicCode.Add("rK", 10);
            regDicCode.Add("PC1", 11);
            regDicCode.Add("PC2", 12);
            regDicCode.Add("SP1", 13);
            regDicCode.Add("SP2", 14);
            regDicCode.Add("FLAG", 15);
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
                if (inDicSize.TryGetValue(arr[0], out x))
                {
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
                    ushort temp = Build16BitIn(input);
                    bw.Write((byte)((temp & 0xFF00) >> 8));
                    bw.Write((byte)((temp & 0x00FF)));
                }
            }
            sr.Close();
            bw.Close();           
        }

        /// <summary>
        /// Encodes a 16 bit command from our ISA into binary
        /// </summary>
        /// <param name="input">The instruction to be encoded.</param>
        /// <returns>code - an unsigned 16 bit integer containing the binary version of the instruction</returns>
        /// <exception cref="Exception">Something went wrong in BuildByteArray2</exception>
        private ushort Build16BitIn(string input)
        {
            string[] arr = input.Split();
            byte Op1;
            byte Op2;
            byte immediate;
            byte opcode;
            ushort code = 0;
            if (inDicCode.TryGetValue(arr[0], out opcode))
            {
                code |= (ushort)((opcode & 0x7F) << 9);
                switch (arr.Length)
                {
                    case 3:                                         //for cmp and mov reg

                        immediate = 0;
                        Op1 = regDicCode[arr[1]];
                        Op2 = regDicCode[arr[2]];
                        code |= (ushort)((immediate & 0x1) << 8);
                        code |= (ushort)((Op1 & 0xF) << 4);
                        code |= (ushort)(Op2 & 0xF);
                        break;
                    case 2:                                         //for pop and push reg
                        immediate = 0;
                        Op1 = regDicCode[arr[1]];
                        code |= (ushort)((immediate & 0x1) << 8);
                        code |= (ushort)((Op1 & 0xF) << 4);
                        code |= (ushort)(0x00 & 0xF);
                        break;
                    case 1:                                          //for all jumps, nop, and halt
                        immediate = 0;
                        code |= (ushort)((immediate & 0x1) << 8);
                        code |= (ushort)(0x00 & 0xFF);
                        break;
                }
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
