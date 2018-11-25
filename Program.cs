
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "1.svg";
            string docType = "<!DOCTYPE svg [<!ATTLIST item id ID #REQUIRED>]>";
            string docTypePath = "_" + path;
            if (!File.Exists(docTypePath))
            {
                var writer = File.CreateText(docTypePath);
                writer.WriteLine(docType);
                var originalSvg = File.ReadAllLines(path);
                writer.Close();
                File.AppendAllLines(docTypePath, originalSvg);

            }
            XmlDocument doc = new XmlDocument();
            doc.Load(docTypePath);
            var node = doc.SelectSingleNode("//*[@id = 'rb']");
            Stack<XmlNode> stack = new Stack<XmlNode>();
            addChildsTo(node, stack);
            var k = 0;
            foreach(var item in stack){
                Console.WriteLine("item -->"+ item.InnerXml);
                Console.WriteLine("k----->" + k++);
            }
            Console.WriteLine("Hello World!");
        }
        static void addChildsTo(XmlNode node, Stack<XmlNode> stack)
        {
            stack.Push(node);
            if (node.HasChildNodes)
            {
                var childs = node.ChildNodes;
                for (var i = 0; i < childs.Count; i++)
                {
                    var child = childs.Item(i);
                    addChildsTo(child, stack);
                }
            }
        }
    }
}
