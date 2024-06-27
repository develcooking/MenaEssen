using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
            if (string.IsNullOrEmpty(content) || content == "Error: Unable to retrieve content.")
            {
                MessageBox.Show("Die Webseite hat keinen Inhalt oder konnte nicht abgerufen werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return string.Empty;  // Geben Sie einen leeren String zurück, statt eines Fehlertexts
                    }
                }
                catch (Exception)
                {
                    return string.Empty;  // Geben Sie einen leeren String zurück, falls eine Ausnahme auftritt
                }
            }
        }

        private void PopulateDataGridView(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                MessageBox.Show("Es sind keine Menüeinträge verfügbar.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dishesDataGridView.Rows.Clear();
                return;
            }

            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            var menuEntryDivs = document.DocumentNode.SelectNodes("//div[@class='day-menu active']//div[contains(@class, 'menu-entry_main-row')]");
            if (menuEntryDivs == null)
            {
                MessageBox.Show("Keine Menüeinträge für heute gefunden.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dishesDataGridView.Rows.Clear();
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
                    string dishType = HttpUtility.HtmlDecode(typeNode.Attributes["data-type-title"]?.Value ?? "N/A");
                    string recipeName = HttpUtility.HtmlDecode(recipeNameNode.InnerText.Trim());
                    string priceStudent = HttpUtility.HtmlDecode(priceNode.Attributes["data-price-student"]?.Value ?? "N/A");
                    string priceServant = HttpUtility.HtmlDecode(priceNode.Attributes["data-price-servant"]?.Value ?? "N/A");
                    string priceGuest = HttpUtility.HtmlDecode(priceNode.Attributes["data-price-guest"]?.Value ?? "N/A");

                    dishesDataGridView.Rows.Add(dishType, recipeName, priceStudent, priceServant, priceGuest);
                }
            }

            dishesDataGridView.AllowUserToAddRows = false;
        }
    }
}