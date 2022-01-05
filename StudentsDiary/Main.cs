using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        static public readonly List<string> _listOfGrups = new List<string>
        {
            "Wszystkie",
            "Pierwsza",
            "Druga",
            "Trzecia",
            "Czwarta",
            "Piąta",
            "Szósta",
            "Siódma",
            "Ósma",
        };

        public Main()
        {
            InitializeComponent();
            cbListOfGrups.DataSource = _listOfGrups;
            RefreshDiary();
            SetColumsHeader();
        }

        void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            if (cbListOfGrups.Text != "Wszystkie")
                students.RemoveAll(x => x.IdGrup != cbListOfGrups.Text);
            dgvDiary.DataSource = students;
        }

        void SetColumsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Nr ID";
            dgvDiary.Columns[1].HeaderText = "Klasa";
            dgvDiary.Columns[2].HeaderText = "Imię";
            dgvDiary.Columns[3].HeaderText = "Nazwisko";
            dgvDiary.Columns[4].HeaderText = "Matematyka";
            dgvDiary.Columns[5].HeaderText = "Technologia";
            dgvDiary.Columns[6].HeaderText = "Fizyka";
            dgvDiary.Columns[7].HeaderText = "Język polski";
            dgvDiary.Columns[8].HeaderText = "Język obcy";
            dgvDiary.Columns[9].HeaderText = "Zajęcia dodatkowe";
            dgvDiary.Columns[10].HeaderText = "Uwagi";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDiary.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Proszę zaznacz ucznia, którego chcesz edytować");
                    return;
                }
                var addEditStudent = new AddEditStudent(Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
                addEditStudent.FormClosing += AddEditStudent_FormClosing;
                addEditStudent.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message} {ex.Source} ");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego chcesz usunąć");
                return;
            }
            var selectedStudent = dgvDiary.SelectedRows[0];

            var confirmDelete =
            MessageBox.Show($"Czy napewno chcesz usunąć ucznia" +
                $" {selectedStudent.Cells[2].Value.ToString() + " " + selectedStudent.Cells[3].Value.ToString().Trim()}",
                "Usuwanie ucznia",
                MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                StudentDelete(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }

        private void StudentDelete(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.SerializeToFile(students);
        }

        private void cbListOfGrups_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}
