using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private int _studentId;
        private Student _student;

        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        private List<string> _listOfGrup = ModifyList(Main._listOfGrups);

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            cbIdGrup.DataSource = _listOfGrup;
            _studentId = id;
            GetStudentData();
            tbFirstName.Select();
        }

        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edycja ucznia";
                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                    throw new Exception("Brak użytkownika o podanym ID");

                FillTextBoxes();
            }
        }
        private static List<string> ModifyList(List<string> list)
        {
            list.RemoveAll(x => x == "Wszystkie");
            return list;
        }

        private void FillTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastname.Text = _student.LastName;
            tbMath.Text = _student.Math;
            tbTechno.Text = _student.Technology;
            tbPhysics.Text = _student.Physics;            
            tbPolishLang.Text = _student.PolishLang;
            tbForeignLang.Text = _student.ForeignLang;
            cbExtraActivities.Checked = _student.ExtraActivities;
            rtbComment.Text = _student.Comments;
            cbIdGrup.Text = _student.IdGrup;            
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
                students.RemoveAll(x => x.Id == _studentId);
            else
                AssignIdToNewStudent(students);

            AddNewUserToList(students);
            _fileHelper.SerializeToFile(students);
            Close();
        }

        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastname.Text,
                ForeignLang = tbForeignLang.Text,
                PolishLang = tbPolishLang.Text,
                Physics = tbPhysics.Text,
                Math = tbMath.Text,
                Technology = tbTechno.Text,
                Comments = rtbComment.Text,
                ExtraActivities = cbExtraActivities.Checked,
                IdGrup = cbIdGrup.Text
                
            };
            students.Add(student);
        }

        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students.
                OrderByDescending(x => x.Id).FirstOrDefault();

            _studentId = studentWithHighestId == null ?
                1 : studentWithHighestId.Id + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
