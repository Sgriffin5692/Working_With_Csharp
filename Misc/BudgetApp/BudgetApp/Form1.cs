using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void savingsButton_Click(object sender, EventArgs e)
        {
            bool incorrectValue = TextBoxValidate();

            if(incorrectValue)
            {
                MessageBox.Show("Enter a number into each text box!");
            }
            else
            {
                double estimatedSavings = double.Parse(balanceTextBox.Text) + CalculateIncome() - CalculateExpenses();
                savingsLabel.Text = $"Estimated Savings: {estimatedSavings:c2}";
            }
        }

        private void clearTextBox_Click(object sender, EventArgs e)
        {
            foreach (GroupBox groupBox in Controls.OfType<GroupBox>())
            {
                foreach (TextBox textbox in groupBox.Controls.OfType<TextBox>())
                {
                    textbox.Text = null;
                }
            }
            savingsLabel.Text = null;
        }

        private double CalculateIncome()
        {
            double totalIncome = double.Parse(mercyTextBox.Text) + double.Parse(giBillTextBox.Text)
                        + double.Parse(miscIncomeTextBox.Text) + double.Parse(vaTextBox.Text);
            return totalIncome;
        }

        private double CalculateExpenses()
        {
            double totalBills = double.Parse(rentTextBox.Text) + double.Parse(insuranceTextBox.Text)
                           + double.Parse(gasTextBox.Text) + double.Parse(phoneTextBox.Text)
                           + double.Parse(internetTextBox.Text) + double.Parse(creditTextBox.Text)
                           + double.Parse(huluTextBox.Text) + double.Parse(safeTextBox.Text)
                           + double.Parse(utlitiesTextBox.Text) + double.Parse(miscBillTextBox.Text)
                           + double.Parse(rothTextBox.Text) + double.Parse(creditCardTextBox.Text);
            double totalCashExp = double.Parse(groceriesTextBox.Text) + double.Parse(miscCashTextBox.Text)
                           + double.Parse(recreationTextBox.Text);
            return totalBills + totalCashExp;
        }

        private bool TextBoxValidate()
        {
            bool _incorrectValue = false;
            // Loop through both group boxes and check for empty strings
            foreach (GroupBox groupBox in Controls.OfType<GroupBox>())
            {
                foreach (TextBox textbox in groupBox.Controls.OfType<TextBox>())
                {
                    if (textbox.Text == "")
                    {
                        _incorrectValue = true;
                    }
                    // Tests for non-parseable value
                    else if(!double.TryParse(textbox.Text, out double test))
                    {
                        _incorrectValue = true;
                    }
                }   
            }
            return _incorrectValue;
        }
    }
}
