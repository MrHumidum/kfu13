namespace kfu13;
public partial class DetailsForm : Form
{
    public DetailsForm(string title, Dictionary<string, string> details)
    {
        InitializeComponent();
        this.Text = title;
        foreach (var item in details)
        {
            listBox1.Items.Add($"{item.Key}: {item.Value}");
        }
    }
}
