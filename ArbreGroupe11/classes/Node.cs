using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbreGroupe11
{
    public class Node
    {
        public string Attribute { get; set; }
        public List<string> Attributes { get; set; }
        public string Value { get; set; }
        public bool IsLeaf { get; set; }
        public Node Parent { get; set; }
        public string BranchValue { get; set; }
        public Dictionary<string, Node> Children { get; set; }
        public Node()
        {
            Children = new Dictionary<string, Node>();
        }

        /*
        public string Attribute { get; set; }
        public double? SplitValue { get; set; }
        public string Class { get; set; }
        public string AttributeValue { get; set; }
        public string ParentAttributeValue { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public Node Parent { get; set; }
        public Dictionary<string, Node> Children { get; set; }

        public Node()
        {
            Children = new Dictionary<string, Node>();
        }

        public Node(string attribute)
            : this()
        {
            Attribute = attribute;
        }

        public Node(string attribute, double splitValue)
            : this(attribute)
        {
            SplitValue = splitValue;
        }

        public Node(string attribute, string attributeValue)
            : this(attribute)
        {
            AttributeValue = attributeValue;
        }
        public bool IsLeaf()
        {
            return Class != null;
        }*/
    }
}
