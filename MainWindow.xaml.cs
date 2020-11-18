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
using System.Diagnostics;

//TODO: 
// divide by zero
// accept decimal input 
// clean up type casts 
// accept more than 2 numbers before equal'ing out 
namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Equation eq = new Equation();

        public MainWindow()
        {
            InitializeComponent();    
        }

        private void OnEqual(object sender, RoutedEventArgs e)
        {
            //get index of operation 
            string displayBox = display.Text;
            int opIndex = displayBox.IndexOf(eq.Operator);
            
            //sets eq.Number2 if empty 
            if (eq.Number2 != "")
            {
                eq.Number2 = displayBox.Substring(opIndex + 1, (displayBox.Length - opIndex - 1));
            }

            display.Text = Convert.ToString(eq.Operation(Convert.ToDouble(eq.Number1), Convert.ToDouble(eq.Number2), eq.Operator));
        }
        private void OnOperator(object sender, RoutedEventArgs e)
        {
            //gets operation chosen 
            Button button = (Button)sender;
            display.Text += button.Content.ToString();
            eq.Operator = button.Content.ToString();

            //gets display info 
            string displayInfo = display.Text;
            int opIndex = displayInfo.IndexOf(eq.Operator);
            eq.Number1 = displayInfo.Substring(0, opIndex);

            if (button.Content.ToString() == "+/-")
            {
                display.Text = Convert.ToString(eq.Negate(Convert.ToDouble(eq.Number1)));
            } 
            // if there's a num2, set it
            // this function is if there are multiple operators before the equals 
            // this needs major rework
            if (displayInfo.Length > displayInfo.IndexOf(eq.Operator)+1)
            {
                eq.Number2 = displayInfo.Substring(opIndex + 1, (displayInfo.Length - opIndex - 1));
            }
        }         
        //used to display numbers only 
        private void OnNum(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            display.Text += button.Content.ToString();
        }
        //used to clear 
        private void OnClear(object sender, RoutedEventArgs e)
        {
            display.Text = "";
            eq.Number1 = "";
            eq.Number2 = "";
            eq.Operator = "";
        }

    }
    class Equation
    {
        public string Number1
        { get; set; }
        public string Number2
        { get; set; }
        public string Operator
        { get; set; }
        public double Negate(double num)
        {
            return num * -1;
        }

        public double Operation(double num1, double num2, string op)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "/":                      
                    return num1 / num2;
                case "*":
                    return num1 * num2;
                case "^":
                    return Math.Pow(num1, num2);
                    // needs to be a different value 
                default:
                    return -0;
            }
        }
    }

}
