using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

using Lab5_threads.Model;

namespace Lab5_threads
{
    public partial class MainForm : Form
    {
        private List<Product> products;
        private bool isOpen;
        private CancellationTokenSource cancellationTokenSource;
        private string logFilePath = "log.xml";
        private decimal storeBalance;
        private XmlDocument xmlDoc;

        public MainForm()
        {
            InitializeComponent();
            products = new List<Product>();
            isOpen = false;
            storeBalance = 100;
            xmlDoc = new XmlDocument();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Завантаження початкових даних про товари в магазині
            products.Add(new Product("Product 1", 10, 5, 10));
            products.Add(new Product("Product 2", 15, 8, 12));
            products.Add(new Product("Product 3", 5, 12, 20));
            dataGridViewProducts.DataSource = products;
        }

        private async void btnToggleShop_Click(object sender, EventArgs e)
        {
            if (isOpen)
            {
                // Закриття магазину
                isOpen = false;
                btnToggleShop.Text = "Відкрити магазин";
                btnAddProducts.Enabled = true;
                cancellationTokenSource.Cancel();
                await Task.Delay(2000); // Надає час для завершення потоку покупців
                cancellationTokenSource.Dispose();

                // Поповнення кількості продуктів під час закриття магазину
                foreach (var product in products)
                {
                    product.Quantity += 10; // Наприклад, поповнюємо на 10 одиниць
                }
            }
            else
            {
                // Відкриття магазину
                isOpen = true;
                btnToggleShop.Text = "Закрити магазин";
                btnAddProducts.Enabled = false;
                cancellationTokenSource = new CancellationTokenSource();
                var token = cancellationTokenSource.Token;
                await Task.Run(() => RunShop(token, updatedProducts =>
                {
                    // Оновлення даних про товари в DataGridView
                    Invoke(new Action(() =>
                    {
                        dataGridViewProducts.DataSource = null;
                        dataGridViewProducts.DataSource = updatedProducts;

                        // Перевірка наявності товару та повідомлення користувача
                        foreach (DataGridViewRow row in dataGridViewProducts.Rows)
                        {
                            var product = (Product)row.DataBoundItem;
                            if (product.Quantity == 0)
                            {
                                row.DefaultCellStyle.BackColor = Color.Red;
                            }
                        }
                    }));
                }));
            }
        }
        private async void btnAddProducts_Click(object sender, EventArgs e)
        {
            await Task.Run(() => AddRandomProducts());
        }
        private Product GenerateRandomProduct()
        {
            var random = new Random();
            var productName = "Product " + random.Next(1, 100);
            var quantity = random.Next(10, 20);
            var buyingPrice = random.Next(5, 20);
            var sellingPrice = buyingPrice + random.Next(1, 10);

            return new Product(productName, quantity, buyingPrice, sellingPrice);
        }
        private void AddRandomProducts()
        {
            // Генерація випадкових нових товарів та додавання їх до списку продуктів магазину
            var random = new Random();
            var numNewProducts = random.Next(1, 4);
            decimal buyingCost = 0;

            for (int i = 0; i < numNewProducts; i++)
            {
                var newProduct = GenerateRandomProduct();
                products.Add(newProduct);

                // Обчислення вартості доданих товарів та віднімання з рахунку магазину
                buyingCost += newProduct.BuyingPrice * newProduct.Quantity;
            }

            storeBalance -= buyingCost;

            // Оновлення інтерфейсу в основному потоці
            BeginInvoke(new Action(() =>
            {
                labelStoreBalance.Text = $"Рахунок: {storeBalance} грн.";

                // Оновлення даних про товари в DataGridView
                dataGridViewProducts.DataSource = null;
                dataGridViewProducts.DataSource = products;

            }));
        }

        private async Task RunShop(CancellationToken cancellationToken, Action<List<Product>> updateDataGridView)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    // Генерація випадкової кількості покупців
                    var random = new Random();
                    var numCustomers = random.Next(1, 5);
                    var customers = new List<Customer>();

                    for (int i = 0; i < numCustomers; i++)
                    {
                        var customer = new Customer();
                        var productIndex = random.Next(0, products.Count);
                        var product = products[productIndex];

                        if (!product.IsAvailable)
                        {
                            // Товар закінчився, повідомлення користувача
                            string message = $"Извините, товар '{product.Name}' закончился.";
                            MessageBox.Show(message, "Товар закончился", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue; // Пропустити поточну ітерацію циклу
                        }

                        var quantity = random.Next(1, product.Quantity);
                        var totalCost = quantity * product.SellingPrice;

                        product.Quantity -= quantity;  // Видалення товару зі складу магазину

                        // Додавання покупки у логи
                        customer.PurchasedProducts.Add(new PurchasedProduct(product.Name, quantity, totalCost));
                        customers.Add(customer);

                        storeBalance += totalCost;  //Додавання суми покупки до рахунку магазину
                    }

                    // Оновлення інтерфейсу в основному потоці
                    BeginInvoke(new Action(() =>
                    {
                        // Виведення лога дій покупців
                        foreach (var customer in customers)
                        {
                            foreach (var purchasedProduct in customer.PurchasedProducts)
                            {
                                var logMessage = $"{customer.Id}: Покупка {purchasedProduct.Quantity} единиц товара '{purchasedProduct.ProductName}', стоимость: {purchasedProduct.TotalCost}";
                                listBoxLogs.Items.Add(logMessage);
                            }
                        }

                        // Відображення рахунку магазину
                        labelStoreBalance.Text = $"Рахунок магазину: {storeBalance} грн.";
                        updateDataGridView(products);
                    }));

                    // Запис логів у файл XML
                    WriteLogsToXml(customers);
                });

                // Пауза між ітераціями
                await Task.Delay(2000);
            }
        }

        private void WriteLogsToXml(List<Customer> customers)
        {
            // Створення кореневого елемента, якщо він не існує
            XmlNode root = xmlDoc.DocumentElement;
            if (root == null)
            {
                root = xmlDoc.CreateElement("Logs");
                xmlDoc.AppendChild(root);
            }

            foreach (var customer in customers)
            {
                // Створення елемента для кожного покупця
                var customerNode = xmlDoc.CreateElement("Customer");
                customerNode.SetAttribute("Id", customer.Id.ToString());
                root.AppendChild(customerNode);

                foreach (var purchasedProduct in customer.PurchasedProducts)
                {
                    // Створення елемента для кожної покупки
                    var purchaseNode = xmlDoc.CreateElement("Purchase");
                    purchaseNode.SetAttribute("ProductName", purchasedProduct.ProductName);
                    purchaseNode.SetAttribute("Quantity", purchasedProduct.Quantity.ToString());
                    purchaseNode.SetAttribute("TotalCost", purchasedProduct.TotalCost.ToString());
                    customerNode.AppendChild(purchaseNode);
                }
            }
            // Збереження файлу XML
            xmlDoc.Save(logFilePath);
        }
    }
}
