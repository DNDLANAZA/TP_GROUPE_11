using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TP_Groupe_11.classes;
using TP_Groupe_11.interfaces;

namespace DecisionTree
{
    public class Test
    {
        public Node BuildTree(List<string[]> data, List<string> attributes)
        {
            // Initialisation du nœud racine
            Node rootNode = new Node();

            // Appel récursif pour construire l'arbre
            BuildTreeRecursive(rootNode, data, attributes);

            // Retourne la racine de l'arbre
            return rootNode;
        }
        private void BuildTreeRecursive(Node node, List<string[]> data, List<string> attributes)
        {
            if (AllSameClass(data))
            {
                node.IsLeaf = true;
                node.Value = data[0][data[0].Length - 1];
                return;
            }

            if (attributes.Count == 0)
            {
                node.IsLeaf = true;
                node.Value = GetMostCommonClass(data);
                return;
            }

            string bestAttribute = GetBestAttribute(data, attributes, out double? splitValue);
            node.Attribute = bestAttribute;
            node.Children = new Dictionary<string, Node>();

            if (IsNumericAttribute(data, attributes.IndexOf(bestAttribute)))
            {
                List<string[]> leftSubset, rightSubset;
                if (splitValue != null)
                {
                    leftSubset = data.Where(instance => double.Parse(instance[attributes.IndexOf(bestAttribute)]) <= splitValue).ToList();
                    rightSubset = data.Where(instance => double.Parse(instance[attributes.IndexOf(bestAttribute)]) > splitValue).ToList();
                }
                else
                {
                    int midIndex = data.Count / 2;
                    leftSubset = data.Take(midIndex).ToList();
                    rightSubset = data.Skip(midIndex).ToList();
                }

                node.Children.Add("<=" + splitValue.ToString(), new Node { BranchValue = "<=" + splitValue.ToString() });
                BuildTreeRecursive(node.Children["<=" + splitValue.ToString()], leftSubset, attributes);

                node.Children.Add(">" + splitValue.ToString(), new Node { BranchValue = ">" + splitValue.ToString() });
                BuildTreeRecursive(node.Children[">" + splitValue.ToString()], rightSubset, attributes);
            }
            else
            {
                List<string> attributeValues = GetAttributeValues(data, attributes.IndexOf(bestAttribute));
                foreach (string value in attributeValues)
                {
                    List<string[]> subset = data.Where(instance => instance[attributes.IndexOf(bestAttribute)] == value).ToList();
                    node.Children.Add(value, new Node { BranchValue = value });
                    BuildTreeRecursive(node.Children[value], subset, attributes.Except(new List<string> { bestAttribute }).ToList());
                }
            }
        }
        public static bool AllSameClass(List<string[]> data)
        {
            string firstClass = data[0][data[0].Length - 1];
            for (int i = 1; i < data.Count; i++)
            {
                if (data[i][data[i].Length - 1] != firstClass)
                {
                    return false;
                }
            }
            return true;
        }
        public static string GetBestAttribute(List<string[]> data, List<string> attributes, out double? splitValue)
        {
            double maxInformationGain = double.MinValue;
            string bestAttribute = "";
            splitValue = null;

            foreach (string attribute in attributes)
            {
                int attributeIndex = Array.IndexOf(data[0], attribute);
                double informationGain;

                if (IsNumericAttribute(data, attributeIndex))
                {
                    informationGain = CalculateInformationGainNumeric(data, attributeIndex, out double? currentSplitValue);
                    if (informationGain > maxInformationGain)
                    {
                        maxInformationGain = informationGain;
                        bestAttribute = attribute;
                        splitValue = currentSplitValue;
                    }
                }
                else
                {
                    informationGain = CalculateInformationGain(data, attributeIndex);
                    if (informationGain > maxInformationGain)
                    {
                        maxInformationGain = informationGain;
                        bestAttribute = attribute;
                    }
                }
            }

            return bestAttribute;
        }
        public static double CalculateInformationGainNumeric(List<string[]> data, int attributeIndex, out double? splitValue)
        {
            double parentEntropy = CalculateEntropy(data);
            double maxInformationGain = double.MinValue;
            double bestSplitValue = 0.0;

            splitValue = null;

            if (!IsValidAttributeIndex(data, attributeIndex))
            {
                // Si l'index d'attribut n'est pas valide, retourne une valeur par défaut
                return 0.0;
            }

            List<double> attributeValues = new List<double>();
            foreach (var instance in data)
            {
                double value;
                if (double.TryParse(instance[attributeIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                {
                    attributeValues.Add(value);
                }
                else
                {
                    // Si le parsing échoue, ignore cette instance
                    continue;
                }
            }

            attributeValues.Sort();

            for (int i = 0; i < attributeValues.Count - 1; i++)
            {
                double currentSplitValue = (attributeValues[i] + attributeValues[i + 1]) / 2.0;
                List<string[]> leftSubset = data.Where(instance =>
                    IsValidNumericValue(instance, attributeIndex, currentSplitValue, true)).ToList();
                List<string[]> rightSubset = data.Where(instance =>
                    IsValidNumericValue(instance, attributeIndex, currentSplitValue, false)).ToList();

                double leftEntropy = CalculateEntropy(leftSubset);
                double rightEntropy = CalculateEntropy(rightSubset);
                double weightedEntropy = (leftSubset.Count / (double)data.Count) * leftEntropy +
                                         (rightSubset.Count / (double)data.Count) * rightEntropy;
                double informationGain = parentEntropy - weightedEntropy;

                if (informationGain > maxInformationGain)
                {
                    maxInformationGain = informationGain;
                    bestSplitValue = currentSplitValue;
                }
            }

            splitValue = bestSplitValue;
            return maxInformationGain;
        }
        public static double CalculateInformationGain(List<string[]> data, int attributeIndex)
        {
            double parentEntropy = CalculateEntropy(data);
            double totalInformationGain = 0.0;
            List<string> attributeValues = GetAttributeValues(data, attributeIndex);

            foreach (var value in attributeValues)
            {
                List<string[]> subset = data.Where(instance => instance[attributeIndex] == value).ToList();
                double subsetEntropy = CalculateEntropy(subset);
                totalInformationGain += ((double)subset.Count / data.Count) * subsetEntropy;
            }

            return parentEntropy - totalInformationGain;
        }
        public static double CalculateEntropy(List<string[]> data)
        {
            Dictionary<string, int> classCounts = new Dictionary<string, int>();
            int totalInstances = data.Count;
            double entropy = 0.0;

            foreach (var instance in data)
            {
                string classValue = instance[instance.Length - 1];
                if (classCounts.ContainsKey(classValue))
                    classCounts[classValue]++;
                else
                    classCounts.Add(classValue, 1);
            }

            foreach (var count in classCounts.Values)
            {
                double probability = (double)count / totalInstances;
                entropy -= probability * Math.Log(probability, 2);
            }

            return entropy;
        }
        public static string GetMostCommonClass(List<string[]> data)
        {
            Dictionary<string, int> classCounts = new Dictionary<string, int>();

            foreach (var instance in data)
            {
                string classValue = instance[instance.Length - 1];
                if (classCounts.ContainsKey(classValue))
                    classCounts[classValue]++;
                else
                    classCounts.Add(classValue, 1);
            }

            int maxCount = 0;
            string mostCommonClass = "";

            foreach (var kvp in classCounts)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mostCommonClass = kvp.Key;
                }
            }

            return mostCommonClass;
        }
        public static List<string> GetAttributeValues(List<string[]> data, int attributeIndex)
        {
            HashSet<string> attributeValues = new HashSet<string>();

            foreach (var instance in data)
            {
                // Vérifier si l'index attributeIndex est valide pour cette instance
                if (attributeIndex >= 0 && attributeIndex < instance.Length)
                {
                    attributeValues.Add(instance[attributeIndex]);
                }
            }

            return new List<string>(attributeValues);
        }
        public static bool IsValidAttributeIndex(List<string[]> data, int attributeIndex)
        {
            return attributeIndex >= 0 && attributeIndex < data[0].Length;
        }
        public static bool IsValidNumericValue(string[] instance, int attributeIndex, double splitValue, bool isLeftSubset)
        {
            double value;
            if (double.TryParse(instance[attributeIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return isLeftSubset ? value <= splitValue : value > splitValue;
            }
            return false;
        }
        public static bool IsNumericAttribute(List<string[]> data, int attributeIndex)
        {
            return IsValidAttributeIndex(data, attributeIndex) &&
                   IsValidNumericValue(data[0], attributeIndex, 0.0, true); // Check if the first instance has a valid numeric value
        }
        public string Classify(string[] instance)
        {
            return Classify(instance);
        }
        public string Classify(string[] instance, Node node, List<string> attributes)
        {
            // Si le nœud est une feuille, retourne la classe prédite
            if (node.IsLeaf)
            {
                return node.Value;
            }

            // Récupère l'index de l'attribut dans la liste des attributs
            int attributeIndex = attributes.IndexOf(node.Attribute);

            // Vérifie que l'index est valide
            if (attributeIndex < 0 || attributeIndex >= instance.Length)
            {
                // Gestion d'erreur : attribut non trouvé
                return "Erreur : attribut non trouvé.";
            }

            // Récupère la valeur de l'attribut pour l'instance actuelle
            string attributeValue = instance[attributeIndex];

            // Si la valeur de l'attribut n'est pas trouvée dans les enfants du nœud actuel, retourne la classe prédite du nœud actuel
            if (!node.Children.TryGetValue(attributeValue, out Node childNode))
            {
                return node.Value;
            }

            // Appel récursif de Classify avec le nœud enfant
            return Classify(instance, childNode, attributes);
        }
    }

}