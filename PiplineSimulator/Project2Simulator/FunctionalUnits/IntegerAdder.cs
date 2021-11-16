using Project2Simulator.Instructions;
using Project2Simulator.Registers;

namespace Project2Simulator.FunctionalUnits
{
    public class IntegerAdder : AdderUnit
    {
        private readonly uint overflowMask = 0x80000000;

        public IntegerAdder(THECommonDataBus bus, CoreID core) : base(bus, core)
        {
        }

        public override bool Cycle()
        {
            switch (opcode)
            {
                case Opcode.ADD:
                    op3.Value = (uint)(op3.Value & ~(1 << 0) | 0);
                    Add();
                    return true;
				case Opcode.ADDC:
                    Add();
                    return true;
				case Opcode.SUB:
                    op3.Value = (uint)(op3.Value & ~(1 << 0) | 0);
                    Sub();
                    return true;
                case Opcode.SUBB:
                    // OP2 + CF
                    // Add the second op and the carry flag
                    op1.Value = op2.Value;
                    op2.Value = 0;
                    Add();

                    // OP1 - (OP2 + CF)
                    // Sub the first op and the result of the carry addition
                    op1.Value = op1.Value;
                    op2.Value = dest1.Value;
                    op3.Value = (uint)(op3.Value & ~(1 << 0) | 0);

                    Sub();
                    return true;
                case Opcode.AND:
                    And();
                    return true;
				case Opcode.OR:
                    Or();
                    return true;
				case Opcode.NOR:
                    Nor();
                    return true;
				case Opcode.NEG:
                    Neg();
                    return true;
				case Opcode.XOR:
                    Xor();
                    return true;
				case Opcode.SHL:
                    Shl();
                    return true;
				case Opcode.SHR:
                    Shr();
                    return true;
				case Opcode.SHAR:
                    Shar();
                    return true;
				case Opcode.ROR:
                    Ror();
                    return true;
				case Opcode.ROL:
                    Rol();
                    return true;
				case Opcode.RORC:
                    Rorc();
                    return true;
				case Opcode.ROLC:
                    Rolc();
                    return true;
				case Opcode.CMP:
                    op3.Value = (uint)(op3.Value & ~(1 << 0) | 0);
                    Sub();
                    return true;
                default:
                    return true;
            }
            throw new System.NotImplementedException();
        }

        private void Add()
        {
            // Get the carry in op3
            uint carryIn = op3.Value & 0x1;

            uint result = 0;

            // Do a full adder for every single bit to account for the carry in
            // Shift the input values to the very first bit because we just want to do single bit things
            for (int i = 0; i < RegisterValue.DataSize; i++)
            {
                uint aBit = (op1.Value >> i) & 0x1;    // Get single bit
                uint bBit = (op2.Value >> i) & 0x1;    // Get single bit

                // Determine the sum bit
                uint sum = carryIn ^ (aBit ^ bBit);

                // Determine the carry out bit
                carryIn = (aBit & bBit) | (bBit & carryIn) | (aBit & carryIn);

                // Or the result with the sum shifted because we don't actually have full adders
                result |= sum << i;
            }

            // Set the final value
            dest1.Value = result;

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | carryIn);

