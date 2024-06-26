using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace MensaEssen
{
    public partial class MensaEssen : Form
    {
        public MensaEssen()
        {
            InitializeComponent();
            this.Load += new EventHandler(MensaEssen_Load);
        }

        private async void MensaEssen_Load(object? sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["MensaUrl"];
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Die URL wurde nicht in der Konfigurationsdatei gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            linkLabel.Text = url;

            string content = await GetContentFromUrl(url);
            PopulateDataGridView(content);
        }

        private void LinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = ConfigurationManager.AppSettings["MensaUrl"];
            if (!string.IsNullOrEmpty(url))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }

        private async Task<string> GetContentFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return "Error: Unable to retrieve content.";
                }
            }
        }

        private void PopulateDataGridView(string html)
        {
            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            var menuEntryDivs = document.DocumentNode.SelectNodes("//div[@class='day-menu active']//div[contains(@class, 'menu-entry_main-row')]");
            if (menuEntryDivs == null)
            {
                MessageBox.Show("No menu entries found for today.");
                return;
            }

            dishesDataGridView.Columns.Clear();
            dishesDataGridView.Columns.Add("DishType", "Typ");
            dishesDataGridView.Columns.Add("DishName", "Gerichte");
            dishesDataGridView.Columns.Add("StudentPrice", "Studis");
            dishesDataGridView.Columns.Add("ServantPrice", "Bedienstete");
            dishesDataGridView.Columns.Add("GuestPrice", "Gäste");

            dishesDataGridView.Columns["DishName"].Width = 300;
            dishesDataGridView.Columns["DishName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dishesDataGridView.Rows.Clear();

            foreach (var entry in menuEntryDivs)
            {
                var typeNode = entry.SelectSingleNode(".//div[contains(@class, 'food-type')]/span");
                var recipeNameNode = entry.SelectSingleNode(".//h5");
                var priceNode = entry.SelectSingleNode(".//div[@class='price']");

                if (typeNode != null && recipeNameNode != null && priceNode != null)
                {
                    string dishType = typeNode.Attributes["data-type-title"]?.Value ?? "N/A";
                    string recipeName = recipeNameNode.InnerText.Trim();
                    string priceStudent = priceNode.Attributes["data-price-student"]?.Value ?? "N/A";
                    string priceServant = priceNode.Attributes["data-price-servant"]?.Value ?? "N/A";
                    string priceGuest = priceNode.Attributes["data-price-guest"]?.Value ?? "N/A";

                    dishesDataGridView.Rows.Add(dishType, recipeName, priceStudent, priceServant, priceGuest);
                }
            }

            dishesDataGridView.AllowUserToAddRows = false;
        }
    }
}