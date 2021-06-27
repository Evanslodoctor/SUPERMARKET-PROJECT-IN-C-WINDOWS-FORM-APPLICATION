using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace supermarket
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtcategoryID.Text = catDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtcategorytName.Text= catDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtcategoryDescription.Text = catDGV.SelectedRows[0].Cells[2].Value.ToString();
        }
        static string myString = @"Data Source=DESKTOP-EVC6IPJ;Initial Catalog=SUPERMAKET;Integrated Security=True";
        SqlConnection cnn = new SqlConnection(myString);

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                string query = "insert into CATEGORY values(" +txtcategoryID.Text+ ",' " +txtcategorytName.Text+ " ','" +txtcategoryDescription.Text+ "')";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category inserted successfully");

                cnn.Close();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void Populate()
        {
            try
            {
                cnn.Open();
                string query2 = "select * form CATEGORY";
                SqlDataAdapter sda = new SqlDataAdapter(query2, cnn);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                catDGV.DataSource = ds.Tables[0];

                cnn.Close();
            } catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            //Populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcategoryID.Text =="")
                {
                    MessageBox.Show("Select Category to Delete");
                }
                else
                {
                    cnn.Open();
                    string query = "delete from CATEGORY where categoryID= " + txtcategoryID.Text + "";
                    SqlCommand cmd = new SqlCommand(query,cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category was deleted succesfully");
                    cnn.Close();
                    Populate();

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcategoryID.Text == "" || txtcategorytName.Text == "" || txtcategoryDescription.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    cnn.Open();
                    string query = "update CATEGORY set categoryName='" + txtcategorytName + "',categoryDescription='" + txtcategoryDescription.Text + "', where categoryID=" + txtcategoryID.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category updated successfully");
                    cnn.Close();
                    Populate();
         
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
