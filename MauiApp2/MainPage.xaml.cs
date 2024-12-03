using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new TodoListViewModel();
        }
    }

    public class TodoItem : INotifyPropertyChanged
    {
        private bool _isDone;


        public string Name {get; set;}

        public bool IsDone
        {
            get => _isDone;
            set
            {
                if (SetField(ref _isDone, value))
                {
                    
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

       public class TodoListViewModel : INotifyPropertyChanged
    {
        private string _newTaskText;
        public TodoListViewModel()
        {

            AddCommand = new Command<string>(x =>
                {
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        Items.Add(new TodoItem { Name = x });
                        NewTaskText = string.Empty;
                    }
                },  
                x => !string.IsNullOrWhiteSpace(x)
                );
         

            RemoveCommand = new Command<TodoItem>(item =>
            {
                if (Items.Contains(item))
                {
                    Items.Remove(item);
                }
            });
        }

        public string NewTaskText
        {
            get => _newTaskText;
            set => SetField(ref _newTaskText, value);
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ObservableCollection<TodoItem> Items { get; } = new ObservableCollection<TodoItem>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


    }
}
