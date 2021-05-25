using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorTecsup.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CalculatorTecsup
{
    public partial class MainPage : ContentPage
    {
        int currentState = 1;
        string mathOperator;
        double firstNumber, secondNumber;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.ViewModelCalculator();
            OnClear(this, null);
        }

        string result;
        public string Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged();
                }
            }
        }

        void SelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            if (Result == "0" || currentState < 0)
            {
                Result = "";
                if (currentState < 0)
                    currentState *= -1;
            }

            Result += pressed;

            double number;
            if (double.TryParse(Result, out number))
            {
                Result = number.ToString("N0");
                if (currentState == 1)
                {
                    firstNumber = number;
                }
                else
                {
                    secondNumber = number;
                }
            }
        }

        void OnSelectOperator(object sender, EventArgs e)
        {
            currentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            mathOperator = pressed;
        }

        void OnClear(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = 1;
            Result = "0";
        }

        void OnCalculate(object sender, EventArgs e)
        {
            if (currentState == 2)
            {
                var result = SimpleCalculator.Calculate(firstNumber, secondNumber, mathOperator);

                Result = result.ToString();
                firstNumber = result;
                currentState = -1;
            }
        }
    }
}
