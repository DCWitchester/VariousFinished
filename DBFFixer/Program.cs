using System;
using System.Linq;

namespace DBFFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            String ProgramPath = args[0];
            String RepairedFile = args[1];
            Byte errorByte = 0x00;
            Byte repairByte = 0x20;
            var ByteStream = System.IO.File.ReadAllBytes(ProgramPath);
            var RepairedStream = ByteStream.Select(item => item.Equals(errorByte) ? repairByte : item).ToArray();
            System.IO.File.WriteAllBytes(ProgramPath, RepairedStream);
        }
        public static void TryUncorupt()
        {
            var ByteStream = System.IO.File.ReadAllBytes(@"D:\encrypt\FP.dbf");
            var RepairedStream = ByteStream.Select(item => (byte)(item + 0xFE - 0x7A + 0x41)).ToArray();
            System.IO.File.WriteAllBytes(@"D:\encrypt\FPFixed.dbf",RepairedStream);
            String t = "";
        }
    }
}
