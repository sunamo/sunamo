static void GenerateAvailablePaymentsMethods()
        {
            var c = TF.ReadFile(@"D:\Documents\Visual Studio 2017\Projects\_tests\CompareTwoFiles\CompareTwoFiles\html\1.html");
            var hd = HtmlAgilityHelper.CreateHtmlDocument();
            hd.LoadHtml(c);

            var nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(hd.DocumentNode, true, "label", "for", "enabledPaymentMethodsId");

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (var item in nodes)
            {
                var table = HtmlAgilityHelper.FindAncestorParentNode(item, "table");
                var n = HtmlAssistant.GetValueOfAttribute("n", table);
                var it = item.InnerText.Trim();
                if (it.Contains("spořitelna"))
                {

                }
                var p = SH.Split(it, Environment.NewLine, " - ");
                if (p.Count != 1)
                {
                    CA.TrimEnd(p, AllChars.dash);
                    CA.Trim(p);
                    it = string.Empty;
                    for (i = 0; i < p.Count - 1; i++)
                    {
                        it += p[i] + " ";
                    }
                    if (it != string.Empty)
                    {
                        it += "- " + p[p.Count - 1];
                    }
                }

                if (it == string.Empty)
                {

                }

                DictionaryHelper.AddOrCreate<string, string>(dict, n, it);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in dict)
            {
                sb.AppendLine(item.Key);
                foreach (var item2 in item.Value)
                {
                    sb.AppendLine(item2);
                }
                sb.AppendLine();
            }

            Clipboard = sb.ToString();
        }