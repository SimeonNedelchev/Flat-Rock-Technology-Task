using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace WebScraper
{
    class Program
    {
        private static void Main(String[] args)
        {
            SaveHtml();

            string path = @"test.html";

            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);

            HtmlNodeCollection names = doc.DocumentNode.SelectNodes("//a/img");
            HtmlNodeCollection prices = doc.DocumentNode.SelectNodes("//span[@class='price-display formatted']");
            HtmlNodeCollection ratings = doc.DocumentNode.SelectNodes("//div[@class='item']");

            string pattern = "\\$[0-9]*.*.[0-9]*\\$";
            Console.WriteLine("[");
            for (int i = 0; i <names.Count; i++)
            {
                Console.WriteLine("\t {");
                Console.WriteLine("\t \"productName\": " +"\"" + System.Web.HttpUtility.HtmlDecode(names[i].GetAttributeValue("alt", "")) + "\"");
                string newPrice = Regex.Replace(prices[i].InnerText, pattern, String.Empty);
                try
                {
                    Console.WriteLine("\t \"price\": " + "\"" + decimal.Parse(newPrice) + "\"");
               
                    if (float.Parse(ratings[i].GetAttributeValue("rating", "N/A")) <= 5)
                    {
                        Console.WriteLine("\t \"rating\": " + "\"" + ratings[i].GetAttributeValue("rating", "N/A") + "\"");
                    }
                    else
                    {
                        Console.WriteLine("\t \"rating\": " + "\"" + float.Parse(ratings[i].GetAttributeValue("rating", "N/A")) / 2 + "\"");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
                }
                Console.WriteLine("\t },");
            }
            Console.WriteLine("]");
        }

        private static void SaveHtml()
        {
            string html = @"
                <div class=""item"" rating=""3"" data-pdid=""5426"">
                    <figure><a href=""https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-Chest-Freezer""><img
                                alt=""Electrolux 700L Chest Freezer &amp; Filter"" src=""/productimages/thumb/1/5426_5731_4009.jpg""
                                data-alternate-image=""/productimages/thumb/2/5426_5731_4010.jpg"" class=""mouseover-set""><span
                                class=""overlay top-horizontal""><span class=""sold-out""><img alt=""Sold Out""
                                        Src=""/Images/Overlay/overlay_1_2_1.png""></span></span></a></figure>
                    <div class=""item-detail"">
                        <h4><a href=""https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-Chest-Freezer"">Electrolux 700L Chest Freezer</a></h4>
                        <div class=""pricing"" itemprop=""offers"" itemscope=""itemscope"" itemtype=""http://schema.org/Offer"">
                            <meta itemprop=""priceCurrency"" content=""NZD"">
                            <p class=""price""><span class=""price-display formatted"" itemprop=""price""><span
                                        style=""display: none"">$2,099.00</span>$<span class=""dollars over500"">2,099</span><span
                                        class=""cents zero"">.00</span></span></p>
                        </div>
                        <p class=""style-number"">WCM7000WD</p>
                        <p class=""offer""><a href=""https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-Chest-Freezer""><span
                                    style=""color:#CC0000;"">WCM7000WD</span></a></p>
                        <div class=""item-asset""><!--.--></div>
                    </div>
                </div>
                <div class=""item"" rating=""3.6"" data-pdid=""5862"">
                    <figure><a href=""https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-Vacuum-Bags""><img
                                alt=""Electrolux Anti-Odour Vacuum Bags"" src=""/productimages/thumb/1/5862_6182_4541.jpg""></a></figure>
                    <div class=""item-detail"">
                        <h4><a href=""https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-Vacuum-Bags"">Electrolux Anti-Odour Vacuum Bags</a></h4>
                        <div class=""pricing"" itemprop=""offers"" itemscope=""itemscope"" itemtype=""http://schema.org/Offer"">
                            <meta itemprop=""priceCurrency"" content=""NZD"">
                            <p class=""price""><span class=""price-display formatted"" itemprop=""price""><span
                                        style=""display: none"">$22.99</span>$<span class=""dollars"">22</span><span
                                        class=""cents"">.99</span></span></p>
                        </div>
                        <p class=""style-number"">E203S</p>
                        <p class=""offer""><a href=""https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-
                Vacuum-Bags""><span style=""color:#CC0000;"">E203S</span></a></p>
                        <div class=""item-asset""><!--.--></div>
                    </div>
                </div>
                <div class=""item"" rating=""8.4"" data-pdid=""4599"">
                    <figure><a href=""https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-Starter-Kit""><img
                                alt=""Electrolux UltraFlex Starter &#91; Kit &#93; "" src=""/productimages/thumb/1/4599_4843_2928.jpg""></a>
                    </figure>
                    <div class=""item-detail"">
                        <h4><a href=""https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-Starter-Kit"">Electrolux UltraFlex &#64; Starter Kit</a></h4>
                        <div class=""pricing"" itemprop=""offers"" itemscope=""itemscope"" itemtype=""http://schema.org/Offer"">
                            <meta itemprop=""priceCurrency"" content=""NZD"">
                            <p class=""price""><span class=""price-display formatted"" itemprop=""price""><span
                                        style=""display: none"">$44.99</span>$<span class=""dollars"">44</span><span
                                        class=""cents"">.99</span></span></p>
                        </div>
                        <p class=""style-number"">USK11ANZ</p>
                        <p class=""offer""><a href=""https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-Starter-Kit""><span
                                    style=""color:#CC0000;"">USK11ANZ</span></a></p>
                        <div class=""item-asset""><!--.--></div>
                    </div>
                </div>
              
                              ";

            HtmlDocument doc = new HtmlDocument(); 
            doc.LoadHtml(html);

            doc.Save("test.html");
        }
    }
}
