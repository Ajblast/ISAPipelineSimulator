using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Project2Simulator.Instructions;
using Project2Simulator.Registers;

namespace PipelineGUI
{
    /// <summary>
    /// Encoder
    /// This class will encode a file of commands written in team 5's ISA into the binary equivalents written in our ISA
    /// </summary>
    public static class Encoder
    {
        private static List<string> labels;         //This holds all of the labels that are intitialized in the remix file, the int in the same position in the labelPositions
                                                    //list holds the address of the label

        private static StreamReader input;          // Input file to read fromt
        private static BinaryWriter output;         // Output file to write to
        private static string intermediary;

        private static Dictionary<string, int> labelPositions;

        public static void Encode(string inputFilepath, string outputFilepath)
        {
            input = new StreamReader(inputFilepath);
            output = new BinaryWriter(File.OpenWrite(outputFilepath));

            labels = new List<string>();
            labelPositions = new Dictionary<string, int>();

            FirstPass();
            SecondPass();
            ThirdPass();

            input.Close();
            output.Close();
        }


        private static void FirstPass()
        {
            string[] arr;
            StringBuilder intermediaryBuilder = new StringBuilder();
            string line;

            while ((line = input.ReadLine()) != null)
            {
                line = line.Replace(", ", " ");

                // Remove comments
                if (line.Contains("//"))
                    line = line.Remove(line.IndexOf("//"), line.Length - line.IndexOf("//"));

                line = line.ToLower();

                // Remove whitespace and skip if line is only whitespace
                line = line.Trim();

                // Check for macros
                arr = line.Split(' ');
                if (arr[0].Equals("jmp"))
                {
                    // jmp macro
                    intermediaryBuilder.AppendLine("cmp rA rA");
                    intermediaryBuilder.AppendLine("jz " + arr[1]);
                }
                else if (line.Equals("") == false)
                    intermediaryBuilder.AppendLine(line);

            }

            intermediary = intermediaryBuilder.ToString();
        }

        private static void SecondPass()
        {
            string[] arr;

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(intermediary));
            StreamReader intermediaryStream = new StreamReader(ms);

            int counter = 0;
            string line;
            while ((line = intermediaryStream.ReadLine()) != null)
            {
                arr = line.Split(' ');

                string[] opcodes = Enum.GetNames(typeof(Opcode));
                for (int i = 0; i < opcodes.Length; i++)
                    opcodes[i] = opcodes[i].ToLower();

                if (opcodes.Contains(arr[0]) == false)
                {
                    // The opcode wasn't recognized. Could be label or just invalid instruction
                    if (arr[0].Substring(arr[0].Length - 1, 1).Equals(":"))
                    {
                        // Label
                        labels.Add(line.Substring(0, line.Length - 1));
                        labelPositions.Add(line.Substring(0, line.Length - 1), counter);
                        continue;
                    }
                    else
                    {
                        // ERROR
                        intermediaryStream.Close();
                        throw new Exception("Unrecognized command: " + arr[0]);
                    }

                }

                counter += 4;
            }

            intermediaryStream.Close();
        }

