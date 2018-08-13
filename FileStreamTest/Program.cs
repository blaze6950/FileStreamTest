using System;
using System.IO;
using System.Text;

namespace FileStreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TextWriteRead();
            //BinaryWriteRead();
            StreamWriterReader();
           // BinaryWriterReader();
           // TestStreamWriter();
        }

        static void TextWriteRead()
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(
                    "test.txt",
                    FileMode.Create,
                    FileAccess.ReadWrite,
                    FileShare.None
                    );

                string str = "Hello, world))))";
                byte[] buffer = Encoding.UTF8.GetBytes(str);

                stream.Write(buffer, 0, buffer.Length);

                Console.WriteLine("Position: " + stream.Position);

                stream.Seek(0, SeekOrigin.Begin);

                Console.WriteLine("Position: " + stream.Position);

                buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);

                string strRead = Encoding.UTF8.GetString(buffer);

                Console.WriteLine(strRead);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            
        }

        static void BinaryWriteRead()
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(
                    "test.dat",
                    FileMode.Create,
                    FileAccess.ReadWrite,
                    FileShare.None
                    );

                int i = 100;
                double d = 12.6;

                byte[] buffer = BitConverter.GetBytes(i);
                stream.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(d);
                stream.Write(buffer, 0, buffer.Length);

                Console.WriteLine("Position: " + stream.Position);

                stream.Seek(0, SeekOrigin.Begin);

                Console.WriteLine("Position: " + stream.Position);

                buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);
                //stream.Read(buffer, sizeof(Int32), sizeof(Double));

                int iRead = BitConverter.ToInt32(buffer, 0);
                double dRead = BitConverter.ToDouble(buffer, sizeof(Int32));

                Console.WriteLine("i: {0}, d: {1}", iRead, dRead);               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

        }

        static void StreamWriterReader()
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(
                    "test1.txt", 
                    FileMode.Create, 
                    FileAccess.ReadWrite,
                    FileShare.None
                    );

                StreamWriter writer = new StreamWriter(stream);
                //writer.AutoFlush = true;
                string s = "test string";
                writer.Write(s);
                writer.WriteLine(100);
                writer.WriteLine(234.56);

                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);

                StreamReader reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                   
                    string str = reader.ReadLine();
                    Console.WriteLine(str);
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
           
        }

        static void BinaryWriterReader()
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(
                    "test.dat",
                    FileMode.Create,
                    FileAccess.ReadWrite,
                    FileShare.None
                    );

                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(100);
                writer.Write(23.5);
                writer.Write("test");
                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);

                BinaryReader reader = new BinaryReader(stream);
                int i = reader.ReadInt32();
                double d = reader.ReadDouble();
                string str = reader.ReadString();

                Console.WriteLine("i : " + i);
                Console.WriteLine("d : " + d);
                Console.WriteLine("str : " + str);              

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        static void TestStreamWriter()
        {
            using(StreamWriter writer = new StreamWriter("test2.txt"))
            {
                writer.AutoFlush = false;
                string str = "test";
                string str1 = "Hello";

                writer.WriteLine(str);
                writer.WriteLine(str1);
            }
        }
    }
}
