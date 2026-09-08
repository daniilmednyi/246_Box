using System.Collections.ObjectModel; 
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _246_Box
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();
        ObservableCollection<Product> filteredProduct = new ObservableCollection<Product>();
        ObservableCollection<Product> cart = new ObservableCollection<Product>();
        ObservableCollection<string> categories = new ObservableCollection<string>();

        public MainWindow()
        {

            InitializeComponent();

            // Добавление книг в основную коллекцию 
            // Каждая книга содержит название, описание, жанр и цену
            products.Add(new Product { Name = "Молоко", Quantity = 20, Category = "Молоко",});
            products.Add(new Product { Name = "Хлеб", Quantity = 10, Category = "Хлеб",});
            products.Add(new Product { Name = "Батон", Quantity = 4, Category = "Хлеб",});
            products.Add(new Product { Name = "Картошка", Quantity = 6, Category = "Овощи",});
            products.Add(new Product { Name = "Сыр", Quantity = 7, Category = "Молоко",});
            products.Add(new Product { Name = "Яблоки", Quantity = 8, Category = "Фрукты",});

            listProduct.ItemsSource = products;

            // Добавление жанров в выпадающий список
            // Жанр 3 добавлен, но не используется ни в одной книге (для демонстрации)
            categories.Add("Все");
            categories.Add("Хлеб");
            categories.Add("Молоко");
            categories.Add("Овощи");
            categories.Add("Фрукты");

            // Привязка коллекции жанров к ComboBox
            comboCategory.ItemsSource = categories;
            comboCategory.SelectedIndex = 0;

            filteredProduct= new ObservableCollection<Product>(products);
            listProduct.ItemsSource = filteredProduct;
            listCart.ItemsSource = cart;

            

        }
        private void listProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listProduct.SelectedItem is Product b)
            {

                txtName.Text = b.Name;
                txtQuantity.Text = b.Quantity.ToString();
                btnBuy.IsEnabled = true;
            }
        }

        private void comboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка, что в ComboBox выбран какой-либо элемент (не null)
            if (comboCategory.SelectedItem != null)
            {

                string g = comboCategory.SelectedItem.ToString();

               
                filteredProduct = new ObservableCollection<Product>(
                    products.Where(b => b.Category == g)
                    );

                listProduct.ItemsSource = filteredProduct;
            }
        }
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if(listProduct.SelectedItem is Product b)
            {
                if(b.Quantity > 0)
                {
                    b.Quantity --;

                    cart.Add(new Product { Name = b.Name, Quantity = 1 });
                    

                    listProduct.SelectedItem = null;
                    txtName.Text = b.Name;
                    txtQuantity.Text = b.Quantity.ToString();
                    btnBuy.IsEnabled = false;
                }
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if(cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
                    
            }
            string result = "Ваш заказ: \n";

            foreach (var item in cart)
            {
                result += item.Name +"\n";

            }
            result += "\nВсего: " + cart.Count + " товаров";

            MessageBox.Show(result,"Заказ оформлен");

            cart.Clear();

        }
        
    }
}