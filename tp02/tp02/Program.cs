using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//stirling approx factor pas Z
//FN de input

namespace tp02
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool quitterLaCalculatrice = false;
            double resultat = 0;
            string opperationAffichee = "";

            string tableDesOpperations = @"+) Addition
-) Soustraction
*) Multiplication
/) Division
^) Exposant

!) Factoriel
s) Série de Taylor

r) Rectangle
t) Triangle
p) Nombres premiers

q) Quitter";

            while (!quitterLaCalculatrice)
            {

                Console.WriteLine("**************************************************");
                Console.WriteLine("*                  CALCULATRICE                  *");
                Console.WriteLine("**************************************************");
                Console.WriteLine((opperationAffichee != "") ? opperationAffichee : "");
                Console.WriteLine($"Resultat : {resultat}");
                Console.WriteLine();
                Console.WriteLine("--- Opérations ---");
                Console.WriteLine();
                Console.WriteLine(tableDesOpperations);
                Console.WriteLine();
                Console.Write("Choisir une opération : ");


                string valeurEntree = Console.ReadLine();
                bool parseEstUnChiffre = double.TryParse(valeurEntree, out double resultatDuParse);

                if (parseEstUnChiffre)
                {
                    resultat = resultatDuParse;
                    continue;
                }

                //Nous pouvons directement faire un if/else sur la valeur entre car nous savons quelle nest pas numerique.
                //Nous faisons un if/else et nom un switch car il y a 3 type d opperation : simple(sans calcule) complexe(avec caluclu) e.g Factoriel et celles avec affichage e.g rectangle


                bool estOpperationSimple = valeurEntree == "+" || valeurEntree == "-" || valeurEntree == "/" || valeurEntree == "*";
                bool estOpperationComplexe = valeurEntree == "!" || valeurEntree == "s";
                bool estOpperationGraphique = valeurEntree == "r" || valeurEntree == "t" || valeurEntree == "p";

                if (valeurEntree == "q")
                {
                    Console.WriteLine();
                    Console.Write("Appuyer sur entrée pour fermer la calculatrice.");
                    Console.ReadLine();
                    quitterLaCalculatrice = true;

                }else if(valeurEntree == "^")
                {


                    int exposantConfirmer = ParseEntierSecuritairemen("Entrer l'exposant : ", "Erreur : exposant n'est pas entier !");

                    opperationAffichee = $"Operation : {resultat} ^ {exposantConfirmer} = ";
                    string calcul = "\n\nCalculs :\n";
                    double resultatDeLexposant = resultat;

                    if(exposantConfirmer < 0)
                    {
                        double demonimateur = resultat;
                        resultatDeLexposant = 1 / resultatDeLexposant;
                        for (int i = 1; i < -exposantConfirmer; i++)
                        {
                            calcul += $"1/{demonimateur} * 1/{resultat} = ";
                            resultatDeLexposant *= 1/resultat;
                            demonimateur *= resultat;
                            calcul += $"1/{demonimateur}\n";
                        }

                        opperationAffichee += resultatDeLexposant;
                        resultat = resultatDeLexposant;
                        opperationAffichee += calcul;
                    }
                    else if(exposantConfirmer > 0)
                    {
                        for(int i = 1; i < exposantConfirmer; i++)
                        {
                            calcul += $"{resultatDeLexposant} * {resultat} = ";
                            resultatDeLexposant *= resultat;
                            calcul += $"{resultatDeLexposant}\n";
                        }

                        opperationAffichee += resultatDeLexposant;
                        resultat = resultatDeLexposant;
                        opperationAffichee += calcul;
                    }
                    else
                    {
                        opperationAffichee = $"{resultat} ^ 0 = 1";
                        resultat = 1;
                    }
                    
                }



                else if (estOpperationSimple)
                {

                    double nombreEntreConfirmer = ParseDoubleSecuritairement("Entrer un nombre : ", "Erreur : nombre invalide!");
                    Console.WriteLine(nombreEntreConfirmer);

                    switch (valeurEntree)
                    {
                        case "+":
                            opperationAffichee = $"\nOperation : {resultat} + {nombreEntreConfirmer} = {resultat + nombreEntreConfirmer}\n";
                            resultat += nombreEntreConfirmer;
                            break;
                        case "-":
                            opperationAffichee = $"\nOperation : {resultat} - {nombreEntreConfirmer} = {resultat - nombreEntreConfirmer}\n";
                            resultat -= nombreEntreConfirmer;
                            break;
                        case "*":
                            opperationAffichee = $"\nOperation : {resultat} * {nombreEntreConfirmer} = {resultat * nombreEntreConfirmer}\n";
                            resultat *= nombreEntreConfirmer;
                            break;
                        case "/":
                            if (nombreEntreConfirmer != 0)
                            {
                                opperationAffichee = $"\nOperation : {resultat} / {nombreEntreConfirmer} = {resultat / nombreEntreConfirmer}\n";
                                resultat /= nombreEntreConfirmer;

                            }
                            else
                            {
                                opperationAffichee = "\nOpération annulée : impossible de diviser par 0 !";
                            }
                                break;

                        default:
                            Console.WriteLine(valeurEntree);
                            break;

                    }
                    
                }

                else if (estOpperationComplexe)
                {
                    if (valeurEntree == "!")
                    {


                        uint nombreConfirmer = ParseEntierPositif("Entrer le factoriel : ", "Erreur : factoriel n'est pas entier positif !");
                        if (nombreConfirmer > 0)
                        {
                            string calcul = "Calculs : \n";
                            long factorielDeN = 1;
                            for (long i = nombreConfirmer; i > 0; i--)
                            {
                                calcul += $"{factorielDeN} * {i} = ";
                                factorielDeN *= i;
                                calcul += $"{factorielDeN}\n";
                            }

                            opperationAffichee = $"\nOperation : {nombreConfirmer}! = {factorielDeN}\n\n{calcul}";
                            resultat = factorielDeN;


                        } else
                        {
                            resultat = 1;
                            opperationAffichee = "\nOperation : 0! = 1";

                        }

                    }

                    else
                    {

                        double xDeLaSerie = ParseDoubleSecuritairement("Entrer le x de la série de Taylor :", "Erreur : x invalide !  ");

  

                        uint nDeLaSerieDeTaylor = ParseEntierPositif("Entrer le n de la série de Taylor : ", "Erreur : n invalide, doit être un nombre entier !");
                            

                        double resultatDeLaSerie = 1;

                        string serieDeTaylorAff = "Serie de Taylor : 1";
                        string calculSerieAff = "Calculs : 1";
                        string valeurAff = "Valeurs : 1";

                        for (int i = 1; i <= nDeLaSerieDeTaylor; i++)
                        {
                            serieDeTaylorAff += $" + ({xDeLaSerie}^{i} / {i}!)";

                            double exposantValeur = Exposant(xDeLaSerie, i);
                            double factorielValeur = factorielPourSerieDeTaylor(i);

                            calculSerieAff += $" + {exposantValeur:0.###} / {factorielValeur:0.###}";
                            double termeIDeLaSerie = (exposantValeur / factorielValeur);
                            resultatDeLaSerie += termeIDeLaSerie;

                            valeurAff += $" + {termeIDeLaSerie:0.###}";
                        }
                        resultat = resultatDeLaSerie;
                        opperationAffichee = $"\n{serieDeTaylorAff}\n{calculSerieAff}\n{valeurAff}\n";

                    }
                }

                else if (estOpperationGraphique)
                {
                    if (valeurEntree == "p")
                    {
                        //initialize au valeur minimale pour raisons de securite.
                        uint valeurDebut = ParseEntierPositif("Entrer un nombre de début :", "Erreur : nombre de début invalide !");
                        uint valeurFin = ParseEntierSuperieurAX("Entrer un nombre de fin :", "Erreur : nombre de début invalide !", valeurDebut);

                        string nombrePremier = "";
                        int nombreDeNombrePremier = 0;
                        
                        //Utilisation d'une Crible D'Eresthor modifier pour calculer et afficher les nombre premier dans l'intervale [a;b]

                        bool[] estPremier = Enumerable.Repeat(true, (int)valeurFin + 1).ToArray();

                        estPremier[0] = false;
                        estPremier[1] = false;
                        uint limit = (uint)Math.Sqrt(valeurFin);

                        for (uint p = 2; p < limit + 1; p++)
                        {
                            if (estPremier[p])
                            {
                                for (uint multipleDeP = p * p; multipleDeP < valeurFin + 1; multipleDeP += p)
                                {
                                    estPremier[multipleDeP] = false;
                                }
                            }
                        }

                        for (uint i = 0; i <= valeurFin; i++)
                        {
                            if (estPremier[i] && i >= valeurDebut)
                            {
                                nombreDeNombrePremier++;
                                nombrePremier += (nombreDeNombrePremier%7==0 && nombreDeNombrePremier > 0) ? $"{i}\n" : $"{i} ";
                            }
                        }

                        Console.WriteLine($"{nombreDeNombrePremier} nombres premiers de {valeurDebut} a {valeurFin} : \n");
                        Console.WriteLine(nombrePremier);
                        Console.WriteLine();
                        Console.Write("Appuyer sur Entrer pour continuer.\n");
                        Console.ReadLine();

                    }
                    else
                    {
                        double hauteur = ParseDoubleSuperieureAZero("Entrer une hauteur : ", "Erreur : hauteur invalide !");
                        
                        if (valeurEntree == "r")
                        {

                            double largeur = ParseDoubleSuperieureAZero("Entrer une largeur : ", "Erreur : largeur invalide !");


                            for (int i = 0; i < Math.Ceiling(hauteur); i++)
                            {
                                Console.WriteLine(string.Concat(Enumerable.Repeat("* ", (int)Math.Ceiling(largeur))));
                            }
                            Console.WriteLine($"Aire      : {(largeur * hauteur):0.000}");
                            Console.WriteLine($"Perimetre : {(2 * largeur + 2 * hauteur):0.000}");
                            Console.WriteLine();
                            Console.Write("Appuyer sur entrée pour continuer.");
                            Console.ReadLine();
                        }
                        else
                        {
                            for (int i = 0; i < Math.Round(hauteur); i++)
                            {
                                Console.WriteLine(string.Concat(Enumerable.Repeat("* ", (i + 1))));
                            }

                            double hypothenuse = Math.Sqrt((hauteur * hauteur) + (hauteur * hauteur));
                            Console.WriteLine($"Aire        : {((hauteur * hauteur) / 2):0.###}");
                            Console.WriteLine($"Hypothenuse : {hypothenuse:0.###}");
                            Console.WriteLine($"Perimetre   : {(hauteur + hauteur + hypothenuse):0.###}");
                            Console.WriteLine();
                            Console.Write("Appuyer sur entrée pour continuer.");
                            Console.ReadLine();
                        }
                    }
                }
                
            }
        }

        static double factorielPourSerieDeTaylor(int x)
        {
            double factorielDeX = 1;

            for(int i = x; i > 0; i--)
            {
                factorielDeX *= i;
            }

            return factorielDeX;
        }
        static double Exposant(double x, int n)
        {
            double resultatDeLexposant = 1;

            for(int i = 0; i < n; i++)
            {
                resultatDeLexposant *= x;
            }

            return resultatDeLexposant;
        }

        static double ParseDoubleSecuritairement(string question, string messageErreur)
        {
            bool valeurEntrerEstValide = false;

            while (!valeurEntrerEstValide)
            {
                Console.Write($"\n{question}");
                string valeurEntree = Console.ReadLine();

                valeurEntrerEstValide = double.TryParse(valeurEntree, out double doubleValide);

                if (valeurEntrerEstValide)
                {   
                    return doubleValide;
                }

                Console.WriteLine(messageErreur); ;
            }

            return 0.0;
        }

        static uint ParseEntierPositif(string question, string messageErreur)
        {
            bool nombreEntrerEstValide = false;

            while (!nombreEntrerEstValide)
            {
                Console.Write($"\n{question}");
                string valeurEntree = Console.ReadLine();

                //Nombre entier est Entier et Positif
                if(uint.TryParse(valeurEntree, out uint entierPositifValide))
                {
                    return entierPositifValide;
                }

                //Cas Erreur

                //Valeur n'est pas un entier positif
                if(double.TryParse(valeurEntree, out double nombreDecimal))
                {
                    Console.WriteLine(messageErreur);
                }
                //si la valeur entree n'est pas numérique.
                else
                {
                    Console.WriteLine("Erreur : nombre invalide!");
                }


            }

            return 0;
        }

        static double ParseDoubleSuperieureAZero(string question, string messageErreur)
        {
            bool valeurEstValide = false;

            while (!valeurEstValide)
            {
                double valeurEntre = ParseDoubleSecuritairement(question, messageErreur);

                if(valeurEntre > 0)
                {
                    valeurEstValide = true;
                    return valeurEntre;
                }

                Console.WriteLine(messageErreur);
            }

            return 1;
        }

        static int ParseEntierSecuritairemen(string question, string messageErreur)
        {
            bool nombreEntrerEstValide = false;
            while (!nombreEntrerEstValide) 
            {
                Console.Write($"{question}");
                string valeurEntree = Console.ReadLine();

                if (int.TryParse(valeurEntree, out int entierValide))
                {
                    nombreEntrerEstValide = true;
                    return entierValide;
                }

                if(double.TryParse(valeurEntree, out double foo))
                {
                    Console.WriteLine(messageErreur);
                }
                else
                {
                    Console.WriteLine("Erreur : nombre invalide!");
                }

            }

            return 0;
        }

        static uint ParseEntierSuperieurAX(string question, string messageErreur, uint x)
        {
            bool valeurEntreeEstValide = false;

            while (!valeurEntreeEstValide)
            {
                uint valeurEntre = ParseEntierPositif(question, messageErreur);

                if(valeurEntre >= x)
                {
                    valeurEntreeEstValide = true;
                    return valeurEntre;
                }

                Console.WriteLine(messageErreur);
            }

            return x + 1;
        }
    }
}
