using System;
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
            List<string> typesDeCodage = new List<string>();
            typesDeCodage.Add("Hamming");
            typesDeCodage.Add("Répétition pure");
            typesDeCodage.Add("Parité");
            this.typeCodage.DataSource = typesDeCodage;
            
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
            string reste = "";
            int count4 = 0;
            while(1 == 1)
            {

                int repere = count4 + 4;
                if (repere == longueurBrute)
                {
                    unBloc = binaryStringBuilder.ToString().Substring(count4, 4);
                    Console.WriteLine("le ième lettre: " + unBloc);

                    motsDeCode.Add(unBloc);
                    break; 
                }
                if (repere > longueurBrute) //raha tsy divisible par 4 kosa de recuperer-na anaty string kely ilay ambiny any am farany
                {
                    //count4 = count4 - 4;
                    reste = binaryStringBuilder.ToString().Substring(count4, (binaryStringBuilder.Length - count4));
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

            //List<string> anasiavana ny valiny
            List<string> res = new List<string>();

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
                    string motCorrige = Utils.corriger(lesEquationsNonVerifiees, motBruite); //le mot io no ovaina ny bit ray anatiny
                    res.Add(motCorrige);
                }
                else
                {
                    res.Add(motBruite);
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

            //Decoder-na ilay message (alana ny am positions 0,1 sy 3)
            Utils.decoder(res);

            //Averina atambatra anaty string anakiray ilay motscorriges sy ilay string kely am farany
            string messageReçuDecode = "";
            foreach(string s in res)
            {
                messageReçuDecode += s;
            }
            messageReçuDecode += reste;
            Console.WriteLine("Le message à decouper par 7-bloc et à reparser en lettre est: " + messageReçuDecode);

            //10001101101001111010011010011100001  THE ORIGINAL
            //10001101101001111010011010011100001  WITH ERRORMAX = 1
            //10111101101001111010001110011100001  WITH ERRORMAX = 2

            //alaina 7 par 7 aloha ilay string lava be
            int count = messageReçuDecode.Length; //tjrs divisible par 7
            List<string> LettresEnBase2 = new List<string>();
            string pourUneLettre = "";
            int count7 = 0;
            while(1 == 1)
            {
                pourUneLettre = messageReçuDecode.Substring(count7, 7);
                Console.WriteLine("La lettre en binaire: " + pourUneLettre);
                LettresEnBase2.Add(pourUneLettre);
                count7 = count7 + 7;
                int repere = count7 + 7;
                if(repere == count)
                {
                    pourUneLettre = messageReçuDecode.Substring(count7, 7);
                    Console.WriteLine("La lettre en binaire: " + pourUneLettre);
                    LettresEnBase2.Add(pourUneLettre);
                    
                    break;
                }
            }

            //Azo ny liste an'ireo lettres fa en base 2
            //averina ho utf8 int byte 
            string resultatFinal = "";
            foreach(string s in LettresEnBase2)
            {
                //pour un string "0101101" , avadika ascii ilay binaire
                string laLettre = Utils.BinaryToString(s);
                resultatFinal += laLettre;
                Console.WriteLine("la lettre: "+laLettre);

            }

            Form2 form2 = new Form2();
            form2.resultatF.Text = resultatFinal;
            form2.Show();
            

            //test hoe tonga ve de afaka atao mitambatra daholo izy tsy mila avadika 7-blocs
            /*string hexa = Utils.BinaryStringToHexString(messageReçuDecode);
            string resultat = Utils.hexToASCII(hexa);
            Console.WriteLine("LE RESULTAT FINAL: " + resultat);*/

            //oetina any amna form hi afficher-na resultat ny mots de code sy ilay string kely apetaka am farany
            //decoder-na alana ny double any am positions 0,1 sy 3 de apetaka ilay string kely am farany 
            //averina ho lettre ny isaky ny 7-bloc 
            //apoitra ny mot azo aveo






        }
    }
}
