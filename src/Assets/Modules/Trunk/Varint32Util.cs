

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
        // Varint32 编码
        public static byte[] EncodeVarint32Header(uint bodyLen)
        {
            var buffer = new byte[5];
            int pos = 0;
            while (bodyLen >= 0x80) // 报文的高8位指示报文是否用到后续字节
            {
                buffer[pos++] = (byte)(bodyLen | 0x80); // 取值，并保证报文高8位为1
                bodyLen >>= 7;
            }
            // 小端序放置数据
            buffer[pos++] = (byte)bodyLen;
            // 重置头部长度
            Array.Resize(ref buffer, pos);
            return buffer;
        }

        // Varint32 解码
        public static int DecodeVarint32Header(byte[] buffer, out int bytesRead)
        {
            bytesRead = 0;
            int result = 0;
            int shift = 0;
            while (true)
            {
                byte b = buffer[bytesRead++];
                result |= (b & 0x7F) << shift; // 读取一个字节并，按小端序移位
                if ((b & 0x80) == 0) // 高8位指示是否有后续字节
                    break;
                shift += 7;
            }
            return result;
        }
    }
}