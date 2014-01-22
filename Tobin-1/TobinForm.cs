/* Project:     Project 1
 * Programmer:  Chris Tobin
 * Due Date:    2.15.2010
 * Description: A program to record the calendar orders of customers to simplify Carl's Calendars 
 *              ordering services.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tobin_1
{
    public partial class TobinForm : Form
    {
        // Declare the constants
        const decimal WALL_CALENDAR_SMALL_Decimal = 11.95m;
        const decimal WALL_CALENDAR_LARGE_Decimal = 19.95m;
        const decimal DESK_CALENDAR_FLIP_STYLE_Decimal = 9.95m;
        const decimal DESK_CALENDAR_BLOTTER_STYLE_Decimal = 12.95m;
        const decimal SALES_TAX_RATE_Decimal = 0.07m;

        public TobinForm()
        {
            InitializeComponent();
        }

        private void TobinForm_Load(object sender, EventArgs e)
        {
            // Display calendar prices in labels when the program starts
            wallCalendarSmallPriceLabel.Text = WALL_CALENDAR_SMALL_Decimal.ToString("C");
            wallCalendarLargePriceLabel.Text = WALL_CALENDAR_LARGE_Decimal.ToString("C");
            deskCalendarFlipStylePriceLabel.Text = DESK_CALENDAR_FLIP_STYLE_Decimal.ToString("C");
            deskCalendarBlotterStylePriceLabel.Text = DESK_CALENDAR_BLOTTER_STYLE_Decimal.ToString("C");
            
            // Enter quantity of 0 for each calendar when the program starts
            wallCalendarSmallQuantityTextBox.Text = "0";
            wallCalendarLargeQuantityTextBox.Text = "0";
            deskCalendarFlipStyleQuantityTextBox.Text = "0";
            deskCalendarBlotterStyleQuantityTextBox.Text = "0";
            
            // Disable the check payment options when the program starts
            checkNumberLabel.Enabled = false;
            checkNumberTextBox.Enabled = false;
            cashRadioButton.Checked = true;

        }
        
        private void calculateButton_Click(object sender, EventArgs e)
        {
            // Declare the variables
            int smallInteger, largeInteger, flipInteger, blotterInteger;
            int qtyInteger = 0;
            decimal orderTotalDecimal, salesTaxDecimal;
            decimal productSubtotalDecimal = 0;

            // Determine which calendars were selected
            try
            {
                // Convert the small wall calendar quantity to an integer
                smallInteger = int.Parse(wallCalendarSmallQuantityTextBox.Text);    
               
                /* If the small wall calendar is selected, multiply the quantity desired by
                 * the price. Then add the quantity of the calendar to the running total.
                 */
                if (wallCalendarSmallCheckBox.Checked)
                {
                    productSubtotalDecimal = (WALL_CALENDAR_SMALL_Decimal * smallInteger);
                    qtyInteger = smallInteger;
                }
                try
                {
                    // Convert the large wall calendar quantity to an integer
                    largeInteger = int.Parse(wallCalendarLargeQuantityTextBox.Text);
                    
                    /* If the large wall calendar is selected, multiply the quantity desired by
                     * the price. Then add the quantity of the calendar to the running total.
                     */
                    if (wallCalendarLargeCheckBox.Checked)
                    {
                        productSubtotalDecimal += (WALL_CALENDAR_LARGE_Decimal * largeInteger);
                        qtyInteger += largeInteger;
                    }
                    try
                    {
                        // Convert the flip desk calendar quantity to an integer
                        flipInteger = int.Parse(deskCalendarFlipStyleQuantityTextBox.Text);

                        /* If the flip style desk calendar is selected, multiply the quantity desired by
                         * the price. Then add the quantity of the calendar to the running total.
                         */
                        if (deskCalendarFlipStyleCheckBox.Checked)
                        {
                            productSubtotalDecimal += (DESK_CALENDAR_FLIP_STYLE_Decimal * flipInteger);
                            qtyInteger += flipInteger;
                        }
                        try
                        {
                            // Convert the blotter style desk calendar quantity to an integer
                            blotterInteger = int.Parse(deskCalendarBlotterStyleQuantityTextBox.Text);

                            /* If the blotter style calendar is selected, multiply the quantity desired by
                             * the price. Then add the quantity of the calendar to the running total.
                             */
                            if (deskCalendarBlotterStyleCheckBox.Checked)
                            {
                                productSubtotalDecimal += (DESK_CALENDAR_BLOTTER_STYLE_Decimal * blotterInteger);
                                qtyInteger += blotterInteger;
                            }
                            // Calculate and display the totals
                            productSubtotalTextBox.Text = productSubtotalDecimal.ToString("C");
                            salesTaxDecimal = (productSubtotalDecimal * SALES_TAX_RATE_Decimal);
                            salesTaxTextBox.Text = salesTaxDecimal.ToString("C");
                            orderTotalDecimal = (productSubtotalDecimal + salesTaxDecimal);
                            orderTotalTextBox.Text = orderTotalDecimal.ToString("C");
                            totalCalendarsOrderedTextBox.Text = qtyInteger.ToString("D");
                        }
                        catch (FormatException)
                        {
                            // Handle missing or non-numeric data for the blotter style desk calendar
                            MessageBox.Show("Quantity of calendars must be numeric. Enter 0 if none are desired.",
                                "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            deskCalendarBlotterStyleQuantityTextBox.Focus();
                            deskCalendarBlotterStyleQuantityTextBox.SelectAll();
                        }
                    }
                    catch (FormatException)
                    {
                        // Handle missing or non-numeric data for the flip style desk calendar
                        MessageBox.Show("Quantity of calendars must be numeric. Enter 0 if none are desired.",
                            "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        deskCalendarFlipStyleQuantityTextBox.Focus();
                        deskCalendarFlipStyleQuantityTextBox.SelectAll();
                    }
                }
                catch (FormatException)
                {
                    // Handle missing or non-numeric data for the large wall calendar
                    MessageBox.Show("Quantity of calendars must be numeric. Enter 0 if none are desired.",
                        "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    wallCalendarLargeQuantityTextBox.Focus();
                    wallCalendarLargeQuantityTextBox.SelectAll();
                }
                
            }
            catch (FormatException)
            {
                // Handle missing or non-numeric data for the small wall calendar
                MessageBox.Show("Quantity of calendars must be numeric. Enter 0 if none are desired.",
                    "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wallCalendarSmallQuantityTextBox.Focus();
                wallCalendarSmallQuantityTextBox.SelectAll();
            }
            

        }

        private void checkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // If the check radio button is checked, display the account number text box
            if (checkRadioButton.Checked)
            {
                checkNumberLabel.Enabled = true;
                checkNumberTextBox.Enabled = true;
                checkNumberTextBox.Focus();
            }
            else
            {
                checkNumberLabel.Enabled = false;
                checkNumberTextBox.Enabled = false;
                checkNumberTextBox.Text = "";
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            // Clear the form
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            phoneNumberMaskedTextBox.Text = "";
            orderDateMaskedTextBox.Text = "";
            wallCalendarSmallCheckBox.Checked = false;
            wallCalendarLargeCheckBox.Checked = false;
            deskCalendarFlipStyleCheckBox.Checked = false;
            deskCalendarBlotterStyleCheckBox.Checked = false;
            wallCalendarSmallQuantityTextBox.Text = "0";
            wallCalendarLargeQuantityTextBox.Text = "0";
            deskCalendarFlipStyleQuantityTextBox.Text = "0";
            deskCalendarBlotterStyleQuantityTextBox.Text = "0";
            cashRadioButton.Checked = true;
            tvAdvertisementCheckBox.Checked = false;
            radioAdvertisement.Checked = false;
            magazineAdvertisementRadioButton.Checked = false;
            friendOrRelativeRadioButton.Checked = false;
            productSubtotalTextBox.Text = "";
            salesTaxTextBox.Text = "";
            orderTotalTextBox.Text = "";
            totalCalendarsOrderedTextBox.Text = "";
            quitButton.Focus();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            // End the program
            this.Close();
        }

        
    }
}
