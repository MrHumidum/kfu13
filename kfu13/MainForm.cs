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

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ShowDetails();
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowDetails();
        }

        private void ShowDetails()
        {
            var node = treeView1.SelectedNode;
            if (node?.Tag == null)
            {
                return;
            }
            string title;
            Dictionary<string, string> details = new Dictionary<string, string>();

            switch (node.Tag)
            {
                case Subscriber sub:
                    {
                    title = "Subscriber Details";
                    details["Name"] = sub.Name;
                    details["Phone Number"] = sub.PhoneNumber;
                    details["Tariff"] = sub.Tariff;
                    details["Balance"] = sub.Balance.ToString();
                        break;
                    }

                case Tariff tariff:
                    {
                    title = "Tariff Details";
                    details["Tariff Name"] = tariff.TariffName;
                    details["Monthly Fee"] = tariff.MonthlyFee.ToString();
                    details["Mobile Minutes"] = tariff.MinutesMobile.ToString();
                    details["Landline Minutes"] = tariff.MinutesLandline.ToString();
                    details["Internet Daytime"] = tariff.InternetDaytime.ToString();
                    details["Internet Nighttime"] = tariff.InternetNighttime.ToString();
                    details["Extra Services"] = tariff.ExtraServices;
                        break;
                    }

                case Service svc:
                    {
                    title = "Service Details";
                    details["Service Name"] = svc.ServiceName;
                    details["Description"] = svc.Description;
                    details["Price"] = svc.Price.ToString();
                    details["Communication Type"] = svc.Communication;
                    details["Roaming Available"] = svc.Roaming.ToString();
                    details["Available in Russia"] = svc.Russia.ToString();
                        break;
                    }

                default:
                    return;
            }

            using var form = new DetailsForm(title, details);
            form.ShowDialog();
        }

        private void buttonLoadJson_Click(object sender, EventArgs e)
        {
            if (SelectFile("JSON files (*.json)|*.json", out var file))
            { 
                LoadJson(file); 
            }
        }

        private void buttonLoadXml_Click(object sender, EventArgs e)
        {
            if (SelectFile("XML files (*.xml)|*.xml", out var file))
            {
                LoadXml(file);
            }
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
                var content = File.ReadAllText(path);
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
                var xdocument = XDocument.Load(path);
                ClearViews();
                PopulateTreeFromXml(xdocument);
                PopulateGridFromXml(xdocument);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке XML: {ex.Message}");
            }
        }

        private void ClearViews()
        {
            treeView1.Nodes.Clear();
            dataGridView1.DataSource = null;
        }

        private void PopulateTreeFromJson(JObject json)
        {
            treeView1.Nodes.Clear();
            var root = new TreeNode("Оператор мобильной связи");

            var subsNode = new TreeNode("Абоненты");
            foreach (var sub in json["MobileOperator"]["Subscribers"] ?? new JArray())
            {
                var name = (string)sub["Name"];
                var phone = (string)sub["PhoneNumber"];
                var tariff = (string)sub["TariffPlan"]["Name"];
                var balance = decimal.TryParse((string)sub["Balance"], out var _balance) ? _balance : 0;
                var subscriber = new Subscriber { Name = name, PhoneNumber = phone, Tariff = tariff, Balance = balance };
                subsNode.Nodes.Add(new TreeNode(name) { Tag = subscriber });
            }
            root.Nodes.Add(subsNode);

            var tariffNode = new TreeNode("Тарифы");
            foreach (var tariff in json["MobileOperator"]["Tariffs"] ?? new JArray())
            {
                var name = (string)tariff["TariffName"];
                var fee = decimal.TryParse((string)tariff["MonthlyFee"], out var mf) ? mf : 0;
                var minMobile = int.TryParse((string)tariff["Minutes"]["Mobile"], out var mm) ? mm : 0;
                var minLand = int.TryParse((string)tariff["Minutes"]["Landline"], out var ml) ? ml : 0;
                var day = int.TryParse((string)tariff["Internet"]["Daytime"], out var id) ? id : 0;
                var night = int.TryParse((string)tariff["Internet"]["Nighttime"], out var inight) ? inight : 0;
                var extra = (string)tariff["ExtraServices"];
                var tariffObj = new Tariff
                {
                    TariffName = name,
                    MonthlyFee = fee,
                    MinutesMobile = minMobile,
                    MinutesLandline = minLand,
                    InternetDaytime = day,
                    InternetNighttime = night,
                    ExtraServices = extra
                };
                tariffNode.Nodes.Add(new TreeNode(name) { Tag = tariffObj });
            }
            root.Nodes.Add(tariffNode);

            var servicesNode = new TreeNode("Услуги");
            foreach (var s in json["MobileOperator"]["Services"] ?? new JArray())
            {
                var name = (string)s["ServiceName"];
                var desc = (string)s["Description"];
                var price = decimal.TryParse((string)s["Price"], out var p) ? p : 0;
                var comm = (string)s["ServiceType"]["Communication"];
                var roam = bool.TryParse((string)s["Availability"]["Roaming"], out var r) && r;
                var rus = bool.TryParse((string)s["Availability"]["Russia"], out var ru) && ru;
                var svcObj = new Service
                {
                    ServiceName = name,
                    Description = desc,
                    Price = price,
                    Communication = comm,
                    Roaming = roam,
                    Russia = rus
                };
                servicesNode.Nodes.Add(new TreeNode(name) { Tag = svcObj });
            }
            root.Nodes.Add(servicesNode);

            treeView1.Nodes.Add(root);
            root.ExpandAll();
        }

        private void PopulateGridFromJson(JObject json)
        {
            var subs = json["MobileOperator"]["Subscribers"] ?? new JArray();
            dataGridView1.DataSource = subs.Select(sub => new
            {
                Имя = (string)sub["Name"],
                Телефон = (string)sub["PhoneNumber"],
                Тариф = (string)sub["TariffPlan"]["Name"],
                Баланс = (string)sub["Balance"],
                Город = (string)sub["Address"]["City"]
            }).ToList();
        }

        private void PopulateTreeFromXml(XDocument xdoc)
        {
            treeView1.Nodes.Clear();
            var root = new TreeNode("Оператор мобильной связи");

            var subsNode = new TreeNode("Абоненты");
            foreach (var sub in xdoc.Root?.Element("Subscribers")?.Elements("Subscriber") ?? Enumerable.Empty<XElement>())
            {
                var name = (string)sub.Element("Name");
                var phone = (string)sub.Element("PhoneNumber");
                var tariff = (string)sub.Element("TariffPlan")?.Element("Name");
                var balance = decimal.TryParse((string)sub.Element("Balance"), out var b) ? b : 0;
                var subscriber = new Subscriber { Name = name, PhoneNumber = phone, Tariff = tariff, Balance = balance };
                subsNode.Nodes.Add(new TreeNode(name) { Tag = subscriber });
            }
            root.Nodes.Add(subsNode);

            var tariffNode = new TreeNode("Тарифы");
            foreach (var tariff in xdoc.Root?.Element("Tariffs")?.Elements("Tariff") ?? Enumerable.Empty<XElement>())
            {
                var name = (string)tariff.Element("TariffName");
                var fee = decimal.TryParse((string)tariff.Element("MonthlyFee"), out var mf) ? mf : 0;
                var mm = int.TryParse((string)tariff.Element("Minutes")?.Element("Mobile"), out var mmo) ? mmo : 0;
                var ml = int.TryParse((string)tariff.Element("Minutes")?.Element("Landline"), out var mlo) ? mlo : 0;
                var day = int.TryParse((string)tariff.Element("Internet")?.Element("Daytime"), out var id) ? id : 0;
                var night = int.TryParse((string)tariff.Element("Internet")?.Element("Nighttime"), out var inight) ? inight : 0;
                var extra = (string)tariff.Element("ExtraServices");
                var tariffObj = new Tariff
                {
                    TariffName = name,
                    MonthlyFee = fee,
                    MinutesMobile = mm,
                    MinutesLandline = ml,
                    InternetDaytime = day,
                    InternetNighttime = night,
                    ExtraServices = extra
                };
                tariffNode.Nodes.Add(new TreeNode(name) { Tag = tariffObj });
            }
            root.Nodes.Add(tariffNode);

            var servicesNode = new TreeNode("Услуги");
            foreach (var s in xdoc.Root?.Element("Services")?.Elements("Service") ?? Enumerable.Empty<XElement>())
            {
                var name = (string)s.Element("ServiceName");
                var desc = (string)s.Element("Description");
                var price = decimal.TryParse((string)s.Element("Price"), out var p) ? p : 0;
                var comm = (string)s.Element("ServiceType")?.Element("Communication");
                var roam = bool.TryParse((string)s.Element("Availability")?.Element("Roaming"), out var r) && r;
                var rus = bool.TryParse((string)s.Element("Availability")?.Element("Russia"), out var ru) && ru;
                var svcObj = new Service
                {
                    ServiceName = name,
                    Description = desc,
                    Price = price,
                    Communication = comm,
                    Roaming = roam,
                    Russia = rus
                };
                servicesNode.Nodes.Add(new TreeNode(name) { Tag = svcObj });
            }
            root.Nodes.Add(servicesNode);

            treeView1.Nodes.Add(root);
            root.ExpandAll();
        }

        private void PopulateGridFromXml(XDocument xdoc)
        {
            dataGridView1.DataSource = xdoc.Root?
                .Element("Subscribers")?
                .Elements("Subscriber")
                .Select(sub => new
                {
                    Имя = (string)sub.Element("Name"),
                    Телефон = (string)sub.Element("PhoneNumber"),
                    Тариф = (string)sub.Element("TariffPlan")?.Element("Name"),
                    Баланс = (string)sub.Element("Balance"),
                    Город = (string)sub.Element("Address")?.Element("City")
                }).ToList();
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
                using var mobileOperatorContext = new MobileOperatorContext();
                ClearDatabase(mobileOperatorContext);
                InsertSubscribers(mobileOperatorContext);
                InsertTariffs(mobileOperatorContext);
                InsertServices(mobileOperatorContext);
                mobileOperatorContext.SaveChanges();
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
                ClearViews();
                using var MobileOperatorContext = new MobileOperatorContext();

                var subs = MobileOperatorContext.Subscribers.ToList();
                var tariffs = MobileOperatorContext.Tariffs.ToList();
                var services = MobileOperatorContext.Services.ToList();

                var root = new TreeNode("Оператор мобильной связи");
                var subsNode = new TreeNode("Абоненты");
                foreach (var sub in subs)
                    subsNode.Nodes.Add(new TreeNode(sub.Name) { Tag = sub });
                root.Nodes.Add(subsNode);

                var tariffNode = new TreeNode("Тарифы");
                foreach (var t in tariffs)
                    tariffNode.Nodes.Add(new TreeNode(t.TariffName) { Tag = t });
                root.Nodes.Add(tariffNode);

                var servicesNode = new TreeNode("Услуги");
                foreach (var s in services)
                    servicesNode.Nodes.Add(new TreeNode(s.ServiceName) { Tag = s });
                root.Nodes.Add(servicesNode);

                treeView1.Nodes.Add(root);
                root.ExpandAll();

                var items = new List<DbItem>();
                foreach (var sub in subs)
                    items.Add(new DbItem { Type = "Subscriber", Name = sub.Name, Value = sub.PhoneNumber });
                dataGridView1.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке из БД: {ex.Message}");
            }
        }

        private void ClearDatabase(MobileOperatorContext MobileOperatorContext)
        {
            MobileOperatorContext.Subscribers.RemoveRange(MobileOperatorContext.Subscribers);
            MobileOperatorContext.Tariffs.RemoveRange(MobileOperatorContext.Tariffs);
            MobileOperatorContext.Services.RemoveRange(MobileOperatorContext.Services);
            MobileOperatorContext.SaveChanges();
        }

        private void InsertSubscribers(MobileOperatorContext MobileOperatorContext)
        {
            foreach (var sub in mobileData["MobileOperator"]["Subscribers"])
            {
                MobileOperatorContext.Subscribers.Add(new Subscriber
                {
                    Name = (string)sub["Name"],
                    PhoneNumber = (string)sub["PhoneNumber"],
                    Tariff = (string)sub["TariffPlan"]["Name"],
                    Balance = (decimal)sub["Balance"]
                });
            }
        }

        private void InsertTariffs(MobileOperatorContext MobileOperatorContext)
        {
            foreach (var tariff in mobileData["MobileOperator"]["Tariffs"])
            {
                var fee = decimal.TryParse((string)tariff["MonthlyFee"], out var mf) ? mf : 0;

                var mobileMin = int.TryParse((string)tariff["Minutes"]["Mobile"], out var mm) ? mm : 0;
                var landMin = int.TryParse((string)tariff["Minutes"]["Landline"], out var ll) ? ll : 0;

                var dayInternet = int.TryParse((string)tariff["Internet"]["Daytime"], out var di) ? di : 0;
                var nightInternet = int.TryParse((string)tariff["Internet"]["Nighttime"], out var ni) ? ni : 0;

                var extra = (string)tariff["ExtraServices"];

                MobileOperatorContext.Tariffs.Add(new Tariff
                {
                    TariffName = (string)tariff["TariffName"],
                    MonthlyFee = fee,
                    MinutesMobile = mobileMin,
                    MinutesLandline = landMin,
                    InternetDaytime = dayInternet,
                    InternetNighttime = nightInternet,
                    ExtraServices = extra
                });
            }
        }

        private void InsertServices(MobileOperatorContext MobileOperatorContext)
        {
            foreach (var service in mobileData["MobileOperator"]["Services"])
            {
                var price = decimal.TryParse((string)service["Price"], out var p) ? p : 0;
                var roam = bool.TryParse((string)service["Availability"]["Roaming"], out var r) && r;
                var rus = bool.TryParse((string)service["Availability"]["Russia"], out var ru) && ru;

                MobileOperatorContext.Services.Add(new Service
                {
                    ServiceName = (string)service["ServiceName"],
                    Description = (string)service["Description"],
                    Price = price,
                    Communication = (string)service["ServiceType"]["Communication"],
                    Roaming = roam,
                    Russia = rus
                });
            }
        }

    }
}
