/* Author; Seth Bowden */
namespace Project2Simulator.ReorderBuffers
{
	public class ReorderBufferID
	{
        public int BufferID {get; private set; }

        public ReorderBufferID(int bufferID)
        {
            BufferID = bufferID;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (((ReorderBufferID) obj).BufferID == this.BufferID)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}

