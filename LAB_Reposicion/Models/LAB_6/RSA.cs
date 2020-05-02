using System.Collections.Generic;
using System.Numerics;
using System.IO;

namespace LAB_Reposicion.Models
{
    public class RSA
    {
        static bool SonPrimos(int num)
        {
            var cont = 0;
            for (int i = 1; i <= num; i++)
            {
                cont = num % i == 0 ? cont + 1 : cont;
            }

            if (cont != 2)
            {
                return false;
            }

            return true;
        }

        public static int CipherDecipher(BigInteger clave, BigInteger key, BigInteger n)
        {
            return (int)BigInteger.ModPow(clave, key, n);
        }

        public static void GenerateKeys(KeysDataTaken info)
        {
            Directory.CreateDirectory("temp");
            if (SonPrimos(info.key1) && SonPrimos(info.key2) && info.key1 > 1 && info.key2 > 1)
            {
                var p = new BigInteger(info.key1);
                var q = new BigInteger(info.key2);
                var n = p * q;
                var phi = (p - 1) * (q - 1);

                if (phi >= 3)
                {
                    var multiplosPhiN = new List<BigInteger> { p, q };

                    var auxCoprimos = phi;
                    var cont = new BigInteger(2);
                    while (auxCoprimos != 1)
                    {
                        if (auxCoprimos % cont == 0)
                        {
                            if (!multiplosPhiN.Contains(cont))
                            {
                                multiplosPhiN.Add(cont);
                            }
                            auxCoprimos = auxCoprimos / cont;
                        }
                        else
                        {
                            cont++;
                        }
                    }

                    var e = new BigInteger(2);
                    var encontrado = false;
                    while (!encontrado && e < phi)
                    {
                        var divisible = false;
                        foreach (var divisor in multiplosPhiN)
                        {
                            if (BigInteger.ModPow(e, 1, divisor) == 0)
                            {
                                divisible = true;
                            }
                        }

                        if (!divisible)
                        {
                            encontrado = true;
                        }
                        else
                        {
                            e++;
                        }
                    }

                    var tablaMod = new BigInteger[2, 2] { { phi, phi }, { e, new BigInteger(1) } };
                    while (tablaMod[1, 0] != 1)
                    {
                        var ArribaIzq = tablaMod[1, 0];
                        var ArribaDer = tablaMod[1, 1];

                        tablaMod[1, 1] = tablaMod[0, 1] - ((tablaMod[0, 0] / tablaMod[1, 0]) * tablaMod[1, 1]);
                        tablaMod[1, 0] = BigInteger.ModPow(tablaMod[0, 0], 1, tablaMod[1, 0]);
                        tablaMod[0, 0] = ArribaIzq;
                        tablaMod[0, 1] = ArribaDer;

                        while (tablaMod[1, 1] < 0)
                        {
                            tablaMod[1, 1] += phi;
                        }
                    }

                    File.WriteAllText($"temp\\K1.txt", $"{n}|{e}|{CipherDecipher(new BigInteger(info.clave), tablaMod[1, 1], n)}");
                    File.WriteAllText($"temp\\K2.txt", $"{n}|{tablaMod[1, 1]}|{CipherDecipher(new BigInteger(info.clave), e, n)}");
                }
            }
        }
    }
}
