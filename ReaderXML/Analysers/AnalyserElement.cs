using ReaderXML.Common;

namespace ReaderXML.Analysers
{
    internal sealed class AnalyserElement
    {
        private readonly Analyser _analyser;

        public AnalyserElement(Analyser analyser)
        {
            _analyser = analyser;
        }

        public void Analyse(string content)
        {
            if (content[0] == '<' && content[1] == '/')
            {
                _analyser.BackElement();
                return;

                // In case we have "</to>", we don't need to analyze it, because it is already added.
                // We are also in one character by one character to read. So it will not be a problem to do this.
                //
                // BUT, if there must have some threads to improve the performance. It can be a problem, it hasn't been
                // tested to have multiple threads.
                //
                // The method "BackElement" from the class "Analyser" will certainly
                // not work in case there are multiple threads..
            }

            List<string> tagElementSplitted = new() { "" };

            if (content.Contains(' '))
            {
                int index = 0;

                // We will take the first word wich is the name of the element.

                for (int i = 0; i < content.Length; i++)
                {
                    if (content[i] == ' ')
                    {
                        // We have the name of this element, but we must ignore all these characters useless between the
                        // name of this element and his attributes.
                        IgnoreAllCharactersSpecials(ref i, ref content);
                        index = i;
                        break;
                    }
                    else
                    {
                        tagElementSplitted[0] += content[i];
                    }
                }

                // We have the name of this element. Now we will analyse all attributes.

                new AnalyserAttribute(this).AnalyseAttributes(index, content, tagElementSplitted);
            }
            else
            {
                tagElementSplitted = new() { content };
            }

            tagElementSplitted[0] = CleanString(tagElementSplitted[0], true);

            if (content[content.Length - 2] == '/')
            {
                _analyser.AddElement(tagElementSplitted.ToArray());
                _analyser.BackElement();
            }
            else
            {
                _analyser.AddElement(tagElementSplitted.ToArray());
            }
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
        /// 
        /// Allows to ignore some characters wich we don't need.
        /// 
        /// </summary>
        /// 
        /// <param name="index"></param>
        /// <param name="content"></param>
        public void IgnoreAllCharactersSpecials(ref int index, ref string content)
        {
            byte characterByteTemp = Convert.ToByte(content[index]);

            while ((characterByteTemp < 33 || characterByteTemp > 126) && index < content.Length)
            {
                index++;
                characterByteTemp = Convert.ToByte(content[index]);
            }
        }
    }
}
