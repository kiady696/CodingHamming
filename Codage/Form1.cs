﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Codage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void coder_Click(object sender, EventArgs e)
        {
            //avadika ho binaire ilay message
            string original = this.aEncoder.Text.Trim();
            Console.WriteLine("Le message original: "+original);
            //Byte[] motDeCodeBrute = System.Text.Encoding.ASCII.GetBytes(original);
            
            /*foreach (char ch in original)
            {
                string c1 = ch.ToString();
                Console.WriteLine("le char a binariser: " + c1);
                binaire += Convert.ToByte(c1, 2);
            }*/

            UTF8Encoding encoding = new UTF8Encoding(); 
            byte[] buf = encoding.GetBytes(original);
            foreach(byte b in buf)
            {
                Console.WriteLine("Les bytes de l'original : " + b.ToString());
            }
            

            StringBuilder binaryStringBuilder = new StringBuilder();
            foreach(byte b in buf)
            {
                binaryStringBuilder.Append(Convert.ToString(b, 2));
            }
            //    K         i         a         d         y
            // 1001011 + 1101001 + 1100001 + 1100100 + 1111001
            Console.WriteLine("Le message en binaire: " + binaryStringBuilder.ToString());

            //longueur du mot de code brute
            int longueurBrute = binaryStringBuilder.Length;
            Console.WriteLine("La longueur brute de la chaîne " + longueurBrute);


            //Zaraina 4-blocs ilay binaryStringBuilder 
            List<string> motsDeCode = new List<string>();
            string unBloc = "";
            int count4 = 0;
            while(1 == 1)
            {

                int repere = count4 + 4;
                if (repere == longueurBrute)
                {
                    break; //divisible par 4 izay ilay mots de code brute
                }
                if (repere > longueurBrute) //raha tsy divisible par 4 kosa de recuperer-na anaty string kely ilay ambiny any am farany
                {
                    //count4 = count4 - 4;
                    string reste = binaryStringBuilder.ToString().Substring(count4, (binaryStringBuilder.Length - count4));
                    Console.WriteLine("le reste ho apetaka any am farany indrindra reefa avy midécode: " + reste);
                    break;
                }

                unBloc = binaryStringBuilder.ToString().Substring(count4, 4);
                Console.WriteLine("le ième lettre: " + unBloc);

                motsDeCode.Add(unBloc);

                count4 = count4 + 4;
                Console.WriteLine("la valeur de count4: " + count4 + " / " + longueurBrute);
     

            }

            //Azo ny mots de code { (0111) , (0010) , (1110) }
            //codena en Hamming aloha : Hamming ( 7 , 4 )
            //avadika ho double[] pour chaque mot de code 
            List<string> lesMotsCoded = Utils.codeToHamming(motsDeCode);
            foreach(string coded7 in lesMotsCoded)
            {
                Console.WriteLine("un mot codé: " + coded7);
                
            }

            //verification codage de hamming
            //bool wellCoded = Utils.isWellCoded(lesMotsCoded);



            //fonction mgenerer erreur randomly

            //fonction mdetecter sy mcorriger erreur Hamming
            //parcourevana ilay liste an'ny mots de code efa générés sy nasiana erreur
            //isaky ny string mot de code jerena raha isFaulty izy 
            //raha faulty izy dia mamerina ny anaran'ny equation(s) tsy verifieny 
            List<string> lesEquationsNonVerifiees = new List<string>();

            //teste kely anisiana bruit
            List<double> motEnDoubleAsianaBruit = new List<double>();

            int nbErreurMax = Int32.Parse(this.errorMax.Text); //maka anle nb erreur max input
            Console.WriteLine("le nombre d\'erreur max entré: " + nbErreurMax);
            foreach (string mot in lesMotsCoded)
            {
                //test kely
                /*motEnDoubleAsianaBruit = mot.Select(x => double.Parse(x.ToString())).ToList();
                Utils.changeBit(motEnDoubleAsianaBruit,3);
                Console.WriteLine("Le mot bruité est: " + motEnDoubleAsianaBruit[0] + motEnDoubleAsianaBruit[1] + motEnDoubleAsianaBruit[2] + motEnDoubleAsianaBruit[3] + motEnDoubleAsianaBruit[4] + motEnDoubleAsianaBruit[5] + motEnDoubleAsianaBruit[6]);
                string motBruite = Utils.toStringFromDoubles(motEnDoubleAsianaBruit);
                lesEquationsNonVerifiees = Utils.isFaulty(motBruite);
                if (lesEquationsNonVerifiees.Count != 0)
                {
                    //Corriger l'erreur de ce mot en fonction des equations qu'il ne verifie pas
                    Utils.corriger(lesEquationsNonVerifiees, motBruite); //le mot io no ovaina ny bit ray anatiny
                }*/

                motEnDoubleAsianaBruit = mot.Select(x => double.Parse(x.ToString())).ToList();
                Console.WriteLine(" --- Le mot original est: " + motEnDoubleAsianaBruit[0] + motEnDoubleAsianaBruit[1] + motEnDoubleAsianaBruit[2] + motEnDoubleAsianaBruit[3] + motEnDoubleAsianaBruit[4] + motEnDoubleAsianaBruit[5] + motEnDoubleAsianaBruit[6]);
                //Random aleatoire = new Random();
                Utils.genError(motEnDoubleAsianaBruit, nbErreurMax); //mgenerer erreur random
                Console.WriteLine("le mot bruité est: " + motEnDoubleAsianaBruit[0] + motEnDoubleAsianaBruit[1] + motEnDoubleAsianaBruit[2] + motEnDoubleAsianaBruit[3] + motEnDoubleAsianaBruit[4] + motEnDoubleAsianaBruit[5] + motEnDoubleAsianaBruit[6]);
                string motBruite = Utils.toStringFromDoubles(motEnDoubleAsianaBruit);
                lesEquationsNonVerifiees = Utils.isFaulty(motBruite);
                if (lesEquationsNonVerifiees.Count != 0)
                {
                    //Corriger l'erreur de ce mot en fonction des equations qu'il ne verifie pas
                    Utils.corriger(lesEquationsNonVerifiees, motBruite); //le mot io no ovaina ny bit ray anatiny
                }



                /*lesEquationsNonVerifiees = Utils.isFaulty(mot);
                if(lesEquationsNonVerifiees.Count != 0)
                {
                    //Corriger l'erreur de ce mot en fonction des equations qu'il ne verifie pas
                    Utils.corriger(lesEquationsNonVerifiees, mot); //le mot io no ovaina ny bit ray anatiny
                }
                Console.WriteLine("le nb d'equation non verifiee pour ce mot: "+lesEquationsNonVerifiees.Count);
                */
            }
                //arakaraky ny hoe firy ny equation tsy verifieny sy iza amle telo ny equation tsy verifieny no anoloana ny bit nisy erreur


            //oetina any amna form hi afficher-na resultat ny mots de code sy ilay string kely apetaka am farany
            //decoder-na alana ny double any am positions 0,1 sy 3 de apetaka ilay string kely am farany 
            //averina ho lettre ny isaky ny 7-bloc 
            //apoitra ny mot azo aveo






        }
    }
}
