using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");
        private int _studentId;

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;
            if (id != 0)
            {
                var students = DeserializeFromFile();
                var student = students.FirstOrDefault(x => x.Id == id);

                if (student == null)
                    throw new Exception("Brak użytkownika o podanym ID");

                tbId.Text = student.Id.ToString();
                tbFirstName.Text = student.FirstName;
                tbLastname.Text = student.LastName;
                rtbComment.Text = student.Comments;
                tbForeignLang.Text = student.ForeignLang;
                tbPolishLang.Text = student.PolishLang;
                tbPhysics.Text = student.Physics;
                tbMath.Text = student.Math;
                tbTechno.Text = student.Technology;
            }
            tbFirstName.Select();
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {
            var students = DeserializeFromFile();
            if (_studentId != 0)
            {
                students.RemoveAll(x => x.Id == _studentId);
            }
            else
            {
                var studentWithHighestId = students.OrderByDescending(x => x.Id).FirstOrDefault();

                _studentId = studentWithHighestId == null ?
                    1 : studentWithHighestId.Id + 1;
            }

            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastname.Text,
                Comments = rtbComment.Text,
                ForeignLang = tbForeignLang.Text,
                PolishLang = tbPolishLang.Text,
                Physics = tbPhysics.Text,
                Math = tbMath.Text,
                Technology = tbTechno.Text
            };

            students.Add(student);
            SerializeToFile(students);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();

        }
        public void SerializeToFile(List<Student> students)
        {
            var serializer = new XmlSerializer(typeof(List<Student>));

            using (var streanWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streanWriter, students);
                streanWriter.Close();
            }
        }

        public List<Student> DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<Student>();

            var serializer = new XmlSerializer(typeof(List<Student>));

            using (var streamReader = new StreamReader(_filePath))
            {
                var students = (List<Student>)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }
        }
    }
}
