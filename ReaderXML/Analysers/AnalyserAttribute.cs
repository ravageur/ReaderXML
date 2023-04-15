namespace ReaderXML.Analysers
{
    internal sealed class AnalyserAttribute
    {
        private AnalyserElement _analyserElement;

        public AnalyserAttribute(AnalyserElement analyserElement)
        {
            _analyserElement = analyserElement;
        }



        /// <summary>
        /// 
        /// We suppose the index has been 
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="content"></param>
        /// <param name="tagElementSplitted"></param>
        public void AnalyseAttributes(int index, string content, List<string> tagElementSplitted)
        {
            string nameOrValueTemp = string.Empty;
            bool isInDetectionOfValue = false;
            bool isInValue = false;



            for (int i = index; i < content.Length; i++)
            {
                if ((content[i] == ' ' || content[i] == '=') && !isInDetectionOfValue)
                {
                    tagElementSplitted.Add(nameOrValueTemp);
                    nameOrValueTemp = string.Empty;
                    isInDetectionOfValue = true;
                    i++; // We increase the index so that we can eliminate all spaces wich msut be avoided to register the value.
                    IgnoreAllCharactersSpecials(ref i, ref content);
                }

                if (isInDetectionOfValue && (content[i] == '\"' || content[i] == '\''))
                {
                    isInValue = !isInValue;

                    if (!isInValue)
                    {
                        tagElementSplitted.Add(nameOrValueTemp);
                        nameOrValueTemp = string.Empty;
                        isInDetectionOfValue = false;
                        i++;
                        IgnoreAllCharactersSpecials(ref i, ref content);
                    }
                    else
                    {
                        continue;
                    }
                }

                nameOrValueTemp += content[i];
            }
        }

        /// <summary>
        /// 
        /// Allows to ignore some characters wich we don't need.
        /// 
        /// This method will ignore the character '=' wich is must ignored.
        /// 
        /// </summary>
        /// 
        /// <param name="index"></param>
        /// <param name="content"></param>
        private void IgnoreAllCharactersSpecials(ref int index, ref string content)
        {
            byte characterByteTemp = Convert.ToByte(content[index]);

            while ((characterByteTemp < 33 || characterByteTemp > 126 || characterByteTemp == 61) && index < content.Length)
            {
                index++;
                characterByteTemp = Convert.ToByte(content[index]);
            }
        }
    }
}
