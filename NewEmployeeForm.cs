using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;


namespace HRManagementSystem
{
    public partial class NewEmployeeForm : Form
    {
        #region Variables
        public string employeeID,
                      firstName,
                      middleName,
                      lastName,
                      gender,
                      maritalStatus,
                      nationality,
                      currentAddress,
                      permanentAddress,

                      fileName,
        
                      globe,
                      smart,
                      age,
                      email,

                      tin,
                      sss,
                      pagibig,
                      philhealth,

                      position,
                      department,
                      branch,
                      head,

                      graduate,
                      college,
                      seniorHigh,
                      juniorHigh,
                      elementary;

        public DateTime dob,
                        startDate;

        public byte picture;
        public bool validation;

        private BasicInfo basicInfo = new BasicInfo();
        private ContactInfo contactInfo = new ContactInfo();
        private GovernmentID governmentID = new GovernmentID();
        private CPosition cPosition = new CPosition();
        private Education education = new Education();
        private MainForm _mainForm;

        private List<BasicInfo> _basicInfo = new List<BasicInfo>();
        private List<CPosition> _cPosition = new List<CPosition>();
        #endregion



        #region Constructors
        public NewEmployeeForm()
        {
            InitializeComponent();
            ComboBoxLoad();
            employeeIDTB.Text = basicInfo.CreateEmployeeID();
        }

        public NewEmployeeForm(MainForm mainForm)
        {
            InitializeComponent();
            employeeIDTB.Text = basicInfo.CreateEmployeeID();

        }
        #endregion



        #region others
        public void InitializeFields()
        {
            employeeIDTB.Text = "";
            firstNameTB.Text = "";
            middleNameTB.Text = "";
            lastNameTB.Text = "";
            dobDTP.Value = DateTime.Now;
            genderCB.Text = "";
            maritalStatusCB.Text = "";
            nationalityTB.Text = "";
            currentAddressTB.Text = "";
            permanentAddressTB.Text = "";

            globeTB.Text = "";
            smartTB.Text = "";
            emailTB.Text = "";

            tinTB.Text = "";
            sssTB.Text = "";
            pagibigTB.Text = "";
            philhealthTB.Text = "";

            positionCB.Text = "";
            departmentCB.Text = "";
            branchCB.Text = "";
            headCB.Text = "";
            startDateDTP.Value = DateTime.Now;

            graduateTB.Text = "";
            collegeTB.Text = "";
            seniorHighTB.Text = "";
            juniorHighTB.Text = "";
            elementaryTB.Text = "";
        }
        private void ShowData(string employeeID)
        {
            basicInfo = basicInfo.RetrieveBasicInfo(employeeID);
            employeeIDTB.Text = basicInfo.EmployeeID;
            firstNameTB.Text = basicInfo.FirstName;
            middleNameTB.Text = basicInfo.MiddleName;
            lastNameTB.Text = basicInfo.LastName;
            dobDTP.Value = basicInfo.DOB;
            genderCB.Text = basicInfo.Gender;
            maritalStatusCB.Text = basicInfo.MaritalStatus;
            nationalityTB.Text = basicInfo.Nationality;
            currentAddressTB.Text = basicInfo.CurrentAddress;
            permanentAddressTB.Text = basicInfo.PermanentAddress;
            picturePB.Image = Image.FromFile(basicInfo.Picture.ToString());

            contactInfo = contactInfo.RetrieveContactInfo(employeeID);
            employeeIDTB.Text = contactInfo.EmployeeID;
            globeTB.Text = contactInfo.Globe;
            smartTB.Text = contactInfo.Smart;
            emailTB.Text = contactInfo.Email;

            governmentID = governmentID.RetrieveGovernmentID(employeeID);
            employeeIDTB.Text = governmentID.EmployeeID;
            tinTB.Text = governmentID.TIN;
            sssTB.Text = governmentID.SSS;
            pagibigTB.Text = governmentID.PAGIBIG;
            philhealthTB.Text = governmentID.PhilHealth;

            cPosition = cPosition.RetrieveCPosition(employeeID);
            employeeIDTB.Text = cPosition.EmployeeID;
            positionCB.Text = cPosition.Position;
            departmentCB.Text = cPosition.Department;
            branchCB.Text = cPosition.Branch;
            headCB.Text = cPosition.Head;
            startDateDTP.Value = cPosition.StartDate;

            education = education.RetrieveEducation(employeeID);
            employeeIDTB.Text = education.EmployeeID;
            graduateTB.Text = education.EduAttainment;
            collegeTB.Text = education.College;
            seniorHighTB.Text = education.SeniorHigh;
            juniorHighTB.Text = education.JuniorHigh;
            elementaryTB.Text = education.Elementary;
        }
        private void ComboBoxLoad()
        {
            _basicInfo = basicInfo.RetrieveBasicInfoList();
            foreach (BasicInfo b in _basicInfo)
            {
                genderCB.Items.Add(b.Gender);
                maritalStatusCB.Items.Add(b.MaritalStatus);
            }

            _cPosition = cPosition.RetrieveCPositionList();
            foreach (CPosition cp in _cPosition)
            {
                headCB.Items.Add(cp.Head);
                branchCB.Items.Add(cp.Branch);
                departmentCB.Items.Add(cp.Department);
                positionCB.Items.Add(cp.Position);
            }

        }
      /*  private bool Validate()
        {
            if(lastNameTB.Text == "" || firstNameTB.Text == "" || int.Parse(ageTB.Text) < 18 || emailTB.Text == "" || positionCB.SelectedIndex == -1 || departmentCB.SelectedIndex == -1 || branchCB.SelectedIndex == -1)
            {
                validation = false;
            }
            else
            {
                validation = true;
            }
            return Validate();
        }*/
        #endregion



