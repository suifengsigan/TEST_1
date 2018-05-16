namespace NXOpen.Utilities
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class ResourceUtilities
    {
        private byte[] assemblyData;
        private static int BLANK_PADDING = 0x20;
        private static int BLOCK_HEADER_SIZE = 11;
        private SHA1 hashAlgorithm;
        private static int PADDING_SIZE = 0x200;
        private static int RESOURCE_BLOCK_SIZE = ((BLOCK_HEADER_SIZE * 2) + PADDING_SIZE);
        private int resourceOffset;

        public ResourceUtilities(byte[] assemblyData)
        {
            this.resourceOffset = this.getNXResource(assemblyData);
            if (this.resourceOffset == -1)
            {
                throw new MissingResourceException("Assembly has not been compiled with an NX resource bundle");
            }
            this.assemblyData = assemblyData;
            this.hashAlgorithm = new SHA1CryptoServiceProvider();
        }

        private bool checkBlock(byte[] byteArray, int offset)
        {
            return ((((((byteArray[offset] == 0x4e) && (byteArray[offset + 1] == 0x58)) && ((byteArray[offset + 2] == 0x41) && (byteArray[offset + 3] == 0x55))) && (((byteArray[offset + 4] == 0x54) && (byteArray[offset + 5] == 0x48)) && ((byteArray[offset + 6] == 0x42) && (byteArray[offset + 7] == 0x4c)))) && ((byteArray[offset + 8] == 0x4f) && (byteArray[offset + 9] == 0x43))) && (byteArray[offset + 10] == 0x4b));
        }

        public byte[] computeHash()
        {
            int num = this.getEmbeddedDataLen();
            int inputCount = ((this.resourceOffset + BLOCK_HEADER_SIZE) + 1) + num;
            this.hashAlgorithm.TransformBlock(this.assemblyData, 0, inputCount, this.assemblyData, 0);
            this.hashAlgorithm.TransformFinalBlock(this.assemblyData, this.resourceOffset + RESOURCE_BLOCK_SIZE, (this.assemblyData.Length - RESOURCE_BLOCK_SIZE) - this.resourceOffset);
            return this.hashAlgorithm.Hash;
        }

        public byte[] getEmbeddedData()
        {
            int count = this.getEmbeddedDataLen();
            int srcOffset = (this.resourceOffset + BLOCK_HEADER_SIZE) + 1;
            byte[] dst = new byte[count];
            Buffer.BlockCopy(this.assemblyData, srcOffset, dst, 0, count);
            return dst;
        }

        private int getEmbeddedDataLen()
        {
            int num = this.resourceOffset + BLOCK_HEADER_SIZE;
            int num2 = this.assemblyData[num++];
            if (num2 == BLANK_PADDING)
            {
                int num3 = 0;
                while (num3 < (PADDING_SIZE - 1))
                {
                    if (this.assemblyData[num + num3] != BLANK_PADDING)
                    {
                        break;
                    }
                    num3++;
                }
                if (num3 == (PADDING_SIZE - 1))
                {
                    throw new MissingResourceException("NX Resource bundle is empty");
                }
            }
            if ((num2 < 0) || (num2 > RESOURCE_BLOCK_SIZE))
            {
                throw new MissingResourceException("Invalid resource size" + num2);
            }
            return num2;
        }

        private int getNXResource(byte[] byteArray)
        {
            int num = byteArray.Length - RESOURCE_BLOCK_SIZE;
            for (int i = 0; i <= num; i++)
            {
                if (this.checkBlock(byteArray, i) && this.checkBlock(byteArray, (i + BLOCK_HEADER_SIZE) + PADDING_SIZE))
                {
                    return i;
                }
            }
            return -1;
        }

        public byte[] getSignature()
        {
            int num = this.getEmbeddedDataLen();
            int srcOffset = ((this.resourceOffset + BLOCK_HEADER_SIZE) + 1) + num;
            int count = this.assemblyData[srcOffset++];
            byte[] dst = new byte[count];
            Buffer.BlockCopy(this.assemblyData, srcOffset, dst, 0, count);
            return dst;
        }

        public SignatureType getSignatureType()
        {
            string strA = Encoding.ASCII.GetString(this.getEmbeddedData())[1].ToString() + " ";
            if (string.Compare(strA, "1 ", false) == 0)
            {
                return SignatureType.NORMAL_TYPE;
            }
            if (string.Compare(strA, "2 ", false) == 0)
            {
                return SignatureType.ALLIANCE_TYPE;
            }
            if (string.Compare(strA, "3 ", false) == 0)
            {
                return SignatureType.SNAP_TYPE;
            }
            if (string.Compare(strA, "4 ", false) != 0)
            {
                throw new MissingResourceException("Embedded Data has wrong signature specification.");
            }
            return SignatureType.BOTH_TYPE;
        }

        public void setEmbeddedData(byte[] embeddedData)
        {
            int length = embeddedData.Length;
            if (length > 0xff)
            {
                length = 0xff;
            }
            int dstOffset = this.resourceOffset + BLOCK_HEADER_SIZE;
            this.assemblyData[dstOffset++] = (byte) length;
            Buffer.BlockCopy(embeddedData, 0, this.assemblyData, dstOffset, length);
        }

        public void setSignature(byte[] signature)
        {
            if (signature.Length > 0xff)
            {
                throw new ArgumentOutOfRangeException("Illegal signature length " + signature.Length);
            }
            int num = this.getEmbeddedDataLen();
            int dstOffset = ((this.resourceOffset + BLOCK_HEADER_SIZE) + 1) + num;
            this.assemblyData[dstOffset++] = (byte) signature.Length;
            Buffer.BlockCopy(signature, 0, this.assemblyData, dstOffset, signature.Length);
        }

        public enum SignatureType
        {
            NORMAL_TYPE,
            ALLIANCE_TYPE,
            SNAP_TYPE,
            BOTH_TYPE,
            MINI_SNAP
        }
    }
}

