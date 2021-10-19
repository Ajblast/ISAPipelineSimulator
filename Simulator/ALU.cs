using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class ALU
    {
        public Register dest;
        public Register op1;
        public Register op2;
        private Register flag;

        public ALU(Register flag)
        {
            dest = new Register();
            op1 = new Register();
            op2 = new Register();
            this.flag = flag;
        }
        public void Add() 
        {
            // Get the carry in flag
            int carryIn = flag.Value & 0x1;

            int result = 0;

            // Do a full adder for every single bit to account for the carry in
            // Shift the input values to the very first bit because we just want to do single bit things
            for (int i = 0; i < 16; i++)
            {
                int aBit = (op1.Value >> i) & 0x1;    // Get single bit
                int bBit = (op2.Value >> i) & 0x1;    // Get single bit

                // Determine the sum bit
                int sum = carryIn ^ (aBit ^ bBit);

                // Determine the carry out bit
                carryIn = (aBit & bBit) | (bBit & carryIn) | (aBit & carryIn);

                // Or the result with the sum shifted because we don't actually have full adders
                result = result | sum << i;
            }

            // Set the final value
            dest.Value = (ushort)result;

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | carryIn);

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Set the overflow flag
            if ((op1.Value & 0x8000) == (op2.Value & 0x8000) && (op1.Value & 0x8000) != (dest.Value & 0x8000))
                flag.Value = (ushort)(flag.Value & ~(1 << 3) | (1 << 3));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 3));   // Clear the overflow flag if there wasn't an overflow

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Sub() 
        {
            // Get the carry in flag
            int carryIn = flag.Value & 0x1;

            int result = 0;

            // Do a full adder for every single bit to account for the carry in
            // Shift the input values to the very first bit because we just want to do single bit things
            for (int i = 0; i < 16; i++)
            {
                int aBit = (op1.Value >> i) & 0x1;    // Get single bit
                int bBit = (op2.Value >> i) & 0x1;    // Get single bit

                // Determine the sum bit
                int diff = carryIn ^ (aBit ^ bBit);

                // Determine the carry out bit
                carryIn = ((~aBit & 1) & carryIn) | ((~aBit & 1) & bBit) | (bBit & carryIn);

                // Or the result with the sum shifted because we don't actually have full adders
                result = result | diff << i;
            }

            // Set the final value
            dest.Value = (ushort)result;

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | carryIn);

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Set the overflow flag
            if ((op1.Value & 0x8000) == (op2.Value & 0x8000) && (op1.Value & 0x8000) != (dest.Value & 0x8000))
                flag.Value = (ushort)(flag.Value & ~(1 << 3) | (1 << 3));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 3));   // Clear the overflow flag if there wasn't an overflow

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }

        public void Rol()
        {
            dest.Value = op1.Value;

            for (ushort i = 0; i < op2.Value; i++)
            {
                // Store the most significant bit
                ushort bit = (ushort)((uint)(op1.Value & 0x8000) >> 15);

                // Shift the register to the left
                dest.Value = (ushort)(dest.Value << 1);

                // Set the least significant as the old most significant bit
                dest.Value |= bit;
            }

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Rolc()
        {
            dest.Value = op1.Value;

            for (ushort i = 0; i < op2.Value; i++)
            {
                // Store the most significant bit
                ushort bit = (ushort)((uint)(op1.Value & 0x8000) >> 15);

                // Shift the register to the left
                dest.Value = (ushort)(dest.Value << 1);

                // Set the least significant as the carry bit
                dest.Value |= (ushort) (flag.Value & 0x1);

                // Set the carry bit as the stored bit
                flag.Value = (ushort)(flag.Value & ~(1 << 0) | bit);
            }

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Ror()
        {
            dest.Value = op1.Value;

            for (ushort i = 0; i < op2.Value; i++)
            {
                // Store the least significant bit
                ushort bit = (ushort)((op1.Value & 0x0001) << 15);

                // Shift the register to the right
                dest.Value = (ushort)((uint)dest.Value >> 1);

                // Set the most significant as the old least significant bit
                dest.Value |= bit;
            }

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Rorc() 
        {
            dest.Value = op1.Value;

            for (ushort i = 0; i < op2.Value; i++)
            {
                // Store the least significant bit
                ushort bit = (ushort)(op1.Value & 0x0001);

                // Shift the register to the right
                dest.Value = (ushort)((uint)dest.Value >> 1);

                // Set the most significant as the carry bit
                dest.Value |= (ushort)((flag.Value & 0x1) << 15);

                // Set the carry bit as the stored bit
                flag.Value = (ushort)(flag.Value & ~(1 << 0) | bit);
            }

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }

        public void Shl()
        {
            // Set the initial value
            dest.Value = op1.Value;

            // Shift left for the amount of times in the operand
            for (short i = 0; i < op2.Value; i++)
            {
                // Set the carry flag to be the bit that is about to be shifted out
                flag.Value = (ushort)(flag.Value & ~(1 << 0) | (dest.Value >> 15) & 0x1);

                dest.Value = (ushort) (dest.Value << 1);
            }

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Shr()
        {
            // Set the initial value
            dest.Value = op1.Value;

            // Shift left for the amount of times in the operand
            for (short i = 0; i < op2.Value; i++)
            {
                // Set the carry flag to be the bit that is about to be shifted out
                flag.Value = (ushort)(flag.Value & ~(1 << 0) | (dest.Value & 0x1));

                dest.Value = (ushort)((uint) dest.Value >> 1);
            }

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Shar()
        {  
            // Set the initial value
            dest.Value = op1.Value;

            // Shift left for the amount of times in the operand
            for (short i = 0; i < op2.Value; i++)
            {
                // Set the carry flag to be the bit that is about to be shifted out
                flag.Value = (ushort)(flag.Value & ~(1 << 0) | (dest.Value & 0x1));

                dest.Value = (ushort)((short)dest.Value >> 1);
            }

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }

        public void And() 
        {
            // And the two values
            dest.Value = (ushort)(op1.Value & op2.Value);

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Set the overflow flag
            if ((op1.Value & 0x8000) == (op2.Value & 0x8000) && (op1.Value & 0x8000) != (dest.Value & 0x8000))
                flag.Value = (ushort)(flag.Value & ~(1 << 3) | (1 << 3));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 3));   // Clear the overflow flag if there wasn't an overflow

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Or() 
        {
            // Or the two values
            dest.Value = (ushort)(op1.Value | op2.Value);

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Nor()
        {
            // Or the two values
            dest.Value = (ushort) ~(op1.Value | op2.Value);

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
            flag.Value &= 0x1f;
        }
        public void Xor()
        {
            // Xor the two values
            dest.Value = (ushort)(op1.Value ^ op2.Value);

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Neg() 
        {
            // Negate the value
            dest.Value = (ushort) ~op1.Value;

            // Set the carry flag
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (ushort)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (ushort)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (ushort)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (ushort)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (ushort)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
    }
}
