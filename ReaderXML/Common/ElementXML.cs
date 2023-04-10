namespace ReaderXML.Common
{
    public sealed class ElementXML : AttributeXML
    {
        public ElementXML? Parent { get; private set; } = null;
        public List<AttributeXML> AttributesXML { get; private set; } = new();
        public List<ElementXML> ElementsXML { get; private set; } = new();

        public ElementXML(string name) : base(name)
        {
            this.Name = name;
            this.Value = "";
        }

        public ElementXML(string name, string value) : base(name, value)
        {
            this.Name = name;
            this.Value = value;
        }

        public ElementXML(string name, ElementXML elementXML) : base(name)
        {
            this.Name = name;
            this.Value = "";
            this.Parent = elementXML;
        }

        public bool Equals(string name)
        {
            return this.Name.Equals(name);
        }

        public ElementXML? Find(string name)
        {
            ElementXML? elementXML = null;

            foreach (ElementXML element in this.ElementsXML)
            {
                if (element.Name.Equals(name))
                {
                    elementXML = element;
                    break;
                }

                elementXML = element.Find(name);
            }

            return elementXML;
        }

        public new string ToString(int nbTabs)
        {
            string toString = "[SubTable]:";

            string newLine = "\n";

            for (int i = 0; i < nbTabs; i++)
            {
                newLine += "\t";
            }

            toString += newLine + "[Name] => " + this.Name;
            toString += newLine + "[Value] => " + (this.Value.Equals("") ? "NOT DEFINED" : this.Value);

            toString += newLine + "[AttributesXML]:" + (this.AttributesXML.Count == 0 ? " NO ATTRIBUTES DEFINED" : "");

            foreach (AttributeXML attributeXML in this.AttributesXML)
            {
                toString += newLine + "\t" + attributeXML.ToString(nbTabs + 2);
            }

            toString += newLine + "[SubTables]:" + (this.ElementsXML.Count == 0 ? " NO SUB TABLES DEFINED" : "");

            foreach (ElementXML elementXML in this.ElementsXML)
            {
                toString += newLine + "\t" + elementXML.ToString(nbTabs + 2);
            }

            return toString;
        }
    }
}
