using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp02
{
    internal class Program
    {



        const string OPTION_QUITTER = "q";
        const string OPTION_ADDITION = "+";
        const string OPTION_SOUSTRACTION = "-";
        const string OPTION_MULTIPLICATION = "*";
        const string OPTION_DIVISION = "/";
        const string OPTION_FACT = "!";
        const string OPTION_SERIE_TAYLOR = "s";
        const string OPTION_RECTANGLE = "r";
        const string OPTION_TRIANGLE = "t";
        const string OPTION_NOMBRES_PREMIERS = "p";
        const string OPTION_EXPOSANT = "^";
        const string TABLE_OPERATIONS = @"+) Addition
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

        static void Main(string[] args)
        {
            bool quitterCalculatrice = false;
            double resultat = 0;
            string operationAffichee = "";

            while (!quitterCalculatrice)
            {
                Console.WriteLine("**************************************************");
                Console.WriteLine("*                  CALCULATRICE                  *");
                Console.WriteLine("**************************************************");
                Console.WriteLine((operationAffichee != "") ? operationAffichee : "");
                Console.WriteLine($"Résultat : {resultat}");
                Console.WriteLine();
                Console.WriteLine("--- Opérations ---");
                Console.WriteLine();
                Console.WriteLine(TABLE_OPERATIONS);
                Console.WriteLine();
                bool operationEstValide = false;
                string valeurEntree = "";
                while (!operationEstValide)
                {
                    //
                    Console.Write("\nChoisir une opération : ");
                    valeurEntree = Console.ReadLine();

                    if(double.TryParse(valeurEntree, out double nouveauResultat))
                    {
                        resultat = nouveauResultat;
                        operationAffichee = "";
                        break;
                    }

                    if (valeurEntree == OPTION_QUITTER ||
                        valeurEntree == OPTION_ADDITION ||
                        valeurEntree == OPTION_SOUSTRACTION ||
                        valeurEntree == OPTION_MULTIPLICATION ||
                        valeurEntree == OPTION_DIVISION ||
                        valeurEntree == OPTION_FACT ||
                        valeurEntree == OPTION_SERIE_TAYLOR ||
                        valeurEntree == OPTION_RECTANGLE ||
                        valeurEntree == OPTION_TRIANGLE ||
                        valeurEntree == OPTION_NOMBRES_PREMIERS ||
                        valeurEntree == OPTION_EXPOSANT) break;
                   
                        Console.WriteLine("Erreur : l'opération est invalide.");

                }
                
                bool parseEstUnChiffre = double.TryParse(valeurEntree, out double resultatDuParse);


                bool estOperationSimple = valeurEntree == OPTION_ADDITION || valeurEntree == OPTION_SOUSTRACTION || valeurEntree == OPTION_DIVISION || valeurEntree == OPTION_MULTIPLICATION;
                bool estOperationComplexe = valeurEntree == OPTION_FACT || valeurEntree == OPTION_SERIE_TAYLOR;
                bool estOperationGraphique = valeurEntree == OPTION_RECTANGLE || valeurEntree == OPTION_TRIANGLE || valeurEntree == OPTION_NOMBRES_PREMIERS;

                if (valeurEntree == OPTION_QUITTER)
                {
                    Console.WriteLine();
                    Console.Write("Appuyer sur Entrée pour fermer la calculatrice.");
                    Console.ReadLine();
                    quitterCalculatrice = true;
                }
                else if (valeurEntree == OPTION_EXPOSANT)
                {
                    int exposant = ParseEntierSecuritaire("Entrer l'exposant : ", "Erreur : exposant non valide !");
                    operationAffichee = $"Opération : {resultat} ^ {exposant} = ";
                    string calcul = "\n\nCalculs :\n";
                    double resultatExposant = resultat;

                    if (exposant < 0)
                    {
                        double denominateur = resultat;
                        resultatExposant = 1 / resultatExposant;
                        for (int i = 1; i < -exposant; i++)
                        {
                            calcul += $"1/{denominateur} * 1/{resultat} = ";
                            resultatExposant *= 1 / resultat;
                            denominateur *= resultat;
                            calcul += $"1/{denominateur}\n";
                        }
                        operationAffichee += resultatExposant;
                        resultat = resultatExposant;
                        operationAffichee += calcul;
                    }
                    else if (exposant > 0)
                    {
                        for (int i = 1; i < exposant; i++)
                        {
                            calcul += $"{resultatExposant} * {resultat} = ";
                            resultatExposant *= resultat;
                            calcul += $"{resultatExposant}\n";
                        }
                        operationAffichee += resultatExposant;
                        resultat = resultatExposant;
                        operationAffichee += calcul;
                    }
                    else
                    {
                        operationAffichee = $"{resultat} ^ 0 = 1";
                        resultat = 1;
                    }
                }
                else if (estOperationSimple)
                {
                    double nombre = ParseDoubleSecuritaire("Entrer un nombre : ", "Erreur : nombre invalide !");
                    Console.WriteLine(nombre);

                    switch (valeurEntree)
                    {
                        case OPTION_ADDITION:
                            operationAffichee = $"\nOpération : {resultat} + {nombre} = {resultat + nombre}\n";
                            resultat += nombre;
                            break;
                        case OPTION_SOUSTRACTION:
                            operationAffichee = $"\nOpération : {resultat} - {nombre} = {resultat - nombre}\n";
                            resultat -= nombre;
                            break;
                        case OPTION_MULTIPLICATION:
                            operationAffichee = $"\nOpération : {resultat} * {nombre} = {resultat * nombre}\n";
                            resultat *= nombre;
                            break;
                        case OPTION_DIVISION:
                            if (nombre != 0)
                            {
                                operationAffichee = $"\nOpération : {resultat} / {nombre} = {resultat / nombre}\n";
                                resultat /= nombre;
                            }
                            else
                            {
                                operationAffichee = "\nOpération annulée : division par 0 impossible !";
                            }
                            break;
                    }
                }
                else if (estOperationComplexe)
                {
                    if (valeurEntree == OPTION_FACT)
                    {
                        uint nombre = ParseEntierPositif("Entrer le factoriel : ", "Erreur : factoriel non valide !");
                        if (nombre > 0)
                        {
                            string calcul = "Calculs : \n";
                            long factoriel = 1;
                            for (long i = nombre; i > 0; i--)
                            {
                                calcul += $"{factoriel} * {i} = ";
                                factoriel *= i;
                                calcul += $"{factoriel}\n";
                            }
                            operationAffichee = $"\nOpération : {nombre}! = {factoriel}\n\n{calcul}";
                            resultat = factoriel;
                        }
                        else
                        {
                            resultat = 1;
                            operationAffichee = "\nOpération : 0! = 1";
                        }
                    }
                    else // Série de Taylor
                    {
                        double xSerie = ParseDoubleSecuritaire("Entrer le x de la série de Taylor : ", "Erreur : x invalide !");
                        uint nSerie = ParseEntierPositif("Entrer le n de la série de Taylor : ", "Erreur : n invalide !");
                        double resultatSerie = 1;

                        string serieAff = "Série de Taylor : 1";
                        string calculAff = "Calculs : 1";
                        string valeursAff = "Valeurs : 1";

                        for (int i = 1; i <= nSerie; i++)
                        {
                            serieAff += $" + ({xSerie}^{i} / {i}!)";

                            double exposantVal = Exposant(xSerie, i);
                            double factorielVal = FactorielPourSerie(i);

                            calculAff += $" + {exposantVal:0.###} / {factorielVal:0.###}";
                            double terme = exposantVal / factorielVal;
                            resultatSerie += terme;
                            valeursAff += $" + {terme:0.###}";
                        }
                        resultat = resultatSerie;
                        operationAffichee = $"\n{serieAff}\n{calculAff}\n{valeursAff}\n";
                    }
                }
                else if (estOperationGraphique)
                {
                    if (valeurEntree == OPTION_NOMBRES_PREMIERS)
                    {
                        uint debut = ParseEntierPositif("Entrer un nombre de début : ", "Erreur : début invalide !");
                        uint fin = ParseEntierSuperieurAX("Entrer un nombre de fin : ", "Erreur : fin invalide !", debut);

                        string nombresPremiers = "";
                        int compteurPremiers = 0;

                        bool[] estPremier = Enumerable.Repeat(true, (int)fin + 1).ToArray();
                        estPremier[0] = false;
                        estPremier[1] = false;
                        uint limite = (uint)Math.Sqrt(fin);

                        for (uint p = 2; p <= limite; p++)
                        {
                            if (estPremier[p])
                            {
                                for (uint multiple = p * p; multiple <= fin; multiple += p)
                                {
                                    estPremier[multiple] = false;
                                }
                            }
                        }

                        for (uint i = 0; i <= fin; i++)
                        {
                            if (estPremier[i] && i >= debut)
                            {
                                compteurPremiers++;
                                nombresPremiers += (compteurPremiers % 7 == 0) ? $"{i}\n" : $"{i} ";
                            }
                        }

                        Console.WriteLine($"{compteurPremiers} nombres premiers de {debut} à {fin} : \n");
                        Console.WriteLine(nombresPremiers);
                        Console.WriteLine();
                        Console.Write("Appuyer sur Entrée pour continuer.\n");
                        Console.ReadLine();
                    }
                    else
                    {
                        double hauteur = ParseDoubleSuperieureAZero("Entrer une hauteur : ", "Erreur : hauteur invalide !");

                        if (valeurEntree == OPTION_RECTANGLE)
                        {
                            double largeur = ParseDoubleSuperieureAZero("Entrer une largeur : ", "Erreur : largeur invalide !");

                            for (int i = 0; i < Math.Ceiling(hauteur); i++)
                            {
                                Console.WriteLine(string.Concat(Enumerable.Repeat("* ", (int)Math.Ceiling(largeur))));
                            }
                            Console.WriteLine($"Aire      : {(largeur * hauteur):0.000}");
                            Console.WriteLine($"Périmètre : {(2 * largeur + 2 * hauteur):0.000}");
                            Console.WriteLine();
                            Console.Write("Appuyer sur entrée pour continuer.");
                            Console.ReadLine();
                        }
                        else // Triangle
                        {
                            for (int i = 0; i < Math.Round(hauteur); i++)
                            {
                                Console.WriteLine(string.Concat(Enumerable.Repeat("* ", (i + 1))));
                            }
                            double hypothenuse = Math.Sqrt((hauteur * hauteur) + (hauteur * hauteur));
                            Console.WriteLine($"Aire        : {((hauteur * hauteur) / 2):0.###}");
                            Console.WriteLine($"Hypoténuse : {hypothenuse:0.###}");
                            Console.WriteLine($"Périmètre   : {(hauteur + hauteur + hypothenuse):0.###}");
                            Console.WriteLine();
                            Console.Write("Appuyer sur entrée pour continuer.");
                            Console.ReadLine();
                        }
                    }
                }

            }
        }

        static double FactorielPourSerie(int x)
        {
            double resultat = 1;
            for (int i = x; i > 0; i--) resultat *= i;
            return resultat;
        }

        static double Exposant(double x, int n)
        {
            double resultat = 1;
            for (int i = 0; i < n; i++) resultat *= x;
            return resultat;
        }

        static double ParseDoubleSecuritaire(string question, string messageErreur)
        {
            bool valide = false;
            while (!valide)
            {
                Console.Write($"\n{question}");
                string entree = Console.ReadLine();
                valide = double.TryParse(entree, out double resultat);
                if (valide) return resultat;
                Console.WriteLine(messageErreur);
            }
            return 0.0;
        }

        static uint ParseEntierPositif(string question, string messageErreur)
        {
            while (true)
            {
                Console.Write($"\n{question}");
                string entree = Console.ReadLine();
                if (uint.TryParse(entree, out uint resultat)) return resultat;
                Console.WriteLine(messageErreur);
            }
        }

        static double ParseDoubleSuperieureAZero(string question, string messageErreur)
        {
            while (true)
            {
                double valeur = ParseDoubleSecuritaire(question, messageErreur);
                if (valeur > 0) return valeur;
                Console.WriteLine(messageErreur);
            }
        }

        static int ParseEntierSecuritaire(string question, string messageErreur)
        {
            while (true)
            {
                Console.Write($"{question}");
                string entree = Console.ReadLine();
                if (int.TryParse(entree, out int resultat)) return resultat;
                Console.WriteLine(messageErreur);
            }
        }

        static uint ParseEntierSuperieurAX(string question, string messageErreur, uint x)
        {
            while (true)
            {
                uint valeur = ParseEntierPositif(question, messageErreur);
                if (valeur >= x) return valeur;
                Console.WriteLine(messageErreur);
            }
        }
    }
}
