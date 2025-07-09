using System;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

public class ApiTester : Form
{
    TextBox urlBox = new TextBox();
    ComboBox methodBox = new ComboBox();
    TextBox paramBox = new TextBox();
    TextBox resultBox = new TextBox();
    Button sendButton = new Button();

    public ApiTester()
    {
        this.Text = "API Test Tool - .NET Version";
        this.Size = new System.Drawing.Size(600, 500);

        System.Windows.Forms.Label urlLabel = new System.Windows.Forms.Label() { Text = "API URL:", Left = 10, Top = 10, Width = 80 };
        urlBox.Left = 100; urlBox.Top = 10; urlBox.Width = 480;

        System.Windows.Forms.Label methodLabel = new System.Windows.Forms.Label() { Text = "Method:", Left = 10, Top = 45, Width = 80 };
        methodBox.Left = 100; methodBox.Top = 45; methodBox.Width = 80;
        methodBox.Items.AddRange(new string[] { "GET", "POST" });
        methodBox.SelectedIndex = 0;

        System.Windows.Forms.Label paramLabel = new System.Windows.Forms.Label() { Text = "Request Params (JSON or Form):", Left = 10, Top = 80, Width = 220 };
        paramBox.Left = 10; paramBox.Top = 110; paramBox.Width = 570; paramBox.Height = 80; paramBox.Multiline = true;

        sendButton.Text = "Send Request"; sendButton.Left = 10; sendButton.Top = 200; sendButton.Width = 570;
        sendButton.Click += async (s, e) => await SendRequest();

        System.Windows.Forms.Label resultLabel = new System.Windows.Forms.Label() { Text = "Response:", Left = 10, Top = 240, Width = 100 };
        resultBox.Left = 10; resultBox.Top = 270; resultBox.Width = 570; resultBox.Height = 180; resultBox.Multiline = true; resultBox.ScrollBars = ScrollBars.Vertical;

        this.Controls.Add(urlLabel);
        this.Controls.Add(urlBox);
        this.Controls.Add(methodLabel);
        this.Controls.Add(methodBox);
        this.Controls.Add(paramLabel);
        this.Controls.Add(paramBox);
        this.Controls.Add(sendButton);
        this.Controls.Add(resultLabel);
        this.Controls.Add(resultBox);
    }

    private async Task SendRequest()
    {
        string url = urlBox.Text.Trim();
        string method = methodBox.SelectedItem.ToString();
        string param = paramBox.Text.Trim();

        resultBox.Text = "Requesting...";
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response;
                if (method == "GET")
                {
                    if (!string.IsNullOrEmpty(param))
                    {
                        url += url.Contains("?") ? "&" + param : "?" + param;
                    }
                    response = await client.GetAsync(url);
                }
                else // POST
                {
                    StringContent content = new StringContent(param, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, content);
                }
                string result = await response.Content.ReadAsStringAsync();
                resultBox.Text = FormatJson(result);
            }
        }
        catch (Exception ex)
        {
            resultBox.Text = "Request error: " + ex.Message;
        }
    }

    // Simple JSON formatter
    private string FormatJson(string json)
    {
        try
        {
            int indent = 0;
            StringBuilder sb = new StringBuilder();
            foreach (char c in json)
            {
                if (c == '{' || c == '[')
                {
                    sb.Append(c).Append('\n');
                    indent++;
                    sb.Append(new string(' ', indent * 2));
                }
                else if (c == '}' || c == ']')
                {
                    sb.Append('\n');
                    indent--;
                    sb.Append(new string(' ', indent * 2)).Append(c);
                }
                else if (c == ',')
                {
                    sb.Append(c).Append('\n').Append(new string(' ', indent * 2));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        catch
        {
            return json;
        }
    }
}