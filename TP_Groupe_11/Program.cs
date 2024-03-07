using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Groupe_11.classes;
using TP_Groupe_11.interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using DecisionTree;
using ArbreGroupe11;

namespace TP_Groupe_11
{
    internal class Program
    {
        private static DataManager dataManager;
        private static Oenologue connectedOenologue;
        private static string oenologuesFilePath = "oenologues.csv";
        static void Main(string[] args)
        {


            //decisionTree.PredictWineQuality(pathToData, node);
            /*
            // Create an instance of ArbreDecisionImplementation
            ArbreDecisionImplementation arbreDecision = new ArbreDecisionImplementation();

            // Load the CSV data
            List<string[]> trainingData = arbreDecision.LoadCsvData("train_reduced.csv");

            // Define the attributes
            List<string> attributes = new List<string>()
            {
                "Alcohol",
                "Sulphates",
                "CitricAcid",
                "VolatileAcidity"
            };

            // Build the decision tree
            Node decisionTree = arbreDecision.BuildTree(trainingData, attributes);

            var wineInstance = new string[] { "0.8", "0.53", "0.25", "0.6"};
            var estimatedQuality = arbreDecision.EstimateWineQuality(wineInstance, decisionTree);

            Console.WriteLine($"Estimation de qualité du vin : {estimatedQuality}");
            var classification = arbreDecision.Classify(wineInstance, decisionTree);
            Console.WriteLine($"Classification: {classification}");

            // Print the decision tree
            //PrintDecisionTree(decisionTree, 0);
            */
            // Build the decision tree
            //Node decisionTree = arbreDecision.BuildTree(trainingData, attributes);

            //// Perform predictions
            //// Example 1
            //string[] example1 = { "9.8", "0.53", "0.25", "0.6" };
            //string prediction1 = arbreDecision.PredictQuality(decisionTree, example1);
            //string prediction2 = arbreDecision.EstimateWineQuality(example1,decisionTree);
            //Console.WriteLine("Prediction for Example 1: " + prediction2);
            //var decisionTree = new ArbreDecisionImplementation();
            //var data = decisionTree.LoadCsvData("C:/Users/dnous/OneDrive/Documents/datavin/test_reduced.csv");

            //var attributes = new List<string> {
            //    "alcohol",
            //    "sulphates",
            //    "citricacid",
            //    "volatileacidity",
            //    "quality"
            //};
            //var root = decisionTree.BuildTree(data, attributes);

            //var wineInstance = new string[] { "5.5", "0.91", "0.14", "0.32", "6" };
            ////var estimatedQuality = decisionTree.EstimateWineQuality(wineInstance, root);

            //////Console.WriteLine($"Estimation de qualité du vin : {estimatedQuality}");
            //var classification = decisionTree.Classify(wineInstance, root);
            //var classifications = decisionTree.CalculateEntropy(data);

            //Console.WriteLine($"Classification: {classification}");
            //Console.WriteLine($"Classification: {classifications}");
            //Console.ReadKey();
            //// Chemin du fichier CSV
            //string csvFilePath = "C:/Users/dnous/OneDrive/Documents/datavin/test_reduced.csv";
            //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    Delimiter = ";", // Enforce ',' as delimiter
            //    PrepareHeaderForMatch = header => header.Header.ToLower() // Ignore casing
            //};
            //// Charger le fichier CSV
            //using (var reader = new StreamReader(csvFilePath))
            //using (var csv = new CsvReader(reader, config))
            //{
            //    //csv.Configuration.Delimiter = ";"; // Spécifie le délimiteur comme point-virgule (;)
            //    //csv.Configuration.HeaderValidated = null; // Ignore la validation des en-têtes
            //    //csv.Read(); // Lire la première ligne (en-têtes) pour avancer le curseur
            //    //csv.ReadHeader();

            //    var records = new List<WineData>();
            //    while (csv.Read())
            //    {
            //        var record = csv.GetRecord<WineData>();
            //        records.Add(record);
            //    }

            //    // Convertir les données en types requis pour l'algorithme de l'arbre de décision
            //    var data = records.Select(d => new string[]
            //    {
            //        d.Alcohol.ToString(CultureInfo.InvariantCulture),
            //        d.Sulphates.ToString(CultureInfo.InvariantCulture),
            //        d.CitricAcid.ToString(CultureInfo.InvariantCulture),
            //        d.VolatileAcidity.ToString(CultureInfo.InvariantCulture),
            //        d.Quality.ToString(CultureInfo.InvariantCulture)
            //    }).ToList();

            //    // Le reste du code reste inchangé
            //    var attributes = new List<string>
            //    {
            //        "alcohol",
            //        "sulphates",
            //        "citricacid",
            //        "volatileacidity",
            //        "quality"
            //    };

            //    var decisionTree = new ArbreDecisionImplementation();
            //    var tree = decisionTree.BuildTree(data, attributes);
            //    var instance = new string[] { "9.8", "0.53", "0.25", "0.6", "3" };
            //    var classification = decisionTree.Classify(instance);

            //    Console.WriteLine($"Classification: {classification}");
            //    Console.ReadKey();
            //}


            string csvFilePath = "train_reduced.csv";
            string csvFilePathTest = "test_reduced.csv";
           

            List<string[]> trainingData = LoadCsvData(csvFilePathTest);
            List<string[]> datas = LoadCsvData(csvFilePath);
            List<string> attributes = new List<string> { "alcohol", "sulphates", "citricacid", "volatileacidity", "quality" };
            //List<string> attributes = GetAttributeNames(data);

            Test decisionTree = new Test();
            Node decisionTreeRoot = decisionTree.BuildTree(datas, attributes);
            string[] instance = new string[] {"9.8","0.53","0.25","0.6","3"};
            string classification = decisionTree.Classify(instance, decisionTreeRoot, attributes);
            Console.WriteLine($"Classification: {classification}");
            double entropie = Test.CalculateEntropy(datas);
            PrintTree(decisionTreeRoot);

            /* // Test de la méthode GetBestAttribute
             *  List<string[]> data = new List<string[]>
                {
                    new string[] { "1.0", "2.0", "3.0", "4.0", "A" },
                    new string[] { "2.0", "3.0", "4.0", "5.0", "B" },
                    new string[] { "3.0", "4.0", "5.0", "6.0", "A" },
                    new string[] { "4.0", "5.0", "6.0", "7.0", "B" }
                };
             double? splitValue;
             string bestAttribute = Test.GetBestAttribute(data, new List<string> { "1", "2", "3", "4" }, out splitValue);
             Console.WriteLine($"Best attribute: {bestAttribute}, Split value: {splitValue}");

             // Test de la méthode GetMostCommonClass
             string mostCommonClass = Test.GetMostCommonClass(data);
             Console.WriteLine($"Most common class: {mostCommonClass}");

             // Test de la méthode IsValidAttributeIndex
             Console.WriteLine($"Is valid attribute index: {Test.IsValidAttributeIndex(data, 2)}");

             // Test de la méthode IsValidNumericValue
             Console.WriteLine($"Is valid numeric value: {Test.IsValidNumericValue(new string[] { "3.0" }, 0, 2.5, true)}");

             // Test de la méthode IsNumericAttribute
             Console.WriteLine($"Is numeric attribute: {Test.IsNumericAttribute(data, 0)}");*/



            // Debut de code pour la gestion des utilisateurs et de la cave

            dataManager = new DataManager();
            LoadOenologues();

            // Création d'un oenologue par défaut (à titre d'exemple)
            Oenologue defaultOenologue = new Oenologue()
            {
                Identifiant = "O0001",
                Nom = "admin",
                Password = "admin"
            };
            dataManager.AddOenologue(defaultOenologue);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("=== Menu principal ===");
                Console.WriteLine("1. Se connecter");
                Console.WriteLine("2. Quitter");
                Console.Write("Choisissez une option : ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ConnectOenologue();
                        break;
                    case "2":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }

                Console.WriteLine();
            }
            // Fin de code pour la gestion des utilisateurs et de la cave 
        }

