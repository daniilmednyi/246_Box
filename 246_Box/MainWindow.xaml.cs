using System.Collections.ObjectModel; // Пространство имен для ObservableCollection
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// Это главное окно приложения, которое содержит элементы управления для отображения и фильтрации книг
    /// </summary>
    public partial class MainWindow : Window
    {
        // Коллекция всех книг, доступных в приложении
        // ObservableCollection автоматически уведомляет UI об изменениях (добавление/удаление)
        ObservableCollection<Book> books = new ObservableCollection<Book>();

        // Коллекция для отображения отфильтрованных книг
        // Используется как источник данных для ListBox после применения фильтра по жанру
        ObservableCollection<Book> display = new ObservableCollection<Book>();

        // Коллекция доступных жанров для выпадающего списка (ComboBox)
        ObservableCollection<string> genres = new ObservableCollection<string>();

        public MainWindow()
        {
            // Инициализация компонентов окна (обязательный вызов для XAML)
            InitializeComponent();

            // Добавление книг в основную коллекцию 
            // Каждая книга содержит название, описание, жанр и цену
            books.Add(new Book { Title = "Книга 1", Description = "Описание 1", Genre = "Жанр 1", Price = 552 });
            books.Add(new Book { Title = "Книга 2", Description = "Описание 2", Genre = "Жанр 2", Price = 43 });
            books.Add(new Book { Title = "Книга 3", Description = "Описание 3", Genre = "Жанр 1", Price = 2 });
            books.Add(new Book { Title = "Книга 4", Description = "Описание 4", Genre = "Жанр 2", Price = 64 });
            books.Add(new Book { Title = "Книга 5", Description = "Описание 5", Genre = "Жанр 1", Price = 76 });
            books.Add(new Book { Title = "Книга 6", Description = "Описание 6", Genre = "Жанр 1", Price = 98 });

            // Установка источника данных для ListBox (список книг)
            // Изначально отображаются все книги
            listBook.ItemsSource = books;

            // Добавление жанров в выпадающий список
            // Жанр 3 добавлен, но не используется ни в одной книге (для демонстрации)
            genres.Add("Жанр 1");
            genres.Add("Жанр 2");
            genres.Add("Жанр 3");

            // Привязка коллекции жанров к ComboBox
            comboGenre.ItemsSource = genres;
        }

        /// <summary>
        /// Обработчик события выбора элемента в списке книг (ListBox)
        /// Срабатывает при клике на книгу в списке
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие (ListBox)</param>
        /// <param name="e">Аргументы события, содержащие информацию о выбранном элементе</param>
        private void listBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка, что выбранный элемент является объектом типа Book
            // Если да, то извлекаем его в переменную b
            if (listBook.SelectedItem is Book b)
            {
                // Отображение цены выбранной книги в текстовом поле txtPrice
                // ToString() преобразует числовое значение в строку
                txtPrice.Text = b.Price.ToString();

                // Отображение описания выбранной книги в текстовом поле txtDesc
                txtDesc.Text = b.Description.ToString();
            }
        }

        /// <summary>
        /// Обработчик события изменения выбранного жанра в ComboBox
        /// Срабатывает при выборе пользователем жанра из выпадающего списка
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие (ComboBox)</param>
        /// <param name="e">Аргументы события</param>
        private void comboGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка, что в ComboBox выбран какой-либо элемент (не null)
            if (comboGenre.SelectedItem != null)
            {
                // Получение выбранного жанра в виде строки
                // ToString() возвращает название жанра
                string g = comboGenre.SelectedItem.ToString();

                // Создание новой отфильтрованной коллекции книг
                // Используется LINQ-запрос Where для фильтрации книг по жанру
                // b.Genre == g - условие: жанр книги должен совпадать с выбранным жанром
                // Результат запроса преобразуется в ObservableCollection для отслеживания изменений
                display = new ObservableCollection<Book>(
                    books.Where(b => b.Genre == g)
                    );

                // Обновление источника данных ListBox
                // Теперь в списке отображаются только книги выбранного жанра
                listBook.ItemsSource = display;
            }
        }
    }
}