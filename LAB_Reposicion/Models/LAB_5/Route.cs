using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_Reposicion.Models.LAB_5
{
    public class Route
    {
        private static string routeDirectory = Environment.CurrentDirectory;

        public static void CifradoVertical(NumbersDataTaken info)
        {
            Directory.CreateDirectory("temp");
            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var bufferLength = info.key1 * info.key2;
                        var byteBuffer = new byte[bufferLength];

                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            var matriz = new byte[info.key1, info.key2];
                            byteBuffer = reader.ReadBytes(bufferLength);
                            var cont = 0;

                            for (int i = 0; i < info.key2; i++)
                            {
                                for (int j = 0; j < info.key1; j++)
                                {
                                    if (cont < byteBuffer.Count())
                                    {
                                        matriz[j, i] = byteBuffer[cont];
                                        cont++;
                                    }
                                }
                            }

                            for (int i = 0; i < info.key1; i++)
                            {
                                for (int j = 0; j < info.key2; j++)
                                {
                                    writer.Write(matriz[i, j]);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void DecifradoVertical(NumbersDataTaken info)
        {
            Directory.CreateDirectory("temp");
            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var bufferLength = info.key1 * info.key2;
                        var byteBuffer = new byte[bufferLength];

                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            var matriz = new byte[info.key1, info.key2];
                            byteBuffer = reader.ReadBytes(bufferLength);
                            var cont = 0;

                            for (int i = 0; i < info.key1; i++)
                            {
                                for (int j = 0; j < info.key2; j++)
                                {
                                    if (cont < byteBuffer.Count())
                                    {
                                        matriz[i, j] = byteBuffer[cont];
                                        cont++;
                                    }
                                    else
                                    {
                                        matriz[i, j] = (byte)0;
                                    }
                                }
                            }

                            for (int i = 0; i < info.key2; i++)
                            {
                                for (int j = 0; j < info.key1; j++)
                                {
                                    if (matriz[j, i] != (byte)0)
                                    {
                                        writer.Write(matriz[j, i]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void CifradoEspiral(NumbersDataTaken info)
        {
            Directory.CreateDirectory("temp");
            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var bufferLength = info.key1 * info.key2;
                        var byteBuffer = new byte[bufferLength];

                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            var Matriz = new byte[info.key1, info.key2];
                            byteBuffer = reader.ReadBytes(bufferLength);

                            var numVueltas = 0;
                            var posX = 0;
                            var posY = 0;
                            var Direccion = "abajo";

                            foreach (var caracter in byteBuffer)
                            {
                                if (Direccion == "abajo" && posY != info.key1 - 1 - numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    posY++;
                                }
                                else if (Direccion == "abajo" && posY == info.key1 - 1 - numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    posX++;
                                    Direccion = "derecha";
                                }
                                else if (Direccion == "derecha" && posX != info.key2 - 1 - numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    posX++;
                                }
                                else if (Direccion == "derecha" && posX == info.key2 - 1 - numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    posY--;
                                    Direccion = "arriba";
                                }
                                else if (Direccion == "arriba" && posY != numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    posY--;
                                }
                                else if (Direccion == "arriba" && posY == numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    numVueltas++;
                                    posX--;
                                    Direccion = "izquierda";
                                }
                                else if (Direccion == "izquierda" && posX != numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    posX--;
                                }
                                else if (Direccion == "izquierda" && posX == numVueltas)
                                {
                                    Matriz[posY, posX] = caracter;
                                    posY++;
                                    Direccion = "abajo";
                                }
                            }

                            for (int i = 0; i < info.key1; i++)
                            {
                                for (int j = 0; j < info.key2; j++)
                                {
                                    writer.Write(Matriz[i, j]);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void DecifradoEspiral(NumbersDataTaken info)
        {
            Directory.CreateDirectory("temp");
            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var bufferLength = info.key1 * info.key2;
                        var byteBuffer = new byte[bufferLength];

                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            var Matriz = new byte[info.key1, info.key2];
                            byteBuffer = reader.ReadBytes(bufferLength);
                            var cont = 0;

                            for (int i = 0; i < info.key1; i++)
                            {
                                for (int j = 0; j < info.key2; j++)
                                {
                                    if (cont < byteBuffer.Count())
                                    {
                                        Matriz[i, j] = byteBuffer[cont];
                                        cont++;
                                    }
                                    else
                                    {
                                        Matriz[i, j] = (byte)0;
                                    }
                                }
                            }

                            var numVueltas = 0;
                            var posX = 0;
                            var posY = 0;
                            var Direccion = "abajo";

                            for (int i = 0; i < bufferLength; i++)
                            {
                                if (Matriz[posY, posX] != 0)
                                {
                                    if (Direccion == "abajo" && posY != info.key1 - 1 - numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        posY++;
                                    }
                                    else if (Direccion == "abajo" && posY == info.key1 - 1 - numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        posX++;
                                        Direccion = "derecha";
                                    }
                                    else if (Direccion == "derecha" && posX != info.key2 - 1 - numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        posX++;
                                    }
                                    else if (Direccion == "derecha" && posX == info.key2 - 1 - numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        posY--;
                                        Direccion = "arriba";
                                    }
                                    else if (Direccion == "arriba" && posY != numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        posY--;
                                    }
                                    else if (Direccion == "arriba" && posY == numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        numVueltas++;
                                        posX--;
                                        Direccion = "izquierda";
                                    }
                                    else if (Direccion == "izquierda" && posX != numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        posX--;
                                    }
                                    else if (Direccion == "izquierda" && posX == numVueltas)
                                    {
                                        writer.Write(Matriz[posY, posX]);
                                        posY++;
                                        Direccion = "abajo";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
