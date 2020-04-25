using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace LAB_Reposicion.Models
{
    public class Ceasar2
    {
        private static Dictionary<int, int> alfa = new Dictionary<int, int>();

        static void ObtenerDic(BigInteger key, int opc)
        {
            alfa = new Dictionary<int, int>();
            var contOriginal = 65;
            var contNuevo = (int)BigInteger.ModPow(key, 1, new BigInteger(26));
            var comienzo = 65;

            do
            {
                if (contNuevo + contOriginal < 91)
                {
                    if (opc == 1)
                    {
                        alfa.Add(contOriginal, contNuevo + contOriginal);
                    }
                    else
                    {
                        alfa.Add(contNuevo + contOriginal, contOriginal);
                    }
                }
                else
                {
                    if (opc == 1)
                    {
                        alfa.Add(contOriginal, comienzo);
                    }
                    else
                    {
                        alfa.Add(comienzo, contOriginal);
                    }
                    comienzo++;
                }
                contOriginal++;
            } while (contOriginal < 91);
        }

        public static void CifrarDecifrado(CeasearDataTaken info, int opc)
        {
            Directory.CreateDirectory("temp");
            var key = 0;
            using (var reader = new StreamReader(info.key.OpenReadStream()))
            {
                var line = reader.ReadLine().Split('|');

                switch (line.Length)
                {
                    case 2:
                        key = Diffie_Hellman.ObtenerKey(new BigInteger(Convert.ToInt32(line[0])), new BigInteger(Convert.ToInt32(line[1])));
                        break;
                    case 3:
                        key = RSA.cifradoDecifrado(new BigInteger(Convert.ToInt32(line[2])), new BigInteger(Convert.ToInt32(line[1])), new BigInteger(Convert.ToInt32(line[0])));
                        break;
                }
            }

            ObtenerDic(new BigInteger(key), opc);

            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.File.FileName}", FileMode.OpenOrCreate))
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
