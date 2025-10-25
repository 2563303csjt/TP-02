using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Auteur : Max-Antoine Nadeau
 * Matricule : 2563303
 * Cours : 420-SF1-JR - Introduction à la programmation
 * TP2 - Calculatrice
 * Description : Calculatrice complète avec opérations, séries et affichage graphique.
 * Date : 2 octobre 2025
 */

namespace tp02
{
    internal class Program
    {

        // Déclaration des constantes utilisées pour identifier les opérations
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

        // Menu principal affiché à l’écran
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
            // Variable qui contrôle la boucle principale
            bool quitterCalculatrice = false;
            // Résultat courant affiché dans le menu
            double resultat = 0;
            // Chaîne utilisée pour afficher la dernière opération effectuée
            string operationAffichee = "";

            // Boucle principale du programme : affiche le menu tant que l’utilisateur ne quitte pas
            while (!quitterCalculatrice)
            {
                // Affichage de l’en-tête et du menu
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

                // Demande à l’utilisateur de choisir une opération
                while (!operationEstValide)
                {
                    Console.Write("\nChoisir une opération : ");
                    valeurEntree = Console.ReadLine();

                    // Si l’utilisateur entre un nombre, le résultat est remplacé par cette valeur
                    if (double.TryParse(valeurEntree, out double nouveauResultat))
                    {
                        resultat = nouveauResultat;
                        operationAffichee = "";
                        break;
                    }

                    // Vérifie si l’entrée correspond à une option valide
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

                // Vérifie le type d’opération sélectionné
                bool estOperationSimple = valeurEntree == OPTION_ADDITION || valeurEntree == OPTION_SOUSTRACTION || valeurEntree == OPTION_DIVISION || valeurEntree == OPTION_MULTIPLICATION;
                bool estOperationComplexe = valeurEntree == OPTION_FACT || valeurEntree == OPTION_SERIE_TAYLOR;
                bool estOperationGraphique = valeurEntree == OPTION_RECTANGLE || valeurEntree == OPTION_TRIANGLE || valeurEntree == OPTION_NOMBRES_PREMIERS;

                // Si l’utilisateur choisit de quitter la calculatrice
                if (valeurEntree == OPTION_QUITTER)
                {
                    Console.WriteLine();
                    Console.Write("Appuyer sur Entrée pour fermer la calculatrice.");
                    Console.ReadLine();
                    quitterCalculatrice = true;
                }

                // ----- GESTION DE L’EXPOSANT -----
                else if (valeurEntree == OPTION_EXPOSANT)
                {
                    int exposant = ParseEntierSecuritaire("Entrer l'exposant : ", "Erreur : exposant non valide !");
                    operationAffichee = $"Opération : {resultat} ^ {exposant} = ";
                    string calcul = "\n\nCalculs :\n";
                    double resultatExposant = resultat;

                    // Vérifie le cas interdit : 0 exposant négatif
                    if (resultat == 0 && exposant < 0)
                    {
                        operationAffichee = "\nOpération annulée : impossible pour 0 d'avoir une puissance négative !\n";
                        continue;
                    }
                   
                    // Cas d’un exposant négatif
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

                    // Cas d’un exposant positif
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

                    // Cas exposant = 0
                    else
                    {
                        operationAffichee = $"{resultat} ^ 0 = 1";
                        resultat = 1;
                    }
                }

                // ----- OPÉRATIONS DE BASE (+, -, *, /) -----
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
                            // Vérifie la division par zéro
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

                // ----- OPÉRATIONS COMPLEXES (factoriel et série de Taylor) -----
                else if (estOperationComplexe)
                {
                    // ----- FACTORIEL -----
                    if (valeurEntree == OPTION_FACT)
                    {
                        uint nombre = ParseEntierSuperieurOuEgaleAX("Entrer le factoriel : ", "Erreur : factoriel non valide !", 0);
                        if (nombre > 0)
                        {
                            string calcul = "Calculs : \n";
                            //utilisation de ulong, car les x! ne peut pas etre negatif donc ulong nou permet d'avoir plus de memoire pour x! et donc de pouvoir calculer des x plus elever.
                            ulong factoriel = 1;
                            for (ulong i = nombre; i > 0; i--)
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

                    // ----- SÉRIE DE TAYLOR -----
                    else
                    {
                        double xSerie = ParseDoubleSecuritaire("Entrer le x de la série de Taylor : ", "Erreur : x invalide !");
                        uint nSerie = ParseEntierSuperieurOuEgaleAX("Entrer le n de la série de Taylor : ", "Erreur : n invalide, n doit être un entier positif!", 0);
                        double resultatSerie = 1;

                        string serieAff = "Série de Taylor : 1";
                        string calculAff = "Calculs : 1";
                        string valeursAff = "Valeurs : 1";

                        // Calcule chaque terme de la série
                        for (int i = 1; i <= nSerie; i++)
                        {
                            serieAff += $" + ({xSerie}^{i} / {i}!)";
                            double exposantVal = Exposant(xSerie, i);
                            double factorielVal = FactorielPourSerieDeTaylor(i);
                            calculAff += $" + {exposantVal:0.###} / {factorielVal:0.###}";
                            double terme = exposantVal / factorielVal;
                            resultatSerie += terme;
                            valeursAff += $" + {terme:0.###}";
                        }
                        resultat = resultatSerie;
                        operationAffichee = $"\n{serieAff}\n{calculAff}\n{valeursAff}\n";
                    }
                }

                // ----- OPÉRATIONS GRAPHIQUES (rectangle, triangle, nombres premiers) -----
                else if (estOperationGraphique)
                {
                    // ----- NOMBRES PREMIERS -----
                    if (valeurEntree == OPTION_NOMBRES_PREMIERS)
                    {
                        uint debut = ParseEntierSuperieurOuEgaleAX("Entrer un nombre de début : ", "Erreur : début invalide !", 1);
                        uint fin = ParseEntierSuperieurOuEgaleAX("Entrer un nombre de fin : ", "Erreur : fin invalide !", debut);

                        string nombresPremiers = "";
                        int compteurPremiers = 0;

                        bool[] estPremier = Enumerable.Repeat(true, (int)fin + 1).ToArray();
                        estPremier[0] = false;
                        estPremier[1] = false;
                        uint limite = (uint)Math.Sqrt(fin);

                        // Crible d'Ératosthène
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

                        // Affiche les nombres premiers trouvés
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

                    // ----- RECTANGLE ET TRIANGLE -----
                    else
                    {
                        double hauteur = ParseDoubleSuperieureAZero("Entrer une hauteur : ", "Erreur : hauteur invalide !");

                        if (valeurEntree == OPTION_RECTANGLE)
                        {
                            double largeur = ParseDoubleSuperieureAZero("Entrer une largeur : ", "Erreur : largeur invalide !");
                            // Affiche un rectangle d’étoiles selon la hauteur et la largeur
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
                        else // Triangle rectangle isocèle
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

        // Calcule le factoriel, utilisé dans la série de Taylor.
        // Je n’utilise pas cette fonction pour l’opération factoriel, car cette dernière requiert un affichage des calculs.
        static double FactorielPourSerieDeTaylor(int x)
        {
            double resultat = 1;
            for (int i = x; i > 0; i--) resultat *= i;
            return resultat;
        }

        // Calcule x^y
        // Je n’utilise pas cette fonction pour l’opération exposant, car cette opération requiert un affichage détaillé.
        static double Exposant(double x, int n)
        {
            double resultat = 1;
            for (int i = 0; i < n; i++) resultat *= x;
            return resultat;
        }

        // Les fonctions suivantes sont utilisées pour valider et convertir les entrées utilisateur.
        // Elles permettent d’éviter la répétition de code, car plusieurs opérations demandent les mêmes types de données.

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

        static double ParseDoubleSuperieureAZero(string question, string messageErreur)
        {
            bool valide = false;
            while (!valide)
            {
                double valeur = ParseDoubleSecuritaire(question, messageErreur);
                if (valeur > 0)
                {
                    valide = true;
                    return valeur;
                }
                Console.WriteLine(messageErreur);
            }
        }

        static int ParseEntierSecuritaire(string question, string messageErreur)
        {
            bool valide = false;
            while (!valide)
            {
                Console.Write($"{question}");
                string entree = Console.ReadLine();
                if (int.TryParse(entree, out int resultat))
                {
                    valide = true;
                    return resultat;
                }
                Console.WriteLine(messageErreur);
            }
        }

        // Parse un entier positif supérieur ou égal à X.
        // Cette fonction est utilisée pour le calcul des nombres premiers et pour toute entrée nécessitant un uint ≥ 0.
        static uint ParseEntierSuperieurOuEgaleAX(string question, string messageErreur, uint x)
        {   
            bool valide = false
            while (!valide)
            {
                Console.Write($"\n{question}");
                string entree = Console.ReadLine();

                if (uint.TryParse(entree, out uint valeur))
                {
                    if (valeur >= x)
                    {
                        valide = true;
                        return valeur;
                    }
                }

                Console.WriteLine(messageErreur);
            }

        }
    }
}
