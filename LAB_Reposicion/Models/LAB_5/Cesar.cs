using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_Reposicion.Models.LAB_5
{
    public class Cesar
    {
        private static Dictionary<int, int> alfa = new Dictionary<int, int>();
        private static string routeDirectory = Environment.CurrentDirectory;

        static void ObtainsDictonary(string key, int opc)
        {
            alfa = new Dictionary<int, int>();
            key = key.ToUpper();
            var contOriginal = 65;
            var contNuevo = 65;

            if (opc == 1)
            {
                do
                {
                    if (key.Length > 0)
                    {
                        if (!alfa.ContainsValue(key[0]))
                        {
                            alfa.Add(contOriginal, key[0]);
                            contOriginal++;
                        }
                        key = key.Substring(1, key.Length - 1);
                    }
                    else
                    {
                        if (!alfa.ContainsValue(contNuevo))
                        {
                            alfa.Add(contOriginal, contNuevo);
                            contOriginal++;
                        }
                        contNuevo++;
                    }
                } while (contOriginal < 91);
            }
            else
            {
                do
                {
                    if (key.Length > 0)
                    {
                        if (!alfa.ContainsKey(key[0]))
                        {
                            alfa.Add(key[0], contOriginal);
                            contOriginal++;
                        }
                        key = key.Substring(1, key.Length - 1);
                    }
                    else
                    {
                        if (!alfa.ContainsKey(contNuevo))
                        {
                            alfa.Add(contNuevo, contOriginal);
                            contOriginal++;
                        }
                        contNuevo++;
                    }
                } while (contOriginal < 91);
            }
        }

        public static void Cifrar(ValuesDataTaken info)
        {
            Directory.CreateDirectory("temp");
            ObtainsDictonary(info.key1, 1);

            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var bufferLength = 10000;
                        var byteBuffer = new byte[bufferLength];
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);

                            foreach (var caracter in byteBuffer)
                            {
                                var actual = Convert.ToInt32(caracter);
                                if (actual >= 65 && actual <= 90)
                                {
                                    writer.Write((byte)alfa[actual]);
                                }
                                else if (actual >= 97 && actual <= 122)
                                {
                                    writer.Write((byte)(alfa[actual - 32] + 32));
                                }
                                else
                                {
                                    writer.Write(caracter);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Decifrar(ValuesDataTaken info)
        {
            Directory.CreateDirectory("temp");
            ObtainsDictonary(info.key1, 2);

            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var bufferLength = 10000;
                        var byteBuffer = new byte[bufferLength];
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);

                            foreach (var caracter in byteBuffer)
                            {
                                var actual = Convert.ToInt32(caracter);
                                if (actual >= 65 && actual <= 90)
                                {
                                    writer.Write((byte)alfa[actual]);
                                }
                                else if (actual >= 97 && actual <= 122)
                                {
                                    writer.Write((byte)(alfa[actual - 32] + 32));
                                }
                                else
                                {
                                    writer.Write(caracter);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
