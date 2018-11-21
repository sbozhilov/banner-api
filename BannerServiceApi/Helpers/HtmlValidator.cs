using System;
using System.Linq;
using HtmlAgilityPack;

namespace BannerServiceApi.Helpers
{
    /// <summary>
    /// Helper class for determining if a string contains an html based on assumption
    /// </summary>
    public class HtmlValidator
    {
        private static readonly Logger Logger = new Logger();

        /// <summary>
        /// Determines if HtmlNode has valid html tags
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <returns></returns>
        private static bool IsItHtml(HtmlNode htmlNode)
        {
            return !(htmlNode.Descendants() ?? throw new InvalidOperationException()).All(n => n.NodeType == HtmlNodeType.Text);
        }

        /// <summary>
        /// Determines if the string contains a valid html based on assumptions
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static bool ContainsHtmlElements(string html)
        {
            try
            {
                var document = new HtmlDocument();
                document.Load(html);
                return IsItHtml(document.DocumentNode);
            }
            catch (Exception e)
            {
                //TODO change to logging using logging framework
                Logger.Log(e.Message);
                return false;
            }
        }
    }
}