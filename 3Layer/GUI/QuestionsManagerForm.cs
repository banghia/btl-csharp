using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _3Layer.BLL;
using _3Layer.DTO;

namespace _3Layer.GUI
{
    public partial class QuestionsManagerForm : Form
    {
        QuestionBLL questionBLL = new QuestionBLL();

        public QuestionsManagerForm()
        {
            InitializeComponent();
        }

  

        private void QuestionsManagerForm_Load(object sender, EventArgs e)
        {
            List<Question> listQuestion = questionBLL.getAll();
            listQuestion.ForEach(delegate(Question question) {
                dataGridView1.Rows.Add(new String[]{
                    question.Id.ToString(),
                    question.Content,
                    question.A,
                    question.B,
                    question.C,
                    question.D,
                    question.Correct.ToString(),
                    question.Level.ToString()
                });
            });
            cmbLevel.SelectedIndex = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;
            if (rowIndex >= 0 && rowIndex!=dataGridView1.Rows.Count)
            {
                txbID.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                txbContent.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                txbA.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                txbB.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
                txbC.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
                txbD.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbContent.Text) || string.IsNullOrWhiteSpace(txbA.Text) || string.IsNullOrWhiteSpace(txbB.Text)
                || string.IsNullOrWhiteSpace(txbC.Text) || string.IsNullOrWhiteSpace(txbD.Text))
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin câu hỏi");
                return;
            }
            Question question = new Question();
            question.Content = txbContent.Text;
            question.A = txbA.Text;
            question.B = txbA.Text;
            question.C = txbA.Text;
            question.D = txbA.Text;
            if (rdbA.Checked) {
                question.Correct = 'A';
            }
            else if (rdbB.Checked)
            {
                question.Correct = 'B';
            }
            else if (rdbC.Checked)
            {
                question.Correct = 'C';
            }
            else if (rdbD.Checked)
            {
                question.Correct = 'D';
            }
            question.Level = int.Parse(cmbLevel.Text);
            question.CatagoryId = 1;
            if (questionBLL.Insert(question))
            {
                MessageBox.Show("Thêm câu hỏi thành công");
            }
            else {
                MessageBox.Show("Thêm câu hỏi thất bại");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            if (string.IsNullOrWhiteSpace(txbID.Text) || !int.TryParse(txbID.Text, out id))
            {
                MessageBox.Show("Lỗi id");
                return;
            }
            if (questionBLL.Delete(id))
            {
                MessageBox.Show("Xoa thanh cong");
            }
            else {
                MessageBox.Show("Xoa that bai");
            }
        }
    }
}
