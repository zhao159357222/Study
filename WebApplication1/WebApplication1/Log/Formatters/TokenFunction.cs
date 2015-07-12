using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Leo.FrameWork.Log.Formatters
{
    internal abstract class TokenFunction
    {
        private string startDelimiter = string.Empty;
        private string endDelimiter = ")}";

        protected TokenFunction(string tokenStartDelimiter)
        {
            if (tokenStartDelimiter == null || tokenStartDelimiter.Length == 0)
            {
                throw new ArgumentNullException("tokenStartDelimiter");
            }
            this.startDelimiter = tokenStartDelimiter;
        }

        protected TokenFunction(string tokenStartDelimiter, string tokenEndDelimiter)
        {
            if (tokenStartDelimiter == null || tokenStartDelimiter.Length == 0)
            {
                throw new ArgumentNullException("tokenStartDelimiter");
            }
            if (tokenEndDelimiter == null || tokenEndDelimiter.Length == 0)
            {
                throw new ArgumentNullException("tokenEndDelimiter");
            }

            this.startDelimiter = tokenStartDelimiter;
            this.endDelimiter = tokenEndDelimiter;
        }
        /// <summary>
        /// Searches for token functions in the message and replace all with formatted values.
        /// </summary>
        /// <param name="messageBuilder">Message template containing tokens.</param>
        /// <param name="log">Log entry containing properties to format.</param>
        public virtual void Format(StringBuilder messageBuilder, LogEntity log)
        {
            int pos = 0;
            while (pos < messageBuilder.Length)
            {
                string messageString = messageBuilder.ToString();
                if (messageString.IndexOf(this.startDelimiter) == -1)
                {
                    break;
                }
                string tokenTemplate = GetInnerTemplate(pos, messageString);
                string tokenToReplace = this.startDelimiter + tokenTemplate + this.endDelimiter;
                pos = messageBuilder.ToString().IndexOf(tokenToReplace);

                string tokenValue = FormatToken(tokenTemplate, log);

                messageBuilder.Replace(tokenToReplace, tokenValue);
            }
        }

        /// <summary>
        /// Abstract method to process the token value between the start and end delimiter.
        /// </summary>
        /// <param name="tokenTemplate">Token value between the start and end delimiters.</param>
        /// <param name="log">Log entry to process.</param>
        /// <returns>Formatted value to replace the token.</returns>
        public abstract string FormatToken(string tokenTemplate, LogEntity log);

        /// <summary>
        /// Returns the template in between the paratheses for a token function.
        /// Expecting tokens in this format: {keyvalue(myKey1)}.
        /// </summary>
        /// <param name="startPos">Start index to search for the next token function.</param>
        /// <param name="message">Message template containing tokens.</param>
        /// <returns>Inner template of the function.</returns>
        protected virtual string GetInnerTemplate(int startPos, string message)
        {
            int tokenStartPos = message.IndexOf(this.startDelimiter, startPos) + this.startDelimiter.Length;
            int endPos = message.IndexOf(this.endDelimiter, tokenStartPos);
            return message.Substring(tokenStartPos, endPos - tokenStartPos);
        }
    }
}