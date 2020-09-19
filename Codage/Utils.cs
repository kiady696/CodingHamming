using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codage
{
    class Utils
    {

        //Le fonction mamadika string ho binary le fonction misy padleft(8,.. iny no apesaina amny voalohany 
        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        //de ito ny fonction inverse an'iny apesaina am farany
        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        public static String hexToASCII(String hex)
        {
            // initialize the ASCII code string as empty. 
            String ascii = "";

            for (int i = 0; i < hex.Length; i += 2)
            {

                // extract two characters from hex string 
                String part = hex.Substring(i, 2);

                // change it into base 16 and  
                // typecast as the character 
                char ch = (char)Convert.ToInt32(part, 16); ;

                // add this char to final ASCII string 
                ascii = ascii + ch;
            }
            return ascii;
        }

        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        //Fonction generateur d'erreur
        public static void genError(List<double> motCoded , int nbErreurMax )
        {
            //mboucle changeBit isaky ny nbErreurMax(j<nbErreurmax) de tsy atao miverina indroa le index anle bit ovaina raha >1 ilay nbErreurMax
            //ny nb erreur atao random fa tsy mihoatra ny nbErreurMax
            //Random aleatoire = new Random();
            int nbErreur = StaticRandom.Instance.Next(0, nbErreurMax + 1); 

            for (int j = 0; j < nbErreur; j++)
            {
               //Random aleatoire2 = new Random();
               int index = StaticRandom.Instance.Next(0, 7); //le position hisy bit hiova a cause du bruit
               changeBit(motCoded, index);              
            }
            
        }

        public static string toStringFromDoubles(List<double> doubles)
        {
            string res = "";
            foreach(double d in doubles)
            {
                res += d.ToString();
            }
            return res;
        }


        public static void changeBit(List<double> motToBeChanged , int index)
        {
            for(int i = 0; i< motToBeChanged.Count;i++)
            {
                if(i == index)
                {
                    if(motToBeChanged[i] == 0)
                    {
                        motToBeChanged[i] = 1;
                    }else if(motToBeChanged[i] == 1){
                        motToBeChanged[i] = 0;
                    }
                }
            }

                
        }

        public static string corriger(List<string> equationsNonVerifiees , string mot)
        {
            List<double> motACorriger = new List<double>();

            motACorriger = mot.Select(x => double.Parse(x.ToString())).ToList();
            int nombreEquationsNonVerifiees = equationsNonVerifiees.Count;

            if(nombreEquationsNonVerifiees == 1) //si une seule equation n'a pas ete verifiee
            {
                if(equationsNonVerifiees[0] == "e1")
                {
                    changeBit(motACorriger , 0); //erreur sur p0
                }else if(equationsNonVerifiees[0] == "e2")
                {
                    changeBit(motACorriger , 1); //erreur sur p1
                }
                else if (equationsNonVerifiees[0] == "e3")
                {
                    changeBit(motACorriger , 3); //erreur sur p2 (4è bit)
                }

            }else if(nombreEquationsNonVerifiees == 2)
            {
                if(equationsNonVerifiees[0] == "e1" && equationsNonVerifiees[1] == "e2")
                {
                    changeBit(motACorriger , 2);
                }
                if (equationsNonVerifiees[0] == "e1" && equationsNonVerifiees[1] == "e3")
                {
                    changeBit(motACorriger , 4);
                }
                if (equationsNonVerifiees[0] == "e2" && equationsNonVerifiees[1] == "e3")
                {
                    changeBit(motACorriger , 5);
                }
            }
            else if(nombreEquationsNonVerifiees == 3)
            {
                changeBit(motACorriger , 6);
            }
            Console.WriteLine("le mot corrigé est: " + motACorriger[0] + motACorriger[1] + motACorriger[2] + motACorriger[3] + motACorriger[4] + motACorriger[5] + motACorriger[6]);
            return toStringFromDoubles(motACorriger);
        }


        //mamerina oe iza avy ny equation(s) tsy vérifiée(s)
        public static List<string> isFaulty(string motCoded)
        {
            List<string> lesEquationsNonVerifiees = new List<string>();

            List<double> motEnDouble = new List<double>();
            motEnDouble = motCoded.Select(x => double.Parse(x.ToString())).ToList();
            Console.WriteLine("le mot de code a verifier: " + motEnDouble[0] + motEnDouble[1] + motEnDouble[2] + motEnDouble[3] + motEnDouble[4] + motEnDouble[5] + motEnDouble[6]);
            double e1 = motEnDouble[0] + motEnDouble[2] + motEnDouble[4] + motEnDouble[6];
            Console.WriteLine("l'equation e1: " + e1);
            double e2 = motEnDouble[1] + motEnDouble[2] + motEnDouble[5] + motEnDouble[6];
            Console.WriteLine("l'equation e2: " + e2);
            double e3 = motEnDouble[3] + motEnDouble[4] + motEnDouble[5] + motEnDouble[6];
            Console.WriteLine("l'equation e3: " + e3);
            if (e1 % 2 != 0)
            {
                lesEquationsNonVerifiees.Add("e1");
            }
            if (e2 % 2 != 0)
            {
                lesEquationsNonVerifiees.Add("e2");
            }
            if (e3 % 2 != 0)
            {
                lesEquationsNonVerifiees.Add("e3");
            }




            return lesEquationsNonVerifiees;
        }

        public static void decoder(List<string> motsCoded)
        {
            
            for(int i = 0; i<motsCoded.Count; i++)
            {
                motsCoded[i] = motsCoded[i].Remove(0, 1); //enleve p0
                motsCoded[i] = motsCoded[i].Remove(0, 1); //enleve p1
                motsCoded[i] = motsCoded[i].Remove(1, 1); //enleve p2
                Console.WriteLine("Le message décodé: " + motsCoded[i]);
                
            }
        }


        //pour verification fotsiny
        public static bool isWellCoded(List<string> motsCoded)
        {
            List<double> motsCodes = new List<double>();
            foreach(string mot in motsCoded)
            {
                motsCodes = mot.Select(x => double.Parse(x.ToString())).ToList();
                Console.WriteLine("le mot en double[] a verifier: " + motsCodes[0] + motsCodes[1] + motsCodes[2] + motsCodes[3] + motsCodes[4] + motsCodes[5] + motsCodes[6]);
                double e1 = motsCodes[0] + motsCodes[2] + motsCodes[4] + motsCodes[6];
                Console.WriteLine("l'equation e1: "+e1);
                double e2 = motsCodes[1] + motsCodes[2] + motsCodes[5] + motsCodes[6];
                Console.WriteLine("l'equation e2: " + e2);
                double e3 = motsCodes[3] + motsCodes[4] + motsCodes[5] + motsCodes[6];
                Console.WriteLine("l'equation e3: " + e3);             
                if (e1 % 2 == 0 && e2 % 2 == 0 && e3 % 2 == 0)
                {
                    Console.WriteLine("Mot bien codé en Hamming");
                   
                }

            }

            return true;
        } 

        public static void rendreBinaire(Matrix<double> codedInHamming)
        {
            for(int i = 0; i<codedInHamming.RowCount; i++)
            {
                for(int j = 0;j<codedInHamming.ColumnCount; j++)
                {
                    if(codedInHamming.At(i,j) == 2)
                    {
                        codedInHamming[i, j] = 0;
                    }else if(codedInHamming.At(i, j) == 3)
                    {
                        codedInHamming[i, j] = 1;
                    }
                }
            }
        }

        public static List<string> codeToHamming(List<string> motsDeCode)
        {
            List<string> res = new List<string>();

            Matrix<double> G = DenseMatrix.OfArray(new double[,] { { 1, 1, 0, 1 }, { 1, 0, 1, 1 }, { 1, 0, 0, 0 }, { 0, 1, 1, 1 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
            List<double> unMotEnDouble = new List<double>();
            foreach(string mot in motsDeCode)
            {
                //Avadika ho double[] le string mot de code "0101" iny 
                unMotEnDouble = mot.Select(x => double.Parse(x.ToString())).ToList();
                Console.WriteLine("le mot en double[]: " + unMotEnDouble[0]+unMotEnDouble[1] + unMotEnDouble[2] + unMotEnDouble[3]);

                Matrix<double> motDeCode = DenseMatrix.OfArray(new double[,] { { unMotEnDouble[0] }, { unMotEnDouble[1] }, { unMotEnDouble[2] }, { unMotEnDouble[3] } });
                Matrix<double> codedInHamming = multiplicationMatrices(G, motDeCode);
                //OVAINA HOE EN BASE 2 ILAY Matrice multipliée ZE 3=1 & ZE 2=0
                rendreBinaire(codedInHamming);
                

                string storingRes = "";
                for(int i = 0; i<codedInHamming.RowCount; i++)
                {
                    storingRes += codedInHamming.At(i, 0);
                }
                res.Add(storingRes);


            }
            


            return res;
        }

        public static Matrix<double> multiplicationMatrices(Matrix<double> G , Matrix<double> motDeCode)
        {
            return G * motDeCode.Modulus(2);
        }

        public static List<double> getBytes(string motDeCode)
        {
            List<double> res = new List<double>();
            foreach(char c in motDeCode)
            {
                double b = c;
                res.Add(b);
            }
            return res;
        }


    }
}