        private static void ThirdPass()
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(intermediary));
            StreamReader intermediaryStream = new StreamReader(ms);
            string line;

            int counter = 0;
            while ((line = intermediaryStream.ReadLine()) != null)
            {
                if (labels.Contains(line.Substring(0, line.Length - 1)))
                    continue;

                uint temp = BuildInstruction(line, counter);
                output.Write((byte)((temp & 0xFF000000) >> 24));
                output.Write((byte)((temp & 0x00FF0000) >> 16));
                output.Write((byte)((temp & 0x0000FF00) >> 8));
                output.Write((byte)((temp & 0x000000FF) >> 0));

                counter += 4;
            }
        }

        private static uint BuildInstruction(string input, int counterPosition)
        {
            string[] values = input.Split(' ');

            uint instruction = 0;

            Opcode opcode = OpcodeHelper.StringToOpcode(values[0]);
            instruction |=  (uint) opcode << 25;

            switch (values.Length)
            {
                case 5:
                    // Only cmpswp is this
                    switch(opcode)
                    {
                        case Opcode.CMPSW:
                            instruction |= 0x00000000;                                          // Immediate bit
                            instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                            instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op1
                            instruction |= (uint)RegisterHelper.StringToName(values[3]) << 0;   // Op2
                            instruction |= (uint)RegisterHelper.StringToName(values[4]) << 4;   // Op3
                            break;
                        default:
                            throw new Exception("Unknown Instruction");
                    }
                    break;
                case 4:
                    switch (opcode)
                    {
                        case Opcode.ADD:
                        case Opcode.ADDC:
                        case Opcode.SUB:
                        case Opcode.SUBB:
                        case Opcode.AND:
                        case Opcode.OR:
                        case Opcode.NOR:
                        case Opcode.XOR:
                        case Opcode.SHL:
                        case Opcode.SHR:
                        case Opcode.SHAR:
                        case Opcode.ROR:
                        case Opcode.ROL:
                        case Opcode.RORC:
                        case Opcode.ROLC:
                        // Atomics
                        case Opcode.ADDA:
                        case Opcode.SUBA:
                        case Opcode.ANDA:
                        case Opcode.ORA:
                        case Opcode.XORA:
                            if (values[3].Contains("#"))
                            {
                                // Arithmetic Immediate
                                int immediate = int.Parse(values[3].Substring(1));

                                instruction |= 0x01000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                                instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op1
                                instruction |= (uint)immediate & 0xFFFF << 0;   // Immediate Value
                            }
                            else
                            {
                                // Arithmetic Register
                                instruction |= 0x00000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                                instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op1
                                instruction |= (uint)RegisterHelper.StringToName(values[3]) << 0;   // Op2
                            }
                            break;
                        default:
                            throw new Exception("Unknown Instruction");
                    }      
                    break;
                case 3:
                    switch(opcode)
                    {
                        case Opcode.NEG:
                            if (values[2].Contains("#"))
                            {
                                int immediate = int.Parse(values[2].Substring(1));

                                instruction |= 0x01000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                                instruction |= (uint)immediate & 0xFFFF << 0;   // Immediate Value
                            }
                            else
                            {
                                instruction |= 0x00000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                                instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op1
                            }
                            break;
                        case Opcode.FETCH:
                        case Opcode.LOAD:
                        case Opcode.STOR:
                            if (values[2].Contains("#"))
                            {
                                int immediate = int.Parse(values[2].Substring(1));

                                instruction |= 0x01000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                                instruction |= (uint)immediate & 0xFFFFF                    << 0;   // Immediate Value
                            }
                            else
                            {
                                instruction |= 0x00000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                                instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op1
                            }
                            break;
                        case Opcode.CMP:
                            instruction |= 0x00000000;                                          // Immediate bit
                            instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Op1
                            instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op2
                            break;
                        case Opcode.MOV:
                            if (values[2].Contains("#"))
                            {
                                int immediate = int.Parse(values[2].Substring(1));

                                instruction |= 0x01000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                                instruction |= (uint)immediate & 0xFFFF << 0;   // Immediate Value
                            }
                            else
                            {
                                instruction |= 0x00000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Op1
                                instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op2
                            }
                            break;
                        case Opcode.SWAP:
                            instruction |= 0x00000000;                                          // Immediate bit
                            instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                            instruction |= (uint)RegisterHelper.StringToName(values[2]) << 16;  // Op1
                            break;
                        default:
                            throw new Exception("Unknown Instruction");
                    }
                    break;
                case 2:
                    switch(opcode)
                    {
                        case Opcode.PUSH:
                        case Opcode.POP:
                            if (values[1].Contains("#"))
                            {
                                int immediate = int.Parse(values[1].Substring(1));

                                instruction |= 0x01000000;                                          // Immediate bit
                                instruction |= (uint)immediate & 0xFFFF << 0;                       // Immediate Value
                            }
                            else
                            {
                                instruction |= 0x00000000;                                          // Immediate bit
                                instruction |= (uint)RegisterHelper.StringToName(values[1]) << 20;  // Destination
                            }
                            break;
                        case Opcode.LDA:
                            {
                                int immediate = int.Parse(values[1].Substring(1));

                                instruction |= 0x01000000;                                              // Immediate bit
                                instruction |= (uint)immediate & 0xFFFFF << 0;                          // Immediate Value
                            }
                            break;
                        case Opcode.JZ:
                        case Opcode.JNZ:
                        case Opcode.JG:
                        case Opcode.JGE:
                        case Opcode.JL:
                        case Opcode.JLE:
                        case Opcode.JA:
                        case Opcode.JAE:
                        case Opcode.JB:
                        case Opcode.JBE:
                            {
                                int immediate;

                                if (values[1][0].Equals('<'))
                                {
                                    string label = values[1].Substring(1, values[1].Length - 2);

                                    int labelPosition = labelPositions[label];          // Position of the label

                                    immediate = labelPosition - counterPosition;        // What is the offset of the label from the current counter
                                }
                                else if (values[1][0].Equals('#'))
                                {
                                    immediate = int.Parse(values[1].Substring(1));      // Just an immediate
                                }
                                else
                                    throw new Exception("Unknown character in jump instruction");

                                instruction |= 0x01000000;                                              // Immediate bit
                                instruction |= (uint)immediate & 0x1FFFFF << 0;                         // Immediate Value

                            }
                            break;
                        default:
                            throw new Exception("Unkown Instruction");
                    }
                    break;
                case 1:
                    switch(opcode)
                    {
                        case Opcode.HALT:
                        case Opcode.NOP:
                            // Does nothing
                            break;
                        default:
                            throw new Exception("Unkown Instruction");
                    }
                    break;
                default:
                    throw new Exception("Uknown Instruction with operands");
            }

            return instruction;
        }
    }
}
