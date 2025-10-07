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
p) Nombres premiers (Bonus)

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
                    Console.WriteLine();
                    while (true)
                    {
                        Console.Write("Entrer l'exposant : ");
                        string exposantEntre = Console.ReadLine();
                        bool exposantEstValide = int.TryParse(exposantEntre, out int exposantConfirmer);
                        bool exposantEstUnNombre = double.TryParse(exposantEntre, out double foo);

                        if (exposantEstValide)
                        {
                            opperationAffichee = $"Operation : {resultat} ^ {exposantConfirmer} = ";
                            string calcul = "\n\nCalculs :\n";
                            double resultatDeLexposant = resultat;

                            if(exposantConfirmer  < 0)
                            {
                                resultat /= -(10 * exposantConfirmer);
                                opperationAffichee += resultat;
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

                            break;
                        }
                        else if (exposantEstUnNombre)
                        {
                            Console.WriteLine("Erreur : exposant n'est pas entier !");
                        }
                        else
                        {
                            Console.WriteLine("Erreur : nombre invalide !");
                        }

                            
                    }
                }



                else if (estOpperationSimple)
                {


                    while (true)
                    {
                        Console.Write("Entrer un nombre : ");
                        string nombreEntre = Console.ReadLine();
                        bool inputEstValide = double.TryParse(nombreEntre, out double nombreConfirmer);

                        if (inputEstValide)
                        {


                            switch (valeurEntree)
                            {
                                case "+":
                                    opperationAffichee = $"\nOperation : {resultat} + {nombreConfirmer} = {resultat + nombreConfirmer}\n";
                                    resultat += nombreConfirmer;
                                    break;
                                case "-":
                                    opperationAffichee = $"\nOperation : {resultat} - {nombreConfirmer} = {resultat - nombreConfirmer}\n";
                                    resultat -= nombreConfirmer;
                                    break;
                                case "*":
                                    opperationAffichee = $"\nOperation : {resultat} * {nombreConfirmer} = {resultat * nombreConfirmer}\n";
                                    resultat *= nombreConfirmer;
                                    break;
                                case "/":
                                    opperationAffichee = $"\nOperation : {resultat} / {nombreConfirmer} = {resultat / nombreConfirmer}\n";
                                    resultat /= nombreConfirmer;
                                    break;

                            }
                            break;
                        }

                        Console.WriteLine("Erreur : nombre invalide!");
                    }

                }

                else if (estOpperationComplexe)
                {
                    if (valeurEntree == "!")
                    {
                        while (true)
                        {
                            Console.Write("Entrer le factoriel : ");
                            string nombreEntre = Console.ReadLine();
                            bool inputEstValideEtPositif = uint.TryParse(nombreEntre, out uint nombreConfirmer);
                            bool inputEstPasValideEtNegatif = double.TryParse(nombreEntre, out double foo);

                            if (inputEstValideEtPositif || nombreConfirmer > 0)
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
                                break;

                            } else if (inputEstValideEtPositif || nombreConfirmer == 0)
                            {
                                resultat = 1;
                                opperationAffichee = "\nOperation : 0! = 1";
                                break;
                            }


                            else if (inputEstPasValideEtNegatif)
                            {
                                Console.WriteLine("Erreur : factoriel n'est pas entier positif !");

                            }

                            else
                            {
                                Console.WriteLine("Erreur : nombre invalide !");

                            }



                        }
                    }

                    else
                    {
                        Console.WriteLine();
                        double xDeLaSerie;
                        while (true)
                        {
                            Console.Write("Entrer le x de la série de Taylor : ");
                            string xDeLaSerieEntre = Console.ReadLine();

                            bool nombreValide = double.TryParse(xDeLaSerieEntre, out double xDeLaSerieConfirmer);

                            if (nombreValide)
                            {
                                xDeLaSerie = xDeLaSerieConfirmer;
                                break;

                            }
                            Console.WriteLine("Erreur : x invalide !");

                        }


                        while (true)
                        {
                            Console.Write("Entrer le n de la série de Taylor : ");
                            string nDeLaSerieEntre = Console.ReadLine();
                            bool nValide = int.TryParse(nDeLaSerieEntre, out int nDeLaSerieConfirmer);

                            if (nValide)
                            {
                                double resultatDeLaSerie = 1;

                                string serieDeTaylorAff = "Serie de Taylor : 1";
                                string calculSerieAff = "Calculs : 1";
                                string valeurAff = "Valeurs : 1";

                                for (int i = 1; i <= nDeLaSerieConfirmer; i++)
                                {
                                    serieDeTaylorAff += $" + ({xDeLaSerie}^{i} / {i}!)";

                                    double exposantValeur = Exposant(xDeLaSerie, i);
                                    double factorielValeur = factorielPourSerieDeTaylor(i);

                                    calculSerieAff += $" + {exposantValeur} / {factorielValeur}";
                                    double termeIDeLaSerie = (exposantValeur / factorielValeur);
                                    resultatDeLaSerie += termeIDeLaSerie;

                                    valeurAff += $" + {termeIDeLaSerie}";
                                }
                                resultat = resultatDeLaSerie;
                                opperationAffichee = $"\n{serieDeTaylorAff}\n{calculSerieAff}\n{valeurAff}\n";
                                Console.WriteLine(resultat);

                                break;
                            }
                            else
                            {
                                Console.WriteLine("Erreur : n invalide, doit être un nombre entier !");
                            }


                        }
                    }
                }

                else if (estOpperationGraphique)
                {
                    if(valeurEntree == "p")
                    {
                        uint valeurDebut;
                        uint valeurFin;

                        while (true)
                        {
                            Console.WriteLine();
                            Console.Write("Entrer un nombre de début :");
                            string valeurDebutEntre = Console.ReadLine();
                            bool nombreDeDebutEstValide = uint.TryParse(valeurDebutEntre, out uint valeurDeDebutConfirmer);

                            if (nombreDeDebutEstValide && valeurDeDebutConfirmer > 0)
                            {
                                valeurDebut = valeurDeDebutConfirmer;
                                break;
                            }
                            Console.WriteLine("Erreur : nombre de début invalide !");
                        }

                        while (true)
                        {
                            Console.WriteLine();
                            Console.Write("Entrer un nombre de fin :");
                            string valeurFinEntre = Console.ReadLine();
                            bool nombreDeFinEstValide = uint.TryParse(valeurFinEntre, out uint valeurDeFinConfirmer);

                            if (nombreDeFinEstValide)
                            {
                                valeurFin = valeurDeFinConfirmer;
                                break;
                            }
                            Console.WriteLine("Erreur : nombre de Fin invalide !");
                        }

                        //commence a max(3, n), fait des bon de 2 -> n-1 : car si n % 2 == 0 -> pas premier et si on arrive pas en f ... != premier
                        // pas de factuer premier VS % nombre impair ... 

                        string nombrePremier = (valeurDebut <= 2) ? "2" : "";
                        int nombreDeNombrePremier = (valeurDebut <=  2) ? 1: 0;

                        for(int i = 1; i < valeurFin; i += 2)
                        {

                            if((i%3 != 0 &&  i % 5 != 0 && i % 7 != 0 && i % 9 != 0 && i >1) || i == 3 || i ==5 || i==7)
                            {
                                nombrePremier += $" {i}";
                                nombreDeNombrePremier++;
                            }
                        }

                        Console.WriteLine($"{nombreDeNombrePremier} nombres premiers de {valeurDebut} a {valeurFin} : ");
                        Console.WriteLine(nombrePremier);
                        Console.WriteLine();
                        Console.Write("Appuyer sur Entrer pour continuer.");

                    }
                    else
                    {
                        double hauteur;
                        double largeur;
                        while (true)
                        {
                            Console.WriteLine();
                            Console.Write("Entrer une hauteur : ");
                            string hauteurEntre = Console.ReadLine();
                            bool hauteurEstValide = double.TryParse(hauteurEntre, out double hauteurConfirmer);

                            if (hauteurEstValide && hauteurConfirmer > 0)
                            {
                                hauteur = hauteurConfirmer;
                                break;
                            }
                            Console.WriteLine("Erreur : hauteur invalide !");
                        }

                        if (valeurEntree == "r")
                        {
                            while (true)
                            {
                                Console.WriteLine();
                                Console.Write("Entrer une largeur : ");
                                string largeurEntre = Console.ReadLine();
                                bool largeurEstValide = double.TryParse(largeurEntre, out double largeurConfirmer);

                                if (largeurEstValide && largeurConfirmer > 0)
                                {
                                    largeur = largeurConfirmer;
                                    break;
                                }
                                Console.WriteLine("Erreur : largeur invalide !");
                            }

                            for (int i = 0; i < Math.Round(hauteur); i++)
                            {
                                Console.WriteLine(string.Concat(Enumerable.Repeat("* ", (int)Math.Round(largeur))));
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

                            // ** = ^ ?
                            double hypothenuse = Math.Sqrt((hauteur * hauteur) + (hauteur * hauteur));
                            Console.WriteLine($"Aire        : {(hauteur * hauteur) / 2}");
                            Console.WriteLine($"Hypothenuse : {hypothenuse:0.000}");
                            Console.WriteLine($"Perimetre   : {(hauteur + hauteur + hypothenuse):0.000}");
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


    }
}
