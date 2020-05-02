using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LAB_Reposicion.Models.LAB_5
{
    public class ZigZag
    {
        private static string routeDirectory = Environment.CurrentDirectory;

        public static void Cipher(NumbersDataTaken info)
        {
            Directory.CreateDirectory("temp");
            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var GrupoOlas = (2 * info.key1) - 2;
                        var len = (float)reader.BaseStream.Length / (float)GrupoOlas;
                        var cantOlas = len % 1 <= 0.5 && len % 1 > 0 ? Math.Round(len) + 1 : Math.Round(len);
                        cantOlas = Convert.ToInt32(cantOlas);

                        var pos = 0;
                        var contNivel = 0;

                        var mensaje = new List<byte>[info.key1];

                        for (int i = 0; i < info.key1; i++)
                        {
                            mensaje[i] = new List<byte>();
                        }

                        var bufferLength = 100000;
                        var byteBuffer = new byte[bufferLength];

                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);
                            foreach (var caracter in byteBuffer)
                            {
                                if (pos == 0 || pos % GrupoOlas == 0)
                                {
                                    mensaje[0].Add(caracter);
                                    contNivel = 0;
                                }
                                else if (pos % GrupoOlas == info.key1 - 1)
                                {
                                    mensaje[info.key1 - 1].Add(caracter);
                                    contNivel = info.key1 - 1;
                                }
                                else if (pos % GrupoOlas < info.key1 - 1)
                                {
                                    contNivel++;
                                    mensaje[contNivel].Add(caracter);
                                }
                                else if (pos % GrupoOlas > info.key1 - 1)
                                {
                                    contNivel--;
                                    mensaje[contNivel].Add(caracter);
                                }
                                pos++;
                            }
                        }

                        for (int i = 0; i < info.key1; i++)
                        {
                            var cantIteracion = i == 0 || i == info.key1 - 1 ? cantOlas : cantOlas * 2;
                            var inicio = mensaje[i].Count();
                            for (int j = inicio; j < cantIteracion; j++)
                            {
                                mensaje[i].Add((byte)0);
                            }
                            writer.Write(mensaje[i].ToArray());
                        }
                    }
                }
            }
        }

        public static void Decipher(NumbersDataTaken info)
        {
            Directory.CreateDirectory("temp");
            using (var reader = new BinaryReader(info.File.OpenReadStream()))
            {
                using (var streamWriter = new FileStream($"temp\\{info.Name}.txt", FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(streamWriter))
                    {
                        var GrupoOlas = (2 * info.key1) - 2;
                        var len = (float)reader.BaseStream.Length / (float)GrupoOlas;
                        var cantOlas = len % 1 <= 0.5 && len % 1 > 0 ? Math.Round(len) + 1 : Math.Round(len);
                        cantOlas = Convert.ToInt32(cantOlas);
                        var intermedios = (Convert.ToInt32(reader.BaseStream.Length) - (2 * cantOlas)) / (info.key1 - 2);

                        var pos = 0;
                        var contNivel = 0;
                        var contIntermedio = 0;

                        var mensaje = new Queue<byte>[info.key1];

                        for (int i = 0; i < info.key1; i++)
                        {
                            mensaje[i] = new Queue<byte>();
                        }

                        var bufferLength = 100000;
                        var byteBuffer = new byte[bufferLength];

                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);
                            foreach (var caracter in byteBuffer)
                            {
                                if (contNivel == info.key1 - 1)
                                {
                                    mensaje[contNivel].Enqueue(caracter);
                                }
                                else
                                {
                                    if (pos < cantOlas)
                                    {
                                        mensaje[0].Enqueue(caracter);
                                    }
                                    else if (pos == cantOlas)
                                    {
                                        contNivel++;
                                        mensaje[contNivel].Enqueue(caracter);
                                        contIntermedio = 1;
                                    }
                                    else if (contIntermedio < intermedios)
                                    {
                                        mensaje[contNivel].Enqueue(caracter);
                                        contIntermedio++;
                                    }
                                    else
                                    {
                                        contNivel++;
                                        mensaje[contNivel].Enqueue(caracter);
                                        contIntermedio = 1;
                                    }
                                    pos++;
                                }
                            }
                        }

                        contNivel = 0;
                         var direccion = true;
                        //True es hacia abajo
                        //False es hacia arriba

                        while (mensaje[contNivel].Peek() != (byte)0 && (mensaje[1].Count() != 0 || (info.key1 == 2 && mensaje[1].Count() != 0)))
                        {
                            if (contNivel == 0)
                            {
                                writer.Write(mensaje[contNivel].Dequeue());
                                contNivel = 1;
                                direccion = true;
                            }
                            else if (contNivel < info.key1 - 1 && direccion)
                            {
                                writer.Write(mensaje[contNivel].Dequeue());
                                contNivel++;
                            }
                            else if (contNivel > 0 && !direccion)
                            {
                                writer.Write(mensaje[contNivel].Dequeue());
                                contNivel--;
                            }
                            else if (contNivel == info.key1 - 1)
                            {
                                writer.Write(mensaje[contNivel].Dequeue());
                                contNivel = info.key1 - 2;
                                direccion = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