        #region Date Time Picker
        private void dobDTP_ValueChanged(object sender, EventArgs e)
        {
            int age = DateTime.Today.Year - dobDTP.Value.Year;
            ageTB.Text = age.ToString();
            if (age < 18)
            {
                MessageBox.Show("The new Employee is under 18 years old.");
                return;
            }
        }
        private void startDateDTP_ValueChanged(object sender, EventArgs e)
        {
            int losYear = DateTime.Today.Year - startDateDTP.Value.Year;
            int losMonth = DateTime.Today.Month - startDateDTP.Value.Month;
            int losDay = DateTime.Today.Day - startDateDTP.Value.Day;

            losTB.Text = (losYear.ToString() + " Year(s)") + " , " + (losMonth.ToString() + " Month(s)") + " , " + (losDay.ToString() + " Day(s)");
        }
        #endregion






        private void picturePB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg;*.jpeg;.*.gif;*.png)|*.jpg;*.jpeg;.*.gif;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureTB.Text = openFileDialog.FileName;
                picturePB.Image = Image.FromFile(openFileDialog.FileName);
            }
            Show();
        }
        public byte[] ImageToByteArray(Image image)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }





        #region Text Box TextChange
        private void genderCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void maritalStatusCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void globeTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void smartTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void emailTB_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (emailTB.Text.Length > 0 && emailTB.Text.Trim().Length != 0)
            {
                if (!rEmail.IsMatch(emailTB.Text.Trim()))
                {
                    MessageBox.Show("Use a Valid Email ID");
                    emailTB.SelectAll();
                    e.Cancel = true;
                }
            }
        }
        #endregion




        #region Buttons
        private void saveB_Click(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                FileStream fs;
                BinaryReader br;
                try
                {
                    employeeID = employeeIDTB.Text.Trim();
                    firstName = firstNameTB.Text.Trim();
                    middleName = middleNameTB.Text.Trim();
                    lastName = lastNameTB.Text.Trim();
                    dob = dobDTP.Value;
                    gender = genderCB.Text.Trim();
                    maritalStatus = maritalStatusCB.Text.Trim();
                    nationality = nationalityTB.Text.Trim();
                    currentAddress = currentAddressTB.Text.Trim();
                    permanentAddress = permanentAddressTB.Text.Trim();
                    //picture = byte.Parse(picturePB.Image.SaveAdd());

                    /*  string FileName = pictureTB.Text;
                      byte[] Picture;
                      fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                      br = new BinaryReader(fs);
                      Picture = br.ReadBytes((int)fs.Length);
                      br.Close();
                      fs.Close();*/

                    globe = globeTB.Text.Trim();
                    smart = smartTB.Text.Trim();
                    email = emailTB.Text.Trim();

                    tin = tinTB.Text.Trim();
                    sss = sssTB.Text.Trim();
                    pagibig = pagibigTB.Text.Trim();
                    philhealth = philhealthTB.Text.Trim();

                    position = positionCB.Text.Trim();
                    department = departmentCB.Text.Trim();
                    branch = branchCB.Text.Trim();
                    head = headCB.Text.Trim();
                    startDate = startDateDTP.Value;

                    graduate = graduateTB.Text.Trim();
                    college = collegeTB.Text.Trim();
                    seniorHigh = seniorHighTB.Text.Trim();
                    juniorHigh = juniorHighTB.Text.Trim();
                    elementary = elementaryTB.Text.TrimStart();

                    basicInfo = new BasicInfo(employeeID,
                                              firstName,
                                              middleName,
                                              lastName,
                                              dob,
                                              gender,
                                              maritalStatus,
                                              nationality,
                                              currentAddress,
                                              permanentAddress,
                                              picture);

                    contactInfo = new ContactInfo(employeeID,
                                                  globe,
                                                  smart,
                                                  email);

                    governmentID = new GovernmentID(employeeID,
                                                    tin,
                                                    sss,
                                                    pagibig,
                                                    philhealth);

                    cPosition = new CPosition(employeeID,
                                              position,
                                              department,
                                              branch,
                                              head,
                                              startDate);

                    education = new Education(employeeID,
                                              graduate,
                                              college,
                                              seniorHigh,
                                              juniorHigh,
                                              elementary);

                    basicInfo.InsertBasicInfo(basicInfo);
                    contactInfo.InsertContactInfo(contactInfo);
                    governmentID.InsertGovernmentID(governmentID);
                    cPosition.InsertCPosition(cPosition);
                    education.InsertEducation(education);

                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void cancelB_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void clearB_Click(object sender, EventArgs e)
        {
            InitializeFields();
        }
        #endregion
    }
}
