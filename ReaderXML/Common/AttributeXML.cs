namespace ReaderXML.Common
{
    public class AttributeXML
    {
        public string Name { get; protected set; }
        public string Value { get; protected set; }

        public AttributeXML(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public AttributeXML(string name)
        {
            this.Name = name;
            this.Value = "";
        }

        public bool SetName(string name)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                this.Name = name;
                return true;
            }

            return false;
        }

        public bool SetValue(string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
            {
                this.Value = value;
                return true;
            }

            return false;
        }

        public string ToString(int nbTabs)
        {
            string toString = "[AttributeXML]:";

            string newLine = "\n";

            for (int i = 0; i < nbTabs; i++)
            {
                newLine += "\t";
            }

            toString += newLine + "[Name] => " + this.Name;
            toString += newLine + "[Value] => " + this.Value;

            return toString;
        }
    }
}
