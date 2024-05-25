using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyManagers.Common.Tool
{
    /// <summary>
    /// Interaction logic for ComboboxSelected.xaml
    /// </summary>
    public partial class ComboboxSelected : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ComboboxSelected()
        {
            InitializeComponent();
             this.DataContext = this;
        }
        public class searchItem
        {
            public string Name { get; set; }
            public object Data { get; set; }
        }

        public bool IsDropDownOpenSelected
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set
            {
                SetValue(IsDropDownOpenProperty, value);
            }
        }
        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpenSelected", typeof(bool), typeof(SearchBar), new PropertyMetadata(false));

        public int CornerRadiusSelected
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadiusSelected", typeof(int), typeof(SearchBar));

        public IEnumerable<object> ItemsSourceSelected
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                Refresh();
                OnPropertyChanged();
            }
        }
        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSourceSelected", typeof(IEnumerable<object>), typeof(SearchBar), new PropertyMetadata(new List<object>()));


        private List<searchItem> _Test = new List<searchItem>();
        public List<searchItem> Test
        {
            get { return _Test; }
        }


        public string DisplayMemberPathSelected
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPathSelected", typeof(string), typeof(SearchBar));



        public List<String> FilterMemberPathsSelected
        {
            get { return (List<String>)GetValue(FilterMemberPathsProperty); }
            set { SetValue(FilterMemberPathsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for DisplayMemberPaths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterMemberPathsProperty =
            DependencyProperty.Register("FilterMemberPathsSelected", typeof(List<String>), typeof(SearchBar), new PropertyMetadata(new List<string>()));

        public Style ItemContainerStyleSelected
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ItemContainerStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyleSelected", typeof(Style), typeof(SearchBar));



        public String PlaceHolderSelected
        {
            get { return (String)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolderSelected", typeof(String), typeof(SearchBar));
        public String PlaceHolderForgroundSelected
        {
            get { return (String)GetValue(PlaceHolderForgroundProperty); }
            set { SetValue(PlaceHolderForgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderForgroundProperty =
            DependencyProperty.Register("PlaceHolderForgroundSelected", typeof(String), typeof(SearchBar));

        public object SelectedItemSelected
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItemSelected", typeof(object), typeof(SearchBar));



        public String TextSelected
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("TextSelected", typeof(String), typeof(SearchBar));



        public int SelectedIndexSelected
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndexSelected", typeof(int), typeof(SearchBar), new PropertyMetadata(-1));




        public SelectionChangedEventHandler SelectionChangedSelected
        {
            get { return (SelectionChangedEventHandler)GetValue(SelectionChangedProperty); }
            set { SetValue(SelectionChangedProperty, value); }
        }
        // Using a DependencyProperty as the backing store for SelectionChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionChangedProperty =
            DependencyProperty.Register("SelectionChangedSelected", typeof(SelectionChangedEventHandler), typeof(SearchBar));



        public char DisplayCharSelected
        {
            get { return (char)GetValue(DisplayCharProperty); }
            set { SetValue(DisplayCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayCharProperty =
            DependencyProperty.Register("DisplayCharSelected", typeof(char), typeof(SearchBar));

        public Action ScrollToEndSelected
        {
            get { return (Action)GetValue(ScrollToEndProperty); }
            set { SetValue(ScrollToEndProperty, value); }
        }
        public static readonly DependencyProperty ScrollToEndProperty =
            DependencyProperty.Register("ScrollToEndSelected", typeof(Action), typeof(SearchBar));

        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; OnPropertyChanged(); }
        }

        private double _PopupMaxWith;

        public double PopupMaxWith
        {
            get { return _PopupMaxWith; }
        }

        private Type itemType;
        public string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",
            };
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e","e","e","e","e","e","e","e","e","e","e",
                "i","i","i","i","i",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                "u","u","u","u","u","u","u","u","u","u","u",
                "y","y","y","y","y",
            };
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public void Refresh()
        {
            if (ItemsSourceSelected != null)
            {
                itemType = ItemsSourceSelected.GetType().GetGenericArguments().Single();
                _Test = new List<searchItem>();
                if (itemType == typeof(string))
                {
                    ItemsSourceSelected.ToList().ForEach(i =>
                    {
                        _Test.Add(new searchItem() { Name = (i as string), Data = ItemsSourceSelected.ToList()[ItemsSourceSelected.ToList().IndexOf(i)] });
                    });
                    OnPropertyChanged("Test");
                    return;
                }
                if (!string.IsNullOrEmpty(DisplayMemberPathSelected))
                {
                    var list = new List<string>();
                    if (DisplayMemberPathSelected.Contains(","))
                    {
                        var dis = DisplayMemberPathSelected.Split(',').ToList();
                        ItemsSourceSelected.ToList().ForEach(item =>
                        {
                            var txt = new List<string>();
                            foreach (var p in item.GetType().GetProperties())
                            {
                                if (dis.Contains(p.Name))
                                {
                                    txt.Add(p.GetValue(item, null).ToString());
                                }
                            }
                            if (!string.IsNullOrEmpty(DisplayCharSelected.ToString())) list.Add(string.Join(" " + DisplayCharSelected.ToString() + " ", txt));
                            else list.Add(string.Join(" ", txt));
                        });
                    }
                    else
                    {
                        ItemsSourceSelected.ToList().ForEach(item =>
                        {
                            var txt = new List<string>();
                            foreach (var p in item.GetType().GetProperties())
                            {
                                if (p.Name == DisplayMemberPathSelected)
                                {
                                    txt.Add(p.GetValue(item, null).ToString());
                                }
                            }
                            list.Add(string.Join(" ", txt));
                        });
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        _Test.Add(new searchItem() { Name = list[i], Data = ItemsSourceSelected.ToList()[i] });
                    }
                    OnPropertyChanged("Test");
                }
                else
                {
                    var list = new List<string>();
                    ItemsSourceSelected.ToList().ForEach(i =>
                    {
                        list.Add(itemType.FullName);
                    });
                    for (int i = 0; i < list.Count; i++)
                    {
                        _Test.Add(new searchItem() { Name = list[i], Data = ItemsSourceSelected.ToList()[i] });
                    }
                    OnPropertyChanged("Test");
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var textbox = sender as TextBox;
            //_Test = new List<searchItem>();
            //if (itemType == typeof(string))
            //{
            //    var list = new List<string>();
            //    ItemsSource.ToList().ForEach(i =>
            //    {
            //        var txt = i as string;
            //        if (RemoveUnicode(txt).ToLower().Contains(RemoveUnicode(textbox.Text).ToLower()))
            //        {
            //            _Test.Add(new searchItem() { Name = txt, Data = ItemsSource.ToList()[ItemsSource.ToList().IndexOf(i)] });
            //        }
            //    });
            //    OnPropertyChanged("Test");
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(DisplayMemberPath))
            //    {
            //        var list = new List<string>();
            //        if (DisplayMemberPath.Contains(","))
            //        {
            //            var dis = DisplayMemberPath.Split(',').ToList();
            //            ItemsSource.ToList().ForEach(item =>
            //            {
            //                var txt = new List<string>();
            //                foreach (var p in item.GetType().GetProperties())
            //                {
            //                    if (dis.Contains(p.Name))
            //                    {
            //                        txt.Add(p.GetValue(item, null).ToString());
            //                    }
            //                }

            //                var temp = "";
            //                if (!string.IsNullOrEmpty(DisplayChar.ToString())) temp = (string.Join(" " + DisplayChar.ToString() + " ", txt));
            //                else temp = string.Join(" ", txt);
            //                if (RemoveUnicode(temp).ToLower().Contains(RemoveUnicode(textbox.Text.ToLower())))
            //                {
            //                    var i = ItemsSource.ToList().IndexOf(item);
            //                    _Test.Add(new searchItem() { Name = temp, Data = ItemsSource.ToList()[i] });
            //                }
            //            });
            //        }
            //        else
            //        {
            //            ItemsSource.ToList().ForEach(item =>
            //            {
            //                var txt = new List<string>();
            //                foreach (var p in item.GetType().GetProperties())
            //                {
            //                    if (p.Name == DisplayMemberPath)
            //                    {
            //                        txt.Add(p.GetValue(item, null).ToString());
            //                    }
            //                }
            //                var temp = string.Join(" ", txt);
            //                if (RemoveUnicode(temp).ToLower().Contains(RemoveUnicode(textbox.Text.ToLower())))
            //                {
            //                    var i = ItemsSource.ToList().IndexOf(item);
            //                    _Test.Add(new searchItem() { Name = temp, Data = ItemsSource.ToList()[i] });
            //                }
            //            });
            //        }
            //        OnPropertyChanged("Test");
            //    }
            //}

            //if (Test.Count <= 0)
            //{

            //}
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var z = (sender as ListView).SelectedItem as searchItem;
            SelectedItemSelected = null;
            TextSelected = "";
            if (z != null)
            {
                TextSelected = z.Name;
                SelectedItemSelected = z.Data;
            }
            if (SelectionChangedSelected != null) SelectionChangedSelected(this, e);
        }
        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (_Test.Count <= 0) Refresh();
            _PopupMaxWith = this.ActualWidth;
            foreach (var item in Test)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    var formattedText = new FormattedText(
                        item.Name,
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Regular, FontStretches.Normal)
                        , this.FontSize, Brushes.Black, new NumberSubstitution(), 1
                    );
                    if (formattedText.Width + 40 > _PopupMaxWith)
                    {
                        _PopupMaxWith = formattedText.Width + 40;
                        OnPropertyChanged("PopupMaxWith");
                    }
                }
            }
        }
        private void Popup_Opened(object sender, EventArgs e)
        {

        }
        private void SearchbarLoaded(object sender, RoutedEventArgs e)
        {
            var x = (sender as Border).Child as TextBox;
            x.Focus();
            x.SelectAll();
        }
        private void RemoveResult(object sender, MouseButtonEventArgs e)
        {
            SelectedItemSelected = null;
            SelectedIndexSelected = -1;
            SearchText = "";
            Refresh();
        }
        private void SearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (SelectedIndexSelected + 1 < Test.Count) SelectedIndexSelected++;
            }
            if (e.Key == Key.Up)
            {
                if (SelectedIndexSelected - 1 >= 0) SelectedIndexSelected--;
            }
        }
        private void lv_PreviewMouseLeftButtonDow(object sender, MouseButtonEventArgs e)
        {
            IsDropDownOpenSelected = false;
        }
        private T GetFirstChildOfType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject == null)
            {
                return null;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                var result = (child as T) ?? GetFirstChildOfType<T>(child);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
        private void lv_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var lv = sender as ListView;
            var scroll = GetFirstChildOfType<ScrollViewer>(lv);
            if (scroll != null)
            {
                if (scroll.ScrollableHeight > 0 && scroll.ScrollableHeight > scroll.VerticalOffset)
                {
                    if (ScrollToEndSelected != null) ScrollToEndSelected();
                }
            }
        }
    }
}
