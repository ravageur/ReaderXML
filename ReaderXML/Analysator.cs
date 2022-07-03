using ReaderXML.common;

namespace ReaderXML
{
    internal sealed class Analysator
    {
        /// <summary>
        /// This list represent the current path into the xml to place other elements xml.
        /// </summary>
        private ElementXML _mainElement;
        private ElementXML? _elementXML; // We keep the last instance to do some operation easily.
        private int _deepLevel = 0;

        /// <summary>
        /// Time to analyze a part of xml.
        /// </summary>
        /// <param name="pathElements"></param>
        /// <param name="content"></param>
        /// <param name="analyzeElementTag"></param>
        /// <param name="analyzeElementValue"></param>
        /// <returns>Return true if an operation has been executed. Otherwise false.</returns>
        public bool Analyze(string content, bool analyzeElementTag, bool analyzeElementValue)
        {
            if (analyzeElementTag)
            {
                return this.AnalyzeTagElement(content);
            }
            else if (analyzeElementValue)
            {
                return this.AnalyzeContent(content);
            }

            return false;
        }



        /// <summary>
        /// Allow to analyze an tag element.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>True if added, otherwise false</returns>
        private bool AnalyzeTagElement(string content)
        {
            string[] tagElementSplitted;

            if (content.Contains(' '))
            {
                tagElementSplitted = content.Split(' ');
            }
            else
            {
                tagElementSplitted = new string[] { content }; 
            }

            tagElementSplitted[0] = this.CleanString(tagElementSplitted[0], true);

            if (tagElementSplitted[0][0] == '/')
            {
                this.BackElement(tagElementSplitted[0]);
            }
            else
            {
                this.AddElement(tagElementSplitted);
            }

            return true;
        }



        /// <summary>
        /// Allow to analyze the content of an element
        /// </summary>
        /// <param name="content"></param>
        private bool AnalyzeContent(string content)
        {
            return this._elementXML?.SetValue(content) ?? false;
        }



        /// <summary>
        /// Allow to add all atributes to the element and the value. Then we add this element to the track.
        /// </summary>
        /// <param name="contentSplitted"></param>
        private void AddElement(string[] contentSplitted)
        {
            ElementXML elementXML;

            if (this._deepLevel == 0)
            {
                elementXML = new(contentSplitted[0]);
                this.AddAttributes(contentSplitted, elementXML);

                this._mainElement = elementXML;
                this._elementXML = elementXML;
                this._deepLevel++;
            }
            else if(this._elementXML is not null)
            {
                elementXML = new(contentSplitted[0], this._elementXML);
                this.AddAttributes(contentSplitted, elementXML);

                this._elementXML?.ElementsXML.Add(elementXML);
                this._elementXML = elementXML;

                this._deepLevel++;
            }
        }



        /// <summary>
        /// Allow to add one or multiple attributes detected in the contentSplitted into the element.
        /// </summary>
        /// <param name="contentSplitted"></param>
        /// <param name="elementXML"></param>
        private void AddAttributes(string[] contentSplitted, ElementXML elementXML)
        {
            string[] attribute;

            for (int i = 1; i < contentSplitted.Length; i++)
            {
                attribute = contentSplitted[i].Split('=');
                elementXML.AttributesXML.Add(new(attribute[0], attribute[1][1..(attribute[1].Length - 1)]));
            }
        }



        /// <summary>
        /// Allow to remove an element by name in the track.
        /// </summary>
        /// <param name="nameElement"></param>
        private void BackElement(string tagElement)
        {
            this._elementXML = this._elementXML?.Parent;
        }



        public string CleanString(string stringToClean, bool isTagElement)
        {
            string stringClean = "";

            bool canAdd = !isTagElement;
            bool canAddClosed = false;

            if (isTagElement)
            {
                foreach (char character in stringToClean)
                {
                    if (character == '<' && canAddClosed == false)
                    {
                        canAdd = true;
                        continue;
                    }
                    else if (character == '>')
                    {
                        canAdd = false;
                        canAddClosed = true;
                    }

                    if (canAdd)
                    {
                        stringClean += character;
                    }
                }
            }

            return stringClean;
        }
        


        /// <summary>
        /// Allow to get the element xml that contain all datas.
        /// </summary>
        /// <returns></returns>
        public ElementXML GetResult()
        {
            return this._mainElement;
        }
    }
}
