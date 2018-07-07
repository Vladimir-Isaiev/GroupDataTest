using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupDataTest
{
    public partial class Form1 : Form
    {
        Repo repo;
        

        int currentFilter;

        string[] filters;

        string subcommand;
        string command;

        public Form1()
        {
            InitializeComponent();
            repo = new Repo();
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            this.dataGridView1.Columns["Id"].Visible = false;
        }

        string SqlCommand()
        {
            command = "SELECT ";
            subcommand = "";

            if (filters[0] == "")
            {
                command = @"SELECT * FROM Shipments";
            }
            else
            {
                for(int i = 0; i<=currentFilter; i++)
                {
                    subcommand += filters[i];
                }

                subcommand = subcommand.Remove(subcommand.Length-1, 1);//удаляем последнюю запятую
                command = "SELECT " + subcommand + ", Sum(Quantity) as Quantity, Sum(Sum) as Sum FROM Shipments GROUP BY " + subcommand;
            }
            return command;
        }

        void FillDataGrid()
        {
            command = SqlCommand();
            DataTable table;
            try
            {
                table = repo.GetData(command);
                dataGridView1.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        

        void Reset()
        {
            checkBoxDate.Checked = false;
            checkBoxOrganization.Checked = false;
            checkBoxCity.Checked = false;
            checkBoxCountry.Checked = false;
            checkBoxManager.Checked = false;

            filters = new string[5];
            filters[0] = "";
            filters[1] = "";
            filters[2] = "";
            filters[3] = "";
            filters[4] = "";
            currentFilter = -1;
            subcommand = "";
            command = "";
        }

        

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Reset();
            FillDataGrid();
            this.dataGridView1.Columns["Id"].Visible = false;
        }

        private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxDate.Checked)
            {
                currentFilter++;
                filters[currentFilter] = " Date,";
            }
            else
            {
                Reset();
            }
            
        }

        private void checkBoxOrganization_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOrganization.Checked)
            {
                currentFilter++;
                filters[currentFilter] = " Organization,";
            }
            else
            {
                Reset();
            }

        }

        private void checkBoxCity_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCity.Checked)
            {
                currentFilter++;
                filters[currentFilter] = " City,";
            }
            else
            {
                Reset();
            }

        }

        private void checkBoxCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCountry.Checked)
            {
                currentFilter++;
                filters[currentFilter] = " Country,";
            }
            else
            {
                Reset();
            }

        }

        private void checkBoxManager_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManager.Checked)
            {
                currentFilter++;
                filters[currentFilter] = " Manager,";
            }
            else
            {
                Reset();
            }

        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            FillDataGrid();
        }
    }
}
