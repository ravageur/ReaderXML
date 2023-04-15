using ReaderXML.Common;

namespace ReaderXML.Analysers
{
    public sealed class Analyser
    {
        /// <summary>
        /// 
        /// This list represent the current path into the xml to place other elements xml.
        /// 
        /// </summary>
        private ElementXML _mainElement = new("");
        private ElementXML? _elementXML; // We keep the last instance to do some operation easily.
        private int _deepLevel = 0;




        /// <summary>
        /// 
        /// Time to analyze a part of xml.
        /// 
        /// </summary>
        /// 
        /// <param name="pathElements"></param>
        /// <param name="content"></param>
        /// <param name="analyzeElement"></param>
        /// <param name="analyzeElementValue"></param>
        /// 
        /// <returns>Return true if an operation has been executed. Otherwise false.</returns>
        public bool Analyze(string content, bool analyzeElement, bool analyzeElementValue)
        {
            if (analyzeElement)
            {
                // Analyse the element (Between '<' and '/>')
                new AnalyserElement(this).Analyse(content);
                return true;
            }
            else if (analyzeElementValue)
            {
                // Analyse the content of the element.
                return _elementXML?.SetValue(content) ?? false;
            }

            return false;
        }





        /// <summary>
        /// 
        /// Allow to add all atributes to the element and the value. Then we add this element to the track.
        /// 
        /// </summary>
        /// <param name="contentSplitted"></param>
        public void AddElement(string[] contentSplitted)
        {
            ElementXML elementXML;

            if (_deepLevel == 0)
            {
                elementXML = new(contentSplitted[0]);
                AddAttributes(contentSplitted, elementXML);

                _mainElement = elementXML;
                _elementXML = elementXML;
                _deepLevel++;
            }
            else if (_elementXML is not null)
            {
                elementXML = new(contentSplitted[0], _elementXML);
                AddAttributes(contentSplitted, elementXML);

                _elementXML?.ElementsXML.Add(elementXML);
                _elementXML = elementXML;

                _deepLevel++;
            }
        }





        /// <summary>
        /// 
        /// Allow to return to the parent of the element.
        /// 
        /// </summary>
        public void BackElement()
        {
            _elementXML = _elementXML?.Parent;
        }





        /// <summary>
        /// 
        /// Allow to add one or multiple attributes detected in the contentSplitted into the element.
        /// 
        /// </summary>
        /// <param name="contentSplitted"></param>
        /// <param name="elementXML"></param>
        private void AddAttributes(string[] contentSplitted, ElementXML elementXML)
        {
            int i = 1;

            while (i < contentSplitted.Length)
            {
                elementXML.AttributesXML.Add(new(contentSplitted[i], contentSplitted[i + 1]));
                i += 2;
            }
        }





        /// <summary>
        /// 
        /// Allow to get the element xml that contain all datas.
        /// 
        /// </summary>
        /// <returns></returns>
        public ElementXML GetResult()
        {
            return _mainElement;
        }
    }
}
