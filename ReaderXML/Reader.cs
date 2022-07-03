using ReaderXML.common;

namespace ReaderXML
{
    /// <summary>
    /// This reader has been created to help some people to read very easily all these xml files.
    /// </summary>
    public sealed class Reader
    {
        private string _xml = "";
        private string _content = "";
        private List<char> _flow = new() { ' ', ' ',  ' ', ' ', ' ', ' ', ' ' };

        private readonly Analysator _analysator = new();

        private bool _isInComment = false;
        private bool _isInElement = false;

        public ElementXML ReadFile(string pathFileXML)
        {
            if (File.Exists(pathFileXML))
            {
                this._xml = File.ReadAllText(pathFileXML);

                int index = PreReadFile();

                for (int i = index; i < this._xml.Length; i++)
                {

                    this.AddCharacterToFlow(this._xml[i]);
                    this.IsThereComment();

                    if (this._isInComment) // We don't need to waste performance for useless comments.
                    {
                        continue;
                    }

                    this._content += this._flow[3];
                    this.IsThereElement();
                    
                }

                return this._analysator.GetResult();
            }
            
            return new ElementXML("FILE NOT READABLE");
        }



        /// <summary>
        /// This method is here to help at the start to find the first element xml.
        /// </summary>
        /// <param name="character"></param>
        private int PreReadFile()
        {
            bool isInElement = false;
            int index = 0;

            for (int i = 0; i < this._xml.Length; i++)
            {
                this.AddCharacterToFlow(this._xml[i]);

                if (this.IsThereStartElementStart())
                {
                    isInElement = true;
                }

                if (isInElement)
                {
                    this._content += this._flow[3];
                }

                if (this._flow[3] == '>' && this._flow[2] == '?')
                {
                    this._analysator.Analyze(this._content.Remove(1, 1), true, false);
                    this._content = "";
                    index++;
                    break;
                }

                index++;
            }

            return index;
        }



        /// <summary>
        /// Allow to update the flow to detect some things like the start of a comment.
        /// </summary>
        /// <param name="character"></param>
        private void AddCharacterToFlow(char character)
        {
            this._flow.Add(character);
            this._flow.RemoveAt(0);
        }



        /// <summary>
        /// Detect if we enter or get out of a comment.
        /// </summary>
        private void IsThereComment()
        {
            if (this.IsThereStartComment())
            {
                this._isInComment = true;
            }
            else if (this.IsThereEndComment())
            {
                this._isInComment = false;
            }
        }



        /// <summary>
        /// Detect if we enter into a comment.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>A boolean</returns>
        private bool IsThereStartComment()
        {
            return new string(this._flow.ToArray())[3..].Contains("<!--");
        }



        /// <summary>
        /// Detect if we get out of a comment.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>A boolean</returns>
        private bool IsThereEndComment()
        {
            return new string(this._flow.ToArray())[0..3].Contains("-->");
        }



        /// <summary>
        /// Allow to detect if there is an element tag. If there is one. It will try to guess if it is the end or the start
        /// of an element tag.
        /// </summary>
        private void IsThereElement()
        {
            if (this.IsThereStartElementStart() && !this._isInElement)
            {
                this._content = CleanString(this._content, false, true);

                string contentTrimmed = this._content.Trim();

                if (contentTrimmed.Length > 1)
                {
                    this._analysator.Analyze(contentTrimmed, false, true);
                }

                this._content = "<";
                this._isInElement = true;
            }
            else if (this.IsThereEndElementStart() && !this._isInElement)
            {
                this._content = CleanString(this._content, false, true);
                if (this._analysator.Analyze(this._content[..^1].Trim(), false, true))
                {
                    this._content = this._content[^1..];
                }

                this._content = "<";
                this._isInElement = true;
            }
            else if (this.IsThereStartElementEnd() && this._isInElement)
            {
                if (this._analysator.Analyze(this._content[..^1], true, false))
                {
                    this._content = "";
                }

                this._isInElement = false;
            }
            else if (this.IsThereEndElementEnd() && this._isInElement)
            {
                if (this._analysator.Analyze(this._content, true, false))
                {
                    this._content = "";
                }

                this._isInElement = false;
            }
        }



        /// <summary>
        /// Detect if we enter into a start element.
        /// </summary>
        /// <returns>A boolean</returns>
        private bool IsThereStartElementStart()
        {
            return this._flow[3] == '<' && !((this._flow[2] == '\'' && this._flow[4] == '\'') || (this._flow[2] == '\"' && this._flow[4] == '\"')) && !(this._flow[4] == '/');
        }



        /// <summary>
        /// Detect if we get out of the start element.
        /// </summary>
        /// <returns>A boolean</returns>
        private bool IsThereStartElementEnd()
        {
            return this._flow[3] == '>' && !((this._flow[2] == '\'' && this._flow[4] == '\'') || (this._flow[2] == '\"' && this._flow[4] == '\"'));
        }



        /// <summary>
        /// Detect if we enter into an end element.
        /// </summary>
        /// <returns>A boolean</returns>
        private bool IsThereEndElementStart()
        {
            return this._flow[3] == '<' && !(this._flow[4] == '\'' || this._flow[2] == '\"') && this._flow[4] == '/';
        }



        /// <summary>
        /// Detect if we get out of the end element.
        /// </summary>
        /// <returns>A boolean</returns>
        private bool IsThereEndElementEnd()
        {
            return this._flow[3] == '>' && !((this._flow[2] == '\'' && this._flow[4] == '\'') || (this._flow[2] == '\"' && this._flow[4] == '\"'));
        }

        private string CleanString(string stringToClean, bool cleanTag, bool cleanValue)
        {
            string stringClean = "";

            if (cleanTag)
            {
                bool canAdd = false;
                bool canAddClosed = false;

                foreach (char character in stringToClean)
                {
                    if (canAddClosed == false && character == '<')
                    {
                        canAdd = true;
                        canAddClosed = true;
                    }
                    else if (character == '>')
                    {
                        canAdd = false;
                    }

                    if (canAdd)
                    {
                        stringClean += character;
                    }
                }
            }
            else if (cleanValue)
            {
                for (int i = stringToClean.Length - 1; i >= 0; i--)
                {
                    if (!(stringToClean[i] == '<' || DetectAnyEscape(stringToClean[i])))
                    {
                        stringToClean = stringToClean[0..(i + 2)];
                        break;
                    }
                }

                for (int i = 0; i < stringToClean.Length; i++)
                {
                    if (!(stringToClean[i] == '>' || DetectAnyEscape(stringToClean[i])))
                    {
                        stringClean = stringToClean[i..];
                        break;
                    }
                }
            }



            return stringClean;
        }

        private bool DetectAnyEscape(char characterToVerify)
        {
            char[] allEscapes = new char[]
            {
                '\'',
                '\"',
                '\\',
                '\0',
                '\a',
                '\b',
                '\f',
                '\n',
                '\r',
                '\t',
                '\v',
            };

            foreach (char escapeCharacter in allEscapes)
            {
                if (escapeCharacter == characterToVerify)
                {
                    return true;
                }
            }

            return false;
        }
    }
}