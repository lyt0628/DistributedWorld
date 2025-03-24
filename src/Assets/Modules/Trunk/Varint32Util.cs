

using System;

namespace QS.Network
{

    /// <summary>
    ///  * BEFORE ENCODE (300 bytes)       AFTER ENCODE (302 bytes)
    /// * +---------------+               +--------+---------------+
    /// * | Protobuf Data |-------------->| Length | Protobuf Data |
    /// * |  (300 bytes)  |               | 0xAC02 |  (300 bytes)  |
    /// * +---------------+               +--------+---------------+
    /// </summary>
    public static class Varint32Util
    {
        // Varint32 ����
        public static byte[] EncodeVarint32Header(uint bodyLen)
        {
            var buffer = new byte[5];
            int pos = 0;
            while (bodyLen >= 0x80) // ���ĵĸ�8λָʾ�����Ƿ��õ������ֽ�
            {
                buffer[pos++] = (byte)(bodyLen | 0x80); // ȡֵ������֤���ĸ�8λΪ1
                bodyLen >>= 7;
            }
            // С�����������
            buffer[pos++] = (byte)bodyLen;
            // ����ͷ������
            Array.Resize(ref buffer, pos);
            return buffer;
        }

        // Varint32 ����
        public static int DecodeVarint32Header(byte[] buffer, out int bytesRead)
        {
            bytesRead = 0;
            int result = 0;
            int shift = 0;
            while (true)
            {
                byte b = buffer[bytesRead++];
                result |= (b & 0x7F) << shift; // ��ȡһ���ֽڲ�����С������λ
                if ((b & 0x80) == 0) // ��8λָʾ�Ƿ��к����ֽ�
                    break;
                shift += 7;
            }
            return result;
        }
    }
}