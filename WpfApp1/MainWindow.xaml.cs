using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            radioButton_add.IsChecked = true;
            comboBox1.Text = "DEC";
            checkBox1.IsChecked = true;
        }

        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void стираниеРезультата()
        {
            textBox_result.Text = "???";
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ",")
                e.Handled = true;
        }

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ",")
                e.Handled = true;
        }

        private void textBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void radioButton_add_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton_add.IsChecked == true) label_symbol.Content = "+";
            progressBar1.Value = 1;
            стираниеРезультата();
        }

        private void radioButton_sub_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton_sub.IsChecked == true) label_symbol.Content = "-";
            progressBar1.Value = 2;
            стираниеРезультата();
        }

        private void radioButton_mult_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton_mult.IsChecked == true) label_symbol.Content = "*";
            progressBar1.Value = 3;
            стираниеРезультата();
        }

        private void radioButton_div_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton_div.IsChecked == true) label_symbol.Content = "/";
            progressBar1.Value = 4;
            стираниеРезультата();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox1.Text == "DEC") checkBox1.IsEnabled = true;
            else checkBox1.IsEnabled = false;
            checkBox1.IsChecked = false;
            textBox_numericUpDown1.Text = 3.ToString();
            стираниеРезультата();
        }

        private void button_Розрахунок_Click(object sender, RoutedEventArgs e)
        {
            double x1 = Convert.ToDouble(textBox1.Text);
            double x2 = Double.Parse(textBox2.Text);
            string symbol = label_symbol.Content.ToString();
            double y;
            switch (symbol)
            {
                case "+":
                    y = x1 + x2;
                    break;
                case "-":
                    y = x1 - x2;
                    break;
                case "*":
                    y = x1 * x2;
                    break;
                case "/":
                    y = x1 / x2;
                    break;

                default:
                    y = 0;
                    break;
            }

            int precision = Convert.ToInt32(textBox_numericUpDown1.Text);
            y = Math.Round(y, precision);

            symbol = Convert.ToString(comboBox1.Text);
            string result;
            switch (symbol)
            {
                case "HEX":
                    result = Convert.ToString((int)y, 16);
                    result = result.ToUpper();
                    break;
                case "DEC":
                    result = Convert.ToString(y);
                    break;
                case "OCT":
                    result = Convert.ToString((int)y, 8);
                    break;
                case "BIN":
                    result = Convert.ToString((int)y, 2);
                    break;
                default:
                    MessageBox.Show("Така система числення не запрограмована", "Увага", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBox_result.Text = "???";
                    return;
                    //break;
            }

            textBox_result.Text = result;

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            WrapPanel_numericUpDown.IsEnabled = true;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            WrapPanel_numericUpDown.IsEnabled = false;
        }

        private void button_Increase1_Click(object sender, RoutedEventArgs e)
        {
            сhange_numericUpDown(+1);
        }

        private void button_Decrease1_Click(object sender, RoutedEventArgs e)
        {
            сhange_numericUpDown(-1);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();    //Clear if any old value is there in Clipboard        
            Clipboard.SetText(textBox_result.Text); //Copy text to Clipboard
            string strClip = Clipboard.GetText(); //Get text from Clipboard

            MessageBox.Show("Результат \n" + strClip + "\n скопійовано \n\n тепер можете нажати Ctrl+V \n і вставити текст кудись", "Копія у буфер обміну", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        private void сhange_numericUpDown(sbyte crease)
        {
            string str = textBox_numericUpDown1.Text;
            int a = Convert.ToInt32(str);
            a += crease;
            if (a >= 0 & a <= 9)
                textBox_numericUpDown1.Text = a.ToString();
        }
    }
}
