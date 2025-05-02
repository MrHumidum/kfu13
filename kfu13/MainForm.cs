using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace kfu13
{
    public partial class MainForm : Form
    {
        private JObject mobileData;

        public MainForm()
        {
            InitializeComponent();

            
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadJson("mobile_operator.json");
        }

        private void buttonLoadJson_Click(object sender, EventArgs e)
        {
            if (SelectFile("JSON files (*.json)|*.json", out var file))
                LoadJson(file);
        }

        private void buttonLoadXml_Click(object sender, EventArgs e)
        {
            if (SelectFile("XML files (*.xml)|*.xml", out var file))
                LoadXml(file);
        }

        private void buttonLoadToDb_Click(object sender, EventArgs e)
        {
            LoadToDatabase();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            ShowSubscribersFromDb();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool SelectFile(string filter, out string filePath)
        {
            openFileDialog.Filter = filter;
            openFileDialog.Title = "Выберите файл";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                return true;
            }
            filePath = null;
            return false;
        }

        private void LoadJson(string path)
        {
            try
            {
                string content = File.ReadAllText(path);
                var json = JObject.Parse(content);
                mobileData = json;

                ClearViews();
                PopulateTreeFromJson(json);
                PopulateGridFromJson(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке JSON: {ex.Message}");
            }
        }

        private void LoadXml(string path)
        {
            try
            {
                var xdoc = XDocument.Load(path);
                ClearViews();
                PopulateTreeFromXml(xdoc);
                PopulateGridFromXml(xdoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке XML: {ex.Message}");
            }
        }

        private void LoadToDatabase()
        {
            if (mobileData == null)
            {
                MessageBox.Show("Сначала загрузите JSON");
                return;
            }

            try
            {
                using var ctx = new MobileOperatorContext();
                ClearDatabase(ctx);
                InsertSubscribers(ctx);
                InsertTariffs(ctx);
                InsertServices(ctx);
                ctx.SaveChanges();
                MessageBox.Show("Данные успешно загружены в базу данных!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке в БД: {ex.Message}");
            }
        }

        private void ShowSubscribersFromDb()
        {
            try
            {
                var dbHelper = new DbHelper();
                dataGridView1.DataSource = dbHelper.LoadSubscribersFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке из БД: {ex.Message}");
            }
        }

        private void ClearViews()
        {
            treeView1.Nodes.Clear();
            dataGridView1.DataSource = null;
        }

        private void PopulateTreeFromJson(JObject json)
        {
            var root = new TreeNode("Оператор мобильной связи");
            root.Nodes.Add(CreateSubscribersNode(json));
            root.Nodes.Add(CreateTariffsNode(json));
            root.Nodes.Add(CreateServicesNode(json));
            treeView1.Nodes.Add(root);
            root.Expand();
        }

        private void PopulateGridFromJson(JObject json)
        {
            var subs = json["MobileOperator"]["Subscribers"] ?? new JArray();
            var list = subs.Select(sub => new
            {
                Имя = (string)sub["Name"],
                Телефон = (string)sub["PhoneNumber"],
                Тариф = (string)sub["TariffPlan"]["Name"],
                Баланс = (string)sub["Balance"],
                Город = (string)sub["Address"]["City"]
            }).ToList();
            dataGridView1.DataSource = list;
        }

        private void PopulateTreeFromXml(XDocument xdoc)
        {
            var root = new TreeNode("Оператор мобильной связи");
            root.Nodes.Add(CreateSubscribersNode(xdoc));
            root.Nodes.Add(CreateTariffsNode(xdoc));
            root.Nodes.Add(CreateServicesNode(xdoc));
            treeView1.Nodes.Add(root);
            root.Expand();
        }

        private void PopulateGridFromXml(XDocument xdoc)
        {
            var subscribers = xdoc.Root.Element("Subscribers").Elements("Subscriber");
            var list = subscribers.Select(sub => new
            {
                Имя = (string)sub.Element("Name"),
                Телефон = (string)sub.Element("PhoneNumber"),
                Тариф = (string)sub.Element("TariffPlan").Element("Name"),
                Баланс = (string)sub.Element("Balance"),
                Город = (string)sub.Element("Address").Element("City")
            }).ToList();
            dataGridView1.DataSource = list;
        }

        private void ClearDatabase(MobileOperatorContext ctx)
        {
            ctx.Subscribers.RemoveRange(ctx.Subscribers);
            ctx.Tariffs.RemoveRange(ctx.Tariffs);
            ctx.Services.RemoveRange(ctx.Services);
            ctx.SaveChanges();
        }

        private void InsertSubscribers(MobileOperatorContext ctx)
        {
            foreach (var sub in mobileData["MobileOperator"]["Subscribers"])
            {
                ctx.Subscribers.Add(new Subscriber
                {
                    Name = (string)sub["Name"],
                    PhoneNumber = (string)sub["PhoneNumber"],
                    Tariff = (string)sub["TariffPlan"]["Name"],
                    Balance = (decimal)sub["Balance"]
                });
            }
        }

        private void InsertTariffs(MobileOperatorContext ctx)
        {
            foreach (var t in mobileData["MobileOperator"]["Tariffs"])
            {
                ctx.Tariffs.Add(new Tariff
                {
                    TariffName = (string)t["TariffName"],
                    MonthlyFee = (decimal)t["MonthlyFee"],
                    MinutesMobile = (int)t["Minutes"]["Mobile"],
                    MinutesLandline = (int)t["Minutes"]["Landline"],
                    InternetDaytime = (int)t["Internet"]["Daytime"],
                    InternetNighttime = (int)t["Internet"]["Nighttime"],
                    ExtraServices = (string)t["ExtraServices"]
                });
            }
        }

        private void InsertServices(MobileOperatorContext ctx)
        {
            foreach (var s in mobileData["MobileOperator"]["Services"])
            {
                ctx.Services.Add(new Service
                {
                    ServiceName = (string)s["ServiceName"],
                    Description = (string)s["Description"],
                    Price = (decimal)s["Price"],
                    Communication = (string)s["ServiceType"]["Communication"],
                    Roaming = (bool)s["Availability"]["Roaming"],
                    Russia = (bool)s["Availability"]["Russia"]
                });
            }
        }

        private TreeNode CreateSubscribersNode(JObject json)
        {
            var node = new TreeNode("Абоненты");
            foreach (var sub in json["MobileOperator"]["Subscribers"])
                node.Nodes.Add(new TreeNode((string)sub["Name"]));
            return node;
        }

        private TreeNode CreateTariffsNode(JObject json)
        {
            var node = new TreeNode("Тарифы");
            foreach (var t in json["MobileOperator"]["Tariffs"])
                node.Nodes.Add(new TreeNode((string)t["TariffName"]));
            return node;
        }

        private TreeNode CreateServicesNode(JObject json)
        {
            var node = new TreeNode("Услуги");
            foreach (var s in json["MobileOperator"]["Services"])
                node.Nodes.Add(new TreeNode((string)s["ServiceName"]));
            return node;
        }

        private TreeNode CreateSubscribersNode(XDocument xdoc)
        {
            var node = new TreeNode("Абоненты");
            foreach (var sub in xdoc.Root.Element("Subscribers").Elements("Subscriber"))
                node.Nodes.Add(new TreeNode((string)sub.Element("Name")));
            return node;
        }

        private TreeNode CreateTariffsNode(XDocument xdoc)
        {
            var node = new TreeNode("Тарифы");
            foreach (var t in xdoc.Root.Element("Tariffs").Elements("Tariff"))
                node.Nodes.Add(new TreeNode((string)t.Element("TariffName")));
            return node;
        }

        private TreeNode CreateServicesNode(XDocument xdoc)
        {
            var node = new TreeNode("Услуги");
            foreach (var s in xdoc.Root.Element("Services").Elements("Service"))
                node.Nodes.Add(new TreeNode((string)s.Element("ServiceName")));
            return node;
        }
    }
}