        static void PrintTree(Node node, string indent = "")
        {
            // Vérifie si le nœud est null
            if (node == null)
                return;

            // Si le nœud est une feuille
            if (node.IsLeaf)
            {
                // Affiche "Feuille" avec la classe associée
                Console.WriteLine($"{indent}Feuille: Classe = {node.Value}");
            }
            // Si le nœud est la racine
            else if (node.Parent == null)
            {
                // Affiche "Noeud racine" avec l'attribut associé
                Console.WriteLine($"{indent}Noeud racine: Attribut = {node.Attribute}");
            }
            // Sinon, le nœud est interne
            else
            {
                // Affiche "Noeud interne" avec l'attribut associé
                Console.WriteLine($"{indent}Noeud interne: Attribut = {node.Attribute}");
            }

            // Parcourt chaque enfant du nœud, en vérifiant d'abord si node.Children est null
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    // Affiche "Branche" avec la valeur de la branche associée
                    Console.WriteLine($"{indent}  Branche: Valeur = {child.Key}");
                    // Appel récursif de PrintTree pour l'enfant actuel
                    PrintTree(child.Value, indent + "    ");
                }
            }
        }
        static List<string[]> LoadCsvData(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", // Enforce ';' as delimiter
                PrepareHeaderForMatch = header => header.Header.ToLower() // Ignore casing
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                var records = new List<WineData>();
                while (csv.Read())
                {
                    var record = csv.GetRecord<WineData>();
                    records.Add(record);
                }
                var data = records.Select(d => new string[]
                {
                    d.Alcohol.ToString(CultureInfo.InvariantCulture),
                    d.Sulphates.ToString(CultureInfo.InvariantCulture),
                    d.CitricAcid.ToString(CultureInfo.InvariantCulture),
                    d.VolatileAcidity.ToString(CultureInfo.InvariantCulture),
                    d.Quality.ToString(CultureInfo.InvariantCulture)
                }).ToList();
                return data;
            }
        }

        // Gestion des vins et de la cave
        private static void ManageVin()
        {
            Cave cave = new Cave();
            bool exit = false;
            while (!exit)
            {
                AfficherMenu();
                Console.Write("Choix : ");
                string choix = Console.ReadLine();
                Console.WriteLine();

                switch (choix)
                {
                    case "1":
                        AjouterVin(cave);
                        break;
                    case "2":
                        MettreAjourVin(cave);
                        break;
                    case "3":
                        SupprimerVin(cave);
                        break;
                    case "4":
                        AugmenterQuantiteVin(cave);
                        break;
                    case "5":
                        RetirerQuantiteVin(cave);
                        break;
                    case "6":
                        AfficherListeVin(cave);
                        break;
                    case "7":
                        AfficherStock(cave);
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer !");
                        break;
                }

                Console.WriteLine();
            }
        }
        static void AfficherMenu()
        {
            Console.WriteLine("====== Menu De Gestion De Cave/Vin ======");
            Console.WriteLine("1. Ajouter un vin");
            Console.WriteLine("2. Mettre à jour un vin");
            Console.WriteLine("3. Supprimer un vin");
            Console.WriteLine("4. Augmenter la quantité d'un vin");
            Console.WriteLine("5. Retirer la quantité d'un vin");
            Console.WriteLine("6. Afficher la liste des vins");
            Console.WriteLine("7. Afficher le stock");
            Console.WriteLine("8. Retour au menu principale");
        }
        static void AjouterVin(Cave cave)
        {
            Console.WriteLine("Ajouter un vin :");

            // Demander les informations du vin à l'utilisateur
            Console.Write("Numéro de référence : ");
            string numRef = Console.ReadLine();

            Console.Write("Nom : ");
            string nom = Console.ReadLine();

            int anneeProduction;
            while (true)
            {
                Console.Write("Année de production : ");
                string anneeProductionStr = Console.ReadLine();

                if (int.TryParse(anneeProductionStr, out anneeProduction))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format d'année de production incorrect ! Veuillez réessayer.");
                }
            }

            double pourcentageAlcool;
            while (true)
            {
                Console.Write("Pourcentage d'alcool : ");
                string pourcentageAlcoolStr = Console.ReadLine();

                if (double.TryParse(pourcentageAlcoolStr, out pourcentageAlcool))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format de pourcentage d'alcool incorrect ! Veuillez réessayer.");
                }
            }
            Console.Write("Origine : ");
            string origine = Console.ReadLine();

            double prix;
            while (true)
            {
                Console.Write("Prix : ");
                string prixStr = Console.ReadLine();

                if (double.TryParse(prixStr, out prix))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format de prix incorrect ! Veuillez réessayer.");
                }
            }

            bool disponibilitee;
            while (true)
            {
                Console.Write("Disponibilité (true/false) : ");
                string disponibiliteStr = Console.ReadLine();

                if (bool.TryParse(disponibiliteStr, out disponibilitee))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format de disponibilité incorrect ! Veuillez réessayer.");
                }
            }

            Console.Write("Marque : ");
            string marque = Console.ReadLine();

            double sulfate;
            while (true)
            {
                Console.Write("Sulfate : ");
                string sulfateStr = Console.ReadLine();

                if (double.TryParse(sulfateStr, out sulfate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format de sulfate incorrect ! Veuillez réessayer.");
                }
            }

            double acidCitrique;
            while (true)
            {
                Console.Write("Acidité citrique : ");
                string acidCitriqueStr = Console.ReadLine();

                if (double.TryParse(acidCitriqueStr, out acidCitrique))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format d'acidité citrique incorrect ! Veuillez réessayer.");
                }
            }

            double acideVolatile;
            while (true)
            {
                Console.Write("Acidité volatile : ");
                string acideVolatileStr = Console.ReadLine();

                if (double.TryParse(acideVolatileStr, out acideVolatile))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format d'acidité volatile incorrect ! Veuillez réessayer.");
                }
            }

            int qualite;
            while (true)
            {
                Console.Write("Qualité : ");
                string qualiteStr = Console.ReadLine();

                if (int.TryParse(qualiteStr, out qualite))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format de qualité incorrect ! Veuillez réessayer.");
                }
            }

            // Saisir le type de vin
            Type_Vin typeVin;
            while (true)
            {
                Console.Write("Type de vin (Rouge/Blanc/Rose) : ");
                string typeVinStr = Console.ReadLine();
                if (Enum.TryParse(typeVinStr, true, out typeVin))
                    break;
                else
                    Console.WriteLine("Type de vin invalide !");
            }

            int quantite;
            while (true)
            {
                Console.Write("Quantité : ");
                string quantiteStr = Console.ReadLine();

                if (int.TryParse(quantiteStr, out quantite))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Format de quantité incorrect ! Veuillez réessayer.");
                }
            }

            // Saisir les cépages
            List<Cepage> cepages = new List<Cepage>();
            while (true)
            {
                Console.Write("Cépage (CabernetSauvignon/Merlot/Chardonnay/SauvignonBlanc/PinotNoir) (q pour terminer) : ");
                string cepageStr = Console.ReadLine();
                if (cepageStr.ToLower() == "q")
                    break;

                Cepage cepage;
                if (Enum.TryParse(cepageStr, true, out cepage))
                {
                    cepages.Add(cepage);
                }
                else
                {
                    Console.WriteLine("Cépage invalide !");
                }
            }


            // Créer un nouvel objet Vin avec les informations fournies
            Vin vin = new Vin(numRef, nom, anneeProduction, pourcentageAlcool, origine, prix, disponibilitee, marque, quantite, sulfate, acidCitrique, acideVolatile, qualite, typeVin );
            vin.Cepages = cepages;
            // Ajouter le vin à la cave
            cave.AjouterVin(vin);
        }
        static void MettreAjourVin(Cave cave)
        {
            Console.WriteLine("Mettre à jour un vin :");

            Console.Write("Numéro de référence du vin à mettre à jour : ");
            string numRef = Console.ReadLine();

            Vin vin = cave.RecherVinAvecRef(numRef);

            if (vin != null)
            {
                // Demander les nouvelles informations du vin à l'utilisateur
                Console.WriteLine("Saisissez les nouvelles informations (appuyez sur Entrée pour ignorer une information) :");
                Console.Write("Nom : ");
                string nom = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nom))
                    vin.Nom = nom;

                Console.Write("Année de production : ");
                string anneeProductionStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(anneeProductionStr))
                    vin.AnneeProduction = Convert.ToInt32(anneeProductionStr);
 
                Console.Write("Pourcentage d'alcool : ");
                string pourcentageAlcoolStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(pourcentageAlcoolStr))
                    vin.Pourcentage_alcool = Convert.ToDouble(pourcentageAlcoolStr);

                Console.Write("Origine : ");
                string origine = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(origine))
                    vin.Origine = origine;

                Console.Write("Prix : ");
                string prixStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(prixStr))
                    vin.Prix = Convert.ToDouble(prixStr);

                Console.Write("Disponibilité (true/false) : ");
                string disponibiliteeStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(disponibiliteeStr))
                    vin.Disponibilitee = Convert.ToBoolean(disponibiliteeStr);

                Console.Write("Marque : ");
                string marque = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(marque))
                    vin.Marque = marque;

                Console.Write("Sulfate : ");
                string sulfateStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(sulfateStr))
                    vin.Sulphate = Convert.ToDouble(sulfateStr);

                Console.Write("Acidité citrique : ");
                string acidCitriqueStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(acidCitriqueStr))
                    vin.Acid_citrique = Convert.ToDouble(acidCitriqueStr);

                Console.Write("Acidité volatile : ");
                string acideVolatileStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(acideVolatileStr))
                    vin.Acide_volatile = Convert.ToDouble(acideVolatileStr);

                Console.Write("Qualité : ");
                string qualite = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(qualite))
                    vin.Qualite = Convert.ToInt16(qualite);

                // Saisir le nouveau type de vin
                Type_Vin nouveauType;
                while (true)
                {
                    Console.Write("Nouveau type de vin (Rouge/Blanc/Rose) : ");
                    string typeStr = Console.ReadLine();

                    if (Enum.TryParse(typeStr, true, out nouveauType))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Type de vin invalide !");
                    }
                }

                vin.Type_Vin = nouveauType;

                // Saisir les nouveaux cépages
                List<Cepage> cepages = new List<Cepage>();
                while (true)
                {
                    Console.Write("Nouveau cépage (CabernetSauvignon/Merlot/Chardonnay/SauvignonBlanc/PinotNoir) et (q pour terminer) : ");
                    string cepageStr = Console.ReadLine();
                    if (cepageStr.ToLower() == "q")
                        break;

                    Cepage cepage;
                    if (Enum.TryParse(cepageStr, true, out cepage))
                    {
                        cepages.Add(cepage);
                    }
                    else
                    {
                        Console.WriteLine("Cépage invalide !");
                    }
                }

                vin.Cepages = cepages;

                // Mettre à jour le vin dans la cave
                cave.MettreAjourVin(vin);
            }
            else
            {
                Console.WriteLine($"Le vin avec la référence {numRef} n'existe pas !");
            }
        }
        static void SupprimerVin(Cave cave)
        {
            Console.WriteLine("Supprimer un vin :");

            Console.Write("Numéro de référence du vin à supprimer : ");
            string numRef = Console.ReadLine();

            cave.SupprimerVin(numRef);
        }
        static void AugmenterQuantiteVin(Cave cave)
        {
            Console.WriteLine("Augmenter la quantité d'un vin :");

            Console.Write("Numéro de référence du vin : ");
            string numRef = Console.ReadLine();

            Console.Write("Quantité à ajouter : ");
            int quantite = Convert.ToInt32(Console.ReadLine());

            cave.AugmenterQuantiteVin(numRef, quantite);
        }
        static void RetirerQuantiteVin(Cave cave)
        {
            Console.WriteLine("Retirer la quantité d'un vin :");

            Console.Write("Numéro de référence du vin : ");
            string numRef = Console.ReadLine();

            Console.Write("Quantité à retirer : ");
            int quantite = Convert.ToInt32(Console.ReadLine());

            cave.RetirerQuantiteVin(numRef, quantite);
        }
        static void AfficherListeVin(Cave cave)
        {
            Console.WriteLine("Liste des vins :");

            List<Vin> vins = cave.AfficherListeVin();

            foreach (Vin vin in vins)
            {
                vin.AfficherVin();
            }
        }
        static void AfficherStock(Cave cave)
        {
            Console.WriteLine("Stock des vins :");

            cave.AfficherStock();
        }
        // Fin de gestion des vins et de la cave

        // Debut pour la gestions des utilisateurs (Oenologue, Client, ProprietaireVignoble)
        private static void ShowOenologueMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("=== Menu Oenologue ===");
                Console.WriteLine("1. Gerer un client");
                Console.WriteLine("2. Gerer proprietaire de terrain");
                Console.WriteLine("3. Gerer la cave ou vin");
                Console.WriteLine("4. Ajouter un oenologue");
                Console.WriteLine("5. Afficher tous les oenologues");
                Console.WriteLine("6. Afficher tous les clients");
                Console.WriteLine("7. Déconnexion");
                Console.Write("Choisissez une option : ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ManageClients();
                        break;
                    case "2":
                        ManageTerrainOwners();                        
                        break;
                    case "3":
                        ManageVin();
                        break;
                    case "4":
                        AddOenologue();
                        break;
                    case "5":
                        ShowAllOenologues();
                        break;
                    case "6":
                        ShowAllClients();
                        break;
                    case "7":
                        exit = true;
                        DisconnectOenologue();
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void LoadOenologues()
        {
            if (File.Exists(oenologuesFilePath))
            {
                using (var reader = new StreamReader(oenologuesFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    IEnumerable<Oenologue> oenologues = csv.GetRecords<Oenologue>();
                    foreach (Oenologue oenologue in oenologues)
                    {
                        dataManager.AddOenologue(oenologue);
                    }
                }
            }
        }
        private static void SaveOenologues()
        {
            using (var writer = new StreamWriter(oenologuesFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(dataManager.GetAllOenologues());
            }
        }
        private static void ConnectOenologue()
        {
            Console.Write("Nom d'utilisateur : ");
            string username = Console.ReadLine();

            Console.Write("Mot de passe : ");
            string password = Console.ReadLine();

            connectedOenologue = dataManager.GetOenologueByUsernameAndPassword(username, password);

            if (connectedOenologue != null)
            {
                Console.WriteLine($"Connecté en tant que {connectedOenologue.Nom}.");
                Console.WriteLine("");
                ShowOenologueMenu();
            }
            else
            {
                Console.WriteLine("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }
        private static void DisconnectOenologue()
        {
            connectedOenologue = null;
            Console.WriteLine("Oenologue déconnecté.");
        }
        private static void AddOenologue()
        {
            Oenologue oenologue = new Oenologue();

            Console.Write("Nom : ");
            oenologue.Nom = Console.ReadLine();

            Console.Write("Email : ");
            oenologue.Email = Console.ReadLine();

            Console.Write("Spécialisation : ");
            oenologue.Domaine_expertise = Console.ReadLine();

            dataManager.AddOenologue(oenologue);
            SaveOenologues();

            Console.WriteLine("Oenologue ajouté avec succès.");
        }
        private static void ShowAllOenologues()
        {
            List<Oenologue> oenologues = dataManager.GetAllOenologues();

            if (oenologues.Count > 0)
            {
                Console.WriteLine("=== Liste des oenologues ===");
                foreach (Oenologue oenologue in oenologues)
                {
                    Console.WriteLine($"Nom : {oenologue.Nom}");
                    Console.WriteLine($"Email : {oenologue.Email}");
                    Console.WriteLine($"Spécialisation : {oenologue.Domaine_expertise}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Aucun oenologue enregistré.");
            }
        }
        private static void ManageClients()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== Gestion des clients ===");
                Console.WriteLine("1. Ajouter un client");
                Console.WriteLine("2. Supprimer un client");
                Console.WriteLine("3. Afficher tous les clients");
                Console.WriteLine("4. Retour au menu principal");
                Console.Write("Choisissez une option : ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddClient();
                        break;
                    case "2":
                        RemoveClient();
                        break;
                    case "3":
                        ShowAllClients();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void AddClient()
        {
            Client client = new Client();

            Console.Write("Nom : ");
            client.Nom = Console.ReadLine();

            Console.Write("Email : ");
            client.Email = Console.ReadLine();

            connectedOenologue.AddClient(client);
            SaveOenologues();

            Console.WriteLine("Client ajouté avec succès.");
        }
        private static void RemoveClient()
        {
            Console.Write("Entrez l'Identifiant du client à supprimer : ");
            string clientId = Console.ReadLine();

            if (!string.IsNullOrEmpty(clientId))
            {
                if (connectedOenologue.RemoveClient(clientId))
                {
                    SaveOenologues();
                    Console.WriteLine("Client supprimé avec succès.");
                }
                else
                {
                    Console.WriteLine("Aucun client trouvé avec cet ID.");
                }
            }
            else
            {
                Console.WriteLine("ID invalide. Veuillez réessayer.");
            }
        }
        private static void ShowAllClients()
        {
            List<Client> clients = connectedOenologue.GetAllClients();

            if (clients.Count > 0)
            {
                Console.WriteLine("=== Liste des clients ===");
                foreach (Client client in clients)
                {
                    Console.WriteLine($"ID : {client.Identifiant}");
                    Console.WriteLine($"Nom : {client.Nom}");
                    Console.WriteLine($"Email : {client.Email}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Aucun client enregistré.");
            }
        }
        private static void ManageTerrainOwners()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== Gestion des propriétaires du terrain ===");
                Console.WriteLine("1. Ajouter un propriétaire du terrain");
                Console.WriteLine("2. Supprimer un propriétaire du terrain");
                Console.WriteLine("3. Afficher tous les propriétaires du terrain");
                Console.WriteLine("4. Retour au menu principal");
                Console.Write("Choisissez une option : ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddTerrainOwner();
                        break;
                    case "2":
                        RemoveTerrainOwner();
                        break;
                    case "3":
                        ShowAllTerrainOwners();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void AddTerrainOwner()
        {
            ProprietaireVignoble terrainOwner = new ProprietaireVignoble();

            Console.Write("Nom : ");
            terrainOwner.Nom = Console.ReadLine();

            Console.Write("Email : ");
            terrainOwner.Email = Console.ReadLine();

            connectedOenologue.AddTerrainOwner(terrainOwner);
            SaveOenologues();

            Console.WriteLine("Propriétaire du terrain ajouté avec succès.");
        }
        private static void RemoveTerrainOwner()
        {
            Console.Write("Entrez l'Identifiant du propriétaire du terrain à supprimer : ");
            string terrainOwnerId = Console.ReadLine();

            if (!string.IsNullOrEmpty(terrainOwnerId))
            {
                if (connectedOenologue.RemoveTerrainOwner(terrainOwnerId))
                {
                    SaveOenologues();
                    Console.WriteLine("Propriétaire du terrain supprimé avec succès.");
                }
                else
                {
                    Console.WriteLine("Aucun propriétaire du terrain trouvé avec cet ID.");
                }
            }
            else
            {
                Console.WriteLine("ID invalide. Veuillez réessayer.");
            }
        }
        private static void ShowAllTerrainOwners()
        {
            List<ProprietaireVignoble> terrainOwners = connectedOenologue.GetAllTerrainOwners();

            if (terrainOwners.Count > 0)
            {
                Console.WriteLine("=== Liste des propriétaires du terrain ===");
                foreach (ProprietaireVignoble terrainOwner in terrainOwners)
                {
                    Console.WriteLine($"ID : {terrainOwner.Identifiant}");
                    Console.WriteLine($"Nom : {terrainOwner.Nom}");
                    Console.WriteLine($"Email : {terrainOwner.Email}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Aucun propriétaire du terrain enregistré.");
            }
        }

        // Fin pour la gestions des utilisateurs (Oenologue, Client, ProprietaireVignoble)
    }
}
