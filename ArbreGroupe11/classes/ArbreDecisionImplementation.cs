using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbreGroupe11;

namespace ArbreGroupe11
{
    

    internal class ArbreDecisionImplementation : IDecisionTree
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
        public bool AllSameClass(List<string[]> data)
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
        public string GetBestAttribute(List<string[]> data, List<string> attributes, out double? splitValue)
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
        public double CalculateInformationGainNumeric(List<string[]> data, int attributeIndex, out double? splitValue)
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
        public double CalculateInformationGain(List<string[]> data, int attributeIndex)
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
        public double CalculateEntropy(List<string[]> data)
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
        public string GetMostCommonClass(List<string[]> data)
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
        public List<string> GetAttributeValues(List<string[]> data, int attributeIndex)
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
        public bool IsValidAttributeIndex(List<string[]> data, int attributeIndex)
        {
            return attributeIndex >= 0 && attributeIndex < data[0].Length;
        }
        public bool IsValidNumericValue(string[] instance, int attributeIndex, double splitValue, bool isLeftSubset)
        {
            double value;
            if (double.TryParse(instance[attributeIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return isLeftSubset ? value <= splitValue : value > splitValue;
            }
            return false;
        }
        public bool IsNumericAttribute(List<string[]> data, int attributeIndex)
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

        //string IDecisionTree.Classify(string[] instance)
        //{
        //    throw new NotImplementedException();
        //}

        public void ConfusionMatrix(string[] predictedLabels, string[] expertLabels, string[] labels)
        {
            throw new NotImplementedException();
        }

        public float Evaluate(string[] instances, string[] predictedLabels, string[] expertLabels)
        {
            throw new NotImplementedException();
        }
        /*
//private Node rootNode;
private List<string[]> data; // Store the loaded CSV data
private List<string> attributes;

//public Node BuildTree(List<string[]> trainingData, List<string> attributes)
//{
//    this.attributes = attributes;
//    this.data = trainingData;

//    List<string> classLabels = trainingData.Select(d => d.Last()).Distinct().ToList();

//    // Si tous les exemples ont la même classe, créer une feuille avec cette classe
//    if (classLabels.Count == 1)
//    {
//        return new Node(classLabels[0]);
//    }

//    // Si aucun attribut n'est disponible, créer une feuille avec la classe majoritaire
//    if (attributes.Count == 0)
//    {
//        string mostCommonClass = GetMostCommonClass(trainingData);
//        return new Node(mostCommonClass);
//    }

//    // Trouver l'attribut qui donne la meilleure division
//    string bestAttribute = GetBestAttribute(trainingData, attributes, out double? splitValue);

//    // Si l'attribut est numérique, diviser les exemples en fonction de la valeur de division
//    if (splitValue.HasValue)
//    {
//        List<string[]> leftSubset = trainingData.Where(d => double.Parse(d[attributes.IndexOf(bestAttribute)]) <= splitValue.Value).ToList();
//        List<string[]> rightSubset = trainingData.Where(d => double.Parse(d[attributes.IndexOf(bestAttribute)]) > splitValue.Value).ToList();

//        // Créer un nœud de décision avec l'attribut et la valeur de division
//        Node node = new Node(bestAttribute, splitValue.Value);

//        // Récursivement construire les sous-arbres
//        node.LeftChild = BuildTree(leftSubset, attributes);
//        node.RightChild = BuildTree(rightSubset, attributes);

//        return node;
//    }
//    else
//    {
//        // Diviser les exemples en fonction des valeurs possibles de l'attribut
//        List<string[]> subset;
//        Node node = null;

//        foreach (string attributeValue in GetAttributeValues(trainingData, attributes.IndexOf(bestAttribute)))
//        {
//            subset = trainingData.Where(d => d[attributes.IndexOf(bestAttribute)] == attributeValue).ToList();

//            // Si le sous-ensemble est vide, créer une feuille avec la classe majoritaire
//            if (subset.Count == 0)
//            {
//                string mostCommonClass = GetMostCommonClass(trainingData);
//                node = new Node(mostCommonClass);
//            }
//            else
//            {
//                // Créer un nœud de décision avec l'attribut et la valeur de division
//                node = new Node(bestAttribute, attributeValue);

//                // Récursivement construire le sous-arbre
//                node.Children.Add(attributeValue, BuildTree(subset, attributes));
//            }

//            node.AttributeValue = attributeValue;
//            node.ParentAttributeValue = bestAttribute;
//            node.Parent = new Node(bestAttribute);

//            // Ajouter le nœud à la liste des enfants du nœud parent
//            node.Parent.Children.Add(attributeValue, node);
//        }
//        // Vérifie si la variable node est null et retourne node.Parent si c'est le cas
//        if (node == null)
//        {
//            Node parentNode = new Node(bestAttribute);
//            parentNode.Children = new Dictionary<string, Node>();
//            return parentNode;
//        }

//        return node.Parent;
//    }
//}

public Node BuildTree(List<string[]> data, List<string> attributes)
{
this.data = data;
this.attributes = attributes;
//this.rootNode = new Node();
// Si aucune donnée n'est disponible, retourner un noeud avec la classe majoritaire
if (data.Count == 0) return new Node { Class = GetMostCommonClass(data) };

// Si toutes les instances ont la même classe, retourner un noeud avec cette classe
if (data.All(d => d.Last() == data[0].Last())) { return new Node { Class = data[0].Last() }; }

// Si aucun attribut n'est disponible, retourner un noeud avec la classe majoritaire

if (attributes.Count == 0) { return new Node { Class = data[0].Last() }; }

string bestAttributes = GetBestAttribute(data, attributes, out double? splitValue);

Node rootNode = new Node { Attribute = bestAttributes, SplitValue = splitValue };

Dictionary<string, List<string[]>> attributeValuesData = new Dictionary<string, List<string[]>>();

if (IsNumericAttribute(data, attributes.IndexOf(bestAttributes)))
{
// Si l'attribut est numérique, divisez les instances en fonction de la valeur de seuil

double threshop = splitValue ?? 0.0;
attributeValuesData["<="] = data.Where(d => double.Parse(d[attributes.IndexOf(bestAttributes)]) > threshop).ToList();
attributeValuesData[">"] = data.Where(d => double.Parse(d[attributes.IndexOf(bestAttributes)]) > threshop).ToList();

}
else
{
// Si l'attribut est catégorique, divisez les instances en fonction de chaque valeur d'attribut distincte
List<string> attributeValues = GetAttributeValues(data, attributes.IndexOf(bestAttributes));

foreach (string value in attributeValues)
{
  attributeValuesData[value] = data.Where(d => d[attributes.IndexOf(bestAttributes)] == value).ToList();
}
}

foreach (var kvp in attributeValuesData)
{
string attributeValue = kvp.Key;
List<string[]> attributeData = kvp.Value;

//if (attributeValuesData.Count == 0)
if (attributeData.Count == 0)
{
  // Si aucune donnée n'est disponible pour une valeur d'attribut spécifique, retourner un noeud avec la classe majoritaire
  rootNode.Children[attributeValue] = new Node { Class = GetMostCommonClass(data) };
}
else
{
  // Récursivement construire un sous-arbre pour la valeur d'attribut spécifique
  List<string> remainingAttributes = new List<string>(attributes);
  //remainingAttributes.Remove(attributeValue);
  remainingAttributes.Remove(bestAttributes);
  //rootNode.Children[attributeValue] = BuildTree(attributeData, remainingAttributes);
  rootNode.Children[attributeValue] = BuildTree(attributeData, remainingAttributes);
}
}

return rootNode;
}
public double CalculateEntropy(List<string[]> data)
{
List<string> classValues = GetAttributeValues(data, data[0].Length - 1);
double entropy = 0.0;

foreach (string value in classValues)
{
int count = data.Count(d => d.Last() == value);
double probability = (double)count / data.Count;
entropy -= probability * Math.Log(probability, 2);
}

return entropy;
}
public double CalculateInformationGain(List<string[]> data, int attributeIndex)
{
double entropy = CalculateEntropy(data);

List<string> attributeValues = GetAttributeValues(data, attributeIndex);
double attributeEntropy = 0.0;

foreach (string value in attributeValues)
{
List<string[]> subset = data.Where(d => d[attributeIndex] == value).ToList();
double subsetWeight = (double)subset.Count / data.Count;
attributeEntropy += subsetWeight * CalculateEntropy(subset);
}

return entropy - attributeEntropy;
}
public double CalculateInformationGainNumeric(List<string[]> data, int attributeIndex, out double? splitValue)
{
double entropy = CalculateEntropy(data);

List<double> attributeValues = data.Select(d => double.Parse(d[attributeIndex])).Distinct().ToList();
double bestAttributeEntropy = double.MaxValue;
splitValue = null;

foreach (double value in attributeValues)
{
List<string[]> subset1 = data.Where(d => double.Parse(d[attributeIndex]) <= value).ToList();
List<string[]> subset2 = data.Where(d => double.Parse(d[attributeIndex]) <= value).ToList();

double subsetEntropy = (subset1.Count / (double)data.Count) * CalculateEntropy(subset1) + (subset2.Count / (double)data.Count) * CalculateEntropy(subset2);

if(subsetEntropy < bestAttributeEntropy)
{
  bestAttributeEntropy = subsetEntropy;
  splitValue = value;
}

}

return entropy - bestAttributeEntropy;
}
public string Classify(string[] instance)
{
// A revenir traiter
return Classify(instance);
}
public string Classify(string[] instance, Node node)
{
//if (node.Children == null) { return null; } // Ajout de cette vérification
//A revenir traiter
if (node.Children == null) { return node.Class; }

//string attributeValue = instance[node.AttributeIndex];
string attributeValue = instance[attributes.IndexOf(node.Attribute)];

//if (node.Children.ContainsKey(attributeValue))
if (node.Children.TryGetValue(attributeValue, out var childNode))
{
// Récursivement classer l'instance en fonction des enfants du nœud correspondant à la valeur d'attribut
//return Classify(instance, node.Children[attributeValue]);
return Classify(instance, childNode);
}
else
{
// Si la valeur de l'attribut n'est pas présente dans les enfants du nœud, retourner la classe majoritaire du nœud
//return node.DefaultClass;
return GetMostCommonClass(data);
}

}
public void ConfusionMatrix(string[] predictedLabels, string[] expertLabels, string[] labels)
{
int[,] confusionMatrix = new int[labels.Length, labels.Length];

for (int i = 0; i < predictedLabels.Length; i++)
{
int predicatedIndex = Array.IndexOf(labels, predictedLabels[i]);
int expertIndex = Array.IndexOf(labels, expertLabels[i]);
if (predicatedIndex >= 0 && expertIndex >= 0 && predicatedIndex < labels.Length && expertIndex < labels.Length)
{
  confusionMatrix[predicatedIndex, expertIndex]++;
}
}
Console.WriteLine("Confusion Matrix: ");
Console.Write("\t");

for(int i = 0; i < labels.Length; i++)
{
Console.Write($"{labels[i]}\t");

for(int j = 0; j < labels.Length; j++)
{
  Console.Write($"{confusionMatrix[i, j]}\t");
}

Console.WriteLine();
}
}
public float Evaluate(string[] instances, string[] predictedLabels, string[] expertLabels)
{
int correctCount = 0;
for(int i=0; i < instances.Length; i++)
{
if (predictedLabels[i] == expertLabels[i]) { correctCount++; }
}

float accuracy = (float)correctCount / instances.Length;

return accuracy;
}
public List<string> GetAttributeValues(List<string[]> data, int attributeIndex)
{
if (attributeIndex >= 0 && attributeIndex < data[0].Length)
{
return data.Select(d => d[attributeIndex]).Distinct().ToList();
}
else
{
// Gérer l'index invalide ici
return new List<string>();
}
}
public string GetBestAttribute(List<string[]> data, List<string> attributes, out double? splitValue)
{
string bestAttribute = null;
double bestInformationGain = 0.0;
splitValue = null;

foreach (string attribute in attributes)
{
int attributeIndex = attributes.IndexOf(attribute);

if(IsNumericAttribute(data, attributeIndex))
{
  double informationGain = CalculateInformationGainNumeric(data, attributeIndex, out double? currentSplitValue);
  if(informationGain > bestInformationGain)
  {
      bestInformationGain = informationGain;
      bestAttribute = attribute;
      splitValue = currentSplitValue;
  }
}
else
{
  double informationGain = CalculateInformationGain(data, attributeIndex);
  if(informationGain > bestInformationGain)
  {
      bestInformationGain = informationGain;
      bestAttribute = attribute;
  }
}
}

return bestAttribute;
}
public string GetMostCommonClass(List<string[]> data)
{
return data.GroupBy(d => d.Last())
.OrderByDescending(g => g.Count())
.First().Key;
}
public bool IsNumericAttribute(List<string[]> data, int attributeIndex)
{
if (attributeIndex >= 0 && attributeIndex < data[0].Length)
{
double value = 0.0;
return data.All(d => double.TryParse(d[attributeIndex], out value));
}
else
{
// Gérer l'index invalide ici
return false;
}
}
public List<string[]> LoadCsvData(string filePath)
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
public string EstimateWineQuality(string[] wineAttributes, Node decisionTreeRoot)
{
return Classify(wineAttributes, decisionTreeRoot);
}
public string PredictQuality(Node decisionTree, string[] example)
{
Node currentNode = decisionTree;

while (currentNode.Class == null)
{
string attribute = currentNode.Attribute;
string attributeValue = example[attributes.IndexOf(attribute)];

if (currentNode.SplitValue.HasValue)
{
  double attributeNumericValue = double.Parse(attributeValue);

  if (attributeNumericValue <= currentNode.SplitValue.Value)
  {
      currentNode = currentNode.LeftChild;
  }
  else
  {
      currentNode = currentNode.RightChild;
  }
}
else
{
  if (currentNode.Children.ContainsKey(attributeValue))
  {
      currentNode = currentNode.Children[attributeValue];
  }
  else
  {
      // If the attribute value is not found in the tree, return null or handle the case accordingly
      return null;
  }
}
}

return currentNode.Class;
}
*/
    }

}
