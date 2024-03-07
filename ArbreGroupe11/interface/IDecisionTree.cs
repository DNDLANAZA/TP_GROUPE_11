using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArbreGroupe11;

namespace ArbreGroupe11
{
    internal interface IDecisionTree
    {
        Node BuildTree(List<string[]> data, List<string> attributes); // pour contruire le noeud {a besoin de GetBestAttribute, CalculateInformationGain et genere noeud }
        string Classify(string[] instance); // 
        string Classify(string[] instance, Node node, List<string> attributes);
        string GetBestAttribute(List<string[]> data, List<string> attributes, out double? splitValue);
        double CalculateInformationGainNumeric(List<string[]> data, int attributeIndex, out double? splitValue);
        double CalculateInformationGain(List<string[]> data, int attributeIndex);
        double CalculateEntropy(List<string[]> data);
        string GetMostCommonClass(List<string[]> data);
        List<string> GetAttributeValues(List<string[]> data, int attributeIndex);
        bool IsNumericAttribute(List<string[]> data, int attributeIndex);
        float Evaluate(string[] instances, string[] predictedLabels, string[] expertLabels);
        void ConfusionMatrix(string[] predictedLabels, string[] expertLabels, string[] labels);
    }
}
