using System;
using System.Numerics;
using System.IO;

namespace LAB_Reposicion.Models
{
    public class Diffie_Hellman
    {
        public static void GenerarLlaves(KeysDataTaken info)
        {
            Directory.CreateDirectory("temp");
            var g = new BigInteger(43);
            var p = new BigInteger(107);

            var a = new BigInteger(new Random().Next());
            var b = new BigInteger(info.clave);

            File.WriteAllText($"temp\\K1.txt", $"{a}|{(int)BigInteger.ModPow(g, b, p)}");
            File.WriteAllText($"temp\\K2.txt", $"{b}|{(int)BigInteger.ModPow(g, a, p)}");
        }

        public static int ObtenerKey(BigInteger privada, BigInteger publica)
        {
            return (int)BigInteger.ModPow(publica, privada, new BigInteger(107));
        }
    }
}