            // Set the zero bit
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Set the overflow dest2
            if ((op1.Value & overflowMask) == (op2.Value & overflowMask) && (op1.Value & overflowMask) != (dest1.Value & overflowMask))
                dest2.Value = (uint)(dest2.Value & ~(1 << 3) | (1 << 3));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 3));   // Clear the overflow dest2 if there wasn't an overflow

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Sub()
        {
            // Get the carry in op3
            uint carryIn = op3.Value & 0x1;

            uint result = 0;

            // Do a full adder for every single bit to account for the carry in
            // Shift the input values to the very first bit because we just want to do single bit things
            for (int i = 0; i < RegisterValue.DataSize; i++)
            {
                uint aBit = (op1.Value >> i) & 0x1;    // Get single bit
                uint bBit = (op2.Value >> i) & 0x1;    // Get single bit

                // Determine the sum bit
                uint diff = carryIn ^ (aBit ^ bBit);

                // Determine the carry out bit
                carryIn = ((~aBit & 1) & carryIn) | ((~aBit & 1) & bBit) | (bBit & carryIn);

                // Or the result with the sum shifted because we don't actually have full adders
                result |= diff << i;
            }

            // Set the final value
            dest1.Value = result;

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | carryIn);

            // Set the zero bit
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Set the overflow dest2
            if ((op1.Value & overflowMask) == (op2.Value & overflowMask) && (op1.Value & overflowMask) != (dest1.Value & overflowMask))
                dest2.Value = (uint)(dest2.Value & ~(1 << 3) | (1 << 3));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 3));   // Clear the overflow dest2 if there wasn't an overflow

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }

        private void Rol()
        {
            dest1.Value = op1.Value;

            for (uint i = 0; i < op2.Value; i++)
            {
                // Store the most significant bit
                uint bit = (op1.Value & overflowMask) >> (RegisterValue.DataSize - 1);

                // Shift the register to the left
                dest1.Value <<= 1;

                // Set the least significant as the old most significant bit
                dest1.Value |= bit;
            }

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | 0);

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Rolc()
        {
            dest1.Value = op1.Value;

            for (uint i = 0; i < op2.Value; i++)
            {
                // Store the most significant bit
                uint bit = (op1.Value & overflowMask) >> (RegisterValue.DataSize - 1);

                // Shift the register to the left
                dest1.Value <<= 1;

                // Set the least significant as the carry bit
                dest1.Value |= (op3.Value & 0x1);

                // Set the carry bit as the stored bit
                op3.Value = (uint)(op3.Value & ~(1 << 0) | bit);
            }

            dest2.Value = op3.Value;

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Ror()
        {
            dest1.Value = op1.Value;

            for (uint i = 0; i < op2.Value; i++)
            {
                // Store the least significant bit
                uint bit = (uint)((op1.Value & 0x0001) << (RegisterValue.DataSize - 1));

                // Shift the register to the right
                dest1.Value = (uint)((uint)dest1.Value >> 1);

                // Set the most significant as the old least significant bit
                dest1.Value |= bit;
            }

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | 0);

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Rorc()
        {
            dest1.Value = op1.Value;

            for (uint i = 0; i < op2.Value; i++)
            {
                // Store the least significant bit
                uint bit = (uint)(op1.Value & 0x0001);

                // Shift the register to the right
                dest1.Value = (uint)((uint)dest1.Value >> 1);

                // Set the most significant as the carry bit
                dest1.Value |= (uint)((op3.Value & 0x1) << (RegisterValue.DataSize - 1));

                // Set the carry bit as the stored bit
                op3.Value = (uint)(op3.Value & ~(1 << 0) | bit);
            }

            dest2.Value = op3.Value;

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }

        private void Shl()
        {
            // Set the initial value
            dest1.Value = op1.Value;

            // Shift left for the amount of times in the operand
            for (int i = 0; i < op2.Value; i++)
            {
                // Set the carry op3 to be the bit that is about to be shifted out
                op3.Value = (uint)(op3.Value & ~(1 << 0) | (dest1.Value >> (RegisterValue.DataSize - 1)) & 0x1);

                dest1.Value = (uint)(dest1.Value << 1);
            }

            dest2.Value = op3.Value;

            // Set the zero bit
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Shr()
        {
            // Set the initial value
            dest1.Value = op1.Value;

            // Shift left for the amount of times in the operand
            for (int i = 0; i < op2.Value; i++)
            {
                // Set the carry op3 to be the bit that is about to be shifted out
                op3.Value = (uint)(op3.Value & ~(1 << 0) | (dest1.Value & 0x1));

                dest1.Value = (uint)((uint)dest1.Value >> 1);
            }

            dest2.Value = op3.Value;

            // Set the zero bit
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Shar()
        {
            // Set the initial value
            dest1.Value = op1.Value;

            // Shift left for the amount of times in the operand
            for (int i = 0; i < op2.Value; i++)
            {
                // Set the carry op3 to be the bit that is about to be shifted out
                op3.Value = (uint)(op3.Value & ~(1 << 0) | (dest1.Value & 0x1));

                dest1.Value = (uint)((int)dest1.Value >> 1);
            }

            dest2.Value = op3.Value;

            // Set the zero bit
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }

        private void And()
        {
            // And the two values
            dest1.Value = (uint)(op1.Value & op2.Value);

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | 0);

            // Set the zero bit
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Set the overflow dest2
            if ((op1.Value & overflowMask) == (op2.Value & overflowMask) && (op1.Value & overflowMask) != (dest1.Value & overflowMask))
                dest2.Value = (uint)(dest2.Value & ~(1 << 3) | (1 << 3));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 3));   // Clear the overflow dest2 if there wasn't an overflow

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Or()
        {
            // Or the two values
            dest1.Value = (uint)(op1.Value | op2.Value);

            // Set the carry op3
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | 0);

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Nor()
        {
            // Or the two values
            dest1.Value = ~(op1.Value | op2.Value);

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | 0);

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Xor()
        {
            // Xor the two values
            dest1.Value = op1.Value ^ op2.Value;

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | 0);

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }
        private void Neg()
        {
            // Negate the value
            dest1.Value = ~op1.Value;

            // Set the carry dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 0) | 0);

            // Set the zero dest2
            if (dest1.Value == 0)
                dest2.Value = (uint)(dest2.Value & ~(1 << 1) | (1 << 1));
            else
                dest2.Value = (uint)(dest2.Value & ~(1 << 1));   // Clear the zero dest2 if the result wasn't zero

            // Clear the Equality dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 2));

            // Clear the overflow dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 3));

            // Set the sign dest2
            dest2.Value = (uint)(dest2.Value & ~(1 << 4) | ((dest1.Value >> (RegisterValue.DataSize - 1)) << 4));

            dest2.Value &= 0x1f;
        }

    }

}

