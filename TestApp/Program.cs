using System;
using Simulator;
using Simulator.Instructions.arithmetic;
using Simulator.Instructions.logical;
using Simulator.Instructions.control;
using Simulator.Instructions.storage;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Create memory
            //Memory memory = new Memory(0x100000);

            //// Create the CPU
            //CPU cpu = new CPU(memory);


            Register dest = new Register();
            Register op1 = new Register();
            Register op2 = new Register();
            Register flag = new Register();
            Register pc1 = new Register();
            Register pc2 = new Register();
            Register re = new Register();
            Register rf = new Register();

            Memory memory = new Memory(8, false);

            ALU alu = new ALU(flag);
            short immValue = 1;

            op1.Value = 0x7FFF;
            op2.Value = (ushort) immValue;
            addcRegister add = new addcRegister(dest, op1, op2, flag, alu);
            addcImmediate add2 = new addcImmediate(dest, op1, (ushort) immValue, flag, alu);
            addRegister add3 = new addRegister(dest, op1, op2, flag, alu);
            addImmediate add4 = new addImmediate(dest, op1, (ushort) immValue, flag, alu);

            // Test adding with carry
            flag.Value = 1;
            add.Execute();
            Console.WriteLine("Addc reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            flag.Value = 1;
            add2.Execute();
            Console.WriteLine("Addc reg {0:X4} and imm {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, immValue, dest.Value, flag.Value);

            // Test add
            flag.Value = 0;
            add3.Execute();
            Console.WriteLine("Add reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            flag.Value = 0;
            add4.Execute();
            Console.WriteLine("Add reg {0:X4} and imm {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, immValue, dest.Value, flag.Value);


            // Test subtracting greater than
            uint op3 = 0x00010000;
            uint op4 = 0x00000001;
            op1.Value = (ushort) (op3 & 0x0000FFFF); // Get the lower order bits of the first operand
            op2.Value = (ushort) (op4 & 0x0000FFFF); // Get the lower order bits of the second operand
            subRegister testForSub = new subRegister(dest, op1, op2, flag, alu); // Sub the lower bits to get the carry
            testForSub.Execute();
            ushort lowerResult = dest.Value;
            Console.WriteLine("Sub reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, lowerResult, flag.Value);

            op1.Value = (ushort)((op3 & 0xFFFF0000) >> 16);    // Get the upper order bits of the first operand
            op2.Value = (ushort)((op4 & 0xFFFF0000) >> 16);    // Get the upper order bits of the second operand

            subbRegister sub = new subbRegister(dest, op1, op2, flag, alu);  // Subtract with borrow
            sub.Execute();
            ushort upperResult = dest.Value;

            Console.WriteLine("Subb {0:X8} and {1:X8} = {2:X4}{3:X4}, FLAGS {4:X}", op3, op4, upperResult, lowerResult, flag.Value);

            Console.WriteLine();

            // Test shifting
            op1.Value = 0xFFFF;
            op2.Value = 1;
            shlRegister shl = new shlRegister(dest, op1, op2, flag, alu);
            shl.Execute();
            Console.WriteLine("Shl reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            shrRegister shr = new shrRegister(dest, op1, op2, flag, alu);
            shr.Execute();
            Console.WriteLine("Shr reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            sharRegister shar = new sharRegister(dest, op1, op2, flag, alu);
            shar.Execute();
            Console.WriteLine("Shar reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);


            // Test Rotates
            op1.Value = 0x00FF;
            op2.Value = 4;
            rorRegister ror = new rorRegister(dest, op1, op2, flag, alu);
            ror.Execute();
            Console.WriteLine("Ror reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            rolRegister rol = new rolRegister(dest, op1, op2, flag, alu);
            rol.Execute();
            Console.WriteLine("Rol reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            flag.Value = 0;
            rorcRegister rorc = new rorcRegister(dest, op1, op2, flag, alu);
            rorc.Execute();
            Console.WriteLine("Rorc reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            flag.Value = 0;
            op1.Value = 0xFF00;
            rolcRegister rolc = new rolcRegister(dest, op1, op2, flag, alu);
            rolc.Execute();
            Console.WriteLine("Rolc reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            // Test Logical
            Console.WriteLine();
            op1.Value = 0x0F0F;
            op2.Value = 0x03F3;

            andRegister and = new andRegister(dest, op1, op2, flag, alu);
            orRegister or = new orRegister(dest, op1, op2, flag, alu);
            xorRegister xor = new xorRegister(dest, op1, op2, flag, alu);
            norRegister nor = new norRegister(dest, op1, op2, flag, alu);
            negRegister neg = new negRegister(dest, op1, flag, alu);

            and.Execute();
            Console.WriteLine("And reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            or.Execute();
            Console.WriteLine("Or reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            xor.Execute();
            Console.WriteLine("Xor reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            nor.Execute();
            Console.WriteLine("Nor reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            neg.Execute();
            Console.WriteLine("Neg reg {0:X4} = {1:X4}, FLAGS {2:X}", op1.Value, dest.Value, flag.Value);


            // Test LDA
            uint immLDA = 0x0003F3F3;
            lda lda = new lda(re, rf, immLDA);
            lda.Execute();

            Console.WriteLine("LDA imm {0:X8} = {1:X4}{2:X4}", immLDA, re.Value, rf.Value);

            Console.WriteLine();

            // Test Jumps
            op1.Value = 1;
            subImmediate tJumpSub = new subImmediate(dest, op1, 1, flag, alu);
            tJumpSub.Execute();
            pc1.Value = 0;
            pc2.Value = 0;
            jz jz = new jz(flag, pc1, pc2, re, rf);
            jz.Execute();
            Console.WriteLine("jz {0:X4}{1:X4}", pc1.Value, pc2.Value);
            pc1.Value = 0;
            pc2.Value = 0;
            jnz jnz = new jnz(flag, pc1, pc2, re, rf);
            jnz.Execute();
            Console.WriteLine("jnz {0:X4}{1:X4}", pc1.Value, pc2.Value);

            op1.Value = 1;
            op2.Value = 0;
            cmp tJA = new cmp(op1, op2, alu);
            tJA.Execute();
            pc1.Value = 0;
            pc2.Value = 0;
            ja ja = new ja(flag, pc1, pc2, re, rf);
            ja.Execute();
            Console.WriteLine("ja {0:X4}{1:X4}", pc1.Value, pc2.Value);
            pc1.Value = 0;
            pc2.Value = 0;
            jae jae = new jae(flag, pc1, pc2, re, rf);
            jae.Execute();
            Console.WriteLine("jae {0:X4}{1:X4}", pc1.Value, pc2.Value);

            pc1.Value = 0;
            pc2.Value = 0;
            jb jb = new jb(flag, pc1, pc2, re, rf);
            jb.Execute();
            Console.WriteLine("jb {0:X4}{1:X4}", pc1.Value, pc2.Value);
            pc1.Value = 0;
            pc2.Value = 0;
            jbe jbe = new jbe(flag, pc1, pc2, re, rf);
            jbe.Execute();
            Console.WriteLine("jbe {0:X4}{1:X4}", pc1.Value, pc2.Value);

            pc1.Value = 0;
            pc2.Value = 0;
            jg jg = new jg(flag, pc1, pc2, re, rf);
            jg.Execute();
            Console.WriteLine("jg {0:X4}{1:X4}", pc1.Value, pc2.Value);
            pc1.Value = 0;
            pc2.Value = 0;
            jge jge = new jge(flag, pc1, pc2, re, rf);
            jge.Execute();
            Console.WriteLine("jge {0:X4}{1:X4}", pc1.Value, pc2.Value);

            pc1.Value = 0;
            pc2.Value = 0;
            jl jl = new jl(flag, pc1, pc2, re, rf);
            jl.Execute();
            Console.WriteLine("jl {0:X4}{1:X4}", pc1.Value, pc2.Value);
            pc1.Value = 0;
            pc2.Value = 0;
            jle jle = new jle(flag, pc1, pc2, re, rf);
            jle.Execute();
            Console.WriteLine("jle {0:X4}{1:X4}", pc1.Value, pc2.Value);


            // Test load and store
            op1.Value = 0x1234;
            storImmediate stor = new storImmediate(memory, 0, op1);
            stor.Execute();

            loadImmediate load = new loadImmediate(memory, op2, 0);
            load.Execute();

            // Test push and pop
            lda pushLDA = new lda(re, rf, 6);
            pushLDA.Execute();
            pushImmediate push = new pushImmediate(memory, 0x5678, re, rf);
            push.Execute();
            pushImmediate push2 = new pushImmediate(memory, 0x9ABC, re, rf);
            push2.Execute();

            pop pop = new pop(memory, op1, re, rf);
            pop.Execute();
            pop pop2 = new pop(memory, op2, re, rf);
            pop2.Execute();

        }
    }
}
